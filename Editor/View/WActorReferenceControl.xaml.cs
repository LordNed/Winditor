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
    /// Interaction logic for WActorReferenceControl.xaml
    /// </summary>
    public partial class WActorReferenceControl : UserControl
    {
        public WDOMNode ActorReference
        {
            get { return (WDOMNode)GetValue(ActorReferenceProperty); }
            set { SetValue(ActorReferenceProperty, value); }
        }

        public static readonly DependencyProperty ActorReferenceProperty = DependencyProperty.Register(
            "ActorReference", typeof(WDOMNode), typeof(WActorReferenceControl), new PropertyMetadata(null));

        public WActorReferenceControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ActorReference.Destroy();
        }
    }
}
