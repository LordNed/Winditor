using GameFormatReader.Common;
using System.Diagnostics;
using System.Collections.Generic;
using WindEditor;
using System.ComponentModel;
using J3DRenderer.Framework;

namespace J3DRenderer.JStudio
{
    public class Material
    {
        public string Name { get; internal set; }
        public Shader Shader { get; internal set; }
        public VertexDescription VtxDesc { get; internal set; }

        public byte Flag { get; internal set; }
        public byte CullModeIndex { get; internal set; }
        public byte NumChannelControlsIndex { get; internal set; }
        public byte NumTexGensIndex { get; internal set; }
        public byte NumTevStagesIndex { get; internal set; }
        public byte ZCompLocIndex { get; internal set; }
        public byte ZModeIndex { get; internal set; }
        public byte DitherIndex { get; internal set; }
        public short[] MaterialColorIndexes { get; internal set; }
        public short[] ColorChannelControlIndexes { get; internal set; }
        public short[] AmbientColorIndexes { get; internal set; }
        public short[] LightingColorIndexes { get; internal set; }
        public short[] TexGenInfoIndexes { get; internal set; }
        public short[] TexGen2InfoIndexes { get; internal set; }
        public short[] TexMatrixIndexes { get; internal set; }
        public short[] DttMatrixIndexes { get; internal set; }
        public short[] TextureIndexes { get; internal set; }
        public short[] TevKonstColorIndexes { get; internal set; }
        public byte[] KonstColorSelectorIndexes { get; internal set; }
        public byte[] KonstAlphaSelectorIndexes { get; internal set; }
        public short[] TevOrderInfoIndexes { get; internal set; }
        public short[] TevColorIndexes { get; internal set; }
        public short[] TevStageInfoIndexes { get; internal set; }
        public short[] TevSwapModeIndexes { get; internal set; }
        public short[] TevSwapModeTableIndexes { get; internal set; }
        public short[] UnknownIndexes { get; internal set; }
        public short FogModeIndex { get; internal set; }
        public short AlphaCompareIndex { get; internal set; }
        public short BlendModeIndex { get; internal set; }
        public short UnknownIndex2 { get; internal set; }

        public void Bind()
        {
            Shader.Bind();
        }
    }

    public class MAT3
    {
        public BindingList<Material> MaterialList { get; protected set; }
        public List<short> MaterialRemapTable { get; protected set; }
        public List<IndirectTexture> IndirectTextures { get; protected set; }
        public List<int> CullModes { get; protected set; }
        public List<WLinearColor> MaterialColors { get; protected set; }
        public List<byte> NumChannelControls { get; protected set; }
        public List<ColorChannelControl> ColorChannelControls { get; protected set; }
        public List<WLinearColor> AmbientColors { get; protected set; }
        public List<WLinearColor> LightingColors { get; protected set; }
        public List<byte> NumTexGens { get; protected set; }
        public List<TexCoordGen> TexGenInfos { get; protected set; }
        public List<TexCoordGen> TexGen2Infos { get; protected set; }
        public List<TexMatrix> TexMatrixInfos { get; protected set; }
        public List<TexMatrix> TexMatrix2Infos { get; protected set; }
        public List<short> TextureIndexes { get; protected set; }
        public List<TevOrder> TevOrderInfos { get; protected set; }
        public List<WLinearColor> TevColors { get; protected set; }
        public List<WLinearColor> TevKonstColors { get; protected set; }
        public List<byte> NumTevStages { get; protected set; }
        public List<TevStage> TevStageInfos { get; protected set; }
        public List<TevSwapMode> TevSwapModeInfos { get; protected set; }
        public List<TevSwapModeTable> TevSwapModeTables { get; protected set; }
        public List<FogInfo> FogInfos { get; protected set; }
        public List<AlphaCompare> AlphaCompares { get; protected set; }
        public List<BlendMode> BlendModeInfos { get; protected set; }
        public List<ZMode> ZModeInfos { get; protected set; }
        public List<bool> ZCompareLocInfo { get; protected set; }
        public List<bool> DitherInfos { get; protected set; }
        public List<NBTScale> NBTScale { get; protected set; }
        

