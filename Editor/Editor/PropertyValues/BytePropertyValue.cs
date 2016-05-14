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
                WEditPropertyValueAction undoRedoEntry = new WEditPropertyValueAction(
                    () => m_value = oldValue,
                    () => m_value = value,
                    () => OnPropertyChanged("Value"));

                if(m_undoStack != null)
                    m_undoStack.Push(undoRedoEntry);
            }
        }

        private byte m_value;

        public TBytePropertyValue(byte defaultValue, string propertyName) : base(propertyName)
        {
            m_value = defaultValue;
        }
    }
}
