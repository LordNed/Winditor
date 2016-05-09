using System.ComponentModel;

namespace WindEditor
{
    public class PropertyValue<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string m_propertyName;
        private readonly WUndoStack m_undoStack;
        private T m_value;

        public PropertyValue(string propertyName, T defaultValue, WUndoStack undoStack)
        {
            m_propertyName = propertyName;
            m_value = defaultValue;
            m_undoStack = undoStack;
        }

        public T Value
        {
            get { return m_value; }
            set
            {
                T oldValue = m_value;
                EditPropertyValueAction undoRedoEntry = new EditPropertyValueAction(
                    () => m_value = oldValue,
                    () => m_value = value,
                    () => OnPropertyChanged("Value"));
                m_undoStack.Push(undoRedoEntry);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
