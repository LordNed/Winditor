using System.Collections.Generic;
using System.ComponentModel;

namespace WindEditor
{
    public interface IPropertyValue : INotifyPropertyChanged, IUndoable
    {
        string Name { get; }

        void SetValue(object value);
        object GetValue();
        IPropertyValueAggregate GetValueAggregateInstance(string propertyName, IList<IPropertyValue> properties);
    }
}
