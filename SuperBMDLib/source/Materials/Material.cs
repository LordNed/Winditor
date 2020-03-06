using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Materials.Enums;
using SuperBMDLib.Util;
using OpenTK;
using Newtonsoft.Json;

namespace SuperBMDLib.Materials
{
    public class Material
    {
        public string Name;
        public byte Flag;
        [JsonIgnore]
        public byte ColorChannelControlsCount;
        [JsonIgnore]
        public byte NumTexGensCount;
        [JsonIgnore]
        public byte NumTevStagesCount;

        public CullMode CullMode;
        public bool ZCompLoc;
        public bool Dither;

        [JsonIgnore]
        public int[] TextureIndices;
        public string[] TextureNames;

        public IndirectTexturing IndTexEntry;
        public Color?[] MaterialColors;
        public ChannelControl?[] ChannelControls;
        public Color?[] AmbientColors;
        public Color?[] LightingColors;
        public TexCoordGen?[] TexCoord1Gens;
        public TexCoordGen?[] PostTexCoordGens;
        public TexMatrix?[] TexMatrix1;
        public TexMatrix?[] PostTexMatrix;
        public TevOrder?[] TevOrders;
        public KonstColorSel[] ColorSels;
        public KonstAlphaSel[] AlphaSels;
        public Color?[] TevColors;
        public Color?[] KonstColors;
        public TevStage?[] TevStages;
        public TevSwapMode?[] SwapModes;
        public TevSwapModeTable?[] SwapTables;

        public Fog FogInfo;
        public AlphaCompare AlphCompare;
        public BlendMode BMode;
        public ZMode ZMode;
        public NBTScale NBTScale;

        public Material()
        {
            MaterialColors = new Color?[2] { new Color(1, 1, 1, 1), null};

            ChannelControls = new ChannelControl?[4];

            IndTexEntry = new IndirectTexturing();

            AmbientColors = new Color?[2] { new Color(50f/255f, 50f/255f, 50f/255f, 50f/255f), null};
            LightingColors = new Color?[8];

            TexCoord1Gens = new TexCoordGen?[8];
            PostTexCoordGens = new TexCoordGen?[8];

            TexMatrix1 = new TexMatrix?[10];
            PostTexMatrix = new TexMatrix?[20];

            TextureIndices = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
            TextureNames = new string[8] { "", "", "", "", "", "", "", "" };

            KonstColors = new Color?[4];
            KonstColors[0] = new Color(1, 1, 1, 1);

            ColorSels = new KonstColorSel[16];
            AlphaSels = new KonstAlphaSel[16];

            TevOrders = new TevOrder?[16];
            TevOrders[0] = new TevOrder(TexCoordId.TexCoord0, TexMapId.TexMap0, GXColorChannelId.Color0);

            TevColors = new Color?[16];
            TevColors[0] = new Color(1, 1, 1, 1);

            TevStages = new TevStage?[16];

            SwapModes = new TevSwapMode?[16];
            SwapModes[0] = new TevSwapMode(0, 0);

            SwapTables = new TevSwapModeTable?[16];
            SwapTables[0] = new TevSwapModeTable(0, 1, 2, 3);

            AlphCompare = new AlphaCompare(CompareType.Greater, 127, AlphaOp.And, CompareType.Always, 0);
            ZMode = new ZMode(true, CompareType.LEqual, true);
            BMode = new BlendMode(Enums.BlendMode.Blend, BlendModeControl.SrcAlpha, BlendModeControl.InverseSrcAlpha, LogicOp.NoOp);
            NBTScale = new NBTScale(0, Vector3.Zero);
            FogInfo = new Fog(0, false, 0, 0, 0, 0, 0, new Color(0, 0, 0, 0), new float[10]);
        }

