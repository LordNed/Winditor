using System;
using System.Collections.Generic;

namespace WindEditor
{
    public class WMapActor : WStaticMeshActor, IUndoable
    {
        public List<IPropertyValue> Properties { get; set; }
        public MapLayer Layer { get; set; }

        public WMapActor() : base("resources/editor/EditorCube.obj")
        {
            Properties = new List<IPropertyValue>();
        }

        public WUndoStack GetUndoStack()
        {
            if (Properties.Count > 0)
                return Properties[0].GetUndoStack();

            return null;
        }

        public void SetUndoStack(WUndoStack undoStack)
        {
            foreach (var property in Properties)
                property.SetUndoStack(undoStack);
        }
    }
}
