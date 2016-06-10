using OpenTK;
using WindEditor;

namespace J3DRenderer.JStudio
{
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
    public class AlphaCompare
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
        public GXLightId LitMask;

        /// <summary> Diffuse function to use. </summary>
        public GXDiffuseFn DiffuseFunction;

        /// <summary> Attenuation function to use. </summary>
        public GXAttenuationFn AttenuationFunction;

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
}
