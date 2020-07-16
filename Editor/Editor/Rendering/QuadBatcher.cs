using OpenTK;
using System.Collections.Generic;
using System;
using System.IO;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using JStudio.OpenGL;

namespace WindEditor
{
    struct WBatchedQuad
    {
        public Vector3 Position;
        public Vector3 Scale;
        public WLinearColor Color;
        public bool IsBillboard;
        public float RemainingLifetime;

        public WBatchedQuad(Vector3 position, Vector3 scale, WLinearColor color, bool billboard, float lifetime)
        {
            Position = position;
            Scale = scale;
            Color = color;
            IsBillboard = billboard;
            RemainingLifetime = lifetime;
        }
    }

    public class WQuadBatcher : IRenderable, IDisposable
    {
        private Dictionary<string, List<WBatchedQuad>> m_batchedQuads;
        private Dictionary<string, Bitmap> m_Textures;

        private Shader m_primitiveShader;
        private int m_vbo;
        private int m_uvs;
        private int m_Tex;
        private int m_vertColors;
        private bool m_renderStateDirty;

        // To detect redundant calls
        private bool m_hasBeenDisposed = false;

        public WQuadBatcher()
        {
            m_batchedQuads = new Dictionary<string, List<WBatchedQuad>>();
            m_Textures = new Dictionary<string, Bitmap>();

            m_primitiveShader = new Shader("UnlitColor");
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnlitTextureQuad.vert"), ShaderType.VertexShader);
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnlitTextureQuad.frag"), ShaderType.FragmentShader);
            m_primitiveShader.LinkShader();

            m_Textures.Add("ZBtoonEX.png", new Bitmap("resources/textures/ZBtoonEX.png"));
            m_Textures.Add("eye.png", new Bitmap("resources/textures/eye.png"));
            m_Textures.Add("target.png", new Bitmap("resources/textures/target.png"));

            // Allocate our buffers now as they get reused a lot.
            m_vbo = GL.GenBuffer();
            m_uvs = GL.GenBuffer();
            m_vertColors = GL.GenBuffer();
            m_Tex = GL.GenTexture();
        }

        private List<WBatchedQuad> GetTextureList(string texture_name)
        {
            if (!m_batchedQuads.ContainsKey(texture_name))
                m_batchedQuads.Add(texture_name, new List<WBatchedQuad>());

            return m_batchedQuads[texture_name];
        }

        public void DrawQuad(string texture_name, Vector3 position, Vector3 scale, WLinearColor color, float lifetime)
        {
            List<WBatchedQuad> tex_list = GetTextureList(texture_name);
            tex_list.Add(new WBatchedQuad(position, scale, color, false, lifetime));

            m_renderStateDirty = true;
        }

        public void DrawBillboard(string texture_name, Vector3 position, Vector3 scale, WLinearColor color, float lifetime)
        {
            List<WBatchedQuad> tex_list = GetTextureList(texture_name);
            tex_list.Add(new WBatchedQuad(position, scale, color, true, lifetime));

            m_renderStateDirty = true;
        }

        public void Clear()
        {
            m_batchedQuads.Clear();
            m_renderStateDirty = true;
        }

        public void Tick(float deltaTime)
        {
            bool dirty = false;

            string[] keys = new string[m_batchedQuads.Count];
            m_batchedQuads.Keys.CopyTo(keys, 0);
            
            foreach (string s in keys)
            {
                List<WBatchedQuad> tex_list = m_batchedQuads[s];

                for (int quadIndex = 0; quadIndex < tex_list.Count; quadIndex++)
                {
                    WBatchedQuad quad = tex_list[quadIndex];
                    if (quad.RemainingLifetime > 0)
                    {
                        quad.RemainingLifetime -= deltaTime;
                    }

                    if (quad.RemainingLifetime <= 0f && quad.RemainingLifetime != -1.0f)
                    {
                        // Remove the line from the array and deincrement to avoid skipping a line.
                        tex_list.RemoveSwap(quadIndex--);
                        dirty = true;
                        continue;
                    }

                    tex_list[quadIndex] = quad;
                }

                if (tex_list.Count == 0)
                    m_batchedQuads.Remove(s);
            }

            if (dirty)
                m_renderStateDirty = true;
        }

        public void Flush()
        {
            m_batchedQuads.Clear();
            m_renderStateDirty = true;
        }

        public void ReleaseResources()
        {
            m_primitiveShader.Dispose();
            GL.DeleteBuffer(m_vbo);
            GL.DeleteBuffer(m_vertColors);
        }

        private Vector3 CalculateQuadVertex(Vector3 position, Vector3 offset)
        {
            return position + offset;
        }

        private Vector3 CalculateBillboardVertex(WSceneView view, Vector3 position, Vector3 offset)
        {
            return position + ((view.ViewMatrix.Column0 * offset.X) + (view.ViewCamera.ViewMatrix.Column1 * offset.Y)).Xyz;
        }

