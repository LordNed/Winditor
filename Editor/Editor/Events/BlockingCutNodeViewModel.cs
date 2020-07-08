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

        public BlockingCutEditorViewModel BlockingCutEditor { get; private set; }

        static BlockingCutNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new BlockingCutNodeView(), typeof(IViewFor<BlockingCutNodeViewModel>));
        }

        public BlockingCutNodeViewModel(Cut cut)
        {
            Cut = cut;
            Name = "Blocking Actions";
            BlockingCutEditor = new BlockingCutEditorViewModel(cut);

            // Create exec input node
            NodeInputViewModel exec_input = new NodeInputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution }, MaxConnections = 1 };
            Inputs.Edit(x => x.Add(exec_input));

            exec_input.Connections.Connect()
                .Subscribe(change => {
                    OnExecInputChanged(change);
                });

            // Create exec output node
            NodeOutputViewModel exec_output = new NodeOutputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution }, MaxConnections = 1, Editor = BlockingCutEditor };
            Outputs.Edit(x => x.Add(exec_output));

            exec_output.Connections.Connect()
                .Subscribe(change => {
                    OnExecOutputChanged(change);
                });
        }

        private void OnExecInputChanged(DynamicData.IChangeSet<ConnectionViewModel> connection_change)
        {
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
            if (change.Current.Output.Parent is CutNodeViewModel cv)
            {
                cv.Cut.NextCut = Cut;
            }
        }

        private void ProcessExecInputRemove(ItemChange<ConnectionViewModel> change)
        {

        }

        private void OnExecOutputChanged(DynamicData.IChangeSet<ConnectionViewModel> connection_change)
        {
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
            if (change.Current.Input.Parent is CutNodeViewModel c)
            {
                Cut = c.Cut;
                BlockingCutEditor.Value = Cut;

                Cut.BlockingCuts[0] = BlockingCutEditor.BlockingCut1;
                Cut.BlockingCuts[1] = BlockingCutEditor.BlockingCut2;
                Cut.BlockingCuts[2] = BlockingCutEditor.BlockingCut3;

                if (Inputs.Items.First().Connections.Count > 0)
                {
                    ConnectionViewModel a = Inputs.Items.First().Connections.Items.First();

                    if (a.Output.Parent is CutNodeViewModel prev_c)
                    {
                        prev_c.Cut.NextCut = Cut;
                    }
                    else if (a.Output.Parent is BeginNodeViewModel begin)
                    {
                        begin.Actor.FirstCut = Cut;
                    }
                }
            }
        }

        private void ProcessExecOutputRemove(ItemChange<ConnectionViewModel> change)
        {
            if (change.Current.Input.Parent is CutNodeViewModel c)
            {
                Cut.BlockingCuts[0] = null;
                Cut.BlockingCuts[1] = null;
                Cut.BlockingCuts[2] = null;

                Cut = null;
                BlockingCutEditor.Value = null;

                if (Inputs.Items.First().Connections.Count > 0)
                {
                    ConnectionViewModel a = Inputs.Items.First().Connections.Items.First();

                    if (a.Output.Parent is CutNodeViewModel prev_c)
                    {
                        prev_c.Cut.NextCut = null;
                    }
                    else if (a.Output.Parent is BeginNodeViewModel begin)
                    {
                        begin.Actor.FirstCut = null;
                    }
                }
            }
        }
    }
}
