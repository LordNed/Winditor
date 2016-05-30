using GameFormatReader.Common;
using OpenTK;
using System;
using System.Collections.Generic;
using WindEditor;

namespace J3DRenderer.JStudio.Animation
{
    public class BCK
    {
        private class KeyframeData
        {
            public short KeyframeCount;
            public short FirstIndex;
            public short Unknown0; // Either 0 or 1?

            public override string ToString()
            {
                return string.Format("KeyframeCount: {0} FirstIndex: {1} Unknown0: {2}", KeyframeCount, FirstIndex, Unknown0);
            }
        }

        private abstract class KeyframeData<T>
        {
            public short Unknown0;
            public T[] Keys;

            public virtual T Interpolate(float t)
            {
                int curKeyframe = WMath.Floor(Keys.Length * t);
                int nextKeyframe = curKeyframe + ((curKeyframe == Keys.Length - 1) ? 0 : 1);



                // do logic
                T t1 = default(T), t2 = default(T);
                float newF = 0f;

                return Interpolate(Keys[curKeyframe], Keys[nextKeyframe], newF);
            }

            protected abstract T Interpolate(T t1, T t2, float t);
        }

        private class Vector3KeyframeData : KeyframeData<Vector3>
        {
            protected override Vector3 Interpolate(Vector3 t1, Vector3 t2, float t)
            {
                return Vector3.Lerp(t1, t2, t);
            }
        }

        private class QuaternionKeyframeData :KeyframeData<Quaternion>
        {
            protected override Quaternion Interpolate(Quaternion t1, Quaternion t2, float t)
            {
                return Quaternion.Slerp(t1, t2, t);
            }
        }

        private class JointAnimationData
        {
            public Vector3KeyframeData TranslationData;
            public QuaternionKeyframeData RotationData;
            public Vector3KeyframeData ScaleData;

            public JointAnimationData()
            {
                TranslationData = new Vector3KeyframeData();
                RotationData = new QuaternionKeyframeData();
                ScaleData = new Vector3KeyframeData();
            }

            public void Interpolate(float f, out Vector3 translation, out Quaternion rotation, out Vector3 scale)
            {
                translation = TranslationData.Interpolate(f);
                rotation = RotationData.Interpolate(f);
                scale = ScaleData.Interpolate(f);
            }
        }

        public enum LoopType
        {
            Once = 0,
            Loop = 2
        }

        public string Magic { get; private set; }
        public string StudioType { get; private set; }
        public LoopType Flag { get; set; }
        public byte AngleMultiplier { get; private set; }
        public short AnimLength { get; private set; }

        private const float kAnimFramerate = 30;

        private List<JointAnimationData> AnimationData;

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
                        Flag = (LoopType) reader.ReadByte(); // 0 = Play Once. 2 = Loop
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
                        short[] rotationData = new short[numRotationShortEntries];
                        reader.BaseStream.Position = tagStart + rotationDataOffset;
                        for (int j = 0; j < numRotationShortEntries; j++)
                            rotationData[j] = reader.ReadInt16();

                        // Read array of translation/position data
                        float[] translationData = new float[numTranslateFloatEntries];
                        reader.BaseStream.Position = tagStart + translateDataOffset;
                        for (int j = 0; j < numTranslateFloatEntries; j++)
                            translationData[j] = reader.ReadSingle();

