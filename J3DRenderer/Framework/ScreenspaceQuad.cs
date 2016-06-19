using JStudio.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace J3DRenderer.Framework
{
    public class ScreenspaceQuad
    {
        private int m_vboId;

        public ScreenspaceQuad()
        {
            m_vboId = GL.GenBuffer();
            Vector3[] verts = new Vector3[6];
            verts[0] = new Vector3(-1, -1, 0); // Bottom Left
            verts[1] = new Vector3(1, 1, 0); // Top Right
            verts[2] = new Vector3(-1, 1, 0); // Top Left
            verts[3] = new Vector3(-1, -1, 0); // Bottom Left
            verts[4] = new Vector3(1, -1, 0); // Bottom Right
            verts[5] = new Vector3(1, 1, 0); // Top Right

            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vboId);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(verts.Length * 12), verts, BufferUsageHint.StaticDraw);
        }

        public void Draw()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vboId);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
