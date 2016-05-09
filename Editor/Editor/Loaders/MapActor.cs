using System.Collections.Generic;

namespace WindEditor
{
    class WMapActor : WStaticMeshActor
    {
        public List<IPropertyValue> Properties { get; set; }
        public MapLayer Layer { get; set; }

        public WMapActor() : base("resources/editor/EditorCube.obj")
        {
            Properties = new List<IPropertyValue>();
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}
