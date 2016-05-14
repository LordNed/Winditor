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
                WEditPropertyValueAction undoRedoEntry = new WEditPropertyValueAction(
                    () => m_value = oldValue,
                    () => m_value = value,
                    () => OnPropertyChanged("Value"));

                if (m_undoStack != null)
                    m_undoStack.Push(undoRedoEntry);
            }
        }

        private string m_value;

        public TStringPropertyValue(string defaultValue, string propertyName):base(propertyName)
        {
            m_value = defaultValue;
        }
    }
}
