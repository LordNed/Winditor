using System;
using System.Windows.Input;

namespace Editor
{
    public class WUndoStack
    {
        public bool CanUndo { get { return m_undoStack.Count > 0; } }
        public bool CanRedo { get { return m_redoStack.Count > 0; } }
        public int UndoLimit { get { return m_undoStack.MaxSize; } }

        public ICommand UndoCommand
        {
            get { return new RelayCommand(x => Undo(), (x) => CanUndo); }
        }

        public ICommand RedoCommand
        {
            get { return new RelayCommand(x => Redo(), (x) => CanRedo); }
        }

        private LimitedSizeStack<IAction> m_undoStack;
        private LimitedSizeStack<IAction> m_redoStack;

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

        /// <summary>
        /// Push a new <see cref="IAction"/> onto the <see cref="WUndoStack"/>, or merges it with the most recently executed command.
        /// This function executes the <see cref="IAction.Redo"/> function in either case. Calling this will clear the <see cref="WUndoStack"/>'s
        /// Redo stack, so the command will always end up being the top-most on the stack.
        /// </summary>
        /// <param name="command"></param>
        public void Push(IAction command)
        {
            // Clear the redo stack when we add a new item to the undo stack.
            m_redoStack.Clear();

            // Call the Redo function to apply the state change encapsulated by the IAction.
            command.Redo();

            // Attempt to merge with our new action. If this fails, add our new action to the undo stack.
            IAction latestAction = m_undoStack.Peek();
            if (latestAction == null)
                m_undoStack.Push(command);
            else if(!latestAction.MergeWith(command))
                m_undoStack.Push(command);
        }

        public void SetUndoLimit(int limit)
        {
            if (limit < 0)
                throw new ArgumentException("Undo Limit cannot be negative!", "limit");

            m_undoStack.SetMaxSize(limit);
            m_redoStack.SetMaxSize(limit);
        }
    }
}
