using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WindEditor.Properties;
using GameFormatReader.Common;

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
