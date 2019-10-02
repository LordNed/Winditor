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
using OpenTK;
using Xceed.Wpf.Toolkit;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for WTransformControl.xaml
    /// </summary>
    public partial class WTransformControl : UserControl
    {
        public WTransformControl()
        {
            InitializeComponent();
        }

        public BindingVector3 BackingVector
        {
            get { return (BindingVector3)GetValue(Vector3Property); }
            set { SetValue(Vector3Property, value); }
        }
        public static readonly DependencyProperty Vector3Property = DependencyProperty.Register(
            "BackingVector", typeof(BindingVector3), typeof(WTransformControl), new PropertyMetadata(new BindingVector3(Vector3.Zero)));
    }
}
