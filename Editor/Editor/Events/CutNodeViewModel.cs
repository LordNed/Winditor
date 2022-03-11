using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodeNetwork.ViewModels;
using ReactiveUI;
using DynamicData;
using System.Collections.ObjectModel;
using NodeNetwork.Toolkit.ValueNode;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reactive.Linq;
using SuperBMDLib.Util;
using OpenTK;

namespace WindEditor.Events
{
    public class CopyCameraFromViewportEventArgs : EventArgs
    {
        public CutNodeViewModel RequestingCut { get; set; }
        public bool IsStart { get; set; }
    }

    public class CutNodeViewModel : NodeViewModel
    {
        public ICommand CreatePropertyCommand { get { return new RelayCommand(x => OnRequestCreatePropertyNode((Tuple<string, string, SubstanceType>)x)); } }

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

        public bool EnableConnectionUpdates
        {
            get { return m_EnableConnectionUpdates; }
            set
            {
                m_EnableConnectionUpdates = value;
            }
        }

        public ContextMenu CutContextMenu { get; private set; }

        static CutNodeViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new CutNodeView(), typeof(IViewFor<CutNodeViewModel>));
        }

        public CutNodeViewModel(Cut cut)
        {
            Cut = cut;
            cut.NodeViewModel = this;

            // Create exec input node
            NodeInputViewModel exec_input = new NodeInputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution }, MaxConnections = 1 };
            Inputs.Edit(x => x.Add(exec_input));

            exec_input.Connections.Connect()
                .Subscribe(change => {
                    OnExecInputChanged(change);
                });

            // Create exec output node
            NodeOutputViewModel exec_output = new NodeOutputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution }, MaxConnections = 1 };
            Outputs.Edit(x => x.Add(exec_output));

            exec_output.Connections.Connect()
                .Subscribe(change => {
                    OnExecOutputChanged(change);
                });

            //PropertyChanged += CutNodeViewModel_PropertyChanged;
        }

        /*private void CutNodeViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Size")
            {
                int a = 0;
            }
        }*/

        public void CreateNodesRecursive(NetworkViewModel model, Cut previous_cut)
        {
            Name = EventDefinitionManager.GetCutDisplayName(Cut.ParentActor.Name, Cut.Name);
            GenerateContextMenu();

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
            BeginNodeViewModel begin_node = new BeginNodeViewModel(Cut.ParentActor);
            model.Nodes.Edit(x => x.Add(begin_node));

            if (Cut.IsBlocked())
            {
                CreateBlockingNode(model, begin_node);
            }
            else
            {
                ConnectionViewModel connection = new ConnectionViewModel(
                    model,
                    Inputs.Items.First(),
                    begin_node.Outputs.Items.First());

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
                Substance s = Cut.Properties[i];

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

                // Create a node for the property
                SubstanceNodeViewModel temp_node = new SubstanceNodeViewModel(s);
                temp_node.Position = prop_offset;

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
            //if (!m_EnableConnectionUpdates)
                //return;

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

            Cut.ParentActor.UpdateCutList();
        }

        private void ProcessExecInputAdd(ItemChange<ConnectionViewModel> change)
        {

        }

        private void ProcessExecInputRemove(ItemChange<ConnectionViewModel> change)
        {

        }

        private void OnExecOutputChanged(DynamicData.IChangeSet<ConnectionViewModel> connection_change)
        {
            //if (!m_EnableConnectionUpdates)
                //return;

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

            Cut.ParentActor.UpdateCutList();
        }

        private void ProcessExecOutputAdd(ItemChange<ConnectionViewModel> change)
        {
            if (change.Current.Input.Parent is CutNodeViewModel cv)
            {
                Cut.NextCut = cv.Cut;
            }
            else if (change.Current.Input.Parent is BlockingCutNodeViewModel bv)
            {
                Cut.NextCut = bv.Cut;
            }
        }

        private void ProcessExecOutputRemove(ItemChange<ConnectionViewModel> change)
        {
            Cut.NextCut = null;
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
            SubstanceNodeViewModel sub_view = change.Current.Output.Parent as SubstanceNodeViewModel;
            if (sub_view == null)
                return;

            Cut.Properties.Add(sub_view.Substance);
        }

        private void ProcessPropertyInputRemove(ItemChange<ConnectionViewModel> change)
        {
            SubstanceNodeViewModel sub_view = change.Current.Output.Parent as SubstanceNodeViewModel;
            if (sub_view == null)
                return;

            int substance_index = Cut.Properties.IndexOf(sub_view.Substance);

            Inputs.Edit(x => x[substance_index + 1].Connections.Dispose());
            Inputs.Edit(x => x.RemoveAt(substance_index + 1));

            Cut.Properties.Remove(sub_view.Substance);
        }

        public void AddProperty()
        {
            PendingConnectionViewModel pending = Parent.PendingConnection;

            if (pending == null || pending.Output == null || !(pending.Output.Parent is SubstanceNodeViewModel))
                return;

            SubstanceNodeViewModel sub_view = pending.Output.Parent as SubstanceNodeViewModel;

            NodeInputViewModel new_prop_input = new NodeInputViewModel();
            new_prop_input.Connections.Connect()
                .Subscribe(change => {
                    OnPropertyInputChanged(change);
                });

            Inputs.Edit(x => x.Add(new_prop_input));

            ConnectionViewModel new_prop_connection = new ConnectionViewModel(
                Parent,
                new_prop_input,
                sub_view.Outputs.Items.First());
            Parent.Connections.Edit(x => x.Add(new_prop_connection));
        }

        public void GenerateContextMenu()
        {
            CutContextMenu = new ContextMenu();

            if (EventDefinitionManager.CanActionCopyFromViewport(Cut.ParentActor.Name, Cut.Name))
            {
                MenuItem CopyItem = new MenuItem() { Header = "Copy Camera from Viewport" };
                CopyItem.Click += (x, y) =>
                {
                    CopyCameraFromViewportEventArgs NewArgs = new CopyCameraFromViewportEventArgs();
                    NewArgs.RequestingCut = this;

                    Cut.ParentActor.ParentEvent.OnCutRequestCopyFromViewport(NewArgs);
                };

                CutContextMenu.Items.Add(CopyItem);
                CutContextMenu.Items.Add(new Separator());
            }

            if (EventDefinitionManager.CanActionCopyStartFromViewport(Cut.ParentActor.Name, Cut.Name))
            {
                MenuItem CopyItem = new MenuItem() { Header = "Copy Start Camera from Viewport" };
                CopyItem.Click += (x, y) =>
                {
                    CopyCameraFromViewportEventArgs NewArgs = new CopyCameraFromViewportEventArgs();
                    NewArgs.RequestingCut = this;
                    NewArgs.IsStart = true;

                    Cut.ParentActor.ParentEvent.OnCutRequestCopyFromViewport(NewArgs);
                };

                CutContextMenu.Items.Add(CopyItem);
                CutContextMenu.Items.Add(new Separator());
            }

            Dictionary<string, string> properties = EventDefinitionManager.GetPropertiesForAction(Cut.ParentActor.Name, Cut.Name);

            foreach (KeyValuePair<string, string> prop_str in properties)
            {
                string SubDefault = "";
                SubstanceType SubType = EventDefinitionManager.GetPropertyTypeAndDefaultValue(Cut.ParentActor.Name, Cut.Name, prop_str.Key, out SubDefault);

                CutContextMenu.Items.Add(new MenuItem()
                {
                    Header = prop_str.Key,
                    Command = CreatePropertyCommand,
                    CommandParameter = new Tuple<string, string, SubstanceType>(prop_str.Value, SubDefault, SubType)
                });
            }
        }

        private void OnRequestCreatePropertyNode(Tuple<string, string, SubstanceType> args)
        {
            Substance new_sub = null;

            switch (args.Item3)
            {
                case SubstanceType.Float:
                    float FloatDefault = 0.0f;
                    float.TryParse(args.Item2, out FloatDefault);

                    new_sub = new Substance<ObservableCollection<PrimitiveBinding<float>>>(args.Item1, args.Item3)
                    {
                        Data = new ObservableCollection<PrimitiveBinding<float>>() { new PrimitiveBinding<float>(FloatDefault) }
                    };
                    break;
                case SubstanceType.Int:
                    int IntDefault = 0;
                    int.TryParse(args.Item2, out IntDefault);

                    new_sub = new Substance<ObservableCollection<PrimitiveBinding<int>>>(args.Item1, args.Item3)
                    {   
                        Data = new ObservableCollection<PrimitiveBinding<int>>() { new PrimitiveBinding<int>(IntDefault) }
                    };
                    break;
                case SubstanceType.String:
                    new_sub = new Substance<PrimitiveBinding<string>>(args.Item1, args.Item3)
                    {
                        Data = new PrimitiveBinding<string>(args.Item2)
                    };
                    break;
                case SubstanceType.Vec3:
                    new_sub = new Substance<ObservableCollection<BindingVector3>>(args.Item1, args.Item3)
                    {
                        Data = new ObservableCollection<BindingVector3>() { new BindingVector3(new OpenTK.Vector3(0, 0, 0)) }
                    };
                    break;
            }

            SubstanceNodeViewModel temp_node = new SubstanceNodeViewModel(new_sub);
            temp_node.Position = Position;

            Parent.Nodes.Edit(x => x.Add(temp_node));

            NodeInputViewModel new_prop_input = new NodeInputViewModel();
            new_prop_input.Connections.Connect()
                .Subscribe(change => {
                    OnPropertyInputChanged(change);
                });

            Inputs.Edit(x => x.Add(new_prop_input));

            ConnectionViewModel new_prop_connection = new ConnectionViewModel(
            Parent,
            new_prop_input,
            temp_node.Outputs.Items.First());
            Parent.Connections.Edit(x => x.Add(new_prop_connection));
        }

        public void CopySettingsFromCamera(WCamera Camera, bool IsStart)
        {
            string EyeName = IsStart ? "StartEye" : "Eye";
            string CenterName = IsStart ? "StartCenter" : "Center";
            string FovyName = IsStart ? "StartFovy" : "Fovy";

            List<NodeInputViewModel> ViewModelList = Inputs.Items.ToList();
            ViewModelList.RemoveAt(0); // First input is always the execution input, so skip it because it doesn't matter for properties.

            NodeInputViewModel EyeNodeInput = ViewModelList.Find(x => x.Connections.Items.First().Output.Parent.Name.ToLower() == EyeName.ToLower());
            SubstanceNodeViewModel EyeNode = EyeNodeInput != null ? EyeNodeInput.Connections.Items.First().Output.Parent as SubstanceNodeViewModel : null;

            NodeInputViewModel CenterNodeInput = ViewModelList.Find(x => x.Connections.Items.First().Output.Parent.Name.ToLower() == CenterName.ToLower());
            SubstanceNodeViewModel CenterNode = CenterNodeInput != null ? CenterNodeInput.Connections.Items.First().Output.Parent as SubstanceNodeViewModel : null;

            NodeInputViewModel FovyNodeInput = ViewModelList.Find(x => x.Connections.Items.First().Output.Parent.Name.ToLower() == FovyName.ToLower());
            SubstanceNodeViewModel FovyNode = FovyNodeInput != null ? FovyNodeInput.Connections.Items.First().Output.Parent as SubstanceNodeViewModel : null;

            System.Windows.Point Offset = new System.Windows.Point(Position.X - 375, Position.Y + 50);

            // EyeNode was already attached to this cut
            if (EyeNode != null)
            {
                Substance<ObservableCollection<BindingVector3>> EyeSub = EyeNode.Substance as Substance<ObservableCollection<BindingVector3>>;
                EyeSub.Data = new ObservableCollection<BindingVector3>() { new BindingVector3(Camera.Transform.Position) };
            }
            // EyeNode needs to be added
            else
            {
                Substance<ObservableCollection<BindingVector3>> EyeSub = new Substance<ObservableCollection<BindingVector3>>(EyeName, SubstanceType.Vec3);
                EyeSub.Data = new ObservableCollection<BindingVector3>() { new BindingVector3(Camera.Transform.Position) };

                AddProperty(EyeSub, Offset);
            }

            Offset.Y += 125;

            // CenterNode was already attached to this cut
            if (CenterNode != null)
            {
                Substance<ObservableCollection<BindingVector3>> CenterSub = CenterNode.Substance as Substance<ObservableCollection<BindingVector3>>;
                Vector3 CenterPos = Camera.Transform.Position + (Camera.Transform.Forward * -1000.0f);
                CenterSub.Data = new ObservableCollection<BindingVector3>() { new BindingVector3(CenterPos) };
            }
            // CenterNode needs to be added
            else
            {
                Substance<ObservableCollection<BindingVector3>> CenterSub = new Substance<ObservableCollection<BindingVector3>>(CenterName, SubstanceType.Vec3);
                Vector3 CenterPos = Camera.Transform.Position + (Camera.Transform.Forward * -1000.0f);
                CenterSub.Data = new ObservableCollection<BindingVector3>() { new BindingVector3(CenterPos) };

                AddProperty(CenterSub, Offset);
            }

            Offset.Y += 125;

            // FovyNode was already attached to this cut
            if (FovyNode != null)
            {
                Substance<ObservableCollection<PrimitiveBinding<float>>> FovySub = FovyNode.Substance as Substance<ObservableCollection<PrimitiveBinding<float>>>;
                FovySub.Data = new ObservableCollection<PrimitiveBinding<float>>() { new PrimitiveBinding<float>(Camera.FieldOfView) };
            }
            // FovyNode needs to be added
            else
            {
                Substance<ObservableCollection<PrimitiveBinding<float>>> FovySub = new Substance<ObservableCollection<PrimitiveBinding<float>>>(FovyName, SubstanceType.Float);
                FovySub.Data = new ObservableCollection<PrimitiveBinding<float>>() { new PrimitiveBinding<float>(Camera.FieldOfView) };

                AddProperty(FovySub, Offset);
            }
        }

        private void AddProperty(Substance Sub, System.Windows.Point Position)
        {
            SubstanceNodeViewModel sub_view = new SubstanceNodeViewModel(Sub);
            sub_view.Position = Position;

            NodeInputViewModel new_prop_input = new NodeInputViewModel();
            new_prop_input.Connections.Connect()
                .Subscribe(change => {
                    OnPropertyInputChanged(change);
                });

            Parent.Nodes.Edit(x => x.Add(sub_view));

            Inputs.Edit(x => x.Add(new_prop_input));

            ConnectionViewModel new_prop_connection = new ConnectionViewModel(
                Parent,
                new_prop_input,
                sub_view.Outputs.Items.First());
            Parent.Connections.Edit(x => x.Add(new_prop_connection));
        }
    }
}
