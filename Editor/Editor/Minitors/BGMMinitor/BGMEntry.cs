using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GameFormatReader.Common;

namespace WindEditor.Minitors.BGM
{
    public enum BGMType
    {
        Sequence,
        Stream
    }

    public class BGMEntry : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        static ushort ID_TYPE_MASK = 0x8000;

        private string m_Name;

        private BGMType m_Type;
        private ushort m_ID;

        private byte m_WaveBank1;
        private byte m_WaveBank2;
        private byte m_WaveBank3;
        private byte m_WaveBank4;

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

        public BGMType Type
        {
            get { return m_Type; }
            set
            {
                if (value != m_Type)
                {
                    m_Type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public ushort ID
        {
            get { return m_ID; }
            set
            {
                if (value != m_ID)
                {
                    m_ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        public byte WaveBankIndex1 { get; set; }
        public byte WaveBankIndex2 { get; set; }

        public byte WaveBank1
        {
            get { return m_WaveBank1; }
            set
            {
                if (value != m_WaveBank1)
                {
                    m_WaveBank1 = value;
                    OnPropertyChanged("WaveBank1");
                }
            }
        }

        public byte WaveBank2
        {
            get { return m_WaveBank2; }
            set
            {
                if (value != m_WaveBank2)
                {
                    m_WaveBank2 = value;
                    OnPropertyChanged("WaveBank2");
                }
            }
        }

        public byte WaveBank3
        {
            get { return m_WaveBank3; }
            set
            {
                if (value != m_WaveBank3)
                {
                    m_WaveBank3 = value;
                    OnPropertyChanged("WaveBank3");
                }
            }
        }

        public byte WaveBank4
        {
            get { return m_WaveBank4; }
            set
            {
                if (value != m_WaveBank4)
                {
                    m_WaveBank4 = value;
                    OnPropertyChanged("WaveBank4");
                }
            }
        }

        public BGMEntry(EndianBinaryReader reader)
        {
            ushort bgmID = reader.ReadUInt16();

            if ((bgmID & ID_TYPE_MASK) == 0)
                m_Type = BGMType.Sequence;
            else
                m_Type = BGMType.Stream;

            m_ID = (ushort)(bgmID & ~ID_TYPE_MASK);

            WaveBankIndex1 = reader.ReadByte();
            WaveBankIndex2 = reader.ReadByte();
        }

        public void Write(EndianBinaryWriter writer)
        {
            ushort bgmID = m_ID;
            if (m_Type == BGMType.Stream)
                bgmID |= ID_TYPE_MASK;

            writer.Write(bgmID);
            writer.Write(WaveBankIndex1);
            writer.Write(WaveBankIndex2);
        }

        public override string ToString()
        {
            return $"{ Name }, { Type.ToString() }, { ID }";
        }
    }
}
