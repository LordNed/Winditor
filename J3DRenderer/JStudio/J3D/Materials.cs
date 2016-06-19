using OpenTK;
using System;
using WindEditor;

namespace JStudio.J3D
{
    #region GX Enums
    public enum GXCompareType
    {
        Never = 0,
        Less = 1,
        Equal = 2,
        LEqual = 3,
        Greater = 4,
        NEqual = 5,
        GEqual = 6,
        Always = 7
    }

    public enum GXAlphaOp
    {
        And = 0,
        Or = 1,
        XOR = 2,
        XNOR = 3
    }

    public enum GXCullMode
    {
        None = 0,   // Do not cull any primitives
        Front = 1,  // Cull front-facing primitives
        Back = 2,   // Cull back-facing primitives
        All = 3     // Cull all primitives
    }

    public enum GXBlendModeControl
    {
        Zero = 0,               // ! < 0.0
        One = 1,                // ! < 1.0
        SrcColor = 2,           // ! < Source Color
        InverseSrcColor = 3,    // ! < 1.0 - (Source Color)
        SrcAlpha = 4,           // ! < Source Alpha
        InverseSrcAlpha = 5,    // ! < 1.0 - (Source Alpha)
        DstAlpha = 6,           // ! < Framebuffer Alpha
        InverseDstAlpha = 7     // ! < 1.0 - (Framebuffer Alpha)
    }

    public enum GXBlendMode
    {
        None = 0,
        Blend = 1,
        Logic = 2,
        Subtract = 3
    }

    public enum GXTevOp
    {
        Add = 0,
        Sub = 1,
        Comp_R8_GT = 8,
        Comp_R8_EQ = 9,
        Comp_GR16_GT = 10,
        Comp_GR16_EQ = 11,
        Comp_BGR24_GT = 12,
        Comp_BGR24_EQ = 13,
        Comp_RGB8_GT = 14,
        Comp_RGB8_EQ = 15,
        Comp_A8_EQ = Comp_RGB8_EQ,
        Comp_A8_GT = Comp_RGB8_GT
    }

    public enum GXTevBias
    {
        Zero = 0,
        AddHalf = 1,
        SubHalf = 2
    }

    public enum GXTevScale
    {
        Scale_1 = 0,
        Scale_2 = 1,
        Scale_4 = 2,
        Divide_2 = 3
    }

    /// <summary>
    /// GXSetTevColorIn(GXTevStageID stage, GXTevColorArg a, GXTevColorArg b, GXTevColorArg c, GXTevColorArg d)
    /// Defines the color input source to a TEV Stage, allowing the TEV Stage to choose
    /// between constant (register) colors/alphas, rasterized color/alpha (per-vertex lighting 
    /// result) and pre-defined constants.
    /// </summary>
    public enum GXCombineColorInput
    {
        ColorPrev = 0,  // ! < Use Color Value from previous TEV stage
        AlphaPrev = 1,  // ! < Use Alpha Value from previous TEV stage
        C0 = 2,         // ! < Use the Color Value from the Color/Output Register 0
        A0 = 3,         // ! < Use the Alpha value from the Color/Output Register 0
        C1 = 4,         // ! < Use the Color Value from the Color/Output Register 1
        A1 = 5,         // ! < Use the Alpha value from the Color/Output Register 1
        C2 = 6,         // ! < Use the Color Value from the Color/Output Register 2
        A2 = 7,         // ! < Use the Alpha value from the Color/Output Register 2
        TexColor = 8,   // ! < Use the Color value from Texture
        TexAlpha = 9,   // ! < Use the Alpha value from Texture
        RasColor = 10,  // ! < Use the color value from rasterizer
        RasAlpha = 11,  // ! < Use the alpha value from rasterizer
        One = 12,
        Half = 13,
        Konst = 14, // ToDo: Is this GX_CC_QUARTER?
        Zero = 15       // 
                        // ToDo: Is this missing GX_CC_TEXRRR, GX_CC_TEXGGG, GX_CC_TEXBBBB?
    }

