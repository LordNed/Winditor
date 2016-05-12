using System.ComponentModel;

namespace WindEditor
{
    public interface IPropertyValue : INotifyPropertyChanged
    {
        string Name { get; }

        void SetValue(object value);
        object GetValue();
    }
}
