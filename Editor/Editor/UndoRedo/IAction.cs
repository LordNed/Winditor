namespace WindEditor
{
    public interface IAction
    {
        /// <summary>
        /// A short string which describes the action for display in the UI.
        /// </summary>
        string ActionText();

        /// <summary>
        /// Execute the action (Redo)
        /// </summary>
        void Redo();

        /// <summary>
        /// Undo changes made by the Execute call (Undo)
        /// </summary>
        void Undo();

        /// <summary>
        /// Attempt to merge with the specified action. This merges the actions, instead
        /// of recording a new one.
        /// </summary>
        /// <param name="withAction">The action to attempt to merge with.</param>
        /// <returns>True if we merged with the action, false if failed and need to record a new action.</returns>
        bool MergeWith(IAction withAction);
    }
}
