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

        public int SceneNumber { get { return RoomCombo.SelectedIndex; } }

        public IslandImportWindow(WMap map)
        {
            InitializeComponent();

            foreach (WScene scene in map.SceneList)
            {
                if (scene is WStage)
                    continue;

                byte room_no = 0;
                string room_removed = scene.Name.ToLowerInvariant().Replace("room", "");
                byte.TryParse(room_removed, out room_no);

                RoomCombo.Items.Add($"Room { room_no }");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
