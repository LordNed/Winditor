using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace WindEditor.a
{
    public abstract class WDOMSerializableNode : WDOMNode
    {
        public WDOMSerializableNode(WWorld world, string header) : base(world)
        {
            Name = header;
        }

        public abstract void PostLoad();
        public abstract void PreSave();
        public abstract void Serialize(EndianBinaryWriter writer);
        public abstract void Deserialize(EndianBinaryReader reader);
    }
}