    /// <summary>
    /// GXSetTevAlphaIn(GXTevStageID stage, GXTevAlphaArg a, GXTevAlphaArg b, GXTevAlphaArg c, GXTevAlphaArg d)
    /// Defines the alpha input source for a TEV stage, allowing the TEV stage to choose different alpha sources.
    /// </summary>
    public enum GXCombineAlphaInput
    {
        AlphaPrev = 0,  // Use the Alpha value form the previous TEV stage
        A0 = 1,         // Use the Alpha value from the Color/Output Register 0
        A1 = 2,         // Use the Alpha value from the Color/Output Register 1
        A2 = 3,         // Use the Alpha value from the Color/Output Register 2
        TexAlpha = 4,   // Use the Alpha value from the Texture
        RasAlpha = 5,   // Use the Alpha value from the rasterizer
        Konst = 6,      // ToDO: Is this GX_CA_ONE?
        Zero = 7
    }

    public enum GXKonstColorSel
    {
        KCSel_1 = 0x00,     // Constant 1.0
        KCSel_7_8 = 0x01,   // Constant 7/8
        KCSel_3_4 = 0x02,   // Constant 3/4
        KCSel_5_8 = 0x03,   // Constant 5/8
        KCSel_1_2 = 0x04,   // Constant 1/2
        KCSel_3_8 = 0x05,   // Constant 3/8
        KCSel_1_4 = 0x06,   // Constant 1/4
        KCSel_1_8 = 0x07,   // Constant 1/8
        KCSel_K0 = 0x0C,    // K0[RGB] Register
        KCSel_K1 = 0x0D,    // K1[RGB] Register
        KCSel_K2 = 0x0E,    // K2[RGB] Register
        KCSel_K3 = 0x0F,    // K3[RGB] Register
        KCSel_K0_R = 0x10,  // K0[RRR] Register
        KCSel_K1_R = 0x11,  // K1[RRR] Register
        KCSel_K2_R = 0x12,  // K2[RRR] Register
        KCSel_K3_R = 0x13,  // K3[RRR] Register
        KCSel_K0_G = 0x14,  // K0[GGG] Register
        KCSel_K1_G = 0x15,  // K1[GGG] Register
        KCSel_K2_G = 0x16,  // K2[GGG] Register
        KCSel_K3_G = 0x17,  // K3[GGG] Register
        KCSel_K0_B = 0x18,  // K0[BBB] Register
        KCSel_K1_B = 0x19,  // K1[BBB] Register
        KCSel_K2_B = 0x1A,  // K2[BBB] Register
        KCSel_K3_B = 0x1B,  // K3[BBB] Register
        KCSel_K0_A = 0x1C,  // K0[AAA] Register
        KCSel_K1_A = 0x1D,  // K1[AAA] Register
        KCSel_K2_A = 0x1E,  // K2[AAA] Register
        KCSel_K3_A = 0x1F   // K3[AAA] Register
    }

    public enum GXKonstAlphaSel
    {
        KASel_1 = 0x00,     // Constant 1.0
        KASel_7_8 = 0x01,   // Constant 7/8
        KASel_3_4 = 0x02,   // Constant 3/4
        KASel_5_8 = 0x03,   // Constant 5/8
        KASel_1_2 = 0x04,   // Constant 1/2
        KASel_3_8 = 0x05,   // Constant 3/8
        KASel_1_4 = 0x06,   // Constant 1/4
        KASel_1_8 = 0x07,   // Constant 1/8
        KASel_K0_R = 0x10,  // K0[R] Register
        KASel_K1_R = 0x11,  // K1[R] Register
        KASel_K2_R = 0x12,  // K2[R] Register
        KASel_K3_R = 0x13,  // K3[R] Register
        KASel_K0_G = 0x14,  // K0[G] Register
        KASel_K1_G = 0x15,  // K1[G] Register
        KASel_K2_G = 0x16,  // K2[G] Register
        KASel_K3_G = 0x17,  // K3[G] Register
        KASel_K0_B = 0x18,  // K0[B] Register
        KASel_K1_B = 0x19,  // K1[B] Register
        KASel_K2_B = 0x1A,  // K2[B] Register
        KASel_K3_B = 0x1B,  // K3[B] Register
        KASel_K0_A = 0x1C,  // K0[A] Register
        KASel_K1_A = 0x1D,  // K1[A] Register
        KASel_K2_A = 0x1E,  // K2[A] Register
        KASel_K3_A = 0x1F   // K3[A] Register
    }

