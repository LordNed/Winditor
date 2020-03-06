using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct ChannelControl : IEquatable<ChannelControl>
    {
        public bool Enable;
        public ColorSrc MaterialSrcColor;
        public LightId LitMask;
        public DiffuseFn DiffuseFunction;
        public J3DAttenuationFn AttenuationFunction;
        public ColorSrc AmbientSrcColor;

        public ChannelControl(bool enable, ColorSrc matSrcColor, LightId litMask, DiffuseFn diffFn, J3DAttenuationFn attenFn, ColorSrc ambSrcColor)
        {
            Enable = enable;
            MaterialSrcColor = matSrcColor;
            LitMask = litMask;
            DiffuseFunction = diffFn;
            AttenuationFunction = attenFn;
            AmbientSrcColor = ambSrcColor;
        }

        public ChannelControl(EndianBinaryReader reader)
        {
            Enable              = reader.ReadBoolean();
            MaterialSrcColor    = (ColorSrc)reader.ReadByte();
            LitMask             = (LightId)reader.ReadByte();
            DiffuseFunction     = (DiffuseFn)reader.ReadByte();
            AttenuationFunction = (J3DAttenuationFn)reader.ReadByte();
            AmbientSrcColor     = (ColorSrc)reader.ReadByte();

            reader.SkipInt16();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Enable);
            writer.Write((byte)MaterialSrcColor);
            writer.Write((byte)LitMask);
            writer.Write((byte)DiffuseFunction);
            writer.Write((byte)AttenuationFunction);
            writer.Write((byte)AmbientSrcColor);

            writer.Write((short)-1);
        }

        public static bool operator==(ChannelControl left, ChannelControl right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(ChannelControl left, ChannelControl right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = Convert.ToInt32(Enable);
            hash ^= (int)MaterialSrcColor << 4;
            hash ^= (int)LitMask << 4;
            hash ^= (int)DiffuseFunction << 6;
            hash ^= (int)AttenuationFunction << 5;
            hash ^= (int)AmbientSrcColor << 2;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ChannelControl))
                return false;
            else
                return Equals((ChannelControl)obj);
        }

        public bool Equals(ChannelControl other)
        {
            return Enable == other.Enable &&
                MaterialSrcColor == other.MaterialSrcColor &&
                LitMask == other.LitMask &&
                DiffuseFunction == other.DiffuseFunction &&
                AttenuationFunction == other.AttenuationFunction &&
                AmbientSrcColor == other.AmbientSrcColor;
        }
    }
}
