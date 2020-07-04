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
    public class BlockingCutNodeViewModel : NodeViewModel
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

        static BlockingCutNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new BlockingCutNodeView(), typeof(IViewFor<BlockingCutNodeViewModel>));
        }

        public BlockingCutNodeViewModel(Cut cut)
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
    }
}
