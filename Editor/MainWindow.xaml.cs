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
        private WindEditor m_editor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorFormat cF = new ColorFormat(8);
            GraphicsMode gm = new GraphicsMode(cF, 24, 8, 1);
            glControlHost.Child = new GLControl(gm);
            glControlHost.SizeChanged += GlControlHost_SizeChanged;


            // Set up the Editor Tick Loop
            m_editor = new WindEditor();
            System.Windows.Forms.Timer editorTickTimer = new System.Windows.Forms.Timer();
            editorTickTimer.Interval = 16; //ms
            editorTickTimer.Tick += (o, args) =>
            {
                DoApplicationTick();
            };
            editorTickTimer.Enabled = true;
        }

        private void GlControlHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            m_editor.OnViewportResized((int)e.NewSize.Width, (int)e.NewSize.Height);
        }

        private void DoApplicationTick()
        {
            // Poll the mouse at a high resolution
            System.Drawing.Point mousePos = glControlHost.Child.PointToClient(System.Windows.Forms.Cursor.Position);

            // ToDo: Clamp it to screne-space of the viewport.
            //m_editorCore.InputSetMousePosition(...)


            m_editor.ProcessTick();

            GLControl glControl = (GLControl)glControlHost.Child;
            glControl.SwapBuffers();
        }
    }
}
