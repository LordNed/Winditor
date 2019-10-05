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
using WindEditor.Editor.Modes;

namespace WindEditor.View
{
    /// <summary>
    /// Interaction logic for WActorReferenceControl.xaml
    /// </summary>
    public partial class WActorReferenceControl : UserControl
    {
        public SourceScene Source {
            get;
            set;
        }
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

            List<WDOMNode> combo_list = new List<WDOMNode>();

            if (Source == SourceScene.Room)
            {
                combo_list = GetNodesFromRoom(binding);
            }
            else if (Source == SourceScene.Stage)
            {
                combo_list = GetNodesFromStage(binding);
            }

            actor_combo.ItemsSource = combo_list;
        }

        private List<WDOMNode> GetNodesFromRoom(BindingExpression binding)
        {
            List<WDOMNode> result = new List<WDOMNode>();

            Type field_type = binding.ResolvedSource.GetType().GetProperty(binding.ResolvedSourcePropertyName).PropertyType;
            WDOMNode source_object = binding.ResolvedSource as WDOMNode;
            WDOMNode cur_object = source_object;

            while (cur_object.Parent != null)
            {
                cur_object = cur_object.Parent;
            }

            result = cur_object.GetChildrenOfType(field_type);

            if (source_object.GetType() == field_type)
            {
                result.Remove(source_object);
            }

            return result;
        }

        private List<WDOMNode> GetNodesFromStage(BindingExpression binding)
        {
            List<WDOMNode> result = new List<WDOMNode>();

            Type field_type = binding.ResolvedSource.GetType().GetProperty(binding.ResolvedSourcePropertyName).PropertyType;
            WDOMNode source_object = binding.ResolvedSource as WDOMNode;

            WStage stage = source_object.World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
            result = stage.GetChildrenOfType(field_type);

            if (source_object.GetType() == field_type)
            {
                result.Remove(source_object);
            }

            return result;
        }

        private void Select_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ActorReference == null || ActorReference.World.CurrentMode.GetType() != typeof(ActorMode))
            {
                return;
            }

            ActorMode mode = ActorReference.World.CurrentMode as ActorMode;
            var s = mode.EditorSelection;

            s.ClearSelection();
            s.AddToSelection(ActorReference);
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            ActorReference = null;
        }
    }
}
