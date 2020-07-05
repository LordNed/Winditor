using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNetwork.ViewModels;
using ReactiveUI;
using DynamicData;

namespace WindEditor.Events
{
    public class CutNodeViewModel : NodeViewModel
    {
        private Cut m_Cut;
        private bool m_EnableConnectionUpdates;

        public Cut Cut
        {
            get { return m_Cut; }
            set
            {
                if (m_Cut != value)
                {
                    m_Cut = value;
                }
            }
        }

        static CutNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new CutNodeView(), typeof(IViewFor<CutNodeViewModel>));
        }

        public CutNodeViewModel(Cut cut)
        {
            Cut = cut;

            // Create exec input node
            NodeInputViewModel exec_input = new NodeInputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution } };
            Inputs.Edit(x => x.Add(exec_input));

            exec_input.Connections.Connect()
                .Subscribe(change => {
                    OnExecInputChanged(change);
                });

            // Create exec output node
            NodeOutputViewModel exec_output = new NodeOutputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution } };
            Outputs.Edit(x => x.Add(exec_output));

            exec_output.Connections.Connect()
                .Subscribe(change => {
                    OnExecOutputChanged(change);
                });
        }

        public void CreateNodesRecursive(NetworkViewModel model, Cut previous_cut)
        {
            // Add ourselves to the node network
            model.Nodes.Edit(x => x.Add(this));

            // If this is the first node in the chain, create the "Begin" node.
            if (previous_cut == null)
            {
                Position = new System.Windows.Point(Position.X + 400, Position.Y);

                CreateBeginNode(model);
            }
            else
            {
                Position = new System.Windows.Point(previous_cut.NodeViewModel.Position.X + 400, previous_cut.NodeViewModel.Position.Y);

                // If this node is blocked, create a blocking node and insert it between the previous node and this node.
                if (Cut.IsBlocked())
                {
                    CreateBlockingNode(model, previous_cut.NodeViewModel);
                }
                // Otherwise, just connect the previous to this node.
                else
                {
                    // Create a connection between this cut and the next cut in the list.
                    ConnectionViewModel cut_connection = new ConnectionViewModel(
                        model,
                        Inputs.Items.First(),
                        previous_cut.NodeViewModel.Outputs.Items.First());

                    // Add the connection to the node network.
                    model.Connections.Edit(x => x.Add(cut_connection));
                }
            }

            // Now that our position is finalized, we can add our property nodes to the graph.
            AddPropertiesToNode();

            if (Cut.NextCut != null)
                Cut.NextCut.NodeViewModel.CreateNodesRecursive(model, Cut);
        }

        private void CreateBeginNode(NetworkViewModel model)
        {
            // Create the begin node for the actor and add it to the network.
            NodeViewModel begin_node = new NodeViewModel() { Name = Cut.ParentActor.Name };
            model.Nodes.Edit(x => x.Add(begin_node));

            // Add an output to the begin node labelled "Begin".
            NodeOutputViewModel begin_output = new NodeOutputViewModel() { Name = "Begin", Port = new ExecPortViewModel { PortType = PortType.Execution } };
            begin_node.Outputs.Edit(x => x.Add(begin_output));

            if (Cut.IsBlocked())
            {
                CreateBlockingNode(model, begin_node);
            }
            else
            {
                ConnectionViewModel connection = new ConnectionViewModel(
                    model,
                    Inputs.Items.First(),
                    begin_output);

                // Add the connection to the node network.
                model.Connections.Edit(x => x.Add(connection));
            }
        }

        private void CreateBlockingNode(NetworkViewModel model, NodeViewModel previous_node)
        {
            // Create blocking node and add it to the graph
            BlockingCutNodeViewModel blocking_node = new BlockingCutNodeViewModel(Cut);
            model.Nodes.Edit(x => x.Add(blocking_node));

            // Set the blocking node's position to where this cut node was
            blocking_node.Position = (System.Windows.Point)(Position - new System.Windows.Point(150, 0));

            // Move this cut node forward to not overlap the blocking node
            Position = new System.Windows.Point(Position.X + 450, Position.Y);

            ConnectionViewModel previous_to_blocking = new ConnectionViewModel(
                model,
                blocking_node.Inputs.Items.First(),
                previous_node.Outputs.Items.First());

            ConnectionViewModel blocking_to_current = new ConnectionViewModel(
                model,
                Inputs.Items.First(),
                blocking_node.Outputs.Items.First());

            // Add the connections to the node network.
            model.Connections.Edit(x => x.Add(previous_to_blocking));
            model.Connections.Edit(x => x.Add(blocking_to_current));
        }

        public void AddPropertiesToNode()
        {
            System.Windows.Point prop_offset = new System.Windows.Point(Position.X - 200, Position.Y + 70);

            for (int i = 0; i < Cut.Properties.Count; i++)
            {
                BaseSubstance s = Cut.Properties[i];

                // If we have enough node inputs already, just grab the one corresponding to this property;
                // Otherwise, add a new input to the input view model.
                NodeInputViewModel prop_input = new NodeInputViewModel();
                if (Inputs.Count > i + 1)
                {
                    prop_input = Inputs.Items.ElementAt(i + 1);
                }
                else
                {
                    Inputs.Edit(x => x.Add(prop_input));
                }

                if (Cut.Properties[i] == null)
                    continue;

                prop_input.Connections.Connect()
                    .Subscribe(change => {
                        OnPropertyInputChanged(change);
                    });

                // Create a node for the property and add the property's relevant substance editor.
                NodeViewModel temp_node = new NodeViewModel() { Name = s.Name, Position = prop_offset };
                s.AddSubstanceEditor(temp_node);

                Parent.Nodes.Edit(x => x.Add(temp_node));

                prop_offset.Y = (temp_node.Position.Y + temp_node.Size.Height + 125);

                // Connect the property node to the cut node.
                ConnectionViewModel first_to_begin = new ConnectionViewModel(
                    Parent,
                    prop_input,
                    temp_node.Outputs.Items.First());
                Parent.Connections.Edit(x => x.Add(first_to_begin));
            }

            m_EnableConnectionUpdates = true;
        }

        private void OnExecInputChanged(DynamicData.IChangeSet<ConnectionViewModel> connection_change)
        {
            if (!m_EnableConnectionUpdates)
                return;

            Change<ConnectionViewModel>[] changes_array = connection_change.ToArray();

            if (changes_array.Length <= 0)
                return;

            Change<ConnectionViewModel> change = changes_array[0];

            switch (change.Reason)
            {
                case ListChangeReason.Add:
                    ProcessExecInputAdd(change.Item);
                    break;
                case ListChangeReason.Remove:
                    ProcessExecInputRemove(change.Item);
                    break;
                default:
                    break;
            }
        }

        private void ProcessExecInputAdd(ItemChange<ConnectionViewModel> change)
        {

        }

        private void ProcessExecInputRemove(ItemChange<ConnectionViewModel> change)
        {

        }

        private void OnExecOutputChanged(DynamicData.IChangeSet<ConnectionViewModel> connection_change)
        {
            if (!m_EnableConnectionUpdates)
                return;

            Change<ConnectionViewModel>[] changes_array = connection_change.ToArray();

            if (changes_array.Length <= 0)
                return;

            Change<ConnectionViewModel> change = changes_array[0];

            switch (change.Reason)
            {
                case ListChangeReason.Add:
                    ProcessExecOutputAdd(change.Item);
                    break;
                case ListChangeReason.Remove:
                    ProcessExecOutputRemove(change.Item);
                    break;
                default:
                    break;
            }
        }

        private void ProcessExecOutputAdd(ItemChange<ConnectionViewModel> change)
        {

        }

        private void ProcessExecOutputRemove(ItemChange<ConnectionViewModel> change)
        {

        }

        private void OnPropertyInputChanged(DynamicData.IChangeSet<ConnectionViewModel> connection_change)
        {
            if (!m_EnableConnectionUpdates)
                return;

            Change<ConnectionViewModel>[] changes_array = connection_change.ToArray();

            if (changes_array.Length <= 0)
                return;

            Change<ConnectionViewModel> change = changes_array[0];

            switch(change.Reason)
            {
                case ListChangeReason.Add:
                    ProcessPropertyInputAdd(change.Item);
                    break;
                case ListChangeReason.Remove:
                    ProcessPropertyInputRemove(change.Item);
                    break;
                default:
                    break;
            }
        }

        private void ProcessPropertyInputAdd(ItemChange<ConnectionViewModel> change)
        {
            // Index of the property is input index - 1. Subtract 1 because the list of inputs includes the exec input!
            int property_index = Inputs.Items.IndexOf(change.Current.Input) - 1;

            NodeOutputViewModel prop_output = change.Current.Output;
            //Cut.Properties[property_index] = change.Current.Output.
        }

        private void ProcessPropertyInputRemove(ItemChange<ConnectionViewModel> change)
        {
            // Index of the property is input index - 1. Subtract 1 because the list of inputs includes the exec input!
            int property_index = Inputs.Items.IndexOf(change.Current.Input) - 1;

            Cut.Properties[property_index] = null;
        }
    }
}
