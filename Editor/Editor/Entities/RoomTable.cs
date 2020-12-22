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
    public class RoomTableRoomSettings
    {
        private byte m_Bitfield;

        [WProperty("Room Settings", "Room Number", true, "The room number of the room this entry represents.")]
        public byte RoomNumber
        {
            get { return (byte)(m_Bitfield & 0x3F); }
            set
            {
                m_Bitfield = (byte)((m_Bitfield & 0xC0) | value & 0x3F);
            }
        }

        [WProperty("Room Settings", "Activate Room when Loaded", true, "If this is checked, the room this entry represents will be visible to the player in addition to the current room, and accessible\nby just walking into it, without using a dungeon door. Otherwise, the room will be hidden until the player transitions into it via a dungeon door.")]
        public bool Unk1
        {
            get { return (m_Bitfield & 0x80) != 0; }
            set
            {
                if (value)
                    m_Bitfield |= 0x80;
                else
                    m_Bitfield &= 0x7F;
            }
        }

        [WProperty("Room Settings", "Unk2", true, "")]
        public bool Unk2
        {
            get { return (m_Bitfield & 0x40) != 0; }
            set
            {
                if (value)
                    m_Bitfield |= 0x40;
                else
                    m_Bitfield &= 0xBF;
            }
        }

        public RoomTableRoomSettings()
        {

        }

        public RoomTableRoomSettings(EndianBinaryReader reader)
        {
            m_Bitfield = reader.ReadByte();
        }

        public void Save(EndianBinaryWriter writer)
        {
            writer.Write(m_Bitfield);
        }

        public override string ToString()
        {
            return $"Settings for Room { RoomNumber }";
        }
    }

    [HideCategories(new string[] { "Transform" })]
    public class RoomTableEntryNode : SerializableDOMNode
    {
        private AdvancedBindingList<RoomTableRoomSettings> m_LoadedRoomEntries;

        [WProperty("Room Data", "Entry Select", true)]
        public AdvancedBindingList<RoomTableRoomSettings> LoadedRoomEntries
        {
            get { return m_LoadedRoomEntries; }
            set
            {
                if (value != m_LoadedRoomEntries)
                {
                    m_LoadedRoomEntries = value;
                    OnPropertyChanged("LoadedRoomEntries");
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

        public int Index;

        public RoomTableEntryNode(FourCC fourCC, WWorld world, EndianBinaryReader reader) : base(fourCC, world)
        {
            LoadedRoomEntries = new AdvancedBindingList<RoomTableRoomSettings>();

            byte RoomCount = reader.ReadByte();

            ReverbAmount = reader.ReadByte();
            TimePasses = Convert.ToBoolean(reader.ReadByte());
            
            reader.SkipByte(); // Padding byte

            int table_offset = reader.ReadInt32();
            reader.BaseStream.Seek(table_offset, System.IO.SeekOrigin.Begin);

            for (int i = 0; i < RoomCount; i++)
            {
                LoadedRoomEntries.AddNew(new object[] { reader });
            }
        }

        public override void Save(EndianBinaryWriter stream)
        {
            stream.Write((byte)LoadedRoomEntries.Count);
            stream.Write((byte)m_ReverbAmount);
            stream.Write(Convert.ToByte(m_TimePasses));
            
            stream.Write((byte)0); // Padding byte
            
            stream.Write((int)0);
        }

        public void WriteLoadedRoomTable(EndianBinaryWriter writer)
        {
            writer.Seek(4, System.IO.SeekOrigin.Current);
            writer.Write((int)writer.BaseStream.Length);

            int next_entry_position = (int)writer.BaseStream.Position;

            writer.Seek(0, System.IO.SeekOrigin.End);

            foreach (RoomTableRoomSettings b in LoadedRoomEntries)
            {
                b.Save(writer);
            }

            writer.Seek(next_entry_position, System.IO.SeekOrigin.Begin);
        }

        public override string ToString()
        {
            return $"Room Table { Index }";
        }
    }
}
