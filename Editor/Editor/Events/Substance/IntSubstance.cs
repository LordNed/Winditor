using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace WindEditor.Events
{
    public class IntSubstance : BaseSubstance
    {
        private int[] m_Data;

        public int[] IntegerData
        {
            get { return m_Data; }
            set
            {
                if (value != m_Data)
                {
                    m_Data = value;
                    OnPropertyChanged("IntegerData");
                }
            }
        }

        public IntSubstance(EndianBinaryReader reader) : base(reader)
        {
            m_Data = new int[m_ElementCount];
        }

        public override void ReadValue(SubstanceData data)
        {
            m_Data = data.GetIntData(m_ElementIndex, m_ElementCount);
        }

        public override void WriteValue(SubstanceData data)
        {
            m_ElementIndex = data.AddIntData(IntegerData);
            m_ElementCount = IntegerData.Length;
        }
    }
}
