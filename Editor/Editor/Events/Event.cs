using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WindEditor.Properties;
using GameFormatReader.Common;
using System.Collections.ObjectModel;

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

        private int[] m_EndFlags;

        private ObservableCollection<Staff> m_Actors;
        private Cut[] m_EndCuts;

        public ObservableCollection<Staff> Actors
        { 
            get { return m_Actors; }
            set
            {
                if (value != m_Actors)
                {
                    m_Actors = value;
                    OnPropertyChanged("Actors");
                }
            }
        }

        public Cut[] EndCuts
        {
            get { return m_EndCuts; }
            set
            {
                if (m_EndCuts != value)
                {
                    m_EndCuts = value;
                    OnPropertyChanged("EndCuts");
                }
            }
        }

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
            Actors = new ObservableCollection<Staff>();
            m_ActorIndices = new int[20];
            m_EndFlags = new int[3];
            EndCuts = new Cut[3];

            Name = "new_event";
            Priority = 0;
            Unknown1 = 1;
            PlayJingle = false;
        }

        public Event(EndianBinaryReader reader, List<Staff> staffs, List<Cut> cuts)
        {
            Actors = new ObservableCollection<Staff>();
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

            m_EndFlags = new int[3];

            m_EndFlags[0] = reader.ReadInt32();
            m_EndFlags[1] = reader.ReadInt32();
            m_EndFlags[2] = reader.ReadInt32();

            PlayJingle = Convert.ToBoolean(reader.ReadByte());

            reader.Skip(27);

            for (int i = 0; i < 20; i++)
            {
                if (m_ActorIndices[i] == -1)
                {
                    break;
                }

                staffs[m_ActorIndices[i]].ParentEvent = this;
                Actors.Add(staffs[m_ActorIndices[i]]);
            }

            EndCuts = new Cut[3];

            for (int i = 0; i < 3; i++)
            {
                if (m_EndFlags[i] != -1)
                {
                    EndCuts[i] = cuts.Find(x => x.Flag == m_EndFlags[i]);
                }
            }
        }

        public void PrepareEventData(ref int id, List<Staff> global_staff, List<Cut> global_cuts)
        {
            List<Cut> event_cuts = new List<Cut>();

            m_StartFlag1 = id++;
            m_StartFlag2 = id++;

            // We have to explicitly set unused actor slots to -1, so this is my solution.
            for (int i = 0; i < 20; i++)
            {
                if (i < Actors.Count)
                {
                    Actors[i].AssignIDs(ref id, event_cuts);

                    m_ActorIndices[i] = global_staff.Count;
                    global_staff.Add(Actors[i]);
                }
                else
                {
                    m_ActorIndices[i] = -1;
                }
            }

            foreach (Cut c in event_cuts)
            {
                c.AssignBlockingIDs(event_cuts);
            }

            for (int i = 0; i < 3; i++)
            {
                if (EndCuts[i] != null && event_cuts.Contains(EndCuts[i]))
                {
                    m_EndFlags[i] = EndCuts[i].Flag;
                }
                else
                {
                    m_EndFlags[i] = -1;
                }
            }

            global_cuts.AddRange(event_cuts);
        }

        public void Write(EndianBinaryWriter writer, ref int index)
        {
            writer.WriteFixedString(Name, 32);
            writer.Write(index++);
            writer.Write(m_Unknown1);
            writer.Write(Priority);

            foreach (int i in m_ActorIndices)
            {
                writer.Write(i);
            }

            writer.Write(Actors.Count);

            writer.Write(m_StartFlag1);
            writer.Write(m_StartFlag2);

            writer.Write(m_EndFlags[0]);
            writer.Write(m_EndFlags[1]);
            writer.Write(m_EndFlags[2]);

            writer.Write(Convert.ToByte(m_PlayJingle));

            writer.Write(new byte[27]);
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
