using OpenTK;
using System.Collections.Generic;
using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using JStudio.OpenGL;

namespace WindEditor
{
    struct WBatchedLine
    {
        public Vector3 Start;
        public Vector3 End;
        public WLinearColor Color;
        public float Thickness;
        public float RemainingLifetime;

        public WBatchedLine(Vector3 start, Vector3 end, WLinearColor color, float thickness, float lifetime)
        {
            Start = start;
            End = end;
            Color = color;
            Thickness = thickness;
            RemainingLifetime = lifetime;
        }
    }

    public class WLineBatcher : IRenderable, IDisposable
    {
        private List<WBatchedLine> m_batchedLines;

        private Shader m_primitiveShader;
        private int m_vbo;
        private int m_vertColors;
        private bool m_renderStateDirty;

        // To detect redundant calls
        private bool m_hasBeenDisposed = false; 


        public WLineBatcher()
        {
            m_batchedLines = new List<WBatchedLine>();

            m_primitiveShader = new Shader("UnlitColor");
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnlitColor.vert"), ShaderType.VertexShader);
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnlitColor.frag"), ShaderType.FragmentShader);
            m_primitiveShader.LinkShader();

            // Allocate our buffers now as they get reused a lot.
            m_vbo = GL.GenBuffer();
            m_vertColors = GL.GenBuffer();
        }

        public void DrawLine(Vector3 start, Vector3 end, WLinearColor color, float thickness, float lifetime)
        {
            m_batchedLines.Add(new WBatchedLine(start, end, color, thickness, lifetime));
            m_renderStateDirty = true;
        }

        public void DrawBox(Vector3 min, Vector3 max, WLinearColor color, float thickness, float lifetime)
        {
            Vector3 center = (max + min) / 2;
            Vector3 extents = (max - min) / 2;

            DrawBox(center, extents, Quaternion.Identity, color, lifetime, thickness);

            m_renderStateDirty = true;
        }

