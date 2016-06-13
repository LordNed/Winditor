using GameFormatReader.Common;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using WindEditor;

namespace JStudio
{
    /// <summary>
    /// The BinaryTextureImage (or BTI) format is used by Wind Waker (and several other Nintendo
    /// games) to store texture images. There are a variety of encoding methods, some of which
    /// are supported right now for decoding. This does not currently support encoding BTI files
    /// but will at some point in time. It does not load mipmaps from the file currently.
    /// 
    /// Image data can be retrieved by calling GetData() which will return an ARGB array of bytes
    /// containing the information. For files without alpha data their values will be set to 0xFF.
    /// 
    /// BTI files are stored both individually on disk and embedded within other file formats. 
    /// </summary>
    public class BinaryTextureImage
    {
        #region Data Types
        /// <summary>
        /// ImageFormat specifies how the data within the image is encoded.
        /// Included is a chart of how many bits per pixel there are, 
        /// the width/height of each block, how many bytes long the
        /// actual block is, and a description of the type of data stored.
        /// </summary>
        public enum TextureFormats
        {
            //Bits per Pixel | Block Width | Block Height | Block Size | Type / Description
            I4 = 0x00,      //  4 | 8 | 8 | 32 | grey
            I8 = 0x01,      //  8 | 8 | 8 | 32 | grey
            IA4 = 0x02,     //  8 | 8 | 4 | 32 | grey + alpha
            IA8 = 0x03,     // 16 | 4 | 4 | 32 | grey + alpha
            RGB565 = 0x04,  // 16 | 4 | 4 | 32 | color
            RGB5A3 = 0x05,  // 16 | 4 | 4 | 32 | color + alpha
            RGBA32 = 0x06,  // 32 | 4 | 4 | 64 | color + alpha
            C4 = 0x08,      //  4 | 8 | 8 | 32 | palette choices (IA8, RGB565, RGB5A3)
            C8 = 0x09,      // 8, 8, 4, 32 | palette choices (IA8, RGB565, RGB5A3)
            C14X2 = 0x0a,   // 16 (14 used) | 4 | 4 | 32 | palette (IA8, RGB565, RGB5A3)
            CMPR = 0x0e,    //  4 | 8 | 8 | 32 | mini palettes in each block, RGB565 or transparent.
        }

        /// <summary>
        /// Defines how textures handle going out of [0..1] range for texcoords.
        /// </summary>
        public enum WrapModes
        {
            ClampToEdge = 0,
            Repeat = 1,
            MirroredRepeat = 2,
        }

        /// <summary>
        /// PaletteFormat specifies how the data within the palette is stored. An
        /// image uses a single palette (except CMPR which defines its own
        /// mini-palettes within the Image data). Only C4, C8, and C14X2 use
        /// palettes. For all other formats the type and count is zero.
        /// </summary>
        public enum PaletteFormats
        {
            IA8 = 0x00,
            RGB565 = 0x01,
            RGB5A3 = 0x02,
        }

        /// <summary>
        /// FilterMode specifies what type of filtering the file should use for min/mag.
        /// </summary>
        public enum FilterMode
        {
            /* Valid in both Min and Mag Filter */
            Nearest = 0x0,                  // Point Sampling, No Mipmap
            Linear = 0x1,                   // Bilinear Filtering, No Mipmap

            /* Valid in only Min Filter */
            NearestMipmapNearest = 0x2,     // Point Sampling, Discrete Mipmap
            NearestMipmapLinear = 0x3,      // Bilinear Filtering, Discrete Mipmap
            LinearMipmapNearest = 0x4,      // Point Sampling, Linear MipMap
            LinearMipmapLinear = 0x5,       // Trilinear Filtering
        }

        /// <summary>
        /// The Palette simply stores the color data as loaded from the file.
        /// It does not convert the files based on the Palette type to RGBA8.
        /// </summary>
        private sealed class Palette
        {
            private byte[] _paletteData;

            public void Load(EndianBinaryReader reader, uint paletteEntryCount)
            {
                //Files that don't have palettes have an entry count of zero.
                if (paletteEntryCount == 0)
                {
                    _paletteData = new byte[0];
                    return;
                }

                //All palette formats are 2 bytes per entry.
                _paletteData = reader.ReadBytes((int)paletteEntryCount * 2);
            }

            public byte[] GetBytes()
            {
                return _paletteData;
            }
        }
        #endregion

