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

        public void AddPropertiesToNode()
        {
            System.Windows.Point prop_offset = new System.Windows.Point(Position.X - 200, Position.Y + 150);

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