        public void DrawBox(Vector3 center, Vector3 box, Quaternion rotation, WLinearColor color, float lifetime, float thickness)
        {
            List<WBatchedLine> lines = new List<WBatchedLine>();

            Vector3 start = Vector3.Transform(new Vector3(box.X, box.Y, box.Z), rotation);
            Vector3 end = Vector3.Transform(new Vector3(box.X, -box.Y, box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(box.X, -box.Y, box.Z), rotation);
            end = Vector3.Transform(new Vector3(-box.X, -box.Y, box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(-box.X, -box.Y, box.Z), rotation);
            end = Vector3.Transform(new Vector3(-box.X, box.Y, box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(-box.X, box.Y, box.Z), rotation);
            end = Vector3.Transform(new Vector3(box.X, box.Y, box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(box.X, box.Y, -box.Z), rotation);
            end = Vector3.Transform(new Vector3(box.X, -box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(box.X, -box.Y, -box.Z), rotation);
            end = Vector3.Transform(new Vector3(-box.X, -box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(-box.X, -box.Y, -box.Z), rotation);
            end = Vector3.Transform(new Vector3(-box.X, box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(-box.X, box.Y, -box.Z), rotation);
            end = Vector3.Transform(new Vector3(box.X, box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(box.X, box.Y, box.Z), rotation);
            end = Vector3.Transform(new Vector3(box.X, box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(box.X, -box.Y, box.Z), rotation);
            end = Vector3.Transform(new Vector3(box.X, -box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(-box.X, -box.Y, box.Z), rotation);
            end = Vector3.Transform(new Vector3(-box.X, -box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            start = Vector3.Transform(new Vector3(-box.X, box.Y, box.Z), rotation);
            end = Vector3.Transform(new Vector3(-box.X, box.Y, -box.Z), rotation);
            lines.Add(new WBatchedLine(center + start, center + end, color, thickness, lifetime));

            m_batchedLines.AddRange(lines);
            m_renderStateDirty = true;
        }

        public void Clear()
        {
            m_batchedLines.Clear();
            m_renderStateDirty = true;
        }

        public void DrawSphere(Vector3 center, float radius, int segments, WLinearColor color, float lifetime, float thickness)
        {
            segments = Math.Max(segments, 4);

            Vector3 v1, v2, v3, v4;
            float angleSeg = (float)(2f * Math.PI / (float)segments);
            float sinY1 = 0f, cosY1 = 1f, sinY2 = 0f, cosY2 = 0f;
            int numSegmentsY = segments;
            float latitude = angleSeg;
            float longitude = 0f;

            List<WBatchedLine> lines = new List<WBatchedLine>(numSegmentsY * segments * 2);
            do
            {
                sinY2 = (float)Math.Sin(latitude);
                cosY2 = (float)Math.Cos(latitude);

                v1 = new Vector3(sinY1, 0f, cosY1) * radius + center;
                v3 = new Vector3(sinY2, 0f, cosY2) * radius + center;
                longitude = angleSeg;

                int numSegmentsX = segments;
                do
                {
                    float sinX = (float)Math.Sin(longitude);
                    float cosX = (float)Math.Cos(longitude);

                    v2 = new Vector3((cosX * sinY1), (sinX * sinY1), cosY1) * radius + center;
                    v4 = new Vector3((cosX * sinY2), (sinX * sinY2), cosY2) * radius + center;

                    lines.Add(new WBatchedLine(v1, v2, color, thickness, lifetime));
                    lines.Add(new WBatchedLine(v1, v3, color, thickness, lifetime));

                    v1 = v2;
                    v3 = v4;
                    longitude += angleSeg;
                    numSegmentsX--;
                } while (numSegmentsX > 0);

                sinY1 = sinY2;
                cosY1 = cosY2;
                latitude += angleSeg;

                numSegmentsY--;
            } while (numSegmentsY > 0);

            m_batchedLines.AddRange(lines);
            m_renderStateDirty = true;
        }

        public void Tick(float deltaTime)
        {
            bool dirty = false;
            for (int lineIndex = 0; lineIndex < m_batchedLines.Count; lineIndex++)
            {
                WBatchedLine line = m_batchedLines[lineIndex];
                if (line.RemainingLifetime > 0)
                {
                    line.RemainingLifetime -= deltaTime;
                }

                if (line.RemainingLifetime <= 0f)
                {
                    // Remove the line from the array and deincrement to avoid skipping a line.
                    m_batchedLines.RemoveSwap(lineIndex--);
                    dirty = true;
                    continue;
                }

                m_batchedLines[lineIndex] = line;
            }

            if (dirty)
                m_renderStateDirty = true;
        }

        public void Flush()
        {
            m_batchedLines.Clear();
            m_renderStateDirty = true;
        }

        public void ReleaseResources()
        {
            m_primitiveShader.Dispose();
            GL.DeleteBuffer(m_vbo);
            GL.DeleteBuffer(m_vertColors);
        }
        
        public void Draw(WSceneView view)
        {
            if (m_renderStateDirty)
            {
                // We've changed what we want to draw since we last rendered, so we'll re-calculate the mesh and upload.
                Vector3[] lineVerts = new Vector3[m_batchedLines.Count * 2];
                WLinearColor[] lineColors = new WLinearColor[m_batchedLines.Count * 2];

                for (int i = 0; i < m_batchedLines.Count; i++)
                {
                    WBatchedLine batchedLine = m_batchedLines[i];
                    lineVerts[(i * 2) + 0] = batchedLine.Start;
                    lineVerts[(i * 2) + 1] = batchedLine.End;

                    lineColors[(i * 2) + 0] = batchedLine.Color;
                    lineColors[(i * 2) + 1] = batchedLine.Color;
                }


                // Upload Verts
                GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * lineVerts.Length), lineVerts, BufferUsageHint.DynamicDraw);

                GL.BindBuffer(BufferTarget.ArrayBuffer, m_vertColors);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * lineColors.Length), lineColors, BufferUsageHint.DynamicDraw);

                m_renderStateDirty = false;
            }

            // Draw the mesh.
            GL.FrontFace(FrontFaceDirection.Cw);
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
            GL.DrawArrays(PrimitiveType.Lines, 0, m_batchedLines.Count * 2);

            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Color0);
        }

        public void AddToRenderer(WSceneView view)
        {
            view.AddOpaqueMesh(this);
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
        ~WLineBatcher()
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
                m_batchedLines.Clear();
                m_batchedLines = null;

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
