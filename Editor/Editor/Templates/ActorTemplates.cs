// 
using GameFormatReader.Common;
using OpenTK;
using System.ComponentModel;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using WindEditor.ViewModel;

namespace WindEditor
{
	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class acorn_leaf : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("uint", "Unknown_1", "acorn_leaf", "Unknown_1", 1000);

		public uint Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return (uint)value_as_int;
			}

			set
			{
                int value_as_int = 0;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public acorn_leaf(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class agbsw0 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "agbsw0", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FFFF | (value_as_int << 0 & 0x0000FFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "agbsw0", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "agbsw0", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "agbsw0", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFF0000 | (value_as_int << 16 & 0xFFFF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "agbsw0", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "agbsw0", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public agbsw0(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class alldie : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "alldie", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public alldie(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class am : Actor
	{
		// Auto-Generated Properties from Templates
		public enum BehaviorTypeEnum
		{
			Wandering = 0,
			Guards_an_area = 1,
		}

					public WProperty_new BehaviorTypeProperty { get; set; } = new WProperty_new("BehaviorTypeEnum", "BehaviorType", "am", "Behavior Type");

		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		public WIntProperty GuardedAreaRadiusHundredsProperty { get; set; } = new WIntProperty("int", "GuardedAreaRadiusHundreds", "am", "Guarded Area Radius (Hundreds)", 0);

		public int GuardedAreaRadiusHundreds
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("GuardedAreaRadiusHundreds");
			}
		}

					public WProperty_new SwitchActivatesArmosKnightProperty { get; set; } = new WProperty_new("bool", "SwitchActivatesArmosKnight", "am", "Switch Activates Armos Knight?");

		public bool SwitchActivatesArmosKnight
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("SwitchActivatesArmosKnight");
			}
		}

		public WIntProperty DisableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "DisableSpawnSwitch", "am", "Disable Spawn Switch", 0);

		public int DisableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("DisableSpawnSwitch");
			}
		}

		// Constructor
		public am(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class am2 : Actor
	{
		// Auto-Generated Properties from Templates
		public int Unused_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unused_1");
			}
		}

		public WIntProperty SightRangeHundredsProperty { get; set; } = new WIntProperty("int", "SightRangeHundreds", "am2", "Sight Range (Hundreds)", 0);

		public int SightRangeHundreds
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("SightRangeHundreds");
			}
		}

					public WProperty_new SwitchActivatesArmosProperty { get; set; } = new WProperty_new("bool", "SwitchActivatesArmos", "am2", "Switch Activates Armos?");

		public bool SwitchActivatesArmos
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("SwitchActivatesArmos");
			}
		}

		public WIntProperty DisableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "DisableSpawnSwitch", "am2", "Disable Spawn Switch", 0);

		public int DisableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("DisableSpawnSwitch");
			}
		}

		// Constructor
		public am2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class amiprop : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "amiprop", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "amiprop", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public amiprop(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class andsw0 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "andsw0", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "andsw0", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "andsw0", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "andsw0", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "andsw0", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "andsw0", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "andsw0", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public andsw0(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class andsw2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "andsw2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "andsw2", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "andsw2", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "andsw2", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "andsw2", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "andsw2", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public andsw2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class arrow_iceeff : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public arrow_iceeff(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class arrow_lighteff : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "arrow_lighteff", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public arrow_lighteff(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class atdoor : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "atdoor", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public atdoor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class att : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "att", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public att(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class auction : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public auction(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bb : Actor
	{
		// Auto-Generated Properties from Templates
		public enum BehaviorTypeEnum
		{
			Flying_around = 0,
			Instantly_targets_Link = 3,
			Sits_in_place_A = 4,
			Carrying_a_Moblin = 5,
			Carrying_a_Bokoblin = 6,
			Sits_in_place_B = 7,
		}

					public WProperty_new BehaviorTypeProperty { get; set; } = new WProperty_new("BehaviorTypeEnum", "BehaviorType", "bb", "Behavior Type");

		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		public WIntProperty SightRangeHundredsProperty { get; set; } = new WIntProperty("int", "SightRangeHundreds", "bb", "Sight Range (Hundreds)", 0);

		public int SightRangeHundreds
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("SightRangeHundreds");
			}
		}

		public WActorReferenceProperty PathProperty { get; set; } = new WActorReferenceProperty("Path_v2", "Path", "bb", "Path", SourceScene.Room);

		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Path");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "bb", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		public WIntProperty DisableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "DisableSpawnSwitch", "bb", "Disable Spawn Switch", 0);

		public int DisableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("DisableSpawnSwitch");
			}
		}

		// Constructor
		public bb(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bdk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bdk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public bdk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bdkobj : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bdkobj", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bdkobj", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public bdkobj(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class beam : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "beam", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "beam", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "beam", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "beam", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "beam", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x10000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x10000000 | (value_as_int << 28 & 0x10000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "beam", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x30000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x30000000 | (value_as_int << 28 & 0x30000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "beam", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xC0000000) >> 30);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xC0000000 | (value_as_int << 30 & 0xC0000000));
				OnPropertyChanged("Unknown_7");
			}
		}

		public WIntProperty Unknown_8Property { get; set; } = new WIntProperty("int", "Unknown_8", "beam", "Unknown_8", 0);

		public int Unknown_8
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_8");
			}
		}

		// Constructor
		public beam(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bflower : Actor
	{
		// Auto-Generated Properties from Templates
		public int FlowerType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("FlowerType");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bflower", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public bflower(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bgn : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bgn", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public bgn(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bgn2 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public bgn2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bgn3 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public bgn3(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bigelf : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bigelf", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bigelf", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "bigelf", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public bigelf(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bita : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bita", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bita", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public bita(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bk", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000010) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000010 | (value_as_int << 4 & 0x00000010));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "bk", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000020) >> 5);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000020 | (value_as_int << 5 & 0x00000020));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "bk", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000C0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000C0 | (value_as_int << 6 & 0x000000C0));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "bk", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "bk", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "bk", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_7");
			}
		}

		public WIntProperty Unknown_8Property { get; set; } = new WIntProperty("int", "Unknown_8", "bk", "Unknown_8", 0);

		public int Unknown_8
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_8");
			}
		}

		// Constructor
		public bk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bl : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Red_Bubble = 0,
			Blue_Bubble = 1,
			Path_Following_Red_Bubble = 2,
			Path_Following_Blue_Bubble = 3,
			Inanimate_Skull = 128,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "bl", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "bl", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		public WActorReferenceProperty PathProperty { get; set; } = new WActorReferenceProperty("Path_v2", "Path", "bl", "Path", SourceScene.Room);

		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Path");
			}
		}

					public WProperty_new FloatatInitialHeightProperty { get; set; } = new WProperty_new("bool", "FloatatInitialHeight", "bl", "Float at Initial Height?");

		public bool FloatatInitialHeight
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("FloatatInitialHeight");
			}
		}

		// Constructor
		public bl(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bmd : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public bmd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bmdfoot : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bmdfoot", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public bmdfoot(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bmdhand : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bmdhand", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bmdhand", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000003) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000003 | (value_as_int << 0 & 0x00000003));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "bmdhand", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000001F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000001F | (value_as_int << 0 & 0x0000001F));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public bmdhand(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bo : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Normal = 0,
			Bud_dying = 1,
			Head_and_stem_dying = 2,
			Bugged_and_invisible = 3,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "bo", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

					public WProperty_new LeaveBehindBabaBudProperty { get; set; } = new WProperty_new("bool", "LeaveBehindBabaBud", "bo", "Leave Behind Baba Bud?");

		public bool LeaveBehindBabaBud
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("LeaveBehindBabaBud");
			}
		}

		// Constructor
		public bo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class boko : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "boko", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x3FFFFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x3FFFFFFF | (value_as_int << 0 & 0x3FFFFFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "boko", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFFFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFFFFFF | (value_as_int << 0 & 0xFFFFFFFF));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "boko", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public boko(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class boss_item : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "boss_item", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public boss_item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bpw : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bpw", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bpw", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "bpw", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public bpw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class branch : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public branch(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bridge : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bridge", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "bridge", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "bridge", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public bridge(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bst : Actor
	{
		// Auto-Generated Properties from Templates
		public int ComponentType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("ComponentType");
			}
		}

		// Constructor
		public bst(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class btd : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public btd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bwd : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bwd", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public bwd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bwdg : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public bwdg(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bwds : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "bwds", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public bwds(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class canon : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "canon", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public canon(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class cc : Actor
	{
		// Auto-Generated Properties from Templates
		public enum BehaviorTypeEnum
		{
			Normal = 0,
			Falls_from_ceiling = 1,
			Bugged_and_does_not_appear = 2,
			Random_movement = 3,
			Hiding_in_pot = 4,
		}

					public WProperty_new BehaviorTypeProperty { get; set; } = new WProperty_new("BehaviorTypeEnum", "BehaviorType", "cc", "Behavior Type");

		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		public enum ColorTypeEnum
		{
			Green = 0,
			Red = 1,
			Blue = 2,
			Dark = 3,
			Yellow = 4,
			Green_and_attacks_instantly = 10,
			Red_and_attacks_instantly = 11,
			Blue_and_attacks_instantly = 12,
			Dark_and_attacks_instantly = 13,
			Yellow_and_attacks_instantly = 14,
			Red_and_attacks_instantly_and_more_vulnerabilities = 15,
		}

					public WProperty_new ColorTypeProperty { get; set; } = new WProperty_new("ColorTypeEnum", "ColorType", "cc", "Color Type");

		public ColorTypeEnum ColorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return (ColorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("ColorType");
			}
		}

		public WIntProperty SightRangeTensProperty { get; set; } = new WIntProperty("int", "SightRangeTens", "cc", "Sight Range (Tens)", 0);

		public int SightRangeTens
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("SightRangeTens");
			}
		}

		public WIntProperty DisableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "DisableSpawnSwitch", "cc", "Disable Spawn Switch", 0);

		public int DisableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("DisableSpawnSwitch");
			}
		}

		// Constructor
		public cc(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class coming2 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public coming2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class coming3 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public coming3(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class dai : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "dai", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public dai(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class daiocta : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "daiocta", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "daiocta", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "daiocta", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "daiocta", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "daiocta", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public daiocta(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class daiocta_eye : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public daiocta_eye(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class deku_item : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty ItemPickupFlagProperty { get; set; } = new WIntProperty("int", "ItemPickupFlag", "Deku Leaf Pickup", "Item Pickup Flag", 0);

		public int ItemPickupFlag
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("ItemPickupFlag");
			}
		}

		// Constructor
		public deku_item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class demo_dk : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public demo_dk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class demo_item : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "demo_item", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "demo_item", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public demo_item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class demo_kmm : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public demo_kmm(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class dk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "dk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public dk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class door10 : TGDR
	{
		// Auto-Generated Properties from Templates
		public WIntProperty SwitchBitProperty { get; set; } = new WIntProperty("int", "SwitchBit", "Door", "Switch Bit", 0);

		public int SwitchBit
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("SwitchBit");
			}
		}

		public WIntProperty TypeProperty { get; set; } = new WIntProperty("int", "Type", "Door", "Type", 0);

		public int Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty EventIDProperty { get; set; } = new WIntProperty("int", "EventID", "Door", "Event ID", 0);

		public int EventID
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000FF000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000FF000 | (value_as_int << 12 & 0x000FF000));
				OnPropertyChanged("EventID");
			}
		}

		public WIntProperty SwitchBit2Property { get; set; } = new WIntProperty("int", "SwitchBit2", "Door", "Switch Bit 2", 0);

		public int SwitchBit2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0FF00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0FF00000 | (value_as_int << 20 & 0x0FF00000));
				OnPropertyChanged("SwitchBit2");
			}
		}

		public WIntProperty FromRoomNumberProperty { get; set; } = new WIntProperty("int", "FromRoomNumber", "Door", "From Room Number", 0);

		public int FromRoomNumber
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x003F | (value_as_int << 0 & 0x003F));
				OnPropertyChanged("FromRoomNumber");
			}
		}

		public WIntProperty ToRoomNumberProperty { get; set; } = new WIntProperty("int", "ToRoomNumber", "Door", "To Room Number", 0);

		public int ToRoomNumber
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x0FC0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x0FC0 | (value_as_int << 6 & 0x0FC0));
				OnPropertyChanged("ToRoomNumber");
			}
		}

		public WIntProperty ShipIDProperty { get; set; } = new WIntProperty("int", "ShipID", "Door", "Ship ID", 0);

		public int ShipID
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x003F | (value_as_int << 0 & 0x003F));
				OnPropertyChanged("ShipID");
			}
		}

		public WIntProperty Arg1Property { get; set; } = new WIntProperty("int", "Arg1", "Door", "Arg 1", 0);

		public int Arg1
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Arg1");
			}
		}

		// Constructor
		public door10(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class door12 : TGDR
	{
		// Auto-Generated Properties from Templates
		public WIntProperty SwitchBitProperty { get; set; } = new WIntProperty("int", "SwitchBit", "Door", "Switch Bit", 0);

		public int SwitchBit
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("SwitchBit");
			}
		}

		public WIntProperty TypeProperty { get; set; } = new WIntProperty("int", "Type", "Door", "Type", 0);

		public int Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty EventIDProperty { get; set; } = new WIntProperty("int", "EventID", "Door", "Event ID", 0);

		public int EventID
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000FF000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000FF000 | (value_as_int << 12 & 0x000FF000));
				OnPropertyChanged("EventID");
			}
		}

		public WIntProperty SwitchBit2Property { get; set; } = new WIntProperty("int", "SwitchBit2", "Door", "Switch Bit 2", 0);

		public int SwitchBit2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0FF00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0FF00000 | (value_as_int << 20 & 0x0FF00000));
				OnPropertyChanged("SwitchBit2");
			}
		}

		public WIntProperty FromRoomNumberProperty { get; set; } = new WIntProperty("int", "FromRoomNumber", "Door", "From Room Number", 0);

		public int FromRoomNumber
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x003F | (value_as_int << 0 & 0x003F));
				OnPropertyChanged("FromRoomNumber");
			}
		}

		public WIntProperty ToRoomNumberProperty { get; set; } = new WIntProperty("int", "ToRoomNumber", "Door", "To Room Number", 0);

		public int ToRoomNumber
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x0FC0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x0FC0 | (value_as_int << 6 & 0x0FC0));
				OnPropertyChanged("ToRoomNumber");
			}
		}

		public WIntProperty ShipIDProperty { get; set; } = new WIntProperty("int", "ShipID", "Door", "Ship ID", 0);

		public int ShipID
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x003F | (value_as_int << 0 & 0x003F));
				OnPropertyChanged("ShipID");
			}
		}

		public WIntProperty Arg1Property { get; set; } = new WIntProperty("int", "Arg1", "Door", "Arg 1", 0);

		public int Arg1
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Arg1");
			}
		}

		// Constructor
		public door12(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class dr : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public dr(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class dr2 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public dr2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class dummy : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public dummy(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ep : Actor
	{
		// Auto-Generated Properties from Templates
		public int Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "Unknowns", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000040) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000040 | (value_as_int << 6 & 0x00000040));
				OnPropertyChanged("Unknown_2");
			}
		}

		public int IsWooden
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000080) >> 7);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000080 | (value_as_int << 7 & 0x00000080));
				OnPropertyChanged("IsWooden");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "Unknowns", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "Unknowns", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public int OnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("OnSwitch");
			}
		}

		// Constructor
		public ep(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fallrock : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public fallrock(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fallrock_tag : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "fallrock_tag", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public fallrock_tag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fan : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "fan", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "fan", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000300) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000300 | (value_as_int << 8 & 0x00000300));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public fan(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ff : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ff", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "ff", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "ff", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFF00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFF00FF | (value_as_int << 0 & 0xFFFF00FF));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public ff(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fganon : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "fganon", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "fganon", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "fganon", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "fganon", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "fganon", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public fganon(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fgmahou : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "fgmahou", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public fgmahou(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fire : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "fire", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "fire", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "fire", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0001F000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0001F000 | (value_as_int << 12 & 0x0001F000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "fire", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000E0000) >> 17);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000E0000 | (value_as_int << 17 & 0x000E0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "fire", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public fire(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class floor : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "floor", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public floor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fm : Actor
	{
		// Auto-Generated Properties from Templates
		public WActorReferenceProperty LinkCapturedExitProperty { get; set; } = new WActorReferenceProperty("ExitData", "LinkCapturedExit", "fm", "Link Captured Exit", SourceScene.Room);

		public ExitData LinkCapturedExit
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<ExitData> list = cur_object.GetChildrenOfType<ExitData>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<ExitData> list = cur_object.GetChildrenOfType<ExitData>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("LinkCapturedExit");
			}
		}

		public enum TypeEnum
		{
			Stalker_unused = 0,
			Follows_path = 1,
			Doesnt_follow_path = 2,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "fm", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000300) >> 8);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000300 | (value_as_int << 8 & 0x00000300));
				OnPropertyChanged("Type");
			}
		}

		public enum TargetingBehaviorTypeEnum
		{
			Target_Link_and_partner = 0,
			Target_Link_only = 1,
			Target_Partner_only = 2,
		}

					public WProperty_new TargetingBehaviorTypeProperty { get; set; } = new WProperty_new("TargetingBehaviorTypeEnum", "TargetingBehaviorType", "fm", "Targeting Behavior Type");

		public TargetingBehaviorTypeEnum TargetingBehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000C00) >> 10);
				return (TargetingBehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000C00 | (value_as_int << 10 & 0x00000C00));
				OnPropertyChanged("TargetingBehaviorType");
			}
		}

		public WActorReferenceProperty PathProperty { get; set; } = new WActorReferenceProperty("Path_v2", "Path", "fm", "Path", SourceScene.Room);

		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Path");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "fm", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		public WIntProperty DisableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "DisableSpawnSwitch", "fm", "Disable Spawn Switch", 0);

		public int DisableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("DisableSpawnSwitch");
			}
		}

		public WActorReferenceProperty PartnerCapturedExitProperty { get; set; } = new WActorReferenceProperty("ExitData", "PartnerCapturedExit", "fm", "Partner Captured Exit", SourceScene.Room);

		public ExitData PartnerCapturedExit
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFF00) >> 8);
				if (value_as_int == 0xFF) { return null; }
				WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
				List<ExitData> list = stage.GetChildrenOfType<ExitData>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
				List<ExitData> list = stage.GetChildrenOfType<ExitData>();
				int value_as_int = list.IndexOf(value);
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("PartnerCapturedExit");
			}
		}

		public WIntProperty SightRangeHundredsProperty { get; set; } = new WIntProperty("int", "SightRangeHundreds", "fm", "Sight Range (Hundreds)", 0);

		public int SightRangeHundreds
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("SightRangeHundreds");
			}
		}

		// Constructor
		public fm(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class f_pc_profile_lst : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public f_pc_profile_lst(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ghostship : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ghostship", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "ghostship", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public ghostship(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class gm : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "gm", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "gm", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "gm", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "gm", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "gm", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public gm(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class gnd : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "gnd", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "gnd", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public gnd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class goal_flag : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "goal_flag", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "goal_flag", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public goal_flag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class grass : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "grass", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "grass", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000030) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000030 | (value_as_int << 4 & 0x00000030));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "grass", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000FC0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000FC0 | (value_as_int << 6 & 0x00000FC0));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public grass(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class gy : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public gy(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class gy_ctrl : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Follows_you_across_sectors = 0,
			Stays_in_sector = 1,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "gy_ctrl", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty NumberofGyorgsProperty { get; set; } = new WIntProperty("int", "NumberofGyorgs", "gy_ctrl", "Number of Gyorgs", 0);

		public int NumberofGyorgs
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("NumberofGyorgs");
			}
		}

		public WIntProperty SightRangeThousandsProperty { get; set; } = new WIntProperty("int", "SightRangeThousands", "gy_ctrl", "Sight Range (Thousands)", 0);

		public int SightRangeThousands
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("SightRangeThousands");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "gy_ctrl", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		// Constructor
		public gy_ctrl(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class himo3 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "himo3", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "himo3", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "himo3", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "himo3", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "himo3", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFFFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFFFFFF | (value_as_int << 0 & 0xFFFFFFFF));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public himo3(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hitobj : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "hitobj", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public hitobj(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hmlif : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "hmlif", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "hmlif", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "hmlif", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "hmlif", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00300000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00300000 | (value_as_int << 20 & 0x00300000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "hmlif", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x07C00000) >> 22);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x07C00000 | (value_as_int << 22 & 0x07C00000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "hmlif", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x78000000) >> 27);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x78000000 | (value_as_int << 27 & 0x78000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "hmlif", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_7");
			}
		}

		public WIntProperty Unknown_8Property { get; set; } = new WIntProperty("int", "Unknown_8", "hmlif", "Unknown_8", 0);

		public int Unknown_8
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_8");
			}
		}

		// Constructor
		public hmlif(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hot_floor : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "hot_floor", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "hot_floor", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000002) >> 1);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000002 | (value_as_int << 1 & 0x00000002));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public hot_floor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hys : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "hys", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "hys", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public hys(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class icelift : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "icelift", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "icelift", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000FF0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000FF0 | (value_as_int << 4 & 0x00000FF0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "icelift", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000FF000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000FF000 | (value_as_int << 12 & 0x000FF000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public icelift(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ikari : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ikari", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "ikari", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public ikari(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class item : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "Item Pickup", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class jbo : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "jbo", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public jbo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kaji : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public kaji(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kamome : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kamome", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kamome", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "kamome", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "kamome", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public kamome(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kanban : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kanban", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFF00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFF00000 | (value_as_int << 20 & 0xFFF00000));
				OnPropertyChanged("Unknown_1");
			}
		}

		public int MessageID
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FFFF | (value_as_int << 0 & 0x0000FFFF));
				OnPropertyChanged("MessageID");
			}
		}

		// Constructor
		public kanban(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kantera : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kantera", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kantera", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "kantera", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public kantera(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kb : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kb", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kb", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "kb", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "kb", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "kb", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public kb(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kddoor : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public kddoor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ki : Actor
	{
		// Auto-Generated Properties from Templates
		public enum BehaviorTypeEnum
		{
			Hanging_from_ceiling = 0,
			Flying_around = 1,
			Launch_forward_on_spawn = 2,
			Immediately_targets_Link = 3,
			Instantly_dies_twice = 30,
			Hanging_from_ceiling_passive = 128,
		}

					public WProperty_new BehaviorTypeProperty { get; set; } = new WProperty_new("BehaviorTypeEnum", "BehaviorType", "ki", "Behavior Type");

		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		public enum SightRangeEnum
		{
			_300 = 0,
			_800 = 1,
			_1500 = 2,
			_3000 = 3,
			_3000_B = 127,
		}

					public WProperty_new SightRangeProperty { get; set; } = new WProperty_new("SightRangeEnum", "SightRange", "ki", "Sight Range");

		public SightRangeEnum SightRange
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00007F00) >> 8);
				return (SightRangeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00007F00 | (value_as_int << 8 & 0x00007F00));
				OnPropertyChanged("SightRange");
			}
		}

					public WProperty_new IsFireKeeseProperty { get; set; } = new WProperty_new("bool", "IsFireKeese", "ki", "Is Fire Keese?");

		public bool IsFireKeese
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00008000) >> 15);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0x00008000 | (value_as_int << 15 & 0x00008000));
				OnPropertyChanged("IsFireKeese");
			}
		}

		public WActorReferenceProperty PathProperty { get; set; } = new WActorReferenceProperty("Path_v2", "Path", "ki", "Path", SourceScene.Room);

		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Path");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "ki", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		// Constructor
		public ki(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kita : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kita", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kita", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public kita(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class klft : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "klft", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "klft", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "klft", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public klft(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kmon : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public kmon(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kn : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kn", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kn", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public kn(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class knob00 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "knob00", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xF0000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xF0000000 | (value_as_int << 28 & 0xF0000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public knob00(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kokiie : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kokiie", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kokiie", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public kokiie(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class komore : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public komore(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ks : Actor
	{
		// Auto-Generated Properties from Templates
		public enum BehaviorTypeEnum
		{
			Group_that_chases_player = 0,
			Group_that_stays_put = 1,
			Single_Morth_that_stays_put = 2,
			Single_Morth_that_chases_player = 3,
			Unknown_1 = 4,
			Unknown_2 = 5,
			Group_in_a_pot = 6,
		}

					public WProperty_new BehaviorTypeProperty { get; set; } = new WProperty_new("BehaviorTypeEnum", "BehaviorType", "ks", "Behavior Type");

		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		public WIntProperty NumberinGroupProperty { get; set; } = new WIntProperty("int", "NumberinGroup", "ks", "Number in Group", 0);

		public int NumberinGroup
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("NumberinGroup");
			}
		}

		public WIntProperty PotSightRangeTensProperty { get; set; } = new WIntProperty("int", "PotSightRangeTens", "ks", "Pot Sight Range (Tens)", 0);

		public int PotSightRangeTens
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("PotSightRangeTens");
			}
		}

		// Constructor
		public ks(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kt : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kt", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFFFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFFFFFF | (value_as_int << 0 & 0xFFFFFFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public kt(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kui : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kui", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kui", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "kui", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public kui(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag00 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kytag00", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kytag00", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "kytag00", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "kytag00", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "kytag00", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "kytag00", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "kytag00", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public kytag00(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag01 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public kytag01(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag02 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kytag02", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public kytag02(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag03 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kytag03", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public kytag03(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag04 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kytag04", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "kytag04", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "kytag04", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public kytag04(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag05 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kytag05", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public kytag05(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag06 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public kytag06(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag07 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "kytag07", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public kytag07(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class lamp : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "lamp", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public lamp(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class lbridge : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "lbridge", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public lbridge(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class leaflift : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "leaflift", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public leaflift(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class lod_bg : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public lod_bg(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class lstair : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "lstair", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public lstair(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class lwood : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public lwood(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class machine : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "machine", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public machine(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class magma : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public magma(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class majuu_flag : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "majuu_flag", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "majuu_flag", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public majuu_flag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mant : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "mant", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public mant(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mbdoor : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "mbdoor", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "mbdoor", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "mbdoor", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x003F | (value_as_int << 0 & 0x003F));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public mbdoor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mdoor : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "mdoor", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "mdoor", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "mdoor", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "mdoor", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public mdoor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mflft : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "mflft", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "mflft", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public mflft(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mgameboard : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public mgameboard(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mmusic : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public mmusic(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mo2 : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Blue_skin_without_lantern = 0,
			Brown_skin_with_lantern = 1,
			Carried_through_the_air = 5,
			Frozen_in_time = 15,
			Blue_skin_without_lantern_unused = 100,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "mo2", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		public enum FrozeninTimePoseEnum
		{
			Attacking_pose = 0,
			Walking_pose = 1,
		}

					public WProperty_new FrozeninTimePoseProperty { get; set; } = new WProperty_new("FrozeninTimePoseEnum", "FrozeninTimePose", "mo2", "Frozen in Time Pose");

		public FrozeninTimePoseEnum FrozeninTimePose
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return (FrozeninTimePoseEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("FrozeninTimePose");
			}
		}

		public WActorReferenceProperty PathProperty { get; set; } = new WActorReferenceProperty("Path_v2", "Path", "mo2", "Path", SourceScene.Room);

		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Path");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "mo2", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		public WIntProperty DisableSpawnonDeathSwitchProperty { get; set; } = new WIntProperty("int", "DisableSpawnonDeathSwitch", "mo2", "Disable Spawn on Death Switch", 0);

		public int DisableSpawnonDeathSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("DisableSpawnonDeathSwitch");
			}
		}

		// Constructor
		public mo2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class movie_player : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public movie_player(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mozo : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "mozo", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public mozo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class msw : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "msw", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public msw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mt : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "mt", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "mt", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00007F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00007F00 | (value_as_int << 8 & 0x00007F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "mt", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00008000) >> 15);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00008000 | (value_as_int << 15 & 0x00008000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "mt", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "mt", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "mt", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public mt(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mtoge : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "mtoge", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public mtoge(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ac1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ac1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_ac1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_ac1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ah : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ah", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_ah", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_ah(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_aj1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_aj1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_aj1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_aj1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_auction : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_auction(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ba1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ba1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_ba1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bj1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_bj1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_bj1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_bj1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bm1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_bm1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_bm1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_bm1", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_bm1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bmcon1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_bmcon1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_bmcon1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bms1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_bms1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_bms1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bmsw : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_bmsw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bs1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_bs1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_bs1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_bs1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_btsw : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_btsw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_btsw2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_btsw2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_btsw2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_cb1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_cb1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFFFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFFFFFF | (value_as_int << 0 & 0xFFFFFFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_cb1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_co1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_co1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_co1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_de1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_de1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_de1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ds1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ds1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_ds1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_gk1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_gk1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_gk1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_gp1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_gp1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_gp1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_gp1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_hi1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_hi1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_hi1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ho : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_ho(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_hr : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_hr", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_hr", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_hr", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_hr(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_jb1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_jb1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_jb1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ji1 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_ji1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kamome : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_kamome", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_kamome", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_kamome(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kf1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_kf1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_kf1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_kf1", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "npc_kf1", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public npc_kf1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kg1 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_kg1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kg2 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_kg2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kk1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_kk1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_kk1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_kk1", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00030000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00030000 | (value_as_int << 16 & 0x00030000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "npc_kk1", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public npc_kk1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_km1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_km1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_km1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ko1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ko1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_ko1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_ko1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kp1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_kp1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_kp1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ls1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ls1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_ls1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_md : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_md", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_md", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_md", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_md(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_mk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_mk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_mk", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_mk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_mn : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_mn", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_mn", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_mn", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_mn(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_mt : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_mt(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_nz : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_nz(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ob1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ob1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_ob1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_ob1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_os : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_os", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_os(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_p1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_p1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_p1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_p1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_p2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_p2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000003) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000003 | (value_as_int << 0 & 0x00000003));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_p2", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000003FC) >> 2);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000003FC | (value_as_int << 2 & 0x000003FC));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_p2", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0003FC00) >> 10);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0003FC00 | (value_as_int << 10 & 0x0003FC00));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_p2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_people : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_people", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_people", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_people", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_people(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_pf1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_pf1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_pf1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_pf1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_photo : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_photo", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_photo", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_photo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_pm1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_pm1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_pm1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_roten : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_roten", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_roten", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_roten(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_rsh1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_rsh1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_rsh1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_rsh1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_sarace : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_sarace(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_so : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_so(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_sv : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_sv(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_tc : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_tc", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_tc", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_tc(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_tt : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public npc_tt(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_uk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_uk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_uk", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "npc_uk", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_uk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ym1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_ym1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_ym1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_yw1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_yw1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "npc_yw1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public npc_yw1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_zk1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_zk1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_zk1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_zl1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "npc_zl1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public npc_zl1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class nz : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "nz", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "nz", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "nz", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public nz(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class nzg : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty SightRangeFivesProperty { get; set; } = new WIntProperty("int", "SightRangeFives", "nzg", "Sight Range (Fives)", 0);

		public int SightRangeFives
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("SightRangeFives");
			}
		}

		public WIntProperty MaximumNumberofRatsProperty { get; set; } = new WIntProperty("int", "MaximumNumberofRats", "nzg", "Maximum Number of Rats", 0);

		public int MaximumNumberofRats
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("MaximumNumberofRats");
			}
		}

		public enum TypeEnum
		{
			Spawns_Rats = 0,
			Spawns_Bombchus = 1,
			Spawns_Rats_and_Bombchus = 2,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "nzg", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Type");
			}
		}

		public int Unused
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unused");
			}
		}

		// Constructor
		public nzg(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_adnno : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_adnno(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ajav : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ajav", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ajav(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ajavw : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_ajavw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_akabe : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_akabe", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_akabe", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000300) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000300 | (value_as_int << 8 & 0x00000300));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_akabe", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00001000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00001000 | (value_as_int << 12 & 0x00001000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_akabe", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_akabe(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_apzl : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_apzl", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_apzl(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ashut : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ashut", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_ashut", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_ashut(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_auzu : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_auzu", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_auzu", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_auzu", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_auzu", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_auzu(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_aygr : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_aygr", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_aygr(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_balancelift : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_balancelift", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_balancelift(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_barrel : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_barrel", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x70000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x70000000 | (value_as_int << 28 & 0x70000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_barrel", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_barrel(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_barrel2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_barrel2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_barrel2", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000100 | (value_as_int << 8 & 0x00000100));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_barrel2", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000400) >> 10);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000400 | (value_as_int << 10 & 0x00000400));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_barrel2", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x007F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x007F0000 | (value_as_int << 16 & 0x007F0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_barrel2", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x03000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x03000000 | (value_as_int << 24 & 0x03000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_barrel2", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x10000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x10000000 | (value_as_int << 28 & 0x10000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public obj_barrel2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_barrier : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_barrier", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000100 | (value_as_int << 8 & 0x00000100));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_barrier", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_barrier(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_bemos : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_bemos", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_bemos", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_bemos", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_bemos", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xF0000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xF0000000 | (value_as_int << 28 & 0xF0000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_bemos", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x003F | (value_as_int << 0 & 0x003F));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_bemos", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x1FC0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x1FC0 | (value_as_int << 6 & 0x1FC0));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public obj_bemos(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_bscurtain : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_bscurtain", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_bscurtain(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_buoyflag : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_buoyflag", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000003) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000003 | (value_as_int << 0 & 0x00000003));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_buoyflag", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000100 | (value_as_int << 8 & 0x00000100));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_buoyflag", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_buoyflag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_buoyrace : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_buoyrace", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_buoyrace", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_buoyrace(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_cafelmp : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_cafelmp(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_canon : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_canon", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_canon", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_canon", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_canon", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_canon(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_coming : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_coming", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_coming", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000040) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000040 | (value_as_int << 6 & 0x00000040));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_coming", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000700) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000700 | (value_as_int << 8 & 0x00000700));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_coming", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_coming", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x03000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x03000000 | (value_as_int << 24 & 0x03000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_coming", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x30000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x30000000 | (value_as_int << 28 & 0x30000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public obj_coming(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_correct : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_correct", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_correct", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_correct", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_correct", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x40000000) >> 30);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x40000000 | (value_as_int << 30 & 0x40000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_correct(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_demo_barrel : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_demo_barrel(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_dmgroom : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_dmgroom(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_doguu : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_doguu", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_doguu(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_doguu_demo : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_doguu_demo", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_doguu_demo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_dragonhead : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_dragonhead", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_dragonhead(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_drift : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_drift", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000007) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000007 | (value_as_int << 0 & 0x00000007));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_drift(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_eayogn : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_eayogn(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ebomzo : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ebomzo", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ebomzo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_eff : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_eff", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_eff(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ekskz : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ekskz", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ekskz(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_eskban : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_eskban", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_eskban(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ferris : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ferris", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ferris(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_figure : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_figure", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_figure(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_firewall : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_firewall", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_firewall(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_flame : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_flame", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_flame", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00001F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00001F00 | (value_as_int << 8 & 0x00001F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_flame", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00006000) >> 13);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00006000 | (value_as_int << 13 & 0x00006000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_flame", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_flame", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x30000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x30000000 | (value_as_int << 28 & 0x30000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_flame", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public obj_flame(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ftree : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ftree", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ftree(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ganonbed : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_ganonbed(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gaship : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_gaship(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gaship2 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_gaship2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gnnbtltaki : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_gnnbtltaki", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_gnnbtltaki(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gnndemotakie : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_gnndemotakie(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gnndemotakis : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_gnndemotakis(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gong : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_gong(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gryw00 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_gryw00", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_gryw00(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_gtaki : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_gtaki(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hami2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hami2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_hami2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hami3 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hami3", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_hami3", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_hami3", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_hami3(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hami4 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hami4", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_hami4(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hat : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hat", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_hat(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hbrf1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hbrf1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_hbrf1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000100 | (value_as_int << 8 & 0x00000100));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_hbrf1", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_hbrf1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hcbh : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hcbh", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_hcbh", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00001FC0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00001FC0 | (value_as_int << 6 & 0x00001FC0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_hcbh", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x001FE000) >> 13);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x001FE000 | (value_as_int << 13 & 0x001FE000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_hcbh(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hfuck1 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_hfuck1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hha : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hha", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_hha", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_hha(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hlift : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hlift", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000007) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000007 | (value_as_int << 0 & 0x00000007));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_hlift", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000010) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000010 | (value_as_int << 4 & 0x00000010));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_hlift", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_hlift", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_hlift(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hole : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hole", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_hole", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_hole(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_homen : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_homen", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000007F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000007F | (value_as_int << 0 & 0x0000007F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_homen", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_homen", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0003F000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0003F000 | (value_as_int << 12 & 0x0003F000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_homen", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x003C0000) >> 18);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x003C0000 | (value_as_int << 18 & 0x003C0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_homen", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public obj_homen(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_homensmoke : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_homensmoke", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_homensmoke", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000002) >> 1);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000002 | (value_as_int << 1 & 0x00000002));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_homensmoke(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hsehi1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_hsehi1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_hsehi1", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FFFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FFFF00 | (value_as_int << 8 & 0x00FFFF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_hsehi1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_htetu1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_htetu1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_htetu1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ice : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ice", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ice(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_iceisland : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_iceisland", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_iceisland(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ikada : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ikada", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_ikada", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000003F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000003F0 | (value_as_int << 4 & 0x000003F0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_ikada", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0003FC00) >> 10);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0003FC00 | (value_as_int << 10 & 0x0003FC00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_ikada", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_ikada", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x03FC0000) >> 18);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x03FC0000 | (value_as_int << 18 & 0x03FC0000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_ikada", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "obj_ikada", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public obj_ikada(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_Itnak : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_Itnak", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_Itnak", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_Itnak(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_jump : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_jump", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_jump(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_kanat : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_kanat", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_kanat(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_kanoke : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_kanoke", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_kanoke", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003E) >> 1);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003E | (value_as_int << 1 & 0x0000003E));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_kanoke", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000040) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000040 | (value_as_int << 6 & 0x00000040));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_kanoke", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_kanoke", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public obj_kanoke(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ladder : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ladder", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000007) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000007 | (value_as_int << 0 & 0x00000007));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_ladder", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_ladder", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_ladder(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_leaves : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_leaves", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_leaves", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00001FC0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00001FC0 | (value_as_int << 6 & 0x00001FC0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_leaves", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x001FE000) >> 13);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x001FE000 | (value_as_int << 13 & 0x001FE000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_leaves(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_light : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_light(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_lpalm : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_lpalm(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_magmarock : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_magmarock", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_magmarock", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_magmarock(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_majyuu_door : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_majyuu_door", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_majyuu_door(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mkie : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_mkie", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_mkie", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000100 | (value_as_int << 8 & 0x00000100));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_mkie", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00001000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00001000 | (value_as_int << 12 & 0x00001000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_mkie", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00002000) >> 13);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00002000 | (value_as_int << 13 & 0x00002000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_mkie", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public obj_mkie(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mkiek : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_mkiek", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_mkiek", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000100 | (value_as_int << 8 & 0x00000100));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_mkiek(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mknjd : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_mknjd", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_mknjd", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_mknjd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mmrr : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_mmrr(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_monument : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_monument", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_monument", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_monument(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_movebox : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_movebox", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_movebox", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_movebox", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x007F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x007F0000 | (value_as_int << 16 & 0x007F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_movebox", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_movebox", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x40000000) >> 30);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x40000000 | (value_as_int << 30 & 0x40000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_movebox", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public obj_movebox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_msdan", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_msdan", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_msdan", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00040000) >> 18);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00040000 | (value_as_int << 18 & 0x00040000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_msdan", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_msdan(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_msdan2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_msdan2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan_sub : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_msdan_sub", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_msdan_sub", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_msdan_sub", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_msdan_sub(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan_sub2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_msdan_sub2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_msdan_sub2", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_msdan_sub2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mshokki : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_mshokki", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000003) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000003 | (value_as_int << 0 & 0x00000003));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_mshokki(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mtest : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_mtest", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000007) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000007 | (value_as_int << 0 & 0x00000007));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_mtest", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_mtest", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_mtest", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_mtest(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_nest : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_nest(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ohatch : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ohatch", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ohatch(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ojtree : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_ojtree(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ospbox : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ospbox", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_ospbox", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000700) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000700 | (value_as_int << 8 & 0x00000700));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_ospbox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_otble : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_otble", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_otble(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_paper : Actor
	{
		// Auto-Generated Properties from Templates
		public int MessageID
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FFFF | (value_as_int << 0 & 0x0000FFFF));
				OnPropertyChanged("MessageID");
			}
		}

		public int Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Type");
			}
		}

		// Constructor
		public obj_paper(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_pbco : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_pbco(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_pbka : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_pbka(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_pfall : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_pfall(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_pirateship : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_pirateship", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_pirateship", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_pirateship", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_pirateship(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_plant : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_plant(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_quake : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_quake", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_quake", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000700) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000700 | (value_as_int << 8 & 0x00000700));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_quake", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00003800) >> 11);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00003800 | (value_as_int << 11 & 0x00003800));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_quake(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_rcloud : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_rcloud", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_rcloud(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_rflw : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_rflw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_rforce : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_rforce(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_roten : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_roten", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_roten(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_shelf : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_shelf", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_shelf", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_shelf(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_shmrgrd : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_shmrgrd", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_shmrgrd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_smplbg : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_smplbg", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_smplbg(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_stair : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_stair", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_stair(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swflat : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_swflat", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000003) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000003 | (value_as_int << 0 & 0x00000003));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_swflat", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_swflat", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_swflat(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swhammer : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_swhammer", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_swhammer", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_swhammer(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swheavy : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_swheavy", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_swheavy", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x07000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x07000000 | (value_as_int << 24 & 0x07000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_swheavy(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swlight : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_swlight", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_swlight", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_swlight", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public obj_swlight(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swpush : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_swpush", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_swpush", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_swpush", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_swpush", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x07000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x07000000 | (value_as_int << 24 & 0x07000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_swpush", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x40000000) >> 30);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x40000000 | (value_as_int << 30 & 0x40000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_swpush", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public obj_swpush(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_table : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_table", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_table(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tapestry : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_tapestry", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_tapestry", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_tapestry(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tenmado : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_tenmado", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_tenmado", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_tenmado(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tide : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_tide", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00070000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00070000 | (value_as_int << 16 & 0x00070000));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_tide", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_tide(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_timer : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_timer", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_timer", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_timer(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tntrap : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_tntrap", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_tntrap", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_tntrap", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_tntrap", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00060000) >> 17);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00060000 | (value_as_int << 17 & 0x00060000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_tntrap(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_toripost : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_toripost(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tousekiki : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_tousekiki(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tower : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_tower(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_trap : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_trap", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_trap", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_trap(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tribox : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_tribox", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_tribox", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public obj_tribox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_try : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_try", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_try", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_try", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_try", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFF00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFF00FF | (value_as_int << 0 & 0xFFFF00FF));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public obj_try(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_usovmc : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_usovmc(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_Vds : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_Vds", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_Vds(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vfan : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_vfan", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_vfan(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vgnfd : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_vgnfd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vmc : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_vmc", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_vmc(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vmsdz : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_vmsdz(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vmsms : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_vmsms(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_volcano : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_volcano", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_volcano(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_Vteng : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_Vteng(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vtil : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_vtil", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_vtil(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vyasi : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_vyasi", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_vyasi(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_warpt : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_warpt", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "obj_warpt", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "obj_warpt", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000FF0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000FF0 | (value_as_int << 4 & 0x00000FF0));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "obj_warpt", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "obj_warpt", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000FF000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000FF000 | (value_as_int << 12 & 0x000FF000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "obj_warpt", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "obj_warpt", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_7");
			}
		}

		public WIntProperty Unknown_8Property { get; set; } = new WIntProperty("int", "Unknown_8", "obj_warpt", "Unknown_8", 0);

		public int Unknown_8
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_8");
			}
		}

		public WIntProperty Unknown_9Property { get; set; } = new WIntProperty("int", "Unknown_9", "obj_warpt", "Unknown_9", 0);

		public int Unknown_9
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Unknown_9");
			}
		}

		public WIntProperty Unknown_10Property { get; set; } = new WIntProperty("int", "Unknown_10", "obj_warpt", "Unknown_10", 0);

		public int Unknown_10
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_10");
			}
		}

		public WIntProperty Unknown_11Property { get; set; } = new WIntProperty("int", "Unknown_11", "obj_warpt", "Unknown_11", 0);

		public int Unknown_11
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Unknown_11");
			}
		}

		// Constructor
		public obj_warpt(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_wood : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_wood(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_xfuta : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public obj_xfuta(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_Yboil : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_Yboil", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_Yboil(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ygush00 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_ygush00", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000007) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000007 | (value_as_int << 0 & 0x00000007));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_ygush00(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_YLzou : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_YLzou", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_YLzou(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_zouK : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "obj_zouK", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public obj_zouK(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class oq : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Freshwater_Octorok = 0,
			Saltwater_Octorok_that_shoots_at_a_certain_range = 1,
			Saltwater_Octorok_that_spawns_close_to_player_and_shoots_after_a_certain_delay = 2,
			Saltwater_Octorok_spawner = 3,
			Saltwater_Octorok_that_shoots_after_a_certain_delay = 4,
			Spawned_saltwater_Octorok = 5,
			Rock_shot_by_a_freshwater_Octorok = 6,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "oq", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		public enum ProjectileTypeEnum
		{
			Rocks = 0,
			Bombs = 1,
		}

					public WProperty_new ProjectileTypeProperty { get; set; } = new WProperty_new("ProjectileTypeEnum", "ProjectileType", "oq", "Projectile Type");

		public ProjectileTypeEnum ProjectileType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return (ProjectileTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("ProjectileType");
			}
		}

		public WIntProperty SightRangeThousandsProperty { get; set; } = new WIntProperty("int", "SightRangeThousands", "oq", "Sight Range (Thousands)", 0);

		public int SightRangeThousands
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("SightRangeThousands");
			}
		}

		// Constructor
		public oq(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class oship : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "oship", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "oship", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "oship", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000F000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000F000 | (value_as_int << 12 & 0x0000F000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "oship", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "oship", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "oship", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "oship", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public oship(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class pedestal : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "pedestal", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public pedestal(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ph : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Peahat = 0,
			Seahat = 1,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "ph", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty HorizontalSightRangeHundredsProperty { get; set; } = new WIntProperty("int", "HorizontalSightRangeHundreds", "ph", "Horizontal Sight Range (Hundreds)", 0);

		public int HorizontalSightRangeHundreds
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("HorizontalSightRangeHundreds");
			}
		}

		public WIntProperty VerticalSightRangeHundredsProperty { get; set; } = new WIntProperty("int", "VerticalSightRangeHundreds", "ph", "Vertical Sight Range (Hundreds)", 0);

		public int VerticalSightRangeHundreds
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("VerticalSightRangeHundreds");
			}
		}

		// Constructor
		public ph(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class pirate_flag : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public pirate_flag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class pt : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Respawning = 0,
			Nonrespawning = 1,
			Nonrespawning_B = 15,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "pt", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
			}
		}

					public WProperty_new InitialMiniblinWontSpawnOnscreenProperty { get; set; } = new WProperty_new("bool", "InitialMiniblinWontSpawnOnscreen", "pt", "Initial Miniblin Won't Spawn Onscreen?");

		public bool InitialMiniblinWontSpawnOnscreen
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000010) >> 4);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0x00000010 | (value_as_int << 4 & 0x00000010));
				OnPropertyChanged("InitialMiniblinWontSpawnOnscreen");
			}
		}

		public enum RespawnDelayEnum
		{
			_20_frames = 0,
			_40_frames = 1,
			_60_frames = 2,
			_80_frames = 3,
			_100_frames = 4,
			_120_frames = 5,
			_140_frames = 6,
			Use_initial_spawn_delay = 7,
		}

					public WProperty_new RespawnDelayProperty { get; set; } = new WProperty_new("RespawnDelayEnum", "RespawnDelay", "pt", "Respawn Delay");

		public RespawnDelayEnum RespawnDelay
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000E0) >> 5);
				return (RespawnDelayEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000E0 | (value_as_int << 5 & 0x000000E0));
				OnPropertyChanged("RespawnDelay");
			}
		}

		public WIntProperty SightRangeHundredsProperty { get; set; } = new WIntProperty("int", "SightRangeHundreds", "pt", "Sight Range (Hundreds)", 0);

		public int SightRangeHundreds
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("SightRangeHundreds");
			}
		}

		public WIntProperty DisableRespawnSwitchProperty { get; set; } = new WIntProperty("int", "DisableRespawnSwitch", "pt", "Disable Respawn Switch", 0);

		public int DisableRespawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("DisableRespawnSwitch");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "pt", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		public WIntProperty InitialSpawnDelayProperty { get; set; } = new WIntProperty("int", "InitialSpawnDelay", "pt", "Initial Spawn Delay", 0);

		public int InitialSpawnDelay
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("InitialSpawnDelay");
			}
		}

		// Constructor
		public pt(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class pw : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Wandering = 0,
			Invisible_until_seeing_Link = 1,
			Invisible_except_Lantern_until_seeing_Link = 2,
			Jalhalla_Poe_A = 3,
			Jalhalla_Poe_B = 4,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "pw", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

					public WProperty_new HoversAtInitialHeightProperty { get; set; } = new WProperty_new("bool", "HoversAtInitialHeight", "pw", "Hovers At Initial Height?");

		public bool HoversAtInitialHeight
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0x00000100 | (value_as_int << 8 & 0x00000100));
				OnPropertyChanged("HoversAtInitialHeight");
			}
		}

		public enum ColorEnum
		{
			Blue = 0,
			Purple = 1,
			Orange = 2,
			Yellow = 3,
			Red = 4,
			Green = 5,
		}

					public WProperty_new ColorProperty { get; set; } = new WProperty_new("ColorEnum", "Color", "pw", "Color");

		public ColorEnum Color
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FE00) >> 9);
				return (ColorEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FE00 | (value_as_int << 9 & 0x0000FE00));
				OnPropertyChanged("Color");
			}
		}

		public WIntProperty SightRangeTensProperty { get; set; } = new WIntProperty("int", "SightRangeTens", "pw", "Sight Range (Tens)", 0);

		public int SightRangeTens
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("SightRangeTens");
			}
		}

		public WActorReferenceProperty PathProperty { get; set; } = new WActorReferenceProperty("Path_v2", "Path", "pw", "Path", SourceScene.Room);

		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Path");
			}
		}

		// Constructor
		public pw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class pz : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "pz", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public pz(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class race_item : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "race_item", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "race_item", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00007F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00007F00 | (value_as_int << 8 & 0x00007F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "race_item", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00078000) >> 15);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00078000 | (value_as_int << 15 & 0x00078000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public race_item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class rd : Actor
	{
		// Auto-Generated Properties from Templates
		public enum IdleAnimationEnum
		{
			Standing = 0,
			Sitting = 1,
		}

					public WProperty_new IdleAnimationProperty { get; set; } = new WProperty_new("IdleAnimationEnum", "IdleAnimation", "rd", "Idle Animation");

		public IdleAnimationEnum IdleAnimation
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return (IdleAnimationEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("IdleAnimation");
			}
		}

		public WIntProperty GuardedAreaRadiusProperty { get; set; } = new WIntProperty("int", "GuardedAreaRadius", "rd", "Guarded Area Radius", 0);

		public int GuardedAreaRadius
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FE) >> 1);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FE | (value_as_int << 1 & 0x000000FE));
				OnPropertyChanged("GuardedAreaRadius");
			}
		}

					public WProperty_new ShouldCheckSwitchToEnableSpawnProperty { get; set; } = new WProperty_new("bool", "ShouldCheckSwitchToEnableSpawn", "rd", "Should Check Switch To Enable Spawn?");

		public bool ShouldCheckSwitchToEnableSpawn
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 0xFF) {
					return true;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = Convert.ToInt32(value);
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("ShouldCheckSwitchToEnableSpawn");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "rd", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		// Constructor
		public rd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class rectangle : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public rectangle(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sail : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public sail(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class saku : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "saku", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "saku", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "saku", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "saku", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public saku(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class salvage : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public salvage(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class salvage_tbox : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "salvage_tbox", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "salvage_tbox", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public salvage_tbox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sbox : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public sbox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class scene_change : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public scene_change(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class seatag : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public seatag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class shand : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "shand", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public shand(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ship : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ship", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "ship", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "ship", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public ship(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class shop_item : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "shop_item", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "shop_item", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public shop_item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class shutter : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "shutter", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "shutter", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public shutter(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class shutter2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "shutter2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public shutter2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sie_flag : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public sie_flag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sitem : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "sitem", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "sitem", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "sitem", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "sitem", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public sitem(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "sk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "sk", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public sk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sk2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "sk2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "sk2", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "sk2", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public sk2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class spotbox : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "spotbox", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public spotbox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ss : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ss", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "ss", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "ss", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public ss(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ssk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ssk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "ssk", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "ssk", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public ssk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sss : Actor
	{
		// Auto-Generated Properties from Templates
		public enum SFXTypeEnum
		{
			SFX_58F7 = 0,
			SFX_58F8 = 1,
		}

					public WProperty_new SFXTypeProperty { get; set; } = new WProperty_new("SFXTypeEnum", "SFXType", "sss", "SFX Type");

		public SFXTypeEnum SFXType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (SFXTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("SFXType");
			}
		}

		public WIntProperty SightRangeTensProperty { get; set; } = new WIntProperty("int", "SightRangeTens", "sss", "Sight Range (Tens)", 0);

		public int SightRangeTens
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("SightRangeTens");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "sss", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		// Constructor
		public sss(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class st : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Normal = 0,
			Underground = 1,
			Lying_in_coffin = 2,
			Standing_in_coffin = 3,
			Upper_body = 14,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "st", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty AmbushSightRangeTensProperty { get; set; } = new WIntProperty("int", "AmbushSightRangeTens", "st", "Ambush Sight Range (Tens)", 0);

		public int AmbushSightRangeTens
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("AmbushSightRangeTens");
			}
		}

		public int Unused
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unused");
			}
		}

		public WIntProperty AmbushSwitchProperty { get; set; } = new WIntProperty("int", "AmbushSwitch", "st", "Ambush Switch", 0);

		public int AmbushSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("AmbushSwitch");
			}
		}

		public WIntProperty DeathSwitchProperty { get; set; } = new WIntProperty("int", "DeathSwitch", "st", "Death Switch", 0);

		public int DeathSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("DeathSwitch");
			}
		}

		// Constructor
		public st(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class steam_tag : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "steam_tag", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000003FC) >> 2);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000003FC | (value_as_int << 2 & 0x000003FC));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public steam_tag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class stone : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "stone", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "stone", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000C0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000C0 | (value_as_int << 6 & 0x000000C0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "stone", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "stone", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x007F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x007F0000 | (value_as_int << 16 & 0x007F0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "stone", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x07000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x07000000 | (value_as_int << 24 & 0x07000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "stone", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x70000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x70000000 | (value_as_int << 28 & 0x70000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public stone(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class stone2 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "stone2", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "stone2", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "stone2", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x007F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x007F0000 | (value_as_int << 16 & 0x007F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "stone2", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x07000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x07000000 | (value_as_int << 24 & 0x07000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "stone2", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x70000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x70000000 | (value_as_int << 28 & 0x70000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "stone2", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public stone2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swattack : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "swattack", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "swattack", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public swattack(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swc00 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "swc00", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "swc00", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "swc00", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00030000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00030000 | (value_as_int << 16 & 0x00030000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public swc00(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swhit0 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "swhit0", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "swhit0", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "swhit0", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "swhit0", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0FF00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0FF00000 | (value_as_int << 20 & 0x0FF00000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "swhit0", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "swhit0", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public swhit0(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class switem : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "switem", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "switem", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00003F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00003F00 | (value_as_int << 8 & 0x00003F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "switem", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x001FC000) >> 14);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x001FC000 | (value_as_int << 14 & 0x001FC000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public switem(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swpropeller : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "swpropeller", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "swpropeller", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public swpropeller(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swtact : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "swtact", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "swtact", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "swtact", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public swtact(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swtdoor : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "swtdoor", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "swtdoor", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public swtdoor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class syan : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public syan(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_attention : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_attention", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_attention", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000300) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000300 | (value_as_int << 8 & 0x00000300));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public tag_attention(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_ba1 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public tag_ba1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_etc : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_etc", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_etc", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public tag_etc(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_event : Actor
	{
		// Auto-Generated Properties from Templates
		public enum TypeEnum
		{
			Unknown_1 = 0,
			Unknown_2 = 1,
			Unknown_3 = 2,
			Unknown_4 = 3,
			Unknown_5 = 4,
			Unknown_6 = 5,
			Unknown_7 = 6,
			Unknown_8 = 7,
			Unknown_9 = 8,
			Unknown_10 = 9,
			Unknown_11 = 10,
			Unknown_12 = 11,
			Unknown_13 = 12,
			Unknown_14 = 13,
			Normal = 255,
		}

					public WProperty_new TypeProperty { get; set; } = new WProperty_new("TypeEnum", "Type", "tag_event", "Type");

		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		public WIntProperty SetSwitchProperty { get; set; } = new WIntProperty("int", "SetSwitch", "tag_event", "Set Switch", 0);

		public int SetSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("SetSwitch");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "tag_event", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		public WActorReferenceProperty EventProperty { get; set; } = new WActorReferenceProperty("MapEvent", "Event", "tag_event", "Event", SourceScene.Stage);

		public MapEvent Event
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				if (value_as_int == 0xFF) { return null; }
				WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
				List<MapEvent> list = stage.GetChildrenOfType<MapEvent>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
				List<MapEvent> list = stage.GetChildrenOfType<MapEvent>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Event");
			}
		}

		public WIntProperty EnableSpawnEventBitProperty { get; set; } = new WIntProperty("int", "EnableSpawnEventBit", "tag_event", "Enable Spawn Event Bit", 0);

		public int EnableSpawnEventBit
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("EnableSpawnEventBit");
			}
		}

		// Constructor
		public tag_event(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_evsw : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_evsw", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_evsw", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FFFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FFFF00 | (value_as_int << 8 & 0x00FFFF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_evsw", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x03000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x03000000 | (value_as_int << 24 & 0x03000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public tag_evsw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_ghostship : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_ghostship", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_ghostship", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public tag_ghostship(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_hint : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_hint", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_hint", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000C0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000C0 | (value_as_int << 6 & 0x000000C0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_hint", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tag_hint", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "tag_hint", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public int MessageID
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("MessageID");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "tag_hint", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public tag_hint(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_island : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_island", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_island", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_island", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public tag_island(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_kb_item : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_kb_item", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_kb_item", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_kb_item", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tag_kb_item", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public tag_kb_item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_kf1 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_kf1", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public tag_kf1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_kk1 : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public tag_kk1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_light : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_light", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000003) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000003 | (value_as_int << 0 & 0x00000003));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_light", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000300) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000300 | (value_as_int << 8 & 0x00000300));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_light", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000003FC) >> 2);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000003FC | (value_as_int << 2 & 0x000003FC));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tag_light", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00003C00) >> 10);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00003C00 | (value_as_int << 10 & 0x00003C00));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "tag_light", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000C000) >> 14);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000C000 | (value_as_int << 14 & 0x0000C000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "tag_light", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "tag_light", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public tag_light(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_md_cb : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty MessageIDProperty { get; set; } = new WIntProperty("int", "MessageID", "tag_md_cb", "MessageID", 0);

		public int MessageID
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FFFF | (value_as_int << 0 & 0x0000FFFF));
				OnPropertyChanged("MessageID");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_md_cb", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_md_cb", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tag_md_cb", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public tag_md_cb(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_mk : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_mk", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_mk", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_mk", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public tag_mk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_msg : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_msg", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000C0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000C0 | (value_as_int << 6 & 0x000000C0));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_msg", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_msg", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tag_msg", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public int MessageID
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("MessageID");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "tag_msg", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_6");
			}
		}

		// Constructor
		public tag_msg(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_photo : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_photo", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public tag_photo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_ret : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_ret", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public tag_ret(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_so : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_so", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_so", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_so", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public tag_so(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_volcano : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_volcano", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tag_volcano", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000C0) >> 6);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000C0 | (value_as_int << 6 & 0x000000C0));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tag_volcano", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tag_volcano", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public tag_volcano(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_waterlevel : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tag_waterlevel", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public tag_waterlevel(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tama : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tama", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFFFF | (value_as_int << 0 & 0xFFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public tama(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tbox : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tbox", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000007F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000007F | (value_as_int << 0 & 0x0000007F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tbox", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F80) >> 7);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F80 | (value_as_int << 7 & 0x00000F80));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tbox", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000FF000) >> 12);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000FF000 | (value_as_int << 12 & 0x000FF000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tbox", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "tbox", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x003F | (value_as_int << 0 & 0x003F));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "tbox", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "tbox", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public tbox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class title : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public title(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tn : Actor
	{
		// Auto-Generated Properties from Templates
		public enum BehaviorTypeEnum
		{
			Wandering = 0,
			Guards_an_area = 4,
			Miniboss = 13,
			Drops_down_during_event = 14,
			Frozen_in_time = 15,
		}

					public WProperty_new BehaviorTypeProperty { get; set; } = new WProperty_new("BehaviorTypeEnum", "BehaviorType", "tn", "Behavior Type");

		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("BehaviorType");
			}
		}

		public enum ColorEnum
		{
			Silver_and_blue = 0,
			Silver_and_red = 1,
			Gold_and_black = 2,
			White_and_silver = 3,
			Black_and_brown = 4,
			Red_and_gold = 5,
			Red_and_gold_B = 15,
		}

					public WProperty_new ColorProperty { get; set; } = new WProperty_new("ColorEnum", "Color", "tn", "Color");

		public ColorEnum Color
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				return (ColorEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("Color");
			}
		}

		public WIntProperty GuardedAreaRadiusTensProperty { get; set; } = new WIntProperty("int", "GuardedAreaRadiusTens", "tn", "Guarded Area Radius (Tens)", 0);

		public int GuardedAreaRadiusTens
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("GuardedAreaRadiusTens");
			}
		}

		public enum FrozeninTimePoseEnum
		{
			Walking_pose = 0,
			Attacking_pose = 1,
		}

					public WProperty_new FrozeninTimePoseProperty { get; set; } = new WProperty_new("FrozeninTimePoseEnum", "FrozeninTimePose", "tn", "Frozen in Time Pose");

		public FrozeninTimePoseEnum FrozeninTimePose
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return (FrozeninTimePoseEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("FrozeninTimePose");
			}
		}

		public WActorReferenceProperty PathProperty { get; set; } = new WActorReferenceProperty("Path_v2", "Path", "tn", "Path", SourceScene.Room);

		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0xFF) { return null; }
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				if (value_as_int >= list.Count) { return null; }
				return list[value_as_int];
			}

			set
			{
				WDOMNode cur_object = this;
				while (cur_object.Parent != null)
				{
					cur_object = cur_object.Parent;
				}
				List<Path_v2> list = cur_object.GetChildrenOfType<Path_v2>();
				int value_as_int = list.IndexOf(value);
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Path");
			}
		}

		public WIntProperty EnableSpawnSwitchProperty { get; set; } = new WIntProperty("int", "EnableSpawnSwitch", "tn", "Enable Spawn Switch", 0);

		public int EnableSpawnSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("EnableSpawnSwitch");
			}
		}

		public enum ExtraEquipmentEnum
		{
			Normal = 0,
			Helmet = 1,
			Shield = 2,
			Helmet_and_shield = 3,
			Shield_and_cape = 4,
			Helmet_shield_and_cape = 5,
			Helmet_shield_and_cape_B = 7,
		}

					public WProperty_new ExtraEquipmentProperty { get; set; } = new WProperty_new("ExtraEquipmentEnum", "ExtraEquipment", "tn", "Extra Equipment");

		public ExtraEquipmentEnum ExtraEquipment
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00E0) >> 5);
				return (ExtraEquipmentEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00E0 | (value_as_int << 5 & 0x00E0));
				OnPropertyChanged("ExtraEquipment");
			}
		}

		public WIntProperty DisableSpawnonDeathSwitchProperty { get; set; } = new WIntProperty("int", "DisableSpawnonDeathSwitch", "tn", "Disable Spawn on Death Switch", 0);

		public int DisableSpawnonDeathSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("DisableSpawnonDeathSwitch");
			}
		}

		// Constructor
		public tn(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class toge : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "toge", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "toge", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public toge(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tori_flag : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public tori_flag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tornado : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tornado", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFFFFFF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFFFFFFFF | (value_as_int << 0 & 0xFFFFFFFF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public tornado(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tpota : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public tpota(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tsubo : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "tsubo", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "tsubo", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00003F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00003F00 | (value_as_int << 8 & 0x00003F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "tsubo", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000C000) >> 14);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000C000 | (value_as_int << 14 & 0x0000C000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "tsubo", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x007F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x007F0000 | (value_as_int << 16 & 0x007F0000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "tsubo", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "tsubo", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x70000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x70000000 | (value_as_int << 28 & 0x70000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "tsubo", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_7");
			}
		}

		// Constructor
		public tsubo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class wall : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "wall", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "wall", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public wall(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpdm20 : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "warpdm20", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public warpdm20(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpf : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "warpf", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xF0000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xF0000000 | (value_as_int << 28 & 0xF0000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public warpf(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpfout : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public warpfout(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpgn : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "warpgn", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "warpgn", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public warpgn(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warphr : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "warphr", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xF0000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xF0000000 | (value_as_int << 28 & 0xF0000000));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public warphr(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpls : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "warpls", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "warpls", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "warpls", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "warpls", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xF0000000) >> 28);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xF0000000 | (value_as_int << 28 & 0xF0000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		// Constructor
		public warpls(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpmj : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "warpmj", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public warpmj(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class waterfall : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "waterfall", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "waterfall", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public waterfall(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class wbird : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public wbird(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class windmill : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "windmill", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public windmill(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class wind_tag : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "wind_tag", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "wind_tag", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "wind_tag", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x001F0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x001F0000 | (value_as_int << 16 & 0x001F0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "wind_tag", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00600000) >> 21);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00600000 | (value_as_int << 21 & 0x00600000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "wind_tag", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00800000) >> 23);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00800000 | (value_as_int << 23 & 0x00800000));
				OnPropertyChanged("Unknown_5");
			}
		}

		public WIntProperty Unknown_6Property { get; set; } = new WIntProperty("int", "Unknown_6", "wind_tag", "Unknown_6", 0);

		public int Unknown_6
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_6");
			}
		}

		public WIntProperty Unknown_7Property { get; set; } = new WIntProperty("int", "Unknown_7", "wind_tag", "Unknown_7", 0);

		public int Unknown_7
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x000F | (value_as_int << 0 & 0x000F));
				OnPropertyChanged("Unknown_7");
			}
		}

		public WIntProperty Unknown_8Property { get; set; } = new WIntProperty("int", "Unknown_8", "wind_tag", "Unknown_8", 0);

		public int Unknown_8
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00F0) >> 4);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00F0 | (value_as_int << 4 & 0x00F0));
				OnPropertyChanged("Unknown_8");
			}
		}

		// Constructor
		public wind_tag(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class wz : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "wz", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "wz", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		public WIntProperty Unknown_3Property { get; set; } = new WIntProperty("int", "Unknown_3", "wz", "Unknown_3", 0);

		public int Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("Unknown_3");
			}
		}

		public WIntProperty Unknown_4Property { get; set; } = new WIntProperty("int", "Unknown_4", "wz", "Unknown_4", 0);

		public int Unknown_4
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("Unknown_4");
			}
		}

		public WIntProperty Unknown_5Property { get; set; } = new WIntProperty("int", "Unknown_5", "wz", "Unknown_5", 0);

		public int Unknown_5
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Unknown_5");
			}
		}

		// Constructor
		public wz(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ygcwp : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ygcwp", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public ygcwp(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ykgr : Actor
	{
		// Auto-Generated Properties from Templates
		public WIntProperty Unknown_1Property { get; set; } = new WIntProperty("int", "Unknown_1", "ykgr", "Unknown_1", 0);

		public int Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_1");
			}
		}

		public WIntProperty Unknown_2Property { get; set; } = new WIntProperty("int", "Unknown_2", "ykgr", "Unknown_2", 0);

		public int Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00F00000) >> 20);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_Parameters = (int)(m_Parameters & ~0x00F00000 | (value_as_int << 20 & 0x00F00000));
				OnPropertyChanged("Unknown_2");
			}
		}

		// Constructor
		public ykgr(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class yougan : Actor
	{
		// Auto-Generated Properties from Templates
		// Constructor
		public yougan(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}
	}


} // namespace WindEditor
