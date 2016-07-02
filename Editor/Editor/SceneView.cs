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

        public Matrix4 ViewMatrix { get { return m_viewCamera.ViewMatrix; } }
        public Matrix4 ProjMatrix { get { return m_viewCamera.ProjectionMatrix; } }

        private int m_viewWidth;
        private int m_viewHeight;
        private WCamera m_viewCamera;
        private WRect m_viewportRect;
        private WViewportOrientationWidget m_orientationWidget;

        private List<IRenderable> m_opaqueRenderList;
        private List<IRenderable> m_transparentRenderList;

        public WSceneView()
        {
            m_opaqueRenderList = new List<IRenderable>();
            m_transparentRenderList = new List<IRenderable>();

            m_viewportRect = new WRect(0, 0, 1f, 1f);
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


            // This is stupid
            Vector3[] frustumPoints = new Vector3[8];
            frustumPoints[0] = UnProject(projMatrix, viewMatrix, new Vector3(0, 0, 0)).Xyz; // Upper Left (Near)
            frustumPoints[1] = UnProject(projMatrix, viewMatrix, new Vector3(1, 0, 0)).Xyz; // Upper Right (Near)
            frustumPoints[2] = UnProject(projMatrix, viewMatrix, new Vector3(0, 1, 0)).Xyz; // Bottom Left (Near)
            frustumPoints[3] = UnProject(projMatrix, viewMatrix, new Vector3(1, 1, 0)).Xyz; // Bottom Right (Near)

            frustumPoints[4] = UnProject(projMatrix, viewMatrix, new Vector3(0, 0, 1)).Xyz; // Upper Left (Far)
            frustumPoints[5] = UnProject(projMatrix, viewMatrix, new Vector3(1, 0, 1)).Xyz; // Upper Right (Far)
            frustumPoints[6] = UnProject(projMatrix, viewMatrix, new Vector3(0, 1, 1)).Xyz; // Bottom Left (Far)
            frustumPoints[7] = UnProject(projMatrix, viewMatrix, new Vector3(1, 1, 1)).Xyz; // Bottom Right (Far)

            WFrustum frustum = new WFrustum(frustumPoints);

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

        private void FrustumCullList(WFrustum frustum, List<IRenderable> sourceList, List<IRenderable> outputList)
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


            Vector3 mouseViewportA = new Vector3(mousePosition.X/viewportDimensions.Width, mousePosition.Y/viewportDimensions.Height, 0f);
            Vector3 mouseViewportB = new Vector3(mousePosition.X/viewportDimensions.Width, mousePosition.Y/viewportDimensions.Height, 1f);

            //Vector2 screenSize = new Vector2(viewportDimensions.Width, viewportDimensions.Height);

            Vector4 nearUnproj = UnProject(m_viewCamera.ProjectionMatrix, m_viewCamera.ViewMatrix, mouseViewportA);
            Vector4 farUnproj = UnProject(m_viewCamera.ProjectionMatrix, m_viewCamera.ViewMatrix, mouseViewportB);

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

        public Vector3 GetCameraPos()
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

        private Vector4 UnProject(Matrix4 projection, Matrix4 view, Vector3 viewportPoint)
        {
            Vector4 clip = new Vector4();

            // Convert from Viewport Space ([0,1]) to Clip Space ([-1, 1])
            clip.X = (2.0f * viewportPoint.X) - 1;
            clip.Y = -((2.0f * viewportPoint.Y)-1);
            clip.Z = viewportPoint.Z;
            clip.W = 1.0f;

            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref clip, ref projInv, out clip);
            Vector4.Transform(ref clip, ref viewInv, out clip);

            if (clip.W > float.Epsilon || clip.W < float.Epsilon)
            {
                clip.X /= clip.W;
                clip.Y /= clip.W;
                clip.Z /= clip.W;
            }

            return clip;
        }
    }
}
