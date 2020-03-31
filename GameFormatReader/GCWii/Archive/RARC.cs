using System;
using System.IO;
using System.Text;
using GameFormatReader.Common;

// TODO: A dumping method for fully extracting the tree.
//       As it is now, files would have to be pulled manually,
//       which isn't convenient at all.

namespace GameFormatReader.GCWii.Archive
{
    /// <summary>
    /// Represents a RARC archive file.
    /// </summary>
    public sealed class RARC
    {
        #region Private Fields

        // Size of the RARC header in bytes.
        private const int HeaderSize = 64;

        // Size of an individual file entry in bytes.
        private const int FileEntrySize = 20;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filepath">Path to a RARC archive file.</param>
        public RARC(string filepath)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            if (!File.Exists(filepath))
                throw new FileNotFoundException($"Unable to find file: {filepath}", filepath);

            using (var reader = new EndianBinaryReader(File.OpenRead(filepath), Endian.Big))
            {
                ReadHeader(reader);
                ReadNodes(reader);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Byte array containing RARC data.</param>
        public RARC(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (var reader = new EndianBinaryReader(new MemoryStream(data), Endian.Big))
            {
                ReadHeader(reader);
                ReadNodes(reader);
            }
        }

        #endregion

        #region Classes

        /// <summary>
        /// Represents a directory within the RARC archive.
        /// </summary>
        public sealed class Node
        {
            /// <summary>4-character string describing the node's type.</summary>
            public string Type          { get; internal set; }
            /// <summary>Directory's name.</summary>
            public string Name          { get; internal set; }
            /// <summary>Hash of the <see cref="Name"/> field.</summary>
            public ushort NameHashcode  { get; internal set; }
            /// <summary>The offset for the first file in this node.</summary>
            public uint FirstFileOffset { get; internal set; }
            /// <summary>The entries within this Node.</summary>
            public FileEntry[] Entries  { get; internal set; }

            public override string ToString()
            {
                return Name;
            }
        }

        /// <summary>
        /// Represents a file or subdirectory within a RARC archive.
        /// </summary>
        public sealed class FileEntry
        {
            /// <summary>File ID. If 0xFFFF, then this entry is a subdirectory link.</summary>
            public ushort ID            { get; internal set; }
            /// <summary>String hash of the <see cref="Name"/> field.</summary>
            public ushort NameHashcode  { get; internal set; }
            /// <summary>Type of entry. 0x2 = Directory, 0x11 = File.</summary>
            public byte Type            { get; internal set; }
            /// <summary>Padding byte. Included here for the sake of documentation. </summary>
            public byte Padding         { get; internal set; }
            /// <summary>File/subdirectory name.</summary>
            public string Name          { get; internal set; }
            /// <summary>Data bytes. If this entry is a directory, it will be the node index.</summary>
            public byte[] Data          { get; internal set; }
            /// <summary>Always zero.</summary>
            public uint ZeroPadding     { get; internal set; }

            // Non actual struct items

            /// <summary>Whether or not this entry is a directory.</summary>
            public bool IsDirectory  { get; internal set; }
            /// <summary>Node index representing the subdirectory. Will only be non-zero if IsDirectory is true.</summary>
            public uint SubDirIndex  { get; internal set; }

            public override string ToString()
            {
                return Name;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Size of this file.
        /// </summary>
        public uint FileSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Offset within this archive where the data begins.
        /// </summary>
        /// <remarks>
        /// The 0x20 value is already added internally.
        /// </remarks>
        public uint DataOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Nodes within this archive
        /// </summary>
        public Node[] Nodes
        {
            get;
            private set;
        }

        /// <summary>
        /// Offset to the file entries.
        /// </summary>
        /// <remarks>
        /// The 0x20 value is already internally added to it.
        /// </remarks>
        public uint FileEntryOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Offset to the string table.
        /// </summary>
        /// <remarks>
        /// The 0x20 value is already internally added to it.
        /// </remarks>
        public uint StringTableOffset
        {
            get;
            private set;
        }

        #endregion

        #region Private Methods

        private void ReadHeader(EndianBinaryReader reader)
        {
            if (new string(reader.ReadChars(4)) != "RARC")
                throw new ArgumentException("The given data and offset does not point to a valid RARC archive.");

            FileSize = reader.ReadUInt32();

            // Skip unknown value
            reader.SkipUInt32();

            DataOffset = reader.ReadUInt32() + 0x20;

            // Skip unknown values.
            reader.Skip(16); // 4 unsigned ints.

            // Total number of nodes
            Nodes = new Node[reader.ReadUInt32()];

            // Skip unknown values.
            reader.Skip(8); // 2 unsigned ints

            FileEntryOffset = reader.ReadUInt32() + 0x20;

            // Skip unknown value
            reader.SkipUInt32();

            StringTableOffset = reader.ReadUInt32() + 0x20;

            // Skip unknown values
            reader.Skip(8); // 2 unsigned ints
        }

        // Reads the node tree. This includes the file entries.
        private void ReadNodes(EndianBinaryReader reader)
        {
            // Nodes begin right after the header.
            reader.BaseStream.Position = HeaderSize;

            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i]                 = new Node();
                Nodes[i].Type            = new string(reader.ReadChars(4));
                Nodes[i].Name            = ReadString(reader, reader.ReadUInt32());
                Nodes[i].NameHashcode    = reader.ReadUInt16();
                Nodes[i].Entries         = new FileEntry[reader.ReadUInt16()];
                Nodes[i].FirstFileOffset = reader.ReadUInt32();
            }

            // Now read the node entries.
            foreach (Node node in Nodes)
            {
                for (int i = 0; i < node.Entries.Length; i++)
                {
                    // Find the entry position
                    reader.BaseStream.Position = FileEntryOffset + ((node.FirstFileOffset + i) * FileEntrySize);

                    node.Entries[i]              = new FileEntry();
                    node.Entries[i].ID           = reader.ReadUInt16();
                    node.Entries[i].NameHashcode = reader.ReadUInt16();
                    node.Entries[i].Type         = reader.ReadByte();
                    node.Entries[i].Padding      = reader.ReadByte();
                    node.Entries[i].Name         = ReadString(reader, reader.ReadUInt16());
                    node.Entries[i].IsDirectory  = (node.Entries[i].ID == 0xFFFF);

                    uint entryDataOffset = reader.ReadUInt32();
                    uint dataSize        = reader.ReadUInt32();

                    // If it's a Directory, then entryDataOffset contains
                    // the index of the parent node.
                    if (node.Entries[i].IsDirectory)
                    {
                        node.Entries[i].SubDirIndex = entryDataOffset;
                    }
                    else // It's a file, get the data.
                    {
                        node.Entries[i].Data = reader.ReadBytesAt(DataOffset + entryDataOffset, (int) dataSize);
                    }

                    node.Entries[i].ZeroPadding = reader.ReadUInt32();
                }
            }
        }

        // Reads a string from the string table.
        private string ReadString(EndianBinaryReader reader, uint offset)
        {
            // Save position, and seek to string table
            long curPos = reader.BaseStream.Position;
            uint stringOffset = offset+StringTableOffset;
            reader.BaseStream.Position = stringOffset;

            byte[] bytes = reader.ReadBytesUntil(0x00);
            string result = Encoding.GetEncoding("shift_jis").GetString(bytes);

            // Seek back to previous position.
            reader.BaseStream.Position = curPos;
            return result;
        }

        #endregion
    }
}
