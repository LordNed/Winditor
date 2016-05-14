using System.ComponentModel;

namespace WindEditor
{
    public interface IPropertyValueAggregate : INotifyPropertyChanged
    {
        string Name { get; }

        void SetValue(object value);
        object GetValue();
    }
}