    public enum GXTexGenSrc
    {
        Position = 0,
        Normal = 1,
        Binormal = 2,
        Tangent = 3,
        Tex0 = 4,
        Tex1 = 5,
        Tex2 = 6,
        Tex3 = 7,
        Tex4 = 8,
        Tex5 = 9,
        Tex6 = 10,
        Tex7 = 11,
        TexCoord0 = 12,
        TexCoord1 = 13,
        TexCoord2 = 14,
        TexCoord3 = 15,
        TexCoord4 = 16,
        TexCoord5 = 17,
        TexCoord6 = 18,
        Color0 = 19,
        Color1 = 20,
    }

    public enum GXTexGenType
    {
        Matrix3x4 = 0,
        Matrix2x4 = 1,
        Bump0 = 2,
        Bump1 = 3,
        Bump2 = 4,
        Bump3 = 5,
        Bump4 = 6,
        Bump5 = 7,
        Bump6 = 8,
        Bump7 = 9,
        SRTG = 10
    }

    public enum GXTexMatrix
    {
        TexMtx0 = 30,
        TexMtx1 = 33,
        TexMtx2 = 36,
        TexMtx3 = 39,
        TexMtx4 = 42,
        TexMtx5 = 45,
        TexMtx6 = 48,
        TexMtx7 = 51,
        TexMtx8 = 54,
        TexMtx9 = 57,
        Identity = 60,
    }

    public enum GXPrimitiveType
    {
        Points = 0xB8,
        Lines = 0xA8,
        LineStrip = 0xB0,
        Triangles = 0x90,
        TriangleStrip = 0x98,
        TriangleFan = 0xA0,
        Quads = 0x80,
    }

    [Flags]
    public enum GXLightMask
    {
        Light0 = 0x001,
        Light1 = 0x002,
        Light2 = 0x004,
        Light3 = 0x008,
        Light4 = 0x010,
        Light5 = 0x020,
        Light6 = 0x040,
        Light7 = 0x080,
        None = 0x000
    }

    public enum GXDiffuseFunction
    {
        None = 0,
        Signed = 1,
        Clamp = 2
    }

    public enum GXAttenuationFunction
    {
        // No attenuation
        None = 2,

        // Specular Computation
        Spec = 0,

        // Spot Light Attenuation
        Spot = 1
    }

    public enum GXColorSrc
    {
        Register = 0, // Use Register Colors
        Vertex = 1 // Use Vertex Colors
    }

    public enum GXLogicOp
    {
        Clear = 0,
        And = 1,
        Copy = 3,
        Equiv = 9,
        Inv = 10,
        InvAnd = 4,
        InvCopy = 12,
        InvOr = 13,
        NAnd = 14,
        NoOp = 5,
        NOr = 8,
        Or = 7,
        RevAnd = 2,
        RevOr = 11,
        Set = 15,
        XOr = 6
    }

    public enum GXTexCoordSlot
    {
        TexCoord0 = 0,
        TexCoord1 = 1,
        TexCoord2 = 2,
        TexCoord3 = 3,
        TexCoord4 = 4,
        TexCoord5 = 5,
        TexCoord6 = 6,
        TexCoord7 = 7,
        Null = 0xFF
    }

    public enum GXColorChannelId
    {
        Color0 = 0,
        Color1 = 1,
        Alpha0 = 2,
        Alpha1 = 3,
        Color0A0 = 4,
        Color1A1 = 5,
        ColorZero = 6,
        AlphaBump = 7,
        AlphaBumpN = 8,
        ColorNull = 0xFF,
    }
    #endregion

    #region Material Classes
    /// <summary>
    /// GX_SetZMode - Sets the Z-buffer compare mode. The result of the Z compare is used to conditionally write color values to the Embedded Frame Buffer.
    /// </summary>
    public class ZMode
    {
        /// <summary> If false, ZBuffering is disabled and the Z buffer is not updated. </summary>
        public bool Enable;

