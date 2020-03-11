using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class StageProperties
    {
        [WProperty("Stage Properties", "Particle Bank", true, "Which particle bank number to load for this stage, from the res/Particle folder.", SourceScene.Room)]
        public int ParticleBank
        {
            get
            {
                int value_as_int = (int)((m_Parameters2 & 0x07F8) >> 3);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters2 = (short)(m_Parameters2 & ~0x07F8 | (value_as_int << 3 & 0x07F8));
                OnPropertyChanged("ParticleBank");
            }
        }
    }
}
