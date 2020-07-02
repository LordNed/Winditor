using System;
using System.IO;

namespace GameFormatReader.GCWii.Discs
{
    /// <summary>
    /// Represents a GameCube or Wii disc.
    /// </summary>
    public abstract class Disc
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filepath">Path to a disc.</param>
        protected Disc(string filepath)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            if (!File.Exists(filepath))
                throw new FileNotFoundException($"File {filepath} does not exist.", filepath);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="DiscHeader"/> for this disc.
        /// </summary>
        public abstract DiscHeader Header
        {
            get;
            protected set;
        }

        #endregion
    }
}
