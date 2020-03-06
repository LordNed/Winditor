using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct AlphaCompare : IEquatable<AlphaCompare>
    {
        /// <summary> subfunction 0 </summary>
        public CompareType Comp0;
        /// <summary> Reference value for subfunction 0. </summary>
        public byte Reference0;
        /// <summary> Alpha combine control for subfunctions 0 and 1. </summary>
        public AlphaOp Operation;
        /// <summary> subfunction 1 </summary>
        public CompareType Comp1;
        /// <summary> Reference value for subfunction 1. </summary>
        public byte Reference1;

        public AlphaCompare(CompareType comp0, byte ref0, AlphaOp operation, CompareType comp1, byte ref1)
        {
            Comp0 = comp0;
            Reference0 = ref0;
            Operation = operation;
            Comp1 = comp1;
            Reference1 = ref1;
        }

        public AlphaCompare(EndianBinaryReader reader)
        {
            Comp0 = (CompareType)reader.ReadByte();
            Reference0 = reader.ReadByte();
            Operation = (AlphaOp)reader.ReadByte();
            Comp1 = (CompareType)reader.ReadByte();
            Reference1 = reader.ReadByte();
            reader.Skip(3);
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write((byte)Comp0);
            writer.Write(Reference0);
            writer.Write((byte)Operation);
            writer.Write((byte)Comp1);
            writer.Write(Reference1);
            writer.Write((sbyte)-1);
            writer.Write((short)-1);
        }

        public static bool operator==(AlphaCompare left, AlphaCompare right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(AlphaCompare left, AlphaCompare right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = (int)Comp0;
            hash ^= Reference0 << 4;
            hash ^= (int)Operation << 7;
            hash ^= (int)Comp1 << 2;
            hash ^= Reference1 << 3;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AlphaCompare))
                return false;
            else
                return Equals((AlphaCompare)obj);
        }

        public bool Equals(AlphaCompare other)
        {
            return Comp0 == other.Comp0 &&
                Reference0 == other.Reference0 &&
                Operation == other.Operation &&
                Comp1 == other.Comp1 &&
                Reference1 == other.Reference1;
        }
    }
}
