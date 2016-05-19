using OpenTK.Graphics.OpenGL;
using OpenTK;
using WindEditor;
using System.IO;

namespace J3DRenderer
{
    class MainWindowViewModel
    {
        private GLControl m_glControl;
        private System.Diagnostics.Stopwatch m_dtStopwatch;

        // Rendering
        private WCamera m_renderCamera;
        private SimpleObjRenderer m_stockMesh;
        private int m_viewportHeight;
        private int m_viewportWidth;

        public MainWindowViewModel()
        {
            m_renderCamera = new WCamera();
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
        }

        internal void OnMainEditorWindowLoaded(GLControl child)
        {
            m_glControl = child;

            Obj obj = new Obj();
            obj.Load("Framework/EditorCube.obj");
            m_stockMesh = new SimpleObjRenderer(obj);

            // Set up the Editor Tick Loop
            System.Windows.Forms.Timer editorTickTimer = new System.Windows.Forms.Timer();
            editorTickTimer.Interval = 16; //ms
            editorTickTimer.Tick += (o, args) =>
            {
                DoApplicationTick();
            };
            editorTickTimer.Enabled = true;
        }

        private void DoApplicationTick()
        {
            // Poll the mouse at a high resolution
            System.Drawing.Point mousePos = m_glControl.PointToClient(System.Windows.Forms.Cursor.Position);

            mousePos.X = WMath.Clamp(mousePos.X, 0, m_glControl.Width);
            mousePos.Y = WMath.Clamp(mousePos.Y, 0, m_glControl.Height);
            WInput.SetMousePosition(new Vector2(mousePos.X, mousePos.Y));

            ProcessTick();
            WInput.Internal_UpdateInputState();

            m_glControl.SwapBuffers();
        }

        private void ProcessTick()
        {
            GL.ClearColor(0.15f, 0.83f, 0.10f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GL.Viewport(0, 0, m_viewportWidth, m_viewportHeight);

            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();

            m_renderCamera.Tick(deltaTime);


            // Render something
            m_stockMesh.Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
        }

        internal void OnViewportResized(int width, int height)
        {
            m_viewportWidth = width;
            m_viewportHeight = height;
            m_renderCamera.AspectRatio = width / (float)height;
        }
    }
}
