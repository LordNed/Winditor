using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Windows;


namespace Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorFormat cF = new ColorFormat(8);
            GraphicsMode gm = new GraphicsMode(cF, 24, 8, 1);

            glControlHost.Child = new GLControl(gm);

            System.Windows.Forms.Timer editorTickTimer = new System.Windows.Forms.Timer();
            editorTickTimer.Interval = 16; //60-ish FPS
            editorTickTimer.Tick += (o, args) =>
            {
                // Poll the mouse at a high resolution
                System.Drawing.Point mousePos = glControlHost.Child.PointToClient(System.Windows.Forms.Cursor.Position);

                // ToDo: Clamp it to screne-space of the viewport.
                //m_editorCore.InputSetMousePosition(...)

                DoEditorTick();
            };
            editorTickTimer.Enabled = true;
        }


        private void DoEditorTick()
        {
            GL.ClearColor(0.6f, 0.25f, 0.35f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            GLControl glControl = (GLControl)glControlHost.Child;
            glControl.SwapBuffers();
        }
    }
}
