using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WindEditor
{
    public abstract class TBasePropertyValue<T> : IPropertyValue
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get { return m_name; } }
        public abstract T Value { get; set; }

        protected WUndoStack m_undoStack;

        [JsonProperty("m_name")]
        protected string m_name;

        public TBasePropertyValue(string propertyName)
        {
            m_name = propertyName;
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

        public WUndoStack GetUndoStack()
        {
            return m_undoStack;
        }

        public void SetUndoStack(WUndoStack undoStack)
        {
            m_undoStack = undoStack;
        }

        public IPropertyValueAggregate GetValueAggregateInstance(string propertyName, IList<IPropertyValue> properties)
        {
            return new BaseValueAggregate<T>(propertyName, properties);
        }
    }
}
