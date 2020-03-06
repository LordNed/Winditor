using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class BlendModeIO
    {
        public static List<BlendMode> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<BlendMode> modes = new List<BlendMode>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
                modes.Add(new BlendMode(reader));

            return modes;
        }

        public static void Write(EndianBinaryWriter writer, List<BlendMode> modes)
        {
            foreach (BlendMode mode in modes)
                mode.Write(writer);
        }
    }
}
