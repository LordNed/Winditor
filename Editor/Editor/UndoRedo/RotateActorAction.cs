using OpenTK;
using System.Collections.Generic;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WRotateActorAction : WUndoCommand
    {
        private List<WDOMNode> m_affectedActors;
        private Quaterniond m_delta;
        private bool m_isDone;
        private FTransformSpace m_transformSpace;
        private IEditorModeGizmo m_mode;

        public WRotateActorAction(IEnumerable<WDOMNode> actors, IEditorModeGizmo mode, Quaterniond delta, FTransformSpace transformSpace, bool isDone, WUndoCommand parent = null) : base("Rotate", parent)
        {
            m_affectedActors = new List<WDOMNode>(actors);
            m_mode = mode;
            m_delta = delta;
            m_isDone = isDone;
            m_transformSpace = transformSpace;
        }

        public override bool MergeWith(WUndoCommand withAction)
        {
            WRotateActorAction otherAction = withAction as WRotateActorAction;
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
                m_delta *= otherAction.m_delta;
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
                if(m_transformSpace == FTransformSpace.Local)
                {
                    m_affectedActors[i].Transform.Rotation *= m_delta;
                }
                else
                {
                    m_affectedActors[i].Transform.Rotation = m_delta * m_affectedActors[i].Transform.Rotation;
                }
            }

            m_mode.UpdateGizmoTransform();
        }

        public override void Undo()
        {
            base.Undo();
            for (int i = 0; i < m_affectedActors.Count; i++)
            {
                if (m_transformSpace == FTransformSpace.Local)
                {
                    m_affectedActors[i].Transform.Rotation *= Quaterniond.Invert(m_delta);
                }
                else
                {
                    m_affectedActors[i].Transform.Rotation = Quaterniond.Invert(m_delta) * m_affectedActors[i].Transform.Rotation;
                }
            }

            m_mode.UpdateGizmoTransform();
        }
    }
}
