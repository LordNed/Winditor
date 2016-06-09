using J3DRenderer.JStudio;
using System;
using System.IO;
using System.Text;
using WindEditor;

namespace J3DRenderer.ShaderGen
{
    public static class FragmentShaderGen
    {
        private const string kColors = "color";
        private const string kKColors = "k";

        private static readonly string[] m_tevCOutputTable = new[] { "prev.rgb", "c0.rgb", "c1.rgb", "c2.rgb" };
        private static readonly string[] m_tevAOutputTable = new[] { "prev.a", "c0.a", "c1.a", "c2.a" };
        private static string[] m_tevOutputRegs = new[]
        {
            "prev",
            "color0",
            "color1",
            "color2"
        };

        /// <summary>
        /// See <see cref="GXColorChannelId"/>
        /// </summary>
        private static string[] m_tevRasTable = new[]
        {
            "col0",                                             // Color0
            "col1",                                             // Color1
            "ERROR13",                                          // Alpha0 (Not Valid?)
            "ERROR14",                                          // Alpha1 (Not Valid?)
            "col0",                                             // Color0A0
            "col1",                                             // Color0A1
            "vec4(0,0,0,0)",                                    // Zero
            "(vec4(1,1,1,1) * alphabump)",                      // Bump Alpha (0...248)
            "(vec4(1,1,1,1) * (alphabump | (alphabump >> 5)))", // Normalized Bump Alpha (0..255)
        };

        /// <summary>
        /// See <see cref="GXKonstColorSel"/>
        /// </summary>
        private static string[] m_tevKSelTableC = new[]
        {
            "255,255,255",      // 1   = 0x00
	        "223,223,223",      // 7_8 = 0x01
	        "191,191,191",      // 3_4 = 0x02
	        "159,159,159",      // 5_8 = 0x03
	        "128,128,128",      // 1_2 = 0x04
	        "96,96,96",         // 3_8 = 0x05
	        "64,64,64",         // 1_4 = 0x06
	        "32,32,32",         // 1_8 = 0x07
	        "0,0,0",            // Invalid = 0x08
	        "0,0,0",            // Invalid = 0x09
	        "0,0,0",            // Invalid = 0x0a
	        "0,0,0",            // Invalid = 0x0b
	        "kColor[0].rgb",    // K0 = 0x0C
	        "kColor[1].rgb",    // K1 = 0x0D
	        "kColor[2].rgb",    // K2 = 0x0E
	        "kColor[3].rgb",    // K3 = 0x0F
	        "kColor[0].rrr",    // K0_R = 0x10
	        "kColor[1].rrr",    // K1_R = 0x11
	        "kColor[2].rrr",    // K2_R = 0x12
	        "kColor[3].rrr",    // K3_R = 0x13
	        "kColor[0].ggg",    // K0_G = 0x14
	        "kColor[1].ggg",    // K1_G = 0x15
	        "kColor[2].ggg",    // K2_G = 0x16
	        "kColor[3].ggg",    // K3_G = 0x17
	        "kColor[0].bbb",    // K0_B = 0x18
	        "kColor[1].bbb",    // K1_B = 0x19
	        "kColor[2].bbb",    // K2_B = 0x1A
	        "kColor[3].bbb",    // K3_B = 0x1B
	        "kColor[0].aaa",    // K0_A = 0x1C
	        "kColor[1].aaa",    // K1_A = 0x1D
	        "kColor[2].aaa",    // K2_A = 0x1E
	        "kColor[3].aaa",    // K3_A = 0x1F
        };

