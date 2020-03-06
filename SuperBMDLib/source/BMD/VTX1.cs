using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using SuperBMDLib.Geometry;
using SuperBMDLib.Geometry.Enums;
using OpenTK;
using SuperBMDLib.Util;

namespace SuperBMDLib.BMD
{
    public class VTX1
    {
        public VertexData Attributes { get; private set; }
        public SortedDictionary<GXVertexAttribute, Tuple<GXDataType, byte>> StorageFormats { get; private set; }

        public VTX1(EndianBinaryReader reader, int offset)
        {
            Attributes = new VertexData();
            StorageFormats = new SortedDictionary<GXVertexAttribute, Tuple<GXDataType, byte>>();

            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);

            reader.SkipInt32();
            int vtx1Size = reader.ReadInt32();
            int attributeHeaderOffset = reader.ReadInt32();

            int[] attribDataOffsets = new int[13];

            for (int i = 0; i < 13; i++)
                attribDataOffsets[i] = reader.ReadInt32();

            GXVertexAttribute attrib = (GXVertexAttribute)reader.ReadInt32();

            while (attrib != GXVertexAttribute.Null)
            {
                GXComponentCount componentCount = (GXComponentCount)reader.ReadInt32();
                GXDataType componentType = (GXDataType)reader.ReadInt32();
                byte fractionalBitCount = reader.ReadByte();
                StorageFormats.Add(attrib, new Tuple<GXDataType, byte>(componentType, fractionalBitCount));

                reader.Skip(3);
                long curPos = reader.BaseStream.Position;

                int attribDataSize = 0;
                int attribOffset = GetAttributeDataOffset(attribDataOffsets, vtx1Size, attrib, out attribDataSize);
                int attribCount = GetAttributeDataCount(attribDataSize, attrib, componentType, componentCount);
                Attributes.SetAttributeData(attrib, LoadAttributeData(reader, offset + attribOffset, attribCount, fractionalBitCount, attrib, componentType, componentCount));

                reader.BaseStream.Seek(curPos, System.IO.SeekOrigin.Begin);
                attrib = (GXVertexAttribute)reader.ReadInt32();
            }

            reader.BaseStream.Seek(offset + vtx1Size, System.IO.SeekOrigin.Begin);
        }

        public VTX1(Assimp.Scene scene)
        {
            Attributes = new VertexData();
            StorageFormats = new SortedDictionary<GXVertexAttribute, Tuple<GXDataType, byte>>();

            int i = -1;

            foreach (Assimp.Mesh mesh in scene.Meshes)
            {
                i++;


                if (mesh.HasVertices)
                {
                    SetAssimpPositionAttribute(mesh);
                    if (!StorageFormats.ContainsKey(GXVertexAttribute.Position))
                        StorageFormats.Add(GXVertexAttribute.Position, new Tuple<GXDataType, byte>(GXDataType.Float32, 0));
                }
                else
                    throw new Exception($"Mesh \"{ mesh.Name }\" ({i}) has no vertices!");

                if (mesh.HasNormals)
                {
                    SetAssimpNormalAttribute(mesh);
                    if (!StorageFormats.ContainsKey(GXVertexAttribute.Normal))
                        StorageFormats.Add(GXVertexAttribute.Normal, new Tuple<GXDataType, byte>(GXDataType.Signed16, 14));
                }
                else
                    Console.WriteLine($"Mesh \"{ mesh.Name }\" ({i}) has no normals.");

                if (mesh.HasVertexColors(0))
                {
                    //Console.WriteLine($"Mesh \"{ mesh.Name }\" ({i}) has vertex colors on channel 0.");
                    SetAssimpColorAttribute(0, GXVertexAttribute.Color0, mesh);
                    if (!StorageFormats.ContainsKey(GXVertexAttribute.Color0))
                        StorageFormats.Add(GXVertexAttribute.Color0, new Tuple<GXDataType, byte>(GXDataType.RGBA8, 0));
                }
                //else
                //    Console.WriteLine($"Mesh \"{ mesh.Name }\" has no colors on channel 0.");

                if (mesh.HasVertexColors(1))
                {
                    //Console.WriteLine($"Mesh \"{ mesh.Name }\" ({i}) has vertex colors on channel 1.");
                    SetAssimpColorAttribute(1, GXVertexAttribute.Color1, mesh);
                    if (!StorageFormats.ContainsKey(GXVertexAttribute.Color1))
                        StorageFormats.Add(GXVertexAttribute.Color1, new Tuple<GXDataType, byte>(GXDataType.RGBA8, 0));
                }
                //else
                    //Console.WriteLine($"Mesh \"{ mesh.Name }\" has no colors on channel 1.");

                for (int texCoords = 0; texCoords < 8; texCoords++)
                {
                    if (mesh.HasTextureCoords(texCoords))
                    {
                        //Console.WriteLine($"Mesh \"{ mesh.Name }\" ({i}) has texture coordinates on channel { texCoords }.");
                        SetAssimpTexCoordAttribute(texCoords, GXVertexAttribute.Tex0 + texCoords, mesh);
                        if (!StorageFormats.ContainsKey(GXVertexAttribute.Tex0 + texCoords))
                            StorageFormats.Add(GXVertexAttribute.Tex0 + texCoords, new Tuple<GXDataType, byte>(GXDataType.Signed16, 8));
                    }
                    //else
                    //    Console.WriteLine($"Mesh \"{ mesh.Name }\" has no texture coordinates on channel { texCoords }.");
                }
            }
        }

