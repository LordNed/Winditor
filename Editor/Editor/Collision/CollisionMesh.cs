using GameFormatReader.Common;
using JStudio.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace WindEditor.Collision
{
    class WCollisionMesh : WDOMNode, IRenderable
    {
        private int m_vbo, m_ebo;
        private Shader m_primitiveShader;
        private int m_triangleCount;

        private FAABox m_aaBox;

        public WCollisionMesh()
        {
            m_primitiveShader = new Shader("UnselectedCollision");
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnselectedCollision.vert"), ShaderType.VertexShader);
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnselectedCollision.frag"), ShaderType.FragmentShader);
            m_primitiveShader.LinkShader();
        }

        public void Load(EndianBinaryReader stream)
        {
            int vertexCount = stream.ReadInt32();
            int vertexOffset = stream.ReadInt32();
            int triangleCount = stream.ReadInt32();
            int triangleOffset = stream.ReadInt32();

            Vector3[] meshVerts = new Vector3[vertexCount];
            stream.BaseStream.Position = vertexOffset;

            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            for(int i = 0; i < vertexCount; i++)
            {
                meshVerts[i] = new Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());

                if (meshVerts[i].X < min.X) min.X = meshVerts[i].X;
                if (meshVerts[i].Y < min.Y) min.Y = meshVerts[i].Y;
                if (meshVerts[i].Z < min.Z) min.Z = meshVerts[i].Z;

                if (meshVerts[i].X > max.X) max.X = meshVerts[i].X;
                if (meshVerts[i].Y > max.Y) max.Y = meshVerts[i].Y;
                if (meshVerts[i].Z > max.Z) max.Z = meshVerts[i].Z;
            }

            m_aaBox = new FAABox(min, max);

            int[] triangleIndexes = new int[triangleCount * 3];
            stream.BaseStream.Position = triangleOffset;

            for (int i = 0; i < triangleCount; i++)
            {
                triangleIndexes[(i * 3) + 0] = stream.ReadUInt16();
                triangleIndexes[(i * 3) + 1] = stream.ReadUInt16();
                triangleIndexes[(i * 3) + 2] = stream.ReadUInt16();
                stream.Skip(4); 
            }

            GL.GenBuffers(1, out m_vbo);
            GL.GenBuffers(1, out m_ebo);

            // Upload Verts
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * meshVerts.Length), meshVerts, BufferUsageHint.StaticDraw);

            // Upload eBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * triangleIndexes.Length), triangleIndexes, BufferUsageHint.StaticDraw);

            m_triangleCount = triangleCount;
        }

        public void ReleaseResources()
        {
            m_primitiveShader.Dispose();
            GL.DeleteBuffer(m_ebo);
            GL.DeleteBuffer(m_vbo);
        }

        #region IRenderable
        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Blend);
            GL.DepthMask(true);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(-5f, 1f);

            Matrix4 modelMatrix = Matrix4.Identity;
            Matrix4 viewMatrix = view.ViewMatrix;
            Matrix4 projMatrix = view.ProjMatrix;

            m_primitiveShader.Bind();
            GL.UniformMatrix4(m_primitiveShader.UniformModelMtx, false, ref modelMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformViewMtx, false, ref viewMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformProjMtx, false, ref projMatrix);

            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);

            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);

            // Draw!
            GL.DrawElements(BeginMode.Triangles, m_triangleCount * 3, DrawElementsType.UnsignedInt, 0);
            GL.DrawElements(BeginMode.Lines, m_triangleCount * 3, DrawElementsType.UnsignedInt, 0);

            // Disable all of our shit.
            GL.PolygonOffset(0, 0);
            GL.Disable(EnableCap.PolygonOffsetFill);


            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.Blend);
            GL.DepthMask(false);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
        }

        Vector3 IRenderable.GetPosition()
        {
            return m_aaBox.Center;
        }

        float IRenderable.GetBoundingRadius()
        {
            return m_aaBox.Max.Length; // ToDo: This isn't correct.
        }
        #endregion
    }
}
