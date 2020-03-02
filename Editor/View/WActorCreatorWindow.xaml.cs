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

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for WActorCreatorWindow.xaml
    /// </summary>
    public partial class WActorCreatorWindow : Window
    {
        private Dictionary<string, WActorDescriptor> m_Descriptors;

        public WActorDescriptor Descriptor { get { return type_view.SelectedItem as WActorDescriptor; } }

        public WActorCreatorWindow()
        {
            InitializeComponent();

            m_Descriptors = WResourceManager.GetActorDescriptors();
            type_view.ItemsSource = m_Descriptors.Values;
        }

        private void create_button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
