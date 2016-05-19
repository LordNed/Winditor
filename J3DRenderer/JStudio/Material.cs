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
        public GXBlendModeControl SourceFact;

        /// <summary> Blending Control </summary>
        public GXBlendModeControl DestinationFact;

        /// <summary> What operation is used to blend them when <see cref="Type"/> is set to <see cref="GXBlendMode.Logic"/>. </summary>
        public GXLogicOp Operation; // Seems to be logic operators such as clear, and, copy, equiv, inv, invand, etc.

        public override string ToString()
        {
            return string.Format("Type: {0} Control: {1} Destination Control: {2} Operation: {3}", Type, SourceFact, DestinationFact, Operation);
        }
    }

    /// <summary>
    /// The color channel can have 1-8 lights associated with it, set using <see cref="ChanCtrl.LitMask"/>. 
    /// The <see cref="ChanCtrl.DiffuseFunction"/> and <see cref="ChanCtrl.AttenuationFunction"/> parameters control the lighting equation for all lights associated with this channel.
    /// The <see cref="ChanCtrl.AmbientSrc"/> and <see cref="ChanCtrl.MaterialSrc"/> used to select whether the input source colors come from the register colors or vertex colors.
    /// </summary>
    public class ChanCtrl
    {
        /// <summary> Whether or not to enable lighting for this channel. If false, the material source color is passed through as the material output color.</summary>
        public bool Enable;

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

    public class TexMatrix
    {
        public byte Projection;
        public byte Type;
        public float CenterS;
        public float CenterT;
        public float Unknown0;
        public float ScaleS;
        public float ScaleT;
        public float Rotation;
        public float TranslateS;
        public float TranslateT;
        public float[,] PreMatrix; // 4x4
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

    public class TevSwapMode
    {
        public byte RasSel;
        public byte TexSel;
    }

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

    public class IndTexMatrix
    {
        public float[] OffsetMatrix; // 2 of them
        public byte ScaleExponent;
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
}
