using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class RoomProperties
    {
        [WProperty("Room Properties", "Particle Bank", true, "Which particle bank number to load for this room, from the res/Particle folder.", SourceScene.Room)]
        public int ParticleBank
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x1FE00000) >> 21);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters = (int)(m_Parameters & ~0x1FE00000 | (value_as_int << 21 & 0x1FE00000));
                OnPropertyChanged("ParticleBank");
            }
        }
    }
}
