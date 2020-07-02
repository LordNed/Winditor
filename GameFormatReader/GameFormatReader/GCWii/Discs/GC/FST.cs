using System.Collections;
using System.Collections.Generic;
using System.Text;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs.GC
{
    /// <summary>
    /// File-system Table. Contains all of the files within a GameCube <see cref="DiscGC"/>.
    /// </summary>
    public sealed class FST : IEnumerable<FSTEntry>
    {
        #region Private Fields

        private readonly List<FSTEntry> fileEntries = new List<FSTEntry>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader">Reader to populate the FST with.</param>
        internal FST(EndianBinaryReader reader)
        {
            InitializeFST(reader);
        }

        #endregion

        #region Private Methods

        // Initializes the FST and gets all the files and folders set up.
        private void InitializeFST(EndianBinaryReader reader)
        {
            reader.BaseStream.Position = 0x424; // FST offset value is stored here.
            uint FSTOffset = reader.ReadUInt32();
            reader.BaseStream.Position = FSTOffset;

            // We use this to find the string table offset that follows after all the files.
            uint stringTableOffset = FSTOffset;

            // Now get the root.
            FSTEntry root = new FSTEntry();
            root.NameOffset = reader.ReadUInt32();
            root.Offset     = reader.ReadUInt32();
            root.FileSize   = reader.ReadUInt32();

            for (uint i = 0; i < root.FileSize; i++)
            {
                // Next file = base FST offset + (current file index * byte size of an individual entry).
                uint offset = FSTOffset + (i * FSTEntry.EntrySize);
                reader.BaseStream.Position = offset;

                FSTEntry file = new FSTEntry();
                file.NameOffset = reader.ReadUInt32();
                file.Offset     = reader.ReadUInt32();
                file.FileSize   = reader.ReadUInt32();

                // Add to overall files present in FST.
                fileEntries.Add(file);

                // Increment tentative string table offset.
                stringTableOffset += FSTEntry.EntrySize;
            }

            BuildFilenames(reader, 1, fileEntries.Count, "", stringTableOffset);
        }

        // Reads the string table and assigns the correct names to FST entries.
        private int BuildFilenames(EndianBinaryReader reader, int firstIndex, int lastIndex, string directory, uint stringTableOffset)
        {
            int currentIndex = firstIndex;

            while (currentIndex < lastIndex)
            {
                FSTEntry entry = fileEntries[currentIndex];
                uint tableOffset = stringTableOffset + (entry.NameOffset & 0xFFFFFF);
                reader.BaseStream.Position = tableOffset;

                // Null char is a terminator in the string table, so read until we hit it.
                string filename = Encoding.GetEncoding("shift_jis").GetString(reader.ReadBytesUntil(0x00));
                entry.Fullname = directory + filename;

                if (entry.IsDirectory)
                {
                    entry.Fullname += "/";
                    currentIndex = BuildFilenames(reader, currentIndex + 1, (int)entry.FileSize, entry.Fullname, stringTableOffset);
                }
                else
                {
                    ++currentIndex;
                }
            }

            return currentIndex;
        }

        #endregion

        #region Interface Implementations

        public FSTEntry this[int index]
        {
            get { return fileEntries[index]; }
        }

        public IEnumerator<FSTEntry> GetEnumerator()
        {
            return ((IEnumerable<FSTEntry>)fileEntries).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