        /// <summary> Determines the comparison that is performed. The newely rasterized Z value is on the left while the value from the Z buffer is on the right. If the result of the comparison is false, the newly rasterized pixel is discarded. </summary>
        public GXCompareType Function;

        /// <summary> If true, the Z buffer is updated with the new Z value after a comparison is performed. 
        /// Example: Disabling this would prevent a write to the Z buffer, useful for UI elements or other things
        /// that shouldn't write to Z Buffer. See glDepthMask. </summary>
        public bool UpdateEnable;

        public override string ToString()
        {
            return string.Format("Enabled: {0} Function: {1} UpdateEnable: {2}", Enable, Function, UpdateEnable);
        }
    }

    /// <summary>
    /// GX_SetAlphaCompare - Sets the parameters for the alpha compare function which uses the alpha output from the last active TEV stage.
    /// The alpha compare operation is:
    ///     alpha_pass = (alpha_src(comp0)ref0) (op) (alpha_src(comp1)ref1)
    /// where alpha_src is the alpha from the last active TEV stage.
    /// </summary>
    public class AlphaTest
    {
        /// <summary> subfunction 0 </summary>
        public GXCompareType Comp0;

        /// <summary> Reference value for subfunction 0. </summary>
        public byte Reference0;

        /// <summary> Alpha combine control for subfunctions 0 and 1. </summary>
        public GXAlphaOp Operation;

        /// <summary> subfunction 1 </summary>
        public GXCompareType Comp1;

        /// <summary> Reference value for subfunction 1. </summary>
        public byte Reference1;

        public override string ToString()
        {
            return string.Format("Compare: {0} Ref: {1} Op: {2} Compare: {3} Reference: {4}", Comp0, Reference0, Operation, Comp1, Reference1);
        }
    }

    /// <summary>
    /// GX_SetBlendMode - Determines how the source image is blended with the Embedded Frame Buffer.
    /// When <see cref="Type"/> is set to <see cref="GXBlendMode.None"/> the source data is written directly to the EFB. 
    /// When set to <see cref="GXBlendMode.Blend"/> source and EFB pixels are blended using the following equation:
    ///     dst_pix_clr = src_pix_clr * src_fact + dst_pix_clr * dst_fact
    /// </summary>
    public class BlendMode
    {
        /// <summary> Blending Type </summary>
        public GXBlendMode Type;

        /// <summary> Blending Control </summary>
        public GXBlendModeControl SourceFactor;

        /// <summary> Blending Control </summary>
        public GXBlendModeControl DestinationFactor;

        /// <summary> What operation is used to blend them when <see cref="Type"/> is set to <see cref="GXBlendMode.Logic"/>. </summary>
        public GXLogicOp Operation; // Seems to be logic operators such as clear, and, copy, equiv, inv, invand, etc.

        public override string ToString()
        {
            return string.Format("Type: {0} Control: {1} Destination Control: {2} Operation: {3}", Type, SourceFactor, DestinationFactor, Operation);
        }
    }

    /// <summary>
    /// The color channel can have 1-8 lights associated with it, set using <see cref="ColorChannelControl.LitMask"/>. 
    /// The <see cref="ColorChannelControl.DiffuseFunction"/> and <see cref="ColorChannelControl.AttenuationFunction"/> parameters control the lighting equation for all lights associated with this channel.
    /// The <see cref="ColorChannelControl.AmbientSrc"/> and <see cref="ColorChannelControl.MaterialSrc"/> used to select whether the input source colors come from the register colors or vertex colors.
    /// </summary>
    public class ColorChannelControl
    {
        /// <summary> Whether or not to enable lighting for this channel. If false, the material source color is passed through as the material output color.</summary>
        public bool LightingEnabled;

        /// <summary> Source for the Material color. When set to <see cref="GXColorSrc.Register"/> the color set by GX_SetChanMatColor is used. </summary>
        public GXColorSrc MaterialSrc;

        /// <summary> Light ID or IDs to associate with this channel. </summary>
        public GXLightMask LitMask;

        /// <summary> Diffuse function to use. </summary>
        public GXDiffuseFunction DiffuseFunction;

