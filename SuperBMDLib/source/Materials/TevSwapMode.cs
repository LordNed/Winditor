using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct TevSwapMode : IEquatable<TevSwapMode>
    {
        public byte RasSel;
        public byte TexSel;

        public TevSwapMode(byte rasSel, byte texSel)
        {
            RasSel = rasSel;
            TexSel = texSel;
        }

        public TevSwapMode(EndianBinaryReader reader)
        {
            RasSel = reader.ReadByte();
            TexSel = reader.ReadByte();
            reader.SkipInt16();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(RasSel);
            writer.Write(TexSel);
            writer.Write((short)-1);
        }

        public static bool operator ==(TevSwapMode left, TevSwapMode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TevSwapMode left, TevSwapMode right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = RasSel;
            hash ^= TexSel << 2;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TevSwapMode))
                return false;
            else
                return Equals((TevSwapMode)obj);
        }

        public bool Equals(TevSwapMode other)
        {
            return RasSel == other.RasSel &&
                TexSel == other.TexSel;
        }
    }
}
