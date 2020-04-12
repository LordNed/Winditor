using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GameFormatReader.Common;
using System.IO;

namespace WindEditor.Events
{
    public class WEventList : WDOMNode
    {
        private BindingList<Event> m_Events;
        private List<Staff> m_Staffs;
        private List<Cut> m_Cuts;
        private List<BaseSubstance> m_Substances;

        private SubstanceData m_SubstanceData;

        public BindingList<Event> Events
        {
            get { return m_Events; } 
            set
            {
                if (value != m_Events)
                {
                    m_Events = value;
                    OnPropertyChanged("Events");
                }
            }
        }

        /// <summary>
        /// Creates an event list from the given file.
        /// </summary>
        /// <param name="world">World containing the collision mesh</param>
        /// <param name="file_name">File to load the model from</param>
        public WEventList(WWorld world, string file_name) : base(world)
        {
            Events = new BindingList<Event>();
            m_Staffs = new List<Staff>();
            m_Cuts = new List<Cut>();
            m_Substances = new List<BaseSubstance>();

            using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(file_name), Endian.Big))
            {
                LoadEventList(reader);
            }
        }

        private void LoadEventList(EndianBinaryReader reader)
        {
            int event_offset = reader.ReadInt32();
            int event_count = reader.ReadInt32();

            int staff_offset = reader.ReadInt32();
            int staff_count = reader.ReadInt32();

            int cut_offset = reader.ReadInt32();
            int cut_count = reader.ReadInt32();

            int substance_offset = reader.ReadInt32();
            int substance_count = reader.ReadInt32();

            ReadSubstances(reader, substance_offset, substance_count);
            ReadCuts(reader, cut_offset, cut_count);
            ReadStaffs(reader, staff_offset, staff_count);
            ReadEvents(reader, event_offset, event_count);
        }

        private void ReadSubstances(EndianBinaryReader reader, int substance_offset, int substance_count)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            m_SubstanceData = new SubstanceData(reader);

            reader.BaseStream.Seek(substance_offset, SeekOrigin.Begin);
            for (int i = 0; i < substance_count; i++)
            {
                reader.BaseStream.Seek(36, SeekOrigin.Current);
                SubstanceType sub_type = (SubstanceType)reader.ReadInt32();
                reader.BaseStream.Seek(-40, SeekOrigin.Current);

                BaseSubstance new_substance = null;

                switch (sub_type)
                {
                    case SubstanceType.Float:
                        new_substance = new FloatSubstance(reader);
                        break;
                    case SubstanceType.Int:
                        new_substance = new IntSubstance(reader);
                        break;
                    case SubstanceType.String:
                        new_substance = new StringSubstance(reader);
                        break;
                    case SubstanceType.Vec3:
                        new_substance = new Vec3Substance(reader);
                        break;
                }

                if (new_substance == null)
                {
                    throw new Exception("EventList substance was unknown type!");
                }

                new_substance.ReadValue(m_SubstanceData);
                m_Substances.Add(new_substance);
            }

            foreach (BaseSubstance s in m_Substances)
            {
                s.AssignNextSubstance(m_Substances);
            }
        }

        private void ReadCuts(EndianBinaryReader reader, int cut_offset, int cut_count)
        {
            reader.BaseStream.Seek(cut_offset, SeekOrigin.Begin);
            for (int i = 0; i < cut_count; i++)
            {
                m_Cuts.Add(new Cut(reader));
            }

            foreach (Cut c in m_Cuts)
            {
                c.AssignNextCutAndSubstances(m_Cuts, m_Substances);
            }
        }

        private void ReadStaffs(EndianBinaryReader reader, int staff_offset, int staff_count)
        {
            reader.BaseStream.Seek(staff_offset, SeekOrigin.Begin);
            for (int i = 0; i < staff_count; i++)
            {
                m_Staffs.Add(new Staff(reader));
            }

            foreach (Staff s in m_Staffs)
            {
                s.AssignFirstCut(m_Cuts);
            }
        }

        private void ReadEvents(EndianBinaryReader reader, int event_offset, int event_count)
        {
            reader.BaseStream.Seek(event_offset, SeekOrigin.Begin);
            for (int i = 0; i < event_count; i++)
            {
                Events.Add(new Event(reader));
            }

            foreach (Event e in Events)
            {
                e.AssignStaff(m_Staffs);
            }
        }

        public override string Name
        { 
            get { return "event_list.dat"; }
        }
    }
}
