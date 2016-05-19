using Newtonsoft.Json;
using OpenTK;

namespace WindEditor
{
    public class TVector3PropertyValue : TBasePropertyValue<Vector3>
    {
        public override Vector3 Value
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
        private Vector3 m_value;

        public TVector3PropertyValue(Vector3 defaultValue, string propertyName):base(propertyName)
        {
            m_value = defaultValue;
        }
    }
}
