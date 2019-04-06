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
    /// Interaction logic for OptionsMenu.xaml
    /// </summary>
    public partial class OptionsMenu : Window
    {
        private OptionsMenuViewModel m_ViewModel;
        private WDetailsViewViewModel m_DetailsModel;

        public OptionsMenu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_ViewModel = (OptionsMenuViewModel)DataContext;
            m_DetailsModel = (WDetailsViewViewModel)SettingsDetails.DataContext;

            m_DetailsModel.ReflectObject(m_ViewModel.Settings);
        }
    }
}
