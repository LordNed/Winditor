using GameFormatReader.Common;
using OpenTK;
using System;
using System.Collections.Generic;
using WindEditor;

namespace JStudio.J3D.Animation
{
    public enum LoopType
    {
        Once = 0,
        Loop = 2
    }

    /// <summary>
    /// Represents a bone animation for the J3D model format. Bones are applied by index order.
    /// </summary>
    public class BCK
    {
        struct AnimIndex
        {
            public ushort Count;
            public ushort Index;
            public ushort Unknown0;
        }

        struct AnimComponent
        {
            public AnimIndex Scale;
            public AnimIndex Rotation;
            public AnimIndex Translation;
        }

        struct AnimatedJoint
        {
            public AnimComponent X;
            public AnimComponent Y;
            public AnimComponent Z;
        }

        private class Key
        {
            public float Tangent;
            public float Time;
            public float Value;
        }

        private class JointAnim
        {
            public List<Key> ScalesX = new List<Key>();
            public List<Key> ScalesY = new List<Key>();
            public List<Key> ScalesZ = new List<Key>();

            public List<Key> RotationsX = new List<Key>();
            public List<Key> RotationsY = new List<Key>();
            public List<Key> RotationsZ = new List<Key>();

            public List<Key> TranslationsX = new List<Key>();
            public List<Key> TranslationsY = new List<Key>();
            public List<Key> TranslationsZ = new List<Key>();
        }

        public string Magic { get; private set; }
        public string StudioType { get; private set; }
        public LoopType LoopMode { get; set; }
        public byte AngleMultiplier { get; private set; }
        public short AnimLength { get; private set; }

        private const float kAnimFramerate = 30;

        private List<JointAnim> AnimationData;

        public void LoadFromStream(EndianBinaryReader reader)
        {
            // Read the J3D Header
            Magic = new string(reader.ReadChars(4)); // "J3D1"
            StudioType = new string(reader.ReadChars(4)); // bck1

            int fileSize = reader.ReadInt32();
            int tagCount = reader.ReadInt32();

            // Skip over an unused space.
            reader.Skip(16);

            LoadTagDataFromFile(reader, tagCount);
        }

        public void ApplyAnimationToPose(SkeletonJoint[] pose, float time)
        {
            if (pose.Length != AnimationData.Count)
                Console.WriteLine("Mis-matched number of joints in pose and number of joints in animation!");

            int numJoints = Math.Min(pose.Length, AnimationData.Count);

            time *= kAnimFramerate;
            float ftime = time % AnimLength;

            for(int i = 0; i < numJoints; i++)
            {
                pose[i].Scale = new Vector3(GetAnimValue(AnimationData[i].ScalesX, ftime), GetAnimValue(AnimationData[i].ScalesY, ftime), GetAnimValue(AnimationData[i].ScalesZ, ftime));

                Vector3 rot = new Vector3(GetAnimValue(AnimationData[i].RotationsX, ftime), GetAnimValue(AnimationData[i].RotationsY, ftime), GetAnimValue(AnimationData[i].RotationsZ, ftime));

                // ZYX order
                pose[i].Rotation = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(rot.Z)) *
                                   Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(rot.Y)) *
                                   Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(rot.X));

                Vector3 translation = new Vector3(GetAnimValue(AnimationData[i].TranslationsX, ftime), GetAnimValue(AnimationData[i].TranslationsY, ftime), GetAnimValue(AnimationData[i].TranslationsZ, ftime));

