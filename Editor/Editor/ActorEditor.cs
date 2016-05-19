using Newtonsoft.Json;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WindEditor.Serialization;

namespace WindEditor
{
    public class WActorEditor
    {
        public SelectionAggregate SelectedObjects { get; protected set; }
        public ICommand CutSelectionCommand { get { return new RelayCommand(x => CutSelection(), (x) => m_selectionList.Count > 0); } }
        public ICommand CopySelectionCommand { get { return new RelayCommand(x => CopySelection(), (x) => m_selectionList.Count > 0); } }
        public ICommand PasteSelectionCommand { get { return new RelayCommand(x => PasteSelection(), (x) => AttemptToDeserializeObjectsFromClipboard() != null); } }
        public ICommand DeleteSelectionCommand { get { return new RelayCommand(x => DeleteSelection(), (x) => m_selectionList.Count > 0); } }
        public ICommand SelectAllCommand { get { return new RelayCommand(x => SelectAll(), (x) => true); } }
        public ICommand SelectNoneCommand { get { return new RelayCommand(x => SelectNone(), (x) => m_selectionList.Count > 0); } }

        private WWorld m_world;
        private enum SelectionType
        {
            Add,
            Remove
        }

        private WTransformGizmo m_transformGizmo;
        private BindingList<WMapActor> m_selectionList;

        [Obsolete("Bring back the transform gizmo :-(")]
        public WActorEditor(WWorld world)
        {
            m_world = world;
            m_selectionList = new BindingList<WMapActor>();
            m_transformGizmo = new WTransformGizmo(m_world);
            //m_world.RegisterInternalObject(m_transformGizmo);

            SelectedObjects = new SelectionAggregate(m_selectionList);
        }

        public void Tick(float deltaTime)
        {
            m_transformGizmo.Tick(deltaTime);

            // Update our Selection Gizmo first, so we can check if it is currently transforming when we check to see
            // if the user's selection has changed.
            UpdateSelectionGizmo();

            // Check to see if they've left clicked and are changing their selection.
            CheckForObjectSelectionChange();
        }

        private void CheckForObjectSelectionChange()
        {
            // If we have a gizmo and we're transforming it, don't check for selection change.
            if (m_transformGizmo != null && m_transformGizmo.IsTransforming)
                return;
            if (WInput.GetMouseButtonDown(0) && !WInput.GetMouseButton(1))
            {
                WRay mouseRay = m_world.GetFocusedSceneView().ProjectScreenToWorld(WInput.MousePosition);
                WMapActor addedActor = Raycast(mouseRay);

                // Check the behaviour of this click to determine appropriate selection modification behaviour.
                // Click w/o Modifiers = Clear Selection, add result to selection
                // Click /w Ctrl = Toggle Selection State
                // Click /w Shift = Add to Selection
                bool ctrlPressed = WInput.GetKey(System.Windows.Input.Key.LeftCtrl) || WInput.GetKey(System.Windows.Input.Key.RightCtrl);
                bool shiftPressed = WInput.GetKey(System.Windows.Input.Key.LeftShift) || WInput.GetKey(System.Windows.Input.Key.RightShift);

                Console.WriteLine("ctrl {0} shift {1}", ctrlPressed, shiftPressed);

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

        private void UpdateSelectionGizmo()
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

            if (WInput.GetKeyDown(System.Windows.Input.Key.Q) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.None);
            }
            if (WInput.GetKeyDown(System.Windows.Input.Key.W) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Translation);
            }
            if (WInput.GetKeyDown(System.Windows.Input.Key.E) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Rotation);
            }
            if (WInput.GetKeyDown(System.Windows.Input.Key.R) && !WInput.GetMouseButton(1))
            {
                m_transformGizmo.SetMode(FTransformMode.Scale);
            }

            if (WInput.GetKeyDown(System.Windows.Input.Key.OemOpenBrackets))
            {
                m_transformGizmo.DecrementSize();
            }

            if (WInput.GetKeyDown(System.Windows.Input.Key.OemCloseBrackets))
            {
                m_transformGizmo.IncrementSize();
            }

            if(WInput.GetKeyDown(System.Windows.Input.Key.OemTilde))
            {
                if (m_transformGizmo.TransformSpace == FTransformSpace.World)
                    m_transformGizmo.SetTransformSpace(FTransformSpace.Local);
                else
                    m_transformGizmo.SetTransformSpace(FTransformSpace.World);

                m_transformGizmo.SetPosition(m_selectionList[0].Transform.Position);
                m_transformGizmo.SetLocalRotation(m_selectionList[0].Transform.Rotation);
            }

