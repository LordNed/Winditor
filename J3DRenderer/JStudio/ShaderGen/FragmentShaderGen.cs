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
            "result",
            "color0",
            "color1",
            "color2"
        };


        public static string GenerateFragmentShader(Material mat, MAT3 data)
        {
            StringBuilder stream = new StringBuilder();

            // Shader Header
            stream.AppendLine("#version 330 core");
            stream.AppendLine();

            // Configure inputs to match our outputs from VS
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Position))
                stream.AppendLine("in vec3 Position;");

            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Normal))
                stream.AppendLine("in vec3 Normal;");

            for (int i = 0; i < data.NumChannelControls[mat.NumChannelControlsIndex]; i++)
                stream.AppendLine(string.Format("in vec4 Color{0};", i));

            for (int texGen = 0; texGen < data.NumTexGens[mat.NumTexGensIndex]; texGen++)
                stream.AppendLine(string.Format("in vec3 TexGen{0};", texGen));

            stream.AppendLine();

            // Final Output
            stream.AppendLine("// Final Output");
            stream.AppendLine("out vec4 PixelColor;");

            // Texture Inputs
            for(int i = 0; i < 8; i++)
            {
                stream.AppendFormat("uniform sampler2D Texture{0};\n", i);
            }

            // Main Function
            stream.AppendLine("void main()");
            stream.AppendLine("{");

            // Default initial values of the TEV registers.
            // ToDo: Does this need swizzling? themikelester has it marked as mat.registerColor[i==0?3:i-1]]
            stream.AppendLine("    // Initial TEV Register Values");
            for (int i = 0; i < 4; i++)
            {
                var tevColor = data.TevColors[mat.TevColorIndexes[i]];
                stream.AppendLine(string.Format("    vec4 {0} = vec4({1}, {2}, {3}, {4});", m_tevOutputRegs[i], tevColor.R, tevColor.G, tevColor.B, tevColor.A));
            }
            stream.AppendLine();

            // Constant Color Registers
            stream.AppendLine("    // Konst TEV Colors");
            for (int i = 0; i < 4; i++)
            {
                var konstColor = data.TevKonstColors[mat.TevKonstColorIndexes[i]];
                stream.AppendLine(string.Format("    vec4 konst{0} = vec4({1}, {2}, {3}, {4});", i, konstColor.R, konstColor.G, konstColor.B, konstColor.A));
            }
            stream.AppendLine();

            // Texture Samples
            bool[] oldCombos = new bool[256];
            for (int i = 0; i < data.NumTevStages[mat.NumTevStagesIndex];  i++)
            {
                TevOrder order = data.TevOrderInfos[mat.TevOrderInfoIndexes[i]];
                int tex = order.TexMap;
                GXTexCoordSlot coord = order.TexCoordId;

                // This TEV probably doesn't use textures.
                if (tex == 0xFF || coord == GXTexCoordSlot.Null)
                    continue;

                if (IsNewTexCombo(tex, (int)coord, oldCombos))
                {
                    string swizzle = ""; // Uhh I don't know if we need to swizzle since everyone's been converted into ARGB
                    stream.AppendLine(string.Format("    vec4 texCol{0} = texture(Texture{0}, TexGen{1}.xy){2};", tex, (int)coord, swizzle));
                }
            }
            stream.AppendLine();

            // ToDo: Implement indirect texturing.
            stream.AppendLine("    // TEV Stages");
            stream.AppendLine();
            stream.AppendLine();

            for (int i = 0; i < data.NumTevStages[mat.NumTevStagesIndex]; i++)
            {
                stream.AppendLine(string.Format("    // TEV Stage {0}", i));
                TevOrder order = data.TevOrderInfos[mat.TevOrderInfoIndexes[i]];
                TevStage stage = data.TevStageInfos[mat.TevStageInfoIndexes[i]];

                TevSwapMode swap = data.TevSwapModeInfos[mat.TevSwapModeIndexes[i]];
                TevSwapModeTable rasTable = data.TevSwapModeTables[swap.RasSel];
                TevSwapModeTable texTable = data.TevSwapModeTables[swap.TexSel];

                // There's swapping involved in the ras table.
                stream.AppendLine(string.Format("    // Rasterization Swap Table: {0}", rasTable));
                if (!(rasTable.R == 0 && rasTable.G == 1 && rasTable.B == 2 && rasTable.A == 3))
                {
                    stream.AppendLine(string.Format("    {0} = {1}{2};", GetVertColorString(order), GetVertColorString(order), GetSwapModeSwizzleString(rasTable)));
                }
                stream.AppendLine();


                // There's swapping involved in the texture table.
                stream.AppendLine(string.Format("    // Texture Swap Table: {0}", texTable));
                if (!(texTable.R == 0 && texTable.G == 1 && texTable.B == 2 && texTable.A == 3))
                {
                    stream.AppendLine(string.Format("    {0} = {1}{2};", GetTexTapString(order), GetTexTapString(order), GetSwapModeSwizzleString(rasTable)));
                }
                stream.AppendLine();

                string[] colorInputs = new string[4];
                colorInputs[0] = GetColorInString(stage.ColorIn[0], mat.KonstColorSelectorIndexes[i], order);
                colorInputs[1] = GetColorInString(stage.ColorIn[1], mat.KonstColorSelectorIndexes[i], order);
                colorInputs[2] = GetColorInString(stage.ColorIn[2], mat.KonstColorSelectorIndexes[i], order);
                colorInputs[3] = GetColorInString(stage.ColorIn[3], mat.KonstColorSelectorIndexes[i], order);

                stream.AppendLine("    // Color and Alpha Operations");
                stream.AppendLine(string.Format("    {0}", GetColorOpString(stage.ColorOp, stage.ColorBias, stage.ColorScale, stage.ColorClamp, stage.ColorRegId, colorInputs)));

                string[] alphaInputs = new string[4];
                alphaInputs[0] = GetAlphaInString(stage.AlphaIn[0], mat.KonstAlphaSelectorIndexes[i], order);
                alphaInputs[1] = GetAlphaInString(stage.AlphaIn[1], mat.KonstAlphaSelectorIndexes[i], order);
                alphaInputs[2] = GetAlphaInString(stage.AlphaIn[2], mat.KonstAlphaSelectorIndexes[i], order);
                alphaInputs[3] = GetAlphaInString(stage.AlphaIn[3], mat.KonstAlphaSelectorIndexes[i], order);

                stream.AppendLine(string.Format("    {0}", GetAlphaOpString(stage.AlphaOp, stage.AlphaBias, stage.AlphaScale, stage.AlphaClamp, stage.AlphaRegId, alphaInputs)));
                stream.AppendLine();
            }
            stream.AppendLine();

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
            stream.AppendLine(string.Format("    if{0}\n\t\tdiscard;", ifContents));

            //string output = "PixelColor = texCol0" + (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Color0) ? " * Color0;" : ";");
            //stream.AppendLine(output);
            stream.AppendLine(string.Format("    PixelColor = {0};", m_tevOutputRegs[0]));

            stream.AppendLine("}");
            stream.AppendLine();

            Directory.CreateDirectory("ShaderDump");
            File.WriteAllText("ShaderDump/" + mat.Name + ".frag", stream.ToString());
            return stream.ToString();
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
