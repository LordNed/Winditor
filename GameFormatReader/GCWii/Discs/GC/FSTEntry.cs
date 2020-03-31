namespace GameFormatReader.GCWii.Discs.GC
{
    /// <summary>
    /// Represents an entry within the FST.
    /// </summary>
    public sealed class FSTEntry
    {
        #region Public Fields

        /// <summary>
        /// The size in bytes for each entry in the FST.
        /// </summary>
        public const int EntrySize = 0xC;

        #endregion

        #region Constructor

        internal FSTEntry()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Filename offset within the string table.
        /// </summary>
        public uint NameOffset
        {
            get;
            internal set;
        }

        /// <summary>
        /// Actual file offset.
        /// </summary>
        public uint Offset
        {
            get;
            internal set;
        }

        /// <summary>
        /// Length of the file.
        /// </summary>
        /// <remarks>
        /// In the case of the root directory, this will be the total number of entries.
        /// </remarks>
        public uint FileSize
        {
            get;
            internal set;
        }

        /// <summary>
        /// The full name of the entry.
        /// </summary>
        public string Fullname
        {
            get;
            internal set;
        }

        /// <summary>
        /// Whether or not this FSTEntry represents a directory.
        /// </summary>
        public bool IsDirectory
        {
            get { return (NameOffset & 0xFF000000) != 0; }
        }

        #endregion
    }
}
