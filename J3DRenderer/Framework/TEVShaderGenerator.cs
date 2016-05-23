using J3DRenderer.JStudio;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using System.Text;
using WindEditor;

namespace J3DRenderer.Framework
{
    public static class TEVShaderGenerator
    {
        public static Shader GenerateShader(Material fromMat, MAT3 data)
        {
            Shader shader = new Shader(fromMat.Name);
            bool success = false;
            {
                string vertexShader = GenerateVertexShader(fromMat, data);
                success = shader.CompileSource(vertexShader, ShaderType.VertexShader);
            }
            if (success)
            {
                //string fragmentShader = GenerateFragmentShader(shader, fromMat, data);
                //success = shader.CompileSource(fragmentShader, ShaderType.FragmentShader);
            }

            if (success)
                // Well, we compiled both the Vertex and the Fragment shader succesfully, let's try to link them together now.
                success = shader.LinkShader();

            if (!success)
            {
                Console.WriteLine("Failed to generate shader for material: {0}", fromMat.Name);
                shader.ReleaseResources();

                // Generate a Fallback Shader for rendering
                shader = new Shader("UnlitTexture");
                shader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.vert"), ShaderType.VertexShader);
                shader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.frag"), ShaderType.FragmentShader);
                shader.LinkShader();

                return shader;
            }

            return shader;
        }