                pose[i].Translation = translation;
            }
        }

        private float GetAnimValue(List<Key> keys, float t)
        {
            if (keys.Count == 0)
                return 0f;

            if (keys.Count == 1)
                return keys[0].Value;

            int i = 1;
            while (keys[i].Time < t)
                i++;

            float time = (t - keys[i - 1].Time) / (keys[i].Time - keys[i - 1].Time); // Scale to [0, 1]
            return Interpolate(keys[i - 1].Value, keys[i - 1].Tangent, keys[i].Value, keys[i].Tangent, time);
        }

        private float Interpolate(float v1, float d1, float v2, float d2, float t)
        {
            // Perform Cubic Interpolation of the values by t
            float a = 2 * (v1 - v2) + d1 + d2;
            float b = -3 * v1 + 3 * v2 - 2 * d1 - d2;
            float c = d1;
            float d = v1;

            return ((a * t + b) * t + c) * t + d;
        }

        private void LoadTagDataFromFile(EndianBinaryReader reader, int tagCount)
        {
            for (int i = 0; i < tagCount; i++)
            {
                long tagStart = reader.BaseStream.Position;

                string tagName = reader.ReadString(4);
                int tagSize = reader.ReadInt32();

                switch (tagName)
                {
                    case "ANK1":
                        LoopMode = (LoopType)reader.ReadByte(); // 0 = Play Once. 2 = Loop
                        AngleMultiplier = reader.ReadByte(); // Multiply Angle Value by pow(2, angleMultiplier)
                        AnimLength = reader.ReadInt16();
                        short jointEntryCount = reader.ReadInt16();
                        short numScaleFloatEntries = reader.ReadInt16();
                        short numRotationShortEntries = reader.ReadInt16();
                        short numTranslateFloatEntries = reader.ReadInt16();
                        int jointDataOffset = reader.ReadInt32();
                        int scaleDataOffset = reader.ReadInt32();
                        int rotationDataOffset = reader.ReadInt32();
                        int translateDataOffset = reader.ReadInt32();

                        // Read array of scale data
                        float[] scaleData = new float[numScaleFloatEntries];
                        reader.BaseStream.Position = tagStart + scaleDataOffset;
                        for (int j = 0; j < numScaleFloatEntries; j++)
                            scaleData[j] = reader.ReadSingle();

                        // Read array of rotation data (but don't convert it)
                        float[] rotationData = new float[numRotationShortEntries];
                        reader.BaseStream.Position = tagStart + rotationDataOffset;
                        for (int j = 0; j < numRotationShortEntries; j++)
                            rotationData[j] = reader.ReadInt16();
                            //Math.Pow(2f, AngleMultiplier) * WMath.RotationShortToFloat(val);

                        // Read array of translation/position data
                        float[] translationData = new float[numTranslateFloatEntries];
                        reader.BaseStream.Position = tagStart + translateDataOffset;
                        for (int j = 0; j < numTranslateFloatEntries; j++)
                            translationData[j] = reader.ReadSingle();

                        // Read the data for each joint that this animation.
                        AnimationData = new List<JointAnim>();
                        float rotScale = (float) Math.Pow(2f, AngleMultiplier) * (180 / 32768f);

                        reader.BaseStream.Position = tagStart + jointDataOffset;
                        for (int j = 0; j < jointEntryCount; j++)
                        {
                            AnimatedJoint animatedJoint = ReadAnimJoint(reader);
                            JointAnim joint = new JointAnim();

                            joint.ScalesX = ReadComp(scaleData, animatedJoint.X.Scale);
                            joint.ScalesY = ReadComp(scaleData, animatedJoint.Y.Scale);
                            joint.ScalesZ = ReadComp(scaleData, animatedJoint.Z.Scale);

                            joint.RotationsX = ReadComp(rotationData, animatedJoint.X.Rotation);
                            joint.RotationsY = ReadComp(rotationData, animatedJoint.Y.Rotation);
                            joint.RotationsZ = ReadComp(rotationData, animatedJoint.Z.Rotation);

                            // Convert all of the rotations from compressed shorts back into -180, 180
                            ConvertRotation(joint.RotationsX, rotScale);
                            ConvertRotation(joint.RotationsY, rotScale);
                            ConvertRotation(joint.RotationsZ, rotScale);

                            joint.TranslationsX = ReadComp(translationData, animatedJoint.X.Translation);
                            joint.TranslationsY = ReadComp(translationData, animatedJoint.Y.Translation);
                            joint.TranslationsZ = ReadComp(translationData, animatedJoint.Z.Translation);

                            AnimationData.Add(joint);
                        }

                        break;
                    default:
                        Console.WriteLine("Unsupported section in BCK File: {0}", tagName); break;
                }
            }
        }

        private void ConvertRotation(List<Key> rots, float scale)
        {
            for(int j = 0; j < rots.Count; j++)
            {
                rots[j].Value *= scale;
                rots[j].Tangent *= scale;
            }
        }


        private AnimIndex ReadAnimIndex(EndianBinaryReader stream)
        {
            return new AnimIndex { Count = stream.ReadUInt16(), Index = stream.ReadUInt16(), Unknown0 = stream.ReadUInt16() };
        }

        private AnimComponent ReadAnimComponent(EndianBinaryReader stream)
        {
            return new AnimComponent { Scale = ReadAnimIndex(stream), Rotation = ReadAnimIndex(stream), Translation = ReadAnimIndex(stream) };
        }

        private AnimatedJoint ReadAnimJoint(EndianBinaryReader stream)
        {
            return new AnimatedJoint { X = ReadAnimComponent(stream), Y = ReadAnimComponent(stream), Z = ReadAnimComponent(stream) };
        }

        private List<Key> ReadComp(float[] src, AnimIndex index)
        {
            List<Key> ret = new List<Key>();

            if (index.Count == 1)
            {
                ret.Add(new Key { Time = 0f, Value = src[index.Index], Tangent = 0f });
            }
            else
            {
                for (int j = 0; j < index.Count; j++)
                {
                    Key key = new Key();
                    key.Time = src[index.Index + 3 * j + 0];
                    key.Value = src[index.Index + 3 * j + 1];
                    key.Tangent = src[index.Index + 3 * j + 2];
                    ret.Add(key);
                }
            }

            return ret;
        }
    }
}
