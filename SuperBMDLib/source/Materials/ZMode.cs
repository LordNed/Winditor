using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials
{
    public struct ZMode : IEquatable<ZMode>
    {
        /// <summary> If false, ZBuffering is disabled and the Z buffer is not updated. </summary>
        public bool Enable;

        /// <summary> Determines the comparison that is performed.
        /// The newely rasterized Z value is on the left while the value from the Z buffer is on the right.
        /// If the result of the comparison is false, the newly rasterized pixel is discarded. </summary>
        public CompareType Function;

        /// <summary> If true, the Z buffer is updated with the new Z value after a comparison is performed. 
        /// Example: Disabling this would prevent a write to the Z buffer, useful for UI elements or other things
        /// that shouldn't write to Z Buffer. See glDepthMask. </summary>
        public bool UpdateEnable;

        public ZMode(bool enable, CompareType func, bool update)
        {
            Enable = enable;
            Function = func;
            UpdateEnable = update;
        }

        public ZMode(EndianBinaryReader reader)
        {
            Enable = reader.ReadBoolean();
            Function = (CompareType)reader.ReadByte();
            UpdateEnable = reader.ReadBoolean();
            reader.SkipByte();
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Enable);
            writer.Write((byte)Function);
            writer.Write(UpdateEnable);
            writer.Write((sbyte)-1);
        }

        public static bool operator ==(ZMode left, ZMode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ZMode left, ZMode right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = Convert.ToInt32(Enable);
            hash ^= (int)Function << 3;
            hash ^= Convert.ToInt32(UpdateEnable) << 2;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ZMode))
                return false;
            else
                return Equals((ZMode)obj);
        }

        public bool Equals(ZMode other)
        {
            return Enable == other.Enable &&
                Function == other.Function &&
                UpdateEnable == other.UpdateEnable;
        }
    }
}