        /// <summary> Delegate defines a function that decodes one instance of type T.</summary>
        /// <param name="stream">The stream to decode the instance from</param>
        private delegate T LoadTypeFromStream<T>(EndianBinaryReader stream);

        internal void LoadMAT3FromStream(EndianBinaryReader reader, long chunkStart, int chunkSize)
        {
            short materialCount = reader.ReadInt16();
            Trace.Assert(reader.ReadUInt16() == 0xFFFF); // Padding

            MaterialList = new BindingList<Material>();

            // Nintendo sets unused offsets to zero, so we can't just use the next variable name in the list. Instead we have to search
            // until we find a non-zero one and calculate the difference that way. Thus, we convert all of the offsets into an int[] for
            // array operations.
            int[] offsets = new int[30];
            for (int i = 0; i < offsets.Length; i++)
                offsets[i] = reader.ReadInt32();

            /* MATERIAL REMAP TABLE (See start of Material loader below) */
            MaterialRemapTable = ReadSection<short>(reader, chunkStart, chunkSize, offsets, 1, ReadShort, 2);

            /* STRING TABLE */
            reader.BaseStream.Position = chunkStart + offsets[2];
            StringTable nameTable = StringTable.FromStream(reader);

            /* INDIRECT TEXTURING */
            IndirectTextures = ReadSection<IndirectTexture>(reader, chunkStart, chunkSize, offsets, 3, ReadIndirectTexture, 312);

            /* CULL MODE */
            CullModes = ReadSection<int>(reader, chunkStart, chunkSize, offsets, 4, ReadInt32, 4);

            /* MATERIAL COLOR */
            MaterialColors = ReadSection<WLinearColor>(reader, chunkStart, chunkSize, offsets, 5, ReadColor32, 4);

            /* NUM COLOR CHAN */
            // THIS IS A GUESS AT DATA TYPE
            NumChannelControls = ReadSection<byte>(reader, chunkStart, chunkSize, offsets, 6, ReadByte, 1);

            /* COLOR CHAN INFO */
            ColorChannelControls = ReadSection<ColorChannelControl>(reader, chunkStart, chunkSize, offsets, 7, ReadChannelControl, 8);

            /* AMBIENT COLOR */
            AmbientColors = ReadSection<WLinearColor>(reader, chunkStart, chunkSize, offsets, 8, ReadColor32, 4);

            /* LIGHT INFO */
            // THIS IS A GUESS AT DATA TYPE
            LightingColors = ReadSection<WLinearColor>(reader, chunkStart, chunkSize, offsets, 9, ReadColorShort, 8);

            /* TEX GEN NUMBER */
            NumTexGens = ReadSection<byte>(reader, chunkStart, chunkSize, offsets, 10, ReadByte, 1);

            /* TEX GEN INFO */
            TexGenInfos = ReadSection<TexCoordGen>(reader, chunkStart, chunkSize, offsets, 11, ReadTexCoordGen, 4);

            /* TEX GEN 2 INFO */
            TexGen2Infos = ReadSection<TexCoordGen>(reader, chunkStart, chunkSize, offsets, 12, ReadTexCoordGen, 4);

            /* TEX MATRIX INFO */
            TexMatrixInfos = ReadSection<TexMatrix>(reader, chunkStart, chunkSize, offsets, 13, ReadTexMatrix, 100);

            /* POST TRANSFORM MATRIX INFO */
            TexMatrix2Infos = ReadSection<TexMatrix>(reader, chunkStart, chunkSize, offsets, 14, ReadTexMatrix, 100);

            /* TEXURE INDEX */
            TextureIndexes = ReadSection<short>(reader, chunkStart, chunkSize, offsets, 15, ReadShort, 2);

            /* TEV ORDER INFO */
            TevOrderInfos = ReadSection<TevOrder>(reader, chunkStart, chunkSize, offsets, 16, ReadTevOrder, 4);

            /* TEV COLORS */
            TevColors = ReadSection<WLinearColor>(reader, chunkStart, chunkSize, offsets, 17, ReadColorShort, 8);

            /* TEV KONST COLORS */
            TevKonstColors = ReadSection<WLinearColor>(reader, chunkStart, chunkSize, offsets, 18, ReadColor32, 4);

            /* NUM TEV STAGES */
            // THIS IS A GUESS AT DATA TYPE
            NumTevStages = ReadSection<byte>(reader, chunkStart, chunkSize, offsets, 19, ReadByte, 1);

            /* TEV STAGE INFO */
            TevStageInfos = ReadSection<TevStage>(reader, chunkStart, chunkSize, offsets, 20, ReadTevCombinerStage, 20);

            /* TEV SWAP MODE INFO */
            TevSwapModeInfos = ReadSection<TevSwapMode>(reader, chunkStart, chunkSize, offsets, 21, ReadTevSwapMode, 4);

            /* TEV SWAP MODE TABLE INFO */
            TevSwapModeTables = ReadSection<TevSwapModeTable>(reader, chunkStart, chunkSize, offsets, 22, ReadTevSwapModeTable, 4);

            /* FOG INFO */
            FogInfos = ReadSection<FogInfo>(reader, chunkStart, chunkSize, offsets, 23, ReadFogInfo, 44);

            /* ALPHA COMPARE INFO */
            AlphaCompares = ReadSection<AlphaCompare>(reader, chunkStart, chunkSize, offsets, 24, ReadAlphaCompare, 8);

            /* BLEND INFO */
            BlendModeInfos = ReadSection<BlendMode>(reader, chunkStart, chunkSize, offsets, 25, ReadBlendMode, 4);

            /* ZMODE INFO */
            ZModeInfos = ReadSection<ZMode>(reader, chunkStart, chunkSize, offsets, 26, ReadZMode, 4);

            /* ZCOMP LOC INFO */
            // THIS IS A GUESS AT DATA TYPE
            ZCompareLocInfo = ReadSection<bool>(reader, chunkStart, chunkSize, offsets, 27, ReadBool, 1);

            /* DITHER INFO */
            // THIS IS A GUESS AT DATA TYPE
            DitherInfos = ReadSection<bool>(reader, chunkStart, chunkSize, offsets, 28, ReadBool, 1);

            /* NBT SCALE INFO */
            NBTScale = ReadSection<NBTScale>(reader, chunkStart, chunkSize, offsets, 29, ReadNBTScale, 16);


            for (int m = 0; m < materialCount; m++)
            {
                // A Material entry is 0x14c long.
                reader.BaseStream.Position = chunkStart + offsets[0] + (m * 0x14c);

                // The first byte of a material is some form of flag. Values found so far are 1, 4 and 0. 1 is the most common.
                // bmdview2 documentation says that means "draw on way down" while 4 means "draw on way up" (of INF1 heirarchy)
                // However, none of the documentation seems to mention type 0 - if the value is 0, it seems to be some junk/EOF
                // marker for the material section. On some files (not all) there will be say, 12 materials, but the highest index
                // in the material remap table only goes up to 10 (so the 11th material) and the 12th will never be referenced. However
                // if we read it like we do here with a for loop, we'll hit that one and try to parse all the indexes and it'll just all
                // around kind of explode.
                //
                // To resolve this, we'll check if the flag value is zero - if so, skip creating a material for it.

                byte flag = reader.ReadByte();
                if (flag == 0)
                    continue;

                // Now that we've read the contents of the material section, we can load their values into a material 
                // class which keeps it nice and tidy and full of class references and not indexes.
                Material material = new Material();
                MaterialList.Add(material);

                material.Name = nameTable[m];
                material.Flag = flag;
                material.CullModeIndex = reader.ReadByte();
                material.NumChannelControlsIndex = reader.ReadByte();
                material.NumTexGensIndex = reader.ReadByte();
                material.NumTevStagesIndex = reader.ReadByte();
                material.ZCompLocIndex = reader.ReadByte();
                material.ZModeIndex = reader.ReadByte();
                material.DitherIndex = reader.ReadByte();

                // Not sure what these materials are used for. gxColorMaterial is the function that reads them.
                material.MaterialColorIndexes = new short[2];
                for (int i = 0; i < material.MaterialColorIndexes.Length; i++)
                    material.MaterialColorIndexes[i] = reader.ReadInt16();

                material.ColorChannelControlIndexes = new short[4];
                for (int i = 0; i < material.ColorChannelControlIndexes.Length; i++)
                    material.ColorChannelControlIndexes[i] = reader.ReadInt16();

                material.AmbientColorIndexes = new short[2];
                for (int i = 0; i < material.AmbientColorIndexes.Length; i++)
                    material.AmbientColorIndexes[i] = reader.ReadInt16();

                material.LightingColorIndexes = new short[8];
                for (int i = 0; i < material.LightingColorIndexes.Length; i++)
                    material.LightingColorIndexes[i] = reader.ReadInt16();

                material.TexGenInfoIndexes = new short[8];
                for (int i = 0; i < material.TexGenInfoIndexes.Length; i++)

                        material.TexGenInfoIndexes[i] = reader.ReadInt16();

                material.TexGen2InfoIndexes = new short[8];
                for (int i = 0; i < material.TexGen2InfoIndexes.Length; i++)
                    material.TexGen2InfoIndexes[i] = reader.ReadInt16();

                material.TexMatrixIndexes = new short[10];
                for (int i = 0; i < material.TexMatrixIndexes.Length; i++)
                        material.TexMatrixIndexes[i] = reader.ReadInt16();

                material.DttMatrixIndexes = new short[20];
                for (int i = 0; i < material.DttMatrixIndexes.Length; i++)
                    material.DttMatrixIndexes[i] = reader.ReadInt16();

                material.TextureIndexes = new short[8];
                for (int i = 0; i < material.TextureIndexes.Length; i++)
                    material.TextureIndexes[i] = reader.ReadInt16();

                material.TevKonstColorIndexes = new short[4];
                for (int i = 0; i < material.TevKonstColorIndexes.Length; i++)
                    material.TevKonstColorIndexes[i] = reader.ReadInt16();

                // Guessing that this one doesn't index anything else as it's just an enum value and there doesn't seem to be an offset for it in the header.
                material.KonstColorSelectorIndexes = new byte[16];
                for (int i = 0; i < material.KonstColorSelectorIndexes.Length; i++)
                    material.KonstColorSelectorIndexes[i] = reader.ReadByte();

                // Guessing that this one doesn't index anything else as it's just an enum value and there doesn't seem to be an offset for it in the header.
                material.KonstAlphaSelectorIndexes = new byte[16];
                for (int i = 0; i < material.KonstAlphaSelectorIndexes.Length; i++)
                    material.KonstAlphaSelectorIndexes[i] = reader.ReadByte();

                material.TevOrderInfoIndexes = new short[16];
                for (int i = 0; i < material.TevOrderInfoIndexes.Length; i++)
                    material.TevOrderInfoIndexes[i] = reader.ReadInt16();

                material.TevColorIndexes = new short[4];
                for (int i = 0; i < material.TevColorIndexes.Length; i++)
                    material.TevColorIndexes[i] = reader.ReadInt16();

                material.TevStageInfoIndexes = new short[16];
                for (int i = 0; i < material.TevStageInfoIndexes.Length; i++)
                    material.TevStageInfoIndexes[i] = reader.ReadInt16();

                material.TevSwapModeIndexes = new short[16];
                for (int i = 0; i < material.TevSwapModeIndexes.Length; i++)
                    material.TevSwapModeIndexes[i] = reader.ReadInt16();

                material.TevSwapModeTableIndexes = new short[4];
                for (int i = 0; i < material.TevSwapModeTableIndexes.Length; i++)
                    material.TevSwapModeTableIndexes[i] = reader.ReadInt16();

                material.UnknownIndexes = new short[12];
                for (int l = 0; l < material.UnknownIndexes.Length; l++)
                    material.UnknownIndexes[l] = reader.ReadInt16();

                material.FogModeIndex = reader.ReadInt16();
                material.AlphaCompareIndex = reader.ReadInt16();
                material.BlendModeIndex = reader.ReadInt16();
                material.UnknownIndex2 = reader.ReadInt16();
            }


            // Now, generate a shader from the data.
            foreach (var material in MaterialList)
                material.Shader = TEVShaderGenerator.GenerateShader(material, this);
        }

