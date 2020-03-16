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
    /// Interaction logic for IslandImportWindow.xaml
    /// </summary>
    public partial class IslandImportWindow : Window
    {
        public string FileName { get { return FileSelector.FileName; } }

        public int RoomNumber { get { return RoomCombo.SelectedIndex; } }

        public IslandImportWindow()
        {
            InitializeComponent();

            for (int i = 1; i < 50; i++)
            {
                RoomCombo.Items.Add($"Room { i }");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
