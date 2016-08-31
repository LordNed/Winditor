using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WindEditor.Serialization;

namespace WindEditor
{
    public class WActorEditor : IDisposable
    {
        public WEditorSelectionAggregate SelectedObjects { get; protected set; }
        public ICommand CutSelectionCommand { get { return new RelayCommand(x => CutSelection(), (x) => m_selectionList.Count > 0); } }
        public ICommand CopySelectionCommand { get { return new RelayCommand(x => CopySelection(), (x) => m_selectionList.Count > 0); } }
        public ICommand PasteSelectionCommand { get { return new RelayCommand(x => PasteSelection(), (x) => AttemptToDeserializeObjectsFromClipboard() != null); } }
        public ICommand DeleteSelectionCommand { get { return new RelayCommand(x => DeleteSelection(), (x) => m_selectionList.Count > 0); } }
        public ICommand SelectAllCommand { get { return new RelayCommand(x => SelectAll(), (x) => true); } }
        public ICommand SelectNoneCommand { get { return new RelayCommand(x => SelectNone(), (x) => m_selectionList.Count > 0); } }
        public ICommand CreateEntityCommand { get { return new RelayCommand(EntityFourCC => CreateEntity(EntityFourCC as string)); } }

        private WWorld m_world;
        private enum SelectionType
        {
            Add,
            Remove
        }

        private WTransformGizmo m_transformGizmo;
        private BindingList<WActorNode> m_selectionList;

        // To detect redundant calls
        private bool m_hasBeenDisposed = false;


        public WActorEditor(WWorld world)
        {
            m_world = world;
            m_selectionList = new BindingList<WActorNode>();
            m_transformGizmo = new WTransformGizmo(m_world);
            SelectedObjects = new WEditorSelectionAggregate(m_selectionList);
        }

        public void UpdateGizmoTransform()
        {
            if(m_selectionList.Count > 0)
            {
                m_transformGizmo.SetPosition(m_selectionList[0].Transform.Position);
                m_transformGizmo.SetLocalRotation(m_selectionList[0].Transform.Rotation);
            }
        }

        public void CreateEntity(string fourCC)
        {
            if (m_world.Map == null || m_world.Map.FocusedScene == null)
                return;

            var actorDescriptors = new List<MapActorDescriptor>();
            foreach (var file in Directory.GetFiles("resources/templates/"))
            {
                MapActorDescriptor descriptor = JsonConvert.DeserializeObject<MapActorDescriptor>(File.ReadAllText(file));
                actorDescriptors.Add(descriptor);
            }

            MapActorDescriptor entityDescriptor = actorDescriptors.Find(x => string.Compare(x.FourCC, fourCC, true) == 0);
            if(entityDescriptor == null)
            {
                Console.WriteLine("Attempted to spawn unsupported FourCC: {0}", fourCC);
                return;
            }

            List<IPropertyValue> actorProperties = new List<IPropertyValue>();
            foreach(var field in entityDescriptor.Fields)
            {
                switch(field.FieldName)
                {
                    case "Position":
                    case "X Rotation":
                    case "Y Rotation":
                    case "Z Rotation":
                    case "X Scale":
                    case "Y Scale":
                    case "Z Scale":
                        continue;
                }

                IPropertyValue propValue = null;
                switch (field.FieldType)
                {
                    case PropertyValueType.Byte:
                        propValue = new TBytePropertyValue(0, field.FieldName);
                        break;
                    case PropertyValueType.Bool:
                        propValue = new TBoolPropertyValue(false, field.FieldName);
                        break;
                    case PropertyValueType.Short:
                        propValue = new TShortPropertyValue(0, field.FieldName);
                        break;
                    case PropertyValueType.Int:
                        propValue = new TIntPropertyValue(0, field.FieldName);
                        break;
                    case PropertyValueType.Float:
                        propValue = new TFloatPropertyValue(0f, field.FieldName);
                        break;
                    case PropertyValueType.FixedLengthString:
                    case PropertyValueType.String:
                        propValue = new TStringPropertyValue("", field.FieldName);
                        break;
                    case PropertyValueType.Vector2:
                        propValue = new TVector2PropertyValue(new OpenTK.Vector2(0f, 0f), field.FieldName);
                        break;
                    case PropertyValueType.Vector3:
                        propValue = new TVector3PropertyValue(new OpenTK.Vector3(0f, 0f, 0f), field.FieldName);
                        break;
                    case PropertyValueType.XRotation:
                    case PropertyValueType.YRotation:
                    case PropertyValueType.ZRotation:
                        propValue = new TShortPropertyValue(0, field.FieldName);
                        break;
                    case PropertyValueType.Color24:
                        propValue = new TLinearColorPropertyValue(new WLinearColor(1f, 1f, 1f), field.FieldName);
                        break;
                    case PropertyValueType.Color32:
                        propValue = new TLinearColorPropertyValue(new WLinearColor(1f, 1f, 1f, 1f), field.FieldName);
                        break;
                    default:
                        Console.WriteLine("Unsupported PropertyValueType: {0}", field.FieldType);
                        break;
                }

                propValue.SetUndoStack(m_world.UndoStack);
                actorProperties.Add(propValue);
            }

            var newActor = new WActorNode(fourCC, m_world);
            newActor.Transform.Position = new OpenTK.Vector3(0, 200, 0);

            newActor.SetParent(m_world.Map.FocusedScene);
            newActor.Properties.AddRange(actorProperties);
            newActor.PostFinishedLoad();

            ModifySelection(SelectionType.Add, newActor, true);
        }

