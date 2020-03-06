using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;
using SuperBMDLib.Util;
using Newtonsoft.Json;

namespace SuperBMDLib.Materials
{
    public struct IndirectTexMatrix : IEquatable<IndirectTexMatrix>
    {
        /// <summary>
        /// The floats that make up the matrix
        /// </summary>
        [JsonConverter(typeof(Matrix2x3Converter))]
        public Matrix2x3 Matrix;
        /// <summary>
        /// The exponent (of 2) to multiply the matrix by
        /// </summary>
        public byte Exponent;

        public IndirectTexMatrix(Matrix2x3 matrix, byte exponent)
        {
            Matrix = matrix;

            Exponent = exponent;
        }

        public IndirectTexMatrix(EndianBinaryReader reader)
        {
            Matrix = new Matrix2x3(
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            Exponent = reader.ReadByte();

            reader.Skip(3);
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(Matrix.M11);
            writer.Write(Matrix.M12);
            writer.Write(Matrix.M13);

            writer.Write(Matrix.M21);
            writer.Write(Matrix.M22);
            writer.Write(Matrix.M23);

            writer.Write((byte)Exponent);
            writer.Write((sbyte)-1);
            writer.Write((short)-1);
        }

        public static bool operator ==(IndirectTexMatrix left, IndirectTexMatrix right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IndirectTexMatrix left, IndirectTexMatrix right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = Exponent;
            hash ^= Matrix.GetHashCode() << 3;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IndirectTexMatrix))
                return false;
            else
                return Equals((IndirectTexMatrix)obj);
        }

        public bool Equals(IndirectTexMatrix other)
        {
            return Exponent == other.Exponent &&
                Matrix == other.Matrix;
        }
    }
}
