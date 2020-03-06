using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct BlendMode : IEquatable<BlendMode>
    {
        /// <summary> Blending Type </summary>
        public Enums.BlendMode Type;
        /// <summary> Blending Control </summary>
        public BlendModeControl SourceFact;
        /// <summary> Blending Control </summary>
        public BlendModeControl DestinationFact;
        /// <summary> What operation is used to blend them when <see cref="Type"/> is set to <see cref="GXBlendMode.Logic"/>. </summary>
        public LogicOp Operation; // Seems to be logic operators such as clear, and, copy, equiv, inv, invand, etc.

        public BlendMode(Enums.BlendMode type, BlendModeControl src, BlendModeControl dest, LogicOp operation)
        {
            Type = type;
            SourceFact = src;
            DestinationFact = dest;
            Operation = operation;
        }

        public BlendMode(EndianBinaryReader reader)
        {
            Type = (Enums.BlendMode)reader.ReadByte();
            SourceFact = (BlendModeControl)reader.ReadByte();
            DestinationFact = (BlendModeControl)reader.ReadByte();
            Operation = (LogicOp)reader.ReadByte();
        }

        public void Write(EndianBinaryWriter write)
        {
            write.Write((byte)Type);
            write.Write((byte)SourceFact);
            write.Write((byte)DestinationFact);
            write.Write((byte)Operation);
        }

        public static bool operator==(BlendMode left, BlendMode right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(BlendMode left, BlendMode right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = (int)Type;
            hash ^= (int)SourceFact << 3;
            hash ^= (int)DestinationFact << 4;
            hash ^= (int)Operation << 3;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BlendMode))
                return false;
            else
                return Equals((BlendMode)obj);
        }

        public bool Equals(BlendMode other)
        {
            return Type == other.Type &&
                SourceFact == other.SourceFact &&
                DestinationFact == other.DestinationFact &&
                Operation == other.Operation;
        }
    }
}
