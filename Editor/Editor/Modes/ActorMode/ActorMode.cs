using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WindEditor.View;
using WindEditor.ViewModel;
using System.Windows.Input;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using WindEditor.Serialization;

namespace WindEditor.Editor.Modes
{
    public partial class ActorMode : IEditorMode
    {
        public ICommand CutSelectionCommand { get { return new RelayCommand(x => CutSelection(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand CopySelectionCommand { get { return new RelayCommand(x => CopySelection(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand PasteSelectionCommand { get { return new RelayCommand(x => PasteSelection(), (x) => true); } }
        public ICommand DeleteSelectionCommand { get { return new RelayCommand(x => DeleteSelection(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand SelectAllCommand { get { return new RelayCommand(x => SelectAll(), (x) => true); } }
        public ICommand SelectNoneCommand { get { return new RelayCommand(x => SelectNone(), (x) => EditorSelection.SelectedObjects.Count > 0); } }
        public ICommand CreateEntityCommand { get { return new RelayCommand(EntityFourCC => CreateEntity()); } }

        private DockPanel m_ModeControlsDock;
        private WDetailsViewViewModel m_DetailsViewModel;

        public DockPanel ModeControlsDock
        {
            get { return m_ModeControlsDock; }
            set
            {
                if (value != m_ModeControlsDock)
                {
                    m_ModeControlsDock = value;
                    OnPropertyChanged("ModeControlsDock");
                }
            }
        }
        public WDetailsViewViewModel DetailsViewModel
        {
            get { return m_DetailsViewModel; }
            set
            {
                if (value != m_DetailsViewModel)
                {
                    m_DetailsViewModel = value;
                    OnPropertyChanged("DetailsViewModel");
                }
            }
        }
        public Selection<WDOMNode> EditorSelection { get; set; }
        public WWorld World { get; }

        public event EventHandler<GenerateUndoEventArgs> GenerateUndoEvent;

        public ActorMode(WWorld world)
        {
            World = world;
            TransformGizmo = new WTransformGizmo(world);

            EditorSelection = new Selection<WDOMNode>(this);
            EditorSelection.OnSelectionChanged += OnSelectionChanged;

            DetailsViewModel = new WDetailsViewViewModel();

            ModeControlsDock = CreateUI();
        }

        public void Update(WSceneView view)
        {
            UpdateSelectionGizmo(view);

            // If we have a gizmo and we're transforming it, don't check for selection change.
            if (TransformGizmo != null && TransformGizmo.IsTransforming)
                return;
            if (WInput.GetMouseButtonDown(0) && !WInput.GetMouseButton(1))
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                var addedActor = Raycast(mouseRay);

                // Check the behaviour of this click to determine appropriate selection modification behaviour.
                // Click w/o Modifiers = Clear Selection, add result to selection
                // Click /w Ctrl = Toggle Selection State
                // Click /w Shift = Add to Selection
                bool ctrlPressed = WInput.GetKey(Key.LeftCtrl) || WInput.GetKey(Key.RightCtrl);
                bool shiftPressed = WInput.GetKey(Key.LeftShift) || WInput.GetKey(Key.RightShift);

                if (!ctrlPressed & !shiftPressed)
                {
                    foreach (WDOMNode node in EditorSelection.SelectedObjects)
                    {
                        node.IsSelected = false;
                    }
                    EditorSelection.ClearSelection();

                    if (addedActor != null)
                    {
                        EditorSelection.AddToSelection(addedActor);
                        addedActor.IsSelected = true;
                    }
                }
                else if (addedActor != null && (ctrlPressed && !shiftPressed))
                {
                    if (addedActor.IsSelected)
                    {
                        EditorSelection.RemoveFromSelection(addedActor);
                        addedActor.IsSelected = false;
                    }
                    else
                    {
                        EditorSelection.AddToSelection(addedActor);
                        addedActor.IsSelected = true;
                    }
                }
                else if (addedActor != null && shiftPressed)
                {
                    if (!EditorSelection.SelectedObjects.Contains(addedActor))
                    {
                        EditorSelection.AddToSelection(addedActor);
                        addedActor.IsSelected = true;
                    }
                }

                UpdateGizmoTransform();
            }

            // Add our gizmo to the renderer this frame.
            ((IRenderable)TransformGizmo).AddToRenderer(view);
        }

        private WDOMNode Raycast(FRay ray)
        {
            if (World.Map == null)
                return null;

            WDOMNode closestResult = null;
            float closestDistance = float.MaxValue;

            foreach (var scene in World.Map.SceneList)
            {
                var allActors = scene.GetChildrenOfType<VisibleDOMNode>();

                foreach (VisibleDOMNode actorNode in allActors)
                {
                    if (!actorNode.ShouldBeRendered())
                        continue;
                    float intersectDistance;
                    bool hitActor = actorNode.Raycast(ray, out intersectDistance);
                    if (hitActor)
                    {
                        if (intersectDistance >= 0 && intersectDistance < closestDistance)
                        {
                            closestDistance = intersectDistance;
                            closestResult = actorNode;
                        }
                    }
                }
            }

            return closestResult;
        }

        public void FilterSceneForRenderer(WSceneView view, WWorld world)
        {
            foreach (WScene scene in world.Map.SceneList)
            {
                foreach (var renderable in scene.GetChildrenOfType<IRenderable>())
                    renderable.AddToRenderer(view);
            }
        }

        public void BroadcastUndoEventGenerated(WUndoCommand command)
        {
            GenerateUndoEvent?.Invoke(this, new GenerateUndoEventArgs(command));
        }

        /// <summary>
        /// Generates a DockPanel that contains the controls specific to this editor mode.
        /// </summary>
        private DockPanel CreateUI()
        {
            DockPanel actor_dock_panel = new DockPanel();

            WDetailsView actor_details = new WDetailsView()
            {
                Name = "Details",
                DataContext = DetailsViewModel
            };

            GroupBox actor_prop_box = new GroupBox()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                Header = "Properties",
                Content = actor_details
            };

            DockPanel.SetDock(actor_prop_box, Dock.Top);

            actor_dock_panel.Children.Add(actor_prop_box);

            return actor_dock_panel;
        }

        public void OnBecomeActive()
        {

        }

        public void OnBecomeInactive()
        {

        }

        private void OnSelectionChanged()
        {
            // This will get invoked when an Undo happens which allows the gizmo to fix itself.
            UpdateGizmoTransform();

            if (EditorSelection.PrimarySelectedObject != null)
                DetailsViewModel.ReflectObject(EditorSelection.PrimarySelectedObject);
        }

        public void ClearSelection()
        {
            foreach (WDOMNode node in EditorSelection.SelectedObjects)
            {
                node.IsSelected = false;
            }
            EditorSelection.ClearSelection();
        }

        #region INotifyPropertyChanged Support
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        private void CutSelection()
        {
            CopySelection();
            DeleteSelection();
        }

        private void CopySelection()
        {
            JsonSerializer serial = new JsonSerializer();
            serial.Converters.Add(new StringEnumConverter());
            serial.Converters.Add(new Vector2Converter());
            serial.Converters.Add(new Vector3Converter());
            serial.Converters.Add(new QuaternionConverter());
            serial.Converters.Add(new WDOMNodeJsonConverter(World, EditorSelection.PrimarySelectedObject));
            serial.Formatting = Formatting.Indented;

            StringWriter sw = new StringWriter();
            serial.Serialize(sw, EditorSelection.SelectedObjects);

            Clipboard.SetText(sw.ToString());
        }

        private void PasteSelection()
        {
            WDOMNode selected = EditorSelection.PrimarySelectedObject;
            WDOMNode parent;

            if (selected is SerializableDOMNode)
                parent = selected.Parent;
            else if (selected is WDOMLayeredGroupNode)
                parent = selected;
            else if (selected is WDOMGroupNode)
                parent = selected;
            else
                return;

            JsonSerializer serial = new JsonSerializer();
            serial.Converters.Add(new StringEnumConverter());
            serial.Converters.Add(new Vector2Converter());
            serial.Converters.Add(new Vector3Converter());
            serial.Converters.Add(new QuaternionConverter());
            serial.Converters.Add(new WDOMNodeJsonConverter(World, parent));
            serial.Formatting = Formatting.Indented;

            List<SerializableDOMNode> pastedEntities;
            try
            {
                string text = Clipboard.GetText();
                StringReader sr = new StringReader(text);
                JsonTextReader reader = new JsonTextReader(sr);
                pastedEntities = serial.Deserialize<List<SerializableDOMNode>>(reader);
            } catch (Exception e)
            {
                string error = "Failed to paste entities from the clipboard.\n\nFull error:\n\n";
                error += e.GetType().FullName;
                error += e.Message;
                error += e.StackTrace;
                System.Windows.Forms.MessageBox.Show(
                    error,
                    "Failed to paste entities",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
                return;
            }

            pastedEntities = pastedEntities.Where(x => x != null).ToList(); // Remove entities that failed to convert
            if (pastedEntities.Count > 0)
            {
                WDOMNode[] entitiesToPaste = pastedEntities.ToArray();
                WDOMNode[] parents = new WDOMNode[entitiesToPaste.Length];
                for (int i = 0; i < entitiesToPaste.Length; i++)
                {
                    parents[i] = entitiesToPaste[i].Parent;
                }

                World.UndoStack.BeginMacro($"Paste {pastedEntities.Count} entities");
                var undoAction = new WCreateEntitiesAction(entitiesToPaste, parents);
                BroadcastUndoEventGenerated(undoAction);
                EditorSelection.ClearSelection();
                EditorSelection.AddToSelection(pastedEntities);
                World.UndoStack.EndMacro();
            }
            foreach (SerializableDOMNode node in pastedEntities) {
                node.IsSelected = true;
            }
        }

        private void DeleteSelection()
        {
            WDOMNode[] entitiesToDelete = EditorSelection.SelectedObjects.ToArray();

            World.UndoStack.BeginMacro($"Delete entities");
            var undoAction = new WDeleteEntitiesAction(entitiesToDelete);
            BroadcastUndoEventGenerated(undoAction);
            EditorSelection.ClearSelection();
            World.UndoStack.EndMacro();
        }

        private void SelectAll()
        {
            List<WDOMNode> actorsToSelect = new List<WDOMNode>();
            foreach (var scene in World.Map.SceneList)
            {
                var allActorsInScene = scene.GetChildrenOfType<VisibleDOMNode>();

                foreach (VisibleDOMNode actorNode in allActorsInScene)
                {
                    actorNode.IsSelected = true;
                    actorsToSelect.Add(actorNode);
                }
            }
            EditorSelection.AddToSelection(actorsToSelect);
        }

        private void SelectNone()
        {
            ClearSelection();
        }

        public void CreateEntity()
        {
            if (!EditorSelection.SingleObjectSelected)
                return;

            WDOMNode selected = EditorSelection.PrimarySelectedObject;
            SerializableDOMNode newNode;

            if (selected is SerializableDOMNode)
            {
                SerializableDOMNode origNode = selected as SerializableDOMNode;
                Type selType = selected.GetType();
                newNode = (SerializableDOMNode)Activator.CreateInstance(selType, origNode.FourCC, World);
                newNode.Name = origNode.Name;
                newNode.PostLoad();
                newNode.SetParent(selected.Parent);

                if (origNode.Parent is WDOMLayeredGroupNode)
                {
                    newNode.Layer = origNode.Layer;
                }
            }
            else if (selected is WDOMLayeredGroupNode)
            {

                WActorCreatorWindow actorCreator = new WActorCreatorWindow();
                actorCreator.ShowDialog();
                // TODO this is a temporary hack to get the actor name from the text input until the UI is properly coded
                string actorName = actorCreator.filter_box.Text;
                if (actorName == "")
                    return;
                Type actorType = WResourceManager.GetTypeByName(actorName);
                if (actorType == typeof(Actor))
                    return;

                WDOMLayeredGroupNode lyrNode = selected as WDOMLayeredGroupNode;
                string unlayedFourCC = lyrNode.FourCC.ToString();
                MapLayer layer = ChunkHeader.FourCCToLayer(ref unlayedFourCC);
                FourCC enumVal = FourCCConversion.GetEnumFromString(unlayedFourCC);

                newNode = (SerializableDOMNode)Activator.CreateInstance(actorType, enumVal, World);
                newNode.Name = actorName;
                newNode.Layer = layer;
                newNode.PostLoad();
                newNode.SetParent(lyrNode);
            }
            else if (selected is WDOMGroupNode)
            {
                WDOMGroupNode grpNode = selected as WDOMGroupNode;

                if (grpNode.FourCC == FourCC.ACTR || grpNode.FourCC == FourCC.SCOB || grpNode.FourCC == FourCC.TRES)
                    return;

                Type newObjType = FourCCConversion.GetTypeFromEnum(grpNode.FourCC);
                newNode = (SerializableDOMNode)Activator.CreateInstance(newObjType, grpNode.FourCC, World);
                newNode.PostLoad();
                newNode.SetParent(grpNode);
            }
            else
                return;

            if (newNode == null)
                return;

            WDOMNode[] entitiesToCreate = { newNode };
            WDOMNode[] parents = { newNode.Parent };

            World.UndoStack.BeginMacro($"Create {newNode.Name}");
            var undoAction = new WCreateEntitiesAction(entitiesToCreate, parents);
            BroadcastUndoEventGenerated(undoAction);
            EditorSelection.ClearSelection();
            EditorSelection.AddToSelection(newNode);
            World.UndoStack.EndMacro();
        }

        ~ActorMode()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    TransformGizmo.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