        public TextureFormats Format { get; private set; }
        public byte AlphaSetting { get; private set; } // 0 for no alpha, 0x02 and other values seem to indicate yes alpha.
        public ushort Width { get; private set; }
        public ushort Height { get; private set; }
        public WrapModes WrapS { get; private set; }
        public WrapModes WrapT { get; private set; }
        public PaletteFormats PaletteFormat { get; private set; }
        public ushort PaletteCount { get; private set; }
        public WLinearColor BorderColor { get; private set; } // This is a guess. It seems to be 0 in most things, but it fits with min/mag filters.
        public FilterMode MinFilter { get; private set; }
        public FilterMode MagFilter { get; private set; }
        public byte MinLOD { get; private set; } // Fixed point number, 1/8 = conversion (ToDo: is this multiply by 8 or divide...)
        public byte MagLOD { get; private set; } // Fixed point number, 1/8 = conversion (ToDo: is this multiply by 8 or divide...)
        public byte MipMapCount { get; private set; }
        public ushort LodBias { get; private set; } // Fixed point number, 1/100 = conversion

        private Palette m_imagePalette;
        private byte[] m_rgbaImageData;

        // headerStart seems to be chunkStart + 0x20 and I don't know why.
        /// <summary>
        /// Load a BinaryTextureImage from a stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="headerStart"></param>
        /// <param name="imageIndex">Optional additional offset used by J3D models. Multiplied by 0x20.</param>
        public void Load(EndianBinaryReader stream, long headerStart, int imageIndex = 0)
        {
            Format = (TextureFormats)stream.ReadByte();
            AlphaSetting = stream.ReadByte();
            Width = stream.ReadUInt16();
            Height = stream.ReadUInt16();
            WrapS = (WrapModes)stream.ReadByte();
            WrapT = (WrapModes)stream.ReadByte();
            byte unknown1 = stream.ReadByte();
            PaletteFormat = (PaletteFormats)stream.ReadByte();
            PaletteCount = stream.ReadUInt16();
            int paletteDataOffset = stream.ReadInt32();
            BorderColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte() / 255f);
            MinFilter = (FilterMode)stream.ReadByte();
            MagFilter = (FilterMode)stream.ReadByte();
            short unknown2 = stream.ReadInt16();
            MipMapCount = stream.ReadByte();
            byte unknown3 = stream.ReadByte();
            LodBias = stream.ReadUInt16();

            int imageDataOffset = stream.ReadInt32();

            // Load the Palette data 
            stream.BaseStream.Position = headerStart + paletteDataOffset + (0x20 * imageIndex);
            m_imagePalette = new Palette();
            m_imagePalette.Load(stream, PaletteCount);

            // Now load and decode image data into an ARGB array.
            stream.BaseStream.Position = headerStart + imageDataOffset + (0x20 * imageIndex);
            m_rgbaImageData = DecodeData(stream, Width, Height, Format, m_imagePalette, PaletteFormat);
        }

