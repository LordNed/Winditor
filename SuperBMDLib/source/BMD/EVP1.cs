using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Rigging;
using Assimp;
using GameFormatReader.Common;
using OpenTK;
using SuperBMDLib.Util;

namespace SuperBMDLib.BMD
{
    public class EVP1
    {
        public List<Weight> Weights {get; private set;}
        public List<Matrix4> InverseBindMatrices { get; private set; }

        public EVP1()
        {
            Weights = new List<Weight>();
            InverseBindMatrices = new List<Matrix4>();
        }

        public EVP1(EndianBinaryReader reader, int offset)
        {
            Weights = new List<Weight>();
            InverseBindMatrices = new List<Matrix4>();

            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
            reader.SkipInt32();
            int evp1Size = reader.ReadInt32();
            int entryCount = reader.ReadInt16();
            reader.SkipInt16();

            int weightCountsOffset = reader.ReadInt32();
            int boneIndicesOffset = reader.ReadInt32();
            int weightDataOffset = reader.ReadInt32();
            int inverseBindMatricesOffset = reader.ReadInt32();

            List<int> counts = new List<int>();
            List<float> weights = new List<float>();
            List<int> indices = new List<int>();

            for (int i = 0; i < entryCount; i++)
                counts.Add(reader.ReadByte());

            reader.BaseStream.Seek(boneIndicesOffset + offset, System.IO.SeekOrigin.Begin);

            for (int i = 0; i < entryCount; i++)
            {
                for (int j = 0; j < counts[i]; j++)
                {
                    indices.Add(reader.ReadInt16());
                }
            }

            reader.BaseStream.Seek(weightDataOffset + offset, System.IO.SeekOrigin.Begin);

            for (int i = 0; i < entryCount; i++)
            {
                for (int j = 0; j < counts[i]; j++)
                {
                    weights.Add(reader.ReadSingle());
                }
            }

            int totalRead = 0;
            for (int i = 0; i < entryCount; i++)
            {
                Weight weight = new Weight();

                for (int j = 0; j < counts[i]; j++)
                {
                    weight.AddWeight(weights[totalRead + j], indices[totalRead + j]);
                }

                Weights.Add(weight);
                totalRead += counts[i];
            }

            reader.BaseStream.Seek(inverseBindMatricesOffset + offset, System.IO.SeekOrigin.Begin);
            int matrixCount = (evp1Size - inverseBindMatricesOffset) / 48;

            for (int i = 0; i < matrixCount; i++)
            {
                Matrix3x4 invBind = new Matrix3x4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                                                  reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                                                  reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                InverseBindMatrices.Add(new Matrix4(invBind.Row0, invBind.Row1, invBind.Row2, Vector4.UnitW));
            }

            reader.BaseStream.Seek(offset + evp1Size, System.IO.SeekOrigin.Begin);
        }

        public EVP1(Scene scene, List<Rigging.Bone> flatSkeleton)
        {
            Weights = new List<Weight>();

            foreach (Mesh mesh in scene.Meshes)
            {
                SortedDictionary<int, Weight> weights = new SortedDictionary<int, Weight>();

                foreach (Assimp.Bone bone in mesh.Bones)
                {
                    Rigging.Bone bmdBone = flatSkeleton.Find(x => x.Name == bone.Name);

                    foreach (VertexWeight vertWeight in bone.VertexWeights)
                    {
                        if (vertWeight.Weight > 1.0f)
                        {
                            if (!weights.ContainsKey(vertWeight.VertexID))
                            {
                                weights.Add(vertWeight.VertexID, new Weight());
                            }

                            weights[vertWeight.VertexID].AddWeight(vertWeight.Weight, flatSkeleton.IndexOf(bmdBone));
                        }
                    }

                    Matrix4 invBind = new Matrix4(
                        bone.OffsetMatrix.A1, bone.OffsetMatrix.A2, bone.OffsetMatrix.A3, bone.OffsetMatrix.A4,
                        bone.OffsetMatrix.B1, bone.OffsetMatrix.B2, bone.OffsetMatrix.B3, bone.OffsetMatrix.B4,
                        bone.OffsetMatrix.C1, bone.OffsetMatrix.C2, bone.OffsetMatrix.C3, bone.OffsetMatrix.C4,
                        bone.OffsetMatrix.D1, bone.OffsetMatrix.D2, bone.OffsetMatrix.D3, bone.OffsetMatrix.D4);

                    bmdBone.SetInverseBindMatrix(invBind);
                }

                Weights.AddRange(weights.Values);
                foreach (Weight weight in Weights) {
                    weight.reorderBones();
                }
            }
        }

