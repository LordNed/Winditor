using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NodeNetwork.Views;

namespace WindEditor.Events
{
    public class ActorTabContextMenu : ContextMenu
    {
        private Staff m_Actor;
        public System.Windows.Point m_MousePoint;

        public ICommand CreateNodeCommand { get { return new RelayCommand(x => OnRequestCreateActorNode((string)x)); } }

        public ActorTabContextMenu(Staff actor) : base()
        {
            Height = 400;
            m_Actor = actor;

            MenuItem add_block = new MenuItem() { Header = "Add Blocking Node" };
            add_block.Click += Add_block_Click;

            Items.Add(add_block);
            Items.Add(new Separator());

            Dictionary<string, string> actions = EventDefinitionManager.GetActionsForActor(actor.Name);

            foreach (KeyValuePair<string, string> str in actions)
            {
                Items.Add(new MenuItem() { Header = str.Value, Command = CreateNodeCommand, CommandParameter = str.Key });
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
    }
}
