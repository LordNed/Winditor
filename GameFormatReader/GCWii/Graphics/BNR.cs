using System;
using System.IO;
using System.Text;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Graphics
{
    /// <summary>
    /// Banner file format
    /// </summary>
    public sealed class BNR
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filepath">Path to the BNR file.</param>
        public BNR(string filepath)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            if (!File.Exists(filepath))
                throw new FileNotFoundException($"File {filepath} does not exist.", filepath);

            ReadBNR(filepath);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Data that contains the BNR file.</param>
        /// <param name="offset">Offset in the data to begin reading at.</param>
        public BNR(byte[] data, int offset)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), $"{nameof(offset)} cannot be less than zero.");

            if (offset >= data.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), $"{nameof(offset)} cannot be greater than the given data's size.");

            ReadBNR(data, offset);
        }

        #endregion

        #region Enums

        /// <summary>
        /// Banner types
        /// </summary>
        public enum BNRType
        {
            /// <summary>
            /// Banner type 1 (US/JP games)
            /// </summary>
            BNR1,

            /// <summary>
            /// Banner type 2 (EU games)
            /// </summary>
            BNR2,
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Banner type
        /// <para>BNR1 for (US/JP)</para>
        /// <para>BNR2 for EU.</para>
        /// </summary>
        public BNRType BannerType 
        {
            get;
            private set;
        }

        /// <summary>
        /// Graphical data stored in the banner.
        /// Pixel format is RGB5A3.
        /// </summary>
        public byte[] Data
        {
            get;
            private set;
        }

        /// <summary>
        /// Basic game title.
        /// </summary>
        public string GameTitle
        {
            get;
            private set;
        }

        /// <summary>
        /// Name of the company/developer of the game.
        /// </summary>
        public string DeveloperName
        {
            get;
            private set;
        }

        /// <summary>
        /// Full title of the game.
        /// </summary>
        public string FullGameTitle
        {
            get;
            private set;
        }

        /// <summary>
        /// Full name of the company/developer or description.
        /// </summary>
        public string FullDeveloperName
        {
            get;
            private set;
        }

        /// <summary>
        /// Descriptions of the game that this banner is for.
        /// </summary>
        /// <remarks>
        /// In BNR1 type banners, this will simply contain one description.
        /// In BNR2 type banners, this will contain six
        /// different language versions of the description.
        /// However, some may simply be the same as the English description,
        /// it depends on the game for the most part.
        /// </remarks>
        public string[] GameDescriptions
        {
            get;
            private set;
        }

        #endregion

        #region Private Methods

        // File-based reading
        private void ReadBNR(string filepath)
        {
            // Same thing as buffer-based reading
            // only difference is we just start at offset zero.
            ReadBNR(File.ReadAllBytes(filepath), 0);
        }

        // Buffer-based reading
        private void ReadBNR(byte[] data, int offset)
        {
            var ms = new MemoryStream(data, offset, data.Length);

            using (var reader = new EndianBinaryReader(ms, Endian.Big))
            {
                string magicWord = new string(reader.ReadChars(4));
                BannerType = (magicWord == "BNR1") ? BNRType.BNR1 : BNRType.BNR2;

                // Skip padding bytes
                reader.BaseStream.Position = 0x20;

                Data = reader.ReadBytes(0x1800);

                // Name related data
                Encoding shiftJis = Encoding.GetEncoding("shift_jis");
                GameTitle = shiftJis.GetString(reader.ReadBytes(0x20));
                DeveloperName = shiftJis.GetString(reader.ReadBytes(0x20));
                FullGameTitle = shiftJis.GetString(reader.ReadBytes(0x40));
                FullDeveloperName = shiftJis.GetString(reader.ReadBytes(0x40));

                if (BannerType == BNRType.BNR1)
                {
                    GameDescriptions = new string[1];
                    GameDescriptions[0] = shiftJis.GetString(reader.ReadBytes(0x80));
                }
                else if (BannerType == BNRType.BNR2)
                {
                    GameDescriptions = new string[6];

                    for (int i = 0; i < 6; i++)
                    {
                        GameDescriptions[i] = shiftJis.GetString(reader.ReadBytes(0x80));
                    }
                }
            }
        }

        #endregion
    }
}
