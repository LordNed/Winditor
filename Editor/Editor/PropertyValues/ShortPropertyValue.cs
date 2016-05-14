namespace WindEditor
{
    public class TShortPropertyValue : TBasePropertyValue<short>
    {
        public override short Value
        {
            get { return m_value; }

            set
            {
                var oldValue = m_value;
                WEditPropertyValueAction undoRedoEntry = new WEditPropertyValueAction(
                    () => m_value = oldValue,
                    () => m_value = value,
                    () => OnPropertyChanged("Value"));

                if (m_undoStack != null)
                    m_undoStack.Push(undoRedoEntry);
            }
        }

        private short m_value;

        public TShortPropertyValue(short defaultValue, string propertyName):base(propertyName)
        {
            m_value = defaultValue;
        }
    }
}
