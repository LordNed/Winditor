using System;
using System.Text;
using WindEditor;

namespace JStudio.J3D.ShaderGen
{
    public static class VertexShaderGen
    {
        public static string GenerateVertexShader(Material mat, MAT3 data)
        {
            StringBuilder stream = new StringBuilder();

            // Shader Header
            stream.AppendLine("// Automatically Generated File. All changes will be lost.");
            stream.AppendLine("#version 330 core");
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
            stream.AppendFormat("out vec4 colors_0;\n");
            stream.AppendFormat("out vec4 colors_1;\n");
            stream.AppendLine();

            // TEV can generate up to 16 (?) sets of Texture Coordinates by taking an incoming data value (UV, POS, NRM, BINRM, TNGT) and transforming it by a matrix.
            stream.AppendFormat("// NumTexGens: {0}\n", data.NumTexGens[mat.NumTexGensIndex]);
            for (int i = 0; i < data.NumTexGens[mat.NumTexGensIndex]; i++)
                stream.AppendFormat("out vec3 TexGen{0};\n", i);
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
                "uniform vec4 COLOR1_Amb;\n" +
                "\n" +
                "struct GXLight\n" +
                "{\n" +
                "    vec4 Position;\n" +
                "    vec4 Direction;\n" +
                "    vec4 Color;\n" +
                "    vec4 CosAtten; //AngleAtten\n" + // 1.875000, 0, 0 ?
                "    vec4 DistAtten;\n" + // 1.875000, 0, 0 ?
                "};\n" +
                "\n" +
                "layout(std140) uniform LightBlock\n" +
                "{\n\tGXLight Lights[8];\n};\n"
                );
            stream.AppendLine();

