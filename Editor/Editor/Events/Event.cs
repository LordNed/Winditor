using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WindEditor.Properties;
using GameFormatReader.Common;

namespace WindEditor.Events
{
    [HideCategories()]
    public class Event : INotifyPropertyChanged
    {
        private string m_Name;

        private int m_Unknown1;
        private int m_Priority;

        private bool m_PlayJingle;

        private int[] m_ActorIndices;

        private int m_StartFlag1;
        private int m_StartFlag2;

        private int m_EndFlag1;
        private int m_EndFlag2;
        private int m_EndFlag3;

        public List<Staff> Actors { get; private set; }

        [WProperty("Event", "Name", true, "Name of the event, typically referenced by the EVNT data in the Stage setup.")]
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

        [WProperty("Event", "Priority", true, "Importance of the event in the event system. Higher-priority events will take precedence over lower-priority ones.")]
        public int Priority
        {
            get { return m_Priority; }
            set
            {
                if (value != m_Priority)
                {
                    m_Priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }

        [WProperty("Event", "Unknown 1", true, "Purpose uncertain.")]
        public int Unknown1
        {
            get { return m_Unknown1; }
            set
            {
                if (value != m_Unknown1)
                {
                    m_Unknown1 = value;
                    OnPropertyChanged("Unknown1");
                }
            }
        }

        [WProperty("Event", "Play Jingle", true, "If checked, the \"Secret Found!\" jingle will play when the event ends.")]
        public bool PlayJingle
        {
            get { return m_PlayJingle; }
            set
            {
                if (value != m_PlayJingle)
                {
                    m_PlayJingle = value;
                    OnPropertyChanged("PlayJingle");
                }
            }
        }

        public Event()
        {
            Actors = new List<Staff>();
            m_ActorIndices = new int[20];

            Name = "new_event";
            Priority = 0;
            Unknown1 = 1;
            PlayJingle = false;
        }

        public Event(EndianBinaryReader reader)
        {
            Actors = new List<Staff>();
            m_ActorIndices = new int[20];

            Name = new string(reader.ReadChars(32)).Trim('\0');

            reader.SkipInt32();

            Unknown1 = reader.ReadInt32();
            Priority = reader.ReadInt32();

            for (int i = 0; i < 20; i++)
            {
                m_ActorIndices[i] = reader.ReadInt32();
            }

            reader.SkipInt32();

            m_StartFlag1 = reader.ReadInt32();
            m_StartFlag2 = reader.ReadInt32();

            m_EndFlag1 = reader.ReadInt32();
            m_EndFlag2 = reader.ReadInt32();
            m_EndFlag3 = reader.ReadInt32();

            PlayJingle = Convert.ToBoolean(reader.ReadByte());

            reader.Skip(27);
        }

        public void AssignStaff(List<Staff> staff_list)
        {
            for (int i = 0; i < 20; i++)
            {
                if (m_ActorIndices[i] == -1)
                {
                    break;
                }

                Actors.Add(staff_list[m_ActorIndices[i]]);
            }
        }

        public override string ToString()
        {
            return $"Name: \"{ Name }\", Staff Count: { Actors.Count }";
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
