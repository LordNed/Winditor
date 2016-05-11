using System.ComponentModel;

namespace WindEditor
{
    public interface IPropertyValue : INotifyPropertyChanged
    {
        void SetValue(object value);
        object GetValue();
    }
}
