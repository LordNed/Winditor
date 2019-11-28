using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.a
{
    public abstract class WDOMEntityNode : WDOMSerializableNode
    {
        public FourCC FourCC { get; protected set; }

        public WDOMEntityNode(WWorld world, FourCC fourcc) : base(world, FourCCConversion.GetStringFromEnum(fourcc))
        {
            FourCC = fourcc;
        }
    }

    public abstract class WDOMPositionableEntityNode : WDOMEntityNode
    {
        public WTransform Transform { get; protected set; }

        public WDOMPositionableEntityNode(WWorld world, FourCC fourcc) : base(world, fourcc)
        {

        }
    }
}