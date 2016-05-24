using J3DRenderer.JStudio;
using System.IO;
using System.Text;

namespace J3DRenderer.ShaderGen
{
    public static class FragmentShaderGen
    {
        public static string GenerateVertexShader(Material mat, MAT3 data)
        {
            StringBuilder stream = new StringBuilder();

            // Shader Header
            stream.AppendLine("// Automatically Generated File. All changes will be lost.");
            stream.AppendLine("#version 330 core");
            stream.AppendLine();


            Directory.CreateDirectory("ShaderDump");
            File.WriteAllText("ShaderDump/" + mat.Name + ".vert", stream.ToString());
            return stream.ToString();
        }
    }
}
