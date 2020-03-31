using System.Text;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs.GC
{
    /// <summary>
    /// Represents a <see cref="DiscHeader"/> for the GameCube.
    /// Mostly the same as the Wii, except for some minor differences.
    /// </summary>
    public sealed class DiscHeaderGC : DiscHeader
    {
        #region Constructor

        internal DiscHeaderGC(EndianBinaryReader reader)
        {
            ReadHeader(reader);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Offset of the debug monitor (dh.bin).
        /// </summary>
        public uint DebugMonitorOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Load address for the debug monitor.
        /// </summary>
        public uint DebugMonitorLoadAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// Offset to the main DOL executable.
        /// </summary>
        public uint MainDolOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Offset to the FST.
        /// </summary>
        public uint FSTOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Size of the FST.
        /// </summary>
        public uint FSTSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Maximum size of the FST.
        /// Usually the same size as FSTSize.
        /// </summary>
        public uint MaxFSTSize
        {
            get;
            private set;
        }

        #endregion

        #region Private Methods

        // Reads GameCube specific header info.
        protected override void ReadHeader(EndianBinaryReader reader)
        {
            Type = DetermineDiscType(reader.ReadChar());
            GameCode = new string(reader.ReadChars(2));
            RegionCode = DetermineRegion(reader.ReadChar());
            MakerCode = new string(reader.ReadChars(2));
            DiscNumber = reader.ReadByte();
            AudioStreaming = reader.ReadBoolean();
            StreamingBufferSize = reader.ReadByte();

            // Skip unused bytes
            reader.BaseStream.Position += 12;

            MagicWord = reader.ReadInt32();

            // Skip to game title. Read until 0x00 (null char) is hit.
            reader.BaseStream.Position = 0x20;
            GameTitle = Encoding.GetEncoding("shift_jis").GetString(reader.ReadBytesUntil(0x00));

            DebugMonitorOffset = reader.ReadUInt32();
            DebugMonitorLoadAddress = reader.ReadUInt32();

            // Skip unused bytes
            reader.BaseStream.Position = 0x420;

            MainDolOffset = reader.ReadUInt32();
            FSTOffset = reader.ReadUInt32();
            FSTSize = reader.ReadUInt32();
            MaxFSTSize = reader.ReadUInt32();
        }

        #endregion
    }
}
