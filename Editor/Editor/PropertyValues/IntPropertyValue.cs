using Newtonsoft.Json;

namespace WindEditor
{
    public class TIntPropertyValue : TBasePropertyValue<int>
    {
        public override int Value
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
        private int m_value;

        public TIntPropertyValue(int defaultValue, string propertyName) : base(propertyName)
        {
            m_value = defaultValue;
        }
    }
}
