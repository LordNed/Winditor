namespace WindEditor
{
    public interface IUndoable
    {
        WUndoStack GetUndoStack();
        void SetUndoStack(WUndoStack undoStack);
    }
}