        public void UpdateForSceneView(WSceneView view)
        {
            // Update our Selection Gizmo first, so we can check if it is currently transforming when we check to see
            // if the user's selection has changed.
            UpdateSelectionGizmo(view);

            // Check to see if they've left clicked and are changing their selection.
            CheckForObjectSelectionChange(view);

            // Add our gizmo to the renderer this frame.
            ((IRenderable)m_transformGizmo).AddToRenderer(view);
        }

        private void CheckForObjectSelectionChange(WSceneView view)
        {
            // If we have a gizmo and we're transforming it, don't check for selection change.
            if (m_transformGizmo != null && m_transformGizmo.IsTransforming)
                return;
            if (WInput.GetMouseButtonDown(0) && !WInput.GetMouseButton(1))
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                WActorNode addedActor = Raycast(mouseRay);

                // Check the behaviour of this click to determine appropriate selection modification behaviour.
                // Click w/o Modifiers = Clear Selection, add result to selection
                // Click /w Ctrl = Toggle Selection State
                // Click /w Shift = Add to Selection
                bool ctrlPressed = WInput.GetKey(Key.LeftCtrl) || WInput.GetKey(Key.RightCtrl);
                bool shiftPressed = WInput.GetKey(Key.LeftShift) || WInput.GetKey(Key.RightShift);

                if (!ctrlPressed & !shiftPressed)
                {
                    ModifySelection(SelectionType.Add, addedActor, true);
                    //m_selectionList.Clear();
                    //if (addedActor != null) m_selectionList.Add(addedActor);
                }
                else if (addedActor != null && (ctrlPressed && !shiftPressed))
                {
                    if (m_selectionList.Contains(addedActor))
                        ModifySelection(SelectionType.Remove, addedActor, false);
                    //m_selectionList.Remove(addedActor);
                    else
                        ModifySelection(SelectionType.Add, addedActor, false);
                        //m_selectionList.Add(addedActor);
                }
                else if (addedActor != null && shiftPressed)
                {
                    if (!m_selectionList.Contains(addedActor))
                        ModifySelection(SelectionType.Add, addedActor, false);
                        //m_selectionList.Add(addedActor);
                }

                if(m_transformGizmo != null && m_selectionList.Count > 0)
                {
                    m_transformGizmo.SetPosition(m_selectionList[0].Transform.Position);
                    m_transformGizmo.SetLocalRotation(m_selectionList[0].Transform.Rotation);
                }
            }
        }

