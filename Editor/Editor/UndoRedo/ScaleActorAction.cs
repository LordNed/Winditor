using OpenTK;
using System.Collections.Generic;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WScaleActorAction : WUndoCommand
    {
        private List<WDOMNode> m_affectedActors;
        private Vector3 m_delta;
        private bool m_isDone;
        private IEditorModeGizmo m_mode;

        public WScaleActorAction(IEnumerable<WDOMNode> actors, IEditorModeGizmo mode, Vector3 delta, bool isDone, WUndoCommand parent = null) : base("Scale", parent)
        {
            m_affectedActors = new List<WDOMNode>(actors);
            m_mode = mode;
            m_delta = delta;
            m_isDone = isDone;
        }

        public override bool MergeWith(WUndoCommand withAction)
        {
            WScaleActorAction otherAction = withAction as WScaleActorAction;
            if (m_isDone || otherAction == null)
                return false;

            bool arrayEquals = m_affectedActors.Count == otherAction.m_affectedActors.Count;
            if(arrayEquals)
            {
                for(int i = 0; i < m_affectedActors.Count; i++)
                {
                    if(!otherAction.m_affectedActors.Contains(m_affectedActors[i]))
                    {
                        arrayEquals = false;
                        break;
                    }
                }
            }

            if(arrayEquals)
            {
                m_delta.X *= otherAction.m_delta.X;
                m_delta.Y *= otherAction.m_delta.Y;
                m_delta.Z *= otherAction.m_delta.Z;
                m_isDone = otherAction.m_isDone;
                return true;
            }

            return false;
        }

        public override void Redo()
        {
            base.Redo();

            for (int i = 0; i < m_affectedActors.Count; i++)
            {
                Vector3 localScale = m_affectedActors[i].Transform.LocalScale;
                localScale.X *= m_delta.X;
                localScale.Y *= m_delta.Y;
                localScale.Z *= m_delta.Z;
                m_affectedActors[i].Transform.LocalScale = localScale;
            }

            m_mode.UpdateGizmoTransform();
        }

        public override void Undo()
        {
            base.Undo();
            for (int i = 0; i < m_affectedActors.Count; i++)
            {
                Vector3 localScale = m_affectedActors[i].Transform.LocalScale;
                localScale.X /= m_delta.X;
                localScale.Y /= m_delta.Y;
                localScale.Z /= m_delta.Z;
                m_affectedActors[i].Transform.LocalScale = localScale;
            }

            m_mode.UpdateGizmoTransform();
        }
    }
}
