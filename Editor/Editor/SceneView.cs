using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System;

namespace WindEditor
{
    /// <summary>
    /// A scene view represents a particular viewport into a given <see cref="WWorld"/>. This allows us to have multiple views into a single world.
    /// </summary>
    public class WSceneView
    {
        public bool IsFocused { get; set; }

        private WWorld m_world;
        private IList<IRenderable> m_renderList;

        private int m_viewWidth;
        private int m_viewHeight;
        private WCamera m_viewCamera;
        private WRect m_viewportRect;
        private WViewportOrientationWidget m_orientationWidget;

        public WSceneView(WWorld world, IList<IRenderable> renderList)
        {
            m_world = world;
            m_renderList = renderList;
            m_viewportRect = new WRect(0, 0, 1f, 1f);

            m_viewCamera = new WCamera();
            m_orientationWidget = new WViewportOrientationWidget();
            world.RegisterObject(m_viewCamera);
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

            DrawOrientationWidget(x, y, viewMatrix, projMatrix);

            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.ScissorTest);
            GL.DepthMask(false);
        }

        private void DrawOrientationWidget(int viewportXOffset, int viewportYOffset, Matrix4 viewMatrix, Matrix4 projMatrix)
        {
            GL.Viewport(viewportXOffset, viewportYOffset, 64, 64);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // Rotation only
            viewMatrix = viewMatrix.ClearTranslation();
            projMatrix = Matrix4.CreateOrthographic(m_viewportRect.Width * m_viewWidth, m_viewportRect.Height * m_viewHeight, 0.01f, 250f);
            Matrix4 modelMatrix = Matrix4.CreateScale(0.9f) * Matrix4.CreateTranslation(m_viewCamera.Transform.Forward * -100f);

            m_orientationWidget.Render(m_viewCamera.Transform.Rotation, viewMatrix, projMatrix, modelMatrix);
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
            m_viewCamera.AspectRatio = (m_viewportRect.Width * width) / (m_viewportRect.Height * height);
        }

        public WRay ProjectScreenToWorld(Vector2 mousePosition)
        {
            WRect viewportDimensions = GetViewportDimensions();
            mousePosition.X -= viewportDimensions.X;
            mousePosition.Y -= viewportDimensions.Y;


            Vector3 mousePosA = new Vector3(mousePosition.X, mousePosition.Y, 0f);
            Vector3 mousePosB = new Vector3(mousePosition.X, mousePosition.Y, 1f);

            Vector2 screenSize = new Vector2(viewportDimensions.Width, viewportDimensions.Height);

            Vector4 nearUnproj = UnProject(m_viewCamera.ProjectionMatrix, m_viewCamera.ViewMatrix, mousePosA, screenSize);
            Vector4 farUnproj = UnProject(m_viewCamera.ProjectionMatrix, m_viewCamera.ViewMatrix, mousePosB, screenSize);

            Vector3 dir = farUnproj.Xyz - nearUnproj.Xyz;
            dir.Normalize();

            return new WRay(nearUnproj.Xyz, dir);
        }

        public Vector2 UnprojectWorldToViewport(Vector3 worldLocation)
        {
            Matrix4 viewProjMatrix = m_viewCamera.ViewMatrix * m_viewCamera.ProjectionMatrix;

            // Transform World to Clip Space
            Vector3 clipSpacePoint = Vector3.TransformPerspective(worldLocation, viewProjMatrix);
            Vector2 viewportSpace = new Vector2((clipSpacePoint.X + 1) / 2f, (-clipSpacePoint.Y + 1) / 2f);
            return viewportSpace;
        }

        internal Vector3 GetCameraPos()
        {
            return m_viewCamera.Transform.Position;
        }

        /// <summary>
        /// Returns the position of the viewport in screenspace pixel coordinates.
        /// </summary>
        public WRect GetViewportDimensions()
        {
            WRect newRect = new WRect();
            newRect.X = m_viewportRect.X * m_viewWidth;
            newRect.Y = m_viewportRect.Y * m_viewHeight;
            newRect.Width = m_viewportRect.Width * m_viewWidth;
            newRect.Height = m_viewportRect.Height * m_viewHeight;

            return newRect;
        }

        private Vector4 UnProject(Matrix4 projection, Matrix4 view, Vector3 mousePos, Vector2 screenSize)
        {
            Vector4 vec = new Vector4();

            vec.X = 2.0f * mousePos.X / screenSize.X - 1;
            vec.Y = -(2.0f * mousePos.Y / screenSize.Y - 1);
            vec.Z = mousePos.Z;
            vec.W = 1.0f;

            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref vec, ref projInv, out vec);
            Vector4.Transform(ref vec, ref viewInv, out vec);

            if (vec.W > float.Epsilon || vec.W < float.Epsilon)
            {
                vec.X /= vec.W;
                vec.Y /= vec.W;
                vec.Z /= vec.W;
            }

            return vec;
        }
    }
}
