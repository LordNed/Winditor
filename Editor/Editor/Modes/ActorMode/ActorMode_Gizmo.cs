using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindEditor.Editor.Modes
{
    public partial class ActorMode : IEditorMode, IEditorModeGizmo
    {
        public WTransformGizmo TransformGizmo { get; set; }

        public void UpdateForSceneView(WSceneView view)
        {
            // Update our Selection Gizmo first, so we can check if it is currently transforming when we check to see
            // if the user's selection has changed.
            UpdateSelectionGizmo(view);

            // Check to see if they've left clicked and are changing their selection.
            CheckForObjectSelectionChange(view);

            // Add our gizmo to the renderer this frame.
            ((IRenderable)TransformGizmo).AddToRenderer(view);
        }

        public void UpdateGizmoTransform()
        {
            OpenTK.Vector3 position = OpenTK.Vector3.Zero;
            OpenTK.Quaternion localRotation = OpenTK.Quaternion.Identity;

            foreach (var entity in EditorSelection.SelectedObjects)
            {
                position += entity.Transform.Position;
            }

            if (EditorSelection.SelectedObjects.Count > 0)
            {
                position /= EditorSelection.SelectedObjects.Count;
                localRotation = EditorSelection.SelectedObjects[0].Transform.LocalRotation;
            }

            TransformGizmo.SetPosition(position);
            TransformGizmo.SetLocalRotation(localRotation);
        }

        private void UpdateSelectionGizmo(WSceneView view)
        {
            if (!TransformGizmo.Enabled && EditorSelection.SelectedObjects.Count > 0 && EditorSelection.PrimarySelectedObject is VisibleDOMNode)
            {
                // Show the Transform Gizmo.
                TransformGizmo.Enabled = true;
                UpdateGizmoTransform();
                // m_transformGizmo.SetPosition(m_selectionList[0].Transform.Position);
                // m_transformGizmo.SetLocalRotation(m_selectionList[0].Transform.Rotation);
            }
            else if (TransformGizmo.Enabled && EditorSelection.SelectedObjects.Count == 0 || !(EditorSelection.PrimarySelectedObject is VisibleDOMNode))
            {
                // Hide the Transform Gizmo.
                TransformGizmo.Enabled = false;
            }

            if (!TransformGizmo.Enabled)
                return;

            if (WInput.GetKeyDown(Key.Q) && !WInput.GetMouseButton(1))
            {
                TransformGizmo.SetMode(FTransformMode.None);
            }
            if (WInput.GetKeyDown(Key.W) && !WInput.GetMouseButton(1))
            {
                TransformGizmo.SetMode(FTransformMode.Translation);
            }
            if (WInput.GetKeyDown(Key.E) && !WInput.GetMouseButton(1))
            {
                TransformGizmo.SetMode(FTransformMode.Rotation);
            }
            if (WInput.GetKeyDown(Key.R) && !WInput.GetMouseButton(1))
            {
                TransformGizmo.SetMode(FTransformMode.Scale);
            }

            if (WInput.GetKeyDown(Key.OemOpenBrackets))
            {
                TransformGizmo.DecrementSize();
            }

            if (WInput.GetKeyDown(Key.OemCloseBrackets))
            {
                TransformGizmo.IncrementSize();
            }

            if (WInput.GetKeyDown(Key.OemTilde))
            {
                if (TransformGizmo.TransformSpace == FTransformSpace.World)
                    TransformGizmo.SetTransformSpace(FTransformSpace.Local);
                else
                    TransformGizmo.SetTransformSpace(FTransformSpace.World);

                UpdateGizmoTransform();
            }

            if (WInput.GetMouseButtonDown(0))
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                if (TransformGizmo.CheckSelectedAxes(mouseRay))
                {
                    TransformGizmo.StartTransform();
                }
            }

            if (WInput.GetMouseButtonUp(0))
            {
                if (TransformGizmo.IsTransforming)
                {
                    // When we end let go of the gizmo, we want to make one last action which specifies that it is done,
                    // so that the next gizmo move doesn't merge with the previous.
                    WUndoCommand undoAction = CreateUndoActionForGizmo(true);
                    if (undoAction != null)
                        BroadcastUndoEventGenerated(undoAction);

                    TransformGizmo.EndTransform();
                }
            }

            if (TransformGizmo.IsTransforming)
            {
                FRay mouseRay = view.ProjectScreenToWorld(WInput.MousePosition);
                if (TransformGizmo.TransformFromInput(mouseRay, view))
                {
                    WUndoCommand undoAction = CreateUndoActionForGizmo(false);
                    if (undoAction != null)
                        BroadcastUndoEventGenerated(undoAction);
                }
            }

            TransformGizmo.UpdateForSceneView(view);
        }

        private WUndoCommand CreateUndoActionForGizmo(bool isDone)
        {
            WUndoCommand undoAction = null;

            var actors = EditorSelection.SelectedObjects;
            switch (TransformGizmo.Mode)
            {
                case FTransformMode.Translation:
                    undoAction = new WTranslateActorAction(actors, this, TransformGizmo.DeltaTranslation, TransformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Rotation:
                    undoAction = new WRotateActorAction(actors, this, TransformGizmo.DeltaRotation, TransformGizmo.TransformSpace, isDone);
                    break;
                case FTransformMode.Scale:
                    undoAction = new WScaleActorAction(actors, this, TransformGizmo.DeltaScale, isDone);
                    Console.WriteLine(TransformGizmo.DeltaScale);
                    break;
                default:
                    break;
            }

            return undoAction;
        }
    }
}
