using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using System.ComponentModel;

namespace WindEditor.Collision
{
    [HideCategories()]
    public class CollisionProperty : IEquatable<CollisionProperty>, INotifyPropertyChanged
    {
        private int m_Bitfield1;
        private int m_Bitfield2;
        private int m_Bitfield3;
        private int m_CameraBehavior;

        public CollisionProperty(EndianBinaryReader reader)
        {
            m_Bitfield1 = reader.ReadInt32();
            m_Bitfield2 = reader.ReadInt32();
            m_Bitfield3 = reader.ReadInt32();
            m_CameraBehavior = reader.ReadInt32();
        }

        public CollisionProperty(int b1, int b2, int b3, int cb)
        {
            m_Bitfield1 = b1;
            m_Bitfield2 = b2;
            m_Bitfield3 = b3;
            m_CameraBehavior = cb;
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(m_Bitfield1);
            writer.Write(m_Bitfield2);
            writer.Write(m_Bitfield3);
            writer.Write(m_CameraBehavior);
        }

        private int Clamp(int value, int max, int min = 0)
        {
            if (value < 0)
                return min;
            if (value > max)
                return max;

            return value;
        }

        public CollisionProperty Clone()
        {
            return new CollisionProperty(m_Bitfield1, m_Bitfield2, m_Bitfield3, m_CameraBehavior);
        }

        public bool Equals(CollisionProperty other)
        {
            if (m_Bitfield1 == other.m_Bitfield1
                && m_Bitfield2 == other.m_Bitfield2
                && m_Bitfield3 == other.m_Bitfield3
                && m_CameraBehavior == other.m_CameraBehavior)
            {
                return true;
            }

            return false;
        }

        #region Bitfield 1

        [WProperty("Surface", "Sound ID", true, "What sound is played when something walks across this collision.")]
        public SoundID SoundID
        {
            get { return (SoundID)((m_Bitfield1 & 0x1F00) >> 8); }
            set
            {
                int stored_value = (m_Bitfield1 & 0x1F00) >> 8;

                if ((int)value != stored_value)
                {
                    m_Bitfield1 &= ~(0x1F00);
                    m_Bitfield1 |= (Clamp((int)value, 0x1F) << 8);

                    OnPropertyChanged("SoundID");
                }
            }
        }

        [WProperty("Camera", "Camera ID", true, "Purpose uncertain. Related to cameras, probably.")]
        public int CamID
        {
            get { return m_Bitfield1 & 0xFF; }
            set
            {
                int stored_value = m_Bitfield1 & 0xFF;

                if (value != stored_value)
                {
                    m_Bitfield1 &= ~(0xFF);
                    m_Bitfield1 |= Clamp(value, 0xFF);

                    OnPropertyChanged("CamID");
                }
            }
        }

        [WProperty("Surface", "Exit Index", true, "When set below 63, causes a map transition using the SCLS entry at the specified index.")]
        public int ExitID
        {
            get { return (m_Bitfield1 & 0x7E000) >> 13; }
            set
            {
                int stored_value = (m_Bitfield1 & 0x7E000) >> 13;

                if (value != stored_value)
                {
                    m_Bitfield1 &= ~(0x7E000);
                    m_Bitfield1 |= (Clamp(value, 0x3F) << 13);

                    OnPropertyChanged("ExitID");
                }
            }
        }

        [WProperty("Misc.", "PolyColor", true, "Purpose uncertain. Possibly related to environment lighting?")]
        public int PolyColor
        {
            get { return (m_Bitfield1 & 0x7F80000) >> 19; }
            set
            {
                int stored_value = (m_Bitfield1 & 0x7F80000) >> 19;

                if (value != stored_value)
                {
                    m_Bitfield1 &= ~(0x7F80000);
                    m_Bitfield1 |= (Clamp(value, 0xFF) << 19);

                    OnPropertyChanged("PolyColor");
                }
            }
        }

        #endregion

        #region Bitfield 2

        [WProperty("Misc.", "Link Number", true, "Purpose uncertain. Possibly switches the spawn point where Link respawns?")]
        public int LinkNo
        {
            get { return m_Bitfield2 & 0xFF; }
            set
            {
                int stored_value = m_Bitfield2 & 0xFF;

                if (value != stored_value)
                {
                    m_Bitfield2 &= ~(0xFF);
                    m_Bitfield2 |= Clamp(value, 0xFF);

                    OnPropertyChanged("LinkNo");
                }
            }
        }

