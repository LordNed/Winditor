using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class IndirectTexturingIO
    {
        public static List<IndirectTexturing> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<IndirectTexturing> indirects = new List<IndirectTexturing>();
            int count = size / 312;

            for (int i = 0; i < count; i++)
                indirects.Add(new IndirectTexturing(reader));

            return indirects;
        }

        public static void Write(EndianBinaryWriter writer, List<IndirectTexturing> indTex)
        {
            foreach (IndirectTexturing ind in indTex)
            {
                ind.Write(writer);
            }
        }
    }
}
