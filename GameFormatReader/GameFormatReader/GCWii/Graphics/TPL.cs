using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameFormatReader.Common;
using GameFormatReader.GCWii.Graphics.Enums;

namespace GameFormatReader.GCWii.Graphics
{
    /// <summary>
    /// Represents a custom format that stores
    /// texture and pallet information.
    /// </summary>
    public sealed class TPL : IEnumerable<TPL.Texture>
    {
        #region Private Fields

        private const int CorrectHeaderSize = 0x0C;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filepath">Path to the TPL file.</param>
        public TPL(string filepath)
        {
            if (filepath == null)
                throw new ArgumentNullException(nameof(filepath));

            if (!File.Exists(filepath))
                throw new FileNotFoundException($"File {filepath} does not exist.", filepath);

            using (var reader = new EndianBinaryReader(File.OpenRead(filepath), Endian.Big))
            {
                ReadHeader(reader);
                ReadTextures(reader);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Byte array containing TPL data.</param>
        public TPL(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (var reader = new EndianBinaryReader(new MemoryStream(data), Endian.Big))
            {
                ReadHeader(reader);
                ReadTextures(reader);
            }
        }

        #endregion

        #region Structs

        /// <summary>
        /// Represents an embedded texture
        /// </summary>
        public sealed class Texture
        {
            /// <summary>Height of this texture in pixels.</summary>
            public int Height { get; internal set; }
            /// <summary>Width of this texture in pixels.</summary>
            public int Width  { get; internal set; }
            /// <summary>Texture format</summary>
            public TextureFormat TexFormat { get; internal set; }
            /// <summary>Texture data</summary>
            public byte[] TextureData { get; internal set; }
            /// <summary>Horizontal wrapping mode</summary>
            public WrapMode WrapS { get; internal set; }
            /// <summary>Vertical wrapping mode</summary>
            public WrapMode WrapT { get; internal set; }
            /// <summary>Minimization filter</summary>
            public TextureFilter MinFilter { get; internal set; }
            /// <summary>Magnification filter</summary>
            public TextureFilter MagFilter { get; internal set; }
            /// <summary>LOD (Level of Detail) bias.</summary>
            public float LODBias { get; internal set; }
            /// <summary>Edge level of detail</summary>
            public byte EdgeLOD { get; internal set; }
            /// <summary>Minimum level of detail</summary>
            public byte MinLOD { get; internal set; }
            /// <summary>Maximum level of detail</summary>
            public byte MaxLOD { get; internal set; }
            /// <summary>Whether or not data is unpacked</summary>
            public byte IsUnpacked { get; internal set; }
            /// <summary>This texture's palette (optional).</summary>
            public Palette Palette { get; internal set; }
        }

        /// <summary>
        /// Represents an embedded palette
        /// </summary>
        public sealed class Palette
        {
            /// <summary>Number of palettes</summary>
            public short NumItems { get; internal set; }
            /// <summary>Whether or not data is unpacked.</summary>
            public byte IsUnpacked { get; internal set; }
            /// <summary>Indicates padding? Truthfully, I don't know</summary>
            public byte Padding { get; internal set; }
            /// <summary>Palette format</summary>
            public TLUTFormat PaletteFormat { get; internal set; }
            /// <summary>Palette data</summary>
            public byte[] Data { get; internal set; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Textures within this file.
        /// </summary>
        public Texture[] Textures
        {
            get;
            internal set;
        }

        #endregion

        #region Private Methods

        private void ReadHeader(EndianBinaryReader reader)
        {
            byte[] magic = reader.ReadBytes(4);
            if (magic[0] != 0x00 || magic[1] != 0x20 ||
                magic[2] != 0xAF || magic[3] != 0x30)
            {
                throw new IOException("Not a valid TPL file - Incorrect file magic");
            }

            int numTextures = reader.ReadInt32();
            Textures = new Texture[numTextures];

            // Note that the header size must be 0x0C to be valid.
            int headerSize = reader.ReadInt32();

            if (headerSize != CorrectHeaderSize)
                throw new IOException($"Incorrect TPL header size. Should be {CorrectHeaderSize}, was {headerSize}");
        }

        private void ReadTextures(EndianBinaryReader reader)
        {
            for (int i = 0; i < Textures.Length; i++)
            {
                int textureOffset = reader.ReadInt32();
                int paletteOffset = reader.ReadInt32();

                Textures[i]           = new Texture();
                Textures[i].Height    = reader.ReadInt16At(textureOffset + 0x00);
                Textures[i].Width     = reader.ReadInt16At(textureOffset + 0x02);
                Textures[i].TexFormat = (TextureFormat) reader.ReadInt32At(textureOffset + 0x04);

                int texDataOffset = reader.ReadInt32At(textureOffset + 0x08);
                Textures[i].TextureData = reader.ReadBytesAt(texDataOffset, GetTextureDataSize(i));

                Textures[i].WrapS = (WrapMode) reader.ReadInt32At(textureOffset + 0x0C);
                Textures[i].WrapT = (WrapMode) reader.ReadInt32At(textureOffset + 0x10);

                Textures[i].MinFilter  = (TextureFilter) reader.ReadInt32At(textureOffset + 0x14);
                Textures[i].MagFilter  = (TextureFilter) reader.ReadInt32At(textureOffset + 0x18);
                Textures[i].LODBias    = reader.ReadSingleAt(textureOffset + 0x1C);
                Textures[i].EdgeLOD    = reader.ReadByteAt(textureOffset   + 0x20);
                Textures[i].MinLOD     = reader.ReadByteAt(textureOffset   + 0x21);
                Textures[i].MaxLOD     = reader.ReadByteAt(textureOffset   + 0x22);
                Textures[i].IsUnpacked = reader.ReadByteAt(textureOffset   + 0x23);

                if (paletteOffset != 0)
                {
                    Textures[i].Palette = new Palette();
                    Textures[i].Palette.NumItems      = reader.ReadInt16At(paletteOffset + 0x00);
                    Textures[i].Palette.IsUnpacked    = reader.ReadByteAt(paletteOffset  + 0x02);
                    Textures[i].Palette.Padding       = reader.ReadByteAt(paletteOffset  + 0x03);
                    Textures[i].Palette.PaletteFormat = (TLUTFormat) reader.ReadInt32At(paletteOffset + 0x04);

                    int dataOffset = reader.ReadInt32At(paletteOffset + 0x08);
                    Textures[i].Palette.Data = reader.ReadBytesAt(dataOffset, Textures[i].Palette.NumItems);
                }
                else
                {
                    Textures[i].Palette = null;
                }
            }
        }

        private int GetTextureDataSize(int i)
        {
            if (i < 0)
                throw new ArgumentException($"{nameof(i)} cannot be less than zero", nameof(i));

            if (i >= Textures.Length)
                throw new ArgumentException($"{nameof(i)} cannot be larger than the total number of textures", nameof(i));

            int size = 0;

            int width = Textures[i].Width;
            int height = Textures[i].Height;

            switch (Textures[i].TexFormat)
            {
                case TextureFormat.I4:
                case TextureFormat.CI4:
                case TextureFormat.CMP:
                    size = ((width + 7) >> 3)*((height + 7) >> 3)*32;
                    break;

                case TextureFormat.I8:
                case TextureFormat.IA4:
                case TextureFormat.CI8:
                    size = ((width + 7) >> 3)*((height + 7) >> 2)*32;
                    break;

                case TextureFormat.IA8:
                case TextureFormat.CI14X2:
                case TextureFormat.RGB565:
                case TextureFormat.RGB5A3:
                    size = ((width + 3) >> 2)*((height + 3) >> 2)*32;
                    break;

                case TextureFormat.RGBA8:
                    size = ((width + 3) >> 2)*((height + 3) >> 2)*64;
                    break;
            }

            return size;
        }

        #endregion

        #region Interface Methods and Properties

        /// <summary>
        /// Retrieves the <see cref="TPL.Texture"/> specified by the given index.
        /// </summary>
        /// <param name="index">The index to get the <see cref="TPL.Texture"/> at.</param>
        /// <returns>The <see cref="TPL.Texture"/> at the given index.</returns>
        public Texture this[int index]
        {
            get { return Textures[index]; }
        }

        public IEnumerator<Texture> GetEnumerator()
        {
            return ((IEnumerable<Texture>)Textures).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
