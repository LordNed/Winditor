using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class CullModeIO
    {
        public static List<CullMode> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<CullMode> modes = new List<CullMode>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
                modes.Add((CullMode)reader.ReadInt32());

            return modes;
        }

        public static void Write(EndianBinaryWriter writer, List<CullMode> modes)
        {
            foreach (CullMode mode in modes)
                writer.Write((int)mode);
        }
    }
}
