using OpenTK;
using System.Collections.Generic;

namespace WindEditor
{
    class TranslateActorAction : IAction
    {
        private List<WActor> m_affectedActors;
        private Vector3 m_delta;
        private bool m_isDone;
        private FTransformSpace m_transformSpace;

        public TranslateActorAction(WActor[] actors, Vector3 delta, FTransformSpace transformSpace, bool isDone)
        {
            m_affectedActors = new List<WActor>(actors);
            m_delta = delta;
            m_isDone = isDone;
            m_transformSpace = transformSpace;
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
            {
                Vector3 transformedDelta = Vector3.Zero;
                if (m_transformSpace == FTransformSpace.World)
                {
                    transformedDelta = m_delta;
                }
                else
                {
                    transformedDelta = Vector3.Transform(m_delta, m_affectedActors[i].Transform.Rotation);
                }

                m_affectedActors[i].Transform.Position += transformedDelta;
            }
        }

        public void Undo()
        {
            for (int i = 0; i < m_affectedActors.Count; i++)
            {
                Vector3 transformedDelta = Vector3.Zero;
                if (m_transformSpace == FTransformSpace.World)
                {
                    transformedDelta = m_delta;
                }
                else
                {
                    transformedDelta = Vector3.Transform(m_delta, m_affectedActors[i].Transform.Rotation);
                }
                m_affectedActors[i].Transform.Position -= transformedDelta;
            }
        }
    }
}
