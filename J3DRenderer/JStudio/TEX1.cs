using GameFormatReader.Common;
using System.Diagnostics;
using System.ComponentModel;
using OpenTK.Graphics.OpenGL;
using WindEditor;
using System.IO;

namespace J3DRenderer.JStudio
{
    public class Texture
    {
        public string Name { get; protected set; }
        public BinaryTextureImage CompressedData { get; protected set; }

        private int m_glTextureIndex;

        public Texture(string name, BinaryTextureImage compressedData)
        {
            Name = name;
            CompressedData = compressedData;

            m_glTextureIndex = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, m_glTextureIndex);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GXToOpenGL.GetWrapMode(compressedData.WrapS));
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GXToOpenGL.GetWrapMode(compressedData.WrapT));
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GXToOpenGL.GetMinFilter(compressedData.MinFilter));
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GXToOpenGL.GetMagFilter(compressedData.MagFilter));

            // Border Color
            WLinearColor borderColor = compressedData.BorderColor;
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, new[] { borderColor.R, borderColor.G, borderColor.B, borderColor.A });

            // ToDo: Min/Mag LOD & Biases

            // Upload Image Data
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, compressedData.Width, compressedData.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, compressedData.GetData());

            // Generate Mip Maps
            if (compressedData.MipMapCount > 0)
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Bind(int toIndex)
        {
            int glIndex = toIndex + (int)TextureUnit.Texture0;
            GL.ActiveTexture((TextureUnit)glIndex);
            GL.BindTexture(TextureTarget.Texture2D, m_glTextureIndex);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class TEX1
    {
        public BindingList<Texture> Textures;

        public void LoadTEX1FromStream(EndianBinaryReader reader, long tagStart)
        {
            ushort numTextures = reader.ReadUInt16();
            Trace.Assert(reader.ReadUInt16() == 0xFFFF); // Padding

            int textureHeaderDataOffset = reader.ReadInt32();
            int stringTableOffset = reader.ReadInt32();

            // Texture Names
            reader.BaseStream.Position = tagStart + stringTableOffset;
            StringTable nameTable = StringTable.FromStream(reader);

            Textures = new BindingList<Texture>();
            for(int t = 0; t < numTextures; t++)
            {
                // Reset the stream position to the start of this header as loading the actual data of the texture
                // moves the stream head around.
                reader.BaseStream.Position = tagStart + textureHeaderDataOffset + (t * 0x20);

                BinaryTextureImage compressedTex = new BinaryTextureImage();
                compressedTex.Load(reader, tagStart + 0x20 /* Size of TEX1 header?*/, t);

                Texture texture = new Texture(nameTable[t], compressedTex);
                Textures.Add(texture);

                compressedTex.SaveImageToDisk(string.Format("TextureDump/{0}_{1}.png", texture.Name, compressedTex.Format));
            }
        }
    }
}
