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

        private NodeViewModel m_NodeViewModel;

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

        public NodeViewModel NodeViewModel
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
            NodeViewModel = new NodeViewModel() { Name = this.Name };

            NodeViewModel.Inputs.Edit(x => x.Add(new NodeInputViewModel()));
            NodeViewModel.Outputs.Edit(x => x.Add(new NodeOutputViewModel()));
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
            foreach (BaseSubstance s in Properties)
            {
                NodeInputViewModel new_prop_input = new NodeInputViewModel();
                NodeViewModel.Inputs.Edit(x => x.Add(new_prop_input));

                NodeViewModel temp_node = new NodeViewModel() { Name = s.Name };
                model.Nodes.Edit(x => x.Add(temp_node));

                NodeOutputViewModel temp_output = new NodeOutputViewModel();
                temp_node.Outputs.Edit(x => x.Add(temp_output));

                ConnectionViewModel first_to_begin = new ConnectionViewModel(
                    model,
                    new_prop_input,
                    temp_output);
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
