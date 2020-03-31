using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs
{
    /// <summary>
    /// Represents a disc header of a GameCube and Wii disc.
    /// </summary>
    /// <remarks>
    /// The basic header is always 1024 bytes.
    /// However, only the first 400 bytes provide relevant info.
    /// </remarks>
    public abstract class DiscHeader
    {
        #region Properties

        /// <summary>
        /// The <see cref="DiscType"/> of this <see cref="Disc"/>.
        /// </summary>
        public DiscType Type
        {
            get;
            protected set;
        }

        /// <summary>
        /// Whether or not this disc is a Gamecube disc.
        /// </summary>
        public bool IsGamecubeDisc
        {
            get;
            protected set;
        }

        /// <summary>
        /// The embedded game code on this <see cref="Disc"/>.
        /// </summary>
        public string GameCode
        {
            get;
            protected set;
        }

        /// <summary>
        /// The <see cref="Region"/> code for this <see cref="Disc"/>.
        /// </summary>
        public Region RegionCode
        {
            get;
            protected set;
        }

        /// <summary>
        /// The MakerCode for this <see cref="Disc"/>.
        /// </summary>
        public string MakerCode
        {
            get;
            protected set;
        }

        /// <summary>
        /// Disc number for this <see cref="Disc"/>.
        /// </summary>
        /// <remarks>
        /// This is usually used only by multi-disc games.
        /// </remarks>
        public int DiscNumber
        {
            get;
            protected set;
        }

        /// <summary>
        /// Whether or not this <see cref="Disc"/> utilizes audio streaming.
        /// </summary>
        public bool AudioStreaming
        {
            get;
            protected set;
        }

        /// <summary>
        /// Size of the audio streaming buffer.
        /// </summary>
        public int StreamingBufferSize
        {
            get;
            protected set;
        }

        /// <summary>
        /// Magic word that identifies whether or not this
        /// disc is a Wii or GameCube disc.
        /// </summary>
        public int MagicWord
        {
            get;
            protected set;
        }

        /// <summary>
        /// Title of the game on this <see cref="Disc"/>.
        /// </summary>
        public string GameTitle
        {
            get;
            protected set;
        }

        #endregion

        #region Protected Methods

        protected static DiscType DetermineDiscType(char id)
        {
            switch (id)
            {
                case 'R':
                    return DiscType.Revolution;

                case 'S':
                    return DiscType.Wii;

                case 'G':
                    return DiscType.Gamecube;

                case 'U':
                    return DiscType.Utility;

                case 'D':
                    return DiscType.GamecubeDemo;

                case 'P':
                    return DiscType.GamecubePromotional;

                case '0':
                    return DiscType.Diagnostic;

                case '1':
                    return DiscType.Diagnostic2;

                case '4':
                    return DiscType.WiiBackup;

                case '_':
                    return DiscType.WiiFitChanInstaller;

                default:
                    return DiscType.Unknown;
            }
        }

        protected static Region DetermineRegion(char id)
        {
            switch (id)
            {
                case 'U':
                    return Region.Australia;

                case 'F':
                    return Region.France;

                case 'D':
                    return Region.Germany;

                case 'I':
                    return Region.Italy;

                case 'J':
                    return Region.Japan;

                case 'K':
                    return Region.Korea;

                case 'P':
                    return Region.PAL;

                case 'R':
                    return Region.Russia;

                case 'S':
                    return Region.Spanish;

                case 'T':
                    return Region.Taiwan;

                case 'E':
                    return Region.USA;

                default:
                    return Region.Unknown;
            }
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Reads the console specific disc header.
        /// </summary>
        /// <param name="reader">The <see cref="EndianBinaryReader"/> used to read the disc.</param>
        protected abstract void ReadHeader(EndianBinaryReader reader);

        #endregion
    }
}
