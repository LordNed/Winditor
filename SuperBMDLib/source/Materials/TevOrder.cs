using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct TevOrder : IEquatable<TevOrder>
    {
        public TexCoordId TexCoord;
        public TexMapId TexMap;
        public GXColorChannelId ChannelId;

        public TevOrder(TexCoordId texCoord, TexMapId texMap, GXColorChannelId chanID)
        {
            TexCoord = texCoord;
            TexMap = texMap;
            ChannelId = chanID;
        }

        public TevOrder(EndianBinaryReader reader)
        {
            TexCoord = (TexCoordId)reader.ReadByte();
            TexMap = (TexMapId)reader.ReadByte();
            ChannelId = (GXColorChannelId)reader.ReadByte();
            reader.SkipByte();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write((byte)TexCoord);
            writer.Write((byte)TexMap);
            writer.Write((byte)ChannelId);
            writer.Write((sbyte)-1);
        }

        public static bool operator ==(TevOrder left, TevOrder right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TevOrder left, TevOrder right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = (int)TexCoord;
            hash ^= (int)TexMap << 6;
            hash ^= (int)ChannelId << 2;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TevOrder))
                return false;
            else
                return Equals((TevOrder)obj);
        }

        public bool Equals(TevOrder other)
        {
            return TexCoord == other.TexCoord &&
                TexMap == other.TexMap &&
                ChannelId == other.ChannelId;
        }
    }
}
