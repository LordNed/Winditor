using OpenTK;
using System;
using System.Collections.Generic;

namespace Editor
{
    class WActorEditor
    {
        private WWorld m_world;
        private IList<ITickableObject> m_objectList;

        private WTransformGizmo m_transformGizmo;
        private List<WActor> m_selectionList;

        public WActorEditor(WWorld world, IList<ITickableObject> actorList)
        {
            m_world = world;
            m_objectList = actorList;
            m_selectionList = new List<WActor>();
            // RegisterObject(new WTransformGizmo(m_persistentLines));
        }

        public void Tick(float deltaTime)
        {
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
                WRay mouseRay = WSceneView.ProjectScreenToWorld(WInput.MousePosition);
                WActor addedActor = Raycast(mouseRay);

                // Check the behaviour of this click to determine appropriate selection modification behaviour.
                // Click w/o Modifiers = Clear Selection, add result to selection
                // Click /w Ctrl = Toggle Selection State
                // Click /w Shift = Add to Selection
                bool ctrlPressed = WInput.GetKey(System.Windows.Input.Key.LeftCtrl) || WInput.GetKey(System.Windows.Input.Key.RightCtrl);
                bool shiftPressed = WInput.GetKey(System.Windows.Input.Key.LeftShift) || WInput.GetKey(System.Windows.Input.Key.RightShift);

                Console.WriteLine("ctrl {0} shift {1}", ctrlPressed, shiftPressed);

                if (!ctrlPressed & !shiftPressed)
                {
                    m_selectionList.Clear();
                    if (addedActor != null) m_selectionList.Add(addedActor);
                }
                else if (addedActor != null && (ctrlPressed && !shiftPressed))
                {
                    if (m_selectionList.Contains(addedActor))
                        m_selectionList.Remove(addedActor);
                    else
                        m_selectionList.Add(addedActor);
                }
                else if (addedActor != null && shiftPressed)
                {
                    if (!m_selectionList.Contains(addedActor)) m_selectionList.Add(addedActor);
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
            if (m_transformGizmo == null && m_selectionList.Count > 0)
            {
                // Create the Transform Gizmo.
                m_transformGizmo = new WTransformGizmo(m_world.m_persistentLines);
                m_world.RegisterObject(m_transformGizmo);

                if(m_selectionList.Count > 0)
                {
                    m_transformGizmo.SetPosition(m_selectionList[0].Transform.Position);
                    m_transformGizmo.SetLocalRotation(m_selectionList[0].Transform.Rotation);
                }
            }
            else if (m_transformGizmo != null && m_selectionList.Count == 0)
            {
                // Remove the Transform Gizmo.
                m_world.UnregisterObject(m_transformGizmo);
                m_transformGizmo = null;
            }

            if (m_transformGizmo == null)
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
                WRay mouseRay = WSceneView.ProjectScreenToWorld(WInput.MousePosition);
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
                    IAction undoAction = CreateUndoActionForGizmo(true);
                    if (undoAction != null)
                        m_world.UndoStack.Push(undoAction);

                    m_transformGizmo.EndTransform();
                }
            }

            if (m_transformGizmo.IsTransforming)
            {
                WRay mouseRay = WSceneView.ProjectScreenToWorld(WInput.MousePosition);
                Vector3 cameraPos = WSceneView.GetCameraPos();
                if (m_transformGizmo.TransformFromInput(mouseRay, cameraPos))
                {
                    IAction undoAction = CreateUndoActionForGizmo(false);
                    if(undoAction != null)
                        m_world.UndoStack.Push(undoAction);
                }
            }
        }

        private WActor Raycast(WRay ray)
        {
            WActor closestResult = null;
            float closestDistance = float.MaxValue;

            foreach (ITickableObject obj in m_objectList)
            {
                WActor actor = obj as WActor;
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

            return closestResult;
        }

        private IAction CreateUndoActionForGizmo(bool isDone)
        {
            if (m_transformGizmo == null)
                return null;

            IAction undoAction = null;

            switch (m_transformGizmo.Mode)
            {
                case FTransformMode.Translation:
                    undoAction = new TranslateActorAction(m_selectionList.ToArray(), m_transformGizmo.DeltaTranslation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Rotation:
                    undoAction = new RotateActorAction(m_selectionList.ToArray(), m_transformGizmo.DeltaRotation, m_transformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Scale:
                    break;
                default:
                    break;
            }

            return undoAction;
        }
    }
}
