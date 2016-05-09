namespace WindEditor
{
    public class TUndoRedoObject
    {
        public PropertyValue<float> TestFloatValue { get; set; }
        public PropertyValue<bool> TestBoolValue { get; set; }
        public PropertyValue<int> TestIntValue { get; set; }
        public PropertyValue<string> TestStringValue { get; set; }

        public TUndoRedoObject(WUndoStack undoStack)
        {
            TestFloatValue = new PropertyValue<float>("TestFloatValue", -1f, undoStack);

            TestBoolValue = new PropertyValue<bool>("TestBoolValue", true, undoStack);
            TestIntValue = new PropertyValue<int>("TestIntValue", 5, undoStack);
            TestStringValue = new PropertyValue<string>("TestStringValue", "Hello World", undoStack);
        }
    }
}
