using OpenTK;
using System;
using System.Collections.Generic;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WCreateEntitiesAction : WUndoCommand
    {
        private WDOMNode[] m_createdEntities;
        private WDOMNode[] m_parents;
        private WDOMNode[] m_prevSiblings;

        public WCreateEntitiesAction(WDOMNode[] createdEntities, WDOMNode[] parents, WDOMNode[] previousSiblings = null)
        {
            m_createdEntities = createdEntities;
            m_parents = parents;
            if (previousSiblings == null)
                m_prevSiblings = new WDOMNode[m_createdEntities.Length];
            else
                m_prevSiblings = previousSiblings;
        }

        public override bool MergeWith(WUndoCommand withAction)
        {
            return false;
        }

        public override void Redo()
        {
            base.Redo();

            for (int i = 0; i < m_createdEntities.Length; i++)
            {
                var entity = m_createdEntities[i];
                var parent = m_parents[i];
                entity.IsSelected = true;
                entity.Undestroy(parent);

                if (m_prevSiblings[i] != null)
                {
                    // Insert after the previous sibling at the correct index.
                    int orderIndex = parent.Children.IndexOf(m_prevSiblings[i]) + 1;
                    parent.Children.Remove(entity);
                    parent.Children.Insert(orderIndex, entity);
                }
            }
        }

        public override void Undo()
        {
            base.Undo();

            foreach (var entity in m_createdEntities)
            {
                entity.IsSelected = false;
                entity.Destroy();
            }
        }
    }
}
