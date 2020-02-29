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

        public WCreateEntitiesAction(WDOMNode[] createdEntities, WDOMNode[] parents)
        {
            m_createdEntities = createdEntities;
            m_parents = parents;
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