            if (WInput.GetMouseButtonDown(0))
            {
                WRay mouseRay = m_world.GetFocusedSceneView().ProjectScreenToWorld(WInput.MousePosition);
                if (m_transformGizmo.CheckSelectedAxes(mouseRay))
                {                            
                    Console.WriteLine("TranslationGizmo clicked. Selected Axes: {0}", m_transformGizmo.SelectedAxes);
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
                WRay mouseRay = m_world.GetFocusedSceneView().ProjectScreenToWorld(WInput.MousePosition);
                Vector3 cameraPos = m_world.GetFocusedSceneView().GetCameraPos();
                if (m_transformGizmo.TransformFromInput(mouseRay, cameraPos))
                {
                    WUndoCommand undoAction = CreateUndoActionForGizmo(false);
                    if(undoAction != null)
                        m_world.UndoStack.Push(undoAction);
                }
            }
        }

        private void ModifySelection(SelectionType action, WMapActor actor, bool clearSelection)
        {
            ModifySelection(action, new[] { actor }, clearSelection);
        }

        private void ModifySelection(SelectionType action, WMapActor[] actors, bool clearSelection)
        {
            // Cache the current selection list.
            WMapActor[] currentSelection = new WMapActor[m_selectionList.Count];
            m_selectionList.CopyTo(currentSelection, 0);

            List<WMapActor> newSelection = new List<WMapActor>(currentSelection);

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

            WSelectionChangedAction selectionAction = new WSelectionChangedAction(currentSelection, newSelection.ToArray(), m_selectionList);
            m_world.UndoStack.Push(selectionAction);
        }

        private WMapActor Raycast(WRay ray)
        {
            WMapActor closestResult = null;
            float closestDistance = float.MaxValue;

            foreach (var scene in m_world.SceneList)
            {
                foreach (IRenderable obj in scene.RenderableObjects)
                {
                    WMapActor actor = obj as WMapActor;
                    if (actor == null)
                        continue;

                    AABox actorBoundingBox = actor.GetAABB();
                    float intersectDistance;

                    if (WMath.RayIntersectsAABB(ray, actorBoundingBox.Min, actorBoundingBox.Max, out intersectDistance))
                    {
                        if (intersectDistance < closestDistance)
                        {
                            closestDistance = intersectDistance;
                            closestResult = actor;
                        }
                    }
                }
            }

            return closestResult;
        }

        private WUndoCommand CreateUndoActionForGizmo(bool isDone)
        {
            WUndoCommand undoAction = null;

            WActor[] actors = new WActor[m_selectionList.Count];
            for (int i = 0; i < m_selectionList.Count; i++)
            {
                actors[i] = m_selectionList[i];
            }

            switch (m_transformGizmo.Mode)
            {
                case FTransformMode.Translation:
                    undoAction = new WTranslateActorAction(actors, m_transformGizmo.DeltaTranslation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Rotation:
                    undoAction = new WRotateActorAction(actors, m_transformGizmo.DeltaRotation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Scale:
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
            BindingList<WMapActor> serializedObjects = AttemptToDeserializeObjectsFromClipboard();
            if (serializedObjects == null)
                return;

            WMapActor[] actorRange = new WMapActor[serializedObjects.Count];
            serializedObjects.CopyTo(actorRange, 0);

            ModifySelection(SelectionType.Add, actorRange, true);
            //m_selectionList.Clear();
            foreach(var item in serializedObjects)
            {
                m_world.FocusedScene.RegisterObject(item);
                //m_selectionList.Add(item);
            }
        }

        private void DeleteSelection()
        {
            foreach (var item in m_selectionList)
            {
                item.GetScene().UnregisterObject(item);
            }

            ModifySelection(SelectionType.Add, new WMapActor[] { null }, true);
            ///m_selectionList.Clear();
        }

        private void SelectAll()
        {
            List<WMapActor> allActors = new List<WMapActor>();
            foreach (IRenderable obj in m_world.FocusedScene.RenderableObjects)
            {
                WMapActor actor = obj as WMapActor;
                if (actor == null)
                    continue;

                allActors.Add(actor);
            }

            ModifySelection(SelectionType.Add, allActors.ToArray(), true);
        }

        private void SelectNone()
        {
            ModifySelection(SelectionType.Add, new WMapActor[] { null }, true);
        }

        private BindingList<WMapActor> AttemptToDeserializeObjectsFromClipboard()
        {
            string clipboardContents = Clipboard.GetText();
            if (string.IsNullOrEmpty(clipboardContents))
                return null;

            BindingList<WMapActor> serializedObjects = null;
            try
            {
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.TypeNameHandling = TypeNameHandling.All;

                serializedObjects = JsonConvert.DeserializeObject<BindingList<WMapActor>>(clipboardContents, jsonSettings);
            }
            catch(JsonSerializationException ex)
            {
                Console.WriteLine("Failed to deseralize clipboard contents. Exception: {0}", ex.Message);
            }
            catch(Exception) { }

            return serializedObjects;
        }
    }
}
