using OpenTK;

namespace WindEditor
{
    public class TVector2PropertyValue : TBasePropertyValue<Vector2>
    {
        public override Vector2 Value
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

        private Vector2 m_value;

        public TVector2PropertyValue(Vector2 defaultValue, string propertyName):base(propertyName)
        {
            m_value = defaultValue;
        }
    }
}
