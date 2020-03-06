using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using Assimp;
using SuperBMDLib.Util;
using SuperBMDLib.Rigging;

namespace SuperBMDLib.BMD
{
    public class DRW1
    {
        public List<bool> WeightTypeCheck { get; private set; }
        public List<int> Indices { get; private set; }

        public List<Weight> MeshWeights { get; private set; }

        public DRW1()
        {
            WeightTypeCheck = new List<bool>();
            Indices = new List<int>();
            MeshWeights = new List<Weight>();
        }

        public DRW1(EndianBinaryReader reader, int offset)
        {
            Indices = new List<int>();

            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
            reader.SkipInt32();
            int drw1Size = reader.ReadInt32();
            int entryCount = reader.ReadInt16();
            reader.SkipInt16();

            int boolDataOffset = reader.ReadInt32();
            int indexDataOffset = reader.ReadInt32();

            WeightTypeCheck = new List<bool>();

            reader.BaseStream.Seek(offset + boolDataOffset, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < entryCount; i++)
                WeightTypeCheck.Add(reader.ReadBoolean());

            reader.BaseStream.Seek(offset + indexDataOffset, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < entryCount; i++)
                Indices.Add(reader.ReadInt16());

            reader.BaseStream.Seek(offset + drw1Size, System.IO.SeekOrigin.Begin);
        }

        public DRW1(Scene scene, Dictionary<string, int> boneNameDict)
        {
            WeightTypeCheck = new List<bool>();
            Indices = new List<int>();

            MeshWeights = new List<Weight>();
            List<Weight> fullyWeighted = new List<Weight>();
            List<Weight> partiallyWeighted = new List<Weight>();

            SortedDictionary<int, Weight> weights = new SortedDictionary<int, Weight>();

            foreach (Mesh mesh in scene.Meshes)
            {
                foreach (Assimp.Bone bone in mesh.Bones)
                {
                    foreach (VertexWeight assWeight in bone.VertexWeights)
                    {
                        if (!weights.ContainsKey(assWeight.VertexID))
                        {
                            weights.Add(assWeight.VertexID, new Weight());
                            weights[assWeight.VertexID].AddWeight(assWeight.Weight, boneNameDict[bone.Name]);
                        }
                        else
                        {
                            weights[assWeight.VertexID].AddWeight(assWeight.Weight, boneNameDict[bone.Name]);
                        }
                    }
                }

                foreach (Weight weight in weights.Values)
                {
                    weight.reorderBones();
                    if (weight.WeightCount == 1)
                    {
                        if (!fullyWeighted.Contains(weight))
                            fullyWeighted.Add(weight);
                    }
                    else
                    {
                        if (!partiallyWeighted.Contains(weight))
                            partiallyWeighted.Add(weight);
                    }
                }

                weights.Clear();
            }

            MeshWeights.AddRange(fullyWeighted);
            MeshWeights.AddRange(partiallyWeighted);

            foreach (Weight weight in MeshWeights)
            {
                if (weight.WeightCount == 1)
                {
                    WeightTypeCheck.Add(false);
                    Indices.Add(weight.BoneIndices[0]);
                }
                else
                {
                    WeightTypeCheck.Add(true);
                    Indices.Add(0); // This will get filled with the correct value when SHP1 is generated
                }
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            long start = writer.BaseStream.Position;

            writer.Write("DRW1".ToCharArray());
            writer.Write(0); // Placeholder for section size
            writer.Write((short)WeightTypeCheck.Count);
            writer.Write((short)-1);

            writer.Write(20); // Offset to weight type bools, always 20
            writer.Write(20 + WeightTypeCheck.Count); // Offset to indices, always 20 + number of weight type bools

            foreach (bool bol in WeightTypeCheck)
                writer.Write(bol);

            foreach (int inte in Indices)
                writer.Write((short)inte);

            StreamUtility.PadStreamWithString(writer, 32);

            long end = writer.BaseStream.Position;
            long length = (end - start);

            writer.Seek((int)start + 4, System.IO.SeekOrigin.Begin);
            writer.Write((int)length);
            writer.Seek((int)end, System.IO.SeekOrigin.Begin);
        }
    }
}
