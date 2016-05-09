namespace WindEditor
{
    public class TFloatPropertyValue : TBasePropertyValue<float>
    {
        public override float Value
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
        private float m_value;

        public TFloatPropertyValue(float defaultValue, WUndoStack undoStack)
        {
            m_value = defaultValue;
            m_undoStack = undoStack;
        }
    }
}
