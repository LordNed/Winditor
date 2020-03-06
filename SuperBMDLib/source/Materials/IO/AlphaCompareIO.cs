using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class AlphaCompareIO
    {
        public static List<AlphaCompare> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<AlphaCompare> compares = new List<AlphaCompare>();
            int count = size / 8;

            for (int i = 0; i < count; i++)
                compares.Add(new AlphaCompare(reader));

            return compares;
        }

        public static void Write(EndianBinaryWriter writer, List<AlphaCompare> comps)
        {
            foreach (AlphaCompare comp in comps)
                comp.Write(writer);
        }
    }
}
