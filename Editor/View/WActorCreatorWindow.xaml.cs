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

        private string m_SearchFilter;

        public WActorDescriptor Descriptor { get { return ActorTypeView.SelectedItem as WActorDescriptor; } }

        public WActorCreatorWindow()
        {
            InitializeComponent();

            m_Descriptors = WResourceManager.GetActorDescriptors();
            ActorTypeView.ItemsSource = m_Descriptors.Values;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ActorTypeView.ItemsSource);
            view.Filter = FilterActorTypes;

            SearchFilter = "";
        }

        private void create_button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string SearchFilter
        {
            get { return m_SearchFilter; }
            set
            {
                if (value != m_SearchFilter)
                {
                    m_SearchFilter = value;
                    //OnPropertyChanged("SearchFilter");

                    CollectionViewSource.GetDefaultView(ActorTypeView.ItemsSource).Refresh();
                }
            }
        }

        private bool FilterActorTypes(object item)
        {
            if (string.IsNullOrEmpty(SearchFilter))
            {
                return true;
            }
            else
            {
                WActorDescriptor descriptor = item as WActorDescriptor;

                if (descriptor.ActorName.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
                if (descriptor.Description.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }

                return false;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchTextBox.Text;
        }
    }
}
