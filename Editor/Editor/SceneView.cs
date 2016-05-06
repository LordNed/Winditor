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
        private WRect m_viewportRect;

        private static WSceneView m_dontlookatme;

        public WSceneView(WWorld world, IList<IRenderable> renderList)
        {
            m_world = world;
            m_renderList = renderList;
            m_viewportRect = new WRect(0, 0, 1f, 1f);

            m_viewCamera = new WCamera();
            world.RegisterObject(m_viewCamera);


            // ... :(
            m_dontlookatme = this;
        }

        public void Render()
        {
            int x = (int)(m_viewportRect.X * m_viewWidth);
            int y = (int)(m_viewportRect.Y * m_viewHeight);
            int width = (int)(m_viewportRect.Width * m_viewWidth);
            int height = (int)(m_viewportRect.Height * m_viewHeight);
            GL.Viewport(x, y, width, height);
            GL.Scissor(x, y, width, height);

            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw); // Windwaker is backwards!
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.ScissorTest);
            GL.DepthMask(true);

            GL.ClearColor(m_viewportRect.X, m_viewportRect.Y, m_viewportRect.Width, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 viewMatrix, projMatrix;
            GetViewAndProjMatrixForView(out viewMatrix, out projMatrix);

            foreach (var item in m_renderList)
            {   
                item.Render(viewMatrix, projMatrix);
            }

            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.ScissorTest);
            GL.DepthMask(false);
        }

        private void GetViewAndProjMatrixForView(out Matrix4 viewMatrix, out Matrix4 projMatrix)
        {
            viewMatrix = Matrix4.LookAt(m_viewCamera.Transform.Position, m_viewCamera.Transform.Position - m_viewCamera.Transform.Forward,  Vector3.UnitY);
            projMatrix = m_viewCamera.ProjectionMatrix;
        }

        internal void SetViewRect(WRect rect)
        {
            m_viewportRect = rect;
        }

        public void SetViewportSize(int width, int height)
        {
            m_viewWidth = width;
            m_viewHeight = height;

            // Recalculate the aspect ratio of our view camera.
            //m_viewCamera.AspectRatio = width / (float)height;
            m_viewCamera.AspectRatio = (m_viewportRect.Width * width) / (m_viewportRect.Height * height);
        }

        public static WRay ProjectScreenToWorld(Vector2 mousePosition)
        {
            var viewCam = m_dontlookatme.m_viewCamera;
            return viewCam.ViewportPointToRay(mousePosition, new Vector2(m_dontlookatme.m_viewWidth, m_dontlookatme.m_viewHeight));
        }

        public static Vector2 UnprojectWorldToViewport(Vector3 worldLocation)
        {
            var viewCam = m_dontlookatme.m_viewCamera;
            return viewCam.WorldPointToViewportPoint(worldLocation);
        }            

        internal static Vector3 GetCameraPos()
        {
            return m_dontlookatme.m_viewCamera.Transform.Position;
        }
    }
}
