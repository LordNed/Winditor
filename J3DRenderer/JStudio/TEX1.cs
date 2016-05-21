using GameFormatReader.Common;
using System.Diagnostics;
using System.ComponentModel;

namespace J3DRenderer.JStudio
{
    public class Texture
    {
        public string Name;
        public BinaryTextureImage CompressedData;

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

                Texture texture = new Texture();
                texture.Name = nameTable[t];
                texture.CompressedData = compressedTex;
                Textures.Add(texture);
            }
        }
    }
}