        private static List<T> Collect<T>(EndianBinaryReader stream, LoadTypeFromStream<T> function, int count)
        {
            List<T> values = new List<T>();
            for (int i = 0; i < count; i++)
            {
                values.Add(function(stream));
            }

            return values;
        }

        private static List<T> ReadSection<T>(EndianBinaryReader stream, long chunkStart, int chunkSize, int[] offsets, int offset, LoadTypeFromStream<T> function, int itemSize)
        {
            // If there's none of this value, early out and return an empty list, otherwise it parses
            // everything from the chunk start up to the next one as the requested section.
            if (offsets[offset] == 0)
                return new List<T>();

            stream.BaseStream.Position = chunkStart + offsets[offset];
            return Collect<T>(stream, function, GetOffsetLength(offsets, offset, chunkSize) / itemSize);
        }

        #region Stream Decoding Functions
        private static WLinearColor ReadColor32(EndianBinaryReader stream)
        {
            return new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte() / 255f);
        }

        private static WLinearColor ReadColorShort(EndianBinaryReader stream)
        {
            // ToDo: Are these actually just divided by 255f? Wouldn't they be divided by short.MaxValue?
            ushort r = stream.ReadUInt16();
            ushort g = stream.ReadUInt16();
            ushort b = stream.ReadUInt16();
            ushort a = stream.ReadUInt16();
            Trace.Assert(r <= 255 && g <= 255 && b <= 255 && a <= 255);
            return new WLinearColor(r / 255f, g / 255f, b / 255f, a/255f);
        }

