using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace WindEditor.Events
{
    public class FloatSubstance : BaseSubstance
    {
        private float[] m_Data;

        public float[] FloatData
        {
            get { return m_Data; }
            set
            {
                if (value != m_Data)
                {
                    m_Data = value;
                    OnPropertyChanged("FloatData");
                }
            }
        }

        public FloatSubstance(EndianBinaryReader reader) : base(reader)
        {
            m_Data = new float[m_ElementCount];
        }

        public override void ReadValue(SubstanceData data)
        {
            m_Data = data.GetFloatData(m_ElementIndex, m_ElementCount);
        }

        public override void WriteValue(SubstanceData data)
        {
            m_ElementIndex = data.AddFloatData(FloatData);
            m_ElementCount = FloatData.Length;
        }
    }
}
