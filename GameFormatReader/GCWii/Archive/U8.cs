using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Archive
{
    /// <summary>
    /// U8 archive structure handling.
    /// </summary>
    public static class U8
    {
        #region Constants

        private const int HeaderSize = 0x20;
        private const int NodeSize   = 0x0C;

        #endregion

        #region Header Class

        // Represents the relevant sections of the header
        private sealed class Header
        {
            /// <summary>Size of the header from the root node until the end of the string table</summary>
            public uint TotalDataSize { get; set; }

            /// <summary>Offset to the data. This is root node offset + HeaderSize, aligned to 0x40.</summary>
            public uint DataOffset { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="reader"><see cref="EndianBinaryReader"/> used to get relevant data.</param>
            public Header(EndianBinaryReader reader)
            {
                // Skip header magic and root node offset (offset is always 0x20).
                reader.BaseStream.Position += 8;

                TotalDataSize = reader.ReadUInt32();
                DataOffset = reader.ReadUInt32();

                // Skip zeroed bytes
                reader.BaseStream.Position += 16;
            }
        }

        #endregion

        #region Node Class

        private sealed class Node
        {
            /// <summary>
            /// Node type
            /// </summary>
            public byte Type { get; private set; }

            /// <summary>
            /// Offset into the string table where the file's name is.
            /// </summary>
            public ushort NameOffset { get; private set; }

            /// <summary>
            /// Binary data for this node.
            /// </summary>
            public byte[] Data { get; private set; }

            /// <summary>
            /// Size of the file this node represents.
            /// <para></para>
            /// For Directories, this is the last file number.
            /// </summary>
            /// <remarks>
            /// This is a 24-bit number encoded within a 32-bit space.
            /// </remarks>
            public int Size { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="reader"><see cref="EndianBinaryReader"/> used to get relevant data.</param>
            public Node(EndianBinaryReader reader)
            {
                // Type is written as an unsigned short, but it really just a byte.
                Type = reader.ReadByte();
                reader.SkipByte();

                NameOffset = reader.ReadUInt16();

                var dataOffset = reader.ReadUInt32();
                Size = reader.ReadInt32() & 0x00FFFFFF;

                Data = reader.ReadBytesAt(dataOffset, Size);
            }
        }

        #endregion

        #region Public Extraction Methods

        /// <summary>
        /// Extracts the U8 file structure from the file
        /// indicated by the given file path.
        /// </summary>
        /// <param name="filepath">Path to the file to extract the U8 file structure from.</param>
        /// <param name="outputDir">Root output directory to place the extracted file hierarchy.</param>
        /// <returns>True if extraction was successful, otherwise false.</returns>
        public static bool Extract(string filepath, string outputDir)
        {
            if (string.IsNullOrEmpty(filepath) || string.IsNullOrEmpty(outputDir))
                return false;

            if (!IsValidU8(filepath))
                return false;

            return ExtractionProcess(File.ReadAllBytes(filepath), outputDir);
        }

        /// <summary>
        /// Extracts the U8 file structure from a byte array in memory.
        /// </summary>
        /// <param name="data">Byte array containing the U8 file structure.</param>
        /// <param name="outputDir">Root output directory to place the extracted file hierarchy.</param>
        /// <returns>True if extraction was successful, otherwise false.</returns>
        public static bool Extract(byte[] data, string outputDir)
        {
            if (string.IsNullOrEmpty(outputDir))
                return false;

            if (data == null)
                return false;

            if (!IsValidU8(data))
                return false;

            return ExtractionProcess(data, outputDir);
        }

        #endregion

        #region Public Utility Functions

        /// <summary>
        /// Utility function for checking if a given file is U8 compressed.
        /// </summary>
        /// <param name="filepath">Path to the file to check.</param>
        /// <returns>True if the file is a valid U8 archive, otherwise false.</returns>
        public static bool IsValidU8(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
                return false;

            if (!File.Exists(filepath))
                return false;

            using (var reader = new BinaryReader(File.OpenRead(filepath)))
            {
                // Check for a malformed header
                if (reader.BaseStream.Length < HeaderSize)
                    return false;

                byte[] data = reader.ReadBytes(4);

                return data[0] == 0x55 &&
                       data[1] == 0xAA &&
                       data[2] == 0x38 &&
                       data[3] == 0x2D;
            }
        }

        /// <summary>
        /// Utility function for checking if given data is using a U8 file structure.
        /// </summary>
        /// <param name="data">The data to check.</param>
        /// <returns>True if <paramref name="data"/> is a valid U8 archive, otherwise false.</returns>
        public static bool IsValidU8(byte[] data)
        {
            if (data == null)
                return false;

            // Check for a malformed header
            if (data.Length < HeaderSize)
                return false;

            return data[0] == 0x55 &&
                   data[1] == 0xAA &&
                   data[2] == 0x38 &&
                   data[3] == 0x2D;
        }

        #endregion

        #region Private Helper Methods

        private static bool ExtractionProcess(byte[] data, string outputDir)
        {
            using (var reader = new EndianBinaryReader(new MemoryStream(data), Endian.Big))
            {
                var header = new Header(reader);
                var rootNode = new Node(reader);

                // Root node specifies the total amount of nodes
                var nodes = new Node[rootNode.Size - 1];
                for (int i = 0; i < nodes.Length; i++)
                    nodes[i] = new Node(reader);

                byte[] stringTable = reader.ReadBytes((int) (header.DataOffset - HeaderSize - rootNode.Size * NodeSize));

                return WriteNodes(nodes, stringTable, outputDir);
            }
        }

        private static bool WriteNodes(Node[] nodes, byte[] stringTable, string outputDir)
        {
            // Create the root output directory
            if (!AttemptToCreateDirectory(outputDir))
                return false;

            var currentDirectory = outputDir;

            // Capacity of 1 to accommodate the root directory.
            var directoryIndex = 0;
            var directorySizes = new List<int>(new int[1]);

            for (int i = 0; i < nodes.Length; i++)
            {
                Node node = nodes[i];

                string name = GetStringFromStringTable(node.NameOffset, stringTable);

                // File node
                if (node.Type == 0x00)
                {
                    using (var fileStream = File.Create(Path.Combine(currentDirectory, name)))
                    {
                        fileStream.Write(node.Data, 0, node.Data.Length);
                    }
                }
                // Directory node
                else if (node.Type == 0x01)
                {
                    directorySizes.Add(node.Size);
                    directoryIndex++;
                    currentDirectory = Path.Combine(currentDirectory, name);

                    if (!AttemptToCreateDirectory(currentDirectory))
                        return false;
                }

                while (directorySizes[directoryIndex] == i+2 && directoryIndex > 0)
                {
                    currentDirectory = Directory.GetParent(currentDirectory).FullName;

                    // Pop the back of the list.
                    directorySizes.RemoveAt(directorySizes.Count - 1);
                    directoryIndex--;
                }
            }

            return true;
        }

        private static string GetStringFromStringTable(int nameOffset, byte[] stringTable)
        {
            // String tables within U8 archives use null-terminated strings.
            int nullCharIndex = Array.FindIndex(stringTable, nameOffset, x => x == '\0');
            byte[] stringBytes = new byte[nullCharIndex - nameOffset];

            Array.Copy(stringTable, nameOffset, stringBytes, 0, stringBytes.Length);

            return Encoding.UTF8.GetString(stringBytes);
        }

        private static bool AttemptToCreateDirectory(string directory)
        {
            try
            {
                Directory.CreateDirectory(directory);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion
    }
}
