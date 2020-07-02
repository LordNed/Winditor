using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs.Wii
{
    /// <summary>
    /// Represents the <see cref="DiscHeader"/> of a Wii disc.
    /// </summary>
    public sealed class DiscHeaderWii : DiscHeader
    {
        #region Constructor

        internal DiscHeaderWii(EndianBinaryReader reader)
        {
            ReadHeader(reader);
        }

        #endregion

        #region Protected Methods

        // Reads the common portion of the header between the Gamecube and Wii discs.
        // Disc classes for GC and Wii must implement the rest of the reading.
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
            reader.BaseStream.Position += 14;

            MagicWord = reader.ReadInt32();

            // Skip the other 4 bytes, since this is a Wii header, not a GameCube one.
            reader.BaseStream.Position += 4;

            GameTitle = new string(reader.ReadChars(64));
            IsHashVerificationDisabled = reader.ReadBoolean();
            IsDiscEncryptionDisabled = reader.ReadBoolean();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Whether or not hash verification is disabled. Will make all disc reads fail even before they reach the DVD drive.
        /// </summary>
        public bool IsHashVerificationDisabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not disc encryption and h3 hash table loading and verification is disabled.
        /// (which effectively also makes all disc reads fail because the h2 hashes won't be able to verify against "something" that will be in the memory of the h3 hash table. none of these two bytes will allow unsigned code) 
        /// </summary>
        public bool IsDiscEncryptionDisabled
        {
            get;
            private set;
        }

        #endregion
    }
}
