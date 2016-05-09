using OpenTK;

namespace WindEditor
{
    public class TVector3PropertyValue : TBasePropertyValue<Vector3>
    {
        public override Vector3 Value
        {
            get { return m_value; }

            set
            {
                var oldValue = m_value;
                EditPropertyValueAction undoRedoEntry = new EditPropertyValueAction(
                    () => m_value = oldValue,
                    () => m_value = value,
                    () => OnPropertyChanged("Value"));
                m_undoStack.Push(undoRedoEntry);
            }
        }

        private readonly WUndoStack m_undoStack;
        private Vector3 m_value;

        public TVector3PropertyValue(Vector3 defaultValue, WUndoStack undoStack)
        {
            m_value = defaultValue;
            m_undoStack = undoStack;
        }
    }
}
