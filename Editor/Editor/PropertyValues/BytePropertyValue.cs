namespace WindEditor
{
    public class TBytePropertyValue : TBasePropertyValue<byte>
    {
        public override byte Value
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
        private byte m_value;

        public TBytePropertyValue(byte defaultValue, WUndoStack undoStack)
        {
            m_value = defaultValue;
            m_undoStack = undoStack;
        }
    }
}
