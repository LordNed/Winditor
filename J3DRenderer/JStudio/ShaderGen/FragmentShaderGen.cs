using J3DRenderer.JStudio;
using System.IO;
using System.Text;
using System;

namespace J3DRenderer.ShaderGen
{
    public static class FragmentShaderGen
    {
        private const string kColors = "color";
        private const string kKColors = "k";

        private static readonly string[] m_tevCOutputTable = new[] { "prev.rgb", "c0.rgb", "c1.rgb", "c2.rgb" };
        private static readonly string[] m_tevAOutputTable = new[] { "prev.a", "c0.a", "c1.a", "c2.a" };

        public static string GenerateFragmentShader(Material mat, MAT3 data)
        {
            StringBuilder stream = new StringBuilder();

            // Shader Header
            stream.AppendLine("// Automatically Generated File. All changes will be lost.");
            stream.AppendLine("#version 330 core");
            stream.AppendFormat("// {0} TEV stages, {1} Texgens, {2} IND stages.\n", data.NumTevStages[mat.NumTevStagesIndex], data.NumTexGens[mat.NumTexGensIndex], data.IndirectTextures[mat.UnknownIndex2].IndTexStageNum);
            stream.AppendLine();

            // Configure inputs to match vertex shader outputs
            if (mat.VtxDesc.AttributeIsEnabled(WindEditor.ShaderAttributeIds.Position))
                stream.AppendLine("in vec3 Position;");
            if (mat.VtxDesc.AttributeIsEnabled(WindEditor.ShaderAttributeIds.Normal))
                stream.AppendLine("in vec3 Normal;");

            // TEV uses up to 4 channels to accumulate the result of Per-Vertex Lighting/Material/Ambient lighting.
            // Color0, Alpha0, Color1, and Alpha1 are the four possible channel names.
            stream.AppendFormat("// NumChannelControls: {0}\n", data.NumChannelControls[mat.NumChannelControlsIndex]);
            for (int i = 0; i < data.NumChannelControls[mat.NumChannelControlsIndex]; i++)
            {
                bool isAlphaChannel = i % 2 != 0;
                if (!isAlphaChannel) stream.AppendFormat("in vec3 Color{0};\n", i / 2); else stream.AppendFormat("in float Alpha{0};\n", i / 2);
            }
            stream.AppendLine();

            // TEV can generate up to 16 (?) sets of Texture Coordinates by taking an incoming data value (UV, POS, NRM, BINRM, TNGT) and transforming it by a matrix.
            stream.AppendFormat("// NumTexGens: {0}\n", data.NumTexGens[mat.NumTexGensIndex]);
            for (int i = 0; i < data.NumTexGens[mat.NumTexGensIndex]; i++)
                stream.AppendFormat("in vec3 TexGen{0};\n", i);
            stream.AppendLine();

            stream.AppendLine("// Final Output");
            stream.AppendLine("out vec4 PixelColor;");

            // Texture Inputs
            stream.AppendLine("SAMPLER_BINDING(0) uniform sampler2DArray samp[8];");

            // Write some other variables or some shit
            stream.AppendFormat(
                "vec4 {0}[4];\n" + // TEV Registers(?)
                "vec4 {1}[4];\n", kColors, kKColors); // Konst Colors

            stream.AppendLine("void main()\n{");

            // More variables!
            stream.AppendFormat("\tvec4 c0 = {0}[1], c1 = {0}[2], c2 = {0}[3], prev = {0}[0];\n", kColors);
            stream.AppendFormat("\tvec4 rastemp = vec4(0,0,0,0), textemp = vec4(0,0,0,0), konsttemp = vec4(0,0,0,0);\n");
            stream.AppendFormat("\tvec4 tevin_a=vec4(0,0,0,0), tevin_b=vec4(0,0,0,0), tevin_c=vec4(0,0,0,0), tevin_d=vec4(0,0,0,0);\n\n"); // TEV Combiner Inputs

            // Meh.
            stream.AppendFormat("\t// Num Tev Stages: {0}\n", data.NumTevStages[mat.NumTevStagesIndex]);
            for (int i = 0; i < data.NumTevStages[mat.NumTevStagesIndex]; i++)
                WriteTEVStage(stream, mat, data, i);

            // The result of the last texenv stage are drawn on the screen, regardless of the used destination register.
            stream.AppendFormat("\tPixelColor = prev;\n");

            Directory.CreateDirectory("ShaderDump");
            File.WriteAllText("ShaderDump/" + mat.Name + ".vert", stream.ToString());
            return stream.ToString();
        }

        private static void WriteTEVStage(StringBuilder stream, Material mat, MAT3 data, int n)
        {
            stream.AppendFormat("\n\t// TEV Stage {0}\n", n);
            TevOrder tevOrder = data.TevOrderInfos[mat.TevOrderInfoIndexes[n]];
            TevStage tevStage = data.TevStageInfos[mat.TevStageInfoIndexes[n]];
            TevSwapMode tevSwap = data.TevSwapModeInfos[mat.TevSwapModeIndexes[n]];
            TevSwapModeTable rasTable = data.TevSwapModeTables[tevSwap.RasSel];
            TevSwapModeTable texTable = data.TevSwapModeTables[tevSwap.TexSel];

            stream.AppendFormat("/t // Rasterization Swap Table: {0}\n", rasTable);

        }
    }
}
