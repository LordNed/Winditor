using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenTK;
using OpenTK.Graphics;

namespace WindEditor.Minitors.Text
{
    /// <summary>
    /// Interaction logic for TextboxRendererWindow.xaml
    /// </summary>
    public partial class TextboxRendererWindow : Window
    {
        private TextboxRendererViewModel m_ViewModel;

        public TextboxRendererWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColorFormat cF = new ColorFormat(8);
            GraphicsMode gm = new GraphicsMode(cF, 24, 8, 1);
            glControlHost.Child = new GLControl(gm);

            m_ViewModel = (TextboxRendererViewModel)DataContext;
            m_ViewModel.OnTextboxRendererWindowLoaded((GLControl)glControlHost.Child);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
