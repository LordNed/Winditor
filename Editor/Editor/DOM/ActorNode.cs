using System;
using System.Collections.Generic;

namespace WindEditor
{
    [Flags]
    public enum ActorFlags
    {
        None = 0,
        Selected = 1,
    }

    public enum MapLayer
    {
        Default,
        Layer0,
        Layer1,
        Layer2,
        Layer3,
        Layer4,
        Layer5,
        Layer6,
        Layer7,
        Layer8,
        Layer9,
        LayerA,
        LayerB,
    }

    public class WActorNode : WDOMNode
    {
        public List<IPropertyValue> Properties { get; }
        public MapLayer Layer { get; set; }
        public ActorFlags Flags { get; set; }

        public WActorNode()
        {
            Properties = new List<IPropertyValue>();
        }
    }
}
