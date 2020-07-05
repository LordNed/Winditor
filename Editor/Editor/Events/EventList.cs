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
                m_Cuts.Add(new Cut(reader, m_Substances));
            }

            foreach (Cut c in m_Cuts)
            {
                c.AssignCutReferences(m_Cuts);
            }
        }

        private void ReadStaffs(EndianBinaryReader reader, int staff_offset, int staff_count)
        {
            reader.BaseStream.Seek(staff_offset, SeekOrigin.Begin);

            for (int i = 0; i < staff_count; i++)
            {
                m_Staffs.Add(new Staff(reader, m_Cuts));
            }
        }

        private void ReadEvents(EndianBinaryReader reader, int event_offset, int event_count)
        {
            reader.BaseStream.Seek(event_offset, SeekOrigin.Begin);
            for (int i = 0; i < event_count; i++)
            {
                Events.Add(new Event(reader, m_Staffs));
            }
        }

        public void ExportToStream(EndianBinaryWriter writer)
        {
            // We're going to rebuild our global lists of data.
            m_Staffs.Clear();
            m_Cuts.Clear();
            m_Substances.Clear();

            int id = 0;

            foreach (Event ev in Events)
                ev.PrepareEventData(ref id, m_Staffs, m_Cuts);

            foreach (Staff st in m_Staffs)
                st.PrepareStaffData(m_Cuts);

            foreach (Cut c in m_Cuts)
                c.PrepareCutData(m_Cuts, m_Substances);

            Write(writer);
        }

        private void Write(EndianBinaryWriter writer)
        {
            // Write header
            writer.Write(new byte[64]);

            // Event data
            int index = 0;
            // Write event data offset + count
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
            writer.Write(64);
            writer.Write(Events.Count);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (Event ev in Events)
                ev.Write(writer, ref index);

            // Staff data
            index = 0;
            // Write staff data offset + count
            writer.BaseStream.Seek(8, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Write(m_Staffs.Count);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (Staff st in m_Staffs)
                st.Write(writer, ref index);

            // Cut data
            index = 0;
            // Write cut data offset + count
            writer.BaseStream.Seek(16, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Write(m_Cuts.Count);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            // Substance data
            index = 0;
            // Write cut data offset + count
            writer.BaseStream.Seek(24, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Write(m_Substances.Count);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (BaseSubstance sub in m_Substances)
                sub.Write(writer, ref index);
        }

        public override string Name
        { 
            get { return "event_list.dat"; }
        }
    }
}