        private void UpdateSelectionGizmo(WSceneView view)
        {
            if (!m_transformGizmo.Enabled && m_selectionList.Count > 0)
            {
                // Show the Transform Gizmo.
                m_transformGizmo.Enabled = true;

                m_transformGizmo.SetPosition(m_selectionList[0].Transform.Position);
                m_transformGizmo.SetLocalRotation(m_selectionList[0].Transform.Rotation);
            }
            else if (m_transformGizmo.Enabled && m_selectionList.Count == 0)
            {
                // Hide the Transform Gizmo.
                m_transformGizmo.Enabled = false;
            }

            if (!m_transformGizmo.Enabled)
                return;

            if (WInput.GetKeyDown(Key.Q) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.None);
            }
            if (WInput.GetKeyDown(Key.W) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Translation);
            }
            if (WInput.GetKeyDown(Key.E) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Rotation);
            }
            if (WInput.GetKeyDown(Key.R) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Scale);
            }

            if (WInput.GetKeyDown(Key.OemOpenBrackets))
            {
                m_transformGizmo.DecrementSize();
            }

            if (WInput.GetKeyDown(Key.OemCloseBrackets))
            {
                m_transformGizmo.IncrementSize();
            }

            if(WInput.GetKeyDown(Key.OemTilde))
            {
                if (m_transformGizmo.TransformSpace == FTransformSpace.World)
                    m_transformGizmo.SetTransformSpace(FTransformSpace.Local);
                else
                    m_transformGizmo.SetTransformSpace(FTransformSpace.World);

                UpdateGizmoTransform();
            }

            if (WInput.GetMouseButtonDown(0))
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                if (m_transformGizmo.CheckSelectedAxes(mouseRay))
                {                            
                    m_transformGizmo.StartTransform();
                }
            }

            if (WInput.GetMouseButtonUp(0))
            {
                if(m_transformGizmo.IsTransforming)
                {
                    // When we end let go of the gizmo, we want to make one last action which specifies that it is done,
                    // so that the next gizmo move doesn't merge with the previous.
                    WUndoCommand undoAction = CreateUndoActionForGizmo(true);
                    if (undoAction != null)
                        m_world.UndoStack.Push(undoAction);

                    m_transformGizmo.EndTransform();
                }
            }

            if (m_transformGizmo.IsTransforming)
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                if (m_transformGizmo.TransformFromInput(mouseRay, view))
                {
                    WUndoCommand undoAction = CreateUndoActionForGizmo(false);
                    if(undoAction != null)
                        m_world.UndoStack.Push(undoAction);
                }
            }

            m_transformGizmo.UpdateForSceneView(view);
        }

        private void ModifySelection(SelectionType action, WActorNode actor, bool clearSelection)
        {
            ModifySelection(action, new[] { actor }, clearSelection);
        }

        private void ModifySelection(SelectionType action, WActorNode[] actors, bool clearSelection)
        {
            // Cache the current selection list.
            WActorNode[] currentSelection = new WActorNode[m_selectionList.Count];
            m_selectionList.CopyTo(currentSelection, 0);

            List<WActorNode> newSelection = new List<WActorNode>(currentSelection);

            // Now build us a new array depending on the action.
            if(clearSelection)
            {
                newSelection.Clear();
            }

            if (action == SelectionType.Add)
            {
                for(int i = 0; i < actors.Length; i++)
                {
                    if(actors[i] != null)
                    {
                        newSelection.Add(actors[i]);
                    }
                }
            }
            else if (action == SelectionType.Remove)
            {
                for(int i = 0; i < actors.Length; i++)
                {
                    if(actors[i] != null)
                    {
                        newSelection.Remove(actors[i]);
                    }
                }
            }

            WSelectionChangedAction selectionAction = new WSelectionChangedAction(this, currentSelection, newSelection.ToArray(), m_selectionList);
            m_world.UndoStack.Push(selectionAction);
        }

        private WActorNode Raycast(FRay ray)
        {
            if (m_world.Map == null)
                return null;

            WActorNode closestResult = null;
            float closestDistance = float.MaxValue;

            foreach (var scene in m_world.Map.SceneList)
            {
                List<WActorNode> allActors = scene.GetChildrenOfType<WActorNode>();

                foreach (WActorNode actorNode in allActors)
                {
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

        private WUndoCommand CreateUndoActionForGizmo(bool isDone)
        {
            WUndoCommand undoAction = null;

            WActorNode[] actors = new WActorNode[m_selectionList.Count];
            for (int i = 0; i < m_selectionList.Count; i++)
            {
                actors[i] = m_selectionList[i];
            }

            switch (m_transformGizmo.Mode)
            {
                case FTransformMode.Translation:
                    undoAction = new WTranslateActorAction(actors, this, m_transformGizmo.DeltaTranslation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Rotation:
                    undoAction = new WRotateActorAction(actors, this, m_transformGizmo.DeltaRotation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Scale:
                    undoAction = new WScaleActorAction(actors, this, m_transformGizmo.DeltaScale, isDone);
                    Console.WriteLine(m_transformGizmo.DeltaScale);
                    break;
                default:
                    break;
            }

            return undoAction;
        }

        private void CutSelection()
        {
            if (m_selectionList.Count == 0)
                return;

            CopySelection();
            DeleteSelection();
        }

        private void CopySelection()
        {
            if (m_selectionList.Count == 0)
                return;

            // We're going to copy the selection by serializing it to a json string. Before we serialize it
            // though, we're going to exclude a few members from being serialized as they're owned by the
            // editor, and irrelevent when pasted.
            var jsonResolver = new IgnorableSerializerContractResolver();
            jsonResolver.Ignore(typeof(WWorld));
            jsonResolver.Ignore(typeof(WScene));
            jsonResolver.Ignore(typeof(WUndoStack));
            jsonResolver.Ignore(typeof(SimpleObjRenderer));

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.TypeNameHandling = TypeNameHandling.Auto;
            jsonSettings.ContractResolver = jsonResolver;
            jsonSettings.Converters.Add(new Vector2Converter());
            jsonSettings.Converters.Add(new Vector3Converter());
            jsonSettings.Converters.Add(new QuaternionConverter());


            string serializedSelectionList = JsonConvert.SerializeObject(m_selectionList, jsonSettings);
            Clipboard.SetText(serializedSelectionList);
        }

        private void PasteSelection()
        {
            throw new NotImplementedException();

            BindingList<WActorNode> serializedObjects = AttemptToDeserializeObjectsFromClipboard();
            if (serializedObjects == null)
                return;

            WActorNode[] actorRange = new WActorNode[serializedObjects.Count];
            serializedObjects.CopyTo(actorRange, 0);

            ModifySelection(SelectionType.Add, actorRange, true);
            //m_selectionList.Clear();
            foreach(var item in serializedObjects)
            {
                //m_world.FocusedScene.RegisterObject(item);
                //m_selectionList.Add(item);
            }
        }

        private void DeleteSelection()
        {
            foreach (var item in m_selectionList)
            {
                item.Parent.RemoveChild(item);
            }

            ModifySelection(SelectionType.Add, new WActorNode[] { null }, true);
        }

        private void SelectAll()
        {
            if (m_world.Map == null || m_world.Map.FocusedScene == null)
                return;

            var allActorsInSelectedScene = m_world.Map.FocusedScene.GetChildrenOfType<WActorNode>();
            ModifySelection(SelectionType.Add, allActorsInSelectedScene.ToArray(), true);
        }

        private void SelectNone()
        {
            ModifySelection(SelectionType.Add, new WActorNode[] { null }, true);
        }

        private BindingList<WActorNode> AttemptToDeserializeObjectsFromClipboard()
        {
            string clipboardContents = Clipboard.GetText();
            if (string.IsNullOrEmpty(clipboardContents))
                return null;

            BindingList<WActorNode> serializedObjects = null;
            try
            {
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.TypeNameHandling = TypeNameHandling.All;

                serializedObjects = JsonConvert.DeserializeObject<BindingList<WActorNode>>(clipboardContents, jsonSettings);
            }
            catch(JsonSerializationException ex)
            {
                Console.WriteLine("Failed to deseralize clipboard contents. Exception: {0}", ex.Message);
            }
            catch(Exception) { }

            return serializedObjects;
        }

        #region IDisposable Support
        ~WActorEditor()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        protected virtual void Dispose(bool manualDispose)
        {
            if (!m_hasBeenDisposed)
            {
                if (manualDispose)
                {
                    // Dispose managed state (managed objects).
                    m_transformGizmo.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.

                m_hasBeenDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