        public object LoadAttributeData(EndianBinaryReader reader, int offset, int count, byte frac, GXVertexAttribute attribute, GXDataType dataType, GXComponentCount compCount)
        {
            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
            object final = null;

            switch (attribute)
            {
                case GXVertexAttribute.Position:
                    switch (compCount)
                    {
                        case GXComponentCount.Position_XY:
                            final = LoadVec2Data(reader, frac, count, dataType);
                            break;
                        case GXComponentCount.Position_XYZ:
                            final = LoadVec3Data(reader, frac, count, dataType);
                            break;
                    }
                    break;
                case GXVertexAttribute.Normal:
                    switch (compCount)
                    {
                        case GXComponentCount.Normal_XYZ:
                            final = LoadVec3Data(reader, frac, count, dataType);
                            break;
                        case GXComponentCount.Normal_NBT:
                            break;
                        case GXComponentCount.Normal_NBT3:
                            break;
                    }
                    break;
                case GXVertexAttribute.Color0:
                case GXVertexAttribute.Color1:
                    final = LoadColorData(reader, count, dataType);
                    break;
                case GXVertexAttribute.Tex0:
                case GXVertexAttribute.Tex1:
                case GXVertexAttribute.Tex2:
                case GXVertexAttribute.Tex3:
                case GXVertexAttribute.Tex4:
                case GXVertexAttribute.Tex5:
                case GXVertexAttribute.Tex6:
                case GXVertexAttribute.Tex7:
                    switch (compCount)
                    {
                        case GXComponentCount.TexCoord_S:
                            final = LoadSingleFloat(reader, frac, count, dataType);
                            break;
                        case GXComponentCount.TexCoord_ST:
                            final = LoadVec2Data(reader, frac, count, dataType);
                            break;
                    }
                    break;
            }

            return final;
        }

