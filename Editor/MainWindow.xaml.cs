using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Windows;
using System.Windows.Forms.Integration;

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
            WindowsFormsHost.EnableWindowsFormsInterop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorFormat cF = new ColorFormat(8);
            GraphicsMode gm = new GraphicsMode(cF, 24, 8, 1);
            glControlHost.Child = new GLControl(gm);
            glControlHost.SizeChanged += GlControlHost_SizeChanged;
            glControlHost.PreviewKeyDown += GlControlHost_PreviewKeyDown;
            glControlHost.PreviewKeyUp += GlControlHost_PreviewKeyUp;
            glControlHost.Child.MouseDown += GlControlHost_MouseDown;
            glControlHost.Child.MouseUp += GlControlHost_MouseUp;
            glControlHost.Child.MouseWheel += GlControlHost_MouseWheel;

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

        private void GlControlHost_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WInput.SetMouseState(WinFormToWPFMouseButton(e), false);
        }

        private void GlControlHost_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WInput.SetMouseState(WinFormToWPFMouseButton(e), true);
        }

        private void GlControlHost_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            WInput.SetMouseScrollDelta(e.Delta);
        }

        private void GlControlHost_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            WInput.SetKeyboardState(e.Key, false);
        }

        private void GlControlHost_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            WInput.SetKeyboardState(e.Key, true);
        }

        private void GlControlHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            m_editor.OnViewportResized((int)e.NewSize.Width, (int)e.NewSize.Height);
        }

        private void DoApplicationTick()
        {
            // Poll the mouse at a high resolution
            System.Drawing.Point mousePos = glControlHost.Child.PointToClient(System.Windows.Forms.Cursor.Position);

            mousePos.X = WMath.Clamp(mousePos.X, 0, (int)glControlHost.ActualWidth);
            mousePos.Y = WMath.Clamp(mousePos.Y, 0, (int)glControlHost.ActualHeight);
            WInput.SetMousePosition(new Vector2(mousePos.X, mousePos.Y));


            m_editor.ProcessTick();
            WInput.Internal_UpdateInputState();

            GLControl glControl = (GLControl)glControlHost.Child;
            glControl.SwapBuffers();
        }

        private static System.Windows.Input.MouseButton WinFormToWPFMouseButton(System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Input.MouseButton btn = System.Windows.Input.MouseButton.Left;
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    btn = System.Windows.Input.MouseButton.Left;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    btn = System.Windows.Input.MouseButton.Middle;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    btn = System.Windows.Input.MouseButton.Right;
                    break;
                case System.Windows.Forms.MouseButtons.XButton1:
                    btn = System.Windows.Input.MouseButton.XButton1;
                    break;
                case System.Windows.Forms.MouseButtons.XButton2:
                    btn = System.Windows.Input.MouseButton.XButton2;
                    break;
            }

            return btn;
        }
    }
}