        /// <summary> Attenuation function to use. </summary>
        public GXAttenuationFunction AttenuationFunction;

        /// <summary> Source for the ambient color. When set to <see cref="GXColorSrc.Register"/> the color set by GX_SetChanAmbColor is used. </summary>
        public GXColorSrc AmbientSrc;
    }

    /// <summary>
    /// Specifies how texture coordinates are generated.
    /// 
    /// Output texture coordinates are usually the result of some transform of an input attribute; either position, normal, or texture coordinate.
    /// You can also generate texture coordinates from the output color channel of the per-vertex lighting calculations.
    /// 
    /// TexMatrixSource idenitfies a default set of texture matrix names that can be supplied.
    /// </summary>
    public class TexCoordGen
    {
        /// <summary> Generation Type </summary>
        public GXTexGenType Type;
        /// <summary> Texture Coordinate Source </summary>
        public GXTexGenSrc Source;
        /// <summary> Texture Matrix Index </summary>
        public GXTexMatrix TexMatrixSource;
    }

    public enum TexMatrixProjection
    {
        TexProj_ST = 0,
        TexProj_STQ = 1
    }

    public class TexMatrix
    {
        public TexMatrixProjection Projection;
        public byte Type;
        public float CenterS;
        public float CenterT;
        public float CenterW;
        public float ScaleS;
        public float ScaleT;
        public float Rotation;
        public float TranslateS;
        public float TranslateT;
        public Matrix4 Matrix; // Projection Matrix? blank63 has it as that. Use above fields to generate a new matrix then multiply by proj matrix?

        public Matrix4 TexMtx
        {
            get
            {
                Matrix4 scale = Matrix4.CreateScale(new Vector3(ScaleS, ScaleT, 1));
                Matrix4 rot = Matrix4.CreateRotationX(Rotation);
                Matrix4 translation = Matrix4.CreateTranslation(/*new Vector3(CenterS, CenterT, CenterW) + */new Vector3(TranslateS, TranslateT, 0));

                return scale * rot * translation;
            }
        }
    }

    public class TevIn
    {
        public byte A;
        public byte B;
        public byte C;
        public byte D;
    }

    public class TevOp
    {
        public byte Operation;
        public byte Bias;
        public byte Scale;
        public byte Clamp;
        public byte Out;
    }

    /// <summary>
    /// GXSetTevOrder(GXTevStageID stage, GXTexCoordID coord, GXTexMapID map, GXChannelID color)
    /// Determines which texture and rasterize color inputs are available to each TEV
    /// stage. GXSetTevColorIn/GXSetTevAlphaIn control how the inputs plug into each 
    /// TEV operation for each stage.
    /// </summary>
    public class TevOrder
    {
        public GXTexCoordSlot TexCoordId;
        public byte TexMap;
        public GXColorChannelId ChannelId;
    }

    public class TevIndirect
    {
        public byte IndStage;
        public byte Format;
        public byte Bias;
        public byte IndMatrix;
        public byte WrapS;
        public byte WrapT;
        public byte AddPrev;
        public byte Utclod; // wtf?
        public byte Alpha;
    }

    public class NBTScale
    {
        public byte Unknown1;
        public Vector3 Scale;

        public NBTScale() { }

        public NBTScale(byte unknown, Vector3 scale)
        {
            Unknown1 = unknown;
            Scale = scale;
        }
    }

    /// <summary>
    /// GXSetTevSwapMode(GXTevStageID stage, GXTevSwapSel ras_sel, GXTevSwapSel tex_sel)
    /// For each TEV Stage you can select entry from the GXSetTevSwapModeTable for the rasterized
    /// input and the texture color input. The texture color inputs and the rasterized color inputs 
    /// can be swapped, and a color channel can be selected.
    /// </summary>
    public class TevSwapMode
    {
        public byte RasSel;
        public byte TexSel;
    }