        private static IndirectTexture ReadIndirectTexture(EndianBinaryReader stream)
        {
            IndirectTexture itm = new IndirectTexture();
            itm.HasLookup = stream.ReadBoolean();
            itm.IndTexStageNum = stream.ReadByte();
            ushort val = stream.ReadUInt16();
            //Trace.Assert(stream.ReadUInt16() == 0xFFFF); // Padding
            itm.Unknown1 = stream.ReadByte();
            itm.Unknown2 = stream.ReadByte();

            for (int i = 0; i < 7; i++)
            {
                Trace.Assert(stream.ReadUInt16() == 0xFFFF); // Padding
            }

            for (int i = 0; i < 3; i++)
            {
                itm.Matrices[i] = new IndirectTextureMatrix(new OpenTK.Matrix2x3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()), stream.ReadByte());
                Trace.Assert(stream.ReadByte() == 0xFF); // Padding
                Trace.Assert(stream.ReadByte() == 0xFF);
                Trace.Assert(stream.ReadByte() == 0xFF);
            }

            for (int i = 0; i < 4; i++)
            {
                itm.Scales[i] = new IndirectTextureScale(stream.ReadByte(), stream.ReadByte());
                Trace.Assert(stream.ReadByte() == 0xFF); // Padding
                Trace.Assert(stream.ReadByte() == 0xFF);
            }

