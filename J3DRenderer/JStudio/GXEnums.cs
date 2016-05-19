namespace J3DRenderer.JStudio
{

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
        Konst = 14,
        Zero = 15       // 
    }

    public enum GXCombineAlphaInput
    {
        AlphaPrev = 0,  // Use the Alpha value form the previous TEV stage
        A0 = 1,         // Use the Alpha value from the Color/Output Register 0
        A1 = 2,         // Use the Alpha value from the Color/Output Register 1
        A2 = 3,         // Use the Alpha value from the Color/Output Register 2
        TexAlpha = 4,   // Use the Alpha value from the Texture
        RasAlpha = 5,   // Use the Alpha value from the rasterizer
        Konst = 6,
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
        Identity = 60,
        TexMtx0 = 30,
        TexMtx1 = 33,
        TexMtx2 = 36,
        TexMtx3 = 39,
        TexMtx4 = 42,
        TexMtx5 = 45,
        TexMtx6 = 48,
        TexMtx7 = 51,
        TexMtx8 = 54,
        TexMtx9 = 57
    }

    public enum GXPrimitiveType
    {
        Points = 0xB8,
        Lines = 0xA8,
        LineStrip = 0xB0,
        Triangles = 0x80,
        TriangleStrip = 0x98,
        TriangleFan = 0xA0,
        Quads = 0x80,
    }

    public enum GXLightId
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

    public enum GXDiffuseFn
    {
        None = 0,
        Signed = 1,
        Clamp = 2
    }

    public enum GXAttenuationFn
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

    /*public enum GXTevClampMode
    {
        Clamp = 0,
        ClampBottom = 2,
        None = 0,
        Top = 1
    }*/
}
