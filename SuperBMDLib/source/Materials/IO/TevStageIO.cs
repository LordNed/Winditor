using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials;
using GameFormatReader.Common;

namespace SuperBMDLib.Materials.IO
{
    public static class TevStageIO
    {
        public static List<TevStage> Load(EndianBinaryReader reader, int offset, int size)
        {
            List<TevStage> stages = new List<TevStage>();
            int count = size / 20;

            for (int i = 0; i < count; i++)
                stages.Add(new TevStage(reader));

            return stages;
        }

        public static void Write(EndianBinaryWriter writer, List<TevStage> stages)
        {
            foreach (TevStage stage in stages)
                stage.Write(writer);
        }
    }
}