            for (int i = 0; i < 16; i++)
            {
                var indirectTevOrder = new IndirectTevOrder();
                indirectTevOrder.TevStageID = stream.ReadByte();
                indirectTevOrder.IndTexFormat = stream.ReadByte();
                indirectTevOrder.IndTexBiasSel = stream.ReadByte();
                indirectTevOrder.IndTexMtxId = stream.ReadByte();
                indirectTevOrder.IndTexWrapS = stream.ReadByte();
                indirectTevOrder.IndTexWrapT = stream.ReadByte();
                indirectTevOrder.AddPrev = stream.ReadBoolean();
                indirectTevOrder.UtcLod = stream.ReadBoolean();
                indirectTevOrder.AlphaSel = stream.ReadByte();
                Trace.Assert(stream.ReadByte() == 0xFF); // Padding
                Trace.Assert(stream.ReadByte() == 0xFF);
                Trace.Assert(stream.ReadByte() == 0xFF);

                itm.TevOrders[i] = indirectTevOrder;
            }

            return itm;
        }

        private static NBTScale ReadNBTScale(EndianBinaryReader stream)
        {
            var nbtScale = new NBTScale();
            nbtScale.Unknown1 = stream.ReadByte();
            Trace.Assert(stream.ReadByte() == 0xFF); // Padding
            Trace.Assert(stream.ReadByte() == 0xFF); 
            Trace.Assert(stream.ReadByte() == 0xFF);
            nbtScale.Scale = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
            return nbtScale;
        }

        private static ZMode ReadZMode(EndianBinaryReader stream)
        {
            var retVal = new ZMode
            {
                Enable = stream.ReadBoolean(),
                Function = (GXCompareType)stream.ReadByte(),
                UpdateEnable = stream.ReadBoolean(),
            };

            Trace.Assert(stream.ReadByte() == 0xFF); // Padding
            return retVal;
        }

        private static AlphaCompare ReadAlphaCompare(EndianBinaryReader stream)
        {
            var retVal = new AlphaCompare
            {
                Comp0 = (GXCompareType)stream.ReadByte(),
                Reference0 = stream.ReadByte(),
                Operation = (GXAlphaOp)stream.ReadByte(),
                Comp1 = (GXCompareType)stream.ReadByte(),
                Reference1 = stream.ReadByte()
            };

            Trace.Assert(stream.ReadByte() == 0xFF); // Padding
            Trace.Assert(stream.ReadByte() == 0xFF); // Padding
            Trace.Assert(stream.ReadByte() == 0xFF); // Padding
            return retVal;
        }

        private static BlendMode ReadBlendMode(EndianBinaryReader stream)
        {
            return new BlendMode
            {
                Type = (GXBlendMode)stream.ReadByte(),
                SourceFact = (GXBlendModeControl)stream.ReadByte(),
                DestinationFact = (GXBlendModeControl)stream.ReadByte(),
                Operation = (GXLogicOp)stream.ReadByte()
            };
        }

        private static ColorChannelControl ReadChannelControl(EndianBinaryReader stream)
        {
            var retVal = new ColorChannelControl
            {
                LightingEnabled = stream.ReadBoolean(),
                MaterialSrc = (GXColorSrc)stream.ReadByte(),
                LitMask = (GXLightId)stream.ReadByte(),
                DiffuseFunction = (GXDiffuseFn)stream.ReadByte(),
                AttenuationFunction = (GXAttenuationFn)stream.ReadByte(),
                AmbientSrc = (GXColorSrc)stream.ReadByte()
            };

            Trace.Assert(stream.ReadUInt16() == 0xFFFF); // Padding
            return retVal;
        }

        private static TexCoordGen ReadTexCoordGen(EndianBinaryReader stream)
        {
            var retVal = new TexCoordGen
            {
                Type = (GXTexGenType)stream.ReadByte(),
                Source = (GXTexGenSrc)stream.ReadByte(),
                TexMatrixSource = (GXTexMatrix)stream.ReadByte()
            };

            Trace.Assert(stream.ReadByte() == 0xFF); // Padding
            return retVal;
        }

        private static TexMatrix ReadTexMatrix(EndianBinaryReader stream)
        {
            var retVal = new TexMatrix();
            retVal.Projection = stream.ReadByte();
            retVal.Type = stream.ReadByte();
            Trace.Assert(stream.ReadUInt16() == 0xFFFF); // Padding
            retVal.CenterS = stream.ReadSingle();
            retVal.CenterT = stream.ReadSingle();
            retVal.Unknown0 = stream.ReadSingle();
            retVal.ScaleS = stream.ReadSingle();
            retVal.ScaleT = stream.ReadSingle();
            retVal.Rotation = stream.ReadInt16() * (180 / 32768f);
            Trace.Assert(stream.ReadUInt16() == 0xFFFF); // Padding
            retVal.TranslateS = stream.ReadSingle();
            retVal.TranslateT = stream.ReadSingle();
            retVal.PreMatrix = new float[4, 4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    retVal.PreMatrix[x, y] = stream.ReadSingle();
                }
            }

            return retVal;
        }

        private static TevIn ReadTevIn(EndianBinaryReader stream)
        {
            return new TevIn { A = stream.ReadByte(), B = stream.ReadByte(), C = stream.ReadByte(), D = stream.ReadByte() };
        }

        private static TevOp ReadTevOp(EndianBinaryReader stream)
        {
            return new TevOp
            {
                Operation = stream.ReadByte(),
                Bias = stream.ReadByte(),
                Scale = stream.ReadByte(),
                Clamp = stream.ReadByte(),
                Out = stream.ReadByte()
            };
        }

        private static TevOrder ReadTevOrder(EndianBinaryReader stream)
        {
            var retVal = new TevOrder
            {
                TexCoordId = (GXTexCoordSlot)stream.ReadByte(),
                TexMap = stream.ReadByte(),
                ChannelId = (GXColorChannelId)stream.ReadByte()
            };

            Trace.Assert(stream.ReadByte() == 0xFF); // Padding
            return retVal;
        }

        private static TevStage ReadTevCombinerStage(EndianBinaryReader stream)
        {
            var retVal = new TevStage();
            retVal.Unknown0 = stream.ReadByte();
            for (int i = 0; i < 4; i++)
                retVal.ColorIn[i] = (GXCombineColorInput)stream.ReadByte();
            retVal.ColorOp = (GXTevOp)stream.ReadByte();
            retVal.ColorBias = (GXTevBias)stream.ReadByte();
            retVal.ColorScale = (GXTevScale)stream.ReadByte();
            retVal.ColorClamp = stream.ReadBoolean();
            retVal.ColorRegId = stream.ReadByte();
            for (int i = 0; i < 4; i++)
                retVal.AlphaIn[i] = (GXCombineAlphaInput)stream.ReadByte();
            retVal.AlphaOp = (GXTevOp)stream.ReadByte();
            retVal.AlphaBias = (GXTevBias)stream.ReadByte();
            retVal.AlphaScale = (GXTevScale)stream.ReadByte();
            retVal.AlphaClamp = stream.ReadBoolean();
            retVal.AlphaRegId = stream.ReadByte();
            retVal.Unknown1 = stream.ReadByte();

            Trace.Assert(retVal.Unknown0 == 0xFF);
            Trace.Assert(retVal.Unknown1 == 0xFF);
            return retVal;
        }

        private static TevSwapMode ReadTevSwapMode(EndianBinaryReader stream)
        {
            var retVal = new TevSwapMode
            {
                RasSel = stream.ReadByte(),
                TexSel = stream.ReadByte()
            };

            Trace.Assert(stream.ReadUInt16() == 0xFFFF); // Padding
            return retVal;
        }

        private static TevSwapModeTable ReadTevSwapModeTable(EndianBinaryReader stream)
        {
            return new TevSwapModeTable
            {
                R = stream.ReadByte(),
                G = stream.ReadByte(),
                B = stream.ReadByte(),
                A = stream.ReadByte()
            };
        }

        private static FogInfo ReadFogInfo(EndianBinaryReader stream)
        {
            var retVal = new FogInfo();

            retVal.Type = stream.ReadByte();
            retVal.Enable = stream.ReadBoolean();
            retVal.Center = stream.ReadUInt16();
            retVal.StartZ = stream.ReadSingle();
            retVal.EndZ = stream.ReadSingle();
            retVal.NearZ = stream.ReadSingle();
            retVal.FarZ = stream.ReadSingle();
            retVal.Color = ReadColor32(stream);
            retVal.Table = new ushort[10];
            for (int i = 0; i < retVal.Table.Length; i++)
                retVal.Table[i] = stream.ReadUInt16();

            return retVal;
        }

        private static int ReadInt32(EndianBinaryReader stream)
        {
            return stream.ReadInt32();
        }

        private static byte ReadByte(EndianBinaryReader stream)
        {
            return stream.ReadByte();
        }

        private static short ReadShort(EndianBinaryReader stream)
        {
            return stream.ReadInt16();
        }

        private static bool ReadBool(EndianBinaryReader stream)
        {
            return stream.ReadBoolean();
        }
        #endregion

        private static int GetOffsetLength(int[] dataOffsets, int currentIndex, int endChunkOffset)
        {
            int currentOffset = dataOffsets[currentIndex];

            // Find the next available offset in the array, and subtract the two offsets to get the length of the data.
            for (int i = currentIndex + 1; i < dataOffsets.Length; i++)
            {
                if (dataOffsets[i] != 0)
                {
                    return dataOffsets[i] - currentOffset;
                }
            }

            // If we didn't find a dataOffset that was valid, then we go to the end of the chunk.
            return endChunkOffset - currentOffset;
        }
    }
}