        private static string[] m_tevKSelTableA = new[]
        {
            "255",              // 1   = 0x00
	        "223",              // 7_8 = 0x01
	        "191",              // 3_4 = 0x02
	        "159",              // 5_8 = 0x03
	        "128",              // 1_2 = 0x04
	        "96",               // 3_8 = 0x05
	        "64",               // 1_4 = 0x06
	        "32",               // 1_8 = 0x07
	        "0",                // Invalid = 0x08
	        "0",                // Invalid = 0x09
	        "0",                // Invalid = 0x0a
	        "0",                // Invalid = 0x0b
	        "0",                // Invalid = 0x0c
	        "0",                // Invalid = 0x0d
	        "0",                // Invalid = 0x0e
	        "0",                // Invalid = 0x0f
	        "kColor[0].r",      // K0_R = 0x10
	        "kColor[1].r",      // K1_R = 0x11
	        "kColor[2].r",      // K2_R = 0x12
	        "kColor[3].r",      // K3_R = 0x13
	        "kColor[0].g",      // K0_G = 0x14
	        "kColor[1].g",      // K1_G = 0x15
	        "kColor[2].g",      // K2_G = 0x16
	        "kColor[3].g",      // K3_G = 0x17
	        "kColor[0].b",      // K0_B = 0x18
	        "kColor[1].b",      // K1_B = 0x19
	        "kColor[2].b",      // K2_B = 0x1A
	        "kColor[3].b",      // K3_B = 0x1B
	        "kColor[0].a",      // K0_A = 0x1C
	        "kColor[1].a",      // K1_A = 0x1D
	        "kColor[2].a",      // K2_A = 0x1E
	        "kColor[3].a",      // K3_A = 0x1F
        };

        private static string[] m_tevCInputTable = new[]
        {
            "prev.rgb",          // CPREV
	        "prev.aaa",          // APREV
	        "c0.rgb",            // C0
	        "c0.aaa",            // A0
	        "c1.rgb",            // C1
	        "c1.aaa",            // A1
	        "c2.rgb",            // C2
	        "c2.aaa",            // A2
	        "textemp.rgb",       // TEXC
	        "textemp.aaa",       // TEXA
	        "rastemp.rgb",       // RASC
	        "rastemp.aaa",       // RASA
	        "vec3(255,255,255)", // ONE
	        "vec3(128,128,128)", // HALF
	        "konsttemp.rgb",     // KONST
	        "vec3(0,0,0)",       // ZERO
        };

        private static string[] m_tevAInputTable = new []
        {
            "prev.a",           // APREV
	        "c0.a",             // A0
	        "c1.a",             // A1
	        "c2.a",             // A2
	        "textemp.a",        // TEXA
	        "rastemp.a",        // RASA
	        "konsttemp.a",      // KONST
	        "0",                // ZERO
        };

