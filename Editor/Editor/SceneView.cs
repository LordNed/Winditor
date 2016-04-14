using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections;
using System.Collections.Generic;

namespace Editor
{
    /// <summary>
    /// A scene view represents a particular viewport into a given <see cref="WWorld"/>. This allows us to have multiple
    /// </summary>
    class WSceneView
    {
        private WWorld m_world;
        private IList<IRenderable> m_renderList;

        public WSceneView(WWorld world, IList<IRenderable> renderList)
        {
            m_world = world;
            m_renderList = renderList;
        }

        public void Render()
        {
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw); // Windwaker is backwards!
            GL.Enable(EnableCap.CullFace);
            GL.DepthMask(true);

            Matrix4 viewMatrix, projMatrix;
            GetViewAndProjMatrixForView(out viewMatrix, out projMatrix);

            foreach (var item in m_renderList)
            {   
                item.Render(viewMatrix, projMatrix);
            }
        }

        private void GetViewAndProjMatrixForView(out Matrix4 viewMatrix, out Matrix4 projMatrix)
        {
            viewMatrix = Matrix4.LookAt(new Vector3(0, 0, -100), Vector3.Zero, Vector3.UnitY);
            projMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90f), 1.5f /* hack */, 1, 1000000);
        }
    }
}
