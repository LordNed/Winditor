using GameFormatReader.Common;
using OpenTK;
using System;
using System.Collections.Generic;
using WindEditor;

namespace JStudio.J3D.Animation
{
    /// <summary>
    /// Represents a bone animation for the J3D model format. Bones are applied by index order, so there is no
    /// information stored about which animation goes to which bone.
    /// </summary>
    public class BCK : BaseJ3DAnimation
    {
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

        public int BoneCount { get { return m_animationData.Count; } }

        private List<JointAnim> m_animationData;

        public BCK(string name) : base(name) { }

        public void LoadFromStream(EndianBinaryReader reader)
        {
            // Read the J3D Header
            Magic = new string(reader.ReadChars(4)); // "J3D1"
            AnimType = new string(reader.ReadChars(4)); // bck1

            int fileSize = reader.ReadInt32();
            int tagCount = reader.ReadInt32();

            // Skip over an unused space.
            reader.Skip(16);

            LoadTagDataFromStream(reader, tagCount);
        }

        public void ApplyAnimationToPose(List<SkeletonJoint> pose)
        {
            if (pose.Count != m_animationData.Count)
                Console.WriteLine("Mis-matched number of joints in pose and number of joints in animation!");

            int numJoints = Math.Min(pose.Count, m_animationData.Count);

            float ftime = (m_timeSinceStartedPlaying * kAnimFramerate) % AnimLengthInFrames;

            for(int i = 0; i < numJoints; i++)
            {
                pose[i].Scale = new Vector3(GetAnimValue(m_animationData[i].ScalesX, ftime), GetAnimValue(m_animationData[i].ScalesY, ftime), GetAnimValue(m_animationData[i].ScalesZ, ftime));

                Vector3 rot = new Vector3(GetAnimValue(m_animationData[i].RotationsX, ftime), GetAnimValue(m_animationData[i].RotationsY, ftime), GetAnimValue(m_animationData[i].RotationsZ, ftime));

                // ZYX order
                pose[i].Rotation = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(rot.Z)) *
                                   Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(rot.Y)) *
                                   Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(rot.X));

                Vector3 translation = new Vector3(GetAnimValue(m_animationData[i].TranslationsX, ftime), GetAnimValue(m_animationData[i].TranslationsY, ftime), GetAnimValue(m_animationData[i].TranslationsZ, ftime));

                pose[i].Translation = translation;
            }
        }

        private void LoadTagDataFromStream(EndianBinaryReader reader, int tagCount)
        {
            for (int i = 0; i < tagCount; i++)
            {
                long tagStart = reader.BaseStream.Position;

                string tagName = reader.ReadString(4);
                int tagSize = reader.ReadInt32();

                switch (tagName)
                {
                    case "ANK1":
                        LoadANK1FromStream(reader, tagStart);

                        break;
                    default:
                        Console.WriteLine("Unsupported section in BCK File: {0}", tagName); break;
                }
            }
        }

        private void LoadANK1FromStream(EndianBinaryReader reader, long tagStart)
        {
            LoopMode = (LoopType)reader.ReadByte(); // 0 = Play Once. 2 = Loop
            byte angleMultiplier = reader.ReadByte(); // Multiply Angle Value by pow(2, angleMultiplier)
            AnimLengthInFrames = reader.ReadInt16();
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

            // Read array of translation/position data
            float[] translationData = new float[numTranslateFloatEntries];
            reader.BaseStream.Position = tagStart + translateDataOffset;
            for (int j = 0; j < numTranslateFloatEntries; j++)
                translationData[j] = reader.ReadSingle();

            // Read the data for each joint that this animation.
            m_animationData = new List<JointAnim>();
            float rotScale = (float)Math.Pow(2f, angleMultiplier) * (180 / 32768f);

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

                m_animationData.Add(joint);
            }
        }
    }
}
