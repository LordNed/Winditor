using GameFormatReader.Common;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace WindEditor.Collision
{
    class WCollisionMesh : IRenderable
    {
        private int m_vbo, m_ebo;
        private Shader m_primitiveShader;
        private int m_triangleCount;

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

            for(int i = 0; i < vertexCount; i++)
            {
                meshVerts[i] = new Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
            }

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

        public void Render(Matrix4 viewMatrix, Matrix4 projMatrix)
        {
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Blend);
            GL.DepthMask(true);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


            Matrix4 modelMatrix = Matrix4.Identity;

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

            // Disable all of our shit.
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.Blend);
            GL.DepthMask(false);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
        }

        public void ReleaseResources()
        {
            m_primitiveShader.ReleaseResources();
            GL.DeleteBuffer(m_ebo);
            GL.DeleteBuffer(m_vbo);
        }
    }
}
