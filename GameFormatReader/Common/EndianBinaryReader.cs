using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// TODO: ReadDecimal method equivalent.
// How do you even go about flipping the endianness on this type?

namespace GameFormatReader.Common
{
    /// <summary>
    /// <see cref="BinaryReader"/> implementation that can read in different <see cref="Endian"/> formats.
    /// </summary>
    public sealed class EndianBinaryReader : BinaryReader
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
        public EndianBinaryReader(Stream stream, Endian endian) : base(stream)
        {
            CurrentEndian = endian;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to wrap within this EndianBinaryReader.</param>
        /// <param name="encoding">The <see cref="Encoding"/> to use for characters.</param>
        /// <param name="endian">The <see cref="Endian"/> to use when reading files.</param>
        public EndianBinaryReader(Stream stream, Encoding encoding, Endian endian) : base(stream, encoding)
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
        public EndianBinaryReader(Stream stream, Encoding encoding, bool leaveOpen, Endian endian) : base (stream, encoding, leaveOpen)
        {
            CurrentEndian = endian;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data to encapsulate</param>
        /// <param name="endian"><see cref="Endian"/> to use when reading the data.</param>
        public EndianBinaryReader(byte[] data, Endian endian)
            : base (new MemoryStream(data))
        {
            CurrentEndian = endian;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data to encapsulate</param>
        /// <param name="encoding">The <see cref="Encoding"/> to use for characters.</param>
        /// <param name="endian"><see cref="Endian"/> to use when reading the data.</param>
        public EndianBinaryReader(byte[] data, Encoding encoding, Endian endian)
            : base(new MemoryStream(data), encoding)
        {
            CurrentEndian = endian;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data to encapsulate</param>
        /// <param name="encoding">The <see cref="Encoding"/> to use for characters.</param>
        /// <param name="leaveOpen">Whether or not to leave the stream open after this EndianBinaryReader is disposed.</param>
        /// <param name="endian"><see cref="Endian"/> to use when reading the data.</param>
        public EndianBinaryReader(byte[] data, Encoding encoding, bool leaveOpen, Endian endian)
            : base(new MemoryStream(data), encoding, leaveOpen)
        {
            CurrentEndian = endian;
        }

        #endregion

        #region Public Methods

        #region Overrides

        public override short ReadInt16()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadInt16();
            }

            // BE to LE or LE to BE
            return base.ReadInt16().SwapBytes();
        }

        public override ushort ReadUInt16()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadUInt16();
            }

            // BE to LE or LE to BE
            return base.ReadUInt16().SwapBytes();
        }

