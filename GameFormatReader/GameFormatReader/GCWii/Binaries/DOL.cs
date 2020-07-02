using System;
using System.IO;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Binaries
{
    /// <summary>
    /// Represents a GameCube executable.
    /// </summary>
    public sealed class DOL
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filepath">Path to the DOL file.</param>
        public DOL(string filepath)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            if (!File.Exists(filepath))
                throw new FileNotFoundException($"File {filepath} does not exist.", filepath);

            ReadDOL(filepath);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Data that contains the DOL.</param>
        /// <param name="offset">Offset in the data to begin reading at.</param>
        public DOL(byte[] data, int offset)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), $"{nameof(offset)} cannot be less than zero.");

            if (offset >= data.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), $"{nameof(offset)} cannot be larger than the given data.");

            ReadDOL(data, offset);
        }

        #endregion

        #region Structs

        /// <summary>
        /// Represents an arbitrary section within a DOL.
        /// </summary>
        public struct Section
        {
            /// <summary>File offset within the DOL for this section.</summary>
            public uint FileOffset  { get; internal set; }
            /// <summary>Load address for this section</summary>
            public uint LoadAddress { get; internal set; }
            /// <summary>Section data</summary>
            public byte[] Data      { get; internal set; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The data sections within this DOL.
        /// </summary>
        public Section[] DataSections
        {
            get;
            private set;
        }

        /// <summary>
        /// The text sections within this DOL.
        /// </summary>
        public Section[] TextSections
        {
            get;
            private set;
        }

        /// <summary>
        /// Stored BSS address
        /// </summary>
        public uint BSSAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// Size of the BSS
        /// </summary>
        public uint BSSSize
        {
            get;
            private set;
        }

        /// <summary>
        /// DOL entry point
        /// </summary>
        public uint EntryPoint
        {
            get;
            private set;
        }

        #endregion

        #region Private

        private void ReadDOL(string filepath)
        {
            byte[] dolBytes = File.ReadAllBytes(filepath);

            ReadDOL(dolBytes, 0);
        }

        private void ReadDOL(byte[] data, int offset)
        {
            using (var reader = new EndianBinaryReader(new MemoryStream(data), Endian.Big))
            {
                InitializeTextSections(reader);
                InitializeDataSections(reader);

                // Skip to BSS stuff.
                reader.BaseStream.Position = 0xD8;
                BSSAddress = reader.ReadUInt32();
                BSSSize    = reader.ReadUInt32();
                EntryPoint = reader.ReadUInt32();
            }
        }

        private void InitializeTextSections(EndianBinaryReader reader)
        {
            TextSections = new Section[7];

            for (int i = 0; i < TextSections.Length; i++)
            {
                TextSections[i] = new Section();
                TextSections[i].FileOffset  = reader.ReadUInt32At(0x00 + (i*4)); // 4 == length of offset value
                TextSections[i].LoadAddress = reader.ReadUInt32At(0x48 + (i*4));
                uint size                   = reader.ReadUInt32At(0x90 + (i*4));

                TextSections[i].Data        = reader.ReadBytesAt(TextSections[i].FileOffset, (int)size);
            }
        }

        private void InitializeDataSections(EndianBinaryReader reader)
        {
            DataSections = new Section[11];

            for (int i = 0; i < DataSections.Length; i++)
            {
                DataSections[i] = new Section();
                DataSections[i].FileOffset  = reader.ReadUInt32At(0x1C + (i*4));
                DataSections[i].LoadAddress = reader.ReadUInt32At(0x64 + (i*4));
                uint size                   = reader.ReadUInt32At(0xAC + (i*4));

                DataSections[i].Data        = reader.ReadBytesAt(DataSections[i].FileOffset, (int)size);
            }
        }

        #endregion
    }
}
