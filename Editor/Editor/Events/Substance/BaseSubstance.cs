using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WindEditor.Properties;
using GameFormatReader.Common;
using NodeNetwork.ViewModels;

namespace WindEditor.Events
{
    public enum SubstanceType
    {
        Float,
        Vec3,
        Unknown,
        Int,
        String
    }

    [HideCategories()]
    public abstract class BaseSubstance : INotifyPropertyChanged
    {
        private string m_Name;

        private SubstanceType m_SubstanceType;

        protected int m_ElementIndex;
        protected int m_ElementCount;

        protected int m_NextSubstanceIndex;

        protected BaseSubstance m_NextSubstance;

        public SubstanceType SubstanceType { get; }

        public BaseSubstance NextSubstance
        {
            get { return m_NextSubstance; }
            set
            {
                if (value != m_NextSubstance)
                {
                    m_NextSubstance = value;
                    OnPropertyChanged("NextSubstance");
                }
            }
        }

        [WProperty("Substance", "Name", true, "Name of the substance.")]
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

        public BaseSubstance(EndianBinaryReader reader)
        {
            NextSubstance = null;

            Name = new string(reader.ReadChars(32)).Trim('\0');

            reader.SkipInt32();

            SubstanceType = (SubstanceType)reader.ReadInt32();

            m_ElementIndex = reader.ReadInt32();
            m_ElementCount = reader.ReadInt32();
            m_NextSubstanceIndex = reader.ReadInt32();

            reader.Skip(12);
        }

        public abstract void ReadValue(SubstanceData data);

        public abstract void WriteValue(SubstanceData data);

        public abstract void AddSubstanceEditor(NodeViewModel input_view_model);

        public void AssignNextSubstance(List<BaseSubstance> substance_list)
        {
            if (m_NextSubstanceIndex != -1)
            {
                NextSubstance = substance_list[m_NextSubstanceIndex];
            }
        }

        public override string ToString()
        {
            return $"Name: \"{ Name }\", Type: { SubstanceType })";
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