        public void Draw(WSceneView view)
        {
            foreach (string s in m_batchedQuads.Keys)
            {
                Bitmap texture = m_Textures[s];
                List<WBatchedQuad> quads = m_batchedQuads[s];

                if (m_renderStateDirty)
                {
                    // We've changed what we want to draw since we last rendered, so we'll re-calculate the mesh and upload.
                    Vector3[] quadVerts = new Vector3[quads.Count * 4];
                    Vector2[] quadUVs = new Vector2[quads.Count * 4];
                    WLinearColor[] quadColors = new WLinearColor[quads.Count * 4];

                    for (int i = 0; i < quads.Count; i++)
                    {
                        WBatchedQuad batchedQuad = quads[i];

                        // Top left
                        quadVerts[(i * 4) + 0] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(-0.5f * batchedQuad.Scale.X, 0.5f * batchedQuad.Scale.Y, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(-0.5f * batchedQuad.Scale.X, 0.5f * batchedQuad.Scale.Y, 0.0f));
                        // Top right
                        quadVerts[(i * 4) + 1] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(0.5f * batchedQuad.Scale.X, 0.5f * batchedQuad.Scale.Y, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(0.5f * batchedQuad.Scale.X, 0.5f * batchedQuad.Scale.Y, 0.0f));
                        // Bottom left
                        quadVerts[(i * 4) + 3] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(-0.5f * batchedQuad.Scale.X, -0.5f * batchedQuad.Scale.Y, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(-0.5f * batchedQuad.Scale.X, -0.5f * batchedQuad.Scale.Y, 0.0f));
                        // Bottom right
                        quadVerts[(i * 4) + 2] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(0.5f * batchedQuad.Scale.X, -0.5f * batchedQuad.Scale.Y, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(0.5f * batchedQuad.Scale.X, -0.5f * batchedQuad.Scale.Y, 0.0f));

                        quadColors[(i * 4) + 0] = batchedQuad.Color;
                        quadColors[(i * 4) + 1] = batchedQuad.Color;
                        quadColors[(i * 4) + 2] = batchedQuad.Color;
                        quadColors[(i * 4) + 3] = batchedQuad.Color;

                        quadUVs[(i * 4) + 0] = new Vector2(0, 0);
                        quadUVs[(i * 4) + 1] = new Vector2(1, 0);
                        quadUVs[(i * 4) + 2] = new Vector2(1, 1);
                        quadUVs[(i * 4) + 3] = new Vector2(0, 1);
                    }


                    // Upload Verts
                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * quadVerts.Length), quadVerts, BufferUsageHint.DynamicDraw);

                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_uvs);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(8 * quadUVs.Length), quadUVs, BufferUsageHint.DynamicDraw);

                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertColors);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * quadColors.Length), quadColors, BufferUsageHint.DynamicDraw);

                    GL.BindTexture(TextureTarget.Texture2D, m_Tex);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

                    System.Drawing.Imaging.BitmapData tex_data = texture.LockBits(new System.Drawing.Rectangle(0, 0, texture.Width, texture.Height),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, texture.Width, texture.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, tex_data.Scan0);

                    texture.UnlockBits(tex_data);

                    //m_renderStateDirty = false;
                }

                // Draw the mesh.
                GL.FrontFace(FrontFaceDirection.Ccw);
                GL.Disable(EnableCap.CullFace);
                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.Blend);
                GL.DepthMask(true);

                GL.Enable(EnableCap.AlphaTest);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                Matrix4 modelMatrix = Matrix4.Identity;
                Matrix4 viewMatrix = view.ViewMatrix;
                Matrix4 projMatrix = view.ProjMatrix;

                m_primitiveShader.Bind();
                GL.UniformMatrix4(m_primitiveShader.UniformModelMtx, false, ref modelMatrix);
                GL.UniformMatrix4(m_primitiveShader.UniformViewMtx, false, ref viewMatrix);
                GL.UniformMatrix4(m_primitiveShader.UniformProjMtx, false, ref projMatrix);

                // Position
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
                GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
                GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);

                // UVs
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_uvs);
                GL.EnableVertexAttribArray((int)ShaderAttributeIds.Tex0);
                GL.VertexAttribPointer((int)ShaderAttributeIds.Tex0, 2, VertexAttribPointerType.Float, false, 8, 0);

                // Color
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertColors);
                GL.EnableVertexAttribArray((int)ShaderAttributeIds.Color0);
                GL.VertexAttribPointer((int)ShaderAttributeIds.Color0, 4, VertexAttribPointerType.Float, true, 16, 0);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, m_Tex);

                // Draw!
                GL.DrawArrays(PrimitiveType.Quads, 0, quads.Count * 4);

                GL.FrontFace(FrontFaceDirection.Cw);
                GL.Enable(EnableCap.CullFace);
                GL.Disable (EnableCap.DepthTest);
                GL.Disable(EnableCap.Blend);
                GL.Disable(EnableCap.AlphaTest);
                GL.DepthMask(false);

                GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
                GL.DisableVertexAttribArray((int)ShaderAttributeIds.Tex0);
                GL.DisableVertexAttribArray((int)ShaderAttributeIds.Color0);
                GL.BindTexture(TextureTarget.Texture2D, -1);
            }
        }

        public void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        Vector3 IRenderable.GetPosition()
        {
            return Vector3.Zero;
        }

        float IRenderable.GetBoundingRadius()
        {
            return float.MaxValue;
        }

        #region IDisposable Support
        ~WQuadBatcher()
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
                    m_primitiveShader.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                GL.DeleteBuffer(m_vbo);
                GL.DeleteBuffer(m_vertColors);

                // Set large fields to null.
                m_batchedQuads.Clear();
                m_batchedQuads = null;

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
