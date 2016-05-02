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
            // Check to see if they've left clicked and are changing their selection.
            CheckForObjectSelectionChange();

            // Then either create, destroy, or check for movement of a movement gizmo.
            UpdateSelectionGizmo();
        }

        private void CheckForObjectSelectionChange()
        {
            if (WInput.GetMouseButtonDown(0) && !WInput.GetMouseButton(1))
            {
                WRay mouseRay = WSceneView.ProjectScreenToWorld(WInput.MousePosition);
                WActor addedActor = Raycast(mouseRay);

                // Check the behaviour of this click to determine appropriate selection modification behaviour.
                // Click w/o Modifiers = Clear Selection, add result to selection
                // Click /w Ctrl = Toggle Selection State
                // Click /w Shift = Add to Selection
                bool ctrlPressed = WInput.GetKey(System.Windows.Input.Key.LeftShift) || WInput.GetKey(System.Windows.Input.Key.RightShift);
                bool shiftPressed = WInput.GetKey(System.Windows.Input.Key.LeftShift) || WInput.GetKey(System.Windows.Input.Key.RightShift);


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
            }
        }

        private void UpdateSelectionGizmo()
        {
            if(m_transformGizmo == null && m_selectionList.Count > 0)
            {
                // Create the Transform Gizmo.
                m_transformGizmo = new WTransformGizmo(null);
                m_world.RegisterObject(m_transformGizmo);
            }
            else if(m_transformGizmo != null && m_selectionList.Count == 0)
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
                m_transformGizmo.EndTransform();
            }

            if (m_transformGizmo.IsTransforming)
            {
                WRay mouseRay = WSceneView.ProjectScreenToWorld(WInput.MousePosition);
                Vector3 cameraPos = WSceneView.GetCameraPos();
                m_transformGizmo.TransformFromInput(mouseRay, cameraPos);
            }
        }

        private WActor Raycast(WRay ray)
        {
            WActor closestResult = null;
            float closestDistance = float.MaxValue;

            foreach(ITickableObject obj in m_objectList)
            {
                WActor actor = obj as WActor;
                if (actor == null)
                    continue;

                AABox actorBoundingBox = actor.GetAABB();
                float intersectDistance;

                if(WMath.RayIntersectsAABB(ray, actorBoundingBox.Min, actorBoundingBox.Max, out intersectDistance))
                {
                    if(intersectDistance < closestDistance)
                    {
                        closestDistance = intersectDistance;
                        closestResult = actor;
                    }
                }
            }

            return closestResult;
        }
    }
}
