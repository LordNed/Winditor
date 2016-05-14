using OpenTK;
using System.Collections.Generic;

namespace WindEditor
{
    class WRotateActorAction : WUndoCommand
    {
        private List<WActor> m_affectedActors;
        private Quaternion m_delta;
        private bool m_isDone;
        private FTransformSpace m_transformSpace;

        public WRotateActorAction(WActor[] actors, Quaternion delta, FTransformSpace transformSpace, bool isDone, WUndoCommand parent = null) : base("Rotate", parent)
        {
            m_affectedActors = new List<WActor>(actors);
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
        }

        public override void Undo()
        {
            base.Undo();
            for (int i = 0; i < m_affectedActors.Count; i++)
            {
                if (m_transformSpace == FTransformSpace.Local)
                {
                    m_affectedActors[i].Transform.Rotation *= Quaternion.Invert(m_delta);
                }
                else
                {
                    m_affectedActors[i].Transform.Rotation = Quaternion.Invert(m_delta) * m_affectedActors[i].Transform.Rotation;
                }
            }
        }
    }
}
