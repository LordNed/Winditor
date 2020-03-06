using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;
using SuperBMDLib.Materials.Enums;
using SuperBMDLib.Util;
using Newtonsoft.Json;

namespace SuperBMDLib.Materials
{
    public struct TexMatrix : IEquatable<TexMatrix>
    {
        public TexGenType Projection;
        public byte Type;

        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 EffectTranslation;

        [JsonConverter(typeof(Vector2Converter))]
        public Vector2 Scale;
        public float Rotation;

        [JsonConverter(typeof(Vector2Converter))]
        public Vector2 Translation;

        [JsonConverter(typeof(Matrix4Converter))]
        public Matrix4 ProjectionMatrix;

        [JsonConstructor]
        public TexMatrix(TexGenType projection, byte type, Vector3 effectTranslation, Vector2 scale, float rotation, Vector2 translation, Matrix4 matrix)
        {
            Projection = projection;
            Type = type;
            EffectTranslation = effectTranslation;

            Scale = scale;
            Rotation = rotation;
            Translation = translation;

            ProjectionMatrix = matrix;
        }

        public TexMatrix(EndianBinaryReader reader)
        {
            Projection = (TexGenType)reader.ReadByte();
            Type = reader.ReadByte();
            reader.SkipInt16();
            EffectTranslation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Scale = new Vector2(reader.ReadSingle(), reader.ReadSingle());
            Rotation = reader.ReadInt16() * (180 / 32768f);
            reader.SkipInt16();
            Translation = new Vector2(reader.ReadSingle(), reader.ReadSingle());

            ProjectionMatrix = new Matrix4(
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write((byte)Projection);
            writer.Write(Type);
            writer.Write((short)-1);
            writer.Write(EffectTranslation);
            writer.Write(Scale);
            writer.Write((short)(Rotation * (32768.0f / 180)));
            writer.Write((short)-1);
            writer.Write(Translation);
            writer.Write(ProjectionMatrix);
        }

        public static bool operator ==(TexMatrix left, TexMatrix right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TexMatrix left, TexMatrix right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = (int)Projection;
            hash ^= Type;
            hash ^= EffectTranslation.GetHashCode() << 7;
            hash ^= Scale.GetHashCode() << 2;
            hash ^= Rotation.GetHashCode() << 6;
            hash ^= Translation.GetHashCode() << 3;
            hash ^= ProjectionMatrix.GetHashCode() << 6;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TexMatrix))
                return false;
            else
                return Equals((TexMatrix)obj);
        }

        public bool Equals(TexMatrix other)
        {
            return Projection == other.Projection &&
                Type == other.Type &&
                EffectTranslation == other.EffectTranslation &&
                Scale == other.Scale &&
                Rotation == other.Rotation &&
                Translation == other.Translation &&
                ProjectionMatrix == other.ProjectionMatrix;
        }
    }
}
