using System.Collections.Generic;
using System.ComponentModel;

namespace WindEditor
{
    public class TStringValueAggregate : INotifyPropertyChanged, IPropertyValue
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Value
        {
            get
            {
                return (string)((IPropertyValue)this).GetValue();
            }
            set
            {
                ((IPropertyValue)this).SetValue(value);
            }
        }

        private IList<IPropertyValue> m_associatedProperties;

        public TStringValueAggregate(IList<IPropertyValue> properties)
        {
            m_associatedProperties = properties;
            foreach(INotifyPropertyChanged propChange in m_associatedProperties)
            {
                propChange.PropertyChanged += PropChange_PropertyChanged;
            }
        }

        private void PropChange_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Console.WriteLine("sender: {0} e: {1}", sender, e);
            OnPropertyChanged("Value");
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
            string commonValue = string.Empty;
            if (m_associatedProperties.Count > 0)
                commonValue = (string)m_associatedProperties[0].GetValue();

            foreach (var property in m_associatedProperties)
            {
                if (string.Compare((string)property.GetValue(), commonValue) != 0)
                    return null;
            }

            return commonValue;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
