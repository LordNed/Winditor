using Newtonsoft.Json;

namespace WindEditor
{
    public class TFloatPropertyValue : TBasePropertyValue<float>
    {
        public override float Value
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

        [JsonProperty("m_value")]
        private float m_value;

        public TFloatPropertyValue(float defaultValue, string propertyName) : base(propertyName)
        {
            m_value = defaultValue;
        }
    }
}
