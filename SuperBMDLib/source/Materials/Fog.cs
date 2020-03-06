using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Util;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct Fog : IEquatable<Fog>
    {
        public byte Type;
        public bool Enable;
        public ushort Center;
        public float StartZ;
        public float EndZ;
        public float NearZ;
        public float FarZ;
        public Color Color;
        public float[] RangeAdjustmentTable;

        public Fog(byte type, bool enable, ushort center, float startZ, float endZ, float nearZ, float farZ, Color color, float[] rangeAdjust)
        {
            Type = type;
            Enable = enable;
            Center = center;
            StartZ = startZ;
            EndZ = endZ;
            NearZ = nearZ;
            FarZ = farZ;
            Color = color;
            RangeAdjustmentTable = rangeAdjust;
        }

        public Fog(EndianBinaryReader reader)
        {
            RangeAdjustmentTable = new float[10];

            Type = reader.ReadByte();
            Enable = reader.ReadBoolean();
            Center = reader.ReadUInt16();
            StartZ = reader.ReadSingle();
            EndZ = reader.ReadSingle();
            NearZ = reader.ReadSingle();
            FarZ = reader.ReadSingle();
            Color = new Color((float)reader.ReadByte() / 255, (float)reader.ReadByte() / 255, (float)reader.ReadByte() / 255, (float)reader.ReadByte() / 255);

            for (int i = 0; i < 10; i++)
            {
                ushort inVal = reader.ReadUInt16();
                RangeAdjustmentTable[i] = (float)inVal / 256;
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Type);
            writer.Write(Enable);
            writer.Write(Center);
            writer.Write(StartZ);
            writer.Write(EndZ);
            writer.Write(NearZ);
            writer.Write(FarZ);
            writer.Write(Color);

            for (int i = 0; i < 10; i++)
                writer.Write((ushort)(RangeAdjustmentTable[i] * 256));
        }

        public static bool operator==(Fog left, Fog right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(Fog left, Fog right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = Type;
            hash ^= Convert.ToInt32(Enable);
            hash ^= Center << 7;
            hash ^= StartZ.GetHashCode() << 4;
            hash ^= EndZ.GetHashCode() << 4;
            hash ^= NearZ.GetHashCode() << 3;
            hash ^= FarZ.GetHashCode() << 5;
            hash ^= Color.GetHashCode() << 6;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Fog))
                return false;
            else
                return Equals((Fog)obj);
        }

        public bool Equals(Fog other)
        {
            return Type == other.Type &&
                Enable == other.Enable &&
                Center == other.Center &&
                StartZ == other.StartZ &&
                EndZ == other.EndZ &&
                NearZ == other.NearZ &&
                FarZ == other.FarZ &&
                Color == other.Color;
        }
    }
}
