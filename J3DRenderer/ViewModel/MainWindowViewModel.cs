using OpenTK.Graphics.OpenGL;
using OpenTK;
using WindEditor;
using System.IO;
using GameFormatReader.Common;
using System.ComponentModel;
using JStudio.J3D;
using JStudio.J3D.Animation;
using JStudio.OpenGL;

namespace J3DRenderer
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public J3D LoadedModel { get { return m_childLink; } }

        private GLControl m_glControl;
        private System.Diagnostics.Stopwatch m_dtStopwatch;

        // Rendering
        private WCamera m_renderCamera;
        private SimpleObjRenderer m_stockMesh;
        private int m_viewportHeight;
        private int m_viewportWidth;
        private J3D m_childLink;
        private J3D m_room;
        private float m_timeSinceStartup;

        GXLight m_mainLight;

        public MainWindowViewModel()
        {
            m_renderCamera = new WCamera();
            m_renderCamera.Transform.Position = new Vector3(500, 75, 500);
            m_renderCamera.Transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitY, WMath.DegreesToRadians(45f));
            m_dtStopwatch = new System.Diagnostics.Stopwatch();
        }

        internal void OnMainEditorWindowLoaded(GLControl child)
        {
            m_glControl = child;

            Obj obj = new Obj();
            obj.Load("Framework/EditorCube.obj");
            m_stockMesh = new SimpleObjRenderer(obj);

            m_childLink = new J3D();
            m_childLink.LoadFromStream(new EndianBinaryReader(File.ReadAllBytes("resources/cl.bdl"), Endian.Big));

            //m_room = new J3D();
            //m_room.LoadFromStream(new EndianBinaryReader(File.ReadAllBytes("resources/mDragB.bdl"), Endian.Big));

            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LoadedModel"));

            // Set up the Editor Tick Loop
            System.Windows.Forms.Timer editorTickTimer = new System.Windows.Forms.Timer();
            editorTickTimer.Interval = 16; //ms
            editorTickTimer.Tick += (o, args) =>
            {
                DoApplicationTick();
            };
            editorTickTimer.Enabled = true;

            var lightPos = new Vector4(250, 200, 250, 0);
            m_mainLight = new GXLight(lightPos, -lightPos.Normalized(), new Vector4(1, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
            var secondLight = new GXLight(lightPos, -lightPos.Normalized(), new Vector4(0, 0, 1, 1), new Vector4(1.075f, 0, 0, 0), new Vector4(1.075f, 0, 0, 0));
            m_childLink.SetHardwareLight(0, m_mainLight);
            m_childLink.SetHardwareLight(1, secondLight);
            m_childLink.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");
            //m_childLink.SetTevkColorOverride(0, WLinearColor.FromHexString("0x3F2658FF")); // Light Color
            //m_childLink.SetTevColorOverride(0, WLinearColor.FromHexString("0x455151FF")); // Ambient Color

            if(m_room != null)
            {
                m_room.SetTevkColorOverride(0, WLinearColor.FromHexString("0xFF8C27FF")); // Light Color
                //m_room.SetTevColorOverride(0, WLinearColor.FromHexString("0x566F7CFF")); // Ambient Color
            }
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
            System.Random rnd = new System.Random(m_glControl.GetHashCode());
            //GL.ClearColor(0.15f, 0.83f, 0.10f, 1f);
            GL.ClearColor(rnd.Next(255) / 255f, rnd.Next(255) / 255f, rnd.Next(255) / 255f, 1f);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GL.Viewport(0, 0, m_viewportWidth, m_viewportHeight);

            float deltaTime = m_dtStopwatch.ElapsedMilliseconds / 1000f;
            m_dtStopwatch.Restart();
            m_renderCamera.Tick(deltaTime);

            deltaTime = WMath.Clamp(deltaTime, 0, 0.25f); // quater second max because debugging
            m_timeSinceStartup += deltaTime;

            // Rotate our light
            float angleInRad = m_timeSinceStartup % WMath.DegreesToRadians(360f);
            Quaternion lightRot = Quaternion.FromAxisAngle(Vector3.UnitY, angleInRad);
            Vector3 newLightPos = Vector3.Transform(new Vector3(-500, 0, 0), lightRot) + new Vector3(0, 50, 0);
            m_mainLight.Position = new Vector4(newLightPos, 0);

            //m_model.SetHardwareLight(0, m_mainLight);


            // Render something
            //m_stockMesh.Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
            m_childLink.Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
            if (m_room != null)
                m_room.Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
        }

        internal void OnViewportResized(int width, int height)
        {
            m_viewportWidth = width;
            m_viewportHeight = height;
            m_renderCamera.AspectRatio = width / (float)height;
        }
    }
}
