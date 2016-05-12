using System.ComponentModel;

namespace WindEditor
{
    public abstract class TBasePropertyValue<T> : IPropertyValue
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract T Value { get; set; }

        public string Name { get; }

        public TBasePropertyValue(string propertyName)
        {
            Name = propertyName;
        }

        public object GetValue()
        {
            return Value;
        }

        public void SetValue(object value)
        {
            Value = (T)value;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
