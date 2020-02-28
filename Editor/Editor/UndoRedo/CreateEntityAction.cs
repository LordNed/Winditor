using OpenTK;
using System;
using System.Collections.Generic;
using WindEditor.Editor.Modes;

namespace WindEditor
{
    class WCreateEntityAction : WUndoCommand
    {
        private WDOMNode m_parentNode;
        private SerializableDOMNode m_createdEntity;

        public WCreateEntityAction(SerializableDOMNode createdEntity)
        {
            m_createdEntity = createdEntity;
            m_parentNode = createdEntity.Parent;
        }

        public override bool MergeWith(WUndoCommand withAction)
        {
            return false;
        }

        public override void Redo()
        {
            base.Redo();

            m_createdEntity.IsSelected = true;
            m_createdEntity.Undestroy(m_parentNode);
        }

        public override void Undo()
        {
            base.Undo();

            m_createdEntity.IsSelected = false;
            m_createdEntity.Destroy();
        }
    }
}
