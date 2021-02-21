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
        public List<Substance> Properties { get; private set; }

        private string m_Name;

        private int m_DuplicateID;

        private int[] m_CheckFlags = new int[3];

        private int m_FirstSubstanceIndex;
        private int m_NextCutIndex;

        private Staff m_ParentActor;

        private Cut m_NextCut;
        private Cut[] m_BlockingCuts = new Cut[3];

        private CutNodeViewModel m_NodeViewModel;

        public Staff ParentActor
        {
            get { return m_ParentActor; }
            set
            {
                if (value != m_ParentActor)
                {
                    m_ParentActor = value;
                    OnPropertyChanged("ParentActor");
                }
            }
        }

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

        public Cut[] BlockingCuts
        {
            get { return m_BlockingCuts; }
            set
            {
                if (value != m_BlockingCuts)
                {
                    m_BlockingCuts = value;
                    OnPropertyChanged("BlockingCuts");
                }
            }
        }

        public CutNodeViewModel NodeViewModel
        {
            get
            {
                if (m_NodeViewModel == null)
                {
                    m_NodeViewModel = new CutNodeViewModel(this) { Name = this.Name };
                }
                return m_NodeViewModel;
            }
            set
            {
                if (m_NodeViewModel != value)
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
                    m_Name = $"{value}";
                    OnPropertyChanged("Name");
                }
            }
        }

        public int Flag { get; private set; }

        public Cut(string name)
        {
            Properties = new List<Substance>();

            Name = name;
            NextCut = null;
        }

        public Cut(EndianBinaryReader reader, List<Substance> substances)
        {
            Properties = new List<Substance>();
            NextCut = null;

            Name = new string(reader.ReadChars(32)).Trim('\0');
            m_DuplicateID = reader.ReadInt32();

            reader.SkipInt32();

            m_CheckFlags[0] = reader.ReadInt32();
            m_CheckFlags[1] = reader.ReadInt32();
            m_CheckFlags[2] = reader.ReadInt32();

            Flag = reader.ReadInt32();

            m_FirstSubstanceIndex = reader.ReadInt32();
            m_NextCutIndex = reader.ReadInt32();

            reader.Skip(16);

            if (m_FirstSubstanceIndex != -1)
            {
                Substance subs = substances[m_FirstSubstanceIndex];

                while (subs != null)
                {
                    Properties.Add(subs);
                    subs = subs.NextSubstance;
                }
            }
        }

        public void AssignCutReferences(List<Cut> cut_list)
        {
            if (m_NextCutIndex != -1)
            {
                NextCut = cut_list[m_NextCutIndex];
            }

            for (int i = 0; i < 3; i++)
            {
                if (m_CheckFlags[i] != -1)
                {
                    BlockingCuts[i] = cut_list.Find(x => x.Flag == m_CheckFlags[i]);
                }
            }
        }

        public void AssignID(ref int id)
        {
            Flag = id++;
            id += Properties.Count;
        }

        public void PrepareCutData(List<Cut> cuts, List<Substance> substances)
        {
            m_NextCutIndex = NextCut != null ? cuts.IndexOf(NextCut) : -1;
            m_FirstSubstanceIndex = Properties.Count > 0 ? substances.Count : -1;

            for (int i = 0; i < Properties.Count; i++)
            {
                if (i + 1 < Properties.Count)
                    Properties[i].NextSubstance = Properties[i + 1];
                else
                    Properties[i].NextSubstance = null;
            }

            substances.AddRange(Properties);
        }

        public void AssignBlockingIDs(List<Cut> cuts)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BlockingCuts[i] != null)
                {
                    int index = cuts.IndexOf(BlockingCuts[i]);
                    m_CheckFlags[i] = index != -1 ? cuts[index].Flag : -1;
                }
                else
                {
                    m_CheckFlags[i] = -1;
                }
            }
        }

        public bool IsBlocked()
        {
            return m_BlockingCuts[0] != null || m_BlockingCuts[1] != null || m_BlockingCuts[2] != null;
        }

        public void Write(EndianBinaryWriter writer, ref int index)
        {
            writer.WriteFixedString(Name, 32);
            writer.Write(m_DuplicateID);
            writer.Write(index++);

            writer.Write(m_CheckFlags[0]);
            writer.Write(m_CheckFlags[1]);
            writer.Write(m_CheckFlags[2]);

            writer.Write(Flag);

            writer.Write(m_FirstSubstanceIndex);
            writer.Write(m_NextCutIndex);

            writer.Write(new byte[16]);
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
