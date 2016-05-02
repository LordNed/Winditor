using System;

namespace Editor
{
    class WUndoStack
    {
        public bool CanUndo { get { return m_undoStack.Count > 0; } }
        public bool CanRedo { get { return m_redoStack.Count > 0; } }
        public int UndoLimit { get { return m_undoLimit; } }

        private LimitedSizeStack<IAction> m_undoStack;
        private LimitedSizeStack<IAction> m_redoStack;
        private int m_undoLimit;

        public WUndoStack()
        {
            m_undoStack = new LimitedSizeStack<IAction>();
            m_redoStack = new LimitedSizeStack<IAction>();

            SetUndoLimit(50);
        }

        public void Undo()
        {
            if (m_undoStack.Count == 0)
                return;

            IAction action = m_undoStack.Pop();
            action.Undo();

            m_redoStack.Push(action);
        }

        public void Redo()
        {
            if (m_redoStack.Count == 0)
                return;

            IAction action = m_redoStack.Pop();
            action.Redo();

            m_undoStack.Push(action);
        }

        public void Push(IAction command)
        {
            // Clear the redo stack when we add a new item to the undo stack.
            m_redoStack.Clear();
            m_undoStack.Push(command);
        }

        public void SetUndoLimit(int limit)
        {
            if (limit < 0)
                throw new ArgumentException("Undo Limit cannot be negative!", "limit");

            if(limit < m_undoLimit)
            {
                throw new NotImplementedException("todo: implement trimming off the oldest entries in the stack.");
            }

            m_undoLimit = limit;
            m_undoStack.SetMaxSize(m_undoLimit);
            m_redoStack.SetMaxSize(m_undoLimit);
        }
    }
}
