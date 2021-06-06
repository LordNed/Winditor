using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace WindEditor
{
    public static class DOL
    {
        public static long AddressToOffset(long address, EndianBinaryReader reader)
        {
            // Check text sections
            for (int i = 0; i < 7; i++)
            {
                uint startOff = reader.ReadUInt32At(0x00 + i * 4);
                uint startAddr = reader.ReadUInt32At(0x48 + i * 4);
                uint size = reader.ReadUInt32At(0x90 + i * 4);
                if (address >= startAddr && address < (startAddr + size))
                {
                    return (address - startAddr) + startOff;
                }
            }

            // Check data sections
            for (int i = 0; i < 11; i++)
            {
                uint startOff = reader.ReadUInt32At(0x1C + i * 4);
                uint startAddr = reader.ReadUInt32At(0x64 + i * 4);
                uint size = reader.ReadUInt32At(0xAC + i * 4);
                if (address >= startAddr && address < (startAddr + size))
                {
                    return (address - startAddr) + startOff;
                }
            }

            throw new ArgumentException($"Address 0x{address:X} could not be found in the DOL.");
        }
    }
}
