using System.IO;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs.GC
{
    /// <summary>
    /// Represents the Apploader on GameCube <see cref="Disc"/>s.
    /// </summary>
    public sealed class Apploader
    {
        #region Private Fields

        private const int ApploaderOffset = 0x2440;
        private const int HeaderSize = 0x20;

        #endregion

        #region Constructor

        internal Apploader(EndianBinaryReader reader)
        {
            reader.BaseStream.Position = ApploaderOffset;

            Version = new string(reader.ReadChars(10));

            // Skip padding
            reader.BaseStream.Position += 6;

            EntryPoint = reader.ReadInt32();
            Size = reader.ReadInt32();
            TrailerSize = reader.ReadInt32();

            // Seek back and pull all the data.
            reader.BaseStream.Position = ApploaderOffset;
            int apploaderSize = HeaderSize + Size + TrailerSize;
            Data = reader.ReadBytes(apploaderSize);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Apploader Version
        /// </summary>
        public string Version
        {
            get;
            private set;
        }

        /// <summary>
        /// Apploader entry point
        /// </summary>
        public int EntryPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Apploader size
        /// </summary>
        public int Size
        {
            get;
            private set;
        }

        /// <summary>
        /// Trailer size
        /// </summary>
        public int TrailerSize
        {
            get;
            private set;
        }

        /// <summary>
        /// The entire Apploader data chunk.
        /// </summary>
        public byte[] Data
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves the embedded Apploader to a given output path.
        /// </summary>
        /// <param name="output">The path to save the apploader to.</param>
        /// <returns>true if the file was saved successfully, false otherwise.</returns>
        public bool Save(string output)
        {
            try
            {
                using (FileStream fs = new FileStream(output, FileMode.Open, FileAccess.Write))
                {
                    fs.Write(Data, 0, Data.Length);
                }
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
