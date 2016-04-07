using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace Editor
{
    class WindEditor
    {
        private Shader m_primitiveShader;

        private int m_vbo, m_ebo;

        public WindEditor()
        {
            m_primitiveShader = new Shader("UnlitColor");
            m_primitiveShader.CompileSource(File.ReadAllText("Editor/Shaders/UnlitColor.vert"), ShaderType.VertexShader);
            m_primitiveShader.CompileSource(File.ReadAllText("Editor/Shaders/UnlitColor.frag"), ShaderType.FragmentShader);
            m_primitiveShader.LinkShader();


            // More shitty hacks
            Vector3[] verts = new Vector3[]
            {
                new Vector3(-1f, 0f, 0f),
                new Vector3(1f, 0f, 0f),
                new Vector3(0f, 1f, 0f)
            };


            int[] indexes = new int[]
            {
                0, 1, 2
            };

            GL.GenBuffers(1, out m_vbo);
            GL.GenBuffers(1, out m_ebo);

            // Upload Verts
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * verts.Length), verts, BufferUsageHint.StaticDraw);

            // Upload eBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * indexes.Length), indexes, BufferUsageHint.StaticDraw);
        }

        public void ProcessTick()
        {
            RenderObjects();
        }

        private void RenderObjects()
        {
            GL.ClearColor(0.6f, 0.25f, 0.35f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);


            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw); // Windwaker is backwards!
            GL.Enable(EnableCap.CullFace);
            GL.DepthMask(true);

            Matrix4 modelMatrix = Matrix4.CreateTranslation(Vector3.Zero);
            Matrix4 viewMatrix = Matrix4.LookAt(new Vector3(0, 5, -1), Vector3.Zero, Vector3.UnitY);
            Matrix4 projMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), 1.5f /* hack */, 1, 100);

            m_primitiveShader.Bind();
            GL.UniformMatrix4(m_primitiveShader.UniformModelMtx, false, ref modelMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformViewMtx, false, ref viewMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformProjMtx, false, ref projMatrix);

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);

            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);

            // Draw!
            GL.DrawElements(BeginMode.Triangles, 3, DrawElementsType.UnsignedInt, 0);

            GL.Flush();
        }
    }
}
