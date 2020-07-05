using System;
using System.IO;
using System.Text;

// TODO: Write method equivalent for the decimal type.
// How do you even go about flipping the endianness on this type?

namespace GameFormatReader.Common
{
    /// <summary>
    /// <see cref="BinaryWriter"/> implementation that can write in different <see cref="Endian"/> formats.
    /// </summary>
    public sealed class EndianBinaryWriter : BinaryWriter
    {
        #region Fields

        // Underlying endianness being used by the .NET Framework.
        private static readonly bool systemLittleEndian = BitConverter.IsLittleEndian;

        #endregion

        #region Properties

        /// <summary>
        /// Current <see cref="Endian"/> this EndianBinaryReader is using.
        /// </summary>
        public Endian CurrentEndian
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor. Uses UTF-8 by default for character encoding.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to wrap within this EndianBinaryReader.</param>
        /// <param name="endian">The <see cref="Endian"/> to use when reading files..</param>
        public EndianBinaryWriter(Stream stream, Endian endian)
            : base(stream)
        {
            CurrentEndian = endian;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to wrap within this EndianBinaryReader.</param>
        /// <param name="encoding">The <see cref="Encoding"/> to use for characters.</param>
        /// <param name="endian">The <see cref="Endian"/> to use when reading files.</param>
        public EndianBinaryWriter(Stream stream, Encoding encoding, Endian endian)
            : base(stream, encoding)
        {
            CurrentEndian = endian;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to wrap within this EndianBinaryReader.</param>
        /// <param name="encoding">The <see cref="Encoding"/> to use for characters.</param>
        /// <param name="leaveOpen">Whether or not to leave the stream open after this EndianBinaryReader is disposed.</param>
        /// <param name="endian">The <see cref="Endian"/> to use when reading from files.</param>
        public EndianBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen, Endian endian)
            : base(stream, encoding, leaveOpen)
        {
            CurrentEndian = endian;
        }

        #endregion

        #region Public Methods

        #region Overrides

        public override void Write(short value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                base.Write(value.SwapBytes());
            }
        }

        public override void Write(ushort value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                base.Write(value.SwapBytes());
            }
        }

        public override void Write(int value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                base.Write(value.SwapBytes());
            }
        }

        public override void Write(uint value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                base.Write(value.SwapBytes());
            }
        }

        public override void Write(long value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                base.Write(value.SwapBytes());
            }
        }

        public override void Write(ulong value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                base.Write(value.SwapBytes());
            }
        }

        public override void Write(float value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                byte[] floatBytes = BitConverter.GetBytes(value);
                Array.Reverse(floatBytes);

                base.Write(BitConverter.ToSingle(floatBytes, 0));
            }
        }

        public override void Write(double value)
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                base.Write(value);
            }
            else // BE to LE or LE to BE
            {
                byte[] doubleBytes = BitConverter.GetBytes(value);
                Array.Reverse(doubleBytes);

                base.Write(BitConverter.ToDouble(doubleBytes, 0));
            }
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Writes a specific number of characters to the underlying 
        /// <see cref="Stream"/>. If <paramref name="length"/> is greater
        /// than <paramref name="str"/>'s length, the length will be
        /// padded with zeros. Similarly, if <paramref name="length"/> is
        /// smaller than <paramref name="str"/>'s length then the string
        /// will be truncated.
        /// </summary>
        /// <param name="str">String to write to the stream.</param>
        /// <param name="length">Maximum number of characters to write.</param>
        public void WriteFixedString(string str, int length)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            if (length < 0)
                throw new ArgumentException("Cannot write a negative length string.");

            for (int i = 0; i < length; i++)
            {
                Write((i < str.Length) ? str[i] : '\0');
            }
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string endianness = (CurrentEndian == Endian.Little) ? "Little Endian" : "Big Endian";

            return $"EndianBinaryWriter - Endianness: {endianness}";
        }

        #endregion

        #endregion
    }
}