                        // Read the data for each joint that this animation.
                        AnimationData = new List<JointAnimationData>();
                        reader.BaseStream.Position = tagStart + jointDataOffset;
                        for(int j = 0; j < jointEntryCount; j++)
                        {
                            JointAnimationData jointData = new JointAnimationData();

                            // X
                            for (int k = 0; k < 3; k++)
                            {
                                jointData.XData[k] = new KeyframeData();
                                jointData.XData[k].KeyframeCount = reader.ReadInt16();
                                jointData.XData[k].FirstIndex = reader.ReadInt16();
                                jointData.XData[k].Unknown0 = reader.ReadInt16();
                            }

                            // Y
                            for (int k = 0; k < 3; k++)
                            {
                                jointData.YData[k] = new KeyframeData();
                                jointData.YData[k].KeyframeCount = reader.ReadInt16();
                                jointData.YData[k].FirstIndex = reader.ReadInt16();
                                jointData.YData[k].Unknown0 = reader.ReadInt16();
                            }

                            // Z
                            for (int k = 0; k < 3; k++)
                            {
                                jointData.ZData[k] = new KeyframeData();
                                jointData.ZData[k].KeyframeCount = reader.ReadInt16();
                                jointData.ZData[k].FirstIndex = reader.ReadInt16();
                                jointData.ZData[k].Unknown0 = reader.ReadInt16();
                            }

                            AnimationData.Add(jointData);
                        }

                        break;
                    default:
                        Console.WriteLine("Unsupported section in BCK File: {0}", tagName); break;
                }
            }
        }

        public void ApplyAnimationToPose(SkeletonJoint[] pose, float time)
        {
            if (pose.Length != AnimationData.Count)
                Console.WriteLine("Mis-matched number of joints in pose and number of joints in animation!");

            int numJoints = Math.Min(pose.Length, AnimationData.Count);

            // Calculate our current frame based on the time sample.
            int timeInFrames = (int)(time * kAnimFramerate);
            int animFrame = Flag == LoopType.Loop ? timeInFrames % AnimLength : WMath.Clamp(timeInFrames, 0, AnimLength);
            float normalizedAnimTime = animFrame / (float)AnimLength;

            float modulatedTime = time;
            while (modulatedTime > AnimLength / kAnimFramerate)
                modulatedTime -= AnimLength / kAnimFramerate;

            for(int i = 0; i < numJoints; i++)
            {
                JointAnimationData jointData = AnimationData[i];

                Vector3 translation, scale; Quaternion rotation;
                jointData.Interpolate(0f, out translation, out rotation, out scale);

                pose[i].Translation = translation;
                pose[i].Rotation = rotation;
                pose[i].Scale = scale;

                short numKeyframes = jointData.XData[1].KeyframeCount;

                int curKeyframe = WMath.Floor(jointData.XData[1].KeyframeCount * normalizedAnimTime);
                int nextKeyframe = curKeyframe + ((curKeyframe == jointData.XData[1].KeyframeCount - 1) ? 0 : 1);

                //int scaleXIndex = jointData.XData[1].FirstIndex + ;
                //int scaleXIndexNext = (scaleXIndex == jointData.XData[1].KeyframeCount - 1) ? scaleXIndex : scaleXIndex + 1;

                float prevTimeInSeconds = ((curKeyframe / (float)numKeyframes) * AnimLength) / kAnimFramerate;
                float nextTimeInSeconds = ((nextKeyframe / (float)numKeyframes) * AnimLength) / kAnimFramerate;

                float scaleXTVal = (modulatedTime -prevTimeInSeconds)/ (nextTimeInSeconds - prevTimeInSeconds);
                scaleXTVal = WMath.Clamp(scaleXTVal, 0, 1);

                if(i == 5)
                {
                    Console.WriteLine("numJoints: {0} timeInFrames: {1} animFrame: {2} normalizedAnimTime: {3} modulatedTime: {4} numKeyframes: {5} curKeyframe: {6} nextKeyframe: {7} prevTimeInSeconds: {8}, nextTimeInSeconds: {9} t: {10}",
                        numJoints, timeInFrames, animFrame, normalizedAnimTime, modulatedTime, numKeyframes, curKeyframe, nextKeyframe, prevTimeInSeconds, nextTimeInSeconds, scaleXTVal);
                }

                // Interpolate(scaleXIndex, scaleXIndexNext, scaleXTVal);
            }

        }
    }
}
