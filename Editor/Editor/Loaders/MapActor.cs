using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WindEditor
{
    public class WMapActor : WStaticMeshActor
    {
        public ObservableCollection<IPropertyValue> Properties { get; set; }
        public MapLayer Layer { get; set; }

        public WMapActor() : base("resources/editor/EditorCube.obj")
        {
            Properties = new ObservableCollection<IPropertyValue>();
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}