        private static string GenerateVertexShader(Material mat, MAT3 data)
        {
            StringBuilder stream = new StringBuilder();

            // Shader Header
            stream.AppendLine("#version 330 core");
            stream.AppendLine("// Automatically Generated File. All changes will be lost.");
            stream.AppendLine();

            // Examine the attributes the mesh has so we can ensure the shader knows about the incoming data.
            // I don't think this is technically right, but meh.
            stream.AppendLine("// Per-Vertex Input");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Position)) stream.AppendLine("in vec3 RawPosition;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Normal)) stream.AppendLine("in vec3 RawNormal;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Color0)) stream.AppendLine("in vec4 RawColor0;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Color1)) stream.AppendLine("in vec4 RawColor1;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex0)) stream.AppendLine("in vec2 RawTex0;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex1)) stream.AppendLine("in vec2 RawTex1;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex2)) stream.AppendLine("in vec2 RawTex2;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex3)) stream.AppendLine("in vec2 RawTex3;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex4)) stream.AppendLine("in vec2 RawTex4;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex5)) stream.AppendLine("in vec2 RawTex5;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex6)) stream.AppendLine("in vec2 RawTex6;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Tex7)) stream.AppendLine("in vec2 RawTex7;");
            stream.AppendLine();

            stream.AppendLine("// Output (Interpolated)");
            stream.AppendLine();

            // TEV uses up to 4 channels to accumulate the result of Per-Vertex Lighting/Material/Ambient lighting.
            // Color0, Alpha0, Color1, and Alpha1 are the four possible channel names.
            stream.AppendFormat("// NumChannelControls: {0}\n", data.NumChannelControls[mat.NumChannelControlsIndex]);
            for (int i = 0; i < data.NumChannelControls[mat.NumChannelControlsIndex]; i++)
            {
                bool isAlphaChannel = i % 2 != 0;
                if (!isAlphaChannel) stream.AppendFormat("out vec3 Color{0};\n", i / 2); else stream.AppendFormat("out float Alpha{0};\n", i / 2);
            }
            stream.AppendLine();

            // TEV can generate up to 16 (?) sets of Texture Coordinates by taking an incoming data value (UV, POS, NRM, BINRM, TNGT) and transforming it by a matrix.
            stream.AppendFormat("// NumTexGens: {0}\n", data.NumTexGens[mat.NumTexGensIndex]);
            for (int i = 0; i < data.NumTexGens[mat.NumTexGensIndex]; i++)
                stream.AppendFormat("out vec3 Tex{0};\n", i);
            stream.AppendLine();

            // Declare shader Uniforms coming in from the CPU.
            stream.AppendLine("// Uniforms");
            stream.AppendLine
                (
                "uniform mat4 ModelMtx;\n" +
                "uniform mat4 ViewMtx;\n" +
                "uniform mat4 ProjMtx;\n" +
                "\n" +
                "uniform mat4 TexMtx[10];\n" +
                "uniform mat4 PostMtx[20];\n" +
                "uniform vec4 COLOR0_Amb;\n" +
                "uniform vec4 COLOR0_Mat;\n" +
                "uniform vec4 COLOR1_Mat;\n" +
                "uniform vec4 COLOR1_Amb;\n"
                //"\n" +
                //"    struct GXLight\n" +
                //"    {\n" +
                //"        vec3 Position;\n" +
                //"        vec3 Direction;\n" +
                //"        vec4 Color;\n" +
                //"        vec4 DistAtten;\n" +
                //"        vec4 AngleAtten;\n" +
                //"    };\n" +
                //"\n" +
                //"    GXLight Lights[8];\n" +
                //"    uniform int NumLights;\n" +
                //"    uniform vec4 ambLightColor;\n"
                );
            stream.AppendLine();

            // Main Shader Code
            stream.AppendLine("// Main Vertex Shader");
            stream.AppendLine("void main()\n{");
            stream.AppendLine("\tmat4 MVP = ProjMtx * ViewMtx * ModelMtx;");
            stream.AppendLine("\tmat4 MV = ViewMtx * ModelMtx;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Position)) stream.AppendLine("\tgl_Position = MVP * vec4(RawPosition, 1);");
            stream.AppendLine();

            stream.AppendFormat("\t// Ambient Colors & Material Colors. {0} Ambient Colors, {1} Material Colors.\n", mat.AmbientColorIndexes.Length, mat.MaterialColorIndexes.Length);

            // Upload Ambient Colors specified by the material
            for (int i = 0; i < mat.AmbientColorIndexes.Length; i++)
            {
                var ambColor = data.AmbientColors[mat.AmbientColorIndexes[i]];
                stream.AppendFormat("\tvec4 ambColor{0} = vec4({1},{2},{3},{4});\n", i, ambColor.R, ambColor.G, ambColor.B, ambColor.A);
            }

            // Upload Material Colors specified by the material
            for (int i = 0; i < mat.MaterialColorIndexes.Length; i++)
            {
                var matColor = data.MaterialColors[mat.MaterialColorIndexes[i]];
                stream.AppendFormat("\tvec4 matColor{0} = vec4({1},{2},{3},{4});\n", i, matColor.R, matColor.G, matColor.B, matColor.A);
            }
            stream.AppendLine();

            // TEV Channel Colors.
            // A vertex can have two colors each (Color0, Color1) and each color has two channels - RGB and A. This gives us
            // up to 4 channels, color0, color1, alpha0, and alpha1. Channels are associated with an ambient color/alpha which can
            // come from a variety of sources - vertex colors, or special ambient and material registers. The register colors
            // are set in GX via the command: GXSetChanAmbColor(GXChanneLID chan, GXColor amb_color), and GXSetChanMatColor(GXChannelID chan, GXColor mat_color);
            // Now, the source for each channel can be controlled by another command: 
            // GXSetChanCtrl(GXCHannelID chan, bool enable, GXColorSrc amb_src, GXColorSrc mat_src, GXLightID light_mask, GXDiffuseFn diff_fn, GXAttnFn attn_fn);
            // 
            // If the lighting channel is disabled, then the material color for that channel is passed through unmodified. The mat_src parameter specifies if the
            // material color comes from the Vertex Color, or from the Material Register. If the channel is enabled, then lighting needs to be computed for each light
            // enabled in the light_mask.
            stream.AppendFormat("\t// {0} Channel Controllers.\n", data.NumChannelControls[mat.NumChannelControlsIndex]);
            for (int i = 0; i < data.NumChannelControls[mat.NumChannelControlsIndex]; i++)
            {
                ColorChannelControl channelControl = data.ColorChannelControls[mat.ColorChannelControlIndexes[i]];
                stream.AppendFormat("\t// Channel Control: {0} - LightingEnabled: {1} MaterialSrc: {2} LightMask: {3} DiffuseFn: {4} AttenuationFn: {5} AmbientSrc: {6}\n",
                    i, channelControl.LightingEnabled, channelControl.MaterialSrc, channelControl.LitMask, channelControl.DiffuseFunction, channelControl.AttenuationFunction, channelControl.AmbientSrc);

                string swizzle, channel;
                switch (i)
                {
                    case /* Color0 */ 0: channel = "0"; swizzle = ".rgb"; break;
                    case /* Alpha0 */ 1: channel = "0"; swizzle = ".a"; break;
                    case /* Color1 */ 2: channel = "1"; swizzle = ".rgb"; break;
                    case /* Alpha1 */ 3: channel = "1"; swizzle = ".a"; break;
                    default: Console.WriteLine("Unknown Color Channel Control Index: {0}", i); continue;
                }

                bool isAlphaChannel = i % 2 != 0;
                string channelTarget = string.Format("{0}{1}", isAlphaChannel ? "Alpha" : "Color", channel);
                string ambColor = (channelControl.AmbientSrc == GXColorSrc.Vertex ? "RawColor" : "ambColor") + channel + swizzle;
                string matColor = (channelControl.MaterialSrc == GXColorSrc.Vertex ? "RawColor" : "matColor") + channel + swizzle;

                #region 0
                // When no local diffuse *light* is applied to an object, the color is equal to the ambient pre-lit color which is simply: "pre_lit_clr * amb_scale"
                // When a light is shining on the object, the percentage of pre-lit color is increased until when the light is brightest, where the full value of pre-lit color is used.
                // lit_clr = pre_lit_clr * (amb_scale + diff_scale * other_attn * diff_lit_color)
                // amb_scfale + diff_scale = 1.0

                //string illumination = GetLightFunctionForColorChannel(...); /* clamp( ambi + SignedInt([0-7]E LightMaski(j)Atteni(j)DiffuseAtteni(j)Color );*/
                //string diffuseAtten = GetDiffuseAttenuationForColorChannel(...); /* 1f if GX_DF_NONE, N Dot L if DX_DF_SIGN, Clamp0(N Dot L) if GX_DF_CLAMP */
                //dl(j) = sqrt(Lj*Lj) || GX_AF_SPOT
                //string aattl(j) = Clamp0(lj*Ldir) || GX_AF_SPOT
                //string atten = ??; /* GX_AF_NONE ? 1 : (Clamp0(a2, AAtti(j)^2 + aljAAtt(j) +a0j)/ k2dl(j)^2+k1jdl(j)+k0j. WHAT THE FUCK.*/

                ////string aatt;
                ////switch (channelControl.AttenuationFunction)
                ////{
                ////    case GXAttenuationFn.None: aatt = "float aatt = 1.0;"; break;
                ////    case GXAttenuationFn.Spec: aatt = string.Format("float aatt = (dot(RawNormal, lightDir) >= 0.0) ? max(0.0, dot(RawNormal, {0}.Direction.xyz)) : 0.0;\n", < lightIndex >); break;
                ////    case GXAttenuationFn.Spot: ??;
                ////        break;
                ////    default:
                ////        break;
                ////}


                ////string attenuationParams = string.Format("float a0 = {0}; float a1 = {1}; float a2 = {2}; float k0 = {3}; float k1 = {4}; float k2 = {5};", 0f, 0f, 1f, 0.5f, 0f, 1 - 0.5f); //ToDo: Get these values from Wind Waker
                ////string aattn = "float aattn = clamp(dot("
                ////string attenuation = "clamp(a2^2 * aattn^2 + a1 * aattn + a0) / (k2 * d^2 + k1 * d + k0, 0, 1);";
                ////string lighting_sum = "vec4 lighting_sum = "



                ////string illum = string.Format("clamp({0} + lighting_sum, 0, 1);\n", ambColor);
                ////string lightFunc = channelControl.LightingEnabled ? illum : "1"; // ToDo: Equation 12
                ////stream.AppendFormat("\t{0} = {1}*{2};\n", channelTarget, matColor, lightFunc); // Equation 9-10
                ////stream.AppendLine();
                ////stream.AppendLine();
                #endregion

                // see: https://github.com/dolphin-emu/dolphin/blob/master/Source/Core/VideoCommon/LightingShaderGen.h#L61
                // From the Top:
                // Ambient = GX_SRC_REG ? AmbientRegister : VertexColor;
                // Material = GX_SRC_REG ? MaterialRegister : VertexColor;
                // Pre-declare: float lightAccum, float3 lightDir, h, cosAttn, distATtn, float dist, dist2, attn
                // lightAccum = 0f; 
                // for(int i = 0; i < 8; i++)
                //    if(LIGHTMASK_ENABLED(i))
                //      dist2 = dot(LIGHT_DIR(i), LIGHT_DIR(i));
                //      dist = sqrt(dist2);
                //      switch(LIGHT_ATTN_FUNC)
                //      case NONE: aatt = 1.0
                //      case SPEC: aatt = max(??? toDo:)
                //      case SPOT: aatt = ???
                //      light_attenuation = clamp(a2^2 * aatt^2 + a1 * aatt + a0) / (k2 * d^2 + k1 * d + k0, 0, 1);
                //      Atten = LIGHT_ATTEN_FUNC == NONE ? 1 : light_attenuation;
                //      DiffuseAtten = DIFFUSE_ATTN_FUNC == NONE ? 1.0 : DIFFUSE_ATTN_FUNC == SIGN ? dot(RawNormal, LIGHT_DIR(i)) : clamp(dot(RawNormal, LIGHT_DIR(i)), 0, 1);
                //      lightAccum += Atten * DiffuseAtten * LIGHT_COLOR(i); <- This dedepends on the Diffuse Function. 
                //
                // Illum = clamp(Ambient + lightAccum, 0, 1);
                // LightFunc = LIGHTING_ENABLED ? Illum : 1.0
                // ChannelColor = Material * LightFunc <-- Final Results
            }

            // TEV "TexGen" Texture Coordinate Generation
            // TEV can generate texture coordinates on the fly from a variety of sources. The various ways all follow the form of:
            // dst_coord = func(src_param, mtx) - that is, the destination coordinate is generated by multiplying an input source by a 2x4 or 3x4 matrix.
            // The input coordinates can come from one of the following locations: TEX0-7, POS, NRM, BINRM, TANGENT.
            // GX has a default set of texture matrices (GXTexMtx enum).
            stream.AppendFormat("\t// {0} Texture Coordinate Generators.\n", data.NumTexGens[mat.NumTexGensIndex]);
            for (int i = 0; i < data.NumTexGens[mat.NumTexGensIndex]; i++)
            {
                TexCoordGen texGen = data.TexGenInfos[mat.TexGenInfoIndexes[i]];
                stream.AppendFormat("\t// TexGen: {0} Type: {1} Source: {2} TexMatrixIndex: {3}\n", i, texGen.Type, texGen.Source, texGen.TexMatrixSource);
                // https://github.com/dolphin-emu/dolphin/blob/master/Source/Core/VideoCommon/VertexShaderGen.cpp#L190
                string texGenSource;
                switch (texGen.Source)
                {
                    case GXTexGenSrc.Position: texGenSource = "vec4(RawPosition.xyz, 1.0)"; break;
                    case GXTexGenSrc.Normal: texGenSource = "vec4(RawNormal.xyz, 1.0)"; break;
                    case GXTexGenSrc.TexCoord0: texGenSource = "vec4(RawTex0.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord1: texGenSource = "vec4(RawTex1.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord2: texGenSource = "vec4(RawTex2.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord3: texGenSource = "vec4(RawTex3.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord4: texGenSource = "vec4(RawTex4.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord5: texGenSource = "vec4(RawTex5.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord6: texGenSource = "vec4(RawTex6.xy, 1.0, 1.0)"; break; // There's no TexCoord7 for some reason.
                    case GXTexGenSrc.Color0: texGenSource = "RawColor0"; break;
                    case GXTexGenSrc.Color1: texGenSource = "RawColor1"; break;
                    case GXTexGenSrc.Binormal: texGenSource = "vec4(RawBinormal.xyz, 1.0)"; break; 
                    case GXTexGenSrc.Tangent: texGenSource = "vec4(RawTangent.xyz, 1.0)"; break;

                    // Honestly, I have no idea what this means. Do they ever get used? Do we need to perform a texture sample? If so, with what uvs?
                    // I'm pretty sure this applies to texture maps, ie: tells you to sample a texture?
                    case GXTexGenSrc.Tex0: 
                    case GXTexGenSrc.Tex1:
                    case GXTexGenSrc.Tex2:
                    case GXTexGenSrc.Tex3:
                    case GXTexGenSrc.Tex4:
                    case GXTexGenSrc.Tex5:
                    case GXTexGenSrc.Tex6:
                    case GXTexGenSrc.Tex7:
                    default: Console.WriteLine("Unsupported TexGenSrc: {0}, defaulting to TexCoord0.", texGen.Source); texGenSource = "RawTex0"; break;
                }

                // TEV Texture Coordinate generation takes the general form:
                // dst_coord = func(src_param, mtx), where func is GXTexGenType, src_param is GXTexGenSrc, and mtx is GXTexMtx.
                string destCoord = string.Format("TexGen{0}", i);

                switch (texGen.Type)
                {
                    case GXTexGenType.Matrix3x4:
                    case GXTexGenType.Matrix2x4:
                        // destCoord = {texGenSource} * texMtx[texGen.TexMatrixSource]
                    case GXTexGenType.SRTG:
                        // destCoord = vec4({texGenSource}.rg, 1, 1) * texMtx[texGen.TexMatrixSource]
                    case GXTexGenType.Bump0:
                    case GXTexGenType.Bump1:
                    case GXTexGenType.Bump2:
                    case GXTexGenType.Bump3:
                    case GXTexGenType.Bump4:
                    case GXTexGenType.Bump5:
                    case GXTexGenType.Bump6:
                    case GXTexGenType.Bump7:
                    default:
                        Console.WriteLine("Unsupported TexGenType: {0}", texGen.Type); break;
                }
            }

            // Append the tail end of our shader file.
            stream.AppendLine("}");
            stream.AppendLine();

            Directory.CreateDirectory("ShaderDump");
            File.WriteAllText("ShaderDump/" + mat.Name + ".vert", stream.ToString());
            return stream.ToString();
        }
    }
}
