using OpenTK;
using System;
using System.Collections.Generic;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WDeleteEntitiesAction : WUndoCommand
    {
        private WDOMNode[] m_deletedEntities;
        private WDOMNode[] m_originalParents;

        public WDeleteEntitiesAction(WDOMNode[] deletedEntities)
        {
            m_deletedEntities = deletedEntities;
            m_originalParents = new WDOMNode[m_deletedEntities.Length];
            for (int i = 0; i < m_deletedEntities.Length; i++)
            {
                m_originalParents[i] = m_deletedEntities[i].Parent;
            }
        }

        public override bool MergeWith(WUndoCommand withAction)
        {
            return false;
        }

        public override void Redo()
        {
            base.Redo();

            foreach (var entity in m_deletedEntities)
            {
                entity.IsSelected = false;
                entity.Destroy();
            }
        }

        public override void Undo()
        {
            base.Undo();

            for (int i = 0; i < m_deletedEntities.Length; i++)
            {
                var entity = m_deletedEntities[i];
                var parent = m_originalParents[i];
                entity.IsSelected = true;
                entity.Undestroy(parent);
            }
        }
    }
}
