using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WindEditor.Properties;
using GameFormatReader.Common;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;

namespace WindEditor.Events
{
    [HideCategories()]
    public class Cut : INotifyPropertyChanged
    {
        public List<BaseSubstance> Properties { get; private set; }

        private string m_Name;

        private int m_DuplicateID;

        private int m_CheckFlag1;
        private int m_CheckFlag2;
        private int m_CheckFlag3;

        private int m_Flag;

        private int m_FirstSubstanceIndex;
        private int m_NextCutIndex;

        private Cut m_NextCut;

        private CutNodeViewModel m_NodeViewModel;

        public Cut NextCut
        {
            get { return m_NextCut; }
            set
            {
                if (value != m_NextCut)
                {
                    m_NextCut = value;
                    OnPropertyChanged("NextCut");
                }
            }
        }

        public CutNodeViewModel NodeViewModel
        {
            get { return m_NodeViewModel; }
            set
            {
                if (value != m_NodeViewModel)
                {
                    m_NodeViewModel = value;
                    OnPropertyChanged("NodeViewModel");
                }
            }
        }

        [WProperty("Cut", "Name", true, "Name of the action.")]
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value != m_Name)
                {
                    m_Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public Cut()
        {
            Properties = new List<BaseSubstance>();

            Name = "new_cut";
            NextCut = null;

            CreateNodeViewModel();
        }

        public Cut(EndianBinaryReader reader)
        {
            Properties = new List<BaseSubstance>();
            NextCut = null;

            Name = new string(reader.ReadChars(32)).Trim('\0');
            m_DuplicateID = reader.ReadInt32();

            reader.SkipInt32();

            m_CheckFlag1 = reader.ReadInt32();
            m_CheckFlag2 = reader.ReadInt32();
            m_CheckFlag3 = reader.ReadInt32();

            m_Flag = reader.ReadInt32();

            m_FirstSubstanceIndex = reader.ReadInt32();
            m_NextCutIndex = reader.ReadInt32();

            reader.Skip(16);

            CreateNodeViewModel();
        }

        private void CreateNodeViewModel()
        {
            NodeViewModel = new CutNodeViewModel(this) { Name = this.Name };

            NodeInputViewModel exec_input = new NodeInputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution } };
            NodeViewModel.Inputs.Edit(x => x.Add(exec_input));

            exec_input.Connections.Connect()
                .Subscribe(change => {
                    var test = change.ToArray();
                    Console.WriteLine(test[0].Reason);
                });

            NodeOutputViewModel exec_output = new NodeOutputViewModel() { Port = new ExecPortViewModel() { PortType = PortType.Execution } };

            NodeViewModel.Outputs.Edit(x => x.Add(exec_output));
        }

        public void AssignNextCutAndSubstances(List<Cut> cut_list, List<BaseSubstance> substance_list)
        {
            if (m_NextCutIndex != -1)
            {
                NextCut = cut_list[m_NextCutIndex];
            }

            if (m_FirstSubstanceIndex != -1)
            {
                BaseSubstance subs = substance_list[m_FirstSubstanceIndex];

                while (subs != null)
                {
                    Properties.Add(subs);
                    subs = subs.NextSubstance;
                }
            }
        }

        public void AddPropertiesToNode(NetworkViewModel model)
        {
            System.Windows.Point prop_offset = new System.Windows.Point(NodeViewModel.Position.X - 200, NodeViewModel.Position.Y + 100);

            for (int i = 0; i < Properties.Count; i++)
            {
                BaseSubstance s = Properties[i];

                // If we have enough node inputs already, just grab the one corresponding to this property;
                // Otherwise, add a new input to the input view model.
                NodeInputViewModel prop_input = new NodeInputViewModel();
                if (NodeViewModel.Inputs.Count > i + 1)
                {
                    prop_input = NodeViewModel.Inputs.Items.ElementAt(i + 1);
                }
                else
                {
                    NodeViewModel.Inputs.Edit(x => x.Add(prop_input));
                }

                // Create a node for the property and add the property's relevant substance editor.
                NodeViewModel temp_node = new NodeViewModel() { Name = s.Name, Position = prop_offset };
                s.AddSubstanceEditor(temp_node);

                model.Nodes.Edit(x => x.Add(temp_node));

                prop_offset.Y += 100;

                // Connect the property node to the cut node.
                ConnectionViewModel first_to_begin = new ConnectionViewModel(
                    model,
                    prop_input,
                    temp_node.Outputs.Items.First());
                model.Connections.Edit(x => x.Add(first_to_begin));
            }
        }

        public override string ToString()
        {
            return $"Name: \"{ Name }\", Property Count: { Properties.Count }";
        }

        #region INotifyPropertyChanged Support

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
