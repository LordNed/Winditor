using GameFormatReader.Common;
using OpenTK;
using System;
using System.Collections.Generic;
using WindEditor;

namespace J3DRenderer.JStudio.Animation
{
    public class BCK
    {
        private abstract class KeyframeData<T>
        {
            public short Unknown0;
            public T[] Keys;

            public virtual T Interpolate(float animNormalizedTime, int animLength, float modulatedTime)
            {
                int curKeyframe = WMath.Floor(Keys.Length * animNormalizedTime);
                int nextKeyframe = curKeyframe + ((curKeyframe == Keys.Length - 1) ? 0 : 1);

                float prevTimeInSeconds = ((curKeyframe / (float)Keys.Length) * animLength) / kAnimFramerate;
                float nextTimeInSeconds = ((nextKeyframe / (float)Keys.Length) * animLength) / kAnimFramerate;

                // We use the animNormalizedTime to figure out which two keyframes to use and then calculate 
                // a new 0-1 value between the two to see how much we should interpolate their values.
                float subT = (modulatedTime - prevTimeInSeconds) / (nextTimeInSeconds - prevTimeInSeconds);
                subT = WMath.Clamp(subT, 0, 1);

                //if (i == 5)
                //{
                //    Console.WriteLine("numJoints: {0} timeInFrames: {1} animFrame: {2} normalizedAnimTime: {3} modulatedTime: {4} numKeyframes: {5} curKeyframe: {6} nextKeyframe: {7} prevTimeInSeconds: {8}, nextTimeInSeconds: {9} t: {10}",
                //        numJoints, timeInFrames, animFrame, normalizedAnimTime, modulatedTime, numKeyframes, curKeyframe, nextKeyframe, prevTimeInSeconds, nextTimeInSeconds, scaleXTVal);
                //}

                return Interpolate(Keys[curKeyframe], Keys[nextKeyframe], subT);
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

        private class FloatKeyframeData : KeyframeData<float>
        {
            protected override float Interpolate(float t1, float t2, float t)
            {
                return t1 * (1 - t) + t2 * t;
            }
        }

        private class JointAnimationData
        {
            public FloatKeyframeData[] TranslationData;
            public FloatKeyframeData[] RotationData;
            public FloatKeyframeData[] ScaleData;

            public JointAnimationData()
            {
                TranslationData = new FloatKeyframeData[3];
                RotationData = new FloatKeyframeData[3];
                ScaleData = new FloatKeyframeData[3];
            }

            public void Interpolate(float f, int animLength, float modulatedTime, out Vector3 translation, out Quaternion rotation, out Vector3 scale)
            {
                translation = new Vector3(TranslationData[0].Interpolate(f, animLength, modulatedTime), TranslationData[1].Interpolate(f, animLength, modulatedTime), TranslationData[2].Interpolate(f, animLength, modulatedTime));
                scale = new Vector3(ScaleData[0].Interpolate(f, animLength, modulatedTime), ScaleData[1].Interpolate(f, animLength, modulatedTime), ScaleData[2].Interpolate(f, animLength, modulatedTime));

                // ZYX order
                rotation =  Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(RotationData[2].Interpolate(f, animLength, modulatedTime))) *
                            Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(RotationData[1].Interpolate(f, animLength, modulatedTime))) *
                            Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(RotationData[0].Interpolate(f, animLength, modulatedTime)));

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
                jointData.Interpolate(0f, AnimLength, modulatedTime, out translation, out rotation, out scale);

                pose[i].Translation = translation;
                pose[i].Rotation = rotation;
                pose[i].Scale = scale;
            }
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
                        Flag = (LoopType)reader.ReadByte(); // 0 = Play Once. 2 = Loop
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
                        {
                            short val = reader.ReadInt16();
                            rotationData[j] = (float)Math.Pow(2f, AngleMultiplier) * WMath.RotationShortToFloat(val);
                        }

                        // Read array of translation/position data
                        float[] translationData = new float[numTranslateFloatEntries];
                        reader.BaseStream.Position = tagStart + translateDataOffset;
                        for (int j = 0; j < numTranslateFloatEntries; j++)
                            translationData[j] = reader.ReadSingle();

                        // Read the data for each joint that this animation.
                        AnimationData = new List<JointAnimationData>();
                        reader.BaseStream.Position = tagStart + jointDataOffset;
                        for (int j = 0; j < jointEntryCount; j++)
                        {
                            JointAnimationData jointData = new JointAnimationData();

                            short[,] xData = new short[3, 3];
                            short[,] yData = new short[3, 3];
                            short[,] zData = new short[3, 3];

                            // X
                            for (int k = 0; k < 3; k++)
                            {
                                xData[k, 0] = reader.ReadInt16(); // keyframeCount
                                xData[k, 1] = reader.ReadInt16(); // startIndex
                                xData[k, 2] = reader.ReadInt16(); // unknown0
                            }

                            // Y
                            for (int k = 0; k < 3; k++)
                            {
                                yData[k, 0] = reader.ReadInt16();
                                yData[k, 1] = reader.ReadInt16();
                                yData[k, 2] = reader.ReadInt16();
                            }

                            // Z
                            for (int k = 0; k < 3; k++)
                            {
                                zData[k, 0] = reader.ReadInt16();
                                zData[k, 1] = reader.ReadInt16();
                                zData[k, 2] = reader.ReadInt16();
                            }

                            jointData.ScaleData = LoadData(scaleData, 0, xData, yData, zData);
                            jointData.RotationData = LoadData(rotationData, 1, xData, yData, zData);
                            jointData.TranslationData = LoadData(translationData, 2, xData, yData, zData);

                            AnimationData.Add(jointData);
                        }

                        break;
                    default:
                        Console.WriteLine("Unsupported section in BCK File: {0}", tagName); break;
                }
            }
        }

        private static FloatKeyframeData[] LoadData(float[] dataBank, int type, short[,] xData, short[,] yData, short[,] zData)
        {
            FloatKeyframeData[] keyframeData = new FloatKeyframeData[3];
            keyframeData[0] = new FloatKeyframeData();
            keyframeData[0].Keys = new float[xData[type, 0]];
            for (int k = 0; k < keyframeData[0].Keys.Length; k++)
                keyframeData[0].Keys[k] = dataBank[xData[type, 1] + k]; // startIndex + 
            keyframeData[0].Unknown0 = xData[type, 2];

            keyframeData[1] = new FloatKeyframeData();
            keyframeData[1].Keys = new float[yData[type, 0]];
            for (int k = 0; k < keyframeData[1].Keys.Length; k++)
                keyframeData[1].Keys[k] = dataBank[yData[type, 1] + k]; // startIndex + 
            keyframeData[1].Unknown0 = yData[type, 2];

            keyframeData[2] = new FloatKeyframeData();
            keyframeData[2].Keys = new float[zData[type, 0]];
            for (int k = 0; k < keyframeData[2].Keys.Length; k++)
                keyframeData[2].Keys[k] = dataBank[zData[type, 1] + k]; // startIndex + 
            keyframeData[2].Unknown0 = zData[type, 2];

            return keyframeData;
        }
    }
}
