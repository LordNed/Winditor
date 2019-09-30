using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JStudio.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace WindEditor.Collision
{
    public partial class WCollisionMesh
    {
        private int m_vbo, m_cbo, m_ebo;
        private Shader m_primitiveShader;

        private Vector3[] m_Vertices;
        private Vector4[] m_Colors;
        private Vector4[] m_Colors_Black;
        private int[] m_Indices;

        private void SetupGL()
        {
            GL.GenBuffers(1, out m_vbo);
            GL.GenBuffers(1, out m_ebo);
            GL.GenBuffers(1, out m_cbo);

            // Upload Verts
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * m_Vertices.Length), m_Vertices, BufferUsageHint.StaticDraw);

            // Upload initial colors
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * m_Colors.Length), m_Colors, BufferUsageHint.DynamicDraw);

            // Upload eBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * m_Indices.Length), m_Indices, BufferUsageHint.StaticDraw);
        }

        private void CreateShader()
        {
            m_primitiveShader = new Shader("UnselectedCollision");
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnselectedCollision.vert"), ShaderType.VertexShader);
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnselectedCollision.frag"), ShaderType.FragmentShader);
            m_primitiveShader.LinkShader();
        }

        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            m_Colors = GetVertexColors();

            Matrix4 modelMatrix = Matrix4.Identity;
            Matrix4 viewMatrix = view.ViewMatrix;
            Matrix4 projMatrix = view.ProjMatrix;

            m_primitiveShader.Bind();
            GL.UniformMatrix4(m_primitiveShader.UniformModelMtx, false, ref modelMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformViewMtx, false, ref viewMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformProjMtx, false, ref projMatrix);

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);

            // CBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Color0);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Color0, 4, VertexAttribPointerType.Float, false, 16, 0);

            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);

            GL.DepthMask(false);

            DrawTris();
            DrawLines();

            GL.DepthMask(true);

            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Color0);
        }

        private void DrawTris()
        {
            // Enable stuff
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(-5f, 1f);

            // CBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * m_Colors.Length), m_Colors, BufferUsageHint.DynamicDraw);

            // Draw!
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.DrawElements(BeginMode.Triangles, m_triangleCount * 3, DrawElementsType.UnsignedInt, 0);

            // Disable stuff
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);

            GL.Disable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(0f, 0f);
        }

        private void DrawLines()
        {
            // Enable stuff
            GL.Disable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.Enable(EnableCap.PolygonOffsetLine);
            GL.PolygonOffset(-10f, 1f);

            // CBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * m_Colors_Black.Length), m_Colors_Black, BufferUsageHint.DynamicDraw);

            // Draw!
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.DrawElements(BeginMode.Triangles, m_triangleCount * 3, DrawElementsType.UnsignedInt, 0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            // Disable stuff
            GL.Disable(EnableCap.CullFace);

            GL.Disable(EnableCap.PolygonOffsetLine);
            GL.PolygonOffset(0f, 0f);
        }

        Vector3 IRenderable.GetPosition()
        {
            return m_aaBox.Center;
        }

        float IRenderable.GetBoundingRadius()
        {
            return m_aaBox.Max.Length; // ToDo: This isn't correct.
        }

        public void ReleaseResources()
        {
            m_primitiveShader.Dispose();
            GL.DeleteBuffer(m_ebo);
            GL.DeleteBuffer(m_vbo);
            GL.DeleteBuffer(m_cbo);
        }
    }
}
