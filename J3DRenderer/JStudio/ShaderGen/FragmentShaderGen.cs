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

            stream.AppendFormat("kColor[0] = vec4({0},{1},{2},{3});\n", data.TevKonstColors[mat.TevKonstColorIndexes[0]].R, data.TevKonstColors[mat.TevKonstColorIndexes[0]].G, data.TevKonstColors[mat.TevKonstColorIndexes[0]].B, data.TevKonstColors[mat.TevKonstColorIndexes[0]].A);
            stream.AppendFormat("kColor[1] = vec4({0},{1},{2},{3});\n", data.TevKonstColors[mat.TevKonstColorIndexes[1]].R, data.TevKonstColors[mat.TevKonstColorIndexes[1]].G, data.TevKonstColors[mat.TevKonstColorIndexes[1]].B, data.TevKonstColors[mat.TevKonstColorIndexes[1]].A);
            stream.AppendFormat("kColor[2] = vec4({0},{1},{2},{3});\n", data.TevKonstColors[mat.TevKonstColorIndexes[2]].R, data.TevKonstColors[mat.TevKonstColorIndexes[2]].G, data.TevKonstColors[mat.TevKonstColorIndexes[2]].B, data.TevKonstColors[mat.TevKonstColorIndexes[2]].A);
            stream.AppendFormat("kColor[3] = vec4({0},{1},{2},{3});\n", data.TevKonstColors[mat.TevKonstColorIndexes[3]].R, data.TevKonstColors[mat.TevKonstColorIndexes[3]].G, data.TevKonstColors[mat.TevKonstColorIndexes[3]].B, data.TevKonstColors[mat.TevKonstColorIndexes[3]].A);


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
            stream.AppendLine("    // Alpha Compare Test");
            AlphaCompare alphaCompare = data.AlphaCompares[mat.AlphaCompareIndex];
            string alphaOp;
            switch (alphaCompare.Operation)
            {
                case GXAlphaOp.And: alphaOp = "&&"; break;
                case GXAlphaOp.Or: alphaOp = "||"; break;
                case GXAlphaOp.XOR: alphaOp = "^"; break; // Not really tested, unsupported in some examples but I don't see why.
                case GXAlphaOp.XNOR: alphaOp = "=="; break;  // Not really tested. ^
                default:
                    Console.WriteLine("Unsupported alpha compare operation: {0}", alphaCompare.Operation);
                    alphaOp = "||";
                    break;
            }

            // clip(result.a < 0.5 && result a > 0.2 ? -1 : 1)
            string ifContents = string.Format("(!({0} {1} {2}))",
                GetCompareString(alphaCompare.Comp0, m_tevOutputRegs[0] + ".a", alphaCompare.Reference0),
                alphaOp,
                GetCompareString(alphaCompare.Comp1, m_tevOutputRegs[0] + ".a", alphaCompare.Reference1));

            // clip equivelent
            stream.AppendLine("    // Alpha Compare (Clip)");
            stream.AppendLine(string.Format("\tif{0}\n\t\tdiscard;", ifContents));

            //string output = "PixelColor = texCol0" + (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Color0) ? " * Color0;" : ";");
            //stream.AppendLine(output);
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

        private static bool IsNewTexCombo(int texMap, int texCoordId, bool[] oldCombos)
        {
            int index = (texMap << 4 | texCoordId);
            if (oldCombos[index])
                return false;

            oldCombos[index] = true;
            return true;
        }

        private static string GetVertColorString(TevOrder orderInfo)
        {
            switch (orderInfo.ChannelId)
            {
                case GXColorChannelId.Color0: return "Color0.rgb";
                case GXColorChannelId.Color1: return "Color1.rgb";
                case GXColorChannelId.Alpha0: return "Color0.aaaa";
                case GXColorChannelId.Alpha1: return "Color1.aaaa";
                case GXColorChannelId.Color0A0: return "Color0.rgba";
                case GXColorChannelId.Color1A1: return "Color1.rgba";
                case GXColorChannelId.ColorZero: return "vec4(0f, 0f, 0f, 0f)";
                case GXColorChannelId.AlphaBump:
                case GXColorChannelId.AlphaBumpN:
                case GXColorChannelId.ColorNull:
                default:
                    Console.WriteLine("Unsupported ChannelId: {0}", orderInfo.ChannelId);
                    return "vec4(0f, 1f, 0f, 1f)";
            }
        }

        private static string GetSwapModeSwizzleString(TevSwapModeTable table)
        {
            char[] swizzleChars = new[] { 'r', 'g', 'b', 'a' };
            return string.Format(".{0}{1}{2}{3}", swizzleChars[table.R], swizzleChars[table.G], swizzleChars[table.B], swizzleChars[table.A]);
        }

        private static string GetTexTapString(TevOrder info)
        {
            return string.Format("texCol{0}", (int)info.TexCoordId);
        }

        private static string GetCompareString(GXCompareType compare, string a, byte refVal)
        {
            string outStr = "";
            float fRef = refVal / 255f;

            if (compare != GXCompareType.Always)
                Console.WriteLine("Untested alpha-test functionality: {0}", compare);

            switch (compare)
            {
                case GXCompareType.Never: outStr = "false"; break;
                case GXCompareType.Less: outStr = "<"; break;
                case GXCompareType.Equal: outStr = "=="; break;
                case GXCompareType.LEqual: outStr = "<="; break;
                case GXCompareType.Greater: outStr = ">"; break;
                case GXCompareType.NEqual: outStr = "!="; break;
                case GXCompareType.GEqual: outStr = ">="; break;
                case GXCompareType.Always: outStr = "true"; break;
                default:
                    Console.WriteLine("Invalid comparison function, defaulting to always.");
                    outStr = "true";
                    break;
            }

            if (outStr == "false" || outStr == "true")
                return outStr;

            return string.Format("{0} {1} {2}", a, outStr, fRef);
        }

        private static string GetColorInString(GXCombineColorInput inputType, GXKonstColorSel konst, TevOrder texMapping)
        {
            switch (inputType)
            {
                case GXCombineColorInput.ColorPrev: return m_tevOutputRegs[0] + ".rgb";
                case GXCombineColorInput.AlphaPrev: return m_tevOutputRegs[0] + ".aaa";
                case GXCombineColorInput.C0: return m_tevOutputRegs[1] + ".rgb";
                case GXCombineColorInput.A0: return m_tevOutputRegs[1] + ".aaa";
                case GXCombineColorInput.C1: return m_tevOutputRegs[2] + ".rgb";
                case GXCombineColorInput.A1: return m_tevOutputRegs[2] + ".aaa";
                case GXCombineColorInput.C2: return m_tevOutputRegs[3] + ".rgb";
                case GXCombineColorInput.A2: return m_tevOutputRegs[3] + ".aaa";
                case GXCombineColorInput.TexColor: return GetTexTapString(texMapping) + ".rgb";
                case GXCombineColorInput.TexAlpha: return GetTexTapString(texMapping) + ".aaa";
                case GXCombineColorInput.RasColor: return GetVertColorString(texMapping) + ".rgb";
                case GXCombineColorInput.RasAlpha: return GetVertColorString(texMapping) + ".aaa";
                case GXCombineColorInput.One: return "vec3(1f, 1f, 1f)";
                case GXCombineColorInput.Half: return "vec3(0.5f, 0.5f, 0.5f)";
                case GXCombineColorInput.Konst: return GetKonstColorString(konst) + ".rgb";
                case GXCombineColorInput.Zero: return "vec3(0f, 0f, 0f)";
                default:
                    Console.WriteLine("Unknown Color Input type: {0}", inputType);
                    return "vec3(0f, 0f, 0f)";
            }
        }

        private static string GetAlphaInString(GXCombineAlphaInput inputType, GXKonstAlphaSel konst, TevOrder texMapping)
        {
            switch (inputType)
            {
                case GXCombineAlphaInput.AlphaPrev: return m_tevOutputRegs[0] + ".a";
                case GXCombineAlphaInput.A0: return m_tevOutputRegs[1] + ".a";
                case GXCombineAlphaInput.A1: return m_tevOutputRegs[2] + ".a";
                case GXCombineAlphaInput.A2: return m_tevOutputRegs[3] + ".a";
                case GXCombineAlphaInput.TexAlpha: return GetTexTapString(texMapping) + ".a";
                case GXCombineAlphaInput.RasAlpha: return GetVertColorString(texMapping) + ".a";
                case GXCombineAlphaInput.Konst: return GetKonstAlphaString(konst);
                case GXCombineAlphaInput.Zero: return "0.0f";
                default:
                    Console.WriteLine("Unknown Alpha Input type: {0}", inputType);
                    return "0.0f";
            }
        }

        private static string GetKonstAlphaString(GXKonstAlphaSel konst)
        {
            switch (konst)
            {
                case GXKonstAlphaSel.KASel_1: return "1.0";
                case GXKonstAlphaSel.KASel_7_8: return "0.875";
                case GXKonstAlphaSel.KASel_3_4: return "0.75";
                case GXKonstAlphaSel.KASel_5_8: return "0.625";
                case GXKonstAlphaSel.KASel_1_2: return "0.5";
                case GXKonstAlphaSel.KASel_3_8: return "0.375";
                case GXKonstAlphaSel.KASel_1_4: return "0.25";
                case GXKonstAlphaSel.KASel_1_8: return "0.125";
                case GXKonstAlphaSel.KASel_K0_R: return "konst0.r";
                case GXKonstAlphaSel.KASel_K1_R: return "konst1.r";
                case GXKonstAlphaSel.KASel_K2_R: return "konst2.r";
                case GXKonstAlphaSel.KASel_K3_R: return "konst3.r";
                case GXKonstAlphaSel.KASel_K0_G: return "konst0.g";
                case GXKonstAlphaSel.KASel_K1_G: return "konst1.g";
                case GXKonstAlphaSel.KASel_K2_G: return "konst2.g";
                case GXKonstAlphaSel.KASel_K3_G: return "konst3.g";
                case GXKonstAlphaSel.KASel_K0_B: return "konst0.b";
                case GXKonstAlphaSel.KASel_K1_B: return "konst1.b";
                case GXKonstAlphaSel.KASel_K2_B: return "konst2.b";
                case GXKonstAlphaSel.KASel_K3_B: return "konst3.b";
                case GXKonstAlphaSel.KASel_K0_A: return "konst0.a";
                case GXKonstAlphaSel.KASel_K1_A: return "konst1.a";
                case GXKonstAlphaSel.KASel_K2_A: return "konst2.a";
                case GXKonstAlphaSel.KASel_K3_A: return "konst3.a";
                default:
                    Console.WriteLine("Unsupported GXKonstAlphaSel: {0}, returning 1.0", konst);
                    return "1.0";
            }
        }

        private static string GetKonstColorString(GXKonstColorSel konst)
        {
            switch (konst)
            {
                case GXKonstColorSel.KCSel_1: return "vec4(1f, 1f, 1f, 1f)";
                case GXKonstColorSel.KCSel_7_8: return "vec4(0.875f, 0.875f, 0.875f, 0.875f)";
                case GXKonstColorSel.KCSel_3_4: return "vec4(0.75f, 0.75f, 0.75f, 0.75f)";
                case GXKonstColorSel.KCSel_5_8: return "vec4(0.625f, 0.625f, 0.625f, 0.625f)";
                case GXKonstColorSel.KCSel_1_2: return "vec4(0.5f, 0.5f, 0.5f, 0.5f)";
                case GXKonstColorSel.KCSel_3_8: return "vec4(0.375f, 0.375f, 0.375f, 0.375f)";
                case GXKonstColorSel.KCSel_1_4: return "vec4(0.25f, 0.25f, 0.25f, 0.25f)";
                case GXKonstColorSel.KCSel_1_8: return "vec4(0.125f, 0.125f, 0.125f, 0.125f)";
                case GXKonstColorSel.KCSel_K0: return "konst0.rgba";
                case GXKonstColorSel.KCSel_K1: return "konst1.rgba";
                case GXKonstColorSel.KCSel_K2: return "konst2.rgba";
                case GXKonstColorSel.KCSel_K3: return "konst3.rgba";
                case GXKonstColorSel.KCSel_K0_R: return "konst0.rrrr";
                case GXKonstColorSel.KCSel_K1_R: return "konst1.rrrr";
                case GXKonstColorSel.KCSel_K2_R: return "konst2.rrrr";
                case GXKonstColorSel.KCSel_K3_R: return "konst3.rrrr";
                case GXKonstColorSel.KCSel_K0_G: return "konst0.gggg";
                case GXKonstColorSel.KCSel_K1_G: return "konst1.gggg";
                case GXKonstColorSel.KCSel_K2_G: return "konst2.gggg";
                case GXKonstColorSel.KCSel_K3_G: return "konst3.gggg";
                case GXKonstColorSel.KCSel_K0_B: return "konst0.bbbb";
                case GXKonstColorSel.KCSel_K1_B: return "konst1.bbbb";
                case GXKonstColorSel.KCSel_K2_B: return "konst2.bbbb";
                case GXKonstColorSel.KCSel_K3_B: return "konst3.bbbb";
                case GXKonstColorSel.KCSel_K0_A: return "konst0.aaaa";
                case GXKonstColorSel.KCSel_K1_A: return "konst1.aaaa";
                case GXKonstColorSel.KCSel_K2_A: return "konst2.aaaa";
                case GXKonstColorSel.KCSel_K3_A: return "konst3.aaaa";
                default:
                    Console.WriteLine("Unsupported GXKonstColorSel: {0}, returning 1.0", konst);
                    return "1.0";
            }
        }

        private static string GetColorOpString(GXTevOp op, GXTevBias bias, GXTevScale scale, bool clamp, byte outputRegIndex, string[] colorInputs)
        {
            string channelSelect = ".rgb";
            string dest = m_tevOutputRegs[outputRegIndex] + channelSelect;
            StringBuilder sb = new StringBuilder();

            switch (op)
            {
                case GXTevOp.Add:
                case GXTevOp.Sub:
                    {
                        // out_color = (d + lerp(a, b, c)); - Add
                        // out_color = (d - lerp(a, b, c)); - Sub
                        string compareOp = (op == GXTevOp.Add) ? "+" : "-";
                        sb.AppendLine(string.Format("{0} = ({1} {5} mix({2}, {3}, {4}));", dest, colorInputs[3], colorInputs[0], colorInputs[2], colorInputs[1], compareOp));
                        sb.AppendLine(GetModString(outputRegIndex, bias, scale, clamp, false));
                    }
                    break;
                case GXTevOp.Comp_R8_GT:
                case GXTevOp.Comp_R8_EQ:
                    {
                        // out_color = (d + ((a.r > b.r) ? c : 0));
                        string compareOp = (op == GXTevOp.Comp_R8_GT) ? ">" : "==";
                        sb.AppendLine(string.Format("{0} = ({1} + (({2}.r {5} {3}.r) ? {4} : 0))", dest, colorInputs[3], colorInputs[0], colorInputs[1], colorInputs[2], compareOp));
                    }
                    break;
                case GXTevOp.Comp_GR16_GT:
                case GXTevOp.Comp_GR16_EQ:
                    {
                        // out_color = (d + (dot(a.gr, rgTo16Bit) > dot(b.gr, rgTo16Bit) ? c : 0));
                        string compareOp = (op == GXTevOp.Comp_GR16_GT) ? ">" : "==";
                        string rgTo16Bit = "vec2(255.0/65535.6, 255.0 * 256.0/65535.0)";
                        sb.AppendLine(string.Format("{0} = ({1} + (dot({2}.gr, {3}) {4} dot({5}.gr, {3}) ? {6} : 0));",
                            dest, colorInputs[3], colorInputs[0], rgTo16Bit, compareOp, colorInputs[1], colorInputs[2]));
                    }
                    break;
                case GXTevOp.Comp_BGR24_GT:
                case GXTevOp.Comp_BGR24_EQ:
                    {
                        // out_color = (d + (dot(a.bgr, bgrTo24Bit) > dot(b.bgr, bgrTo24Bit) ? c : 0));
                        string compareOp = (op == GXTevOp.Comp_BGR24_GT) ? ">" : "==";
                        string bgrTo24Bit = "vec3(255.0/16777215.0, 255.0 * 256.0/16777215.0, 255.0*65536.0/16777215.0)";
                        sb.AppendLine(string.Format("{0} = ({1} + (dot({2}.bgr, {5}) {6} dot({3}.bgr, {5}) ? {4} : 0));",
                            dest, colorInputs[3], colorInputs[0], colorInputs[1], colorInputs[2], bgrTo24Bit, compareOp));
                    }
                    break;
                case GXTevOp.Comp_RGB8_GT:
                case GXTevOp.Comp_RGB8_EQ:
                    {
                        // out_color.r = d.r + ((a.r > b.r) ? c.r : 0);
                        // out_color.g = d.g + ((a.g > b.g) ? c.g : 0);
                        // out_color.b = d.b + ((a.b > b.b) ? c.b : 0);
                        string compareOp = (op == GXTevOp.Comp_RGB8_GT) ? ">" : "==";
                        string format = "{0}.{6} = {1}.{6} + (({2}.{6} {5} {3}.{6}) ? {4}.{6} : 0);";

                        sb.AppendLine(string.Format(format, dest, colorInputs[3], colorInputs[0], colorInputs[1], colorInputs[2], compareOp, "r"));
                        sb.AppendLine(string.Format(format, dest, colorInputs[3], colorInputs[0], colorInputs[1], colorInputs[2], compareOp, "g"));
                        sb.AppendLine(string.Format(format, dest, colorInputs[3], colorInputs[0], colorInputs[1], colorInputs[2], compareOp, "b"));
                    }
                    break;
                default:
                    Console.WriteLine("Unsupported Color Op: {0}!", op);
                    sb.AppendLine("// Invalid Color op for TEV broke here.");
                    break;
            }

            if (op > GXTevOp.Sub)
            {
                //if(bias != 3 || scale != 0 || clamp != 1)
                // warn(unexpected bias, scale, clamp)...?
            }

            return sb.ToString();
        }

        private static string GetAlphaOpString(GXTevOp op, GXTevBias bias, GXTevScale scale, bool clamp, byte outputRegIndex, string[] alphaInputs)
        {
            string channelSelect = ".a";
            string dest = m_tevOutputRegs[outputRegIndex] + channelSelect;
            StringBuilder sb = new StringBuilder();

            switch (op)
            {
                case GXTevOp.Add:
                case GXTevOp.Sub:
                    {
                        // out_color = (d + lerp(a, b, c)); - Add
                        // out_color = (d - lerp(a, b, c)); - Sub
                        string compareOp = (op == GXTevOp.Add) ? "+" : "-";
                        sb.AppendLine(string.Format("{0} = ({1} {5} mix({2}, {3}, {4}));", dest, alphaInputs[3], alphaInputs[0], alphaInputs[1], alphaInputs[2], compareOp));
                        sb.AppendLine(GetModString(outputRegIndex, bias, scale, clamp, true));
                    }
                    break;
                case GXTevOp.Comp_A8_EQ:
                case GXTevOp.Comp_A8_GT:
                    {
                        // out_color = (d + ((a.a > b.a) ? c : 0))
                        string compareOp = (op == GXTevOp.Comp_R8_GT) ? ">" : "==";
                        sb.AppendLine(string.Format("{0} = ({1} + (({2} {5} {3}) ? {4} : 0))", dest, alphaInputs[3], alphaInputs[0], alphaInputs[1], alphaInputs[2], compareOp));
                    }
                    break;

                default:
                    Console.WriteLine("Unsupported op in GetAlphaOpString: {0}", op);
                    sb.AppendLine("// Invalid Alpha op for TEV broke here.");
                    break;

            }

            if (op == GXTevOp.Comp_A8_GT || op == GXTevOp.Comp_A8_EQ)
            {
                // if(bias != 3 || scale != 1 || clamp != 1)
                // warn unexpected bias/scale/etc
            }

            return sb.ToString();
        }

        private static string GetModString(byte outputRegIndex, GXTevBias bias, GXTevScale scale, bool clamp, bool isAlpha)
        {
            float biasVal = 0f;
            float scaleVal = 1f;

            switch (bias)
            {
                case GXTevBias.Zero: biasVal = 0f; break;
                case GXTevBias.AddHalf: biasVal = 0.5f; break;
                case GXTevBias.SubHalf: biasVal = -0.5f; break;
            }

            switch (scale)
            {
                case GXTevScale.Scale_1: scaleVal = 1f; break;
                case GXTevScale.Scale_2: scaleVal = 2f; break;
                case GXTevScale.Scale_4: scaleVal = 4f; break;
                case GXTevScale.Divide_2: scaleVal = 0.5f; break;
            }

            // If we're not modifying it, early out.
            if (scaleVal == 1f && biasVal == 0f && !clamp)
                return "";

            string channelSelect = isAlpha ? ".a" : ".rgb";
            string dest = m_tevOutputRegs[outputRegIndex] + channelSelect;
            StringBuilder sb = new StringBuilder();

            if (scaleVal == 1f && biasVal == 0f)
            {
                // result = saturate(result)
                sb.AppendLine(string.Format("{0} = clamp({0},0.0,1.0);", dest));
            }
            else
            {
                // result = saturate(result * scale + bias * scale)
                sb.AppendLine(string.Format("{0} = clamp({0} * {1} + {2} * {1},0.0, 1.0);", dest, scaleVal, biasVal));
            }

            return sb.ToString();
        }
    }
}
