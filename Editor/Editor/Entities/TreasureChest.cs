using JStudio.J3D;
using OpenTK;
using System;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class TreasureChest
	{
		public override string ToString()
		{
			return Name;
		}

        private ChestType m_Type;

        [WProperty("Treasure Chest", "Type", true)]
        public ChestType Type
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

        private byte m_SpawnSwitchId;

        [WProperty("Treasure Chest", "Spawn Switch ID", true)]
        public byte SpawnSwitchId
        {
            get { return m_SpawnSwitchId; }
            set
            {
                if (value != m_SpawnSwitchId)
                {
                    m_SpawnSwitchId = value;
                    OnPropertyChanged("SpawnSwitchId");
                }
            }
        }

        private byte m_OpenFlagId;

        [WProperty("Treasure Chest", "Open Flag ID", false)]
        public byte OpenFlagId
        {
            get { return m_OpenFlagId; }
            set
            {
                if (value != m_OpenFlagId)
                {
                    m_OpenFlagId = value;
                    OnPropertyChanged("OpenFlagId");
                }
            }
        }

        private ChestBehavior m_Behavior;

        [WProperty("Treasure Chest", "Behavior", true)]
        public ChestBehavior Behavior
        {
            get { return m_Behavior; }
            set
            {
                if (value != m_Behavior)
                {
                    m_Behavior = value;
                    OnPropertyChanged("Behavior");
                }
            }
        }

        private byte m_RoomNumber;

        [WProperty("Treasure Chest", "Room Number", true)]
        public byte RoomNumber
        {
            get { return m_RoomNumber; }
            set
            {
                if (value != m_RoomNumber)
                {
                    m_RoomNumber = value;
                    OnPropertyChanged("RoomNumber");
                }
            }
        }

        private byte m_OpenSwitchId;

        [WProperty("Treasure Chest", "Open Switch ID", true)]
        public byte OpenSwitchId
        {
            get { return m_OpenSwitchId; }
            set
            {
                if (value != m_OpenSwitchId)
                {
                    m_OpenSwitchId = value;
                    OnPropertyChanged("OpenSwitchId");
                }
            }
        }

        private ItemID m_Item;

        [WProperty("Treasure Chest", "Item", true)]
        public ItemID Item
        {
            get { return m_Item; }
            set
            {
                if (value != m_Item)
                {
                    m_Item = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        public override void PostLoad()
        {
            base.PostLoad();

            Type = (ChestType)((Parameters & 0xF00000) >> 20);
            SpawnSwitchId = (byte)((Parameters & 0xFF000) >> 12);
            OpenFlagId = (byte)((Parameters & 0xF80) >> 7);
            Behavior = (ChestBehavior)((Parameters & 0x7F));

            RoomNumber = (byte)(AuxParameters1 & 0x3F);

            OpenSwitchId = (byte)(AuxParameters2 & 0xFF);
            Item = (ItemID)((AuxParameters2 & 0xFF00) >> 8);
        }
    }
}
