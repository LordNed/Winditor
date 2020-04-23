
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
		
		[WProperty("acorn_leaf", "Unknown_1", true, "", SourceScene.Room)]
		public int Unknown_1
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
				OnPropertyChanged("Unknown_1");
			}
		}

		// Constructor
		public acorn_leaf(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class agbsw0 : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("agbsw0", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("agbsw0", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("agbsw0", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("agbsw0", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("agbsw0", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("agbsw0", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class alldie : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("alldie", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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


		[WProperty("Armos Knight", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		[WProperty("Armos Knight", "Guarded Area Radius (Hundreds)", true, "", SourceScene.Room)]
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

		[WProperty("Armos Knight", "Switch Activates Armos Knight?", true, "If this is checked, the switch below is for activating the Armos Knight, rather than for disabling the Armos Knight's Spawn.", SourceScene.Room)]
		public bool SwitchActivatesArmosKnight
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("SwitchActivatesArmosKnight");
			}
		}

		[WProperty("Armos Knight", "Disable Spawn Switch", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			GuardedAreaRadiusHundreds = -1;
			DisableSpawnSwitch = -1;
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

		[WProperty("Armos", "Sight Range (Hundreds)", true, "This number multiplied by 100 is the range it can see Link within.", SourceScene.Room)]
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

		[WProperty("Armos", "Switch Activates Armos?", true, "If this is checked, the switch below is for activating the Armos Knight, rather than for disabling the Armos's Spawn.", SourceScene.Room)]
		public bool SwitchActivatesArmos
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("SwitchActivatesArmos");
			}
		}

		[WProperty("Armos", "Disable Spawn Switch", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unused_1 = -1;
			SightRangeHundreds = -1;
			DisableSpawnSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class amiprop : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("amiprop", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("amiprop", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class andsw0 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("andsw0", "Num Switches to Check", true, "How many switches to check.", SourceScene.Room)]
		public int NumSwitchestoCheck
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
				OnPropertyChanged("NumSwitchestoCheck");
			}
		}
		public enum TypeEnum
		{
			Normal = 0,
			Check_for_unset_after_set = 1,
			Time_limit_check = 2,
			Normal_with_65_frames_delayed_set = 3,
		}


		[WProperty("andsw0", "Type", true, "'Normal' just checks all the switches it should and then sets 'Switch to Set'.\n'Check for unset after set' does the same, but if any of the switches to check are ever unset, 'Switch to Set' will also be unset.\n'Time limit check' starts a time limit once the first switch to check has been set, you must set the other ones within it.\n'Normal with 65 frames delayed set' waits 65 frames after the last switch to check is set before setting 'Switch to Set'.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("andsw0", "First Switch to Check", true, "The first switch index to check. If this is 0, use (Switch to Set + 1) as the first switch to check instead.\nThe other switches it checks are sequential after the first one. For example, if the first switch is 5, and 'Num Switches to Check' is 3, it will check switches 5, 6, and 7.", SourceScene.Room)]
		public int FirstSwitchtoCheck
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
				OnPropertyChanged("FirstSwitchtoCheck");
			}
		}

		[WProperty("andsw0", "Switch to Set", true, "The switch to be set once all the switches to check have been set.", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("andsw0", "Event", true, "The event to start once all the switches to check have been set.", SourceScene.Stage)]
		public MapEvent Event
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
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
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Event");
			}
		}

		[WProperty("andsw0", "Time Limit (Half Seconds)", true, "For the 'Time limit check' type, this number times 15 frames is how long the player has to set all the switches after the first switch is set.", SourceScene.Room)]
		public int TimeLimitHalfSeconds
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
				OnPropertyChanged("TimeLimitHalfSeconds");
			}
		}

		// Constructor
		public andsw0(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			NumSwitchestoCheck = -1;
			FirstSwitchtoCheck = -1;
			SwitchtoSet = -1;
			Event = null;
			TimeLimitHalfSeconds = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class andsw2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("andsw2", "Num Switches to Check", true, "", SourceScene.Room)]
		public int NumSwitchestoCheck
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
				OnPropertyChanged("NumSwitchestoCheck");
			}
		}
		public enum TypeEnum
		{
			Unknown_0 = 0,
			Unknown_1 = 1,
		}


		[WProperty("andsw2", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("andsw2", "Switch to Set", true, "The switch to be set once all the switches to check have been set.", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("andsw2", "First Switch to Check", true, "The first switch index to check. The other switches it checks are after this one sequentially.\nFor example, if this is 5 and 'Num Switches to Check' is 3, it will check switches 5, 6, and 7.", SourceScene.Room)]
		public int FirstSwitchtoCheck
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
				OnPropertyChanged("FirstSwitchtoCheck");
			}
		}

		[WProperty("andsw2", "Event", true, "", SourceScene.Stage)]
		public MapEvent Event
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00FF) >> 0);
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
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("Event");
			}
		}

		[WProperty("andsw2", "Time Limit (Half Seconds)", true, "", SourceScene.Room)]
		public int TimeLimitHalfSeconds
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
				OnPropertyChanged("TimeLimitHalfSeconds");
			}
		}

		// Constructor
		public andsw2(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			NumSwitchestoCheck = -1;
			SwitchtoSet = -1;
			FirstSwitchtoCheck = -1;
			Event = null;
			TimeLimitHalfSeconds = -1;
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
		
		[WProperty("arrow_lighteff", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class atdoor : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("atdoor", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class att : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("att", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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


		[WProperty("Kargaroc", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		[WProperty("Kargaroc", "Sight Range (Hundreds)", true, "", SourceScene.Room)]
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

		[WProperty("Kargaroc", "Path", true, "", SourceScene.Room)]
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

		[WProperty("Kargaroc", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("Kargaroc", "Disable Spawn Switch", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeHundreds = -1;
			Path = null;
			EnableSpawnSwitch = -1;
			DisableSpawnSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bdk : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("bdk", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bdkobj : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("bdkobj", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("bdkobj", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class beam : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("beam", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("beam", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("beam", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("beam", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("beam", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("beam", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("beam", "Unknown_7", true, "", SourceScene.Room)]
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

		[WProperty("beam", "Unknown_8", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			Unknown_7 = -1;
			Unknown_8 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bflower : Actor
	{
		// Auto-Generated Properties from Templates
				public enum FlowerTypeEnum
		{
			Ripe = 0,
			Withered = 1,
		}


		[WProperty("Bomb Flower", "Flower Type", true, "", SourceScene.Room)]
		public FlowerTypeEnum FlowerType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				if (!Enum.IsDefined(typeof(FlowerTypeEnum), value_as_int))
					value_as_int = 0;
				return (FlowerTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("FlowerType");
				UpdateModel();
			}
		}

		[WProperty("Bomb Flower", "Watered Switch", true, "Withered bomb flowers will set this switch when you pour water on them. If the switch is still set when the room reloads, the withered bomb flower will still be ripe then.\nNo effect on bomb flowers that are ripe to begin with.", SourceScene.Room)]
		public int WateredSwitch
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
				OnPropertyChanged("WateredSwitch");
			}
		}

		// Constructor
		public bflower(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			WateredSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bgn : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("bgn", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("bigelf", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("bigelf", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("bigelf", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bita : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("bita", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("bita", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bk : Actor
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Wandering = 0,
			Unknown_1 = 1,
			Unknown_2 = 2,
			Hiding_in_a_Pot = 3,
			Guarding = 4,
			Being_carried_by_a_Kargaroc = 5,
			Search_Light_Operator = 6,
			Jumping = 7,
			Guarding_and_Yawning = 10,
			Pink_Bokoblin_with_Telescope = 11,
			Debug = 15,
		}


		[WProperty("Bokoblin", "Type", true, "The behavior of the Bokoblin when it spawns.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("Bokoblin", "Switch Spawns Bokoblin", true, "If this is set, the switch ID below is for spawning the Bokoblin, rather than a switch to set when it's killed.", SourceScene.Room)]
		public bool SwitchSpawnsBokoblin
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000010) >> 4);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00000010 | (value_as_int << 4 & 0x00000010));
				OnPropertyChanged("SwitchSpawnsBokoblin");
			}
		}

		[WProperty("Bokoblin", "Is Green", true, "If this is set, the Bokoblin is green. However, this is overriden by the Pink Bokoblin with Telescope type.", SourceScene.Room)]
		public bool IsGreen
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000020) >> 5);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00000020 | (value_as_int << 5 & 0x00000020));
				OnPropertyChanged("IsGreen");
			}
		}
		public enum WeaponEnum
		{
			Unlit_Torch = 0,
			Machete_1 = 1,
			Lit_Torch = 2,
			Machete_2 = 3,
		}


		[WProperty("Bokoblin", "Weapon", true, "The weapon that the Bokoblin is holding when it spawns.", SourceScene.Room)]
		public WeaponEnum Weapon
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000C0) >> 6);
				if (!Enum.IsDefined(typeof(WeaponEnum), value_as_int))
					value_as_int = 0;
				return (WeaponEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000C0 | (value_as_int << 6 & 0x000000C0));
				OnPropertyChanged("Weapon");
			}
		}

		[WProperty("Bokoblin", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("Bokoblin", "Path", true, "The path that the Bokoblin follows.", SourceScene.Room)]
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

		[WProperty("Bokoblin", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("Bokoblin", "Disable Spawn on Death Switch", true, "", SourceScene.Room)]
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
		public bk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_5 = -1;
			Path = null;
			EnableSpawnSwitch = -1;
			DisableSpawnonDeathSwitch = -1;
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


		[WProperty("bl", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("bl", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("bl", "Path", true, "", SourceScene.Room)]
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

		[WProperty("bl", "Float at Initial Height?", true, "If this is not checked, the Bubble will float near the ground below it instead of in the air where it is actually placed.", SourceScene.Room)]
		public bool FloatatInitialHeight
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFF000000) >> 24);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("FloatatInitialHeight");
			}
		}

		// Constructor
		public bl(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			EnableSpawnSwitch = -1;
			Path = null;
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
		
		[WProperty("bmdfoot", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bmdhand : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("bmdhand", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("bmdhand", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("bmdhand", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
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


		[WProperty("bo", "Type", true, "Use the 'Normal' type. The two dying types are spawned by it automatically when it dies.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("bo", "Leave Behind Baba Bud?", true, "", SourceScene.Room)]
		public bool LeaveBehindBabaBud
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
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
		
		[WProperty("boko", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("boko", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("boko", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class boss_item : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("boss_item", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class bpw : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("bpw", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("bpw", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("bpw", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
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
				public int TypeBitfield
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
				OnPropertyChanged("TypeBitfield");
				UpdateModel();
			}
		}

		[WProperty("Plank Bridge", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("Plank Bridge", "Path", true, "The path for the bridge to be stretched along.", SourceScene.Room)]
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

		// Constructor
		public bridge(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			TypeBitfield = -1;
			Unknown_2 = -1;
			Path = null;
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			ComponentType = -1;
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
		
		[WProperty("bwd", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("bwds", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class canon : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("canon", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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


		[WProperty("cc", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
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


		[WProperty("cc", "Color Type", true, "The 'attacks instantly' types are used by Wizzrobes. The 'attacks instantly and more vulnerabilities' type can die from Boomerang/Grappling Hook/Hookshot for some reason, but this type is unused.", SourceScene.Room)]
		public ColorTypeEnum ColorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(ColorTypeEnum), value_as_int))
					value_as_int = 0;
				return (ColorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("ColorType");
				UpdateModel();
			}
		}

		[WProperty("cc", "Sight Range (Tens)", true, "", SourceScene.Room)]
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

		[WProperty("cc", "Disable Spawn Switch", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeTens = -1;
			DisableSpawnSwitch = -1;
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
		
		[WProperty("dai", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class daiocta : Actor
	{
		// Auto-Generated Properties from Templates
				public enum NumberofEyesEnum
		{
			Four_eyes = 0,
			Eight_eyes = 1,
			Twelve_eyes = 2,
		}


		[WProperty("Big Octo", "Number of Eyes", true, "", SourceScene.Room)]
		public NumberofEyesEnum NumberofEyes
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(NumberofEyesEnum), value_as_int))
					value_as_int = 0;
				return (NumberofEyesEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("NumberofEyes");
			}
		}

		[WProperty("Big Octo", "Sight Range (Hundreds)", true, "The range within it will see the player and start the fight (not counting vertical distance).\nIf this is 255, it will default to 50 (5000 units) instead.\nIf this is less than 50, it will default to 50 (5000 units) instead.", SourceScene.Room)]
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

		[WProperty("Big Octo", "Post-Loss Spawn ID", true, "If the player loses the fight against the Big Octo, this spawn ID will be where in this room the player respawns from.\nIf this is 255, the normal behavior for respawning the player is used instead.", SourceScene.Room)]
		public int PostLossSpawnID
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
				OnPropertyChanged("PostLossSpawnID");
			}
		}

		[WProperty("Big Octo", "Disable Spawn on Death Switch", true, "", SourceScene.Room)]
		public int DisableSpawnonDeathSwitch
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
				OnPropertyChanged("DisableSpawnonDeathSwitch");
			}
		}

		[WProperty("Big Octo", "Death Event Waits for Great Fairy", true, "If this is checked, the cutscene played after the Big Octo is defeated will not end normally, and will instead wait for the Great Fairy's cutscene to start.\nIf there is no Great Fairy set up properly here, the game will softlock with no way to end the cutscene.", SourceScene.Room)]
		public bool DeathEventWaitsforGreatFairy
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				if (value_as_int == 1) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 0 : 1;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("DeathEventWaitsforGreatFairy");
			}
		}

		// Constructor
		public daiocta(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeHundreds = -1;
			PostLossSpawnID = -1;
			DisableSpawnonDeathSwitch = -1;
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
		
		[WProperty("Deku Leaf Pickup", "Item Pickup Flag", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			ItemPickupFlag = -1;
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
		
		[WProperty("demo_item", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("demo_item", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("dk", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class door10 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Door", "Switch 1", true, "For normal-type doors, this is the switch it will check and unlock itself once it's set.\nFor barred-type doors, this is the switch it will set when all enemies are dead.", SourceScene.Room)]
		public int Switch1
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
				OnPropertyChanged("Switch1");
				UpdateModel();
			}
		}
		public enum TypeEnum
		{
			Normal = 0,
			Boss = 1,
			Barred_until_all_enemies_dead = 2,
			Unknown = 3,
			Locked = 4,
			Locked_and_barred = 5,
		}


		[WProperty("Door", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("Door", "Event ID", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Switch 2", true, "", SourceScene.Room)]
		public int Switch2
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
				OnPropertyChanged("Switch2");
			}
		}

		[WProperty("Door", "From Room Number", true, "", SourceScene.Room)]
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

		[WProperty("Door", "To Room Number", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Ship ID", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Arg 1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Switch1 = -1;
			EventID = -1;
			Switch2 = -1;
			FromRoomNumber = -1;
			ToRoomNumber = -1;
			ShipID = -1;
			Arg1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class door12 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Door", "Switch 1", true, "For normal-type doors, this is the switch it will check and unlock itself once it's set.\nFor barred-type doors, this is the switch it will set when all enemies are dead.", SourceScene.Room)]
		public int Switch1
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
				OnPropertyChanged("Switch1");
			}
		}
		public enum TypeEnum
		{
			Normal = 0,
			Boss = 1,
			Barred_until_all_enemies_dead = 2,
			Unknown_3 = 3,
			Locked = 4,
		}


		[WProperty("Door", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000F00) >> 8);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000F00 | (value_as_int << 8 & 0x00000F00));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("Door", "Event ID", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Switch 2", true, "", SourceScene.Room)]
		public int Switch2
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
				OnPropertyChanged("Switch2");
			}
		}

		[WProperty("Door", "From Room Number", true, "", SourceScene.Room)]
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

		[WProperty("Door", "To Room Number", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Ship ID", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Arg 1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Switch1 = -1;
			EventID = -1;
			Switch2 = -1;
			FromRoomNumber = -1;
			ToRoomNumber = -1;
			ShipID = -1;
			Arg1 = -1;
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
	public partial class ep : TriggerRegion
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Has_brazier_1 = 0,
			Does_not_have_brazier_1 = 1,
			Does_not_have_brazier_2 = 2,
			Has_brazier_2 = 3,
		}


		[WProperty("Torch", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("Torch", "Has Fireflies?", true, "", SourceScene.Room)]
		public bool HasFireflies
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000040) >> 6);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00000040 | (value_as_int << 6 & 0x00000040));
				OnPropertyChanged("HasFireflies");
			}
		}

		[WProperty("Torch", "Is Wooden?", true, "If this is true and the Torch Type is brazier, the brazier is wooden instead of gold.", SourceScene.Room)]
		public bool IsWooden
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000080) >> 7);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00000080 | (value_as_int << 7 & 0x00000080));
				OnPropertyChanged("IsWooden");
				UpdateModel();
			}
		}

		[WProperty("Torch", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("Torch", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("Torch", "On Switch", true, "The ID of the switch that, when set, lights the torch. If -1, the torch is lit by default.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_4 = -1;
			Unknown_5 = -1;
			OnSwitch = -1;
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
		
		[WProperty("fallrock_tag", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fan : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("fan", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("fan", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ff : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("ff", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("ff", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("ff", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fganon : Actor
	{
		// Auto-Generated Properties from Templates
				public enum BehaviorTypeEnum
		{
			Forsaken_Fortress_Encounter = 0,
			Maze_Encounter = 1,
			After_Light_Arrows_Encounter = 2,
			Clone = 3,
			Maze_Encounter_Immortal_for_Testing = 15,
		}


		[WProperty("Phantom Ganon", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("BehaviorType");
			}
		}

		[WProperty("Phantom Ganon", "Which Clone", true, "1-4 for which of the clones this is. No effect on anything besides the 'Clone' behavior type.", SourceScene.Room)]
		public int WhichClone
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
				OnPropertyChanged("WhichClone");
			}
		}

		[WProperty("Phantom Ganon", "Sight Range (Tens)", true, "The fight won't start until the player is within this number times ten units of where Phantom Ganon was placed.\nNote that this only affects the range on the X/Z plane. Phantom Ganon does not check difference in height between himself and the player, and will trigger the fight even if the player is very far above or below him.", SourceScene.Room)]
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

		[WProperty("Phantom Ganon", "Disable Spawn on Death Switch", true, "Only works for the 'Forsaken Fortress' and 'After Light Arrows' behavior types.", SourceScene.Room)]
		public int DisableSpawnonDeathSwitch
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
				OnPropertyChanged("DisableSpawnonDeathSwitch");
			}
		}

		[WProperty("Phantom Ganon", "Fight in Progress Switch", true, "He will set this switch when the fight starts and unset it when he's defeated.", SourceScene.Room)]
		public int FightinProgressSwitch
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
				OnPropertyChanged("FightinProgressSwitch");
			}
		}

		// Constructor
		public fganon(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			WhichClone = -1;
			SightRangeTens = -1;
			DisableSpawnonDeathSwitch = -1;
			FightinProgressSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fgmahou : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("fgmahou", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fire : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("fire", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("fire", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("fire", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("fire", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("fire", "Unknown_5", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class floor : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("floor", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class fm : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("fm", "Link Captured Exit", true, "Which exit this Floormaster takes Link through when it captures him.", SourceScene.Room)]
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


		[WProperty("fm", "Type", true, "The unused Stalker type follows Link without coming out of the floor. That type only appears when its Enable Spawn Switch is set while in the same room as the Floormaster.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000300) >> 8);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
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


		[WProperty("fm", "Targeting Behavior Type", true, "", SourceScene.Room)]
		public TargetingBehaviorTypeEnum TargetingBehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000C00) >> 10);
				if (!Enum.IsDefined(typeof(TargetingBehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (TargetingBehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000C00 | (value_as_int << 10 & 0x00000C00));
				OnPropertyChanged("TargetingBehaviorType");
			}
		}

		[WProperty("fm", "Path", true, "Only the \"Follows path\" type uses this path.", SourceScene.Room)]
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

		[WProperty("fm", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("fm", "Disable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("fm", "Partner Captured Exit", true, "Which stage exit this Floormaster takes Medli/Makar through when it captures them.", SourceScene.Stage)]
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

		[WProperty("fm", "Sight Range (Hundreds)", true, "Defaults to a range of 3000 if you set this to 0 or 255.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			LinkCapturedExit = null;
			Path = null;
			EnableSpawnSwitch = -1;
			DisableSpawnSwitch = -1;
			PartnerCapturedExit = null;
			SightRangeHundreds = -1;
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
		
		[WProperty("ghostship", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("ghostship", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class gm : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("gm", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("gm", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("gm", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("gm", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("gm", "Unknown_5", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class gnd : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("gnd", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("gnd", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class goal_flag : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("goal_flag", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("goal_flag", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class grass : Actor
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Grass = 0,
			Tree = 1,
			White_Flower = 2,
			Pink_Flower = 3,
		}


		[WProperty("Foliage", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000030) >> 4);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000030 | (value_as_int << 4 & 0x00000030));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}
		public enum SpawnPatternEnum
		{
			Single = 0,
			_7_Grass_Bunches = 1,
			_21_Grass_Bunches = 2,
			_3_Trees = 3,
			_7_White_Flowers = 4,
			_17_White_Flowers = 5,
			_7_Pink_Flowers = 6,
			_5_Trees = 7,
		}


		[WProperty("Foliage", "Spawn Pattern", true, "", SourceScene.Room)]
		public SpawnPatternEnum SpawnPattern
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(SpawnPatternEnum), value_as_int))
					value_as_int = 0;
				return (SpawnPatternEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("SpawnPattern");
			}
		}

		[WProperty("Grass and Flowers", "Dropped Item", true, "", SourceScene.Room)]
		public OnlyRandomDroppedItemID DroppedItem
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000FC0) >> 6);
				if (!Enum.IsDefined(typeof(OnlyRandomDroppedItemID), value_as_int))
					value_as_int = 0;
				return (OnlyRandomDroppedItemID)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000FC0 | (value_as_int << 6 & 0x00000FC0));
				OnPropertyChanged("DroppedItem");
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


		[WProperty("gy_ctrl", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("gy_ctrl", "Number of Gyorgs", true, "How many Gyorgs to spawn. 15 defaults to 1 Gyorg.", SourceScene.Room)]
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

		[WProperty("gy_ctrl", "Sight Range (Thousands)", true, "This number multiplied by 1000 is the range it can see KoRL within and will start spawning Gyorgs.", SourceScene.Room)]
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

		[WProperty("gy_ctrl", "Enable Spawn Switch", true, "If this switch is valid, the spawner will not start spawning Gyorgs until the switch is set.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			NumberofGyorgs = -1;
			SightRangeThousands = -1;
			EnableSpawnSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class himo3 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("himo3", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("himo3", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("himo3", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("himo3", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("himo3", "Unknown_5", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hitobj : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("hitobj", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hmlif : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("hmlif", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("hmlif", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("hmlif", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("hmlif", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("hmlif", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("hmlif", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("hmlif", "Unknown_7", true, "", SourceScene.Room)]
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

		[WProperty("hmlif", "Unknown_8", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			Unknown_7 = -1;
			Unknown_8 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hot_floor : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("hot_floor", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("hot_floor", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class hys : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("hys", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("hys", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class icelift : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("icelift", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("icelift", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("icelift", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ikari : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("ikari", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("ikari", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class item : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Item Pickup", "Item ID", true, "", SourceScene.Room)]
		public ItemID ItemID
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(ItemID), value_as_int))
					value_as_int = 0;
				return (ItemID)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("ItemID");
				UpdateModel();
			}
		}

		[WProperty("Item Pickup", "Item Pickup Flag", true, "", SourceScene.Room)]
		public int ItemPickupFlag
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
				OnPropertyChanged("ItemPickupFlag");
			}
		}

		[WProperty("Item Pickup", "Enable Spawn Switch", true, "", SourceScene.Room)]
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
		public enum BehaviorTypeEnum
		{
			Fade_away = 0,
			Float_in_place_and_do_not_fade_away = 1,
			Unknown_2 = 2,
			Do_not_fade_away = 3,
		}


		[WProperty("Item Pickup", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x03000000) >> 24);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x03000000 | (value_as_int << 24 & 0x03000000));
				OnPropertyChanged("BehaviorType");
			}
		}
		public enum ItemActionEnum
		{
			Normal = 0,
			Unknown_1 = 1,
			Unknown_2 = 2,
			Unknown_3 = 3,
			Unknown_4 = 4,
			Make_a_ding_sound = 5,
			Unknown_6 = 6,
			Unknown_7 = 7,
			Unknown_8 = 8,
			Unknown_9 = 9,
			Unknown_10 = 10,
			Unknown_11 = 11,
			Unknown_12 = 12,
		}


		[WProperty("Item Pickup", "Item Action", true, "", SourceScene.Room)]
		public ItemActionEnum ItemAction
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFC000000) >> 26);
				if (!Enum.IsDefined(typeof(ItemActionEnum), value_as_int))
					value_as_int = 0;
				return (ItemActionEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0xFC000000 | (value_as_int << 26 & 0xFC000000));
				OnPropertyChanged("ItemAction");
			}
		}

		[WProperty("Item Pickup", "Enable Activation Switch", true, "The player will not be able to pick this item up until this switch is set.\nIt will still be visible and animated, but touching it will not give the item.\nAlso note that the item will not begin to fade away before this switch is set either.", SourceScene.Room)]
		public int EnableActivationSwitch
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
				OnPropertyChanged("EnableActivationSwitch");
			}
		}

		// Constructor
		public item(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			ItemPickupFlag = -1;
			EnableSpawnSwitch = -1;
			EnableActivationSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class jbo : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("jbo", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("kamome", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kamome", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("kamome", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("kamome", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kanban : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kanban", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			MessageID = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kantera : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kantera", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kantera", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("kantera", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kb : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kb", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kb", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("kb", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("kb", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("kb", "Unknown_5", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kddoor : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Door", "Switch Bit", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Type", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Event ID", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Switch Bit 2", true, "", SourceScene.Room)]
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

		[WProperty("Door", "From Room Number", true, "", SourceScene.Room)]
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

		[WProperty("Door", "To Room Number", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Ship ID", true, "", SourceScene.Room)]
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

		[WProperty("Door", "Arg 1", true, "", SourceScene.Room)]
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
		public kddoor(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchBit = -1;
			Type = -1;
			EventID = -1;
			SwitchBit2 = -1;
			FromRoomNumber = -1;
			ToRoomNumber = -1;
			ShipID = -1;
			Arg1 = -1;
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


		[WProperty("ki", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
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
		}


		[WProperty("ki", "Sight Range", true, "", SourceScene.Room)]
		public SightRangeEnum SightRange
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00007F00) >> 8);
				if (!Enum.IsDefined(typeof(SightRangeEnum), value_as_int))
					value_as_int = 3;
				return (SightRangeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00007F00 | (value_as_int << 8 & 0x00007F00));
				OnPropertyChanged("SightRange");
			}
		}

		[WProperty("ki", "Is Fire Keese?", true, "", SourceScene.Room)]
		public bool IsFireKeese
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00008000) >> 15);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00008000 | (value_as_int << 15 & 0x00008000));
				OnPropertyChanged("IsFireKeese");
			}
		}

		[WProperty("ki", "Path", true, "", SourceScene.Room)]
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

		[WProperty("ki", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Path = null;
			EnableSpawnSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kita : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kita", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kita", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class klft : Actor
	{
		// Auto-Generated Properties from Templates
				public enum Unknown_1Enum
		{
			Unknown_0 = 0,
			Unknown_1 = 1,
			Unknown_2 = 2,
			Unknown_3 = 3,
		}


		[WProperty("klft", "Unknown_1", true, "", SourceScene.Room)]
		public Unknown_1Enum Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(Unknown_1Enum), value_as_int))
					value_as_int = 0;
				return (Unknown_1Enum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}

		[WProperty("klft", "Path", true, "", SourceScene.Room)]
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

		[WProperty("klft", "At Point 1 Switch", true, "The lift will use this switch to remember which end of the pulley rope it was last left on.\nIt will start at point 0. When the player leaves the room, it will set this switch if it's at point 1, or unset it if it's at point 0.\nIf this switch is 255 or 0, it will be ignored and not set/unset, the lift will always start at point 0.", SourceScene.Room)]
		public int AtPoint1Switch
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
				OnPropertyChanged("AtPoint1Switch");
			}
		}

		// Constructor
		public klft(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Path = null;
			AtPoint1Switch = -1;
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
		
		[WProperty("kn", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kn", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class knob00 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("knob00", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kokiie : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kokiie", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kokiie", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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


		[WProperty("ks", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		[WProperty("ks", "Number in Group", true, "The number of Morths in this group. If you set this to 0 or higher than 21 it will just be a single Morth instead.", SourceScene.Room)]
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

		[WProperty("ks", "Pot Sight Range (Tens)", true, "This number multiplied by 10 is the range around the pot the Morths are in that they'll notice the player and break out of the pot.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			NumberinGroup = -1;
			PotSightRangeTens = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kt : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kt", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kui : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kui", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kui", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("kui", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("kui", "Unknown_4", true, "", SourceScene.Room)]
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
		public kui(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag00 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kytag00", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kytag00", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("kytag00", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("kytag00", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("kytag00", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("kytag00", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("kytag00", "Unknown_7", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			Unknown_7 = -1;
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
		
		[WProperty("Wind Direction Setter", "Path", true, "", SourceScene.Room)]
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

		// Constructor
		public kytag02(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Path = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag03 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kytag03", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag04 : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kytag04", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("kytag04", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("kytag04", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class kytag05 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("kytag05", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("kytag07", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class lamp : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("lamp", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class lbridge : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("lbridge", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class leaflift : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("leaflift", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("lstair", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("machine", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class magma : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Magma", "Bubbles Path", true, "The magma's bubbles will appear on this path's points.", SourceScene.Room)]
		public Path_v2 BubblesPath
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xFFFFFFFF) >> 0);
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
				m_Parameters = (int)(m_Parameters & ~0xFFFFFFFF | (value_as_int << 0 & 0xFFFFFFFF));
				OnPropertyChanged("BubblesPath");
			}
		}

		// Constructor
		public magma(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			BubblesPath = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class majuu_flag : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("majuu_flag", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("majuu_flag", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mant : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("mant", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mbdoor : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("mbdoor", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("mbdoor", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("mbdoor", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mdoor : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("mdoor", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("mdoor", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("mdoor", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("mdoor", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mflft : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("mflft", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("mflft", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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


		[WProperty("mo2", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
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


		[WProperty("mo2", "Frozen in Time Pose", true, "", SourceScene.Room)]
		public FrozeninTimePoseEnum FrozeninTimePose
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(FrozeninTimePoseEnum), value_as_int))
					value_as_int = 0;
				return (FrozeninTimePoseEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("FrozeninTimePose");
			}
		}

		[WProperty("mo2", "Path", true, "", SourceScene.Room)]
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

		[WProperty("mo2", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("mo2", "Disable Spawn on Death Switch", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Path = null;
			EnableSpawnSwitch = -1;
			DisableSpawnonDeathSwitch = -1;
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
		
		[WProperty("mozo", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class msw : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("msw", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mt : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("mt", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("mt", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("mt", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("mt", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("mt", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("mt", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class mtoge : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("mtoge", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ac1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_ac1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_ac1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ah : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_ah", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_ah", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_aj1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_aj1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_aj1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("npc_ba1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bj1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_bj1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_bj1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bm1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_bm1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_bm1", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("npc_bm1", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bmcon1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_bmcon1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_bms1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_bms1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("npc_bs1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_bs1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("npc_btsw2", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_cb1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_cb1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_co1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_co1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_de1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_de1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ds1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_ds1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_gk1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_gk1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_gp1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_gp1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_gp1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_hi1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_hi1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("npc_hr", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_hr", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("npc_hr", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_jb1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_jb1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("npc_kamome", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_kamome", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kf1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_kf1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_kf1", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("npc_kf1", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("npc_kf1", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
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
		
		[WProperty("npc_kk1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_kk1", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("npc_kk1", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("npc_kk1", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_km1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_km1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ko1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_ko1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_ko1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_kp1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_kp1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ls1 : Actor
	{
		// Auto-Generated Properties from Templates
				public enum BehaviorTypeEnum
		{
			Looking_Out_of_Lookout = 0,
			Captured_in_FF1_on_Floor = 1,
			For_Cutscenes = 2,
			Telling_Link_to_See_Grandma = 3,
			Ending = 4,
			Unknown_255 = 255,
		}


		[WProperty("Aryll", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
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
		
		[WProperty("npc_md", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_md", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("npc_md", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_mk : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_mk", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_mk", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_mn : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_mn", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_mn", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("npc_mn", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
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
		
		[WProperty("npc_ob1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_ob1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_os : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_os", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_p1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_p1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_p1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_p2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_p2", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_p2", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("npc_p2", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_people : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Windfall Townsperson", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("Windfall Townsperson", "Path", true, "", SourceScene.Room)]
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

		[WProperty("Windfall Townsperson", "Unknown_3", true, "", SourceScene.Room)]
		public bool Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("Unknown_3");
			}
		}

		// Constructor
		public npc_people(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Path = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_pf1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_pf1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_pf1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_photo : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_photo", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_photo", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_pm1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_pm1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_roten : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_roten", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_roten", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_rsh1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_rsh1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_rsh1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("npc_tc", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_tc", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
				public enum BehaviorTypeEnum
		{
			Normal = 1,
			Hiding = 2,
		}


		[WProperty("npc_uk", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		[WProperty("npc_uk", "Path", true, "", SourceScene.Room)]
		public Path_v2 Path
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
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
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Path");
			}
		}
		public enum WhichKillerBeeEnum
		{
			Jin = 0,
			Jan = 1,
			JunRoberto = 2,
		}


		[WProperty("npc_uk", "Which Killer Bee", true, "", SourceScene.Room)]
		public WhichKillerBeeEnum WhichKillerBee
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				if (!Enum.IsDefined(typeof(WhichKillerBeeEnum), value_as_int))
					value_as_int = 0;
				return (WhichKillerBeeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("WhichKillerBee");
				UpdateModel();
			}
		}

		// Constructor
		public npc_uk(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Path = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_ym1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_ym1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_yw1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_yw1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("npc_yw1", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_zk1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_zk1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class npc_zl1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("npc_zl1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class nz : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("nz", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("nz", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("nz", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class nzg : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("nzg", "Sight Range (Fives)", true, "This number multiplied by 5 is the range around the hole it will notice the player and start spawning Rats and/or Bombchus.", SourceScene.Room)]
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

		[WProperty("nzg", "Maximum Number of Rats", true, "", SourceScene.Room)]
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


		[WProperty("nzg", "Type", true, "The type that spawns just Rats also has the Rat Shopkeeper in it.\nThe type that spawns both Rats and Bombchus has a random 50% chance of which to spawn each time it spawns one.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeFives = -1;
			MaximumNumberofRats = -1;
			Unused = -1;
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
		
		[WProperty("obj_ajav", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
	public partial class obj_akabe : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_akabe", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_akabe", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_akabe", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_akabe", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_apzl : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_apzl", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ashut : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_ashut", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_ashut", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_auzu : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_auzu", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_auzu", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_auzu", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_auzu", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_aygr : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_aygr", "Has Ladder", true, "", SourceScene.Room)]
		public bool HasLadder
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("HasLadder");
				UpdateModel();
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
		
		[WProperty("obj_balancelift", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_barrel : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_barrel", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_barrel", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_barrel2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_barrel2", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_barrel2", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_barrel2", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_barrel2", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("obj_barrel2", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("obj_barrel2", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_barrier : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_barrier", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_barrier", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_bemos : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_bemos", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_bemos", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_bemos", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_bemos", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("obj_bemos", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("obj_bemos", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_bscurtain : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_bscurtain", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_buoyflag : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_buoyflag", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_buoyflag", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_buoyflag", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_buoyrace : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_buoyrace", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_buoyrace", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("obj_canon", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_canon", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_canon", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_canon", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_coming : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_coming", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_coming", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_coming", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_coming", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("obj_coming", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("obj_coming", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_correct : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_correct", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_correct", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_correct", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_correct", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
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
		
		[WProperty("obj_doguu", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_doguu_demo : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_doguu_demo", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_dragonhead : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_dragonhead", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_drift : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_drift", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_ebomzo", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_eff : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_eff", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ekskz : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_ekskz", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_eskban : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_eskban", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ferris : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_ferris", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_figure : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_figure", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_firewall : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_firewall", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_flame : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_flame", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_flame", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_flame", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_flame", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("obj_flame", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("obj_flame", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ftree : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_ftree", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_gnnbtltaki", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_gryw00", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_hami2", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hami3 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hami3", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_hami3", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_hami3", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hami4 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hami4", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hat : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hat", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hbrf1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hbrf1", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_hbrf1", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_hbrf1", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hcbh : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hcbh", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_hcbh", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_hcbh", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
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
		
		[WProperty("obj_hha", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_hha", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hlift : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hlift", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_hlift", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_hlift", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_hlift", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hole : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hole", "Exit", true, "", SourceScene.Room)]
		public ExitData Exit
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
				OnPropertyChanged("Exit");
			}
		}

		[WProperty("obj_hole", "Has Visible Hole", true, "", SourceScene.Room)]
		public bool HasVisibleHole
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (value_as_int == 0) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("HasVisibleHole");
			}
		}

		[WProperty("obj_hole", "Scale", true, "If this is not 65535, this number divided by 10 is the scale of the hole.", SourceScene.Room)]
		public int Scale
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
				OnPropertyChanged("Scale");
			}
		}

		// Constructor
		public obj_hole(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Exit = null;
			Scale = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_homen : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_homen", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_homen", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_homen", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_homen", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("obj_homen", "Unknown_5", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_homensmoke : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_homensmoke", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_homensmoke", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_hsehi1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_hsehi1", "Switch to Set", true, "", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("obj_hsehi1", "Message", true, "", SourceScene.Room)]
		public int Message
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
				OnPropertyChanged("Message");
			}
		}

		// Constructor
		public obj_hsehi1(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchtoSet = -1;
			Message = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_htetu1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_htetu1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ice : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Chunk of Ice", "Melted Switch", true, "Switch to set once this chunk of ice has been melted.", SourceScene.Room)]
		public int MeltedSwitch
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
				OnPropertyChanged("MeltedSwitch");
			}
		}

		// Constructor
		public obj_ice(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			MeltedSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_iceisland : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_iceisland", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ikada : Actor
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Raft = 0,
			Beedles_Shop_Ship = 1,
			Submarine = 2,
			Beedles_Special_Shop_Ship = 3,
			Salvage_Corp_Ship = 4,
		}


		[WProperty("Various Ships", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("obj_ikada", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_ikada", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_ikada", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("obj_ikada", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("obj_ikada", "Path to Follow", true, "", SourceScene.Room)]
		public Path_v2 PathtoFollow
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0xFF00) >> 8);
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
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("PathtoFollow");
			}
		}

		[WProperty("Salvage Corp Ship", "Salvage Corp Path to Follow", true, "", SourceScene.Room)]
		public Path_v2 SalvageCorpPathtoFollow
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
				OnPropertyChanged("SalvageCorpPathtoFollow");
			}
		}

		// Constructor
		public obj_ikada(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			PathtoFollow = null;
			SalvageCorpPathtoFollow = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_Itnak : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_Itnak", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_Itnak", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_jump : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_jump", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_kanat : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_kanat", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_kanoke : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_kanoke", "Is Upright", true, "", SourceScene.Room)]
		public bool IsUpright
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("IsUpright");
				UpdateModel();
			}
		}

		[WProperty("obj_kanoke", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_kanoke", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_kanoke", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("obj_kanoke", "Unknown_5", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ladder : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_ladder", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_ladder", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_ladder", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_leaves : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_leaves", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_leaves", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_leaves", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
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
		
		[WProperty("obj_magmarock", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_magmarock", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_majyuu_door : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_majyuu_door", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mkie : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_mkie", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_mkie", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_mkie", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_mkie", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("obj_mkie", "Unknown_5", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mkiek : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_mkiek", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_mkiek", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mknjd : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_mknjd", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_mknjd", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("obj_monument", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_monument", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_movebox : Actor
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Breakable_Wooden_Crate = 0,
			Black_Box_A = 1,
			Black_Box_With_Statue_on_Top = 2,
			Big_Black_Box = 3,
			Unbreakable_Wooden_Crate_A = 4,
			Golden_Crate = 5,
			Metal_Box = 6,
			Metal_Box_With_Spring = 7,
			Unbreakable_Wooden_Crate_B = 8,
			Unbreakable_Wooden_Crate_C = 9,
			Mirror = 10,
			Black_Box_B = 11,
			Mossy_Black_Box = 12,
		}


		[WProperty("Movable Box", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("Movable Box", "Disable Flag on Top", true, "", SourceScene.Room)]
		public bool DisableFlagonTop
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("DisableFlagonTop");
			}
		}

		[WProperty("Movable Box", "Unknown_7", true, "For the \"Black Box With Statue on Top\" type, this will be passed as parameter Unknown_1 to the statue entity (obj_mkie) spawned on top of the box.", SourceScene.Room)]
		public int Unknown_7
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
				OnPropertyChanged("Unknown_7");
			}
		}

		[WProperty("Movable Box Item", "Dropped Item", true, "", SourceScene.Room)]
		public DroppedItemID DroppedItem
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				if (!Enum.IsDefined(typeof(DroppedItemID), value_as_int))
					value_as_int = 0;
				return (DroppedItemID)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("DroppedItem");
			}
		}

		[WProperty("Movable Box Item", "Dropped Item Pickup Flag", true, "", SourceScene.Room)]
		public int DroppedItemPickupFlag
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
				OnPropertyChanged("DroppedItemPickupFlag");
			}
		}

		[WProperty("Movable Box Stay Moved Options", "Do Not Stay Moved After Reload", true, "", SourceScene.Room)]
		public bool DoNotStayMovedAfterReload
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x40000000) >> 30);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x40000000 | (value_as_int << 30 & 0x40000000));
				OnPropertyChanged("DoNotStayMovedAfterReload");
			}
		}

		[WProperty("Movable Box Stay Moved Options", "Stay Moved to Path", true, "", SourceScene.Room)]
		public Path_v2 StayMovedtoPath
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
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
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("StayMovedtoPath");
			}
		}

		[WProperty("Movable Box Stay Moved Options", "Stay Moved Switch 1", true, "The switch to keep track of the box being moved to point 1 on its path.", SourceScene.Room)]
		public int StayMovedSwitch1
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
				OnPropertyChanged("StayMovedSwitch1");
			}
		}

		[WProperty("Movable Box Stay Moved Options", "Stay Moved Switch 2", true, "The switch to keep track of the box being moved to point 2 on its path.", SourceScene.Room)]
		public int StayMovedSwitch2
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
				OnPropertyChanged("StayMovedSwitch2");
			}
		}

		// Constructor
		public obj_movebox(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_7 = -1;
			DroppedItemPickupFlag = -1;
			StayMovedtoPath = null;
			StayMovedSwitch1 = -1;
			StayMovedSwitch2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_msdan", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_msdan", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_msdan", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_msdan", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_msdan2", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan_sub : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_msdan_sub", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_msdan_sub", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_msdan_sub", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_msdan_sub2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_msdan_sub2", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_msdan_sub2", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mshokki : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_mshokki", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_mtest : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_mtest", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_mtest", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_mtest", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_mtest", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
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
		
		[WProperty("obj_ohatch", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_ospbox", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_ospbox", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_otble : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_otble", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			MessageID = -1;
			Type = -1;
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
		
		[WProperty("Pirate Ship", "Unknown_1", true, "", SourceScene.Room)]
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
		public enum DoorTypeEnum
		{
			Normal = 0,
			Requires_password = 1,
		}


		[WProperty("Pirate Ship", "Door Type", true, "", SourceScene.Room)]
		public DoorTypeEnum DoorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(DoorTypeEnum), value_as_int))
					value_as_int = 0;
				return (DoorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("DoorType");
			}
		}

		[WProperty("Pirate Ship", "Path", true, "", SourceScene.Room)]
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

		[WProperty("Pirate Ship", "Unknown_4", true, "", SourceScene.Room)]
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
		public obj_pirateship(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Path = null;
			Unknown_4 = -1;
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
		
		[WProperty("obj_quake", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_quake", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_quake", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_rcloud : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_rcloud", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_roten", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_search : Actor
	{
		// Auto-Generated Properties from Templates
				public enum Unknown_1Enum
		{
			Unknown_0 = 0,
			Unknown_1 = 1,
			Unknown_255 = 255,
		}


		[WProperty("Searchlight", "Unknown_1", true, "", SourceScene.Room)]
		public Unknown_1Enum Unknown_1
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(Unknown_1Enum), value_as_int))
					value_as_int = 0;
				return (Unknown_1Enum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Unknown_1");
			}
		}
		public enum Unknown_2Enum
		{
			Unknown_0 = 0,
			Unknown_1 = 1,
			Unknown_2 = 2,
			Unknown_3 = 3,
			Unknown_4 = 4,
			Unknown_5 = 5,
			Unknown_6 = 6,
		}


		[WProperty("Searchlight", "Unknown_2", true, "", SourceScene.Room)]
		public Unknown_2Enum Unknown_2
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(Unknown_2Enum), value_as_int))
					value_as_int = 0;
				return (Unknown_2Enum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Unknown_2");
			}
		}

		[WProperty("Searchlight", "Path to Search", true, "", SourceScene.Room)]
		public Path_v2 PathtoSearch
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
				OnPropertyChanged("PathtoSearch");
			}
		}

		[WProperty("Searchlight", "Switch to Check", true, "", SourceScene.Room)]
		public int SwitchtoCheck
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
				OnPropertyChanged("SwitchtoCheck");
			}
		}

		[WProperty("Searchlight", "Switch to Set", true, "", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		// Constructor
		public obj_search(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			PathtoSearch = null;
			SwitchtoCheck = -1;
			SwitchtoSet = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_shelf : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_shelf", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_shelf", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_shmrgrd : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Spiked Hammer Button", "Switch to Set", true, "This switch will be set when the button is pressed.", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		// Constructor
		public obj_shmrgrd(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchtoSet = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_smplbg : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_smplbg", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_stair : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Falling Stair", "Disable Spawn Switch", true, "Once this switch is set, the stair will no longer appear.", SourceScene.Room)]
		public int DisableSpawnSwitch
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
				OnPropertyChanged("DisableSpawnSwitch");
			}
		}

		// Constructor
		public obj_stair(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			DisableSpawnSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swflat : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_swflat", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_swflat", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_swflat", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swhammer : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Hammer Button", "Switch to Unset", true, "This switch will be unset when the button is pressed.", SourceScene.Room)]
		public int SwitchtoUnset
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
				OnPropertyChanged("SwitchtoUnset");
			}
		}

		[WProperty("Hammer Button", "Switch to Set", true, "This switch will be set when the button is pressed.", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		// Constructor
		public obj_swhammer(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchtoUnset = -1;
			SwitchtoSet = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swheavy : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_swheavy", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_swheavy", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swlight : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_swlight", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_swlight", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_swlight", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_swpush : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Button", "Event to Start", true, "", SourceScene.Stage)]
		public MapEvent EventtoStart
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
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
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("EventtoStart");
			}
		}

		[WProperty("Button", "Switch to Set", true, "The switch for this button being pressed.", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("Button", "Alt Model (No Effect)", true, "This param seems like it was intended to change the visual model of the button.\nHowever, all 4 types of button have the same model specified for their normal and alternative models, so this has no effect.", SourceScene.Room)]
		public bool AltModelNoEffect
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00010000) >> 16);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x00010000 | (value_as_int << 16 & 0x00010000));
				OnPropertyChanged("AltModelNoEffect");
			}
		}
		public enum TypeEnum
		{
			Press_Once = 0,
			Hold_Down = 1,
			Press_Once_Inverted = 2,
			Iron_Boots_Button_Base = 3,
		}


		[WProperty("Button", "Type", true, "'Press Once' buttons stay down once you've pressed them. They only unpress themselves if something else unsets their switch.\n'Hold Down' buttons must be held down by something or they will unpress themselves automatically.\n'Press Once Inverted' buttons start off pressed down, and only unpress themselves when something else sets their switch. When one is pressed, it unsets its switch instead of setting it.\n'Iron Boots Button Base' is unused and doesn't appear to work.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x07000000) >> 24);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x07000000 | (value_as_int << 24 & 0x07000000));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("Button", "Should Use Disabled Switch", true, "For 'Hold Down' type buttons, this must be checked for 'Disabled Switch' to work. No effect on other types.", SourceScene.Room)]
		public bool ShouldUseDisabledSwitch
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x40000000) >> 30);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x40000000 | (value_as_int << 30 & 0x40000000));
				OnPropertyChanged("ShouldUseDisabledSwitch");
			}
		}

		[WProperty("Button", "Disabled Switch", true, "For 'Hold Down' type buttons, they will stop automatically unpressing themselves once this switch is set by something else (though they still need to be pressed once manually). No effect on other types.", SourceScene.Room)]
		public int DisabledSwitch
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
				OnPropertyChanged("DisabledSwitch");
			}
		}

		// Constructor
		public obj_swpush(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			EventtoStart = null;
			SwitchtoSet = -1;
			DisabledSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_table : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_table", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tapestry : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_tapestry", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_tapestry", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tenmado : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_tenmado", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_tenmado", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tide : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_tide", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_tide", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_timer : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_timer", "Time Limit (Half Seconds)", true, "This number multiplied by 15 frames is how long to wait before unsetting the switch.", SourceScene.Room)]
		public int TimeLimitHalfSeconds
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
				OnPropertyChanged("TimeLimitHalfSeconds");
			}
		}

		[WProperty("obj_timer", "Switch to Unset", true, "When something else sets this switch, this timer object starts a countdown. When the countdown reaches zero, it unsets this switch.", SourceScene.Room)]
		public int SwitchtoUnset
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
				OnPropertyChanged("SwitchtoUnset");
			}
		}

		// Constructor
		public obj_timer(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			TimeLimitHalfSeconds = -1;
			SwitchtoUnset = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tntrap : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_tntrap", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_tntrap", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_tntrap", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_tntrap", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
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
		
		[WProperty("obj_trap", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_trap", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_tribox : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_tribox", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_tribox", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_try : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_try", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("obj_try", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("obj_try", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("obj_try", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
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
		
		[WProperty("obj_Vds", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vfan : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_vfan", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_vmc", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_volcano", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("obj_vtil", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_vyasi : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_vyasi", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_warpt : Actor
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Locked_noncyclic_pot = 0,
			Unlocked_noncyclic_pot = 1,
			First_in_cycle = 2,
			Second_in_cycle = 3,
			Third_in_cycle = 4,
		}


		[WProperty("Warp Pot", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
			}
		}
		public enum UnlockedRegisterEnum
		{
			DRC_0xA207 = 0,
			FW_0xA107 = 1,
			Unused_0xA007 = 2,
			WT_0x9F07 = 3,
			ET_0xA307 = 4,
			Unused_0xA407 = 5,
		}


		[WProperty("Cyclic Warp Pot", "Unlocked Register", true, "", SourceScene.Room)]
		public UnlockedRegisterEnum UnlockedRegister
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				if (!Enum.IsDefined(typeof(UnlockedRegisterEnum), value_as_int))
					value_as_int = 0;
				return (UnlockedRegisterEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("UnlockedRegister");
			}
		}

		[WProperty("Cyclic Warp Pot", "Exit to First Pot in Cycle", true, "", SourceScene.Room)]
		public ExitData ExittoFirstPotinCycle
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
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
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("ExittoFirstPotinCycle");
			}
		}

		[WProperty("Cyclic Warp Pot", "Exit to Second Pot in Cycle", true, "", SourceScene.Room)]
		public ExitData ExittoSecondPotinCycle
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
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("ExittoSecondPotinCycle");
			}
		}

		[WProperty("Cyclic Warp Pot", "Exit to Third Pot in Cycle", true, "", SourceScene.Room)]
		public ExitData ExittoThirdPotinCycle
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
				m_Parameters = (int)(m_Parameters & ~0xFF000000 | (value_as_int << 24 & 0xFF000000));
				OnPropertyChanged("ExittoThirdPotinCycle");
			}
		}

		[WProperty("Cyclic Warp Pot", "Top Unblocked Switch", true, "Only has an effect if this warp pot is locked.\nFor a normal warp pot blocked with a lid, set this to 255.\nFor a warp pot that has its top blocked by some other object like a boulder, set this to what the switch set by that other object when it's destroyed is.", SourceScene.Room)]
		public int TopUnblockedSwitch
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
				OnPropertyChanged("TopUnblockedSwitch");
			}
		}

		[WProperty("Cyclic Warp Pot", "Play Unlocked Sound Effect", true, "", SourceScene.Room)]
		public bool PlayUnlockedSoundEffect
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				if (value_as_int == 255) {
					return true;
				} else {
					return false;
				}
				
			}

			set
			{
				int value_as_int = value ? 255 : 0;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("PlayUnlockedSoundEffect");
			}
		}

		[WProperty("Cyclic Warp Pot", "Is Locked", true, "", SourceScene.Room)]
		public bool IsLocked
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0xFF00) >> 8);
				if (value_as_int == 255) {
					return true;
				} else {
					return false;
				}
				
			}

			set
			{
				int value_as_int = value ? 255 : 0;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0xFF00 | (value_as_int << 8 & 0xFF00));
				OnPropertyChanged("IsLocked");
			}
		}

		[WProperty("Noncyclic Warp Pot", "Unused Exit to This Pot", true, "", SourceScene.Room)]
		public ExitData UnusedExittoThisPot
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000FF0) >> 4);
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
				m_Parameters = (int)(m_Parameters & ~0x00000FF0 | (value_as_int << 4 & 0x00000FF0));
				OnPropertyChanged("UnusedExittoThisPot");
			}
		}

		[WProperty("Noncyclic Warp Pot", "Exit to Destination Pot", true, "", SourceScene.Room)]
		public ExitData ExittoDestinationPot
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000FF000) >> 12);
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
				m_Parameters = (int)(m_Parameters & ~0x000FF000 | (value_as_int << 12 & 0x000FF000));
				OnPropertyChanged("ExittoDestinationPot");
			}
		}

		[WProperty("Noncyclic Warp Pot", "This Unlocked Switch", true, "The warp pot will set this switch when it is unlocked.\nIf this is an 'Unlocked'-type warp pot, it sets this automatically when you enter the room.", SourceScene.Room)]
		public int ThisUnlockedSwitch
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
				OnPropertyChanged("ThisUnlockedSwitch");
			}
		}

		[WProperty("Noncyclic Warp Pot", "Destination Unlocked Switch", true, "The warp pot will not let the player go through it until this switch is set.", SourceScene.Room)]
		public int DestinationUnlockedSwitch
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
				OnPropertyChanged("DestinationUnlockedSwitch");
			}
		}

		// Constructor
		public obj_warpt(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			ExittoFirstPotinCycle = null;
			ExittoSecondPotinCycle = null;
			ExittoThirdPotinCycle = null;
			TopUnblockedSwitch = -1;
			UnusedExittoThisPot = null;
			ExittoDestinationPot = null;
			ThisUnlockedSwitch = -1;
			DestinationUnlockedSwitch = -1;
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
		
		[WProperty("obj_Yboil", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_ygush00 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_ygush00", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_YLzou : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_YLzou", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class obj_zouK : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("obj_zouK", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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


		[WProperty("Octorok", "Type", true, "The 'Spawned saltwater Octorok' type does not appear when manually placed, it's just for the spawner to create.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}
		public enum ProjectileTypeEnum
		{
			Rocks = 0,
			Bombs = 1,
		}


		[WProperty("Freshwater Octorok", "Projectile Type", true, "This only affects what freshwater Octoroks shoot, not saltwater ones.", SourceScene.Room)]
		public ProjectileTypeEnum ProjectileType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(ProjectileTypeEnum), value_as_int))
					value_as_int = 0;
				return (ProjectileTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("ProjectileType");
			}
		}

		[WProperty("Saltwater Octorok Spawner", "Sight Range (Thousands)", true, "For the 'Saltwater Octorok spawner' type, this number multiplied by 1000 is the range within it will notice the player and start spawning saltwater Octoroks.\nNo effect on other types besides the spawner.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeThousands = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class oship : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("oship", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("oship", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("oship", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("oship", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("oship", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("oship", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("oship", "Unknown_7", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			Unknown_7 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class pedestal : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("pedestal", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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


		[WProperty("ph", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("ph", "Horizontal Sight Range (Hundreds)", true, "This number multiplied by 100 is the radius of the cylinder it can see Link within. 255 will default to a range of 1000 for Peahats or 12000 for Seahats.", SourceScene.Room)]
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

		[WProperty("ph", "Vertical Sight Range (Hundreds)", true, "This number multiplied by 100 is half the height of the cylinder it can see Link within. 255 will default to a range of 500 for Peahats or 6000 for Seahats.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			HorizontalSightRangeHundreds = -1;
			VerticalSightRangeHundreds = -1;
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
		}


		[WProperty("Miniblin", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 1;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("Miniblin", "Initial Miniblin Won't Spawn Onscreen?", true, "If this is checked, the first Miniblin won't spawn until the player's camera is turned away from its spawn point.\n(Miniblins after the first one always act like that regardless of whether this is checked or not.)", SourceScene.Room)]
		public bool InitialMiniblinWontSpawnOnscreen
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000010) >> 4);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
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


		[WProperty("Miniblin", "Respawn Delay", true, "Number of frames after you kill it before it can respawn.", SourceScene.Room)]
		public RespawnDelayEnum RespawnDelay
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000E0) >> 5);
				if (!Enum.IsDefined(typeof(RespawnDelayEnum), value_as_int))
					value_as_int = 0;
				return (RespawnDelayEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000E0 | (value_as_int << 5 & 0x000000E0));
				OnPropertyChanged("RespawnDelay");
			}
		}

		[WProperty("Miniblin", "Sight Range (Hundreds)", true, "", SourceScene.Room)]
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

		[WProperty("Miniblin", "Disable Respawn Switch", true, "If this is a valid switch and used with a respawning type Miniblin, this being set stops it from respawning.\nAlternatively, you can also use this with a non-respawning Miniblin, in which case it acts like a 'Deactivate on Death Switch' that it sets on death - but you should only set a temporary switch this way, as if you set a permanent switch the Miniblin will be permanently inactive the next time you enter the room but won't be considered dead.", SourceScene.Room)]
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

		[WProperty("Miniblin", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("Miniblin", "Initial Spawn Delay", true, "Number of frames after you enter the room before it can spawn the first Miniblin. If setting this to something other than 0, it is recommended to also check 'Initial Miniblin Won't Spawn Onscreen?'. If you don't, then the first Miniblin will be visible before it actually spawns, but simply be deactivated and invincible.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeHundreds = -1;
			DisableRespawnSwitch = -1;
			EnableSpawnSwitch = -1;
			InitialSpawnDelay = -1;
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


		[WProperty("Poe", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("Poe", "Hovers At Initial Height?", true, "If checked, this Poe will maintain its height in the air like normal. If unchecked, it will slowly float down to the floor.", SourceScene.Room)]
		public bool HoversAtInitialHeight
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000100) >> 8);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
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


		[WProperty("Poe", "Color", true, "", SourceScene.Room)]
		public ColorEnum Color
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FE00) >> 9);
				if (!Enum.IsDefined(typeof(ColorEnum), value_as_int))
					value_as_int = 0;
				return (ColorEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FE00 | (value_as_int << 9 & 0x0000FE00));
				OnPropertyChanged("Color");
				UpdateModel();
			}
		}

		[WProperty("Poe", "Sight Range (Tens)", true, "For the 'Invisible except Lantern until seeing Link' type, this number multiplied by 10 is the range it can see Link within and will materialize. No effect on other types.", SourceScene.Room)]
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

		[WProperty("Poe", "Path", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeTens = -1;
			Path = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class pz : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("pz", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class race_item : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("race_item", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("race_item", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("race_item", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
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


		[WProperty("ReDead", "Idle Animation", true, "", SourceScene.Room)]
		public IdleAnimationEnum IdleAnimation
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000001) >> 0);
				if (!Enum.IsDefined(typeof(IdleAnimationEnum), value_as_int))
					value_as_int = 0;
				return (IdleAnimationEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000001 | (value_as_int << 0 & 0x00000001));
				OnPropertyChanged("IdleAnimation");
				UpdateModel();
			}
		}

		[WProperty("ReDead", "Guarded Area Radius", true, "The ReDead has a base radius of 650 and this value is added to that.\nBecause the maximum value here is 127, this doesn't change the size of the area it guards very much.", SourceScene.Room)]
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

		[WProperty("ReDead", "Should Check Switch To Enable Spawn?", true, "If this is checked, the Enable Spawn Switch must be set for the ReDead to spawn.\nIf the Enable Spawn Switch is invalid, then unlike most enemies, this ReDead will never appear instead of always appearing.", SourceScene.Room)]
		public bool ShouldCheckSwitchToEnableSpawn
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (value_as_int == 0) {
					return true;
				} else {
					return false;
				}
				
			}

			set
			{
				int value_as_int = value ? 0 : 1;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("ShouldCheckSwitchToEnableSpawn");
			}
		}

		[WProperty("ReDead", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			GuardedAreaRadius = -1;
			EnableSpawnSwitch = -1;
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
		
		[WProperty("saku", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("saku", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("saku", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("saku", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class salvage : Actor
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Needs_Chart = 0,
			Unused = 1,
			Checks_Switch = 2,
			Normal_Light_Ring = 3,
			Night_Only = 4,
			Octorok_Vase = 5,
			Full_Moon_Night_Only = 6,
		}


		[WProperty("Salvage Point", "Type", true, "'Needs Chart' are the important pillar of light salvage points that won't appear until you have the relevant Treasure Chart or Triforce Chart.\n'Checks Switch' only appear once a certain switch is set, such as Big Octo salvage points.\n'Normal Light Ring' are light rings that always appear.\n'Night Only' are light rings that only appear at night.\n'Octorok Vase' are invisible salvage points that will cause the player to fish up an old vase, and then spawn a Saltwater Octorok next to them.\n'Full Moon Night Only' are light rings that only appear at night when the full moon is out.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xF0000000) >> 28);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0xF0000000 | (value_as_int << 28 & 0xF0000000));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("Salvage Point", "Item ID", true, "", SourceScene.Room)]
		public ItemID ItemID
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000FF0) >> 4);
				if (!Enum.IsDefined(typeof(ItemID), value_as_int))
					value_as_int = 0;
				return (ItemID)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000FF0 | (value_as_int << 4 & 0x00000FF0));
				OnPropertyChanged("ItemID");
			}
		}

		[WProperty("Salvage Point", "Salvaged Object Type", true, "", SourceScene.Room)]
		public int SalvagedObjectType
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
				OnPropertyChanged("SalvagedObjectType");
			}
		}

		[WProperty("Chart Salvage Point", "Chart", true, "Affects which chart reveals this salvage point.\nOnly works for 'Needs Chart' type salvage points.", SourceScene.Room)]
		public int Chart
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
				OnPropertyChanged("Chart");
			}
		}

		[WProperty("Light Ring Salvage Point", "Salvage Flag", true, "Which salvage flag in this sector to use for this salvage point. Can be from 0-15.\nOr set this to 31 if you want the salvage point to reappear every time the player exits and re-enters the sector.\nOnly works for 'Checks Switch', 'Normal Light Ring', and 'Night Only' type salvage points.", SourceScene.Room)]
		public int SalvageFlag
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
				OnPropertyChanged("SalvageFlag");
			}
		}
		public enum SalvagedEventBitEnum
		{
			Event_bit_0x2080 = 0,
			Event_bit_0x2004 = 1,
			Event_bit_0x2002 = 2,
			Event_bit_0x2804 = 3,
			Event_bit_0x2802 = 4,
			Event_bit_0x2801 = 5,
			Event_bit_0x2980 = 6,
			Event_bit_0x2940 = 7,
			Event_bit_0x3B01 = 8,
			Event_bit_0x3C80 = 9,
			Event_bit_0x3C40 = 10,
			Event_bit_0x3C20 = 11,
			Event_bit_0x3C10 = 12,
			Event_bit_0x3C08 = 13,
			Event_bit_0x3C04 = 14,
			Event_bit_0x3C02 = 15,
		}


		[WProperty("Full Moon Salvage Point", "Salvaged Event Bit", true, "This event bit will be set when the player salvages it, and the salvage point will not appear as long as it's set.\nAll sixteen of these are cleared once per ingame week, on Friday.\nOnly works for 'Full Moon Night Only' type salvage points.", SourceScene.Room)]
		public SalvagedEventBitEnum SalvagedEventBit
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0FF00000) >> 20);
				if (!Enum.IsDefined(typeof(SalvagedEventBitEnum), value_as_int))
					value_as_int = 0;
				return (SalvagedEventBitEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0FF00000 | (value_as_int << 20 & 0x0FF00000));
				OnPropertyChanged("SalvagedEventBit");
			}
		}

		[WProperty("Chart Salvage Point", "Duplicate Placement ID", true, "When placing a salvage point that needs a chart to see, you should actually place 4 in the same sector in different placements, with the only difference being that this ID should be 0, 1, 2, or 3, one for each.\nWhen the player starts a new save file, the game will randomly pick one of these different placements from 0-2, and only that placement of chart salvages will appear on that save file.\nIn Second Quest, only the ones with placement ID 3 will appear.\nOnly works for 'Needs Chart' type salvage points.", SourceScene.Room)]
		public int DuplicatePlacementID
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x0003) >> 0);
				return value_as_int;
			}

			set
			{
				int value_as_int = value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x0003 | (value_as_int << 0 & 0x0003));
				OnPropertyChanged("DuplicatePlacementID");
			}
		}

		[WProperty("Switch Salvage Point", "Switch to Check", true, "The salvage point will not appear until this switch is set.\nOnly works for 'Checks Switch' type salvage points.", SourceScene.Room)]
		public int SwitchtoCheck
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
				OnPropertyChanged("SwitchtoCheck");
			}
		}

		// Constructor
		public salvage(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SalvagedObjectType = -1;
			Chart = -1;
			SalvageFlag = -1;
			DuplicatePlacementID = -1;
			SwitchtoCheck = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class salvage_tbox : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("salvage_tbox", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("salvage_tbox", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("shand", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ship : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("ship", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("ship", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("ship", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class shop_item : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("shop_item", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("shop_item", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class shutter : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("shutter", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("shutter", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class shutter2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("shutter2", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("sitem", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("sitem", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("sitem", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("sitem", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sk : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("sk", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("sk", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class sk2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("sk2", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("sk2", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("sk2", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class spotbox : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("spotbox", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ss : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("ss", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("ss", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("ss", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ssk : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("ssk", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("ssk", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("ssk", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
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


		[WProperty("sss", "SFX Type", true, "The SFX to play when it comes out of the ground. There's no noticeable difference between the two, so this is a useless parameter.", SourceScene.Room)]
		public SFXTypeEnum SFXType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(SFXTypeEnum), value_as_int))
					value_as_int = 0;
				return (SFXTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("SFXType");
			}
		}

		[WProperty("sss", "Sight Range (Tens)", true, "This number multiplied by 10 is used as a range within it notices Link and comes out of the ground. 255 will default to a range of 1000.", SourceScene.Room)]
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

		[WProperty("sss", "Enable Spawn Switch", true, "If this is not 0 or 255, the Dexivine will hide underground until this switch is set.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SightRangeTens = -1;
			EnableSpawnSwitch = -1;
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


		[WProperty("Stalfos", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000000F | (value_as_int << 0 & 0x0000000F));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("Stalfos", "Ambush Sight Range (Tens)", true, "If the Stalfos is underground or in a coffin, this is the range within it will notice the player.", SourceScene.Room)]
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

		[WProperty("Stalfos", "Ambush Switch", true, "If this is a valid switch and the Stalfos is underground or in a coffin, it will only come out when this switch is set, overriding the normal behave of coming out when the player comes near it.", SourceScene.Room)]
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

		[WProperty("Stalfos", "Disable Spawn on Death Switch", true, "", SourceScene.Room)]
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
		public st(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			AmbushSightRangeTens = -1;
			Unused = -1;
			AmbushSwitch = -1;
			DisableSpawnonDeathSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class steam_tag : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("steam_tag", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class stone : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("stone", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("stone", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("stone", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("stone", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("stone", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("stone", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class stone2 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("stone2", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("stone2", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("stone2", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("stone2", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("stone2", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("stone2", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swattack : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("swattack", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("swattack", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swc00 : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Switch Setter Trigger Region", "Switch to Set", true, "", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("Switch Setter Trigger Region", "Prerequisite Switch", true, "If this is not set to 255, then this switch must be set before this trigger region becomes active.", SourceScene.Room)]
		public int PrerequisiteSwitch
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
				OnPropertyChanged("PrerequisiteSwitch");
			}
		}
		public enum TypeEnum
		{
			Set_and_Unset = 0,
			Unknown_1 = 1,
			Unknown_2 = 2,
			Only_Set = 3,
		}


		[WProperty("Switch Setter Trigger Region", "Type", true, "'Set and Unset' will set its 'Switch to Set' when the player enters the region, and unset it when the player leaves the region.\n'Only Set' will set it, but never unset it.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00030000) >> 16);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00030000 | (value_as_int << 16 & 0x00030000));
				OnPropertyChanged("Type");
			}
		}

		// Constructor
		public swc00(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchtoSet = -1;
			PrerequisiteSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swhit0 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("swhit0", "Switch to Set", true, "Switch to set when hit.", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("swhit0", "Event", true, "The event to start when hit? (Only for type 'Unknown 3'.)", SourceScene.Stage)]
		public MapEvent Event
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
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
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Event");
			}
		}
		public enum TypeEnum
		{
			Unknown_0 = 0,
			Unknown_1 = 1,
			Unknown_2 = 2,
			Unknown_3 = 3,
			Unknown_4 = 4,
		}


		[WProperty("swhit0", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000F0000) >> 16);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 4;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000F0000 | (value_as_int << 16 & 0x000F0000));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("swhit0", "Time Limit (Half Seconds)", true, "This number times 15 frames is how long the player has to hit all the crystals after the first crystal is hit.", SourceScene.Room)]
		public int TimeLimitHalfSeconds
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
				OnPropertyChanged("TimeLimitHalfSeconds");
			}
		}

		[WProperty("swhit0", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("swhit0", "Switch to Check", true, "Once this switch is set, the crystal will consider the puzzle finished and no longer be on a timer.", SourceScene.Room)]
		public int SwitchtoCheck
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
				OnPropertyChanged("SwitchtoCheck");
			}
		}

		// Constructor
		public swhit0(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchtoSet = -1;
			Event = null;
			TimeLimitHalfSeconds = -1;
			Unknown_5 = -1;
			SwitchtoCheck = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class switem : TriggerRegion
	{
		// Auto-Generated Properties from Templates
				public enum WhatTriggersItEnum
		{
			Sword_or_Enemy_Weapon = 0,
			Bombs = 1,
			Boomerang = 2,
			Skull_Hammer = 3,
			Any_Arrows = 4,
			Hookshot = 5,
			Never_Triggers = 6,
			Anything = 255,
		}


		[WProperty("Hidden Item Pickup", "What Triggers It", true, "", SourceScene.Room)]
		public WhatTriggersItEnum WhatTriggersIt
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(WhatTriggersItEnum), value_as_int))
					value_as_int = 0;
				return (WhatTriggersItEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("WhatTriggersIt");
			}
		}

		[WProperty("Hidden Item Pickup", "Spawned Item", true, "", SourceScene.Room)]
		public DroppedItemID SpawnedItem
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00003F00) >> 8);
				if (!Enum.IsDefined(typeof(DroppedItemID), value_as_int))
					value_as_int = 0;
				return (DroppedItemID)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00003F00 | (value_as_int << 8 & 0x00003F00));
				OnPropertyChanged("SpawnedItem");
			}
		}

		[WProperty("Hidden Item Pickup", "Item Pickup Flag", true, "", SourceScene.Room)]
		public int ItemPickupFlag
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
				OnPropertyChanged("ItemPickupFlag");
			}
		}

		// Constructor
		public switem(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			ItemPickupFlag = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swpropeller : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("swpropeller", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("swpropeller", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swtact : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("swtact", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("swtact", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("swtact", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class swtdoor : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("swtdoor", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("swtdoor", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
	public partial class tag_attention : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_attention", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_attention", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("tag_etc", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_etc", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_event : TriggerRegion
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


		[WProperty("tag_event", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("Type");
			}
		}

		[WProperty("tag_event", "Switch to Set", true, "Switch that this trigger region sets when it triggers the event.", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("tag_event", "Enable Spawn Switch", true, "Switch that must be set before this trigger region will become active.", SourceScene.Room)]
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

		[WProperty("tag_event", "Event", true, "The event to start when entering this region.", SourceScene.Stage)]
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

		[WProperty("tag_event", "Enable Spawn Event Bit", true, "Event bit that must be set before this trigger region will become active.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchtoSet = -1;
			EnableSpawnSwitch = -1;
			Event = null;
			EnableSpawnEventBit = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_evsw : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("Event Bit Switch Setter", "Switch to Set", true, "", SourceScene.Room)]
		public int SwitchtoSet
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
				OnPropertyChanged("SwitchtoSet");
			}
		}

		[WProperty("Event Bit Switch Setter", "Event Bit to Check", true, "", SourceScene.Room)]
		public int EventBittoCheck
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
				OnPropertyChanged("EventBittoCheck");
			}
		}
		public enum TypeEnum
		{
			Set_and_Unset = 0,
			Only_Set = 1,
		}


		[WProperty("Event Bit Switch Setter", "Type", true, "'Set and Unset' will set the switch if the event bit is set, and unset it if it's not.\n'Only Set' will set the switch if the event bit is set, but will never unset it.", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x03000000) >> 24);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x03000000 | (value_as_int << 24 & 0x03000000));
				OnPropertyChanged("Type");
			}
		}

		// Constructor
		public tag_evsw(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			SwitchtoSet = -1;
			EventBittoCheck = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_ghostship : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_ghostship", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_ghostship", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_hint : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_hint", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_hint", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_hint", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("tag_hint", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("tag_hint", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("tag_hint", "Unknown_7", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			MessageID = -1;
			Unknown_7 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_island : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_island", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_island", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_island", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_kb_item : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_kb_item", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_kb_item", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_kb_item", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("tag_kb_item", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_kf1 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_kf1", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
				public enum TypeEnum
		{
			Invisible_Light_Region = 0,
			Light_Beam = 1,
			Light_Detector = 2,
		}


		[WProperty("tag_light", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00000003) >> 0);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00000003 | (value_as_int << 0 & 0x00000003));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("tag_light", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_light", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("tag_light", "Unknown_4", true, "", SourceScene.Room)]
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
				UpdateModel();
			}
		}

		[WProperty("tag_light", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("tag_light", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("tag_light", "Unknown_7", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			Unknown_7 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_md_cb : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_md_cb", "MessageID", true, "", SourceScene.Room)]
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

		[WProperty("tag_md_cb", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_md_cb", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("tag_md_cb", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			MessageID = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_mk : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_mk", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_mk", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_mk", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_msg : TriggerRegion
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_msg", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_msg", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_msg", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("tag_msg", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("tag_msg", "Unknown_6", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			MessageID = -1;
			Unknown_6 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_photo : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_photo", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_ret : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_ret", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_so : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_so", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_so", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_so", "Unknown_3", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_volcano : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_volcano", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tag_volcano", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tag_volcano", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("tag_volcano", "Unknown_4", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tag_waterlevel : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tag_waterlevel", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tama : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tama", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class tbox : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("tbox", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("tbox", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("tbox", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("tbox", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("tbox", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("tbox", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("tbox", "Unknown_7", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			Unknown_7 = -1;
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


		[WProperty("tn", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000000F) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
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
		}


		[WProperty("tn", "Color", true, "", SourceScene.Room)]
		public ColorEnum Color
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000F0) >> 4);
				if (!Enum.IsDefined(typeof(ColorEnum), value_as_int))
					value_as_int = 5;
				return (ColorEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000F0 | (value_as_int << 4 & 0x000000F0));
				OnPropertyChanged("Color");
				UpdateModel();
			}
		}

		[WProperty("tn", "Guarded Area Radius (Tens)", true, "", SourceScene.Room)]
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


		[WProperty("tn", "Frozen in Time Pose", true, "", SourceScene.Room)]
		public FrozeninTimePoseEnum FrozeninTimePose
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
				if (!Enum.IsDefined(typeof(FrozeninTimePoseEnum), value_as_int))
					value_as_int = 0;
				return (FrozeninTimePoseEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("FrozeninTimePose");
				UpdateModel();
			}
		}

		[WProperty("tn", "Path", true, "", SourceScene.Room)]
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

		[WProperty("tn", "Enable Spawn Switch", true, "If this switch is valid, the Darknut will not appear until it is set.", SourceScene.Room)]
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
		}


		[WProperty("tn", "Extra Equipment", true, "", SourceScene.Room)]
		public ExtraEquipmentEnum ExtraEquipment
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters1 & 0x00E0) >> 5);
				if (!Enum.IsDefined(typeof(ExtraEquipmentEnum), value_as_int))
					value_as_int = 5;
				return (ExtraEquipmentEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_AuxillaryParameters1 = (short)(m_AuxillaryParameters1 & ~0x00E0 | (value_as_int << 5 & 0x00E0));
				OnPropertyChanged("ExtraEquipment");
				UpdateModel();
			}
		}

		[WProperty("tn", "Disable Spawn on Death Switch", true, "The Darknut will set this switch when it dies and will not spawn while this is set.", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			GuardedAreaRadiusTens = -1;
			Path = null;
			EnableSpawnSwitch = -1;
			DisableSpawnonDeathSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class toge : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("toge", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("toge", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("tornado", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("tsubo", "Dropped Item", true, "", SourceScene.Room)]
		public DroppedItemID DroppedItem
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000003F) >> 0);
				if (!Enum.IsDefined(typeof(DroppedItemID), value_as_int))
					value_as_int = 0;
				return (DroppedItemID)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000003F | (value_as_int << 0 & 0x0000003F));
				OnPropertyChanged("DroppedItem");
			}
		}
		public enum BehaviorTypeEnum
		{
			Spawns_When_Switch_is_Set = 0,
			Has_one_Boko_Stick = 1,
			Has_two_Boko_Sticks = 2,
			Has_three_Boko_Sticks = 3,
			Unknown_4 = 4,
			Inactive_Until_Switch_is_Set = 5,
			Unknown_6 = 6,
			Unknown_7 = 7,
			Unknown_8 = 8,
			Unknown_9 = 9,
			Unknown_10 = 10,
			Unknown_11 = 11,
			Unknown_12 = 12,
			Unknown_13 = 13,
			Unknown_14 = 14,
			Unknown_15 = 15,
			Unknown_16 = 16,
			Unknown_17 = 17,
			Unknown_18 = 18,
			Unknown_19 = 19,
			Unknown_20 = 20,
			Unknown_21 = 21,
			Unknown_22 = 22,
			Unknown_23 = 23,
			Unknown_24 = 24,
			Unknown_25 = 25,
			Unknown_26 = 26,
			Unknown_27 = 27,
			Unknown_28 = 28,
			Unknown_29 = 29,
			Unknown_30 = 30,
			Unknown_31 = 31,
			Unknown_32 = 32,
			Unknown_33 = 33,
			Unknown_34 = 34,
			Unknown_35 = 35,
			Unknown_36 = 36,
			Unknown_37 = 37,
			Unknown_38 = 38,
			Unknown_39 = 39,
			Unknown_40 = 40,
			Unknown_41 = 41,
			Unknown_42 = 42,
			Unknown_43 = 43,
			Unknown_44 = 44,
			Unknown_45 = 45,
			Unknown_46 = 46,
			Unknown_47 = 47,
			Unknown_48 = 48,
			Unknown_49 = 49,
			Unknown_50 = 50,
			Unknown_51 = 51,
			Unknown_52 = 52,
			Unknown_53 = 53,
			Unknown_54 = 54,
			Unknown_55 = 55,
			Unknown_56 = 56,
			Unknown_57 = 57,
			Unknown_58 = 58,
			Unknown_59 = 59,
			Unknown_60 = 60,
			Unknown_61 = 61,
			Unknown_62 = 62,
			Normal = 63,
		}


		[WProperty("tsubo", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00003F00) >> 8);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x00003F00 | (value_as_int << 8 & 0x00003F00));
				OnPropertyChanged("BehaviorType");
			}
		}
		public enum Unknown_3Enum
		{
			Unknown_0 = 0,
			Invisible = 1,
			Unknown_2 = 2,
			Unknown_3 = 3,
		}


		[WProperty("tsubo", "Unknown_3", true, "", SourceScene.Room)]
		public Unknown_3Enum Unknown_3
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000C000) >> 14);
				if (!Enum.IsDefined(typeof(Unknown_3Enum), value_as_int))
					value_as_int = 0;
				return (Unknown_3Enum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0000C000 | (value_as_int << 14 & 0x0000C000));
				OnPropertyChanged("Unknown_3");
			}
		}

		[WProperty("tsubo", "Item Pickup Flag", true, "", SourceScene.Room)]
		public int ItemPickupFlag
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
				OnPropertyChanged("ItemPickupFlag");
			}
		}
		public enum TypeEnum
		{
			Small_pot = 0,
			Large_pot = 1,
			Water_pot = 2,
			Barrel = 3,
			Stool = 4,
			Skull = 5,
			Bucket = 6,
			Nut = 7,
			Golden_crate = 8,
			TotG_Pillar_Statue_A = 9,
			TotG_Pillar_Statue_B = 10,
			TotG_Pillar_Statue_C = 11,
			TotG_Pillar_Statue_D = 12,
			Seed = 13,
			Fancy_pot = 14,
			Wooden_crate = 15,
		}


		[WProperty("tsubo", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0F000000) >> 24);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x0F000000 | (value_as_int << 24 & 0x0F000000));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("tsubo", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("tsubo", "Do Not Ground On Spawn", true, "Normally, pots will teleport themselves to the nearest ground when they are spawned.\nIf this is checked, the pot will instead float where it was placed.\nFor Nuts, this also causes it to not start to rot away automatically on spawn.", SourceScene.Room)]
		public bool DoNotGroundOnSpawn
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x80000000) >> 31);
				if (value_as_int == 0) {
					return false;
				} else if (value_as_int == 255) {
					return false;
				} else {
					return true;
				}
				
			}

			set
			{
				int value_as_int = value ? 1 : 0;
				m_Parameters = (int)(m_Parameters & ~0x80000000 | (value_as_int << 31 & 0x80000000));
				OnPropertyChanged("DoNotGroundOnSpawn");
			}
		}

		[WProperty("tsubo", "Enable Spawn/Activation Switch", true, "If the pot's behavior is set to 'Spawns When Switch is Set' and this switch is valid, the pot will not appear until this switch is set.\nIf the pot's behavior is set to 'Inactive Until Switch is Set' and this switch is valid, the pot will be visible, but cannot be broken, and picking it up will be strange and buggy, until this switch is set.\nNo effect on other behaviors.", SourceScene.Room)]
		public int EnableSpawnActivationSwitch
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
				OnPropertyChanged("EnableSpawnActivationSwitch");
			}
		}

		// Constructor
		public tsubo(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			BehaviorType = BehaviorTypeEnum.Normal;
			ItemPickupFlag = -1;
			Unknown_6 = -1;
			EnableSpawnActivationSwitch = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class wall : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("wall", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("wall", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpdm20 : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("warpdm20", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpf : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("warpf", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
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
		
		[WProperty("warpgn", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("warpgn", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warphr : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("warphr", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpls : Actor
	{
		// Auto-Generated Properties from Templates
				public enum TypeEnum
		{
			Pink_Warp = 0,
			White_Warp = 1,
		}


		[WProperty("Light Beam Warp", "Type", true, "", SourceScene.Room)]
		public TypeEnum Type
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0xF0000000) >> 28);
				if (!Enum.IsDefined(typeof(TypeEnum), value_as_int))
					value_as_int = 0;
				return (TypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0xF0000000 | (value_as_int << 28 & 0xF0000000));
				OnPropertyChanged("Type");
				UpdateModel();
			}
		}

		[WProperty("Light Beam Warp", "Exit", true, "", SourceScene.Room)]
		public ExitData Exit
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x0000FF00) >> 8);
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
				m_Parameters = (int)(m_Parameters & ~0x0000FF00 | (value_as_int << 8 & 0x0000FF00));
				OnPropertyChanged("Exit");
			}
		}

		[WProperty("Light Beam Warp", "Activation Switch", true, "The warp will not appear and be active until this switch is set.", SourceScene.Room)]
		public int ActivationSwitch
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
				OnPropertyChanged("ActivationSwitch");
			}
		}

		[WProperty("Light Beam Warp", "Activated Event", true, "The event to start when activated.", SourceScene.Stage)]
		public MapEvent ActivatedEvent
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x00FF0000) >> 16);
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
				m_Parameters = (int)(m_Parameters & ~0x00FF0000 | (value_as_int << 16 & 0x00FF0000));
				OnPropertyChanged("ActivatedEvent");
			}
		}

		// Constructor
		public warpls(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Exit = null;
			ActivationSwitch = -1;
			ActivatedEvent = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class warpmj : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("warpmj", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class waterfall : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("waterfall", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("waterfall", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
		
		[WProperty("windmill", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class wind_tag : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("wind_tag", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("wind_tag", "Unknown_2", true, "", SourceScene.Room)]
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

		[WProperty("wind_tag", "Unknown_3", true, "", SourceScene.Room)]
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

		[WProperty("wind_tag", "Unknown_4", true, "", SourceScene.Room)]
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

		[WProperty("wind_tag", "Unknown_5", true, "", SourceScene.Room)]
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

		[WProperty("wind_tag", "Unknown_6", true, "", SourceScene.Room)]
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

		[WProperty("wind_tag", "Unknown_7", true, "", SourceScene.Room)]
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

		[WProperty("wind_tag", "Unknown_8", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
			Unknown_3 = -1;
			Unknown_4 = -1;
			Unknown_5 = -1;
			Unknown_6 = -1;
			Unknown_7 = -1;
			Unknown_8 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class wz : Actor
	{
		// Auto-Generated Properties from Templates
				public enum BehaviorTypeEnum
		{
			Shoots_Fireballs = 0,
			Spawns_Enemies_and_Shoots_Fireballs = 1,
			Miniboss = 2,
			Unknown_1 = 3,
			Fireball = 10,
			Spawner_Orb_1 = 12,
			Spawner_Orb_2 = 13,
		}


		[WProperty("wz", "Behavior Type", true, "", SourceScene.Room)]
		public BehaviorTypeEnum BehaviorType
		{ 
			get
			{
				int value_as_int = (int)((m_Parameters & 0x000000FF) >> 0);
				if (!Enum.IsDefined(typeof(BehaviorTypeEnum), value_as_int))
					value_as_int = 0;
				return (BehaviorTypeEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_Parameters = (int)(m_Parameters & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
				OnPropertyChanged("BehaviorType");
			}
		}

		[WProperty("wz", "Disable Spawn on Death Switch", true, "", SourceScene.Room)]
		public int DisableSpawnonDeathSwitch
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
				OnPropertyChanged("DisableSpawnonDeathSwitch");
			}
		}

		[WProperty("wz", "Enable Spawn Switch", true, "", SourceScene.Room)]
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

		[WProperty("wz", "Path", true, "", SourceScene.Room)]
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
		public enum EnemySummonTableEnum
		{
			Many_Keese_and_Magtails = 0,
			Many_Keese_and_Red_Bubbles = 1,
			Blue_Bubbles = 2,
			Many_Morths = 3,
			Many_Green_ChuChus = 4,
			Red_and_Blue_Bubbles_and_Poes = 5,
			Stalfos_and_ReDeads = 6,
			Moblins_and_Darknuts = 7,
			Peahats_Keese_and_Kargarocs = 8,
			Keese_and_Magtails = 9,
			Keese_and_Red_Bubbles = 10,
			Morths = 11,
			Green_ChuChus = 12,
			Bokoblins_and_Moblins = 13,
			Kargarocs_Keese_Green_and_Red_ChuChus_and_Morths = 14,
		}


		[WProperty("wz", "Enemy Summon Table", true, "", SourceScene.Room)]
		public EnemySummonTableEnum EnemySummonTable
		{ 
			get
			{
				int value_as_int = (int)((m_AuxillaryParameters2 & 0x00FF) >> 0);
				if (!Enum.IsDefined(typeof(EnemySummonTableEnum), value_as_int))
					value_as_int = 0;
				return (EnemySummonTableEnum)value_as_int;
			}

			set
			{
				int value_as_int = (int)value;
				m_AuxillaryParameters2 = (short)(m_AuxillaryParameters2 & ~0x00FF | (value_as_int << 0 & 0x00FF));
				OnPropertyChanged("EnemySummonTable");
			}
		}

		// Constructor
		public wz(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			
		}

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			DisableSpawnonDeathSwitch = -1;
			EnableSpawnSwitch = -1;
			Path = null;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ygcwp : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("ygcwp", "Unknown_1", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ykgr : Actor
	{
		// Auto-Generated Properties from Templates
		
		[WProperty("ykgr", "Unknown_1", true, "", SourceScene.Room)]
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

		[WProperty("ykgr", "Unknown_2", true, "", SourceScene.Room)]
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

		override public void PopulateDefaultProperties()
		{
			base.PopulateDefaultProperties();
			Unknown_1 = -1;
			Unknown_2 = -1;
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
