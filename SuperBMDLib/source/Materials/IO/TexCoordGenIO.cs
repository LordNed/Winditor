using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class TexCoordGenIO
    {
        public static List<TexCoordGen> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<TexCoordGen> gens = new List<TexCoordGen>();
            int count = size / 4;

            for (int i = 0; i < count; i++)
                gens.Add(new TexCoordGen(reader));

            return gens;
        }

        public static void Write(EndianBinaryWriter writer, List<TexCoordGen> gens)
        {
            foreach (TexCoordGen gen in gens)
                gen.Write(writer);
        }
    }
}
