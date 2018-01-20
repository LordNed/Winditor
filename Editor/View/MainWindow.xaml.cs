using WindEditor.ViewModel;
using OpenTK;
using OpenTK.Graphics;
using System.Windows;
using System.Windows.Forms.Integration;
using System.IO;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace WindEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel m_viewModel;

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

            m_viewModel = (MainWindowViewModel)DataContext;
            m_viewModel.OnMainEditorWindowLoaded((GLControl)glControlHost.Child);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if(droppedFilePaths.Length > 0)
                {
                    // Only check the first thing dropped in, we except them to drag/drop a map in, not a set of scenes.
                    if(Directory.Exists(droppedFilePaths[0]))
                    {
                        m_viewModel.WindEditor.LoadProject(droppedFilePaths[0], droppedFilePaths[0]);
                    }
                }
            }
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
            m_viewModel.WindEditor.OnViewportResized((int)e.NewSize.Width, (int)e.NewSize.Height);
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

		/// <summary>
		/// We can't use PropertyBinding on the PropertyDefinitions, but we can get a callback when the object changes
		/// and then get the properties off of them manually, insert them, and then forcibly update it again... :D
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PropertyGrid_SelectedObjectChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			PropertyGrid grid = sender as PropertyGrid;
			if(grid != null)
			{
				grid.PropertyDefinitions.Clear();
				WDOMNode node = grid.SelectedObject as WDOMNode;
				if(node != null)
				{
					foreach(var property in node.VisibleProperties)
					{
						grid.PropertyDefinitions.Add(property);
					}

					grid.Update();
				}
			}
		}
	}
}
