using J3DRenderer.JStudio;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using WindEditor;

namespace J3DRenderer.ShaderGen
{
    public static class TEVShaderGenerator
    {
        private static bool m_allowCachedOverride = true;

        public static Shader GenerateShader(Material fromMat, MAT3 data)
        {
            Directory.CreateDirectory("ShaderDump");

            Shader shader = new Shader(fromMat.Name);
            bool success = false;
            {
                // Load it from the shader dump if it already exists, which allows us to hand-modify shaders.
                string filenameHash = string.Format("ShaderDump/{0}_{1}.vert", fromMat.Name, fromMat.GetHashCode());
                string vertexShader = File.Exists(filenameHash) && m_allowCachedOverride ? File.ReadAllText(filenameHash) : VertexShaderGen.GenerateVertexShader(fromMat, data);
                success = shader.CompileSource(vertexShader, ShaderType.VertexShader);

                File.WriteAllText(filenameHash, vertexShader);
            }
            if (success)
            {
                // Load it from the shader dump if it already exists, which allows us to hand-modify shaders.
                string filenameHash = string.Format("ShaderDump/{0}.frag", fromMat.Name);
                string fragmentShader = File.Exists(filenameHash) && m_allowCachedOverride ? File.ReadAllText(filenameHash) : FragmentShaderGen.GenerateFragmentShader(fromMat, data);
                success = shader.CompileSource(fragmentShader, ShaderType.FragmentShader);

                File.WriteAllText(filenameHash, fragmentShader);
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
                shader.CompileSource(File.ReadAllText("resources/shaders/UnlitColor.vert"), ShaderType.VertexShader);
                shader.CompileSource(File.ReadAllText("resources/shaders/UnlitColor.frag"), ShaderType.FragmentShader);
                shader.LinkShader();

                return shader;
            }

            return shader;
        }

        
    }
}
