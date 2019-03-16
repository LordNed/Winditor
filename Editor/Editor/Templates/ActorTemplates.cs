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
