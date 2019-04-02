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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for WDetailsView.xaml
    /// </summary>
    public partial class WDetailsView : UserControl
    {
        public WDetailsView()
        {
            InitializeComponent();
            DataContextChanged += WDetailsView_DataContextChanged;
        }

        private void WDetailsView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