        public void SetUpTev(bool hasTexture, bool hasVtxColor, int texIndex, string texName)
        {
            Flag = 1;
            // Set up channel control 0 to use vertex colors, if they're present
            if (hasVtxColor)
            {
                AddChannelControl(J3DColorChannelId.Color0, false, ColorSrc.Vertex, LightId.None, DiffuseFn.None, J3DAttenuationFn.None_0, ColorSrc.Register);
                AddChannelControl(J3DColorChannelId.Alpha0, false, ColorSrc.Vertex, LightId.None, DiffuseFn.None, J3DAttenuationFn.None_0, ColorSrc.Register);
            }
            else {
                AddChannelControl(J3DColorChannelId.Color0, false, ColorSrc.Register, LightId.None, DiffuseFn.Clamp, J3DAttenuationFn.Spec, ColorSrc.Register);
                AddChannelControl(J3DColorChannelId.Alpha0, false, ColorSrc.Register, LightId.None, DiffuseFn.Clamp, J3DAttenuationFn.Spec, ColorSrc.Register);
            }

            // These settings are common to all the configurations we can use
            TevStageParameters stageParams = new TevStageParameters
            {
                ColorInD = CombineColorInput.Zero,
                ColorOp = TevOp.Add,
                ColorBias = TevBias.Zero,
                ColorScale = TevScale.Scale_1,
                ColorClamp = true,
                ColorRegId = TevRegisterId.TevPrev,

                AlphaInD = CombineAlphaInput.Zero,
                AlphaOp = TevOp.Add,
                AlphaBias = TevBias.Zero,
                AlphaScale = TevScale.Scale_1,
                AlphaClamp = true,
                AlphaRegId = TevRegisterId.TevPrev
            };

            if (hasTexture)
            {
                // Generate texture stuff
                AddTexGen(TexGenType.Matrix2x4, TexGenSrc.Tex0, Enums.TexMatrix.Identity);
                AddTexMatrix(TexGenType.Matrix3x4, 0, OpenTK.Vector3.Zero, OpenTK.Vector2.One, 0, OpenTK.Vector2.Zero, OpenTK.Matrix4.Identity);
                AddTevOrder(TexCoordId.TexCoord0, TexMapId.TexMap0, GXColorChannelId.ColorNull);
                AddTexIndex(texIndex);

                // Texture + Vertex Color
                if (hasVtxColor)
                {
                    stageParams.ColorInA = CombineColorInput.Zero;
                    stageParams.ColorInB = CombineColorInput.RasColor;
                    stageParams.ColorInC = CombineColorInput.TexColor;
                    stageParams.AlphaInA = CombineAlphaInput.Zero;
                    stageParams.AlphaInB = CombineAlphaInput.RasAlpha;
                    stageParams.AlphaInC = CombineAlphaInput.TexAlpha;
                }
                // Texture alone
                else
                {
                    stageParams.ColorInA = CombineColorInput.TexColor;
                    stageParams.ColorInB = CombineColorInput.Zero;
                    stageParams.ColorInC = CombineColorInput.Zero;
                    stageParams.AlphaInA = CombineAlphaInput.TexAlpha;
                    stageParams.AlphaInB = CombineAlphaInput.Zero;
                    stageParams.AlphaInC = CombineAlphaInput.Zero;
                }
            }
            // No texture!
            else
            {
                // No vertex colors either, so make sure there's a material color (white) to use instead
                if (!hasVtxColor)
                {
                    MaterialColors[0] = new Color(1, 1, 1, 1);
                    AddChannelControl(J3DColorChannelId.Color0, false, ColorSrc.Register, LightId.None, DiffuseFn.None, J3DAttenuationFn.None_0, ColorSrc.Register);
                    AddChannelControl(J3DColorChannelId.Alpha0, false, ColorSrc.Register, LightId.None, DiffuseFn.None, J3DAttenuationFn.None_0, ColorSrc.Register);
                }

                // Set up TEV to use the material color we just set
                stageParams.ColorInA = CombineColorInput.RasColor;
                stageParams.ColorInB = CombineColorInput.Zero;
                stageParams.ColorInC = CombineColorInput.Zero;
                stageParams.AlphaInA = CombineAlphaInput.RasAlpha;
                stageParams.AlphaInB = CombineAlphaInput.Zero;
                stageParams.AlphaInC = CombineAlphaInput.Zero;
            }

            AddTevStage(stageParams);
        }

        public void AddChannelControl(J3DColorChannelId id, bool enable, ColorSrc MatSrcColor, LightId litId, DiffuseFn diffuse, J3DAttenuationFn atten, ColorSrc ambSrcColor)
        {
            ChannelControl control = new ChannelControl
            {
                Enable = enable,
                MaterialSrcColor = MatSrcColor,
                LitMask = litId,
                DiffuseFunction = diffuse,
                AttenuationFunction = atten,
                AmbientSrcColor = ambSrcColor
            };

            ChannelControls[(int)id] = control;
        }

