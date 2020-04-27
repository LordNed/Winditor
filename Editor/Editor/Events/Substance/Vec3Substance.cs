using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using OpenTK;

namespace WindEditor.Events
{
    public class Vec3Substance : BaseSubstance
    {
        private Vector3[] m_Data;

        public Vector3[] Vec3Data
        {
            get { return m_Data; }
            set
            {
                if (value != m_Data)
                {
                    m_Data = value;
                    OnPropertyChanged("Vec3Data");
                }
            }
        }

        public Vec3Substance(EndianBinaryReader reader) : base(reader)
        {
            m_Data = new Vector3[m_ElementCount];
        }

        public override void ReadValue(SubstanceData data)
        {
            m_Data = data.GetVec3Data(m_ElementIndex, m_ElementCount);
        }

        public override void WriteValue(SubstanceData data)
        {
            m_ElementIndex = data.AddVec3Data(Vec3Data);
            m_ElementCount = Vec3Data.Length;
        }

        public override void AddSubstanceEditor(NodeViewModel view_model)
        {
            ValueNodeOutputViewModel<int?> int_output = new ValueNodeOutputViewModel<int?>();
            view_model.Outputs.Edit(x => x.Add(int_output));
        }
    }
}
