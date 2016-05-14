using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace WindEditor
{
    public class BaseValueAggregate<T> : INotifyPropertyChanged, IPropertyValueAggregate
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; protected set; }
        public object Value
        {
            get { return ((IPropertyValueAggregate)this).GetValue(); }
            set
            {
                // Converters will return a ValidationResult if the conversion
                // from string to type fails. If it failed to convert, we don't
                // want to try to set the value as there's no way to cast this
                // in reference types which are structs.
                ValidationResult valResult = value as ValidationResult;
                if (valResult != null && !valResult.IsValid)
                    return;

                ((IPropertyValueAggregate)this).SetValue(value);
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

        void IPropertyValueAggregate.SetValue(object value)
        {
            // Snag the Undo/Redo system off of the first associated property, so we can put all of these Set values into a macro.
            WUndoStack undoStack = null;
            if(m_associatedProperties.Count > 0)
            {
                IUndoable undoableValue = m_associatedProperties[0] as IUndoable;
                undoStack = undoableValue.GetUndoStack();
            }

            if (undoStack != null)
                undoStack.BeginMacro(string.Format("Set {0}", Name));

            foreach (var property in m_associatedProperties)
            {
                property.SetValue(value);
            }

            if (undoStack != null)
                undoStack.EndMacro();
        }

        object IPropertyValueAggregate.GetValue()
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