        public override int ReadInt32()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadInt32();
            }

            // BE to LE or LE to BE
            return base.ReadInt32().SwapBytes();
        }

        public override uint ReadUInt32()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadUInt32();
            }

            // BE to LE or LE to BE
            return base.ReadUInt32().SwapBytes();
        }

        public override long ReadInt64()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadInt64();
            }

            // BE to LE or LE to BE
            return base.ReadInt64().SwapBytes();
        }

        public override ulong ReadUInt64()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadUInt64();
            }

            // BE to LE or LE to BE
            return base.ReadUInt64().SwapBytes();
        }

        public override float ReadSingle()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadSingle();
            }
            
            // BE to LE or LE to BE
            byte[] floatBytes = BitConverter.GetBytes(base.ReadUInt32());
            Array.Reverse(floatBytes);

            return BitConverter.ToSingle(floatBytes, 0);
        }

        public override double ReadDouble()
        {
            if (systemLittleEndian && CurrentEndian == Endian.Little ||
                !systemLittleEndian && CurrentEndian == Endian.Big)
            {
                return base.ReadDouble();
            }

            byte[] doubleBytes = BitConverter.GetBytes(base.ReadUInt64());
            Array.Reverse(doubleBytes);

            return BitConverter.ToDouble(doubleBytes, 0);
        }

        /// <summary>
        /// Reads a specific number of characters from the 
        /// underlying <see cref="Stream"/>.
        /// </summary>
        /// <param name="length">The numer of characters to read.</param>
        /// <returns>The string of characters of length <paramref name="length"/>.</returns>
        public string ReadString(uint length)
        {
            string resultStr = string.Empty;
            for (uint i = 0; i < length; i++)
                resultStr += ReadChar();

            return resultStr;
        }

        #endregion

        #region PeekRead[x] Methods

        /// <summary>
        /// Reads a signed byte relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the signed byte that was read.</returns>
        public sbyte PeekReadSByte()
        {
            sbyte res = ReadSByte();

            BaseStream.Position -= sizeof(SByte);

            return res;
        }

        /// <summary>
        /// Reads an unsigned byte relative to the current position
        /// in the underlying <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the byte that was read.</returns>
        public byte PeekReadByte()
        {
            byte res = ReadByte();

            BaseStream.Position -= sizeof(Byte);

            return res;
        }

        /// <summary>
        /// Reads a signed 16-bit integer relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the signed 16-bit integer that was read.</returns>
        public short PeekReadInt16()
        {
            short res = ReadInt16();

            BaseStream.Position -= sizeof(Int16);

            return res;
        }

        /// <summary>
        /// Reads an unsigned 16-bit integer relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the unsigned 16-bit integer that was read.</returns>
        public ushort PeekReadUInt16()
        {
            ushort res = ReadUInt16();

            BaseStream.Position -= sizeof(UInt16);

            return res;
        }

        /// <summary>
        /// Reads a signed 32-bit integer relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the signed 32-bit integer that was read.</returns>
        public int PeekReadInt32()
        {
            int res = ReadInt32();

            BaseStream.Position -= sizeof(Int32);

            return res;
        }

        /// <summary>
        /// Reads an unsigned 32-bit integer relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the unsigned 32-bit integer that was read.</returns>
        public uint PeekReadUInt32()
        {
            uint res = ReadUInt32();

            BaseStream.Position -= sizeof(UInt32);

            return res;
        }

        /// <summary>
        /// Reads a signed 64-bit integer relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the signed 64-bit integer that was read.</returns>
        public long PeekReadInt64()
        {
            long res = ReadInt64();

            BaseStream.Position -= sizeof(Int64);

            return res;
        }

        /// <summary>
        /// Reads an unsigned 64-bit integer relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the unsigned 64-bit integer that was read.</returns>
        public ulong PeekReadUInt64()
        {
            ulong res = ReadUInt64();

            BaseStream.Position -= sizeof(UInt64);

            return res;
        }

        /// <summary>
        /// Reads a 32-bit floating-point number relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the 32-bit floating-point number that was read.</returns>
        public float PeekReadSingle()
        {
            float res = ReadSingle();

            BaseStream.Position -= sizeof(Single);

            return res;
        }

        /// <summary>
        /// Reads a 64-bit floating-point number relative to the current position
        /// in the underlying  <see cref="Stream"/> without advancing position.
        /// </summary>
        /// <returns>the 64-bit floating-point number that was read.</returns>
        public double PeekReadDouble()
        {
            double res = ReadDouble();

            BaseStream.Position -= sizeof(Double);

            return res;
        }

        /// <summary>
        /// Reads count number of bytes into an array without
        /// advancing the position of the underlying <see cref="Stream"/>.
        /// </summary>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>byte array containing the bytes read.</returns>
        public byte[] PeekReadBytes(int count)
        {
            if (count < 0)
                throw new ArgumentException($"{nameof(count)} cannot be negative", nameof(count));

            byte[] res = ReadBytes(count);

            BaseStream.Position -= count;

            return res;
        }

        #endregion

        #region Read[x]At Methods

        /// <summary>
        /// Reads a boolean at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the boolean value read at the offset.</returns>
        public bool ReadBooleanAt(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            bool res = ReadBoolean();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a signed byte at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the signed byte read at the offset.</returns>
        public sbyte ReadSByteAt(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            sbyte res = ReadSByte();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads an unsigned byte at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the unsigned byte read at the offset.</returns>
        public byte ReadByteAt(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            byte res = ReadByte();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a signed 16-bit integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the signed 16-bit integer read at the offset.</returns>
        public short ReadInt16At(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            short res = ReadInt16();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads an unsigned 16-bit integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the unsigned 16-bit integer read at the offset.</returns>
        public ushort ReadUInt16At(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            ushort res = ReadUInt16();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a signed 32-bit integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the signed 32-bit integer read at the offset.</returns>
        public int ReadInt32At(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            int res = ReadInt32();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads an unsigned 32-bit integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the unsigned 32-bit integer read at the offset.</returns>
        public uint ReadUInt32At(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            uint res = ReadUInt32();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a signed 64-bit integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the signed 64-bit integer read at the offset.</returns>
        public long ReadInt64At(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            long res = ReadInt64();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads an unsigned 64-bit integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the unsigned 64-bit integer read at the offset.</returns>
        public ulong ReadUInt64At(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            ulong res = ReadUInt64();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a 32-bit floating point integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the 32-bit floating point integer read at the offset.</returns>
        public float ReadSingleAt(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            float res = ReadSingle();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a 64-bit floating point integer at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the 64-bit floating point integer read at the offset.</returns>
        public double ReadDoubleAt(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            double res = ReadDouble();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a decimal type at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the decimal type read at the offset.</returns>
        public decimal ReadDecimalAt(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            decimal res = ReadDecimal();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads an character at a given offset without 
        /// changing the underlying <see cref="Stream"/> position.
        /// </summary>
        /// <param name="offset">The offset to read at.</param>
        /// <returns>the character read at the offset.</returns>
        public ulong ReadCharAt(long offset)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            char res = ReadChar();

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a count amount of bytes starting at the given offset
        /// without changing the position of the underlying <see cref="Stream"/>.
        /// </summary>
        /// <param name="offset">Offset to begin reading at.</param>
        /// <param name="count">Number of bytes to read.</param>
        /// <returns>Byte array containing the read bytes.</returns>
        public byte[] ReadBytesAt(long offset, int count)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            byte[] res = ReadBytes(count);

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        /// <summary>
        /// Reads a count amount of characters starting at the given offset
        /// without changing the position of the underlying <see cref="Stream"/>.
        /// </summary>
        /// <param name="offset">Offset to begin reading at.</param>
        /// <param name="count">Number of characters to read.</param>
        /// <returns>Character array containing the read characters.</returns>
        public char[] ReadCharsAt(long offset, int count)
        {
            long origPos = BaseStream.Position;

            // Seek to the given offset
            BaseStream.Position = offset;

            // Read
            char[] res = ReadChars(count);

            // Flip back to the original position.
            BaseStream.Position = origPos;

            return res;
        }

        #endregion

        #region Read[x]Until Methods

        /// <summary>
        /// Reads bytes from the underlying <see cref="Stream"/>
        /// until the given terminating byte is hit.
        /// </summary>
        /// <param name="terminator">The terminator to stop reading at.</param>
        /// <returns>The array of bytes read until the terminator was hit.</returns>
        public byte[] ReadBytesUntil(byte terminator)
        {
            List<byte> bytes = new List<byte>();
            byte b;

            while ((b = ReadByte()) != terminator)
            {
                bytes.Add(b);
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// Reads characters from the underlying <see cref="Stream"/>
        /// until the given terminating character is hit.
        /// </summary>
        /// <param name="terminator">The terminator to stop reading at.</param>
        /// <returns>The array of characters read until the terminator was hit.</returns>
        public char[] ReadBytesUntil(char terminator)
        {
            List<char> chars = new List<char>();
            char c;

            while ((c = ReadChar()) != terminator)
            {
                chars.Add(c);
            }

            return chars.ToArray();
        }

        /// <summary>
        /// Reads characters from the underlying <see cref="Stream"/>
        /// until the given terminating character is hit.
        /// </summary>
        /// <param name="terminator">The terminator to stop reading at.</param>
        /// <returns>The string of characters read until the terminator was hit.</returns>
        public string ReadStringUntil(char terminator)
        {
            StringBuilder sb = new StringBuilder();
            char c;

            while ((c = ReadChar()) != terminator)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        #endregion

        #region Skip[x] Methods

        // While some of these may be equivalents, depending on what is being read
        // (ie. a file format structure), it may be more readable to use one or the other.
        // eg. it may be more readable to say an unsigned int is being skipped than a signed int.


        /// <summary>
        /// Skips the underlying <see cref="Stream"/> ahead by
        /// count bytes from its current position.
        /// </summary>
        /// <param name="count">The number of bytes to skip.</param>
        public void Skip(long count)
        {
            if (count >= BaseStream.Length)
                throw new ArgumentException($"{nameof(count)} cannot be larger than the length of the underlying stream.", nameof(count));

            if ((BaseStream.Position + count) >= BaseStream.Length)
                throw new ArgumentException("Skipping " + count + " bytes would exceed the underlying stream's length.");

            BaseStream.Position += count;
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of an unsigned byte.
        /// </summary>
        public void SkipByte()
        {
            Skip(sizeof(Byte));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of a signed byte.
        /// </summary>
        public void SkipSByte()
        {
            Skip(sizeof(SByte));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of a signed 16-bit integer.
        /// </summary>
        public void SkipInt16()
        {
            Skip(sizeof(Int16));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of an unsigned 16-bit integer.
        /// </summary>
        public void SkipUInt16()
        {
            Skip(sizeof(UInt16));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of a signed 32-bit integer.
        /// </summary>
        public void SkipInt32()
        {
            Skip(sizeof(Int32));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of an unsigned 32-bit integer.
        /// </summary>
        public void SkipUInt32()
        {
            Skip(sizeof(UInt32));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of a signed 64-bit integer.
        /// </summary>
        public void SkipInt64()
        {
            Skip(sizeof(Int64));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of an unsigned 64-bit integer.
        /// </summary>
        public void SkipUInt64()
        {
            Skip(sizeof(UInt64));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of a 32-bit floating-point number.
        /// </summary>
        public void SkipSingle()
        {
            Skip(sizeof(Single));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of a 64-bit floating-point number.
        /// </summary>
        public void SkipDouble()
        {
            Skip(sizeof(Double));
        }

        /// <summary>
        /// Skips the underlying <see cref="Stream"/>
        /// ahead by the size of a 128-bit floating-point number.
        /// </summary>
        public void SkipDecimal()
        {
            Skip(sizeof(Decimal));
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string endianness = (CurrentEndian == Endian.Little) ? "Little Endian" : "Big Endian";

            return $"EndianBinaryReader - Endianness: {endianness}";
        }

        #endregion

        #endregion
    }
}
