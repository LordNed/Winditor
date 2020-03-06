using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using GameFormatReader.Common;
using SuperBMDLib.Util;

namespace SuperBMDLib.Materials.IO
{
    public static class ColorIO
    {
        public static List<Color> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<Color> colors = new List<Color>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
            {
                byte r = reader.ReadByte();
                byte g = reader.ReadByte();
                byte b = reader.ReadByte();
                byte a = reader.ReadByte();

                colors.Add(new Color((float)r / 255, (float)g / 255, (float)b / 255, (float)a / 255));
            }

            return colors;
        }

        public static void Write(EndianBinaryWriter writer, List<Color> colors)
        {
            foreach (Color col in colors)
                writer.Write(col);
        }
    }
}
