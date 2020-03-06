using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct IndirectTevOrder : IEquatable<IndirectTevOrder>
    {
        public TexCoordId TexCoord;
        public TexMapId TexMap;

        public IndirectTevOrder(TexCoordId coordId, TexMapId mapId)
        {
            TexCoord = coordId;
            TexMap = mapId;
        }

        public IndirectTevOrder(EndianBinaryReader reader)
        {
            TexCoord = (TexCoordId)reader.ReadByte();
            TexMap = (TexMapId)reader.ReadByte();
            reader.SkipInt16();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write((byte)TexCoord);
            writer.Write((byte)TexMap);
            writer.Write((short)-1);
        }

        public static bool operator ==(IndirectTevOrder left, IndirectTevOrder right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IndirectTevOrder left, IndirectTevOrder right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = (int)TexCoord;
            hash ^= (int)TexMap << 3;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IndirectTevOrder))
                return false;
            else
                return Equals((IndirectTevOrder)obj);
        }

        public bool Equals(IndirectTevOrder other)
        {
            return TexCoord == other.TexCoord &&
                TexMap == other.TexMap;
        }
    }
}
