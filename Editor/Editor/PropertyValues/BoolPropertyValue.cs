namespace WindEditor
{
    public class TBoolPropertyValue : TBasePropertyValue<bool>
    {
        public override bool Value
        {
            get { return m_value; }

            set
            {
                var oldValue = m_value;
                WEditPropertyValueAction undoRedoEntry = new WEditPropertyValueAction(
                    () => m_value = oldValue,
                    () => m_value = value,
                    () => OnPropertyChanged("Value"));
                m_undoStack.Push(undoRedoEntry);
            }
        }

        private readonly WUndoStack m_undoStack;
        private bool m_value;

        public TBoolPropertyValue(bool defaultValue, string propertyName, WUndoStack undoStack) : base(propertyName)
        {
            m_value = defaultValue;
            m_undoStack = undoStack;
        }
    }
}
