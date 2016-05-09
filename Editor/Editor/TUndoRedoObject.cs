namespace WindEditor
{
    public class TUndoRedoObject
    {
        public TBoolPropertyValue TestBoolValue { get; set; }
        public TStringPropertyValue TestStringValue { get; set; }

        public TUndoRedoObject(WUndoStack undoStack)
        {
            TestBoolValue = new TBoolPropertyValue(true, undoStack);
            TestStringValue = new TStringPropertyValue("Hello World", undoStack);
        }
    }
}
