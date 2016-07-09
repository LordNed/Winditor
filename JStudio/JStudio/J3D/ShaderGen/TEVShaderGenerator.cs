using JStudio.OpenGL;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace JStudio.J3D.ShaderGen
{
    public static class TEVShaderGenerator
    {
        private static bool m_allowCachedOverride = false;

        public static Shader GenerateShader(Material fromMat, MAT3 data, bool dumpShaders)
        {
            Directory.CreateDirectory("ShaderDump");

            Shader shader = new Shader(fromMat.Name);
            bool success = false;
            {
                // Load it from the shader dump if it already exists, which allows us to hand-modify shaders.
                string filenameHash = string.Format("ShaderDump/{0}.vert", fromMat.Name);

                bool loadedFromDisk = File.Exists(filenameHash) && m_allowCachedOverride;
                string vertexShader = loadedFromDisk ? File.ReadAllText(filenameHash) : VertexShaderGen.GenerateVertexShader(fromMat, data);
                
                success = shader.CompileSource(vertexShader, ShaderType.VertexShader);

                if(!loadedFromDisk && dumpShaders)
                    File.WriteAllText(filenameHash, vertexShader);
            }
            if (success)
            {
                // Load it from the shader dump if it already exists, which allows us to hand-modify shaders.
                string filenameHash = string.Format("ShaderDump/{0}.frag", fromMat.Name);

                bool loadedFromDisk = File.Exists(filenameHash) && m_allowCachedOverride;
                string fragmentShader =  loadedFromDisk ? File.ReadAllText(filenameHash) : FragmentShaderGen.GenerateFragmentShader(fromMat, data);

                success = shader.CompileSource(fragmentShader, ShaderType.FragmentShader);

                if(!loadedFromDisk && dumpShaders)
                    File.WriteAllText(filenameHash, fragmentShader);
            }

            if (success)
                // Well, we compiled both the Vertex and the Fragment shader succesfully, let's try to link them together now.
                success = shader.LinkShader();

            //success = false;
            if (!success)
            {
                Console.WriteLine("Failed to generate shader for material: {0}", fromMat.Name);
                shader.Dispose();

                // Generate a Fallback Shader for rendering
                shader = new Shader("Debug_NormalColors");
                shader.CompileSource(File.ReadAllText("resources/shaders/Debug_NormalColors.vert"), ShaderType.VertexShader);
                shader.CompileSource(File.ReadAllText("resources/shaders/Debug_NormalColors.frag"), ShaderType.FragmentShader);
                shader.LinkShader();

                return shader;
            }

            return shader;
        }

        
    }
}