        [WProperty("Surface", "Wall Type", true, "Determines how a wall acts, e.g. if it's climbable or can be grabbed like a block.")]
        public WallCode WallCode
        {
            get { return (WallCode)((m_Bitfield2 & 0xF00) >> 8); }
            set
            {
                int stored_value = (m_Bitfield2 & 0xF00) >> 8;

                if ((int)value != stored_value)
                {
                    m_Bitfield2 &= ~(0xF00);
                    m_Bitfield2 |= (Clamp((int)value, 0xF) << 8);

                    OnPropertyChanged("WallCode");
                }
            }
        }

        [WProperty("Surface", "Special Type", true, "Purpose uncertain. Can prevent sidling and force Link to slide down.")]
        public SpecialCode SpecialCode
        {
            get { return (SpecialCode)((m_Bitfield2 & 0xF000) >> 12); }
            set
            {
                int stored_value = (m_Bitfield2 & 0xF000) >> 12;

                if ((int)value != stored_value)
                {
                    m_Bitfield2 &= ~(0xF000);
                    m_Bitfield2 |= (Clamp((int)value, 0xF) << 12);

                    OnPropertyChanged("SpecialCode");
                }
            }
        }

        [WProperty("Surface", "Attribute Type", true, "What the collision acts like on contact.")]
        public AttributeCode AttributeCode
        {
            get { return (AttributeCode)((m_Bitfield2 & 0x1F0000) >> 16); }
            set
            {
                int stored_value = (m_Bitfield2 & 0x1F0000) >> 16;

                if ((int)value != stored_value)
                {
                    m_Bitfield2 &= ~(0x1F0000);
                    m_Bitfield2 |= (Clamp((int)value, 0x1F) << 16);

                    OnPropertyChanged("AttributeCode");
                }
            }
        }

        [WProperty("Surface", "Ground Type", true, "Purpose uncertain. Can force Link to respawn and walk as if on a slope.")]
        public GroundCode GroundCode
        {
            get { return (GroundCode)((m_Bitfield2 & 0x3E00000) >> 21); }
            set
            {
                int stored_value = (m_Bitfield2 & 0x3E00000) >> 21;

                if ((int)value != stored_value)
                {
                    m_Bitfield2 &= ~(0x3E00000);
                    m_Bitfield2 |= (Clamp((int)value, 0x1F) << 21);

                    OnPropertyChanged("GroundCode");
                }
            }
        }

        #endregion

        #region Bitfield 3

        [WProperty("Camera", "CamMoveBG", true, "Purpose uncertain.")]
        public int CamMoveBG
        {
            get { return m_Bitfield3 & 0xFF; }
            set
            {
                int stored_value = m_Bitfield3 & 0xFF;

                if (value != stored_value)
                {
                    m_Bitfield3 &= ~(0xFF);
                    m_Bitfield3 |= Clamp(value, 0xFF);

                    OnPropertyChanged("CamMoveBG");
                }
            }
        }

        [WProperty("Camera", "RoomCamID", true, "Purpose uncertain.")]
        public int RoomCamID
        {
            get { return (m_Bitfield3 & 0xFF00) >> 8; }
            set
            {
                int stored_value = (m_Bitfield3 & 0xFF00) >> 8;

                if (value != stored_value)
                {
                    m_Bitfield3 &= ~(0xFF00);
                    m_Bitfield3 |= (Clamp(value, 0xFF) << 8);

                    OnPropertyChanged("RoomCamID");
                }
            }
        }

        [WProperty("Camera", "RoomPathID", true, "Purpose uncertain.")]
        public int RoomPathID
        {
            get { return (m_Bitfield3 & 0xFF0000) >> 16; }
            set
            {
                int stored_value = (m_Bitfield3 & 0xFF0000) >> 16;

                if (value != stored_value)
                {
                    m_Bitfield3 &= ~(0xFF0000);
                    m_Bitfield3 |= (Clamp(value, 0xFF) << 16);

                    OnPropertyChanged("RoomPathID");
                }
            }
        }

        [WProperty("Camera", "RoomPathPntNo", true, "Purpose uncertain.")]
        public int RoomPathPntNo
        {
            get { return (int)(m_Bitfield3 & 0xFF000000) >> 24; }
            set
            {
                int stored_value = (int)(m_Bitfield3 & 0xFF000000) >> 24;

                if (value != stored_value)
                {
                    m_Bitfield3 &= (int)~(0xFF000000);
                    m_Bitfield3 |= (Clamp(value, 0xFF, -1) << 24);

                    OnPropertyChanged("RoomPathPntNo");
                }
            }
        }

        #endregion

        [WProperty("Camera", "Camera Behavior", true, "Purpose uncertain.")]
        public int CameraBehavior
        {
            get { return m_CameraBehavior; }
            set
            {
                if (value != m_CameraBehavior)
                {
                    m_CameraBehavior = value;
                    OnPropertyChanged("CameraBehavior");
                }
            }
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
