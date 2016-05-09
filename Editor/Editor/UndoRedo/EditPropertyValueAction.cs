using System;

namespace WindEditor
{
    public class EditPropertyValueAction : IAction
    {
        private Action m_undoAction;
        private Action m_redoAction;
        private Action m_onPropertyChanged;

        public string ActionText()
        {
            return "Value";
        }

        public EditPropertyValueAction(Action onUndo, Action onRedo, Action onPropertyChanged)
        {
            if (onUndo == null)
                throw new ArgumentNullException("onUndo", "Undo callback cannot be null.");
            if (onRedo == null)
                throw new ArgumentNullException("onRedo", "Redo callback cannot be null.");
            if (onPropertyChanged == null)
                throw new ArgumentNullException("onPropertyChanged", "On Property Changed callback cannot be null.");

            m_undoAction = onUndo;
            m_redoAction = onRedo;
            m_onPropertyChanged = onPropertyChanged;
        }

        public bool MergeWith(IAction withAction)
        {
            return false;
        }

        public void Redo()
        {
            m_redoAction.Invoke();
            m_onPropertyChanged.Invoke();
        }

        public void Undo()
        {
            m_undoAction.Invoke();
            m_onPropertyChanged.Invoke();
        }
    }
}
