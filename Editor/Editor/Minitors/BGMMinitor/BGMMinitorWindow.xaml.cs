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

namespace WindEditor.Minitors.BGM
{
    /// <summary>
    /// Interaction logic for BGMMinitor.xaml
    /// </summary>
    public partial class BGMMinitorWindow : Window
    {
        public BGMMinitorWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BGMMinitor context = DataContext as BGMMinitor;
            context.UpdateMapNameCombobox((BGMType)e.AddedItems[0]);
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            BGMMinitor context = DataContext as BGMMinitor;
            context.UpdateIslandNameCombobox((BGMType)e.AddedItems[0]);
        }
    }
}
