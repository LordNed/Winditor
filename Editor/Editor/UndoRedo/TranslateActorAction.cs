using OpenTK;
using System.Collections.Generic;

namespace Editor
{
    class TranslateActorAction : IAction
    {
        private List<WActor> m_affectedActors;
        private Vector3 m_delta;

        public TranslateActorAction(WActor[] actors, Vector3 delta)
        {
            m_affectedActors = new List<WActor>(actors);
            m_delta = delta;
        }

        public string ActionText()
        {
            return "Move";
        }

        public bool MergeWith(IAction withAction)
        {
            TranslateActorAction otherAction = withAction as TranslateActorAction;
            if (otherAction == null)
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
