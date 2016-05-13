using System;
using System.Collections.Generic;

namespace WindEditor
{
    public abstract class WUndoCommand
    {
        public string ActionText { get { return m_text; } }

        private WUndoCommand m_parent;
        private string m_text;
        private List<WUndoCommand> m_commandChildren;

        public WUndoCommand(WUndoCommand parent = null)
        {
            m_parent = parent;
            m_text = string.Empty;
            m_commandChildren = new List<WUndoCommand>();

            if(m_parent != null)
            {
                m_parent.m_commandChildren.Add(this);
            }
        }

        public WUndoCommand(string text, WUndoCommand parent = null)
        {
            m_parent = parent;
            m_text = text;
            m_commandChildren = new List<WUndoCommand>();

            if (m_parent != null)
            {
                m_parent.m_commandChildren.Add(this);
            }
        }

        public int GetNumChildren()
        {
            return m_commandChildren.Count;
        }

        public WUndoCommand GetChild(int index)
        {
            if (index < 0 || index >= m_commandChildren.Count)
                throw new ArgumentOutOfRangeException("index", "Must be >= 0 and less than the number of children!");

            return m_commandChildren[index];
        }

        public void SetText(string text)
        {
            m_text = text;
        }

        public virtual bool MergeWith(WUndoCommand withAction) { return false; }
        public abstract void Undo();
        public abstract void Redo();
    }
}
