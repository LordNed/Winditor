using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;

namespace WindEditor.Events
{
    public class StringSubstance : BaseSubstance
    {
        private string m_Data;

        public string StringData
        {
            get { return m_Data; }
            set
            {
                if (value != m_Data)
                {
                    m_Data = value;
                    OnPropertyChanged("StringData");
                }
            }
        }

        public StringSubstance(EndianBinaryReader reader) : base(reader)
        {
            m_Data = "";
        }

        public override void ReadValue(SubstanceData data)
        {
            m_Data = data.GetStringData(m_ElementIndex, m_ElementCount);
        }

        public override void WriteValue(SubstanceData data)
        {
            m_ElementIndex = data.AddStringData(StringData);
            m_ElementCount = StringData.Length;
        }

        public override void AddSubstanceEditor(NodeViewModel view_model)
        {
            ValueNodeOutputViewModel<int?> int_output = new ValueNodeOutputViewModel<int?>();
            view_model.Outputs.Edit(x => x.Add(int_output));
        }
    }
}
