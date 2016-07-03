using OpenTK.Graphics.OpenGL;
using OpenTK;
using WindEditor;
using System.IO;
using GameFormatReader.Common;
using System.ComponentModel;
using JStudio.J3D;
using JStudio.J3D.Animation;
using JStudio.OpenGL;
using J3DRenderer.Framework;

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
        private float m_timeSinceStartup;

        private J3D[] m_skybox;

        private ScreenspaceQuad m_screenQuad;
        private Shader m_alphaVisualizationShader;

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

            m_alphaVisualizationShader = new Shader("AlphaVisualize");
            m_alphaVisualizationShader.CompileSource(File.ReadAllText("resources/shaders/Debug_AlphaVisualizer.vert"), ShaderType.VertexShader);
            m_alphaVisualizationShader.CompileSource(File.ReadAllText("resources/shaders/Debug_AlphaVisualizer.frag"), ShaderType.FragmentShader);
            m_alphaVisualizationShader.LinkShader();

            m_screenQuad = new ScreenspaceQuad();

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
            LoadChildLink(secondLight);
            m_childLink.SetTevkColorOverride(0, WLinearColor.FromHexString("0x3F2658FF")); // Light Color
            m_childLink.SetTevColorOverride(0, WLinearColor.FromHexString("0x455151FF")); // Ambient Color

            m_skybox = new J3D[4];
            for (int i = 500; i < m_skybox.Length; i++)
            {
                string[] fileNames = new[] { "vr_sky.bdl", "vr_kasumi_mae.bdl", "vr_uso_umi.bdl", "vr_back_cloud.bdl" };

                m_skybox[i] = new J3D();
                m_skybox[i].LoadFromStream(new EndianBinaryReader(File.ReadAllBytes("resources/skybox/" + fileNames[i]), Endian.Big));

                if (i == 0)
                {
                    m_skybox[i].SetTevkColorOverride(0, WLinearColor.FromHexString("0x1E3C5AFF")); // vr_sky Walls
                    m_skybox[i].SetTevColorOverride(0, WLinearColor.FromHexString("0xC8FFFFFF")); // vr_sky Floor
                }
                if (i == 1)
                {
                    m_skybox[i].SetTevColorOverride(0, WLinearColor.FromHexString("0x325a82FF")); // vr_kasumi_mae
                }
                if (i == 2)
                    m_skybox[i].SetTevkColorOverride(0, WLinearColor.FromHexString("0x0A0A3CFF")); // vr_uso_umi
                if (i == 3)
                {
                    m_skybox[i].SetTevColorOverride(0, WLinearColor.FromHexString("0x8278966E")); // vr_back_cloud
                    //m_skybox[i].SetTevkColorOverride(0, WLinearColor.FromHexString("0xFFFFFF00")); // vr_back_cloud
                }


            }

            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LoadedModel"));
        }

        private void LoadChildLink(GXLight secondLight)
        {
            m_childLink = new J3D();
            m_childLink.LoadFromStream(new EndianBinaryReader(File.ReadAllBytes("resources/model_normalstest.bmd"), Endian.Big));
            m_childLink.SetHardwareLight(0, m_mainLight);
            m_childLink.SetHardwareLight(1, secondLight);
            m_childLink.SetTextureOverride("ZBtoonEX", "resources/textures/ZBtoonEX.png");

            // Animations
            foreach (var bck in Directory.GetFiles("resources/cl/bcks/"))
            {
                m_childLink.LoadBoneAnimation(bck);
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
            GL.ClearColor(rnd.Next(255) / 255f, rnd.Next(255) / 255f, rnd.Next(255) / 255f, 1);

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

            // Render something
            for (int i = 0; i < m_skybox.Length; i++)
            {
                if (m_skybox[i] == null)
                    continue;

                m_skybox[i].Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
            }

            if (m_childLink != null)
            {
                m_childLink.SetHardwareLight(0, m_mainLight);
                m_childLink.Tick(deltaTime);
                m_childLink.Render(m_renderCamera.ViewMatrix, m_renderCamera.ProjectionMatrix, Matrix4.Identity);
            }

            // Debug Rendering
            if (WInput.GetKey(System.Windows.Input.Key.I))
            {
                GL.Disable(EnableCap.CullFace);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.DstAlpha, BlendingFactorDest.Zero);

                m_alphaVisualizationShader.Bind();
                m_screenQuad.Draw();
            }
        }

        internal void OnViewportResized(int width, int height)
        {
            m_viewportWidth = width;
            m_viewportHeight = height;
            m_renderCamera.AspectRatio = width / (float)height;
        }
    }
}