        private List<float> LoadSingleFloat(EndianBinaryReader reader, byte frac, int count, GXDataType dataType)
        {
            List<float> floatList = new List<float>();

            for (int i = 0; i < count; i++)
            {
                switch (dataType)
                {
                    case GXDataType.Unsigned8:
                        byte compu81 = reader.ReadByte();
                        float compu81Float = (float)compu81 / (float)(1 << frac);
                        floatList.Add(compu81Float);
                        break;
                    case GXDataType.Signed8:
                        sbyte comps81 = reader.ReadSByte();
                        float comps81Float = (float)comps81 / (float)(1 << frac);
                        floatList.Add(comps81Float);
                        break;
                    case GXDataType.Unsigned16:
                        ushort compu161 = reader.ReadUInt16();
                        float compu161Float = (float)compu161 / (float)(1 << frac);
                        floatList.Add(compu161Float);
                        break;
                    case GXDataType.Signed16:
                        short comps161 = reader.ReadInt16();
                        float comps161Float = (float)comps161 / (float)(1 << frac);
                        floatList.Add(comps161Float);
                        break;
                    case GXDataType.Float32:
                        floatList.Add(reader.ReadSingle());
                        break;
                }
            }

            return floatList;
        }

        private List<Vector2> LoadVec2Data(EndianBinaryReader reader, byte frac, int count, GXDataType dataType)
        {
            List<Vector2> vec2List = new List<Vector2>();

            for (int i = 0; i < count; i++)
            {
                switch(dataType)
                {
                    case GXDataType.Unsigned8:
                        byte compu81 = reader.ReadByte();
                        byte compu82 = reader.ReadByte();
                        float compu81Float = (float)compu81 / (float)(1 << frac);
                        float compu82Float = (float)compu82 / (float)(1 << frac);
                        vec2List.Add(new Vector2(compu81Float, compu82Float));
                        break;
                    case GXDataType.Signed8:
                        sbyte comps81 = reader.ReadSByte();
                        sbyte comps82 = reader.ReadSByte();
                        float comps81Float = (float)comps81 / (float)(1 << frac);
                        float comps82Float = (float)comps82 / (float)(1 << frac);
                        vec2List.Add(new Vector2(comps81Float, comps82Float));
                        break;
                    case GXDataType.Unsigned16:
                        ushort compu161 = reader.ReadUInt16();
                        ushort compu162 = reader.ReadUInt16();
                        float compu161Float = (float)compu161 / (float)(1 << frac);
                        float compu162Float = (float)compu162 / (float)(1 << frac);
                        vec2List.Add(new Vector2(compu161Float, compu162Float));
                        break;
                    case GXDataType.Signed16:
                        short comps161 = reader.ReadInt16();
                        short comps162 = reader.ReadInt16();
                        float comps161Float = (float)comps161 / (float)(1 << frac);
                        float comps162Float = (float)comps162 / (float)(1 << frac);
                        vec2List.Add(new Vector2(comps161Float, comps162Float));
                        break;
                    case GXDataType.Float32:
                        vec2List.Add(new Vector2(reader.ReadSingle(), reader.ReadSingle()));
                        break;
                }
            }

            return vec2List;
        }

