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

namespace WindEditor.Editors.Text
{
    /// <summary>
    /// Interaction logic for TextReferenceControl.xaml
    /// </summary>
    public partial class TextReferenceControl : UserControl
    {
        public ushort MessageID
        {
            get { return (ushort)GetValue(MessageIDProperty); }
            set { SetValue(MessageIDProperty, value); }
        }

        public static readonly DependencyProperty MessageIDProperty = DependencyProperty.Register(
            "MessageID", typeof(ushort), typeof(TextReferenceControl), new PropertyMetadata(ushort.MinValue));

        public delegate void DoLookupDelegate(ushort msg_id);

        public DoLookupDelegate DoLookup;

        public TextReferenceControl()
        {
            InitializeComponent();
        }

        private void LookupButton_Click(object sender, RoutedEventArgs e)
        {
            DoLookup.Invoke(MessageID);
        }
    }
}
