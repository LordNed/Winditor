using OpenTK;
using System.Collections.Generic;
using System;
using System.IO;
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

        private Shader m_primitiveShader;
        private int m_vbo;
        private int m_vertColors;
        private bool m_renderStateDirty;

        // To detect redundant calls
        private bool m_hasBeenDisposed = false;

        public WQuadBatcher()
        {
            m_batchedQuads = new Dictionary<string, List<WBatchedQuad>>();

            m_primitiveShader = new Shader("UnlitColor");
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnlitColor.vert"), ShaderType.VertexShader);
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnlitColor.frag"), ShaderType.FragmentShader);
            m_primitiveShader.LinkShader();

            // Allocate our buffers now as they get reused a lot.
            m_vbo = GL.GenBuffer();
            m_vertColors = GL.GenBuffer();
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
                List<WBatchedQuad> quads = m_batchedQuads[s];

                if (m_renderStateDirty)
                {
                    // We've changed what we want to draw since we last rendered, so we'll re-calculate the mesh and upload.
                    Vector3[] quadVerts = new Vector3[quads.Count * 4];
                    WLinearColor[] quadColors = new WLinearColor[quads.Count * 4];

                    for (int i = 0; i < quads.Count; i++)
                    {
                        WBatchedQuad batchedQuad = quads[i];

                        // Top left
                        quadVerts[(i * 4) + 0] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(-0.5f, 0.5f, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(-0.5f, 0.5f, 0.0f));
                        // Top right
                        quadVerts[(i * 4) + 1] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(0.5f, 0.5f, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(0.5f, 0.5f, 0.0f));
                        // Bottom left
                        quadVerts[(i * 4) + 3] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(-0.5f, -0.5f, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(-0.5f, -0.5f, 0.0f));
                        // Bottom right
                        quadVerts[(i * 4) + 2] = batchedQuad.IsBillboard ? CalculateBillboardVertex(view, batchedQuad.Position, new Vector3(0.5f, -0.5f, 0.0f))
                            : CalculateQuadVertex(batchedQuad.Position, new Vector3(0.5f, -0.5f, 0.0f));

                        quadVerts[(i * 4) + 0].Scale(batchedQuad.Scale);
                        quadVerts[(i * 4) + 1].Scale(batchedQuad.Scale);
                        quadVerts[(i * 4) + 2].Scale(batchedQuad.Scale);
                        quadVerts[(i * 4) + 3].Scale(batchedQuad.Scale);

                        quadColors[(i * 4) + 0] = batchedQuad.Color;
                        quadColors[(i * 4) + 1] = batchedQuad.Color;
                        quadColors[(i * 4) + 2] = batchedQuad.Color;
                        quadColors[(i * 4) + 3] = batchedQuad.Color;
                    }


                    // Upload Verts
                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * quadVerts.Length), quadVerts, BufferUsageHint.DynamicDraw);

                    GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertColors);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * quadColors.Length), quadColors, BufferUsageHint.DynamicDraw);

                    //m_renderStateDirty = false;
                }

                // Draw the mesh.
                GL.FrontFace(FrontFaceDirection.Ccw);
                GL.Enable(EnableCap.CullFace);
                GL.Enable(EnableCap.DepthTest);
                GL.Disable(EnableCap.Blend);
                GL.DepthMask(true);
                //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

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

                // Color
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertColors);
                GL.EnableVertexAttribArray((int)ShaderAttributeIds.Color0);
                GL.VertexAttribPointer((int)ShaderAttributeIds.Color0, 4, VertexAttribPointerType.Float, true, 16, 0);

                // Draw!
                GL.DrawArrays(PrimitiveType.Quads, 0, quads.Count * 4);

                GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
                GL.DisableVertexAttribArray((int)ShaderAttributeIds.Color0);
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
