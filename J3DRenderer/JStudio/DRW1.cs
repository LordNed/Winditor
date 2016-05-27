using System.Collections.Generic;
using GameFormatReader.Common;
using System.Diagnostics;
using OpenTK;
using WindEditor;

namespace J3DRenderer.JStudio
{
    public class DRW1
    {
        public List<bool> IsWeighted;
        public List<ushort> Indexes;

        public void LoadDRW1FromStream(EndianBinaryReader reader, long tagStart)
        {
            ushort sectionCount = reader.ReadUInt16();
            Trace.Assert(reader.ReadUInt16() == 0xFFFF); // Padding

            uint isWeightedOffset = reader.ReadUInt32();
            uint indexOffset = reader.ReadUInt32();

            IsWeighted = new List<bool>();
            Indexes = new List<ushort>();

            reader.BaseStream.Position = tagStart + isWeightedOffset;
            for (int k = 0; k < sectionCount; k++)
                IsWeighted.Add(reader.ReadBoolean());

            reader.BaseStream.Position = tagStart + indexOffset;
            for (int k = 0; k < sectionCount; k++)
                Indexes.Add(reader.ReadUInt16());
        }
    }
}
