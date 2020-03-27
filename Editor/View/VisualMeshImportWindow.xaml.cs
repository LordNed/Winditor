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
    /// Interaction logic for VisualMeshImportWindow.xaml
    /// </summary>
    public partial class VisualMeshImportWindow : Window
    {
        public string FileName { get { return FileSelector.FileName; } }

        public int SceneNumber { get { return SceneCombo.SelectedIndex; } }

        public int SlotNumber { get { return SlotCombo.SelectedIndex; } }

        public bool GenerateMaterials { get { return (bool)materials_box.IsChecked; } }

        public VisualMeshImportWindow(WMap map)
        {
            InitializeComponent();

            SceneCombo.Items.Add("Stage");

            foreach (WScene scene in map.SceneList)
            {
                if (scene is WStage)
                    continue;

                byte room_no = 0;
                string room_removed = scene.Name.ToLowerInvariant().Replace("room", "");
                byte.TryParse(room_removed, out room_no);

                SceneCombo.Items.Add($"Room { room_no }");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void SceneCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SlotCombo.Items.Clear();

            if (SceneNumber == 0)
            {
                SlotCombo.Items.Add("Main Skybox");
                SlotCombo.Items.Add("Horizon Clouds");
                SlotCombo.Items.Add("Horizon Sea");
                SlotCombo.Items.Add("Horizon Gradient");
                SlotCombo.Items.Add("Normal Dungeon Door");
                SlotCombo.Items.Add("Boss Dungeon Door");
                SlotCombo.Items.Add("Dungeon Door Lock");
                SlotCombo.Items.Add("Dungeon Door Bars");
            }
            else
            {
                SlotCombo.Items.Add("Main");
                SlotCombo.Items.Add("Water");
                SlotCombo.Items.Add("Misc.");
                SlotCombo.Items.Add("Backfill");
            }

            SlotCombo.SelectedIndex = 0;
        }
    }
}
