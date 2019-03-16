// 
using GameFormatReader.Common;
using OpenTK;
using System.ComponentModel;
using System.Diagnostics;
using System;
using WindEditor.ViewModel;

namespace WindEditor
{
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Bokoblin : Actor
	{
		// Auto-Generated Properties from Templates
		private int m_Type;

		private int m_SwitchSpawnsBokoblin;

		private int m_IsMiniboss;

		private int m_HeldItemId;

		private int m_AlertRange;

		[WProperty("Bokoblin", "Alert Range", true)]
		 public int AlertRange
		{ 
			get { return m_AlertRange; }
			set
			{
				m_AlertRange = value;
				OnPropertyChanged("AlertRange");
			}
		}

		private int m_PathIndex;

		private int m_SwitchID;

		[WProperty("Bokoblin", "Switch ID", true)]
		 public int SwitchID
		{ 
			get { return m_SwitchID; }
			set
			{
				m_SwitchID = value;
				OnPropertyChanged("SwitchID");
			}
		}

		// Constructor
		public Bokoblin(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
			m_Type = (int)(Parameters & 15) >> 0;
			m_SwitchSpawnsBokoblin = (int)(Parameters & 16) >> 5;
			m_IsMiniboss = (int)(Parameters & 32) >> 5;
			m_HeldItemId = (int)(Parameters & 192) >> 6;
			AlertRange = (int)(Parameters & 65280) >> 8;
			m_PathIndex = (int)(Parameters & 16711680) >> 16;
			SwitchID = (int)(Parameters & 4278190080) >> 24;
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
			Parameters |= (int)(m_Type & 15) << 0;
			Parameters |= (int)(m_SwitchSpawnsBokoblin & 16) << 5;
			Parameters |= (int)(m_IsMiniboss & 32) << 5;
			Parameters |= (int)(m_HeldItemId & 192) << 6;
			Parameters |= (int)(AlertRange & 65280) << 8;
			Parameters |= (int)(m_PathIndex & 16711680) << 16;
			Parameters |= (int)(SwitchID & 4278190080) << 24;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Foliage : Actor
	{
		// Auto-Generated Properties from Templates
		private int m_Pattern;

		[WProperty("Foliage", "Pattern", true)]
		 public int Pattern
		{ 
			get { return m_Pattern; }
			set
			{
				m_Pattern = value;
				OnPropertyChanged("Pattern");
			}
		}

		private int m_Type;

		private int m_ItemDrop;

		[WProperty("Foliage", "Item Drop", true)]
		 public int ItemDrop
		{ 
			get { return m_ItemDrop; }
			set
			{
				m_ItemDrop = value;
				OnPropertyChanged("ItemDrop");
			}
		}

		// Constructor
		public Foliage(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
			Pattern = (int)(Parameters & 15) >> 0;
			m_Type = (int)(Parameters & 48) >> 4;
			ItemDrop = (int)(Parameters & 4032) >> 6;
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
			Parameters |= (int)(Pattern & 15) << 0;
			Parameters |= (int)(m_Type & 48) << 4;
			Parameters |= (int)(ItemDrop & 4032) << 6;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Gohdan : Actor
	{
		// Auto-Generated Properties from Templates
		private int m_Component;

		// Constructor
		public Gohdan(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
			m_Component = (int)(Parameters & 15) >> 0;
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
			Parameters |= (int)(m_Component & 15) << 0;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Kargaroc : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public Kargaroc(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class LargeRock : Actor
	{
		// Auto-Generated Properties from Templates
		private int m_Unk1;

		[WProperty("Rock", "Unk1", true)]
		 public int Unk1
		{ 
			get { return m_Unk1; }
			set
			{
				m_Unk1 = value;
				OnPropertyChanged("Unk1");
			}
		}

		private int m_Unk2;

		[WProperty("Rock", "Unk2", true)]
		 public int Unk2
		{ 
			get { return m_Unk2; }
			set
			{
				m_Unk2 = value;
				OnPropertyChanged("Unk2");
			}
		}

		private int m_SwitchID;

		[WProperty("Rock", "Switch ID", true)]
		 public int SwitchID
		{ 
			get { return m_SwitchID; }
			set
			{
				m_SwitchID = value;
				OnPropertyChanged("SwitchID");
			}
		}

		private int m_Unk4;

		[WProperty("Rock", "Unk4", true)]
		 public int Unk4
		{ 
			get { return m_Unk4; }
			set
			{
				m_Unk4 = value;
				OnPropertyChanged("Unk4");
			}
		}

		private int m_Unk5;

		[WProperty("Rock", "Unk5", true)]
		 public int Unk5
		{ 
			get { return m_Unk5; }
			set
			{
				m_Unk5 = value;
				OnPropertyChanged("Unk5");
			}
		}

		private int m_Unk6;

		[WProperty("Rock", "Unk6", true)]
		 public int Unk6
		{ 
			get { return m_Unk6; }
			set
			{
				m_Unk6 = value;
				OnPropertyChanged("Unk6");
			}
		}

		// Constructor
		public LargeRock(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
			Unk1 = (int)(Parameters & 63) >> 0;
			Unk2 = (int)(Parameters & 192) >> 6;
			SwitchID = (int)(Parameters & 65280) >> 8;
			Unk4 = (int)(Parameters & 8323072) >> 16;
			Unk5 = (int)(Parameters & 117440512) >> 24;
			Unk6 = (int)(Parameters & 1879048192) >> 28;
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
			Parameters |= (int)(Unk1 & 63) << 0;
			Parameters |= (int)(Unk2 & 192) << 6;
			Parameters |= (int)(SwitchID & 65280) << 8;
			Parameters |= (int)(Unk4 & 8323072) << 16;
			Parameters |= (int)(Unk5 & 117440512) << 24;
			Parameters |= (int)(Unk6 & 1879048192) << 28;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class LargeTree : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public LargeTree(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Medli : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public Medli(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Tetra : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public Tetra(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
		}
	}
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Zelda : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public Zelda(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		public void GetPropertiesFromParameters()
		{
		}

		public void SetParametersWithProperties()
		{
			Parameters = 0;
		}
	}

} // namespace WindEditor