        public void AddTexGen(TexGenType genType, TexGenSrc genSrc, Enums.TexMatrix mtrx)
        {
            TexCoordGen newGen = new TexCoordGen(genType, genSrc, mtrx);

            for (int i = 0; i < 8; i++)
            {
                if (TexCoord1Gens[i] == null)
                {
                    TexCoord1Gens[i] = newGen;
                    break;
                }

                if (i == 7)
                    throw new Exception($"TexGen array for material \"{ Name }\" is full!");
            }

            NumTexGensCount++;
        }

        public void AddTexMatrix(TexGenType projection, byte type, Vector3 effectTranslation, Vector2 scale, float rotation, Vector2 translation, Matrix4 matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                if (TexMatrix1[i] == null)
                {
                    TexMatrix1[i] = new TexMatrix(projection, type, effectTranslation, scale, rotation, translation, matrix);
                    break;
                }

                if (i == 9)
                    throw new Exception($"TexMatrix1 array for material \"{ Name }\" is full!");
            }
        }

        public void AddTexIndex(int index)
        {
            for (int i = 0; i < 8; i++)
            {
                if (TextureIndices[i] == -1)
                {
                    TextureIndices[i] = index;
                    break;
                }

                if (i == 7)
                    throw new Exception($"TextureIndex array for material \"{ Name }\" is full!");
            }
        }

        public void AddTevOrder(TexCoordId coordId, TexMapId mapId, GXColorChannelId colorChanId)
        {
            for (int i = 0; i < 8; i++)
            {
                if (TevOrders[i] == null)
                {
                    TevOrders[i] = new TevOrder(coordId, mapId, colorChanId);
                    break;
                }

                if (i == 9)
                    throw new Exception($"TevOrder array for material \"{ Name }\" is full!");
            }
        }

        public void AddTevStage(TevStageParameters parameters)
        {
            for (int i = 0; i < 16; i++)
            {
                if (TevStages[i] == null)
                {
                    TevStages[i] = new TevStage(parameters);
                    break;
                }

                if (i == 15)
                    throw new Exception($"TevStage array for material \"{ Name }\" is full!");
            }

            NumTevStagesCount++;
        }

        private TevStageParameters SetUpTevStageParametersForTexture()
        {
            TevStageParameters parameters = new TevStageParameters
            {
                ColorInA = CombineColorInput.TexColor,
                ColorInB = CombineColorInput.TexColor,
                ColorInC = CombineColorInput.Zero,
                ColorInD = CombineColorInput.Zero,

                ColorOp = TevOp.Add,
                ColorBias = TevBias.Zero,
                ColorScale = TevScale.Scale_1,
                ColorClamp = true,
                ColorRegId = TevRegisterId.TevPrev,

                AlphaInA = CombineAlphaInput.TexAlpha,
                AlphaInB = CombineAlphaInput.TexAlpha,
                AlphaInC = CombineAlphaInput.Zero,
                AlphaInD = CombineAlphaInput.Zero,

                AlphaOp = TevOp.Add,
                AlphaBias = TevBias.Zero,
                AlphaScale = TevScale.Scale_1,
                AlphaClamp = true,
                AlphaRegId = TevRegisterId.TevPrev
            };

            return parameters;
        }

        public Material(Material src)
        {
            Flag = src.Flag;
            ColorChannelControlsCount = src.ColorChannelControlsCount;
            NumTevStagesCount = src.NumTevStagesCount;
            NumTexGensCount = src.NumTexGensCount;
            CullMode = src.CullMode;
            ZCompLoc = src.ZCompLoc;
            Dither = src.Dither;
            TextureIndices = src.TextureIndices;
            TextureNames = src.TextureNames;
            IndTexEntry = src.IndTexEntry;
            MaterialColors = src.MaterialColors;
            ChannelControls = src.ChannelControls;
            AmbientColors = src.AmbientColors;
            LightingColors = src.LightingColors;
            TexCoord1Gens = src.TexCoord1Gens;
            PostTexCoordGens = src.PostTexCoordGens;
            TexMatrix1 = src.TexMatrix1;
            PostTexMatrix = src.PostTexMatrix;
            TevOrders = src.TevOrders;
            ColorSels = src.ColorSels;
            AlphaSels = src.AlphaSels;
            TevColors = src.TevColors;
            KonstColors = src.KonstColors;
            TevStages = src.TevStages;
            SwapModes = src.SwapModes;
            SwapTables = src.SwapTables;

            FogInfo = src.FogInfo;
            AlphCompare = src.AlphCompare;
            BMode = src.BMode;
            ZMode = src.ZMode;
            NBTScale = src.NBTScale;
        }

