using System;
using System.IO;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Sound
{
    /// <summary>
    /// A somewhat common sound format used within some Wii games, such as
    /// Super Smash Bros. Brawl and Mario Kart Wii which contains ADPCM sound data.
    /// </summary>
    public sealed class BRSTM
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename">Path to the BRSTM file to load.</param>
        public BRSTM(string filename) : this(File.ReadAllBytes(filename))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Byte array containing BRSTM data.</param>
        public BRSTM(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            BinaryReader br = new BinaryReader(new MemoryStream(data));

            char[] headerID = br.ReadChars(4);

            if (!IsValidHeader(headerID))
                throw new ArgumentException("Cannot read BRSTM file. Incorrect header ID", nameof(data));

            Header = new FileHeader();
            Header.ID = new string(headerID);
            Header.ByteOrderMark = br.ReadBytes(2);

            Endian endianness = IsLittleEndian(Header.ByteOrderMark) ? Endian.Little : Endian.Big;
            using (var reader = new EndianBinaryReader(br.BaseStream, endianness))
            {
                Header.MajorVersion      = reader.ReadByte();
                Header.MinorVersion      = reader.ReadByte();
                Header.FileSize          = reader.ReadInt32();
                Header.HeaderSize        = reader.ReadUInt16();
                Header.NumberOfChunks    = reader.ReadUInt16();
                Header.OffsetToHeadChunk = reader.ReadInt32();
                Header.HeadChunkSize     = reader.ReadInt32();
                Header.OffsetToAdpcChunk = reader.ReadInt32();
                Header.AdpcChunkSize     = reader.ReadInt32();
                Header.OffsetToDataChunk = reader.ReadInt32();
                Header.DataChunkSize     = reader.ReadInt32();
                Header.Unknown           = reader.ReadBytes(24);

                ReadHeadChunk(reader);
                ReadAdpcChunk(reader);
                ReadDataChunk(reader);
            }
        }

        #endregion

        #region Section Classes

        /// <summary>
        /// BRSTM file header
        /// </summary>
        public sealed class FileHeader
        {
            internal FileHeader()
            {
            }

            /// <summary>
            /// Header ID
            /// </summary>
            public string ID { get; internal set; }

            /// <summary>
            /// Byte order mark. Usually 0xFE 0xFF for big endian.
            /// </summary>
            public byte[] ByteOrderMark { get; internal set; }

            /// <summary>
            /// Major version
            /// </summary>
            public byte MajorVersion { get; internal set; }

            /// <summary>
            /// Minor version
            /// </summary>
            public byte MinorVersion { get; internal set; }

            /// <summary>
            /// Size of the whole file.
            /// </summary>
            public int FileSize { get; internal set; }

            /// <summary>
            /// Size of the file header.
            /// </summary>
            public int HeaderSize { get; internal set; }

            /// <summary>
            /// Number of chunks.
            /// </summary>
            public int NumberOfChunks { get; internal set; }

            /// <summary>
            /// Offset to HEAD chunk
            /// </summary>
            public int OffsetToHeadChunk { get; internal set; }

            /// <summary>
            /// HEAD chunk size.
            /// </summary>
            public int HeadChunkSize { get; internal set; }

            /// <summary>
            /// Offset to the ADPC chunk.
            /// </summary>
            public int OffsetToAdpcChunk { get; internal set; }

            /// <summary>
            /// ADPC chunk size.
            /// </summary>
            public int AdpcChunkSize { get; internal set; }

            /// <summary>
            /// Offset to the DATA chunk.
            /// </summary>
            public int OffsetToDataChunk { get; internal set; }

            /// <summary>
            /// DATA chunk size
            /// </summary>
            public int DataChunkSize { get; internal set; }

            /// <summary>
            /// Unknown data. Suspected to be padding.
            /// </summary>
            public byte[] Unknown { get; internal set; }
        }

        /// <summary>
        /// HEAD section
        /// </summary>
        public sealed class HeadSection
        {
            internal HeadSection()
            {
            }

            /// <summary>Section header - "HEAD"</summary>
            public string ID { get; internal set; }

            /// <summary>Section length in bytes</summary>
            public int Length { get; internal set; }

            /// <summary>Section data</summary>
            public byte[] Data { get; internal set; }
        }

        /// <summary>
        /// ADPC section
        /// </summary>
        public sealed class AdpcSection
        {
            internal AdpcSection()
            {
            }

            /// <summary>Section header - "ADPC"</summary>
            public string ID { get; internal set; }

            /// <summary>Section length in bytes.</summary>
            public int Length { get; internal set; }

            /// <summary>Section data</summary>
            public byte[] Data { get; internal set; }
        }

        /// <summary>
        /// DATA section
        /// </summary>
        public sealed class DataSection
        {
            internal DataSection()
            {
            }

            /// <summary>Section header - "DATA"</summary>
            public string ID { get; internal set; }

            /// <summary>Section length in bytes</summary>
            public int Length { get; internal set; }

            /// <summary>Unknown. Suspected to be number of sub-section in ADPCM data.</summary>
            public int Unknown { get; internal set; }

            /// <summary>Padding/Null bytes</summary>
            public byte[] Padding { get; internal set; }

            /// <summary>ADPCM Data</summary>
            public byte[] Data { get; internal set; }

            /// <summary>More padding bytes</summary>
            public byte[] Padding2 { get; internal set; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// File header
        /// </summary>
        public FileHeader Header
        {
            get;
            private set;
        }

        /// <summary>
        /// HEAD section
        /// </summary>
        public HeadSection HEAD
        {
            get;
            private set;
        }

        /// <summary>
        /// ADPC section
        /// </summary>
        public AdpcSection ADPC
        {
            get;
            private set;
        }

        /// <summary>
        /// DATA section
        /// </summary>
        public DataSection DATA
        {
            get;
            private set;
        }

        #endregion

        #region Private Methods

        private void ReadHeadChunk(EndianBinaryReader reader)
        {
            reader.BaseStream.Position = Header.OffsetToHeadChunk;

            HEAD        = new HeadSection();
            HEAD.ID     = new string(reader.ReadChars(4));
            HEAD.Length = reader.ReadInt32();
            HEAD.Data   = reader.ReadBytes(HEAD.Length - 8); // -8 to take reading the ID and length into account.
        }

        private void ReadAdpcChunk(EndianBinaryReader reader)
        {
            reader.BaseStream.Position = Header.OffsetToAdpcChunk;

            ADPC        = new AdpcSection();
            ADPC.ID     = new string(reader.ReadChars(4));
            ADPC.Length = reader.ReadInt32();
            ADPC.Data   = reader.ReadBytes(ADPC.Length - 8); // - 8 to take reading the ID and length into account.
        }

        private void ReadDataChunk(EndianBinaryReader reader)
        {
            reader.BaseStream.Position = Header.OffsetToDataChunk;

            DATA          = new DataSection();
            DATA.ID       = new string(reader.ReadChars(4));
            DATA.Length   = reader.ReadInt32();
            DATA.Unknown  = reader.ReadInt32();
            DATA.Padding  = reader.ReadBytes(14);
            DATA.Data     = reader.ReadBytes(DATA.Length - 26); // - 26 to take into account the previously read data
            DATA.Padding2 = reader.ReadBytes(26);
        }

        #endregion

        #region Static Helper Methods

        // Checks if the file contains a valid header ID.
        private static bool IsValidHeader(char[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Length < 4)
                throw new ArgumentException("Invalid header magic, data too small.", nameof(data));

            return data[0] == 'R' &&
                   data[1] == 'S' &&
                   data[2] == 'T' &&
                   data[3] == 'M';
        }

        private static bool IsLittleEndian(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Length < 2)
                throw new ArgumentException("Byte order mark data cannot be less than 2 bytes in size.", nameof(data));

            return data[0] == 0xFF &&
                   data[1] == 0xFE;
        }

        #endregion
    }
}
