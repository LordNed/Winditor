using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WindEditor.ViewModel;
using GameFormatReader.Common;

namespace WindEditor
{
    [HideCategories(new string[] { "Transform" })]
    public class RoomTableEntryNode : SerializableDOMNode
    {
        private List<byte> m_LoadedRoomIndices;

        private byte m_LoadedRoomCount;

        public byte LoadedRoomCount
        {
            get { return m_LoadedRoomCount; }
            set
            {
                if (value != m_LoadedRoomCount)
                {
                    m_LoadedRoomCount = value;
                    OnPropertyChanged("LoadedRoomCount");
                }
            }
        }

        private byte m_ReverbAmount;

        [WProperty("Room Table Entry", "Reverb Amount", true, "The amount of echo in this room.")]
        public byte ReverbAmount
        {
            get { return m_ReverbAmount; }
            set
            {
                if (value != m_ReverbAmount)
                {
                    m_ReverbAmount = value;
                    OnPropertyChanged("ReverbAmount");
                }
            }
        }

        private bool m_TimePasses;

        [WProperty("Room Table Entry", "Time passes?", true, "Determines whether time passes in this room.")]
        public bool TimePasses
        {
            get { return m_TimePasses; }
            set
            {
                if (value != m_TimePasses)
                {
                    m_TimePasses = value;
                    OnPropertyChanged("TimePasses");
                }
            }
        }

        private byte m_Unknown1;

        [WProperty("Unknowns", "Unknown 1", true)]
        public byte Unknown1
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

        public RoomTableEntryNode(FourCC fourCC, WWorld world, EndianBinaryReader reader) : base(fourCC, world)
        {
            m_LoadedRoomIndices = new List<byte>();

            LoadedRoomCount = reader.ReadByte();
            ReverbAmount = reader.ReadByte();
            TimePasses = Convert.ToBoolean(reader.ReadByte());
            Unknown1 = reader.ReadByte();

            int table_offset = reader.ReadInt32();
            reader.BaseStream.Seek(table_offset, System.IO.SeekOrigin.Begin);

            for (int i = 0; i < LoadedRoomCount; i++)
            {
                m_LoadedRoomIndices.Add(reader.ReadByte());
            }
        }

        public override void Save(EndianBinaryWriter stream)
        {
            stream.Write((byte)m_LoadedRoomIndices.Count);
            stream.Write((byte)m_ReverbAmount);
            stream.Write(Convert.ToByte(m_TimePasses));
            stream.Write((byte)m_Unknown1);
            stream.Write((int)0);
        }

        public void WriteLoadedRoomTable(EndianBinaryWriter writer)
        {
            writer.Seek(4, System.IO.SeekOrigin.Current);
            writer.Write((int)writer.BaseStream.Length);

            int next_entry_position = (int)writer.BaseStream.Position;

            writer.Seek(0, System.IO.SeekOrigin.End);

            foreach (byte b in m_LoadedRoomIndices)
            {
                writer.Write(b);
            }

            writer.Seek(next_entry_position, System.IO.SeekOrigin.Begin);
        }
    }
}
