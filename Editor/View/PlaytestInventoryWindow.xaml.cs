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
using WindEditor.ViewModel;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for PlaytestInventoryWindow.xaml
    /// </summary>
    public partial class PlaytestInventoryWindow : Window
    {
        public PlaytestInventoryViewModel ViewModel { get; private set; }

        public PlaytestInventoryWindow()
        {
            InitializeComponent();

            ViewModel = new PlaytestInventoryViewModel(this);
            DataContext = ViewModel;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox Cbox = sender as CheckBox;

            if (!ViewModel.TempUncheckedBoxes.Contains(Cbox))
                ViewModel.TempCheckedBoxes.Add(Cbox);

            ViewModel.OnCheckboxChecked(Convert.ToByte(Cbox.Tag));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox Cbox = sender as CheckBox;

            if (!ViewModel.TempCheckedBoxes.Contains(Cbox))
                ViewModel.TempUncheckedBoxes.Add(Cbox);

            ViewModel.OnCheckboxUnchecked(Convert.ToByte(Cbox.Tag));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null)
                return;

            if (ViewModel.TempComboBoxChanges.Find(x => x.Source == e.Source) == null)
                ViewModel.TempComboBoxChanges.Add(e);

            ComboBoxItem NewSelection = e.AddedItems.Count != 0 ? e.AddedItems[0] as ComboBoxItem : null;
            ComboBoxItem OldSelection = e.RemovedItems.Count != 0 ? e.RemovedItems[0] as ComboBoxItem : null;

            byte NewValue = NewSelection != null ? Convert.ToByte(NewSelection.Tag) : (byte)255;
            byte OldValue = OldSelection != null ? Convert.ToByte(OldSelection.Tag) : (byte)255;

            ViewModel.OnComboBoxChanged(OldValue, NewValue);
        }

        private void Triforce_ComboBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null)
                return;

            if (ViewModel.TempTriforceBoxChanges == null)
                ViewModel.TempTriforceBoxChanges = e;

            ComboBox Cbox = sender as ComboBox;
            ViewModel.OnTriforceCountChanged(Cbox.SelectedIndex);
        }
    }
}
