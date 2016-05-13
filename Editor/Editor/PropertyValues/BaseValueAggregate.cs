using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace WindEditor
{
    public class BaseValueAggregate<T> : INotifyPropertyChanged, IPropertyValue
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; protected set; }
        public object Value
        {
            get { return ((IPropertyValue)this).GetValue(); }
            set
            {
                // Converters will return a ValidationResult if the conversion
                // from string to type fails. If it failed to convert, we don't
                // want to try to set the value as there's no way to cast this
                // in reference types which are structs.
                ValidationResult valResult = value as ValidationResult;
                if (valResult != null && !valResult.IsValid)
                    return;

                ((IPropertyValue)this).SetValue(value);
            }
        }

        private IList<IPropertyValue> m_associatedProperties;

        public BaseValueAggregate(string propertyName, IList<IPropertyValue> properties)
        {
            m_associatedProperties = properties;
            Name = propertyName;

            // Listen to PropertyChanged events on every property value incase Undo/Redo changes the value.
            foreach (INotifyPropertyChanged propChange in m_associatedProperties)
            {
                propChange.PropertyChanged += OnTrackedPropertyValueChanged;
            }
        }

        void IPropertyValue.SetValue(object value)
        {
            foreach (var property in m_associatedProperties)
            {
                property.SetValue(value);
            }
        }

        object IPropertyValue.GetValue()
        {
            // Return either the common value or null for no value.
            T commonValue = default(T);
            if (m_associatedProperties.Count > 0)
                commonValue = (T)m_associatedProperties[0].GetValue();

            foreach (var property in m_associatedProperties)
            {
                if (!property.GetValue().Equals(commonValue))
                    return null;
            }

            return commonValue;
        }

        private void OnTrackedPropertyValueChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Value");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