        public void SetInverseBindMatrices(Assimp.Scene scene, List<Rigging.Bone> flatSkel)
        {
            for (int i = 0; i < flatSkel.Count; i++)
                InverseBindMatrices.Add(new Matrix4(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW));

            foreach (Mesh mesh in scene.Meshes)
            {
                foreach (Assimp.Bone bone in mesh.Bones)
                {
                    Matrix4x4 assMat = bone.OffsetMatrix;

                    Matrix4 transposed = new Matrix4(assMat.A1, assMat.B1, assMat.C1, assMat.D1,
                                                     assMat.A2, assMat.B2, assMat.C2, assMat.D2,
                                                     assMat.A3, assMat.B3, assMat.C3, assMat.D3,
                                                     assMat.A4, assMat.B4, assMat.C4, assMat.D4);

                    int index = flatSkel.FindIndex(x => x.Name == bone.Name);
                    InverseBindMatrices[index] = transposed;
                    flatSkel[index].SetInverseBindMatrix(transposed);
                }
            }
        }

        public void SetInverseBindMatrices(List<Rigging.Bone> flatSkel)
        {
            if (InverseBindMatrices.Count == 0)
            {
                // If the original file didn't specify any inverse bind matrices, use default values instead of all zeroes.
                // And these must be set both in the skeleton and the EVP1.
                for (int i = 0; i < flatSkel.Count; i++)
                {
                    Matrix4 newMat = new Matrix4(Vector4.UnitX, Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW);
                    InverseBindMatrices.Add(newMat);
                    flatSkel[i].SetInverseBindMatrix(newMat);
                }
                return;
            }

            for (int i = 0; i < flatSkel.Count; i++)
            {
                Matrix4 newMat = InverseBindMatrices[i];
                flatSkel[i].SetInverseBindMatrix(newMat);
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            long start = writer.BaseStream.Position;

            writer.Write("EVP1".ToCharArray());
            writer.Write(0); // Placeholder for section size
            writer.Write((short)Weights.Count);
            writer.Write((short)-1);

            if (Weights.Count == 0)
            {
                writer.Write((int)0);
                writer.Write((int)0);
                writer.Write((int)0);
                writer.Write((int)0);
                writer.Seek((int)start + 4, System.IO.SeekOrigin.Begin);
                writer.Write(32);
                writer.Seek(0, System.IO.SeekOrigin.End);
                StreamUtility.PadStreamWithString(writer, 8);
                return;
            }

            writer.Write(28); // Offset to weight count data. Always 28
            writer.Write(28 + Weights.Count); // Offset to bone/weight indices. Always 28 + the number of weights
            writer.Write(0); // Placeholder for weight data offset
            writer.Write(0); // Placeholder for inverse bind matrix data offset

            foreach (Weight w in Weights)
                writer.Write((byte)w.WeightCount);

            foreach (Weight w in Weights)
            {
                foreach (int inte in w.BoneIndices)
                    writer.Write((short)inte);
            }

            StreamUtility.PadStreamWithString(writer, 4);

            long curOffset = writer.BaseStream.Position;

            writer.Seek((int)start + 20, System.IO.SeekOrigin.Begin);
            writer.Write((int)(curOffset - start));
            writer.Seek((int)curOffset, System.IO.SeekOrigin.Begin);

            foreach (Weight w in Weights)
            {
                foreach (float fl in w.Weights)
                    writer.Write(fl);
            }

            curOffset = writer.BaseStream.Position;

            writer.Seek((int)start + 24, System.IO.SeekOrigin.Begin);
            writer.Write((int)(curOffset - start));
            writer.Seek((int)curOffset, System.IO.SeekOrigin.Begin);

            foreach (Matrix4 mat in InverseBindMatrices)
            {
                Vector4 Row1 = mat.Column0;
                Vector4 Row2 = mat.Column1;
                Vector4 Row3 = mat.Column2;

                writer.Write(Row1.X);
                writer.Write(Row1.Y);
                writer.Write(Row1.Z);
                writer.Write(Row1.W);

                writer.Write(Row2.X);
                writer.Write(Row2.Y);
                writer.Write(Row2.Z);
                writer.Write(Row2.W);

                writer.Write(Row3.X);
                writer.Write(Row3.Y);
                writer.Write(Row3.Z);
                writer.Write(Row3.W);
            }

            StreamUtility.PadStreamWithString(writer, 32);

            long end = writer.BaseStream.Position;
            long length = (end - start);

            writer.Seek((int)start + 4, System.IO.SeekOrigin.Begin);
            writer.Write((int)length);
            writer.Seek((int)end, System.IO.SeekOrigin.Begin);
        }
    }
}
