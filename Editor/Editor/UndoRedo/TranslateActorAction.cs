using OpenTK;
using System.Collections.Generic;

namespace Editor
{
    class TranslateActorAction : IAction
    {
        private List<WActor> m_affectedActors;
        private Vector3 m_delta;
        private bool m_isDone;

        public TranslateActorAction(WActor[] actors, Vector3 delta, bool isDone)
        {
            m_affectedActors = new List<WActor>(actors);
            m_delta = delta;
            m_isDone = isDone;
        }

        public string ActionText()
        {
            return "Move";
        }

        public bool MergeWith(IAction withAction)
        {
            TranslateActorAction otherAction = withAction as TranslateActorAction;
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
                m_delta += otherAction.m_delta;
                m_isDone = otherAction.m_isDone;
                return true;
            }

            return false;
        }

        public void Redo()
        {
            for (int i = 0; i < m_affectedActors.Count; i++)
                m_affectedActors[i].Transform.Position += m_delta;
        }

        public void Undo()
        {
            for (int i = 0; i < m_affectedActors.Count; i++)
                m_affectedActors[i].Transform.Position -= m_delta;
        }
    }
}
