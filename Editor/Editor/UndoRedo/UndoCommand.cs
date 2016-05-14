using System;
using System.Collections.Generic;

namespace WindEditor
{
    public class WUndoCommand
    {
        public string ActionText { get { return m_text; } }

        public WUndoCommand Parent { get { return m_parent; } }

        protected WUndoCommand m_parent;
        protected string m_text;
        protected List<WUndoCommand> m_commandChildren;

        public WUndoCommand(WUndoCommand parent = null)
        {
            SetParent(parent);
            m_text = string.Empty;
            m_commandChildren = new List<WUndoCommand>();
        }

        public WUndoCommand(string text, WUndoCommand parent = null)
        {
            SetParent(parent);
            m_text = text;
            m_commandChildren = new List<WUndoCommand>();
        }
        
        public void SetText(string text)
        {
            m_text = text;
        }

        public virtual void Undo()
        {
            // Invoke all of our Children's undo functions as well so that we can use WUndoCommands to group like, but non-mergable, undo commands together.
            foreach (var child in m_commandChildren)
                child.Undo();
        }

        public virtual void Redo()
        {
            // Invoke all of our Children's redo functions as well so that we can use WUndoCommands to group like, but non-mergable, redo commands together.
            foreach (var child in m_commandChildren)
                child.Redo();
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

        public void SetParent(WUndoCommand parent)
        {
            // Remove us from the old parent
            if (m_parent != null)
                m_parent.m_commandChildren.Remove(this);

            m_parent = parent;
            if(m_parent != null)
            {
                m_parent.m_commandChildren.Add(this);
            }
        }

        public virtual bool MergeWith(WUndoCommand withAction) { return false; }
    }
}
