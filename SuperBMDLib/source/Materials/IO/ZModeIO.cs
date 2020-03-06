using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class ZModeIO
    {
        public static List<ZMode> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<ZMode> modes = new List<ZMode>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
                modes.Add(new ZMode(reader));

            return modes;
        }

        public static void Write(EndianBinaryWriter writer, List<ZMode> modes)
        {
            foreach (ZMode mode in modes)
                mode.Write(writer);
        }
    }
}
