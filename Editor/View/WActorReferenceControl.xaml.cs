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
using System.Reflection;
using System.ComponentModel;

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

        public void FillComboBox()
        {
            BindingExpression binding = GetBindingExpression(ActorReferenceProperty);
            Type field_type = binding.ResolvedSource.GetType().GetProperty(binding.ResolvedSourcePropertyName).PropertyType;

            WDOMNode source_object = binding.ResolvedSource as WDOMNode;
            WDOMNode cur_object = source_object;

            while (cur_object.Parent != null)
            {
                cur_object = cur_object.Parent;
            }

            List<WDOMNode> ba = cur_object.GetChildrenOfType(field_type);

            if (source_object.GetType() == field_type)
            {
                ba.Remove(source_object);
            }

            actor_combo.ItemsSource = ba;
        }

        private void Select_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ActorReference == null)
            {
                return;
            }

            Selection s = ActorReference.World.ActorEditor.EditorSelection;

            s.ClearSelection();
            s.AddToSelection(ActorReference);
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            ActorReference = null;
        }
    }
}
