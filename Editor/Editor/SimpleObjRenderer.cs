using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.IO;

namespace Editor
{
    class SimpleObjRenderer : PrimitiveComponent
    {
        struct UniqueVertex
        {
            public Vector3 Position;
            public Vector2 TexCoord;
            public Vector3 Normal;
        }

        private int m_vertexVBO;
        private int m_indexVBO;
        private int m_texcoordVBO;
        private int m_normalVBO;
        private int m_textureVBO;

        private Shader m_shader;
        private int m_triangleCount;

        public SimpleObjRenderer(Obj file)
        {
            m_vertexVBO = GL.GenBuffer();
            m_indexVBO = GL.GenBuffer();
            m_texcoordVBO = file.TexCoords.Count > 0 ? GL.GenBuffer() : -1;
            m_normalVBO = file.Normals.Count > 0 ? GL.GenBuffer() : -1;
            m_textureVBO = file.Material.Diffuse != null ? GL.GenTexture() : -1;

            m_shader = new Shader("UnlitTexture");
            m_shader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.vert"), ShaderType.VertexShader);
            m_shader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.frag"), ShaderType.FragmentShader);
            m_shader.LinkShader();

            // Generate an array of all vertices instead of the compact form OBJ comes as.
            Vector3[] positions = null;
            Vector2[] texcoords = null;
            Vector3[] normals = null;
            int[] triangles = new int[file.Faces.Count * 3];
            m_triangleCount = file.Faces.Count;

            List<UniqueVertex> uniqueVerts = new List<UniqueVertex>();

            for (int i = 0; i < file.Faces.Count; i++)
            {
                Obj.ObjFace face = file.Faces[i];
                for (int k = 0; k < 3; k++)
                {
                    var vertex = new UniqueVertex();
                    vertex.Position = file.Vertices[face.Positions[k]];
                    if (face.TexCoords != null) vertex.TexCoord = file.TexCoords[face.TexCoords[k]];
                    if (face.Normals != null) vertex.Normal = file.Normals[face.Normals[k]];


                    int vertIndex = uniqueVerts.IndexOf(vertex);
                    if (vertIndex < 0)
                    {
                        uniqueVerts.Add(vertex);
                        vertIndex = uniqueVerts.Count - 1;
                    }

                    triangles[(i * 3) + k] = vertIndex;
                }
            }

            // Copy the data out of the interlaced buffers.
            positions = new Vector3[uniqueVerts.Count];
            texcoords = file.TexCoords.Count > 0 ? new Vector2[uniqueVerts.Count] : null;
            normals = file.Normals.Count > 0 ? new Vector3[uniqueVerts.Count] : null;

            for (int i = 0; i < uniqueVerts.Count; i++)
            {
                positions[i] = uniqueVerts[i].Position;
                if (texcoords != null) texcoords[i] = new Vector2(uniqueVerts[i].TexCoord.X, 1-uniqueVerts[i].TexCoord.Y);
                if (normals != null) normals[i] = uniqueVerts[i].Normal;
            }

            // Positions
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertexVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * positions.Length), positions, BufferUsageHint.StaticDraw);

            // Upload Indexes
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_indexVBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * triangles.Length), triangles, BufferUsageHint.StaticDraw);

            // Texcoords
            if (m_texcoordVBO >= 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_texcoordVBO);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(8 * texcoords.Length), texcoords, BufferUsageHint.StaticDraw);
            }

            // Normals
            if (m_normalVBO >= 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_normalVBO);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * normals.Length), normals, BufferUsageHint.StaticDraw);
            }

            // Texture
            if (m_textureVBO >= 0)
            {
                Obj.ObjMaterial mat = file.Material;
                //using (var imageData = new MemoryStream())
                //{
                  //  mat.Diffuse.Save(imageData, System.Drawing.Imaging.ImageFormat.Bmp);
                    //byte[] data = imageData.ToArray();

                    GL.BindTexture(TextureTarget.Texture2D, m_textureVBO);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

                    // Black/white checkerboard
                    float[] pixels = new[]
                    {
                        0.0f, 0.0f, 0.0f,   255.0f, 255.0f, 255.0f,
                        255.0f, 255.0f, 255.0f,   0.0f, 0.0f, 0.0f
                    };
                    //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 2, 2, 0, PixelFormat.Rgb, PixelType.Float, pixels);


                    System.Drawing.Imaging.BitmapData bmpData = mat.Diffuse.LockBits(new System.Drawing.Rectangle(0, 0, mat.Diffuse.Width, mat.Diffuse.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, mat.Diffuse.Width, mat.Diffuse.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);
                    mat.Diffuse.UnlockBits(bmpData);

                    //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, mat.Diffuse.Width, mat.Diffuse.Height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, data);
                    //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                //}
            }
        }

        public override void ReleaseResources()
        {
            GL.DeleteBuffer(m_vertexVBO);
            GL.DeleteBuffer(m_indexVBO);
            GL.DeleteTexture(m_textureVBO);
            if (m_texcoordVBO >= 0) GL.DeleteBuffer(m_texcoordVBO);
            if (m_normalVBO >= 0) GL.DeleteBuffer(m_normalVBO);
            m_shader.ReleaseResources();
        }

        public override void Render(Matrix4 viewMatrix, Matrix4 projMatrix)
        {
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);


            Matrix4 modelMatrix = Matrix4.Identity;

            m_shader.Bind();
            GL.UniformMatrix4(m_shader.UniformModelMtx, false, ref modelMatrix);
            GL.UniformMatrix4(m_shader.UniformViewMtx, false, ref viewMatrix);
            GL.UniformMatrix4(m_shader.UniformProjMtx, false, ref projMatrix);

            // Position
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertexVBO);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);

            // Texcoord
            if (m_texcoordVBO >= 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_texcoordVBO);
                GL.EnableVertexAttribArray((int)ShaderAttributeIds.Tex0);
                GL.VertexAttribPointer((int)ShaderAttributeIds.Tex0, 2, VertexAttribPointerType.Float, false, 8, 0);
            }

            // Normals
            if (m_normalVBO >= 0)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_normalVBO);
                GL.EnableVertexAttribArray((int)ShaderAttributeIds.Normal);
                GL.VertexAttribPointer((int)ShaderAttributeIds.Normal, 3, VertexAttribPointerType.Float, true, 12, 0);
            }

            // Texture
            if (m_textureVBO >= 0)
            {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, m_textureVBO);
            }

            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_indexVBO);

            // Draw!
            GL.DrawElements(BeginMode.Triangles, m_triangleCount * 3, DrawElementsType.UnsignedInt, 0);

            // Disable all of our shit.
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);

            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Tex0);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Normal);
            GL.BindTexture(TextureTarget.Texture2D, -1);
        }

        public override void Tick(float deltaTime)
        {

        }
    }
}
