using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class TevSwapModeIO
    {
        public static List<TevSwapMode> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<TevSwapMode> modes = new List<TevSwapMode>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
                modes.Add(new TevSwapMode(reader));

            return modes;
        }

        public static void Write(EndianBinaryWriter writer, List<TevSwapMode> modes)
        {
            foreach (TevSwapMode mode in modes)
                mode.Write(writer);
        }
    }
}
