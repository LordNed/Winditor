using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.IO;
using JStudio.OpenGL;

namespace WindEditor
{
    public class SimpleObjRenderer : IDisposable
    {
        struct UniqueVertex
        {
            public Vector3 Position;
            public Vector2 TexCoord;
            public Vector3 Normal;
        }

        public bool Highlighted;
        public bool BlendingEnabled;
        public bool FaceCullingEnabled;
        public Vector4 TintColor;
        public TextureWrapMode TexWrapMode;

        private int m_vertexVBO;
        private int m_indexVBO;
        private int m_texcoordVBO;
        private int m_normalVBO;
        private int m_textureVBO;

        private Shader m_unhighlightedShader;
        private Shader m_highlightedShader;
        private int m_triangleCount;

        private FAABox m_boundingBox;

        // To detect redundant calls
        private bool m_hasBeenDisposed = false;


        public SimpleObjRenderer(Obj file, Vector4 tint_color, bool enable_blending = false, bool enable_face_culling = true, TextureWrapMode wrap_mode = TextureWrapMode.Repeat)
        {
            m_vertexVBO = GL.GenBuffer();
            m_indexVBO = GL.GenBuffer();
            m_texcoordVBO = file.TexCoords.Count > 0 ? GL.GenBuffer() : -1;
            m_normalVBO = file.Normals.Count > 0 ? GL.GenBuffer() : -1;
            m_textureVBO = file.Material.DiffuseTexture != null ? GL.GenTexture() : -1;

            m_unhighlightedShader = new Shader("UnlitTexture");
            m_unhighlightedShader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.vert"), ShaderType.VertexShader);
            m_unhighlightedShader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.frag"), ShaderType.FragmentShader);
            m_unhighlightedShader.LinkShader();

            m_highlightedShader = new Shader("TransformGizmoHighlight");
            m_highlightedShader.CompileSource(File.ReadAllText("resources/shaders/UnlitTexture.vert"), ShaderType.VertexShader);
            m_highlightedShader.CompileSource(File.ReadAllText("resources/shaders/TransformGizmoHighlight.frag"), ShaderType.FragmentShader);
            m_highlightedShader.LinkShader();

            BlendingEnabled = enable_blending;
            FaceCullingEnabled = enable_face_culling;
            TintColor = tint_color;
            TexWrapMode = wrap_mode;

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

            m_boundingBox = new FAABox();
            for (int i = 0; i < uniqueVerts.Count; i++)
            {
                positions[i] = uniqueVerts[i].Position;
                m_boundingBox.Encapsulate(positions[i]);

                if (texcoords != null) texcoords[i] = new Vector2(uniqueVerts[i].TexCoord.X, 1 - uniqueVerts[i].TexCoord.Y);
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

                GL.BindTexture(TextureTarget.Texture2D, m_textureVBO);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TexWrapMode);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TexWrapMode);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

                // Black/white checkerboard
                float[] pixels = new[]
                {
                    0.0f, 0.0f, 0.0f,   255.0f, 255.0f, 255.0f,
                    255.0f, 255.0f, 255.0f,   0.0f, 0.0f, 0.0f
                };
                //GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 2, 2, 0, PixelFormat.Rgb, PixelType.Float, pixels);

                System.Drawing.Imaging.BitmapData bmpData = mat.DiffuseTexture.LockBits(new System.Drawing.Rectangle(0, 0, mat.DiffuseTexture.Width, mat.DiffuseTexture.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, mat.DiffuseTexture.Width, mat.DiffuseTexture.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);
                mat.DiffuseTexture.UnlockBits(bmpData);
            }
        }

        public FAABox GetAABB()
        {
            return m_boundingBox;
        }

        public void Render(Matrix4 viewMatrix, Matrix4 projMatrix, Matrix4 modelMatrix)
        {
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.CullFace(CullFaceMode.Front);
            if (FaceCullingEnabled)
            {
                GL.Enable(EnableCap.CullFace);
            } else
            {
                GL.Disable(EnableCap.CullFace);
            }
            GL.Enable(EnableCap.DepthTest);

            if (BlendingEnabled)
            {
                // Enable transparency
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.Enable(EnableCap.AlphaTest);
                GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);
            }
            else
            {
                GL.Disable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.Disable(EnableCap.AlphaTest);
                GL.AlphaFunc(AlphaFunction.Always, 0f);
            }

            GL.DepthMask(true);

            Shader curShader = Highlighted ? m_highlightedShader : m_unhighlightedShader;
            curShader.Bind();

            GL.UniformMatrix4(curShader.UniformModelMtx, false, ref modelMatrix);
            GL.UniformMatrix4(curShader.UniformViewMtx, false, ref viewMatrix);
            GL.UniformMatrix4(curShader.UniformProjMtx, false, ref projMatrix);

            if (curShader.UniformColor0Mat != -1)
                GL.Uniform4(curShader.UniformColor0Mat, ref TintColor);

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
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);

            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Tex0);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Normal);
            GL.BindTexture(TextureTarget.Texture2D, -1);
        }

        #region IDisposable Support
        ~SimpleObjRenderer()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        protected virtual void Dispose(bool manualDispose)
        {
            if (!m_hasBeenDisposed)
            {
                if (manualDispose)
                {
                    // Dispose managed state (managed objects).
                    m_unhighlightedShader.Dispose();
                    m_highlightedShader.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                GL.DeleteBuffer(m_vertexVBO);
                GL.DeleteBuffer(m_indexVBO);
                GL.DeleteBuffer(m_normalVBO);
                GL.DeleteBuffer(m_texcoordVBO);
                GL.DeleteTexture(m_textureVBO);

                // Set large fields to null.

                m_hasBeenDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
