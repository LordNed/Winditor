using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class NBTScaleIO
    {
        public static List<NBTScale> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<NBTScale> scales = new List<NBTScale>();
            int count = size / 16;

            for (int i = 0; i < count; i++)
                scales.Add(new NBTScale(reader));

            return scales;
        }

        public static void Write(EndianBinaryWriter writer, List<NBTScale> scales)
        {
            foreach (NBTScale scale in scales)
                scale.Write(writer);
        }
    }
}
