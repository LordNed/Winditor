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
    /// Interaction logic for CollisionImportWindow.xaml
    /// </summary>
    public partial class CollisionImportWindow : Window
    {
        public string FileName { get { return FileSelector.FileName; } }

        public int RoomNumber { get { return RoomCombo.SelectedIndex; } }

        public CollisionImportWindow(WMap map)
        {
            InitializeComponent();

            int room_count = map.SceneList.Count - 1;

            for (int i = 0; i < room_count; i++)
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
