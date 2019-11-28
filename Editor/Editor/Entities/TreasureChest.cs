using JStudio.J3D;
using OpenTK;
using System;
using WindEditor.ViewModel;

namespace WindEditor.a
{
	public partial class TreasureChest : ILayerable
	{
		public override string ToString()
		{
			return Name;
		}

        private ChestType m_Type;

        [WProperty("Treasure Chest", "Type", true, "The model used to display the chest.")]
        public ChestType Type
        {
            get { return m_Type; }
            set
            {
                if (value != m_Type)
                {
                    m_Type = value;
                    OnPropertyChanged("Type");
                    UpdateModel();
                }
            }
        }

        private byte m_SpawnSwitchId;

        [WProperty("Flags", "Spawn Switch ID", true, "The switch ID that causes the chest to spawn when set.")]
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

        [WProperty("Flags", "Open Flag ID", false)]
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

        [WProperty("Treasure Chest", "Behavior", true, "The way the chest acts when created.")]
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

        [WProperty("Flags", "Open Switch ID", true)]
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

        [WProperty("Treasure Chest", "Item", true, "The item given to the player when the chest is opened.")]
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

        public MapLayer Layer { get; set; }

        public override void PostLoad()
        {
            Type = (ChestType)((m_Parameters & 0xF00000) >> 20);
            SpawnSwitchId = (byte)((m_Parameters & 0xFF000) >> 12);
            OpenFlagId = (byte)((m_Parameters & 0xF80) >> 7);
            Behavior = (ChestBehavior)((m_Parameters & 0x7F));

            RoomNumber = (byte)(m_AuxParameters1 & 0x3F);

            OpenSwitchId = (byte)(m_AuxParameters2 & 0xFF);
            Item = (ItemID)((m_AuxParameters2 & 0xFF00) >> 8);

            // Some unused rooms have chest types that don't exist any more.
            // We'll force these to be the default.
            if ((int)Type > 4)
            {
                Type = ChestType.Light_wood;
            }

            UpdateModel();
        }

        public override void PreSave()
        {
            m_Parameters = 0;
            m_AuxParameters1 = 0;
            m_AuxParameters2 = 0;

            m_Parameters |= 0xFF << 24;
            m_Parameters |= ((int)Type & 0xF) << 20;
            m_Parameters |= (SpawnSwitchId & 0xFF) << 12;
            m_Parameters |= (OpenFlagId & 0x1F) << 7;
            m_Parameters |= ((int)Behavior & 0x7F);

            m_AuxParameters1 |= (short)(RoomNumber & 0x3F);

            m_AuxParameters2 |= (short)(OpenSwitchId & 0xFF);
            m_AuxParameters2 |= (short)(((int)Item & 0xFF) << 8);
        }

        private void UpdateModel()
        {
            switch(Type)
            {
                case ChestType.Big_Key:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Key Chest");
                    break;
                case ChestType.Metal:
                    m_actorMeshes = WResourceManager.LoadActorResource("Metal Chest");
                    break;
                case ChestType.Dark_wood:
                    m_actorMeshes = WResourceManager.LoadActorResource("Dark Wood Chest");
                    break;
                case ChestType.Light_wood:
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Light Wood Chest");
                    break;
            }
        }

        public MapLayer FourCCToLayer()
        {
            string fourcc_str = FourCC.ToString().ToLowerInvariant();

            // "ACTR" is the default layer, always loaded.
            if (fourcc_str.EndsWith("r"))
            {
                return MapLayer.Default;
            }
            // Layers 10 and 11 use hexadecimal numbers A and B; we have to handle
            // these separately from layers 0-9.
            else if (fourcc_str.EndsWith("a"))
            {
                return MapLayer.LayerA;
            }
            else if (fourcc_str.EndsWith("b"))
            {
                return MapLayer.LayerB;
            }
            else
            {
                // The FourCC is "ACT0" through "ACT9", so we can get the proper layer
                // by adding 1 to the integer at the end of the FourCC string.
                int layer_no = Convert.ToInt32(fourcc_str[fourcc_str.Length - 1]);
                return (MapLayer)(layer_no + 1);
            }
        }

        public FourCC LayerToFourCC()
        {
            // Default layer
            if (Layer == MapLayer.Default)
            {
                return FourCC.TRES;
            }
            // Like above, layers 10 and 11 need special handling.
            else if (Layer == MapLayer.LayerA)
            {
                return FourCC.TREa;
            }
            else if (Layer == MapLayer.LayerB)
            {
                return FourCC.TREb;
            }
            // The FourCCs for layers 0-9 can be calculated by the layer index + 1.
            else
            {
                return FourCC.TRE0 + (int)Layer;
            }
        }
    }
}