    /// <summary>
    /// GXSetTevSwapModeTable(GXTevSwapSel select, GXTevColorChan red, GXTevColorChan green, GXTevColorChan blue, GXColorChan alpha)
    /// The swap table allows the rasterized/color colors to be swapped component-wise. An entry 
    /// into the swap table specifies how input components remap to output components.
    /// 
    /// For any given R, G, B, or A it defines the input component which should be mapped to the
    /// respective R, G, B, or A output component.
    /// </summary>
    public class TevSwapModeTable
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}, {3}]", R, G, B, A);
        }
    }

    public class IndTexOrder
    {
        public byte TexCoord;
        public byte TexMap;
    }

    public class IndTexCoordScale
    {
        public byte ScaleS;
        public byte ScaleT;
    }

    public class TevStage
    {
        public byte Unknown0; // Always 0xFF
        public GXCombineColorInput[] ColorIn; // 4
        public GXTevOp ColorOp;
        public GXTevBias ColorBias;
        public GXTevScale ColorScale;
        public bool ColorClamp;
        public byte ColorRegId;
        public GXCombineAlphaInput[] AlphaIn; // 4
        public GXTevOp AlphaOp;
        public GXTevBias AlphaBias;
        public GXTevScale AlphaScale;
        public bool AlphaClamp;
        public byte AlphaRegId;
        public byte Unknown1; // Always 0xFF

        public TevStage()
        {
            ColorIn = new GXCombineColorInput[4];
            AlphaIn = new GXCombineAlphaInput[4];
        }
    }

    public class FogInfo
    {
        public byte Type;
        public bool Enable;
        public ushort Center;
        public float StartZ;
        public float EndZ;
        public float NearZ;
        public float FarZ;
        public WLinearColor Color;
        public ushort[] Table; // 10 of these.
    }

    public class IndirectTexture
    {
        /// <summary> Determines if an indirect texture lookup is to take place </summary>
        public bool HasLookup;

        /// <summary> The number of indirect texturing stages to use </summary>
        public byte IndTexStageNum;

        /// <summary> Unknown value 1. Related to TevOrders. </summary>
        public byte Unknown1;

        /// <summary> Unknown value 2. Related to TevOrders. </summary>
        public byte Unknown2;

        /// <summary> Usually 0xFFFF but sometimes 0x0000. </summary>
        public ushort[] Unknown3;

        /// <summary> The dynamic 2x3 matrices to use when transforming the texture coordinates. </summary>
        public IndirectTextureMatrix[] Matrices;

        /// <summary> U and V scales to use when transforming the texture coordinates </summary>
        public IndirectTextureScale[] Scales;

        /// <summary> Instructions for setting up the specified TEV stage for lookup operations </summary>
        public IndirectTevOrder[] TevOrders;

        public IndirectTexture()
        {
            Matrices = new IndirectTextureMatrix[3];
            Scales = new IndirectTextureScale[4];
            TevOrders = new IndirectTevOrder[16];
            Unknown3 = new ushort[7];
        }
    }

    public class IndirectTextureMatrix
    {
        public Matrix2x3 Matrix;
        public byte ScaleExponent; // 2^Exponent to multiply the matrix by.

        public IndirectTextureMatrix(Matrix2x3 matrix, byte scaleExponent)
        {
            Matrix = matrix;
            ScaleExponent = scaleExponent;
        }
    }

    /// <summary>
    /// Defines S (U) and T (V) scale values to transform source texture coordinates during an indirect texture lookup.
    /// </summary>
    public class IndirectTextureScale
    {
        /// <summary> Scale value for the source texture coordinates' S (U) component. </summary>
        public byte ScaleS;

        /// <summary> Scale value for the source texture coordinates' T (V) component. </summary>
        public byte ScaleT;

        public IndirectTextureScale(byte scaleS, byte scaleT)
        {
            ScaleS = scaleS;
            ScaleT = scaleT;
        }
    }

    /// <summary>
    /// Configures a TEV stage during an indirect texture lookup.
    /// </summary>
    public class IndirectTevOrder
    {
        public byte TevStageID;
        public byte IndTexFormat;
        public byte IndTexBiasSel;
        public byte IndTexMtxId;
        public byte IndTexWrapS;
        public byte IndTexWrapT;
        public bool AddPrev;
        public bool UtcLod;
        public byte AlphaSel;
    }
    #endregion
}
