using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;

namespace WindEditor.Events
{
    public class ActorTabContextMenu : ContextMenu
    {
        private Staff m_Actor;
        public System.Windows.Point m_MousePoint;

        public ICommand CreateNodeCommand { get { return new RelayCommand(x => OnRequestCreateActorNode((string)x)); } }
        public ICommand CreatePropertyCommand { get { return new RelayCommand(x => OnRequestCreatePropertyNode((KeyValuePair<string, SubstanceType>)x)); } }

        public ActorTabContextMenu(Staff actor) : base()
        {
            m_Actor = actor;

            MenuItem add_block = new MenuItem() { Header = "Add Blocking Node" };
            add_block.Click += Add_block_Click;

            Items.Add(add_block);
            Items.Add(new Separator());

            // Cuts
            MenuItem cuts_block = new MenuItem() { Header = "Actions" };
            Items.Add(cuts_block);

            // Properties
            MenuItem props_block = new MenuItem() { Header = "Properties" };
            Items.Add(props_block);

            Dictionary<string, string> actions = EventDefinitionManager.GetActionsForActor(actor.Name);

            foreach (KeyValuePair<string, string> str in actions)
            {
                cuts_block.Items.Add(new MenuItem() { Header = str.Value, Command = CreateNodeCommand, CommandParameter = str.Key });

                Dictionary<string, string> properties = EventDefinitionManager.GetPropertiesForAction(actor.Name, str.Key);

                MenuItem action_prop_block = new MenuItem() { Header = str.Value };
                props_block.Items.Add(action_prop_block);

                foreach (KeyValuePair<string, string> prop_str in properties)
                {

                    action_prop_block.Items.Add(new MenuItem() { Header = prop_str.Key, Command = CreatePropertyCommand,
                        CommandParameter = new KeyValuePair<string, SubstanceType>(prop_str.Value, EventDefinitionManager.GetPropertyType(actor.Name, str.Key, prop_str.Key)) });
                }
            }
        }

        private Point GetMouseLocation(NetworkView view)
        {
            System.Windows.Point p = System.Windows.Input.Mouse.GetPosition(view);

            return new Point(p.X + view.NetworkViewportRegion.X, p.Y + view.NetworkViewportRegion.Y);
        }

        private void Add_block_Click(object sender, RoutedEventArgs e)
        {
            NetworkView view = PlacementTarget as NetworkView;
            BlockingCutNodeViewModel b = new BlockingCutNodeViewModel(null);

            b.Position = GetMouseLocation(view);

            view.ViewModel.Nodes.Edit(x => x.Add(b));
        }

        private void OnRequestCreateActorNode(string cut_name)
        {
            NetworkView view = PlacementTarget as NetworkView;

            Cut c = new Cut(cut_name);
            c.ParentActor = m_Actor;

            CutNodeViewModel cv = new CutNodeViewModel(c);
            cv.Name = EventDefinitionManager.GetCutDisplayName(m_Actor.Name, cut_name);
            cv.Position = GetMouseLocation(view);

            view.ViewModel.Nodes.Edit(x => x.Add(cv));
        }

        private void OnRequestCreatePropertyNode(KeyValuePair<string, SubstanceType> args)
        {
            NetworkView view = PlacementTarget as NetworkView;

            Substance new_sub = null;

            switch (args.Value)
            {
                case SubstanceType.Float:
                    new_sub = new Substance<ObservableCollection<PrimitiveBinding<float>>>(args.Key, args.Value)
                    {
                        Data = new ObservableCollection<PrimitiveBinding<float>>() { new PrimitiveBinding<float>(0.0f) }
                    };
                    break;
                case SubstanceType.Int:
                    new_sub = new Substance<ObservableCollection<PrimitiveBinding<int>>>(args.Key, args.Value)
                    {
                        Data = new ObservableCollection<PrimitiveBinding<int>>() { new PrimitiveBinding<int>(0) }
                    };
                    break;
                case SubstanceType.String:
                    new_sub = new Substance<PrimitiveBinding<string>>(args.Key, args.Value)
                    {
                        Data = new PrimitiveBinding<string>("")
                    };
                    break;
                case SubstanceType.Vec3:
                    new_sub = new Substance<ObservableCollection<BindingVector3>>(args.Key, args.Value)
                    {
                        Data = new ObservableCollection<BindingVector3>() { new BindingVector3(new OpenTK.Vector3(0, 0, 0)) }
                    };
                    break;
            }

            SubstanceNodeViewModel temp_node = new SubstanceNodeViewModel(new_sub);
            temp_node.Position = GetMouseLocation(view);

            view.ViewModel.Nodes.Edit(x => x.Add(temp_node));
        }
    }
}
