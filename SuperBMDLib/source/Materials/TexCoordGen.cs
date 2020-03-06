using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct TexCoordGen : IEquatable<TexCoordGen>
    {
        public TexGenType Type;
        public TexGenSrc Source;
        public Enums.TexMatrix TexMatrixSource;

        public TexCoordGen(TexGenType type, TexGenSrc src, Enums.TexMatrix mtrx)
        {
            Type = type;
            Source = src;
            TexMatrixSource = mtrx;
        }

        public TexCoordGen(EndianBinaryReader reader)
        {
            Type =            (TexGenType)reader.ReadByte();
            Source =          (TexGenSrc)reader.ReadByte();
            TexMatrixSource = (Enums.TexMatrix)reader.ReadByte();

            reader.SkipByte();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write((byte)Type);
            writer.Write((byte)Source);
            writer.Write((byte)TexMatrixSource);

            // Pad entry to 4 bytes
            writer.Write((sbyte)-1);
        }

        public static bool operator ==(TexCoordGen left, TexCoordGen right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TexCoordGen left, TexCoordGen right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = (int)Type;
            hash ^= (int)Source << 3;
            hash ^= (int)TexMatrixSource << 4;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TexCoordGen))
                return false;
            else
                return Equals((TexCoordGen)obj);
        }

        public bool Equals(TexCoordGen other)
        {
            return Type == other.Type &&
                Source == other.Source &&
                TexMatrixSource == other.TexMatrixSource;
        }
    }
}
