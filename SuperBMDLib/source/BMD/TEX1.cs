using Assimp;
using GameFormatReader.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SuperBMDLib.Materials;
using SuperBMDLib.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace SuperBMDLib.BMD
{
    public class TEX1
    {
        public List<BinaryTextureImage> Textures { get; private set; }

        public TEX1(EndianBinaryReader reader, int offset)
        {
            Textures = new List<BinaryTextureImage>();

            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
            reader.SkipInt32();
            int tex1Size = reader.ReadInt32();
            short texCount = reader.ReadInt16();
            reader.SkipInt16();

            int textureHeaderOffset = reader.ReadInt32();
            int textureNameTableOffset = reader.ReadInt32();

            List<string> names = NameTableIO.Load(reader, offset + textureNameTableOffset);

            reader.BaseStream.Seek(textureHeaderOffset + offset, System.IO.SeekOrigin.Begin);

            for (int i = 0; i < texCount; i++)
            {
                reader.BaseStream.Seek((offset + 0x20 + (0x20 * i)), System.IO.SeekOrigin.Begin);

                BinaryTextureImage img = new BinaryTextureImage(names[i]);
                img.Load(reader, (offset + 0x20 + (0x20 * i)));
                Textures.Add(img);
            }
        }

        public TEX1(Assimp.Scene scene, Arguments args)
        {
            Textures = new List<BinaryTextureImage>();

            if (args.texheaders_path != "")
            {
                string dir_path = Path.GetDirectoryName(args.texheaders_path);
                LoadTexturesFromJson(args.texheaders_path, dir_path);
            }
            else
                LoadTexturesFromScene(scene, Path.GetDirectoryName(args.input_path));
        }

        private void LoadTexturesFromJson(string headers_path, string directory_path)
        {
            JsonSerializer serial = new JsonSerializer();
            serial.Formatting = Formatting.Indented;
            serial.Converters.Add(new StringEnumConverter());

            using (StreamReader strm_reader = new StreamReader(headers_path))
            {
                strm_reader.BaseStream.Seek(0, SeekOrigin.Begin);
                JsonTextReader reader = new JsonTextReader(strm_reader);
                Textures = serial.Deserialize<List<BinaryTextureImage>>(reader);
            }

            foreach (BinaryTextureImage tex in Textures)
            {
                // We'll search for duplicate texture names.
                BinaryTextureImage duplicate_search = Textures.Find(x => x.Name == tex.Name);

                // Otherwise we have to load the image from disk
                string name_without_ext = Path.Combine(directory_path, tex.Name);
                string full_img_path = FindImagePath(name_without_ext);

                if (full_img_path == "")
                {
                    throw new Exception($"Could not find texture \"{ name_without_ext }\".");
                }

                tex.LoadImageFromDisk(full_img_path);
            }
        }

        private void LoadTexturesFromScene(Assimp.Scene scene, string model_directory)
        {
            foreach (Assimp.Mesh mesh in scene.Meshes)
            {
                Assimp.Material mat = scene.Materials[mesh.MaterialIndex];

                if (mat.HasTextureDiffuse)
                {
                    BinaryTextureImage img = new BinaryTextureImage();
                    img.Load(mat.TextureDiffuse, model_directory);
                    Textures.Add(img);
                }
            }
        }

        private string FindImagePath(string name_without_ext)
        {
            if (File.Exists(name_without_ext + ".png"))
                return name_without_ext + ".png";
            if (File.Exists(name_without_ext + ".jpg"))
                return name_without_ext + ".jpg";
            if (File.Exists(name_without_ext + ".tga"))
                return name_without_ext + ".tga";
            if (File.Exists(name_without_ext + ".bmp"))
                return name_without_ext + ".bmp";

            return "";
        }

        public void DumpTextures(string directory)
        {
            if (!System.IO.Directory.Exists(directory) && directory != "")
                System.IO.Directory.CreateDirectory(directory);

            foreach (BinaryTextureImage tex in Textures)
            {
                tex.SaveImageToDisk(directory);
            }

            JsonSerializer serial = new JsonSerializer();
            serial.Formatting = Formatting.Indented;
            serial.Converters.Add(new StringEnumConverter());

            using (FileStream strm = new FileStream(Path.Combine(directory, "tex_headers.json"), FileMode.Create, FileAccess.Write))
            {
                StreamWriter writer = new StreamWriter(strm);
                writer.AutoFlush = true;
                serial.Serialize(writer, Textures);
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            long start = writer.BaseStream.Position;

            writer.Write("TEX1".ToCharArray());
            writer.Write(0); // Placeholder for section size
            writer.Write((short)Textures.Count);
            writer.Write((short)-1);
            writer.Write(32); // Offset to the start of the texture data. Always 32
            writer.Write(0); // Placeholder for string table offset

            StreamUtility.PadStreamWithString(writer, 32);

            List<string> names = new List<string>();
            Dictionary<string, Tuple<byte[], ushort[]>> image_palette_Data = new Dictionary<string, Tuple<byte[], ushort[]>>();
            Dictionary<string, int> imageDataOffsets = new Dictionary<string, int>();
            Dictionary<string, int> paletteDataOffsets = new Dictionary<string, int>();

            foreach (BinaryTextureImage img in Textures)
            {
                if (image_palette_Data.ContainsKey(img.Name))
                {
                    img.PaletteCount = (ushort)image_palette_Data[img.Name].Item2.Length;
                    img.PalettesEnabled = (image_palette_Data[img.Name].Item2.Length > 0);
                }
                else
                {
                    image_palette_Data.Add(img.Name, img.EncodeData());
                    imageDataOffsets.Add(img.Name, 0);
                    paletteDataOffsets.Add(img.Name, 0);
                }

                names.Add(img.Name);
                img.WriteHeader(writer);
            }

            long curOffset = writer.BaseStream.Position;

            // Write the palette data and note the offset in paletteDataOffsets
            foreach (string key in image_palette_Data.Keys)
            {
                paletteDataOffsets[key] = (int)(curOffset - start);

                if (image_palette_Data[key].Item2.Length > 0)
                {
                    foreach (ushort st in image_palette_Data[key].Item2)
                        writer.Write(st);

                    StreamUtility.PadStreamWithString(writer, 32);
                }

                curOffset = writer.BaseStream.Position;
            }

            // Write the image data and note the offset in imageDataOffsets
            foreach (string key in image_palette_Data.Keys)
            {
                imageDataOffsets[key] = (int)(curOffset - start);

                writer.Write(image_palette_Data[key].Item1);

                curOffset = writer.BaseStream.Position;
            }

            // Write texture name table offset
            writer.Seek((int)start + 16, System.IO.SeekOrigin.Begin);
            writer.Write((int)(curOffset - start));
            writer.Seek((int)curOffset, System.IO.SeekOrigin.Begin);
            NameTableIO.Write(writer, names);

            StreamUtility.PadStreamWithString(writer, 32);

            long end = writer.BaseStream.Position;
            long length = (end - start);

            // Write TEX1 size
            writer.Seek((int)start + 4, System.IO.SeekOrigin.Begin);
            writer.Write((int)length);
            writer.Seek((int)end, System.IO.SeekOrigin.Begin);

            writer.Seek((int)start + 32, SeekOrigin.Begin);

            // Write palette and image data offsets to headers
            for (int i = 0; i < Textures.Count; i++)
            {
                int header_offset_const = 32 + i * 32;

                // Start is the beginning of the TEX1 section;
                // (i * 32) is the offset of the header in the header data block;
                // 32 is the offset of the header data block from the beginning of TEX1;
                // 12 is the offset of the palette data offset in the header
                writer.Seek((int)start + (i * 32) + 32 + 12, SeekOrigin.Begin);
                writer.Write(paletteDataOffsets[Textures[i].Name] - header_offset_const);

                // Same as above, except instead of 12 it's 28.
                // 28 is the offset of the image data offset in the header
                writer.Seek((int)start + (i * 32) + 32 + 28, SeekOrigin.Begin);
                writer.Write(imageDataOffsets[Textures[i].Name] - header_offset_const);
            }
        }

        public BinaryTextureImage this[int i]
        {
            get
            {
                if (Textures != null && Textures.Count > i)
                {
                    return Textures[i];
                }
                else
                {
                    Console.WriteLine($"Could not retrieve texture at index { i }.");
                    return null;
                }
            }
            set
            {
                if (Textures == null)
                    Textures = new List<BinaryTextureImage>();

                Textures[i] = value;
            }
        }

        public BinaryTextureImage this[string s]
        {
            get
            {
                if (Textures == null)
                {
                    Console.WriteLine("There are no textures currently loaded.");
                    return null;
                }

                if (Textures.Count == 0)
                {
                    Console.WriteLine("There are no textures currently loaded.");
                    return null;
                }

                foreach (BinaryTextureImage tex in Textures)
                {
                    if (tex.Name == s)
                        return tex;
                }

                Console.WriteLine($"No texture with the name { s } was found.");
                return null;
            }

            private set
            {
                if (Textures == null)
                {
                    Textures = new List<BinaryTextureImage>();
                    Console.WriteLine("There are no textures currently loaded.");
                    return;
                }

                for (int i = 0; i < Textures.Count; i++)
                {
                    if (Textures[i].Name == s)
                    {
                        Textures[i] = value;
                        break;
                    }

                    if (i == Textures.Count - 1)
                        Console.WriteLine($"No texture with the name { s } was found.");
                }
            }
        }
    }
}