        private List<Vector3> LoadVec3Data(EndianBinaryReader reader, byte frac, int count, GXDataType dataType)
        {
            List<Vector3> vec3List = new List<Vector3>();

            for (int i = 0; i < count; i++)
            {
                switch (dataType)
                {
                    case GXDataType.Unsigned8:
                        byte compu81 = reader.ReadByte();
                        byte compu82 = reader.ReadByte();
                        byte compu83 = reader.ReadByte();
                        float compu81Float = (float)compu81 / (float)(1 << frac);
                        float compu82Float = (float)compu82 / (float)(1 << frac);
                        float compu83Float = (float)compu83 / (float)(1 << frac);
                        vec3List.Add(new Vector3(compu81Float, compu82Float, compu83Float));
                        break;
                    case GXDataType.Signed8:
                        sbyte comps81 = reader.ReadSByte();
                        sbyte comps82 = reader.ReadSByte();
                        sbyte comps83 = reader.ReadSByte();
                        float comps81Float = (float)comps81 / (float)(1 << frac);
                        float comps82Float = (float)comps82 / (float)(1 << frac);
                        float comps83Float = (float)comps83 / (float)(1 << frac);
                        vec3List.Add(new Vector3(comps81Float, comps82Float, comps83Float));
                        break;
                    case GXDataType.Unsigned16:
                        ushort compu161 = reader.ReadUInt16();
                        ushort compu162 = reader.ReadUInt16();
                        ushort compu163 = reader.ReadUInt16();
                        float compu161Float = (float)compu161 / (float)(1 << frac);
                        float compu162Float = (float)compu162 / (float)(1 << frac);
                        float compu163Float = (float)compu163 / (float)(1 << frac);
                        vec3List.Add(new Vector3(compu161Float, compu162Float, compu163Float));
                        break;
                    case GXDataType.Signed16:
                        short comps161 = reader.ReadInt16();
                        short comps162 = reader.ReadInt16();
                        short comps163 = reader.ReadInt16();
                        float comps161Float = (float)comps161 / (float)(1 << frac);
                        float comps162Float = (float)comps162 / (float)(1 << frac);
                        float comps163Float = (float)comps163 / (float)(1 << frac);
                        vec3List.Add(new Vector3(comps161Float, comps162Float, comps163Float));
                        break;
                    case GXDataType.Float32:
                        vec3List.Add(new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                        break;
                }
            }

            return vec3List;
        }

        private List<Color> LoadColorData(EndianBinaryReader reader, int count, GXDataType dataType)
        {
            List<Color> colorList = new List<Color>();

            for (int i = 0; i < count; i++)
            {
                switch (dataType)
                {
                    case GXDataType.RGB565:
                        short colorShort = reader.ReadInt16();
                        int r5 = (colorShort & 0xF800) >> 11;
                        int g6 = (colorShort & 0x07E0) >> 5;
                        int b5 = (colorShort & 0x001F);
                        colorList.Add(new Color((float)r5 / 255.0f, (float)g6 / 255.0f, (float)b5 / 255.0f));
                        break;
                    case GXDataType.RGB8:
                        byte r8 = reader.ReadByte();
                        byte g8 = reader.ReadByte();
                        byte b8 = reader.ReadByte();
                        reader.SkipByte();
                        colorList.Add(new Color((float)r8 / 255.0f, (float)g8 / 255.0f, (float)b8 / 255.0f));
                        break;
                    case GXDataType.RGBX8:
                        byte rx8 = reader.ReadByte();
                        byte gx8 = reader.ReadByte();
                        byte bx8 = reader.ReadByte();
                        reader.SkipByte();
                        colorList.Add(new Color((float)rx8 / 255.0f, (float)gx8 / 255.0f, (float)bx8 / 255.0f));
                        break;
                    case GXDataType.RGBA4:
                        short colorShortA = reader.ReadInt16();
                        int r4 = (colorShortA & 0xF000) >> 12;
                        int g4 = (colorShortA & 0x0F00) >> 8;
                        int b4 = (colorShortA & 0x00F0) >> 4;
                        int a4 = (colorShortA & 0x000F);
                        colorList.Add(new Color((float)r4 / 255.0f, (float)g4 / 255.0f, (float)b4 / 255.0f, (float)a4 / 255.0f));
                        break;
                    case GXDataType.RGBA6:
                        int colorInt = reader.ReadInt32();
                        int r6 =  (colorInt & 0xFC0000) >> 18;
                        int ga6 = (colorInt & 0x03F000) >> 12;
                        int b6 =  (colorInt & 0x000FC0) >> 6;
                        int a6 =  (colorInt & 0x00003F);
                        colorList.Add(new Color((float)r6 / 255.0f, (float)ga6 / 255.0f, (float)b6 / 255.0f, (float)a6 / 255.0f));
                        break;
                    case GXDataType.RGBA8:
                        byte ra8 = reader.ReadByte();
                        byte ga8 = reader.ReadByte();
                        byte ba8 = reader.ReadByte();
                        byte aa8 = reader.ReadByte();
                        colorList.Add(new Color((float)ra8 / 255.0f, (float)ga8 / 255.0f, (float)ba8 / 255.0f, (float)aa8 / 255.0f));
                        break;
                }
            }

            return colorList;
        }

        private int GetAttributeDataOffset(int[] offsets, int vtx1Size, GXVertexAttribute attribute, out int size)
        {
            int offset = 0;
            size = 0;
            Vtx1OffsetIndex start = Vtx1OffsetIndex.PositionData;

            switch (attribute)
            {
                case GXVertexAttribute.Position:
                    start = Vtx1OffsetIndex.PositionData;
                    offset = offsets[(int)Vtx1OffsetIndex.PositionData];
                    break;
                case GXVertexAttribute.Normal:
                    start = Vtx1OffsetIndex.NormalData;
                    offset = offsets[(int)Vtx1OffsetIndex.NormalData];
                    break;
                case GXVertexAttribute.Color0:
                    start = Vtx1OffsetIndex.Color0Data;
                    offset = offsets[(int)Vtx1OffsetIndex.Color0Data];
                    break;
                case GXVertexAttribute.Color1:
                    start = Vtx1OffsetIndex.Color1Data;
                    offset = offsets[(int)Vtx1OffsetIndex.Color1Data];
                    break;
                case GXVertexAttribute.Tex0:
                    start = Vtx1OffsetIndex.TexCoord0Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord0Data];
                    break;
                case GXVertexAttribute.Tex1:
                    start = Vtx1OffsetIndex.TexCoord1Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord1Data];
                    break;
                case GXVertexAttribute.Tex2:
                    start = Vtx1OffsetIndex.TexCoord2Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord2Data];
                    break;
                case GXVertexAttribute.Tex3:
                    start = Vtx1OffsetIndex.TexCoord3Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord3Data];
                    break;
                case GXVertexAttribute.Tex4:
                    start = Vtx1OffsetIndex.TexCoord4Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord4Data];
                    break;
                case GXVertexAttribute.Tex5:
                    start = Vtx1OffsetIndex.TexCoord5Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord5Data];
                    break;
                case GXVertexAttribute.Tex6:
                    start = Vtx1OffsetIndex.TexCoord6Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord6Data];
                    break;
                case GXVertexAttribute.Tex7:
                    start = Vtx1OffsetIndex.TexCoord7Data;
                    offset = offsets[(int)Vtx1OffsetIndex.TexCoord7Data];
                    break;
                default:
                    throw new ArgumentException("attribute");
            }

            for (int i = (int)start + 1; i < 13; i++)
            {
                if (i == 12)
                {
                    size = vtx1Size - offset;
                    break;
                }

                int nextOffset = offsets[i];

                if (nextOffset == 0)
                    continue;
                else
                {
                    size = nextOffset - offset;
                    break;
                }
            }

            return offset;
        }

        private int GetAttributeDataCount(int size, GXVertexAttribute attribute, GXDataType dataType, GXComponentCount compCount)
        {
            int compCnt = 0;
            int compStride = 0;

            if (attribute == GXVertexAttribute.Color0 || attribute == GXVertexAttribute.Color1)
            {
                switch (dataType)
                {
                    case GXDataType.RGB565:
                    case GXDataType.RGBA4:
                        compCnt = 1;
                        compStride = 2;
                        break;
                    case GXDataType.RGB8:
                    case GXDataType.RGBX8:
                    case GXDataType.RGBA6:
                    case GXDataType.RGBA8:
                        compCnt = 4;
                        compStride = 1;
                        break;
                }
            }

            else
            {
                switch (dataType)
                {
                    case GXDataType.Unsigned8:
                    case GXDataType.Signed8:
                        compStride = 1;
                        break;
                    case GXDataType.Unsigned16:
                    case GXDataType.Signed16:
                        compStride = 2;
                        break;
                    case GXDataType.Float32:
                        compStride = 4;
                        break;
                }

                switch (attribute)
                {
                    case GXVertexAttribute.Position:
                        if (compCount == GXComponentCount.Position_XY)
                            compCnt = 2;
                        else if (compCount == GXComponentCount.Position_XYZ)
                            compCnt = 3;
                        break;
                    case GXVertexAttribute.Normal:
                        if (compCount == GXComponentCount.Normal_XYZ)
                            compCnt = 3;
                        break;
                    case GXVertexAttribute.Tex0:
                    case GXVertexAttribute.Tex1:
                    case GXVertexAttribute.Tex2:
                    case GXVertexAttribute.Tex3:
                    case GXVertexAttribute.Tex4:
                    case GXVertexAttribute.Tex5:
                    case GXVertexAttribute.Tex6:
                    case GXVertexAttribute.Tex7:
                        if (compCount == GXComponentCount.TexCoord_S)
                            compCnt = 1;
                        else if (compCount == GXComponentCount.TexCoord_ST)
                            compCnt = 2;
                        break;
                }
            }

            return size / (compCnt * compStride);
        }

        private void SetAssimpPositionAttribute(Assimp.Mesh mesh) {
            if (!Attributes.CheckAttribute(GXVertexAttribute.Position))
                Attributes.SetAttributeData(GXVertexAttribute.Position, new List<Vector3>());
        }

        private void SetAssimpNormalAttribute(Assimp.Mesh mesh)
        {
            if (!Attributes.CheckAttribute(GXVertexAttribute.Normal))
                Attributes.SetAttributeData(GXVertexAttribute.Normal, new List<Vector3>());
        }

        private void SetAssimpColorAttribute(int channel, GXVertexAttribute colorAttrib, Assimp.Mesh mesh)
        {
            List<Color> tempList = new List<Color>();

            for (int col = 0; col < mesh.VertexColorChannels[channel].Count; col++)
                tempList.Add(mesh.VertexColorChannels[channel][col].ToSuperBMDColorRGBA());

            if (!Attributes.CheckAttribute(colorAttrib))
                Attributes.SetAttributeData(colorAttrib, tempList);
            else
            {
                List<Color> attribData = (List<Color>)Attributes.GetAttributeData(colorAttrib);

                foreach (Color col in tempList)
                {
                    if (!attribData.Contains(col))
                        attribData.Add(col);
                }

                Attributes.SetAttributeData(colorAttrib, attribData);
            }
        }

        private void SetAssimpTexCoordAttribute(int channel, GXVertexAttribute texCoordAttrib, Assimp.Mesh mesh)
        {
            List<Vector2> tempList = new List<Vector2>();

            for (int vec = 0; vec < mesh.TextureCoordinateChannels[channel].Count; vec++)
            {
                Vector2 tempCoord = mesh.TextureCoordinateChannels[channel][vec].ToOpenTKVector2();
                tempList.Add(new Vector2(tempCoord.X, 1.0f - tempCoord.Y));
            }

            if (!Attributes.CheckAttribute(texCoordAttrib))
                Attributes.SetAttributeData(texCoordAttrib, tempList);
            else
            {
                List<Vector2> attribData = (List<Vector2>)Attributes.GetAttributeData(texCoordAttrib);

                foreach (Vector2 vec in tempList)
                {
                    if (!attribData.Contains(vec))
                        attribData.Add(vec);
                }

                Attributes.SetAttributeData(texCoordAttrib, attribData);
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            long start = writer.BaseStream.Position;

            writer.Write("VTX1".ToCharArray());
            writer.Write(0); // Placeholder for section size
            writer.Write(0x40); // Offset to attribute data

            for (int i = 0; i < 13; i++) // Placeholders for attribute data offsets
                writer.Write(0);

            WriteAttributeHeaders(writer);

            StreamUtility.PadStreamWithString(writer, 32);

            WriteAttributeData(writer, (int)start);

            long end = writer.BaseStream.Position;
            long length = (end - start);

            writer.Seek((int)start + 4, System.IO.SeekOrigin.Begin);
            writer.Write((int)length);
            writer.Seek((int)end, System.IO.SeekOrigin.Begin);
        }

        private void WriteAttributeHeaders(EndianBinaryWriter writer)
        {
            foreach (GXVertexAttribute attrib in Enum.GetValues(typeof(GXVertexAttribute)))
            {
                if (!Attributes.CheckAttribute(attrib) || attrib == GXVertexAttribute.PositionMatrixIdx)
                    continue;

                writer.Write((int)attrib);

                switch (attrib)
                {
                    case GXVertexAttribute.PositionMatrixIdx:
                        break;
                    case GXVertexAttribute.Position:
                        writer.Write(1);
                        writer.Write((int)StorageFormats[attrib].Item1);
                        writer.Write(StorageFormats[attrib].Item2);
                        writer.Write((sbyte)-1);
                        writer.Write((short)-1);
                        break;
                    case GXVertexAttribute.Normal:
                        writer.Write(0);
                        writer.Write((int)StorageFormats[attrib].Item1);
                        writer.Write(StorageFormats[attrib].Item2);
                        writer.Write((sbyte)-1);
                        writer.Write((short)-1);
                        break;
                    case GXVertexAttribute.Color0:
                    case GXVertexAttribute.Color1:
                        writer.Write(1);
                        writer.Write((int)StorageFormats[attrib].Item1);
                        writer.Write(StorageFormats[attrib].Item2);
                        writer.Write((sbyte)-1);
                        writer.Write((short)-1);
                        break;
                    case GXVertexAttribute.Tex0:
                    case GXVertexAttribute.Tex1:
                    case GXVertexAttribute.Tex2:
                    case GXVertexAttribute.Tex3:
                    case GXVertexAttribute.Tex4:
                    case GXVertexAttribute.Tex5:
                    case GXVertexAttribute.Tex6:
                    case GXVertexAttribute.Tex7:
                        writer.Write(1);
                        writer.Write((int)StorageFormats[attrib].Item1);
                        writer.Write(StorageFormats[attrib].Item2);
                        writer.Write((sbyte)-1);
                        writer.Write((short)-1);
                        break;
                }
            }

            writer.Write(255);
            writer.Write(1);
            writer.Write(0);
            writer.Write((byte)0);
            writer.Write((sbyte)-1);
            writer.Write((short)-1);
        }

        private void WriteAttributeData(EndianBinaryWriter writer, int baseOffset)
        {
            foreach (GXVertexAttribute attrib in Enum.GetValues(typeof(GXVertexAttribute)))
            {
                if (!Attributes.CheckAttribute(attrib) || attrib == GXVertexAttribute.PositionMatrixIdx)
                    continue;

                long endOffset = writer.BaseStream.Position;

                switch (attrib)
                {
                    case GXVertexAttribute.Position:
                        writer.Seek(baseOffset + 0x0C, System.IO.SeekOrigin.Begin);
                        writer.Write((int)(writer.BaseStream.Length - baseOffset));
                        writer.Seek((int)endOffset, System.IO.SeekOrigin.Begin);

                        foreach (Vector3 posVec in (List<Vector3>)Attributes.GetAttributeData(attrib))
                        {
                            switch (StorageFormats[attrib].Item1)
                            {
                                case GXDataType.Unsigned8:
                                    writer.Write((byte)Math.Round(posVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((byte)Math.Round(posVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((byte)Math.Round(posVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Signed8:
                                    writer.Write((sbyte)Math.Round(posVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((sbyte)Math.Round(posVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((sbyte)Math.Round(posVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Unsigned16:
                                    writer.Write((ushort)Math.Round(posVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((ushort)Math.Round(posVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((ushort)Math.Round(posVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Signed16:
                                    writer.Write((short)Math.Round(posVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((short)Math.Round(posVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((short)Math.Round(posVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Float32:
                                    writer.Write(posVec);
                                    break;
                            }
                        }
                        break;
                    case GXVertexAttribute.Normal:
                        writer.Seek(baseOffset + 0x10, System.IO.SeekOrigin.Begin);
                        writer.Write((int)(writer.BaseStream.Length - baseOffset));
                        writer.Seek((int)endOffset, System.IO.SeekOrigin.Begin);

                        foreach (Vector3 normVec in Attributes.Normals)
                        {
                            switch (StorageFormats[attrib].Item1)
                            {
                                case GXDataType.Unsigned8:
                                    writer.Write((byte)Math.Round(normVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((byte)Math.Round(normVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((byte)Math.Round(normVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Signed8:
                                    writer.Write((sbyte)Math.Round(normVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((sbyte)Math.Round(normVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((sbyte)Math.Round(normVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Unsigned16:
                                    writer.Write((ushort)Math.Round(normVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((ushort)Math.Round(normVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((ushort)Math.Round(normVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Signed16:
                                    writer.Write((short)Math.Round(normVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((short)Math.Round(normVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((short)Math.Round(normVec.Z * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Float32:
                                    writer.Write(normVec);
                                    break;
                            }
                        }
                        break;
                    case GXVertexAttribute.Color0:
                    case GXVertexAttribute.Color1:
                        writer.Seek(baseOffset + 0x18 + (int)(attrib - 11) * 4, System.IO.SeekOrigin.Begin);
                        writer.Write((int)(writer.BaseStream.Length - baseOffset));
                        writer.Seek((int)endOffset, System.IO.SeekOrigin.Begin);

                        foreach (Color col in (List<Color>)Attributes.GetAttributeData(attrib))
                            writer.Write(col);
                        break;
                    case GXVertexAttribute.Tex0:
                    case GXVertexAttribute.Tex1:
                    case GXVertexAttribute.Tex2:
                    case GXVertexAttribute.Tex3:
                    case GXVertexAttribute.Tex4:
                    case GXVertexAttribute.Tex5:
                    case GXVertexAttribute.Tex6:
                    case GXVertexAttribute.Tex7:
                        writer.Seek(baseOffset + 0x20 + (int)(attrib - 13) * 4, System.IO.SeekOrigin.Begin);
                        writer.Write((int)(writer.BaseStream.Length - baseOffset));
                        writer.Seek((int)endOffset, System.IO.SeekOrigin.Begin);

                        foreach (Vector2 texVec in (List<Vector2>)Attributes.GetAttributeData(attrib))
                        {
                            switch (StorageFormats[attrib].Item1)
                            {
                                case GXDataType.Unsigned8:
                                    writer.Write((byte)Math.Round(texVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((byte)Math.Round(texVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Signed8:
                                    writer.Write((sbyte)Math.Round(texVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((sbyte)Math.Round(texVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Unsigned16:
                                    writer.Write((ushort)Math.Round(texVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((ushort)Math.Round(texVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Signed16:
                                    writer.Write((short)Math.Round(texVec.X * (1 << StorageFormats[attrib].Item2)));
                                    writer.Write((short)Math.Round(texVec.Y * (1 << StorageFormats[attrib].Item2)));
                                    break;
                                case GXDataType.Float32:
                                    writer.Write(texVec);
                                    break;
                            }
                        }
                        break;
                }

                StreamUtility.PadStreamWithString(writer, 32);
            }
        }
        public void NormalsSwapYZ() {
            for (int i = 0; i < Attributes.Normals.Count; i++) {
                Vector3 normal = Attributes.Normals[i];

                float tmp = normal.Y;
                //normal.Y = normal.Z;
                //normal.Z = tmp;
                
                // X Y -1 not good?
                // Y Z -1 not good?
                // Y must be -1

                //normal.X *= -1;
                //normal.Y *= -1;
                //normal.Z *= -1;


                Attributes.Normals[i] = normal;
            }
        }
    }
}
