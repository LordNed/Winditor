using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System;

namespace Editor
{
    /// <summary>
    /// A scene view represents a particular viewport into a given <see cref="WWorld"/>. This allows us to have multiple views into a single world.
    /// </summary>
    class WSceneView
    {
        private WWorld m_world;
        private IList<IRenderable> m_renderList;

        private int m_viewWidth;
        private int m_viewHeight;
        private WCamera m_viewCamera;

        public WSceneView(WWorld world, IList<IRenderable> renderList)
        {
            m_world = world;
            m_renderList = renderList;

            m_viewCamera = new WCamera();
            world.RegisterObject(m_viewCamera);
        }

        public void Render()
        {
            GL.Viewport(0, 0, m_viewWidth, m_viewHeight);

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
            viewMatrix = Matrix4.LookAt(m_viewCamera.Transform.Position, m_viewCamera.Transform.Position - m_viewCamera.Transform.Forward,  Vector3.UnitY);
            projMatrix = m_viewCamera.ProjectionMatrix;
        }

        public void SetViewportSize(int width, int height)
        {
            m_viewWidth = width;
            m_viewHeight = height;

            // Recalculate the aspect ratio of our view camera.
            m_viewCamera.AspectRatio = width / (float)height;
        }
    }
}