            // Main Shader Code
            stream.AppendLine("// Main Vertex Shader");
            stream.AppendLine("void main()\n{");
            stream.AppendLine("\tmat4 MVP = ProjMtx * ViewMtx * ModelMtx;");
            stream.AppendLine("\tmat4 MV = ViewMtx * ModelMtx;");
            if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Position)) stream.AppendLine("\tgl_Position = MVP * vec4(RawPosition, 1);");
            stream.AppendLine();

            // Do Color Channel Fixups
            if(data.NumChannelControls[mat.NumChannelControlsIndex] < 2)
            {
                if (mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.Color1))
                    stream.AppendFormat("\tcolors_1 = RawColor1;\n");
                else
                    stream.AppendFormat("\tcolors_1 = vec4(1, 1, 1, 1);\n");
            }

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
            stream.AppendFormat("\t// {0} Channel Controller(s).\n", data.NumChannelControls[mat.NumChannelControlsIndex]);
            stream.AppendLine("\tvec4 ambColor = vec4(1,1,1,1);\n\tvec4 matColor = vec4(1,1,1,1);\n\tvec4 lightAccum = vec4(0,0,0,0);\n\tvec4 lightFunc;");
            stream.AppendLine("\tvec3 ldir; float dist; float dist2; float attn;"); // Declaring these all anyways in case we use lighting.
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
                string channelTarget = string.Format("colors_{0}", channel);
                string ambColor = (channelControl.AmbientSrc == GXColorSrc.Vertex ? "RawColor" : "ambColor") + channel;
                string matColor = (channelControl.MaterialSrc == GXColorSrc.Vertex ? "RawColor" : "matColor") + channel;

                stream.AppendFormat("\tambColor = {0};\n", ambColor);
                stream.AppendFormat("\tmatColor = {0};\n", matColor);

                for (int l = 0; l < 8; l++)
                {
                    bool isLit = channelControl.LitMask.HasFlag((GXLightMask)(1 << l));
                    stream.AppendFormat("// ChannelControl: {0} Light: {1} LitMask: {2}\n", i, l, isLit);
                    if (isLit)
                    {
                        GenerateLightVertexShader(stream, channelControl, l, swizzle, isAlphaChannel ? 1 : 3);
                    }
                }

                if (channelControl.LightingEnabled)
                    stream.AppendLine("\tvec4 illum = clamp(ambColor + lightAccum, 0, 1);");
                stream.AppendFormat("\tlightFunc = {0};\n", channelControl.LightingEnabled ? "illum" : "vec4(1.0, 1.0, 1.0, 1.0)");
                stream.AppendFormat("\t{0}{1} = (matColor * lightFunc){1};\n", channelTarget, swizzle);
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

                TexMatrixProjection matrixProj = TexMatrixProjection.TexProj_ST;
                if (texGen.TexMatrixSource != GXTexMatrix.Identity)
                {
                    matrixProj = data.TexMatrixInfos[(((int)texGen.TexMatrixSource) - 30) / 3].Projection;
                }

                stream.AppendFormat("\t// TexGen: {0} Type: {1} Source: {2} TexMatrixIndex: {3}\n", i, texGen.Type, texGen.Source, texGen.TexMatrixSource);
                stream.AppendLine("\t{"); // False scope block so we can re-declare variables
                string texGenSource;
                switch (texGen.Source)
                {
                    case GXTexGenSrc.Position: texGenSource = "vec4(RawPosition.xyz, 1.0)"; break;
                    case GXTexGenSrc.Normal: texGenSource = "vec4(RawNormal.xyz, 1.0)"; break;
                    case GXTexGenSrc.Tex0: texGenSource = "vec4(RawTex0.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Tex1: texGenSource = "vec4(RawTex1.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Tex2: texGenSource = "vec4(RawTex2.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Tex3: texGenSource = "vec4(RawTex3.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Tex4: texGenSource = "vec4(RawTex4.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Tex5: texGenSource = "vec4(RawTex5.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Tex6: texGenSource = "vec4(RawTex6.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Tex7: texGenSource = "vec4(RawTex7.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.Color0: texGenSource = "colors_0"; break;
                    case GXTexGenSrc.Color1: texGenSource = "colors_1"; break;
                    case GXTexGenSrc.Binormal: texGenSource = "vec4(RawBinormal.xyz, 1.0)"; break;
                    case GXTexGenSrc.Tangent: texGenSource = "vec4(RawTangent.xyz, 1.0)"; break;

                    // This implies using a texture coordinate set already generated by TEV.
                    case GXTexGenSrc.TexCoord0: texGenSource = "vec4(TexGen0.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord1: texGenSource = "vec4(TexGen1.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord2: texGenSource = "vec4(TexGen2.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord3: texGenSource = "vec4(TexGen3.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord4: texGenSource = "vec4(TexGen4.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord5: texGenSource = "vec4(TexGen5.xy, 1.0, 1.0)"; break;
                    case GXTexGenSrc.TexCoord6: texGenSource = "vec4(TexGen6.xy, 1.0, 1.0)"; break;
                    default: Console.WriteLine("Unsupported TexGenSrc: {0}, defaulting to TexCoord0.", texGen.Source); texGenSource = "RawTex0"; break;
                }

                // TEV Texture Coordinate generation takes the general form:
                // dst_coord = func(src_param, mtx), where func is GXTexGenType, src_param is GXTexGenSrc, and mtx is GXTexMtx.
                string destCoord = string.Format("TexGen{0}", i);
                switch (texGen.Type)
                {
                    case GXTexGenType.Matrix3x4:
                    case GXTexGenType.Matrix2x4:
                        //if(mat.VtxDesc.AttributeIsEnabled(ShaderAttributeIds.TexMtxId))
                        //{
                        //    stream.AppendFormat("int temp = {0}.z\n", destCoord);
                        //    if (texMtx.Projection == TexMatrixProjection.TexProj_STQ)
                        //        stream.AppendFormat("{0}.xyz = vec3(dot({1}, TexMtx[temp]), dot({1}, TexMtx[temp+1]), dot({1}, TexMtx[temp+2]));\n", destCoord, texGenSource);
                        //    else
                        //        stream.AppendFormat("{0}.xyz = vec3(dot({1}, TexMtx[temp]), dot({1}, TexMtx[temp+1]), 1);\n", destCoord, texGenSource);
                        //}
                        //else
                        {
                            if (matrixProj == TexMatrixProjection.TexProj_STQ)
                                //stream.AppendFormat("{0}.xyz = vec3(dot({1}, TexMtx[{2}]), dot({1}, TexMtx[{3}]), dot({1}, TexMtx[{4}]));\n", destCoord, texGenSource, 3 * i, 3 * i + 1, 3 * i + 2);
                                stream.AppendFormat("{0} = ({1} * TexMtx[{2}]).xyz;\n", destCoord, texGenSource, i);
                            else
                                //stream.AppendFormat("{0}.xyz = vec3(dot({1}, TexMtx[{2}]), dot({1}, TexMtx[{3}]), 1);\n", destCoord, texGenSource, 3 * i, 3 * i + 1);
                                stream.AppendFormat("{0} = ({1} * TexMtx[{2}]).xyz;\n", destCoord, texGenSource, i); // Untested, this might be a 2x4 multiply?
                        }
                        // destCoord = {texGenSource} * texMtx[texGen.TexMatrixSource]
                        break;
                    case GXTexGenType.SRTG:
                        stream.AppendFormat("\t{0} = vec3({1}.rg, 1);\n", destCoord, texGenSource); break;
                    case GXTexGenType.Bump0:
                    case GXTexGenType.Bump1:
                    case GXTexGenType.Bump2:
                    case GXTexGenType.Bump3:
                    case GXTexGenType.Bump4:
                    case GXTexGenType.Bump5:
                    case GXTexGenType.Bump6:
                    case GXTexGenType.Bump7:
                    // Transform the light dir into tangent space.
                    // ldir = normalize(Lights[{0}.Position.xyz - RawPosition.xyz);\n {0} = "texInfo.embosslightshift";
                    // destCoord = TexGen{0} + float3(dot(ldir, RawNormal), dot(ldir, RawBinormal), 0.0);\n", {0} = i, {1} = "texInfo.embosssourceshift";
                    default:
                        Console.WriteLine("Unsupported TexGenType: {0}", texGen.Type); break;
                }

                // Dual Tex Transforms
                if (data.TexMatrix2Infos.Count > 0)
                {
                    // ToDo: Should this just be... i? lol
                    Console.WriteLine("PostMtx transforms are... not really anything supported?");
                    TexMatrix postTexMtx = data.TexMatrix2Infos[((int)texGen.TexMatrixSource) - 30];
                    int postIndex = mat.PostTexMatrixIndexes[i];
                    stream.AppendFormat("float4 P0 = PostMtx[{0}];\n", postIndex);
                    stream.AppendFormat("float4 P1 = PostMtx[{0}];\n", postIndex + 1);
                    stream.AppendFormat("float4 P2 = PostMtx[{0}];\n", postIndex + 2);

                    stream.AppendFormat("{0}.xyz = vec3(dot(P0.xyz, {0}.xyz) + P0.w, dot(P1.xyz, {0}.xyz) + P1.w, dot(P2.xyz, {0}.xyz) + P2.w);\n", destCoord);
                }

                stream.AppendLine("\t}"); // End of false-scope block.
            }

            // Append the tail end of our shader file.
            stream.AppendLine("}");
            stream.AppendLine();
            return stream.ToString();
        }

        private static void GenerateLightVertexShader(StringBuilder stream, ColorChannelControl channelControl, int lightIndex, string lightAccumSwizzle, int numSwizzleComponents)
        {
            switch (channelControl.AttenuationFunction)
            {
                case GXAttenuationFunction.None:
                    stream.AppendFormat("\tldir = normalize(Lights[{0}].Position.xyz - RawPosition.xyz);\n", lightIndex);
                    stream.AppendLine("\tattn = 1.0;");
                    stream.AppendLine("\tif(length(ldir) == 0.0)\n\t\tldir = RawNormal;");
                    break;
                case GXAttenuationFunction.Spec:
                    stream.AppendFormat("\tldir = normalize(Lights[{0}].Position.xyz - RawPosition.xyz);\n", lightIndex);
                    stream.AppendFormat("\tattn = (dot(RawNormal, ldir) >= 0.0) ? max(0.0, dot(RawNormal, Lights[{0}].Direction.xyz)) : 0.0;\n", lightIndex);
                    stream.AppendFormat("\tcosAttn = Lights[{0}].CosAtten.xyz;\n", lightIndex);
                    stream.AppendFormat("\tdistAttn = {1}(Lights[{0}].DistAtten.xyz);\n", lightIndex, (channelControl.DiffuseFunction == GXDiffuseFunction.None) ? "" : "normalize");
                    stream.AppendFormat("\tattn = max(0.0f, dot(cosAttn, vec3(1.0, attn, attn*attn))) / dot(distAttn, vec3(1.0, attn, attn*attn));");
                    break;
                case GXAttenuationFunction.Spot:
                    stream.AppendFormat("\tldir = normalize(Lights[{0}].Position.xyz - RawPosition.xyz);\n", lightIndex);
                    stream.AppendLine("\tdist2 = dot(ldir, ldir);");
                    stream.AppendLine("\tdist = sqrt(dist2);");
                    stream.AppendLine("\tldir = ldir/dist;");
                    stream.AppendFormat("\tattn = max(0.0, dot(ldir, Lights[{0}].Direction.xyz));\n", lightIndex);
                    stream.AppendFormat("\tattn = max(0.0, Lights[{0}].CosAtten.x + Lights[{0}].CosAtten.y*attn + Lights[{0}].CosAtten.z*attn*attn) / dot(Lights[{0}].DistAtten.xyz, vec3(1.0, dist, dist2));\n", lightIndex);
                    break;
                default:
                    Console.WriteLine("Unsupported AttenuationFunction Value: {0}", channelControl.AttenuationFunction);
                    break;
            }

            switch (channelControl.DiffuseFunction)
            {
                case GXDiffuseFunction.None:
                    stream.AppendFormat("\tlightAccum{1} += attn * Lights[{0}].Color;\n", lightIndex, lightAccumSwizzle);
                    break;
                case GXDiffuseFunction.Signed:
                case GXDiffuseFunction.Clamp:
                    stream.AppendFormat("\tlightAccum{1} += attn * {2}dot(ldir, RawNormal)) * vec{3}(Lights[{0}].Color{1});\n",
                        lightIndex, lightAccumSwizzle, channelControl.DiffuseFunction != GXDiffuseFunction.Signed ? "max(0.0," : "(", numSwizzleComponents);
                    break;
                default:
                    Console.WriteLine("Unsupported DiffusenFunction Value: {0}", channelControl.AttenuationFunction);
                    break;
            }
            stream.AppendLine();
        }
    }
}
