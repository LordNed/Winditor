namespace WindEditor
{
    public class TStringPropertyValue : TBasePropertyValue<string>
    {
        public override string Value
        {
            get { return m_value; }

            set
            {
                var oldValue = m_value;
                EditPropertyValueAction undoRedoEntry = new EditPropertyValueAction(
                    () => m_value = oldValue,
                    () => m_value = value,
                    () => OnPropertyChanged("Value"));
                m_undoStack.Push(undoRedoEntry);
            }
        }

        private readonly WUndoStack m_undoStack;
        private string m_value;

        public TStringPropertyValue(string defaultValue, string propertyName, WUndoStack undoStack):base(propertyName)
        {
            m_value = defaultValue;
            m_undoStack = undoStack;
        }
    }
}
