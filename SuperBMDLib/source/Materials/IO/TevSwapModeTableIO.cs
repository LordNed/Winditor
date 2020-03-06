using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class TevSwapModeTableIO
    {
        public static List<TevSwapModeTable> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<TevSwapModeTable> modes = new List<TevSwapModeTable>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
                modes.Add(new TevSwapModeTable(reader));

            return modes;
        }

        public static void Write(EndianBinaryWriter writer, List<TevSwapModeTable> tables)
        {
            foreach (TevSwapModeTable table in tables)
                table.Write(writer);
        }
    }
}
