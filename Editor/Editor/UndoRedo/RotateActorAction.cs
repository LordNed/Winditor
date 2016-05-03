using OpenTK;
using System.Collections.Generic;

namespace Editor
{
    class RotateActorAction : IAction
    {
        private List<WActor> m_affectedActors;
        private Quaternion m_delta;
        private bool m_isDone;
        private FTransformSpace m_transformSpace;

        public RotateActorAction(WActor[] actors, Quaternion delta, FTransformSpace transformSpace, bool isDone)
        {
            m_affectedActors = new List<WActor>(actors);
            m_delta = delta;
            m_isDone = isDone;
            m_transformSpace = transformSpace;
        }

        public string ActionText()
        {
            return "Rotate";
        }

        public bool MergeWith(IAction withAction)
        {
            RotateActorAction otherAction = withAction as RotateActorAction;
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

        public void Redo()
        {
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

        public void Undo()
        {
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
