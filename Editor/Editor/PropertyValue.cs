using System;

namespace Editor
{
    public class PropertyValue<T>
    {
        private readonly string m_propertyName;
        private readonly Action<Action, Action> m_onModifiedCallback;
        private T m_value;

        public PropertyValue(string propertyName, T defaultValue)
        {
            m_propertyName = propertyName;
            m_value = defaultValue;
        }

        public T Value
        {
            get { return m_value; }
            set
            {
                T oldValue = m_value;
                m_onModifiedCallback.Invoke(
                    () => m_value = value,
                    () => m_value = oldValue
                    );
            }
        }
    }
}
