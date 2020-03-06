using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.Mdl
{
    public struct XFCommand
    {
        public XFRegister Register;
        public List<XFCommandArgument> Args;

        public XFCommand(XFRegister register)
        {
            Register = register;
            Args = new List<XFCommandArgument>();
        }

        public void Write(EndianBinaryWriter writer)
        {
            if (Args.Count <= 0)
            {
                throw new Exception("Cannot save an XF command with no arguments.");
            }

            writer.Write((byte)0x10);
            writer.Write((short)(Args.Count - 1));
            writer.Write((short)Register);

            foreach (XFCommandArgument i in Args)
                writer.Write(i.Value);
        }
    }

    public struct XFCommandArgument
    {
        public int Value;

        public XFCommandArgument(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Sets the specified range of bits to the given value.
        /// </summary>
        /// <param name="value">Value to set the bits to</param>
        /// <param name="offset">Starting position of the desired bits to modify</param>
        /// <param name="size">Number of bits to modify</param>
        public void SetBits(int value, int offset, int size)
        {
            Value = Value & (int)(~((0xFFFFFFFF >> (32 - size)) << offset) & 0xFFFFFFFF) | (value << offset);
        }

        /// <summary>
        /// Sets the specified bit to the given boolean value.
        /// </summary>
        /// <param name="flag">Boolean value to set the bit to</param>
        /// <param name="offset">Position of the bit to set</param>
        public void SetFlag(bool flag, int offset)
        {
            if (flag)
                SetBits(1, offset, 1);
            else
                SetBits(0, offset, 1);
        }
    }
}
