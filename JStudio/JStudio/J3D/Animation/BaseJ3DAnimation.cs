using GameFormatReader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JStudio.J3D.Animation
{
    public enum LoopType
    {
        Once = 0,
        Loop = 2
    }

    public enum TangentType : ushort
    {
        TangentIn = 0,
        TangentInOut = 1
    }

    public abstract class BaseJ3DAnimation
    {
        protected struct AnimIndex
        {
            public ushort Count;
            public ushort Index;
            public TangentType KeyTangentType;
        }

        protected struct AnimComponent
        {
            public AnimIndex Scale;
            public AnimIndex Rotation;
            public AnimIndex Translation;
        }

        protected struct AnimatedJoint
        {
            public AnimComponent X;
            public AnimComponent Y;
            public AnimComponent Z;
        }

        protected class Key
        {
            public float Time;
            public float Value;
            public float TangentIn;
            public float TangentOut;

            public Key(float time, float val, float tangentIn, float tangentOut)
            {
                Time = time;
                Value = val;
                TangentIn = tangentIn;
                TangentOut = tangentOut;
            }

            public Key(float time, float val, float tangentIn) : this(time, val, tangentIn, tangentIn) { }
        }

        public string Name { get; protected set; }
        public string Magic { get; protected set; }
        public string AnimType { get; protected set; }
        public LoopType LoopMode { get; protected set; }
        public short AnimLengthInFrames { get; protected set; }

        public string AnimLengthInSeconds { get { return string.Format("{0}s", (AnimLengthInFrames / kAnimFramerate).ToString("0.00")); } }

        protected const float kAnimFramerate = 30f;
        protected float m_timeSinceStartedPlaying;
        protected bool m_isPlaying;

        public BaseJ3DAnimation(string name)
        {
            Name = name;
        }

        public virtual void Tick(float deltaTime)
        {
            if(m_isPlaying)
                m_timeSinceStartedPlaying += deltaTime;
        }

        public virtual void Start()
        {
            m_isPlaying = true;
            m_timeSinceStartedPlaying = 0f;
        }

        public virtual void Stop()
        {
            m_isPlaying = false;
            m_timeSinceStartedPlaying = 0f;
        }

        public virtual void Pause()
        {
            m_isPlaying = false;
        }

        public virtual void Resume()
        {
            m_isPlaying = true;
        }

        protected virtual float GetAnimValue(List<Key> keys, float t)
        {
            if (keys.Count == 0)
                return 0f;

            if (keys.Count == 1)
                return keys[0].Value;

            int i = 1;
            while (keys[i].Time < t)
                i++;

            float time = (t - keys[i - 1].Time) / (keys[i].Time - keys[i - 1].Time); // Scale to [0, 1]
            return CubicInterpolation(keys[i - 1], keys[i], time);
        }

        protected virtual float CubicInterpolation(Key key1, Key key2, float t)
        {
            float a = 2 * (key1.Value - key2.Value) + key1.TangentOut + key2.TangentIn;
            float b = -3 * key1.Value + 3 * key2.Value - 2 * key1.TangentOut - key2.TangentIn;
            float c = key1.TangentOut;
            float d = key1.Value;

            return ((a * t + b) * t + c) * t + d;   
        }

        protected void ConvertRotation(List<Key> rots, float scale)
        {
            for (int j = 0; j < rots.Count; j++)
            {
                rots[j].Value *= scale;
                rots[j].TangentIn *= scale;
                rots[j].TangentOut *= scale;
            }
        }

        protected AnimIndex ReadAnimIndex(EndianBinaryReader stream)
        {
            return new AnimIndex { Count = stream.ReadUInt16(), Index = stream.ReadUInt16(), KeyTangentType = (TangentType)stream.ReadUInt16() };
        }

        protected AnimComponent ReadAnimComponent(EndianBinaryReader stream)
        {
            return new AnimComponent { Scale = ReadAnimIndex(stream), Rotation = ReadAnimIndex(stream), Translation = ReadAnimIndex(stream) };
        }

        protected AnimatedJoint ReadAnimJoint(EndianBinaryReader stream)
        {
            return new AnimatedJoint { X = ReadAnimComponent(stream), Y = ReadAnimComponent(stream), Z = ReadAnimComponent(stream) };
        }

        protected List<Key> ReadComp(float[] src, AnimIndex index)
        {
            List<Key> ret = new List<Key>();

            if (index.Count == 1)
            {
                ret.Add(new Key(0f, src[index.Index], 0f, 0f));
            }
            else
            {
                for (int j = 0; j < index.Count; j++)
                {
                    Key key = null;
                    if (index.KeyTangentType == TangentType.TangentIn)
                        key = new Key(src[index.Index + 3 * j + 0], src[index.Index + 3 * j + 1], src[index.Index + 3 * j + 2]);
                    else
                        key = new Key(src[index.Index + 4 * j + 0], src[index.Index + 4 * j + 1], src[index.Index + 4 * j + 2], src[index.Index + 4 * j + 3]);

                    ret.Add(key);
                }
            }

            return ret;
        }
    }
}
