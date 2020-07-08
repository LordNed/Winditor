using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;
using DynamicData;

namespace WindEditor.Events
{
    public class BeginNodeViewModel : NodeViewModel
    {
        private Staff m_Actor;

        public Staff Actor
        {
            get { return m_Actor; }
            set
            {
                if (m_Actor != value)
                {
                    m_Actor = value;
                }
            }
        }

        static BeginNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeView(), typeof(IViewFor<BeginNodeViewModel>));
        }

        public BeginNodeViewModel(Staff actor)
        {
            Actor = actor;
            Name = Actor.Name;

            // Create exec output node
            NodeOutputViewModel exec_output = new NodeOutputViewModel() {Name="Begin", Port = new ExecPortViewModel() { PortType = PortType.Execution }, MaxConnections = 1 };
            Outputs.Edit(x => x.Add(exec_output));

            exec_output.Connections.Connect()
                .Subscribe(change => {
                    OnExecOutputChanged(change);
                });
        }

        public void OnExecOutputChanged(IChangeSet<ConnectionViewModel> connection_change)
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
            if (change.Current.Input.Parent is CutNodeViewModel cv)
            {
                Actor.FirstCut = cv.Cut;
            }
            else if (change.Current.Input.Parent is BlockingCutNodeViewModel bv)
            {
                Actor.FirstCut = bv.Cut;
            }
        }

        private void ProcessExecOutputRemove(ItemChange<ConnectionViewModel> change)
        {
            Actor.FirstCut = null;
        }
    }
}
