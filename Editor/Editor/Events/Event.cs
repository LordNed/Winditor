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
        private Cut m_EndCut1;
        private Cut m_EndCut2;
        private Cut m_EndCut3;

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

        [WProperty("Event", "Ending Cut 1", true, "This event will end only once all three Ending Cuts have been executed. Leave unused slots blank.")]
        public Cut EndCut1
        {
            get { return m_EndCut1; }
            set
            {
                if (m_EndCut1 != value)
                {
                    m_EndCut1 = value;
                    OnPropertyChanged("EndCut1");
                }
            }
        }

        [WProperty("Event", "Ending Cut 2", true, "This event will end only once all three Ending Cuts have been executed. Leave unused slots blank.")]
        public Cut EndCut2
        {
            get { return m_EndCut2; }
            set
            {
                if (m_EndCut2 != value)
                {
                    m_EndCut2 = value;
                    OnPropertyChanged("EndCut2");
                }
            }
        }

        [WProperty("Event", "Ending Cut 3", true, "This event will end only once all three Ending Cuts have been executed. Leave unused slots blank.")]
        public Cut EndCut3
        {
            get { return m_EndCut3; }
            set
            {
                if (m_EndCut3 != value)
                {
                    m_EndCut3 = value;
                    OnPropertyChanged("EndCut3");
                }
            }
        }

        public Event()
        {
            Actors = new ObservableCollection<Staff>();
            m_ActorIndices = new int[20];
            m_EndFlags = new int[3];

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

            if (m_EndFlags[0] != -1)
            {
                EndCut1 = cuts.Find(x => x.Flag == m_EndFlags[0]);
            }

            if (m_EndFlags[1] != -1)
            {
                EndCut2 = cuts.Find(x => x.Flag == m_EndFlags[1]);
            }

            if (m_EndFlags[2] != -1)
            {
                EndCut3 = cuts.Find(x => x.Flag == m_EndFlags[2]);
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

            if (EndCut1 != null && event_cuts.Contains(EndCut1))
            {
                m_EndFlags[0] = EndCut1.Flag;
            }
            else
            {
                m_EndFlags[0] = -1;
            }

            if (EndCut2 != null && event_cuts.Contains(EndCut2))
            {
                m_EndFlags[1] = EndCut2.Flag;
            }
            else
            {
                m_EndFlags[1] = -1;
            }

            if (EndCut3 != null && event_cuts.Contains(EndCut3))
            {
                m_EndFlags[2] = EndCut3.Flag;
            }
            else
            {
                m_EndFlags[2] = -1;
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
