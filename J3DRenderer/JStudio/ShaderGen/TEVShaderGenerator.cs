using J3DRenderer.JStudio;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using WindEditor;

namespace J3DRenderer.ShaderGen
{
    public static class TEVShaderGenerator
    {
        public static Shader GenerateShader(Material fromMat, MAT3 data)
        {
            Shader shader = new Shader(fromMat.Name);
            bool success = false;
            {
                string vertexShader = VertexShaderGen.GenerateVertexShader(fromMat, data);
                success = shader.CompileSource(vertexShader, ShaderType.VertexShader);
            }
            if (success)
            {
                string fragmentShader = FragmentShaderGen.GenerateFragmentShader(fromMat, data);
                success = shader.CompileSource(fragmentShader, ShaderType.FragmentShader);
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

        
    }
}
