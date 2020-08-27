using OpenTK;
using System.Collections.Generic;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WTranslateCameraPropertyAction : WUndoCommand
    {
        private List<BindingVector3> m_affectedProperties;
        private Vector3 m_delta;
        private bool m_isDone;
        private FTransformSpace m_transformSpace;
        private IEditorModeGizmo m_mode;

        public WTranslateCameraPropertyAction(IEnumerable<BindingVector3> properties, IEditorModeGizmo mode, Vector3 delta, FTransformSpace transformSpace, bool isDone, WUndoCommand parent = null) : base("Move", parent)
        {
            m_affectedProperties = new List<BindingVector3>(properties);
            m_mode = mode;
            m_delta = delta;
            m_isDone = isDone;
            m_transformSpace = transformSpace;
        }

        public override bool MergeWith(WUndoCommand withAction)
        {
            WTranslateCameraPropertyAction otherAction = withAction as WTranslateCameraPropertyAction;
            if (m_isDone || otherAction == null)
                return false;

            bool arrayEquals = m_affectedProperties.Count == otherAction.m_affectedProperties.Count;
            if (arrayEquals)
            {
                for (int i = 0; i < m_affectedProperties.Count; i++)
                {
                    if (!otherAction.m_affectedProperties.Contains(m_affectedProperties[i]))
                    {
                        arrayEquals = false;
                        break;
                    }
                }
            }

            if (arrayEquals)
            {
                m_delta += otherAction.m_delta;
                m_isDone = otherAction.m_isDone;
                return true;
            }

            return false;
        }

        public override void Redo()
        {
            base.Redo();
            for (int i = 0; i < m_affectedProperties.Count; i++)
            {
                Vector3 transformedDelta = Vector3.Zero;
                if (m_transformSpace == FTransformSpace.World)
                {
                    transformedDelta = m_delta;
                }
                else
                {
                    //transformedDelta = Vector3.Transform(m_delta, m_affectedActors[i].Transform.Rotation);
                }

                m_affectedProperties[i].BackingVector += transformedDelta;
            }

            m_mode.UpdateGizmoTransform();
        }

        public override void Undo()
        {
            base.Undo();
            for (int i = 0; i < m_affectedProperties.Count; i++)
            {
                Vector3 transformedDelta = Vector3.Zero;
                if (m_transformSpace == FTransformSpace.World)
                {
                    transformedDelta = m_delta;
                }
                else
                {
                    //transformedDelta = Vector3.Transform(m_delta, m_affectedActors[i].Transform.Rotation);
                }
                m_affectedProperties[i].BackingVector -= transformedDelta;
            }

            m_mode.UpdateGizmoTransform();
        }
    }
}