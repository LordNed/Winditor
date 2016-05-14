using System;
using System.Windows.Input;

namespace WindEditor
{
    public class WUndoStack
    {
        public bool CanUndo { get { return m_undoStack.Count > 0 || m_macroParent != null; } }
        public bool CanRedo { get { return m_redoStack.Count > 0 || m_macroParent != null; } }
        public int UndoLimit { get { return m_undoStack.MaxSize; } }

        public ICommand UndoCommand
        {
            get { return new RelayCommand(x => Undo(), (x) => CanUndo); }
        }

        public ICommand RedoCommand
        {
            get { return new RelayCommand(x => Redo(), (x) => CanRedo); }
        }

        private LimitedSizeStack<WUndoCommand> m_undoStack;
        private LimitedSizeStack<WUndoCommand> m_redoStack;
        private WUndoCommand m_macroParent;

        public WUndoStack()
        {
            m_undoStack = new LimitedSizeStack<WUndoCommand>();
            m_redoStack = new LimitedSizeStack<WUndoCommand>();

            SetUndoLimit(50);
        }

        public void Undo()
        {
            if (!CanUndo)
                return;

            WUndoCommand action = m_undoStack.Pop();
            action.Undo();

            m_redoStack.Push(action);
        }

        public void Redo()
        {
            if (!CanRedo)
                return;

            WUndoCommand action = m_redoStack.Pop();
            action.Redo();

            m_undoStack.Push(action);
        }

        /// <summary>
        /// Pushes an empty command with the specified actionText onto the Undo/Redo stack. Any subsequent commands 
        /// pushed onto the stack will be appended to the empty command's children until <see cref="EndMacro"/> is called.
        /// 
        /// You can nestle BeginMacro/EndMacro calls, but every Begin must have an End, and while a macro is being composed
        /// the stack is disabled. Disabling the stack means that CanUndo and CanRedo return false, and attempting to Undo
        /// or Redo will fail.
        /// </summary>
        /// <param name="actionText"></param>
        public void BeginMacro(string actionText)
        {
            WUndoCommand macroCommand = new WUndoCommand(actionText, m_macroParent);
            m_macroParent = macroCommand;
        }

        /// <summary>
        /// This ends the composition of the lastest macro created by <see cref="BeginMacro(string)"/>.
        /// </summary>
        public void EndMacro()
        {
            if(m_macroParent == null)
            {
                Console.WriteLine("WUndoStack: EndMacro called but no previous call to BeginMacro!");
                return;
            }

            Push(m_macroParent);

            // Grab the parent, which will either be the next macro we want to end, or null meaning we have no macros left.
            m_macroParent = m_macroParent.Parent;
        }

        /// <summary>
        /// Push a new <see cref="IAction"/> onto the <see cref="WUndoStack"/>, or merges it with the most recently executed command.
        /// This function executes the <see cref="IAction.Redo"/> function in either case. Calling this will clear the <see cref="WUndoStack"/>'s
        /// Redo stack, so the command will always end up being the top-most on the stack.
        /// </summary>
        /// <param name="command"></param>
        public void Push(WUndoCommand command)
        {
            // If we have an open macro, we push it to the macro instead of the stack, and when the macro is ended, that is when it is finally pushed to the stack.
            // command.GetType().IsSubclassOf(typeof(WUndoCommand)) will return false if we're pushing a WUndoCommand, ie: the end of a macro.
            if(m_macroParent != null && !command.GetType().IsSubclassOf(typeof(WUndoCommand)))
            {
                command.SetParent(m_macroParent);
                return;
            }

            // Clear the redo stack when we add a new item to the undo stack.
            m_redoStack.Clear();

            // Call the Redo function to apply the state change encapsulated by the IAction.
            command.Redo();

            // Attempt to merge with our new action. If this fails, add our new action to the undo stack.
            WUndoCommand latestAction = m_undoStack.Peek();
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