        public void SaveImageToDisk(string outputFile)
        {
            using (Bitmap bmp = new Bitmap(Width, Height))
            {
                Rectangle rect = new Rectangle(0, 0, Width, Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                //Lock the bitmap for writing, copy the bits and then unlock for saving.
                IntPtr ptr = bmpData.Scan0;
                byte[] imageData = m_rgbaImageData;
                Marshal.Copy(imageData, 0, ptr, imageData.Length);
                bmp.UnlockBits(bmpData);

                // Bitmaps will throw an exception if the output folder doesn't exist so...
                Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
                bmp.Save(outputFile);
            }
        }

        public byte[] GetData()
        {
            return m_rgbaImageData;
        }

        /// <summary>
        /// This function is for debugging purposes and does not encompass encoding data.
        /// </summary>
        public void LoadImageFromDisk(string filePath)
        {

            using (Bitmap bitmap = new Bitmap(filePath))
            {
                Format = TextureFormats.RGBA32;
                AlphaSetting = 0;
                Width = (ushort)bitmap.Width;
                Height = (ushort)bitmap.Height;
                WrapS = WrapModes.ClampToEdge;
                WrapT = WrapModes.ClampToEdge;
                PaletteFormat = PaletteFormats.IA8;
                PaletteCount = 0;
                BorderColor = WLinearColor.Red;
                MinFilter = FilterMode.Linear;
                MagFilter = FilterMode.Linear;
                MipMapCount = 0;
                LodBias = 0;

                byte[] data = new byte[Width * Height * 4];

                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                Marshal.Copy(bmpData.Scan0, data, 0, data.Length);
                bitmap.UnlockBits(bmpData);
                m_rgbaImageData = data;
            }
        }

        #region Decoding
        private static byte[] DecodeData(EndianBinaryReader stream, uint width, uint height, TextureFormats format, Palette imagePalette, PaletteFormats paletteFormat)
        {
            switch (format)
            {
                case TextureFormats.I4:
                    return DecodeI4(stream, width, height);
                case TextureFormats.I8:
                    return DecodeI8(stream, width, height);
                case TextureFormats.IA4:
                    return DecodeIA4(stream, width, height);
                case TextureFormats.IA8:
                    return DecodeIA8(stream, width, height);
                case TextureFormats.RGB565:
                    return DecodeRgb565(stream, width, height);
                case TextureFormats.RGB5A3:
                    return DecodeRgb5A3(stream, width, height);
                case TextureFormats.RGBA32:
                    return DecodeRgba32(stream, width, height);
                case TextureFormats.C4:
                    return DecodeC4(stream, width, height, imagePalette, paletteFormat);
                case TextureFormats.C8:
                    return DecodeC8(stream, width, height, imagePalette, paletteFormat);
                case TextureFormats.CMPR:
                    return DecodeCmpr(stream, width, height);
                case TextureFormats.C14X2:
                default:
                    Console.WriteLine("Unsupported Binary Texture Image format {0}, unable to decode!", format);
                    return new byte[0];
            }
        }

        private static byte[] DecodeRgba32(EndianBinaryReader stream, uint width, uint height)
        {
            uint numBlocksW = width / 4; //4 byte block width
            uint numBlocksH = height / 4; //4 byte block height 

            byte[] decodedData = new byte[width * height * 4];

            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //For each block, we're going to examine block width / block height number of 'pixels'
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 4; pX++)
                        {
                            //Ensure the pixel we're checking is within bounds of the image.
                            if ((xBlock * 4 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;

                            //Now we're looping through each pixel in a block, but a pixel is four bytes long. 
                            uint destIndex = (uint)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 4) + pX));
                            decodedData[destIndex + 3] = stream.ReadByte(); //Alpha
                            decodedData[destIndex + 2] = stream.ReadByte(); //Red
                        }
                    }

                    //...but we have to do it twice, because RGBA32 stores two sub-blocks per block. (AR, and GB)
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 4; pX++)
                        {
                            //Ensure the pixel we're checking is within bounds of the image.
                            if ((xBlock * 4 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;

                            //Now we're looping through each pixel in a block, but a pixel is four bytes long. 
                            uint destIndex = (uint)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 4) + pX));
                            decodedData[destIndex + 1] = stream.ReadByte(); //Green
                            decodedData[destIndex + 0] = stream.ReadByte(); //Blue
                        }
                    }

                }
            }

            return decodedData;
        }

        private static byte[] DecodeC4(EndianBinaryReader stream, uint width, uint height, Palette imagePalette, PaletteFormats paletteFormat)
        {
            //4 bpp, 8 block width/height, block size 32 bytes, possible palettes (IA8, RGB565, RGB5A3)
            uint numBlocksW = width / 8;
            uint numBlocksH = height / 8;

            byte[] decodedData = new byte[width * height * 8];

            //Read the indexes from the file
            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //Inner Loop for pixels
                    for (int pY = 0; pY < 8; pY++)
                    {
                        for (int pX = 0; pX < 8; pX += 2)
                        {
                            //Ensure we're not reading past the end of the image.
                            if ((xBlock * 8 + pX >= width) || (yBlock * 8 + pY >= height))
                                continue;

                            byte data = stream.ReadByte();
                            byte t = (byte)(data & 0xF0);
                            byte t2 = (byte)(data & 0x0F);

                            decodedData[width * ((yBlock * 8) + pY) + (xBlock * 8) + pX + 0] = (byte)(t >> 4);
                            decodedData[width * ((yBlock * 8) + pY) + (xBlock * 8) + pX + 1] = t2;
                        }
                    }
                }
            }

            //Now look them up in the palette and turn them into actual colors.
            byte[] finalDest = new byte[decodedData.Length / 2];

            int pixelSize = paletteFormat == PaletteFormats.IA8 ? 2 : 4;
            int destOffset = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    UnpackPixelFromPalette(decodedData[y * width + x], ref finalDest, destOffset, imagePalette.GetBytes(), paletteFormat);
                    destOffset += pixelSize;
                }
            }

            return finalDest;
        }

        private static byte[] DecodeC8(EndianBinaryReader stream, uint width, uint height, Palette imagePalette, PaletteFormats paletteFormat)
        {
            //4 bpp, 8 block width/4 block height, block size 32 bytes, possible palettes (IA8, RGB565, RGB5A3)
            uint numBlocksW = width / 8;
            uint numBlocksH = height / 4;

            byte[] decodedData = new byte[width * height * 8];

            //Read the indexes from the file
            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //Inner Loop for pixels
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 8; pX++)
                        {
                            //Ensure we're not reading past the end of the image.
                            if ((xBlock * 8 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;


                            byte data = stream.ReadByte();
                            decodedData[width * ((yBlock * 4) + pY) + (xBlock * 8) + pX] = data;
                        }
                    }
                }
            }

            //Now look them up in the palette and turn them into actual colors.
            byte[] finalDest = new byte[decodedData.Length / 2];

            int pixelSize = paletteFormat == PaletteFormats.IA8 ? 2 : 4;
            int destOffset = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    UnpackPixelFromPalette(decodedData[y * width + x], ref finalDest, destOffset, imagePalette.GetBytes(), paletteFormat);
                    destOffset += pixelSize;
                }
            }

            return finalDest;
        }

        private static byte[] DecodeRgb565(EndianBinaryReader stream, uint width, uint height)
        {
            //16 bpp, 4 block width/height, block size 32 bytes, color.
            uint numBlocksW = width / 4;
            uint numBlocksH = height / 4;

            byte[] decodedData = new byte[width * height * 4];

            //Read the indexes from the file
            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //Inner Loop for pixels
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 4; pX++)
                        {
                            //Ensure we're not reading past the end of the image.
                            if ((xBlock * 4 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;

                            ushort sourcePixel = stream.ReadUInt16();
                            RGB565ToRGBA8(sourcePixel, ref decodedData,
                                (int)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 4) + pX)));
                        }
                    }
                }
            }

            return decodedData;
        }

        private static byte[] DecodeCmpr(EndianBinaryReader stream, uint width, uint height)
        {
            //Decode S3TC1
            byte[] buffer = new byte[width * height * 4];

            for (int y = 0; y < height / 4; y += 2)
            {
                for (int x = 0; x < width / 4; x += 2)
                {
                    for (int dy = 0; dy < 2; ++dy)
                    {
                        for (int dx = 0; dx < 2; ++dx)
                        {
                            if (4 * (x + dx) < width && 4 * (y + dy) < height)
                            {
                                byte[] fileData = stream.ReadBytes(8);
                                Buffer.BlockCopy(fileData, 0, buffer, (int)(8 * ((y + dy) * width / 4 + x + dx)), 8);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < width * height / 2; i += 8)
            {
                // Micro swap routine needed
                Swap(ref buffer[i], ref buffer[i + 1]);
                Swap(ref buffer[i + 2], ref buffer[i + 3]);

                buffer[i + 4] = S3TC1ReverseByte(buffer[i + 4]);
                buffer[i + 5] = S3TC1ReverseByte(buffer[i + 5]);
                buffer[i + 6] = S3TC1ReverseByte(buffer[i + 6]);
                buffer[i + 7] = S3TC1ReverseByte(buffer[i + 7]);
            }

            //Now decompress the DXT1 data within it.
            return DecompressDxt1(buffer, width, height);
        }

        private static void Swap(ref byte b1, ref byte b2)
        {
            byte tmp = b1; b1 = b2; b2 = tmp;
        }

        private static ushort Read16Swap(byte[] data, uint offset)
        {
            return (ushort)((Buffer.GetByte(data, (int)offset + 1) << 8) | Buffer.GetByte(data, (int)offset));
        }

        private static uint Read32Swap(byte[] data, uint offset)
        {
            return (uint)((Buffer.GetByte(data, (int)offset + 3) << 24) | (Buffer.GetByte(data, (int)offset + 2) << 16) | (Buffer.GetByte(data, (int)offset + 1) << 8) | Buffer.GetByte(data, (int)offset));
        }

        private static byte S3TC1ReverseByte(byte b)
        {
            byte b1 = (byte)(b & 0x3);
            byte b2 = (byte)(b & 0xC);
            byte b3 = (byte)(b & 0x30);
            byte b4 = (byte)(b & 0xC0);

            return (byte)((b1 << 6) | (b2 << 2) | (b3 >> 2) | (b4 >> 6));
        }

        private static byte[] DecompressDxt1(byte[] src, uint width, uint height)
        {
            uint dataOffset = 0;
            byte[] finalData = new byte[width * height * 4];

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    // Haha this is in little-endian (DXT1) so we have to swap the already swapped bytes.
                    ushort color1 = Read16Swap(src, dataOffset);
                    ushort color2 = Read16Swap(src, dataOffset + 2);
                    uint bits = Read32Swap(src, dataOffset + 4);
                    dataOffset += 8;

                    byte[][] ColorTable = new byte[4][];
                    for (int i = 0; i < 4; i++)
                        ColorTable[i] = new byte[4];

                    RGB565ToRGBA8(color1, ref ColorTable[0], 0);
                    RGB565ToRGBA8(color2, ref ColorTable[1], 0);

                    if (color1 > color2)
                    {
                        ColorTable[2][0] = (byte)((2 * ColorTable[0][0] + ColorTable[1][0] + 1) / 3);
                        ColorTable[2][1] = (byte)((2 * ColorTable[0][1] + ColorTable[1][1] + 1) / 3);
                        ColorTable[2][2] = (byte)((2 * ColorTable[0][2] + ColorTable[1][2] + 1) / 3);
                        ColorTable[2][3] = 0xFF;

                        ColorTable[3][0] = (byte)((ColorTable[0][0] + 2 * ColorTable[1][0] + 1) / 3);
                        ColorTable[3][1] = (byte)((ColorTable[0][1] + 2 * ColorTable[1][1] + 1) / 3);
                        ColorTable[3][2] = (byte)((ColorTable[0][2] + 2 * ColorTable[1][2] + 1) / 3);
                        ColorTable[3][3] = 0xFF;
                    }
                    else
                    {
                        ColorTable[2][0] = (byte)((ColorTable[0][0] + ColorTable[1][0] + 1) / 2);
                        ColorTable[2][1] = (byte)((ColorTable[0][1] + ColorTable[1][1] + 1) / 2);
                        ColorTable[2][2] = (byte)((ColorTable[0][2] + ColorTable[1][2] + 1) / 2);
                        ColorTable[2][3] = 0xFF;

                        ColorTable[3][0] = (byte)((ColorTable[0][0] + 2 * ColorTable[1][0] + 1) / 3);
                        ColorTable[3][1] = (byte)((ColorTable[0][1] + 2 * ColorTable[1][1] + 1) / 3);
                        ColorTable[3][2] = (byte)((ColorTable[0][2] + 2 * ColorTable[1][2] + 1) / 3);
                        ColorTable[3][3] = 0x00;
                    }

                    for (int iy = 0; iy < 4; ++iy)
                    {
                        for (int ix = 0; ix < 4; ++ix)
                        {
                            if (((x + ix) < width) && ((y + iy) < height))
                            {
                                int di = (int)(4 * ((y + iy) * width + x + ix));
                                int si = (int)(bits & 0x3);
                                finalData[di + 0] = ColorTable[si][0];
                                finalData[di + 1] = ColorTable[si][1];
                                finalData[di + 2] = ColorTable[si][2];
                                finalData[di + 3] = ColorTable[si][3];
                            }
                            bits >>= 2;
                        }
                    }
                }
            }

            return finalData;
        }

        private static byte[] DecodeIA8(EndianBinaryReader stream, uint width, uint height)
        {
            uint numBlocksW = width / 4; //4 byte block width
            uint numBlocksH = height / 4; //4 byte block height 

            byte[] decodedData = new byte[width * height * 4];

            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //For each block, we're going to examine block width / block height number of 'pixels'
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 4; pX++)
                        {
                            //Ensure the pixel we're checking is within bounds of the image.
                            if ((xBlock * 4 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;

                            //Now we're looping through each pixel in a block, but a pixel is four bytes long. 
                            uint destIndex = (uint)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 4) + pX));
                            byte byte0 = stream.ReadByte();
                            byte byte1 = stream.ReadByte();
                            decodedData[destIndex + 3] = byte0;
                            decodedData[destIndex + 2] = byte1;
                            decodedData[destIndex + 1] = byte1;
                            decodedData[destIndex + 0] = byte1;
                        }
                    }
                }
            }

            return decodedData;
        }

        private static byte[] DecodeIA4(EndianBinaryReader stream, uint width, uint height)
        {
            uint numBlocksW = width / 8;
            uint numBlocksH = height / 4;

            byte[] decodedData = new byte[width * height * 4];

            for (int yBlock = 0; yBlock < height; yBlock++)
            {
                for (int xBlock = 0; xBlock < width; xBlock++)
                {
                    //For each block, we're going to examine block width / block height number of 'pixels'
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 8; pX++)
                        {
                            //Ensure the pixel we're checking is within bounds of the image.
                            if ((xBlock * 8 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;


                            byte value = stream.ReadByte();

                            byte alpha = (byte)((value & 0xF0) >> 4);
                            byte lum = (byte)(value & 0x0F);

                            uint destIndex = (uint)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 8) + pX));

                            decodedData[destIndex + 0] = (byte)(lum * 0x11);
                            decodedData[destIndex + 1] = (byte)(lum * 0x11);
                            decodedData[destIndex + 2] = (byte)(lum * 0x11);
                            decodedData[destIndex + 3] = (byte)(alpha * 0x11);
                        }
                    }
                }
            }

            return decodedData;
        }

        private static byte[] DecodeI4(EndianBinaryReader stream, uint width, uint height)
        {
            uint numBlocksW = width / 8; //8 byte block width
            uint numBlocksH = height / 8; //8 byte block height 

            byte[] decodedData = new byte[width * height * 4];

            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //For each block, we're going to examine block width / block height number of 'pixels'
                    for (int pY = 0; pY < 8; pY++)
                    {
                        for (int pX = 0; pX < 8; pX += 2)
                        {
                            //Ensure the pixel we're checking is within bounds of the image.
                            if ((xBlock * 8 + pX >= width) || (yBlock * 8 + pY >= height))
                                continue;

                            byte data = stream.ReadByte();
                            byte t = (byte)((data & 0xF0) >> 4);
                            byte t2 = (byte)(data & 0x0F);
                            uint destIndex = (uint)(4 * (width * ((yBlock * 8) + pY) + (xBlock * 8) + pX));

                            decodedData[destIndex + 0] = (byte)(t * 0x11);
                            decodedData[destIndex + 1] = (byte)(t * 0x11);
                            decodedData[destIndex + 2] = (byte)(t * 0x11);
                            decodedData[destIndex + 3] = 0xFF;

                            decodedData[destIndex + 4] = (byte)(t2 * 0x11);
                            decodedData[destIndex + 5] = (byte)(t2 * 0x11);
                            decodedData[destIndex + 6] = (byte)(t2 * 0x11);
                            decodedData[destIndex + 7] = 0xFF;
                        }
                    }
                }
            }

            return decodedData;
        }

        private static byte[] DecodeI8(EndianBinaryReader stream, uint width, uint height)
        {
            uint numBlocksW = width / 8; //8 pixel block width
            uint numBlocksH = height / 4; //4 pixel block height 

            byte[] decodedData = new byte[width * height * 4];

            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //For each block, we're going to examine block width / block height number of 'pixels'
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 8; pX++)
                        {
                            //Ensure the pixel we're checking is within bounds of the image.
                            if ((xBlock * 8 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;

                            byte data = stream.ReadByte();
                            uint destIndex = (uint)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 8) + pX));

                            decodedData[destIndex + 0] = data;
                            decodedData[destIndex + 1] = data;
                            decodedData[destIndex + 2] = data;
                            decodedData[destIndex + 3] = 0xFF;
                        }
                    }
                }
            }

            return decodedData;
        }

        private static byte[] DecodeRgb5A3(EndianBinaryReader stream, uint width, uint height)
        {
            uint numBlocksW = width / 4; //4 byte block width
            uint numBlocksH = height / 4; //4 byte block height 

            byte[] decodedData = new byte[width * height * 4];

            for (int yBlock = 0; yBlock < numBlocksH; yBlock++)
            {
                for (int xBlock = 0; xBlock < numBlocksW; xBlock++)
                {
                    //For each block, we're going to examine block width / block height number of 'pixels'
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 4; pX++)
                        {
                            //Ensure the pixel we're checking is within bounds of the image.
                            if ((xBlock * 4 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;

                            ushort sourcePixel = stream.ReadUInt16();
                            RGB5A3ToRGBA8(sourcePixel, ref decodedData,
                                (int)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 4) + pX)));
                        }
                    }
                }
            }

            return decodedData;
        }

        private static void UnpackPixelFromPalette(int paletteIndex, ref byte[] dest, int offset, byte[] paletteData, PaletteFormats format)
        {
            switch (format)
            {
                case PaletteFormats.IA8:
                    dest[0] = paletteData[2 * paletteIndex + 1];
                    dest[1] = paletteData[2 * paletteIndex + 0];
                    break;
                case PaletteFormats.RGB565:
                    {
                        ushort palettePixelData = (ushort)((Buffer.GetByte(paletteData, 2 * paletteIndex) << 8) | Buffer.GetByte(paletteData, 2 * paletteIndex + 1));
                        RGB565ToRGBA8(palettePixelData, ref dest, offset);
                    }
                    break;
                case PaletteFormats.RGB5A3:
                    {
                        ushort palettePixelData = (ushort)((Buffer.GetByte(paletteData, 2 * paletteIndex) << 8) | Buffer.GetByte(paletteData, 2 * paletteIndex + 1));
                        RGB5A3ToRGBA8(palettePixelData, ref dest, offset);
                    }
                    break;
            }
        }



        /// <summary>
        /// Convert a RGB565 encoded pixel (two bytes in length) to a RGBA (4 byte in length)
        /// pixel.
        /// </summary>
        /// <param name="sourcePixel">RGB565 encoded pixel.</param>
        /// <param name="dest">Destination array for RGBA pixel.</param>
        /// <param name="destOffset">Offset into destination array to write RGBA pixel.</param>
        private static void RGB565ToRGBA8(ushort sourcePixel, ref byte[] dest, int destOffset)
        {
            byte r, g, b;
            r = (byte)((sourcePixel & 0xF100) >> 11);
            g = (byte)((sourcePixel & 0x7E0) >> 5);
            b = (byte)((sourcePixel & 0x1F));

            r = (byte)((r << (8 - 5)) | (r >> (10 - 8)));
            g = (byte)((g << (8 - 6)) | (g >> (12 - 8)));
            b = (byte)((b << (8 - 5)) | (b >> (10 - 8)));

            dest[destOffset] = b;
            dest[destOffset + 1] = g;
            dest[destOffset + 2] = r;
            dest[destOffset + 3] = 0xFF; //Set alpha to 1
        }

        /// <summary>
        /// Convert a RGB5A3 encoded pixel (two bytes in length) to an RGBA (4 byte in length)
        /// pixel.
        /// </summary>
        /// <param name="sourcePixel">RGB5A3 encoded pixel.</param>
        /// <param name="dest">Destination array for RGBA pixel.</param>
        /// <param name="destOffset">Offset into destination array to write RGBA pixel.</param>
        private static void RGB5A3ToRGBA8(ushort sourcePixel, ref byte[] dest, int destOffset)
        {
            byte r, g, b, a;

            //No alpha bits
            if ((sourcePixel & 0x8000) == 0x8000)
            {
                a = 0xFF;
                r = (byte)((sourcePixel & 0x7C00) >> 10);
                g = (byte)((sourcePixel & 0x3E0) >> 5);
                b = (byte)(sourcePixel & 0x1F);

                r = (byte)((r << (8 - 5)) | (r >> (10 - 8)));
                g = (byte)((g << (8 - 5)) | (g >> (10 - 8)));
                b = (byte)((b << (8 - 5)) | (b >> (10 - 8)));
            }
            //Alpha bits
            else
            {
                a = (byte)((sourcePixel & 0x7000) >> 12);
                r = (byte)((sourcePixel & 0xF00) >> 8);
                g = (byte)((sourcePixel & 0xF0) >> 4);
                b = (byte)(sourcePixel & 0xF);

                a = (byte)((a << (8 - 3)) | (a << (8 - 6)) | (a >> (9 - 8)));
                r = (byte)((r << (8 - 4)) | r);
                g = (byte)((g << (8 - 4)) | g);
                b = (byte)((b << (8 - 4)) | b);
            }

            dest[destOffset + 0] = a;
            dest[destOffset + 1] = b;
            dest[destOffset + 2] = g;
            dest[destOffset + 3] = r;
        }
        #endregion
    }
}