        public static string GenerateFragmentShader(Material mat, MAT3 data)
        {
            StringBuilder stream = new StringBuilder();

            // Shader Header
            stream.AppendLine("// Automatically Generated File. All changes will be lost.");
            stream.AppendLine("#version 330 core");
            stream.AppendLine();

            // Configure inputs to match our outputs from VS
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Position))
                stream.AppendLine("in vec3 Position;");

            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Normal))
                stream.AppendLine("in vec3 Normal;");

            //for (int i = 0; i < data.NumChannelControls[mat.NumChannelControlsIndex]; i++)
            stream.AppendLine("in vec4 colors_0;");
            stream.AppendLine("in vec4 colors_1;");

            for (int texGen = 0; texGen < data.NumTexGens[mat.NumTexGensIndex]; texGen++)
                stream.AppendLine(string.Format("in vec3 TexGen{0};", texGen));

            stream.AppendLine();


            // Texture Inputs
            for (int i = 0; i < 8; i++)
            {
                stream.AppendFormat("uniform sampler2D Texture{0};\n", i);
            }

            //stream.AppendLine("layout(std140) uniform PSBlock\n{");
            //stream.AppendLine(
            //    "\tvec4 color[4];\n" +
            //    "\tvec4 kColor[4];\n" +
            //    "};");

            // Final Output
            stream.AppendLine("// Final Output");
            stream.AppendLine("out vec4 PixelColor;\n\n");

            // Main Function
            stream.AppendLine("void main()\n{\n");

            // Debug override color and kColor
            stream.AppendFormat("vec4 color[4];\n");
            stream.AppendFormat("vec4 kColor[4];\n");

            stream.AppendFormat("color[0] = vec4({0},{1},{2},{3});\n", data.TevColors[mat.TevColorIndexes[0]].R, data.TevColors[mat.TevColorIndexes[0]].G, data.TevColors[mat.TevColorIndexes[0]].B, data.TevColors[mat.TevColorIndexes[0]].A);
            stream.AppendFormat("color[1] = vec4({0},{1},{2},{3});\n", data.TevColors[mat.TevColorIndexes[1]].R, data.TevColors[mat.TevColorIndexes[1]].G, data.TevColors[mat.TevColorIndexes[1]].B, data.TevColors[mat.TevColorIndexes[1]].A);
            stream.AppendFormat("color[2] = vec4({0},{1},{2},{3});\n", data.TevColors[mat.TevColorIndexes[2]].R, data.TevColors[mat.TevColorIndexes[2]].G, data.TevColors[mat.TevColorIndexes[2]].B, data.TevColors[mat.TevColorIndexes[2]].A);
            stream.AppendFormat("color[3] = vec4({0},{1},{2},{3});\n", data.TevColors[mat.TevColorIndexes[3]].R, data.TevColors[mat.TevColorIndexes[3]].G, data.TevColors[mat.TevColorIndexes[3]].B, data.TevColors[mat.TevColorIndexes[3]].A);

            stream.AppendFormat("kColor[0] = vec4({0},{1},{2},{3});\n", mat.TevKonstColorIndexes[0].R, mat.TevKonstColorIndexes[0].G, mat.TevKonstColorIndexes[0].B, mat.TevKonstColorIndexes[0].A);
            stream.AppendFormat("kColor[1] = vec4({0},{1},{2},{3});\n", mat.TevKonstColorIndexes[1].R, mat.TevKonstColorIndexes[1].G, mat.TevKonstColorIndexes[1].B, mat.TevKonstColorIndexes[1].A);
            stream.AppendFormat("kColor[2] = vec4({0},{1},{2},{3});\n", mat.TevKonstColorIndexes[2].R, mat.TevKonstColorIndexes[2].G, mat.TevKonstColorIndexes[2].B, mat.TevKonstColorIndexes[2].A);
            stream.AppendFormat("kColor[3] = vec4({0},{1},{2},{3});\n", mat.TevKonstColorIndexes[3].R, mat.TevKonstColorIndexes[3].G, mat.TevKonstColorIndexes[3].B, mat.TevKonstColorIndexes[3].A);


            stream.Append("\tvec4 c0 = color[0], c1 = color[1], c2 = color[2], prev = color[3];\n" +
                        "\tvec4 rastemp = vec4(0,0,0,0), textemp = vec4(0,0,0,0), konsttemp = vec4(0,0,0,0);\n" +
                        "\tvec3 comp16 = vec3(1, 256, 0), comp24 = vec3(1, 256, 256*256);\n" + // Uhh
                        "\tfloat alphabump=0;\n" +
                        "\tvec3 tevcoord=vec3(0,0,0);\n" +
                        //"\tvec2 wrappedcoord=vec2(0,0), tempcoord=vec2(0,0);\n" +
                        "\tvec4 tevin_a = vec4(0, 0, 0, 0), tevin_b = vec4(0, 0, 0, 0), tevin_c = vec4(0, 0, 0, 0), tevin_d = vec4(0, 0, 0, 0);\n");


            // Cannot assign to input variables in GLSL so we copy them to a local instance instead.
            stream.AppendFormat("\tvec4 col0 = colors_0;\n");
            stream.AppendFormat("\tvec4 col1 = colors_1;\n");
            stream.AppendLine();


            // Write up to 16 TEV Stage Operations
            for (int i = 0; i < data.NumTevStages[mat.NumTevStagesIndex]; i++)
                WriteStage(stream, i, mat, data);

            // Alpha Compare
            WriteAlphaTest(stream, mat, data);
            //WriteFog(stream, mat, data);

            stream.AppendLine("\tPixelColor = prev;");

            stream.AppendLine("}");
            stream.AppendLine();

            return stream.ToString();
        }

        private static void WriteStage(StringBuilder stream, int stageIndex, Material mat, MAT3 data)
        {
            // Basic TEV Operation:
            // Inputs: [0 <= A, B, C <= 255] [-1024 <= D <= 1023]
            // Lerp between A and B using C as the interpolation factor. Optionally negate the result 
            // using op. Input D and a bias (0, +0.5, -0.5) are added to the result. Then a constant
            // scale (1, 2, 4, 0.5) is applied. Result is optionally clamped before being written to 
            // an output buffer. 

            TevStage tevStage = data.TevStageInfos[mat.TevStageInfoIndexes[stageIndex]];
            TevOrder tevOrder = data.TevOrderInfos[mat.TevOrderInfoIndexes[stageIndex]];
            stream.AppendFormat("\t// TEV Stage {0}\n", stageIndex);
            stream.AppendFormat("\t// Unknown0: {0} ColorInA: {1} ColorInB: {2} ColorInC: {3} ColorInD: {4} ColorOp: {5} ColorBias: {6} ColorScale: {7} ColorClamp: {8} ColorRegId: {9}\n", tevStage.Unknown0, tevStage.ColorIn[0], tevStage.ColorIn[1], tevStage.ColorIn[2], tevStage.ColorIn[3], tevStage.ColorOp, tevStage.ColorBias, tevStage.ColorScale, tevStage.ColorClamp, tevStage.ColorRegId);
            stream.AppendFormat("\t// AlphaInA: {0} AlphaInB: {1} AlphaInC: {2} AlphaInD: {3} AlphaOp: {4} AlphaBias: {5} AlphaScale: {6} AlphaClamp: {7} AlphaRegId: {8} Unknown1: {9}\n", tevStage.AlphaIn[0], tevStage.AlphaIn[1], tevStage.AlphaIn[2], tevStage.AlphaIn[3], tevStage.AlphaOp, tevStage.AlphaBias, tevStage.AlphaScale, tevStage.AlphaClamp, tevStage.AlphaRegId, tevStage.Unknown1);
            stream.AppendFormat("\t// Tev Order TexCoordId: {0} TexMap: {1} ChannelId: {2}\n", tevOrder.TexCoordId, tevOrder.TexMap, tevOrder.ChannelId);

            TevSwapMode swapMode = data.TevSwapModeInfos[mat.TevSwapModeIndexes[stageIndex]];
            TevSwapModeTable rasSwapTable = data.TevSwapModeTables[swapMode.RasSel];
            TevSwapModeTable texSwapTable = data.TevSwapModeTables[swapMode.TexSel];
            stream.AppendFormat("\t// TEV Swap Mode: RasSel: {0} TexSel: {1}\n", swapMode.RasSel, swapMode.TexSel);
            stream.AppendFormat("\t// Ras Swap Table: R: {0} G: {1} B: {2} A: {3}\n", rasSwapTable.R, rasSwapTable.G, rasSwapTable.B, rasSwapTable.A);
            stream.AppendFormat("\t// Tex Swap Table: R: {0} G: {1} B: {2} A: {3}\n", texSwapTable.R, texSwapTable.G, texSwapTable.B, texSwapTable.A);

            int texcoord = (int)tevOrder.TexCoordId;
            bool bHasTexCoord = (int)tevOrder.TexCoordId < data.NumTexGens[mat.NumTexGensIndex];

            // Build a Swap Mode for swapping texture color input/rasterized color input to the TEV Stage.
            string[] rasSwapModeTable = new string[4];
            string swapColors = "rgba";
            for (int i = 0; i < 4; i++)
            {
                char[] swapTable = new char[4];

                swapTable[0] = swapColors[rasSwapTable.R];
                swapTable[1] = swapColors[rasSwapTable.G];
                swapTable[2] = swapColors[rasSwapTable.B];
                swapTable[3] = swapColors[rasSwapTable.A];
                rasSwapModeTable[i] = new string(swapTable);
            }

            string[] texSwapModeTable = new string[4];
            for (int i = 0; i < 4; i++)
            {
                char[] swapTable = new char[4];

                swapTable[0] = swapColors[texSwapTable.R];
                swapTable[1] = swapColors[texSwapTable.G];
                swapTable[2] = swapColors[texSwapTable.B];
                swapTable[3] = swapColors[texSwapTable.A];
                texSwapModeTable[i] = new string(swapTable);
            }


            // ToDo: Implement Indirect Stages

            // If our TEV Stage uses rasterized alpha or color inputs
            if (tevStage.ColorIn[0] == GXCombineColorInput.RasAlpha || tevStage.ColorIn[0] == GXCombineColorInput.RasColor ||
                tevStage.ColorIn[1] == GXCombineColorInput.RasAlpha || tevStage.ColorIn[1] == GXCombineColorInput.RasColor ||
                tevStage.ColorIn[2] == GXCombineColorInput.RasAlpha || tevStage.ColorIn[2] == GXCombineColorInput.RasColor ||
                tevStage.ColorIn[3] == GXCombineColorInput.RasAlpha || tevStage.ColorIn[3] == GXCombineColorInput.RasColor ||
                tevStage.AlphaIn[0] == GXCombineAlphaInput.RasAlpha || tevStage.AlphaIn[1] == GXCombineAlphaInput.RasAlpha ||
                tevStage.AlphaIn[2] == GXCombineAlphaInput.RasAlpha || tevStage.AlphaIn[3] == GXCombineAlphaInput.RasAlpha)
            {
                stream.AppendFormat("\t// TEV Swap Mode\n");
                stream.AppendFormat("\trastemp = {0}.{1};\n", m_tevRasTable[(int)tevOrder.ChannelId], rasSwapModeTable[rasSwapTable.A]); // ToDo: No idea if this works.
            }

            if(tevOrder.TexCoordId != GXTexCoordSlot.Null)
            {
                int texmap = tevOrder.TexMap;
                if(true /* !bHasIndStage*/)
                {
                    if (bHasTexCoord)
                        stream.AppendFormat("\ttevcoord.xy = TexGen{0}.xy;\n", texcoord);
                    else
                        stream.AppendFormat("\ttevcoord.xy = vec2(0, 0);\n"); 
                }

                string texswap = texSwapModeTable[texSwapTable.A]; // Again, no idea if this works.

                stream.Append("\ttextemp = ");
                SampleTexture(stream, "vec2(tevcoord.xy)", texswap, texmap);
            }
            else
            {
                stream.AppendFormat("\ttextemp = vec4(1,1,1,1); // tevOrder specified no texture!\n");
            }

            if(tevStage.ColorIn[0] == GXCombineColorInput.Konst || tevStage.ColorIn[1] == GXCombineColorInput.Konst ||
               tevStage.ColorIn[2] == GXCombineColorInput.Konst || tevStage.ColorIn[3] == GXCombineColorInput.Konst ||
               tevStage.AlphaIn[0] == GXCombineAlphaInput.Konst || tevStage.AlphaIn[1] == GXCombineAlphaInput.Konst ||
               tevStage.AlphaIn[2] == GXCombineAlphaInput.Konst || tevStage.AlphaIn[3] == GXCombineAlphaInput.Konst)
            {
                GXKonstColorSel kc = mat.KonstColorSelectorIndexes[stageIndex];
                GXKonstAlphaSel ka = mat.KonstAlphaSelectorIndexes[stageIndex];
                stream.AppendFormat("\t// KonstColorSel: {0} KonstAlphaSel: {1}\n", kc, ka);
                stream.AppendFormat("\tkonsttemp = vec4({0}, {1});\n", m_tevKSelTableC[(int)kc], m_tevKSelTableA[(int)ka]);
            }

            stream.AppendFormat("\ttevin_a = vec4({0}, {1});\n", m_tevCInputTable[(int)tevStage.ColorIn[0]], m_tevAInputTable[(int)tevStage.AlphaIn[0]]);
            stream.AppendFormat("\ttevin_b = vec4({0}, {1});\n", m_tevCInputTable[(int)tevStage.ColorIn[1]], m_tevAInputTable[(int)tevStage.AlphaIn[1]]);
            stream.AppendFormat("\ttevin_c = vec4({0}, {1});\n", m_tevCInputTable[(int)tevStage.ColorIn[2]], m_tevAInputTable[(int)tevStage.AlphaIn[2]]);
            stream.AppendFormat("\ttevin_d = vec4({0}, {1});\n", m_tevCInputTable[(int)tevStage.ColorIn[3]], m_tevAInputTable[(int)tevStage.AlphaIn[3]]);

            // COLOR COMBINER
            stream.AppendFormat("\t// Color Combine\n");
            stream.AppendFormat("\t{0} = clamp(", m_tevCOutputTable[tevStage.ColorRegId]);
            if(tevStage.ColorOp == GXTevOp.Add || tevStage.ColorOp == GXTevOp.Sub)
            {
                WriteTevRegular(stream, "rgb", tevStage.ColorBias, tevStage.ColorOp, tevStage.ColorScale, tevStage.ColorClamp);
            }
            else
            {
                string[] opTable = new string[]
                {
                    "((tevin_a.r > tevin_b.r) ?  tevin_c.rgb : vec3(0,0,0))",                                // GXTevOp::Comp_R8_GT
                    "((tevin_a.r == tevin_b.r) ? tevin_c.rgb : vec3(0,0,0))",                                // GXTevOp::Comp_R8_EQ
                    "((dot(tevin_a.rgb, comp16) >  dot(tevin_b.rgb, comp16)) ? tevin_c.rgb : vec3(0,0,0))",  // GXTevOp::Comp_GR16_GT
                    "((dot(tevin_a.rgb, comp16) == dot(tevin_b.rgb, comp16)) ? tevin_c.rgb : vec3(0,0,0))",  // GXTevOp::Comp_GR16_EQ
                    "((dot(tevin_a.rgb, comp24) >  dot(tevin_b.rgb, comp24)) ? tevin_c.rgb : vec3(0,0,0))",  // GXTevOp::Comp_GR24_GT
                    "((dot(tevin_a.rgb, comp24) == dot(tevin_b.rgb, comp24)) ? tevin_c.rgb : vec3(0,0,0))",  // GXTevOp::Comp_GR24_EQ
                    "(max(sign(tevin_a.rgb - tevin_b.rgb), vec3(0,0,0)) * tevin_c.rgb)",                     // GXTevOp::Comp_RGB8_GT
                    "((vec3(1,1,1) - sign(abs(tevin_a.rgb - tevin_b.rgb))) * tevin_c.rgb)",                  // GXTevOp::Comp_RGB8_EQ
                };

                int index = (int)tevStage.ColorOp - 8;
                stream.AppendFormat("tevin_d.rgb + {0}", opTable[index]);
            }

            if (tevStage.ColorClamp)
                stream.AppendFormat(", vec3(0,0,0), vec3(1,1,1))");
            else
                stream.AppendFormat(", vec3(-1024, -1024, -1024), vec3(1023, 1023, 1023))");
            stream.AppendFormat(";\n");

            // ALPHA COMBINER
            stream.AppendFormat("\t// Alpha Combine\n");
            stream.AppendFormat("\t{0} = clamp(", m_tevAOutputTable[tevStage.AlphaRegId]);
            if (tevStage.AlphaOp == GXTevOp.Add || tevStage.AlphaOp == GXTevOp.Sub)
            {
                WriteTevRegular(stream, "a", tevStage.AlphaBias, tevStage.AlphaOp, tevStage.AlphaScale, tevStage.AlphaClamp);
            }
            else
            {
                string[] opTable = new string[]
                {
                    "((tevin_a.r > tevin_b.r) ?  tevin_c.a : 0)",                                   // GXTevOp::Comp_R8_GT
                    "((tevin_a.r == tevin_b.r) ? tevin_c.a : 0)",                                   // GXTevOp::Comp_R8_EQ
                    "((dot(tevin_a.rgb, comp16) >  dot(tevin_b.rgb, comp16)) ? tevin_c.a : 0)",     // GXTevOp::Comp_GR16_GT
                    "((dot(tevin_a.rgb, comp16) == dot(tevin_b.rgb, comp16)) ? tevin_c.a : 0)",     // GXTevOp::Comp_GR16_EQ
                    "((dot(tevin_a.rgb, comp24) >  dot(tevin_b.rgb, comp24)) ? tevin_c.a : 0)",     // GXTevOp::Comp_GR24_GT
                    "((dot(tevin_a.rgb, comp24) == dot(tevin_b.rgb, comp24)) ? tevin_c.a : 0)",     // GXTevOp::Comp_GR24_EQ
                    "((tevin_a.a >  tevin_b.a) ? tevin_c.a : 0)",                                   // GXTevOp::Comp_RGB8_GT
                    "((tevin_a.a == tevin_b.a) ? tevin_c.a : 0)",                                   // GXTevOp::Comp_RGB8_EQ
                };

                int index = (int)tevStage.AlphaOp - 8;
                stream.AppendFormat("tevin_d.a + {0}", opTable[index]);
            }

            if (tevStage.AlphaClamp)
                stream.AppendFormat(", 0, 1)");
            else
                stream.AppendFormat(", -1024, 1023)");
            stream.AppendFormat(";\n");
            stream.AppendLine();
        }

        private static void SampleTexture(StringBuilder stream, string texCoords, string texSwap, int texMap)
        {
            stream.AppendFormat("texture(Texture{0}, {1}.xy).{2};\n", texMap, texCoords, texSwap);
        }

        private static void WriteTevRegular(StringBuilder stream, string components, GXTevBias bias, GXTevOp op, GXTevScale scale, bool clamp)
        {
            string[] tevScale = new[]
            {
                "* 1",      // GXTevScale::Scale_1
                "* 2",      // GXTevScale::Scale_2
                "* 4",      // GXTevScale::Scale_4
                "* 0.5"     // GXTevScale::Divide_2
            };

            string[] tevBias = new[]
            {
                "",         // GXTevBias::Zero
                " + 0.5",   // GXTevBias::AddHalf
                " - 0.5",   // GXTevBias::SubHalf
                ""
            };

            string[] tevOpTable = new[]
            {
                "+",    // GXTevOp::Add
                "-",    // GXTevOp::Sub
            };

            // Regular TEV stage: ((d + bias) + lerp(a,b,c)) *scale
            // lerp(a, b, c) op (d + bias) * scale
            //stream.AppendFormat("(mix(tevin_b.{0}, tevin_a.{0}, tevin_c.{0})", components); // lerp(a, b, c)
            stream.AppendFormat("((tevin_a.{0} * (vec4(1,1,1,1).{0} - tevin_c.{0}) + tevin_b.{0}  * tevin_c.{0})", components);
            stream.AppendFormat(" {0} ", tevOpTable[(int)op]); // "+/-" (op)
            stream.AppendFormat("(tevin_d.{0}{1})", components, tevBias[(int)bias]); // (d + bias)
            stream.AppendFormat("{0})", tevScale[(int)scale]);
        }

        private static string[] m_tevAlphaFuncTable = new[]
        {
            "(false)",              // NEVER
            "(prev.a < {0})",       // LESS THAN
            "(prev.a == {0})",      // EQUAL
            "(prev.a <= {0})",      // LESS THAN OR EQUAL
            "(prev.a > {0})",       // GREATER THAN
            "(prev.a != {0})",      // NOT EQUAL
            "(prev.a >= {0})",      // GREATER THAN OR EQUAL
            "(true)"                // ALWAYS
        };

        private static string[] m_tevAlphaFuncLogicTable = new[]
        {
            " && ",
            " || ",
            " != ",
            " == ",
        };

        private static void WriteAlphaTest(StringBuilder stream, Material mat, MAT3 data)
        {
            string[] alphaRef = new[]
            {
                "alphaRef.r",
                "alphaRef.g"
            };

            AlphaCompare aCompare = data.AlphaCompares[mat.AlphaCompareIndex];
            stream.AppendFormat("\t// Alpha Compare: Compare A: {0} Reference A: {1} Op: {2} Compare B: {3} Reference B: {4}\n", aCompare.Comp0, aCompare.Reference0, aCompare.Operation, aCompare.Comp1, aCompare.Reference1);

            stream.Append("\tif(!( ");
            stream.AppendFormat(m_tevAlphaFuncTable[(int)aCompare.Comp0], (aCompare.Reference0/255f)); // LHS
            stream.AppendFormat("{0}", m_tevAlphaFuncLogicTable[(int)aCompare.Operation]); // Logic Operation
            stream.AppendFormat(m_tevAlphaFuncTable[(int)aCompare.Comp1], (aCompare.Reference1/255f));

            stream.Append(")) {\n");
            stream.Append("\t\tdiscard;\n");
            stream.Append("\t\treturn;\n");
            stream.Append("\t}\n");
        }

    }
}
