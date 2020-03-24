using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class RoomProperties
    {
        [WProperty("Room Properties", "Enable Dark Mode", true, "", SourceScene.Room)]
        public bool EnableDarkMode
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
                return value_as_int == 0 ? false : true;
            }

            set
            {
                int value_as_int = Convert.ToInt32(value);
                m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
                OnPropertyChanged("EnableDarkMode");
            }
        }

        [WProperty("Room Properties", "Dark Mode", true, "", SourceScene.Room)]
        public int DarkMode
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00000078) >> 3);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters = (int)(m_Parameters & ~0x00000078 | (value_as_int << 3 & 0x00000078));
                OnPropertyChanged("DarkMode");
            }
        }

        [WProperty("Room Properties", "Draw Depth", true, "", SourceScene.Room)]
        public int DrawDepth
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00007F80) >> 7);
                return value_as_int;
            }

            set
            {
                int value_as_int = value;
                m_Parameters = (int)(m_Parameters & ~0x00007F80 | (value_as_int << 7 & 0x00007F80));
                OnPropertyChanged("DrawDepth");
            }
        }

        public enum WindStrengthEnum
        {
            _30_Percent = 0,
            _60_Percent = 1,
            _90_Percent = 2,
            No_Wind = 3,
        }

        [WProperty("Room Properties", "Wind Strength", true, "", SourceScene.Room)]
        public WindStrengthEnum WindStrength
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x000C0000) >> 18);
                return (WindStrengthEnum)value_as_int;
            }

            set
            {
                int value_as_int = (int)value;
                m_Parameters = (int)(m_Parameters & ~0x000C0000 | (value_as_int << 18 & 0x000C0000));
                OnPropertyChanged("WindStrength");
            }
        }

        [WProperty("Room Properties", "Unknown 1", true, "", SourceScene.Room)]
        public bool Unknown1
        {
            get
            {
                int value_as_int = (int)((m_Parameters & 0x00100000) >> 20);
                return value_as_int == 0 ? false : true;
            }

            set
            {
                int value_as_int = Convert.ToInt32(value);
                m_Parameters = (int)(m_Parameters & ~0x00100000 | (value_as_int << 20 & 0x00100000));
                OnPropertyChanged("Unknown1");
            }
        }

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
