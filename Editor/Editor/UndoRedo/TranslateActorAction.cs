﻿using OpenTK;
using System.Collections.Generic;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WTranslateActorAction : WUndoCommand
    {
        private List<WDOMNode> m_affectedActors;
        private Vector3 m_delta;
        private bool m_isDone;
        private FTransformSpace m_transformSpace;
        private IEditorModeGizmo m_mode;

        public WTranslateActorAction(IEnumerable<WDOMNode> actors, IEditorModeGizmo mode, Vector3 delta, FTransformSpace transformSpace, bool isDone, WUndoCommand parent = null) : base("Move", parent)
        {
            m_affectedActors = new List<WDOMNode>(actors);
            m_mode = mode;
            m_delta = delta;
            m_isDone = isDone;
            m_transformSpace = transformSpace;
        }

        public override bool MergeWith(WUndoCommand withAction)
        {
            WTranslateActorAction otherAction = withAction as WTranslateActorAction;
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

        public override void Redo()
        {
            base.Redo();
            for (int i = 0; i < m_affectedActors.Count; i++)
            {
                Vector3 transformedDelta = Vector3.Zero;
                if (m_transformSpace == FTransformSpace.World)
                {
                    transformedDelta = m_delta;
                }
                else
                {
                    transformedDelta = (Vector3)Vector3d.Transform((Vector3d)m_delta, m_affectedActors[i].Transform.Rotation);
                }

                m_affectedActors[i].Transform.Position += transformedDelta;
            }

            m_mode.UpdateGizmoTransform();
        }

        public override void Undo()
        {
            base.Undo();
            for (int i = 0; i < m_affectedActors.Count; i++)
            {
                Vector3 transformedDelta = Vector3.Zero;
                if (m_transformSpace == FTransformSpace.World)
                {
                    transformedDelta = m_delta;
                }
                else
                {
                    transformedDelta = (Vector3)Vector3d.Transform((Vector3d)m_delta, m_affectedActors[i].Transform.Rotation);
                }
                m_affectedActors[i].Transform.Position -= transformedDelta;
            }

            m_mode.UpdateGizmoTransform();
        }
    }
}
