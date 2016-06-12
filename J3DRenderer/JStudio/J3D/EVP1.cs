using GameFormatReader.Common;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JStudio.J3D
{
    public class EVP1
    {
        public class Envelope
        {
            public byte NumBones;
            public ushort[] BoneIndexes;
            public float[] BoneWeights;
        }

        public List<Envelope> Envelopes;
        public List<Matrix4> InverseBindPose;

        public void LoadEVP1FromStream(EndianBinaryReader reader, long tagStart)
        {
            ushort envelopeCount = reader.ReadUInt16();
            Trace.Assert(reader.ReadUInt16() == 0xFFFF); // Padding

            uint boneInfluenceCountOffset = reader.ReadUInt32(); // This points to an array which is envelopeCount many long bytes which specify how many bones influence that particular envelope.
            uint boneIndexDataOffset = reader.ReadUInt32(); // For each influence of each envelope, which bone index is the envelope referring to
            uint weightDataOffset = reader.ReadUInt32(); // For each influence of each envelope, a float indicating how much weight the envelope has.
            uint boneMatrixOffset = reader.ReadUInt32(); // Matrix Table (3x4 float array) - Skeleton Inverse Bind Pose. You have to get the highest index from boneIndex to know how many to read.

            byte[] numBoneInfluences = new byte[envelopeCount];
            InverseBindPose = new List<Matrix4>();
            Envelopes = new List<Envelope>();

            // How many bones influence the given index
            reader.BaseStream.Position = tagStart + boneInfluenceCountOffset;
            for (int i = 0; i < envelopeCount; i++)
                numBoneInfluences[i] = reader.ReadByte();

            // For each influence, an index remap?
            int numMatrices = 0;
            reader.BaseStream.Position = tagStart + boneIndexDataOffset;
            for (int m = 0; m < envelopeCount; m++)
            {
                Envelope env = new Envelope();
                env.NumBones = numBoneInfluences[m];
                env.BoneWeights = new float[env.NumBones];
                env.BoneIndexes = new ushort[env.NumBones];
                Envelopes.Add(env);

                for (int j = 0; j < numBoneInfluences[m]; j++)
                {
                    ushort val = reader.ReadUInt16();
                    env.BoneIndexes[j] = val;
                    numMatrices = Math.Max(numMatrices, val + 1);
                }
            }

            // For each influence, how much does that influence have an affect.
            reader.BaseStream.Position = tagStart + weightDataOffset;
            for (int m = 0; m < envelopeCount; m++)
            {
                Envelope env = Envelopes[m];
                for (int j = 0; j < numBoneInfluences[m]; j++)
                {
                    float val = reader.ReadSingle();
                    env.BoneWeights[j] = val;
                }
            }

            // For each envelope index, what is the Inverse Bind Pose matrix? The Inverse Bind Pose matrix will transform
            // a vertex from being in model space into local space around its bone.
            reader.BaseStream.Position = tagStart + boneMatrixOffset;
            for (int m = 0; m < numMatrices; m++)
            {
                Matrix3x4 matrix = new Matrix3x4();
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 4; k++)
                        matrix[j, k] = reader.ReadSingle();
                }

                Matrix4 bindPoseMatrix = new Matrix4(matrix.Row0, matrix.Row1, matrix.Row2, new Vector4(0, 0, 0, 1));

                // We transpose this matrix for use in OpenTK so it lines up with existing joint-matrix calculations.
                bindPoseMatrix.Transpose();
                InverseBindPose.Add(bindPoseMatrix);
            }
        }
    }
}