        public void Debug_Print()
        {
            Console.WriteLine($"TEV stage count: { NumTevStagesCount }\n\n");

            for (int i = 0; i < 16; i++)
            {
                if (TevStages[i] == null)
                    continue;

                Console.WriteLine($"Stage { i }:");
                Console.WriteLine(TevStages[i].ToString());
            }
        }

        public void Readjust()
        {
            for (int i = 0; i < 16; i++)
            {
                if (TevStages[i] != null)
                    NumTevStagesCount++;
            }

            for (int i = 0; i < 8; i++)
            {
                if (TexCoord1Gens[i] != null)
                    NumTexGensCount++;
            }

            // Note: Despite the name, this doesn't seem to control the number of color channel controls.
            // At least in Wind Waker, every single model has 1 for this value regardless of how many color channel controls it has.
            ColorChannelControlsCount = 1;
        }

        public static bool operator ==(Material left, Material right)
        {
            if (left.Flag != right.Flag)
                return false;
            if (left.CullMode != right.CullMode)
                return false;
            if (left.ColorChannelControlsCount != right.ColorChannelControlsCount)
                return false;
            if (left.NumTexGensCount != right.NumTexGensCount)
                return false;
            if (left.NumTevStagesCount != right.NumTevStagesCount)
                return false;
            if (left.ZCompLoc != right.ZCompLoc)
                return false;
            if (left.ZMode != right.ZMode)
                return false;
            if (left.Dither != right.Dither)
                return false;

            for (int i = 0; i < 2; i++)
            {
                if (left.MaterialColors[i] != right.MaterialColors[i])
                    return false;
            }
            for (int i = 0; i < 4; i++)
            {
                if (left.ChannelControls[i] != right.ChannelControls[i])
                    return false;
            }
            for (int i = 0; i < 2; i++)
            {
                if (left.AmbientColors[i] != right.AmbientColors[i])
                    return false;
            }
            for (int i = 0; i < 8; i++)
            {
                if (left.LightingColors[i] != right.LightingColors[i])
                    return false;
            }
            for (int i = 0; i < 8; i++)
            {
                if (left.TexCoord1Gens[i] != right.TexCoord1Gens[i]) // TODO: does != actually work on these types of things?? might need custom operators
                    return false;
            }
            for (int i = 0; i < 8; i++)
            {
                if (left.PostTexCoordGens[i] != right.PostTexCoordGens[i])
                    return false;
            }
            for (int i = 0; i < 10; i++)
            {
                if (left.TexMatrix1[i] != right.TexMatrix1[i])
                    return false;
            }
            for (int i = 0; i < 20; i++)
            {
                if (left.PostTexMatrix[i] != right.PostTexMatrix[i])
                    return false;
            }
            for (int i = 0; i < 8; i++)
            {
                if (left.TextureNames[i] != right.TextureNames[i])
                    return false;
            }
            for (int i = 0; i < 4; i++)
            {
                if (left.KonstColors[i] != right.KonstColors[i])
                    return false;
            }
            for (int i = 0; i < 16; i++)
            {
                if (left.ColorSels[i] != right.ColorSels[i])
                    return false;
            }
            for (int i = 0; i < 16; i++)
            {
                if (left.AlphaSels[i] != right.AlphaSels[i])
                    return false;
            }
            for (int i = 0; i < 16; i++)
            {
                if (left.TevOrders[i] != right.TevOrders[i])
                    return false;
            }
            for (int i = 0; i < 4; i++)
            {
                if (left.TevColors[i] != right.TevColors[i])
                    return false;
            }
            for (int i = 0; i < 16; i++)
            {
                if (left.TevStages[i] != right.TevStages[i])
                    return false;
            }
            for (int i = 0; i < 16; i++)
            {
                if (left.SwapModes[i] != right.SwapModes[i])
                    return false;
            }
            for (int i = 0; i < 16; i++)
            {
                if (left.SwapTables[i] != right.SwapTables[i])
                    return false;
            }

            if (left.FogInfo != right.FogInfo)
                return false;
            if (left.AlphCompare != right.AlphCompare)
                return false;
            if (left.BMode != right.BMode)
                return false;
            if (left.NBTScale != right.NBTScale)
                return false;

            return true;
        }

        public static bool operator !=(Material left, Material right)
        {
            if (left == right)
                return false;
            else
               return true;
        }
    }
}
