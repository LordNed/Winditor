using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace WindEditor
{
    /// <summary>
    /// A scene view represents a particular viewport into a given <see cref="WWorld"/>. This allows us to have multiple views into a single world.
    /// </summary>
    public class WSceneView : IDisposable
    {
        public bool IsFocused { get; set; }

        public Matrix4 ViewMatrix { get { return m_viewCamera.ViewMatrix; } }
        public Matrix4 ProjMatrix { get { return m_viewCamera.ProjectionMatrix; } }

        private int m_viewWidth;
        private int m_viewHeight;
        private WCamera m_viewCamera;
        private FRect m_viewportRect;
        private WViewportOrientationWidget m_orientationWidget;

        private List<IRenderable> m_opaqueRenderList;
        private List<IRenderable> m_transparentRenderList;

        // To detect redundant calls
        private bool m_hasBeenDisposed = false;

        public WSceneView()
        {
            m_opaqueRenderList = new List<IRenderable>();
            m_transparentRenderList = new List<IRenderable>();

            m_viewportRect = new FRect(0, 0, 1f, 1f);
            m_viewCamera = new WCamera();
            m_orientationWidget = new WViewportOrientationWidget();
        }

        public void StartFrame()
        {
            m_opaqueRenderList.Clear();
            m_transparentRenderList.Clear();
        }

        public void AddOpaqueMesh(IRenderable mesh)
        {
            m_opaqueRenderList.Add(mesh);
        }

        public void AddTransparentMesh(IRenderable mesh)
        {
            m_transparentRenderList.Add(mesh);
        }

        public void DrawFrame()
        {
            ResetGraphicsState();

            int x = (int)(m_viewportRect.X * m_viewWidth);
            int y = (int)(m_viewportRect.Y * m_viewHeight);
            int width = (int)(m_viewportRect.Width * m_viewWidth);
            int height = (int)(m_viewportRect.Height * m_viewHeight);
            GL.Viewport(x, y, width, height);
            GL.Scissor(x, y, width, height);

            GL.ClearColor(m_viewportRect.X, m_viewportRect.Y, m_viewportRect.Width, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 viewMatrix, projMatrix;
            GetViewAndProjMatrixForView(out viewMatrix, out projMatrix);

            FFrustum frustum = new FFrustum(ref viewMatrix, ref projMatrix);

            List<IRenderable> renderablesInFrustum = new List<IRenderable>(m_opaqueRenderList.Count);
            FrustumCullList(frustum, m_opaqueRenderList, renderablesInFrustum);

            // Render all Opaque Geometry first.
            foreach (var mesh in renderablesInFrustum)
            {
                mesh.Draw(this);
            }

            // Render all Transparent Geometry afterwards. ToDo: depth-sort this first.
            foreach (var mesh in m_transparentRenderList)
                mesh.Draw(this);

            DrawOrientationWidget(x, y, viewMatrix, projMatrix);
            ResetGraphicsState();
        }

        private void FrustumCullList(FFrustum frustum, List<IRenderable> sourceList, List<IRenderable> outputList)
        {
            for (int i = 0; i < sourceList.Count; i++)
            {
                Halfspace contains = frustum.ContainsSphere(sourceList[i].GetPosition(), sourceList[i].GetBoundingRadius());

                if (contains != Halfspace.Negative)
                {
                    outputList.Add(sourceList[i]);
                }
            }
        }

        public void UpdateSceneCamera(float deltaTime)
        {
            m_viewCamera.Tick(deltaTime);
        }

        private void ResetGraphicsState()
        {
            GL.Viewport(0, 0, m_viewWidth, m_viewHeight);
            GL.Scissor(0, 0, m_viewWidth, m_viewHeight);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.ScissorTest);

            GL.CullFace(CullFaceMode.Back);
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
            viewMatrix = Matrix4.LookAt(m_viewCamera.Transform.Position, m_viewCamera.Transform.Position - m_viewCamera.Transform.Forward, Vector3.UnitY);
            projMatrix = m_viewCamera.ProjectionMatrix;
        }

        internal void SetViewRect(FRect rect)
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

        public FRay ProjectScreenToWorld(Vector2 mousePosition)
        {
            FRect viewportDimensions = GetViewportDimensions();
            mousePosition.X -= viewportDimensions.X;
            mousePosition.Y -= viewportDimensions.Y;

            Vector3 mouseViewportA = new Vector3(mousePosition.X/viewportDimensions.Width, mousePosition.Y/viewportDimensions.Height, 0f);
            Vector3 mouseViewportB = new Vector3(mousePosition.X/viewportDimensions.Width, mousePosition.Y/viewportDimensions.Height, 1f);

            Vector4 nearUnproj = FFrustum.UnProject(m_viewCamera.ProjectionMatrix, m_viewCamera.ViewMatrix, mouseViewportA);
            Vector4 farUnproj = FFrustum.UnProject(m_viewCamera.ProjectionMatrix, m_viewCamera.ViewMatrix, mouseViewportB);

            Vector3 dir = farUnproj.Xyz - nearUnproj.Xyz;
            dir.Normalize();

            return new FRay(nearUnproj.Xyz, dir);
        }

        public Vector2 UnprojectWorldToViewport(Vector3 worldLocation)
        {
            Matrix4 viewProjMatrix = m_viewCamera.ViewMatrix * m_viewCamera.ProjectionMatrix;

            // Transform World to Clip Space
            Vector3 clipSpacePoint = Vector3.TransformPerspective(worldLocation, viewProjMatrix);
            Vector2 viewportSpace = new Vector2((clipSpacePoint.X + 1) / 2f, (-clipSpacePoint.Y + 1) / 2f);
            return viewportSpace;
        }

        public Vector3 GetCameraPos()
        {
            return m_viewCamera.Transform.Position;
        }

        /// <summary>
        /// Returns the position of the viewport in screenspace pixel coordinates.
        /// </summary>
        public FRect GetViewportDimensions()
        {
            FRect newRect = new FRect();
            newRect.X = m_viewportRect.X * m_viewWidth;
            newRect.Y = m_viewportRect.Y * m_viewHeight;
            newRect.Width = m_viewportRect.Width * m_viewWidth;
            newRect.Height = m_viewportRect.Height * m_viewHeight;

            return newRect;
        }

        #region IDisposable Support
        ~WSceneView()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_hasBeenDisposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    m_orientationWidget.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.

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
