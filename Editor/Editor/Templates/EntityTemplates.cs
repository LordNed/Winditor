// 
using GameFormatReader.Common;
using OpenTK;
using System.ComponentModel;
using System.Diagnostics;

namespace WindEditor
{
	public abstract class SerializableDOMNode : WDOMNode
	{
		public readonly string FourCC;
		public MapLayer Layer { get { return m_layer; } set { m_layer = value; OnPropertyChanged("Layer"); } }

		private MapLayer m_layer;

		public SerializableDOMNode(string fourCC, WWorld world) : base(world)
		{
			FourCC = fourCC;
			OnConstruction();
		}

		// Called by the constructor, override this if you want to put things in your own constructor in a partial class.
		public virtual void OnConstruction() {}

		// This is called after the data is loaded out of the disk. Use this if you need to post-process the loaded data.
		public virtual void PostLoad() {}

		public virtual void Load(EndianBinaryReader stream) {}
		public virtual void Save(EndianBinaryWriter stream) {}
	}

	public partial class VisibleDOMNode : SerializableDOMNode
	{
		public VisibleDOMNode(string fourCC, WWorld world) : base(fourCC, world)
		{
		}
	}
	 

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class MinimapSettings_Unused : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private Vector2 m_FullMapImageScale;
		public Vector2 FullMapImageScale
		{ 
			get { return m_FullMapImageScale; }
			set
			{
				m_FullMapImageScale = value;
				OnPropertyChanged("FullMapImageScale");
			}
		}
				

		private Vector2 m_FullMapSpaceScale;
		public Vector2 FullMapSpaceScale
		{ 
			get { return m_FullMapSpaceScale; }
			set
			{
				m_FullMapSpaceScale = value;
				OnPropertyChanged("FullMapSpaceScale");
			}
		}
				

		private Vector2 m_FullMapTranslation;
		public Vector2 FullMapTranslation
		{ 
			get { return m_FullMapTranslation; }
			set
			{
				m_FullMapTranslation = value;
				OnPropertyChanged("FullMapTranslation");
			}
		}
				

		private Vector2 m_ZoomedMapScrolling1;
		public Vector2 ZoomedMapScrolling1
		{ 
			get { return m_ZoomedMapScrolling1; }
			set
			{
				m_ZoomedMapScrolling1 = value;
				OnPropertyChanged("ZoomedMapScrolling1");
			}
		}
				

		private Vector2 m_ZoomedMapScrolling2;
		public Vector2 ZoomedMapScrolling2
		{ 
			get { return m_ZoomedMapScrolling2; }
			set
			{
				m_ZoomedMapScrolling2 = value;
				OnPropertyChanged("ZoomedMapScrolling2");
			}
		}
				

		private Vector2 m_ZoomedMapTranslation;
		public Vector2 ZoomedMapTranslation
		{ 
			get { return m_ZoomedMapTranslation; }
			set
			{
				m_ZoomedMapTranslation = value;
				OnPropertyChanged("ZoomedMapTranslation");
			}
		}
				

		private float m_ZoomedMapScale;
		public float ZoomedMapScale
		{ 
			get { return m_ZoomedMapScale; }
			set
			{
				m_ZoomedMapScale = value;
				OnPropertyChanged("ZoomedMapScale");
			}
		}
				

		private byte m_Unknown;
		public byte Unknown
		{ 
			get { return m_Unknown; }
			set
			{
				m_Unknown = value;
				OnPropertyChanged("Unknown");
			}
		}
				

		private byte m_MapImageIndex;
		public byte MapImageIndex
		{ 
			get { return m_MapImageIndex; }
			set
			{
				m_MapImageIndex = value;
				OnPropertyChanged("MapImageIndex");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Padding;
		public byte Padding
		{ 
			get { return m_Padding; }
			set
			{
				m_Padding = value;
				OnPropertyChanged("Padding");
			}
		}
				


		// Constructor
		public MinimapSettings_Unused(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_FullMapImageScale = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_FullMapSpaceScale = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_FullMapTranslation = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapScrolling1 = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapScrolling2 = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapTranslation = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapScale = stream.ReadSingle(); 
			m_Unknown = stream.ReadByte(); 
			m_MapImageIndex = stream.ReadByte(); 
			m_Unknown2 = stream.ReadByte(); 
			m_Padding = stream.ReadByte(); Trace.Assert(m_Padding == 0xFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class MinimapSettings : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private Vector2 m_FullMapImageScale;
		public Vector2 FullMapImageScale
		{ 
			get { return m_FullMapImageScale; }
			set
			{
				m_FullMapImageScale = value;
				OnPropertyChanged("FullMapImageScale");
			}
		}
				

		private Vector2 m_FullMapSpaceScale;
		public Vector2 FullMapSpaceScale
		{ 
			get { return m_FullMapSpaceScale; }
			set
			{
				m_FullMapSpaceScale = value;
				OnPropertyChanged("FullMapSpaceScale");
			}
		}
				

		private Vector2 m_FullMapTranslation;
		public Vector2 FullMapTranslation
		{ 
			get { return m_FullMapTranslation; }
			set
			{
				m_FullMapTranslation = value;
				OnPropertyChanged("FullMapTranslation");
			}
		}
				

		private Vector2 m_ZoomedMapScrolling1;
		public Vector2 ZoomedMapScrolling1
		{ 
			get { return m_ZoomedMapScrolling1; }
			set
			{
				m_ZoomedMapScrolling1 = value;
				OnPropertyChanged("ZoomedMapScrolling1");
			}
		}
				

		private Vector2 m_ZoomedMapScrolling2;
		public Vector2 ZoomedMapScrolling2
		{ 
			get { return m_ZoomedMapScrolling2; }
			set
			{
				m_ZoomedMapScrolling2 = value;
				OnPropertyChanged("ZoomedMapScrolling2");
			}
		}
				

		private Vector2 m_ZoomedMapTranslation;
		public Vector2 ZoomedMapTranslation
		{ 
			get { return m_ZoomedMapTranslation; }
			set
			{
				m_ZoomedMapTranslation = value;
				OnPropertyChanged("ZoomedMapTranslation");
			}
		}
				

		private float m_ZoomedMapScale;
		public float ZoomedMapScale
		{ 
			get { return m_ZoomedMapScale; }
			set
			{
				m_ZoomedMapScale = value;
				OnPropertyChanged("ZoomedMapScale");
			}
		}
				

		private byte m_Unknown;
		public byte Unknown
		{ 
			get { return m_Unknown; }
			set
			{
				m_Unknown = value;
				OnPropertyChanged("Unknown");
			}
		}
				

		private byte m_MapImageIndex;
		public byte MapImageIndex
		{ 
			get { return m_MapImageIndex; }
			set
			{
				m_MapImageIndex = value;
				OnPropertyChanged("MapImageIndex");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Padding;
		public byte Padding
		{ 
			get { return m_Padding; }
			set
			{
				m_Padding = value;
				OnPropertyChanged("Padding");
			}
		}
				


		// Constructor
		public MinimapSettings(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_FullMapImageScale = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_FullMapSpaceScale = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_FullMapTranslation = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapScrolling1 = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapScrolling2 = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapTranslation = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			m_ZoomedMapScale = stream.ReadSingle(); 
			m_Unknown = stream.ReadByte(); 
			m_MapImageIndex = stream.ReadByte(); 
			m_Unknown2 = stream.ReadByte(); 
			m_Padding = stream.ReadByte(); Trace.Assert(m_Padding == 0xFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Actor : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private int m_Parameters;
		public int Parameters
		{ 
			get { return m_Parameters; }
			set
			{
				m_Parameters = value;
				OnPropertyChanged("Parameters");
			}
		}
				

		private short m_FlagToSet;
		public short FlagToSet
		{ 
			get { return m_FlagToSet; }
			set
			{
				m_FlagToSet = value;
				OnPropertyChanged("FlagToSet");
			}
		}
				

		private short m_EnemyNumber;
		public short EnemyNumber
		{ 
			get { return m_EnemyNumber; }
			set
			{
				m_EnemyNumber = value;
				OnPropertyChanged("EnemyNumber");
			}
		}
				


		// Constructor
		public Actor(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_Parameters = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			float xRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion xRotQ = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(xRot));Transform.Rotation = Transform.Rotation * xRotQ; 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			m_FlagToSet = stream.ReadInt16(); 
			m_EnemyNumber = stream.ReadInt16(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class CameraViewpoint_v1 : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private short m_Padding;
				


		// Constructor
		public CameraViewpoint_v1(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			float xRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion xRotQ = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(xRot));Transform.Rotation = Transform.Rotation * xRotQ; 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			float zRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion zRotQ = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(zRot));Transform.Rotation = Transform.Rotation * zRotQ; 
			m_Padding = stream.ReadInt16(); Trace.Assert((ushort)m_Padding == 0xFFFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class CameraViewpoint_v2 : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private short m_Padding;
				


		// Constructor
		public CameraViewpoint_v2(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			float xRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion xRotQ = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(xRot));Transform.Rotation = Transform.Rotation * xRotQ; 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			float zRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion zRotQ = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(zRot));Transform.Rotation = Transform.Rotation * zRotQ; 
			m_Padding = stream.ReadInt16(); Trace.Assert((ushort)m_Padding == 0xFFFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class CameraType_v1 : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_CameraType;
		public string CameraType
		{ 
			get { return m_CameraType; }
			set
			{
				m_CameraType = value;
				OnPropertyChanged("CameraType");
			}
		}
				

		private short m_CameraPointIndex;
		public short CameraPointIndex
		{ 
			get { return m_CameraPointIndex; }
			set
			{
				m_CameraPointIndex = value;
				OnPropertyChanged("CameraPointIndex");
			}
		}
				

		private byte m_Padding1;
		public byte Padding1
		{ 
			get { return m_Padding1; }
			set
			{
				m_Padding1 = value;
				OnPropertyChanged("Padding1");
			}
		}
				

		private byte m_Padding2;
		public byte Padding2
		{ 
			get { return m_Padding2; }
			set
			{
				m_Padding2 = value;
				OnPropertyChanged("Padding2");
			}
		}
				

		private byte m_Padding3;
		public byte Padding3
		{ 
			get { return m_Padding3; }
			set
			{
				m_Padding3 = value;
				OnPropertyChanged("Padding3");
			}
		}
				


		// Constructor
		public CameraType_v1(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_CameraType = stream.ReadString(16).Trim(new[] { ' ' }); 
			m_CameraPointIndex = stream.ReadInt16(); 
			m_Padding1 = stream.ReadByte(); Trace.Assert(m_Padding1 == 0xFF || m_Padding1== 0); // Padding
			m_Padding2 = stream.ReadByte(); Trace.Assert(m_Padding2 == 0xFF || m_Padding2== 0); // Padding
			m_Padding3 = stream.ReadByte(); Trace.Assert(m_Padding3 == 0xFF || m_Padding3== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class CameraType_v2 : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_CameraType;
		public string CameraType
		{ 
			get { return m_CameraType; }
			set
			{
				m_CameraType = value;
				OnPropertyChanged("CameraType");
			}
		}
				

		private byte m_CameraPointIndex;
		public byte CameraPointIndex
		{ 
			get { return m_CameraPointIndex; }
			set
			{
				m_CameraPointIndex = value;
				OnPropertyChanged("CameraPointIndex");
			}
		}
				

		private byte m_Padding1;
		public byte Padding1
		{ 
			get { return m_Padding1; }
			set
			{
				m_Padding1 = value;
				OnPropertyChanged("Padding1");
			}
		}
				

		private byte m_Padding2;
		public byte Padding2
		{ 
			get { return m_Padding2; }
			set
			{
				m_Padding2 = value;
				OnPropertyChanged("Padding2");
			}
		}
				

		private byte m_Padding3;
		public byte Padding3
		{ 
			get { return m_Padding3; }
			set
			{
				m_Padding3 = value;
				OnPropertyChanged("Padding3");
			}
		}
				


		// Constructor
		public CameraType_v2(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_CameraType = stream.ReadString(16).Trim(new[] { ' ' }); 
			m_CameraPointIndex = stream.ReadByte(); 
			m_Padding1 = stream.ReadByte(); Trace.Assert(m_Padding1 == 0xFF || m_Padding1== 0); // Padding
			m_Padding2 = stream.ReadByte(); Trace.Assert(m_Padding2 == 0xFF || m_Padding2== 0); // Padding
			m_Padding3 = stream.ReadByte(); Trace.Assert(m_Padding3 == 0xFF || m_Padding3== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Door_DOOR : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private int m_Parameters;
		public int Parameters
		{ 
			get { return m_Parameters; }
			set
			{
				m_Parameters = value;
				OnPropertyChanged("Parameters");
			}
		}
				

		private short m_AuxiliaryParameters;
		public short AuxiliaryParameters
		{ 
			get { return m_AuxiliaryParameters; }
			set
			{
				m_AuxiliaryParameters = value;
				OnPropertyChanged("AuxiliaryParameters");
			}
		}
				

		private short m_Unknown1;
		public short Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				

		private short m_Unknown2;
		public short Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_ScaleX;
		public byte ScaleX
		{ 
			get { return m_ScaleX; }
			set
			{
				m_ScaleX = value;
				OnPropertyChanged("ScaleX");
			}
		}
				

		private byte m_ScaleY;
		public byte ScaleY
		{ 
			get { return m_ScaleY; }
			set
			{
				m_ScaleY = value;
				OnPropertyChanged("ScaleY");
			}
		}
				

		private byte m_ScaleZ;
		public byte ScaleZ
		{ 
			get { return m_ScaleZ; }
			set
			{
				m_ScaleZ = value;
				OnPropertyChanged("ScaleZ");
			}
		}
				

		private byte m_Padding;
				


		// Constructor
		public Door_DOOR(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_Parameters = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_AuxiliaryParameters = stream.ReadInt16(); 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			m_Unknown1 = stream.ReadInt16(); 
			m_Unknown2 = stream.ReadInt16(); 
			m_ScaleX = stream.ReadByte(); 
			m_ScaleY = stream.ReadByte(); 
			m_ScaleZ = stream.ReadByte(); 
			m_Padding = stream.ReadByte(); Trace.Assert(m_Padding == 0xFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class DungeonFloorSettings : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private float m_LowerBoundaryYHeight;
		public float LowerBoundaryYHeight
		{ 
			get { return m_LowerBoundaryYHeight; }
			set
			{
				m_LowerBoundaryYHeight = value;
				OnPropertyChanged("LowerBoundaryYHeight");
			}
		}
				

		private byte m_FloorNumber;
		public byte FloorNumber
		{ 
			get { return m_FloorNumber; }
			set
			{
				m_FloorNumber = value;
				OnPropertyChanged("FloorNumber");
			}
		}
				

		private byte m_IncludedRoom0;
		public byte IncludedRoom0
		{ 
			get { return m_IncludedRoom0; }
			set
			{
				m_IncludedRoom0 = value;
				OnPropertyChanged("IncludedRoom0");
			}
		}
				

		private byte m_IncludedRoom1;
		public byte IncludedRoom1
		{ 
			get { return m_IncludedRoom1; }
			set
			{
				m_IncludedRoom1 = value;
				OnPropertyChanged("IncludedRoom1");
			}
		}
				

		private byte m_IncludedRoom2;
		public byte IncludedRoom2
		{ 
			get { return m_IncludedRoom2; }
			set
			{
				m_IncludedRoom2 = value;
				OnPropertyChanged("IncludedRoom2");
			}
		}
				

		private byte m_IncludedRoom3;
		public byte IncludedRoom3
		{ 
			get { return m_IncludedRoom3; }
			set
			{
				m_IncludedRoom3 = value;
				OnPropertyChanged("IncludedRoom3");
			}
		}
				

		private byte m_IncludedRoom4;
		public byte IncludedRoom4
		{ 
			get { return m_IncludedRoom4; }
			set
			{
				m_IncludedRoom4 = value;
				OnPropertyChanged("IncludedRoom4");
			}
		}
				

		private byte m_IncludedRoom5;
		public byte IncludedRoom5
		{ 
			get { return m_IncludedRoom5; }
			set
			{
				m_IncludedRoom5 = value;
				OnPropertyChanged("IncludedRoom5");
			}
		}
				

		private byte m_IncludedRoom6;
		public byte IncludedRoom6
		{ 
			get { return m_IncludedRoom6; }
			set
			{
				m_IncludedRoom6 = value;
				OnPropertyChanged("IncludedRoom6");
			}
		}
				

		private byte m_IncludedRoom7;
		public byte IncludedRoom7
		{ 
			get { return m_IncludedRoom7; }
			set
			{
				m_IncludedRoom7 = value;
				OnPropertyChanged("IncludedRoom7");
			}
		}
				

		private byte m_IncludedRoom8;
		public byte IncludedRoom8
		{ 
			get { return m_IncludedRoom8; }
			set
			{
				m_IncludedRoom8 = value;
				OnPropertyChanged("IncludedRoom8");
			}
		}
				

		private byte m_IncludedRoom9;
		public byte IncludedRoom9
		{ 
			get { return m_IncludedRoom9; }
			set
			{
				m_IncludedRoom9 = value;
				OnPropertyChanged("IncludedRoom9");
			}
		}
				

		private byte m_IncludedRoom10;
		public byte IncludedRoom10
		{ 
			get { return m_IncludedRoom10; }
			set
			{
				m_IncludedRoom10 = value;
				OnPropertyChanged("IncludedRoom10");
			}
		}
				

		private byte m_IncludedRoom11;
		public byte IncludedRoom11
		{ 
			get { return m_IncludedRoom11; }
			set
			{
				m_IncludedRoom11 = value;
				OnPropertyChanged("IncludedRoom11");
			}
		}
				

		private byte m_IncludedRoom12;
		public byte IncludedRoom12
		{ 
			get { return m_IncludedRoom12; }
			set
			{
				m_IncludedRoom12 = value;
				OnPropertyChanged("IncludedRoom12");
			}
		}
				

		private byte m_IncludedRoom13;
		public byte IncludedRoom13
		{ 
			get { return m_IncludedRoom13; }
			set
			{
				m_IncludedRoom13 = value;
				OnPropertyChanged("IncludedRoom13");
			}
		}
				

		private byte m_IncludedRoom14;
		public byte IncludedRoom14
		{ 
			get { return m_IncludedRoom14; }
			set
			{
				m_IncludedRoom14 = value;
				OnPropertyChanged("IncludedRoom14");
			}
		}
				


		// Constructor
		public DungeonFloorSettings(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_LowerBoundaryYHeight = stream.ReadSingle(); 
			m_FloorNumber = stream.ReadByte(); 
			m_IncludedRoom0 = stream.ReadByte(); 
			m_IncludedRoom1 = stream.ReadByte(); 
			m_IncludedRoom2 = stream.ReadByte(); 
			m_IncludedRoom3 = stream.ReadByte(); 
			m_IncludedRoom4 = stream.ReadByte(); 
			m_IncludedRoom5 = stream.ReadByte(); 
			m_IncludedRoom6 = stream.ReadByte(); 
			m_IncludedRoom7 = stream.ReadByte(); 
			m_IncludedRoom8 = stream.ReadByte(); 
			m_IncludedRoom9 = stream.ReadByte(); 
			m_IncludedRoom10 = stream.ReadByte(); 
			m_IncludedRoom11 = stream.ReadByte(); 
			m_IncludedRoom12 = stream.ReadByte(); 
			m_IncludedRoom13 = stream.ReadByte(); 
			m_IncludedRoom14 = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class DungeonMapSettings : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private float m_MapSizeX;
		public float MapSizeX
		{ 
			get { return m_MapSizeX; }
			set
			{
				m_MapSizeX = value;
				OnPropertyChanged("MapSizeX");
			}
		}
				

		private float m_MapSizeY;
		public float MapSizeY
		{ 
			get { return m_MapSizeY; }
			set
			{
				m_MapSizeY = value;
				OnPropertyChanged("MapSizeY");
			}
		}
				

		private float m_MapScaleInverse;
		public float MapScaleInverse
		{ 
			get { return m_MapScaleInverse; }
			set
			{
				m_MapScaleInverse = value;
				OnPropertyChanged("MapScaleInverse");
			}
		}
				

		private float m_Unknown1;
		public float Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				


		// Constructor
		public DungeonMapSettings(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_MapSizeX = stream.ReadSingle(); 
			m_MapSizeY = stream.ReadSingle(); 
			m_MapScaleInverse = stream.ReadSingle(); 
			m_Unknown1 = stream.ReadSingle(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class LightSource : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private Vector3 m_Radius;
		public Vector3 Radius
		{ 
			get { return m_Radius; }
			set
			{
				m_Radius = value;
				OnPropertyChanged("Radius");
			}
		}
				

		private WLinearColor m_Color;
		public WLinearColor Color
		{ 
			get { return m_Color; }
			set
			{
				m_Color = value;
				OnPropertyChanged("Color");
			}
		}
				


		// Constructor
		public LightSource(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_Radius = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_Color = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class EnvironmentLightingConditions : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private byte m_ClearColorA;
		public byte ClearColorA
		{ 
			get { return m_ClearColorA; }
			set
			{
				m_ClearColorA = value;
				OnPropertyChanged("ClearColorA");
			}
		}
				

		private byte m_RainingColorA;
		public byte RainingColorA
		{ 
			get { return m_RainingColorA; }
			set
			{
				m_RainingColorA = value;
				OnPropertyChanged("RainingColorA");
			}
		}
				

		private byte m_SnowingA;
		public byte SnowingA
		{ 
			get { return m_SnowingA; }
			set
			{
				m_SnowingA = value;
				OnPropertyChanged("SnowingA");
			}
		}
				

		private byte m_UnknownA;
		public byte UnknownA
		{ 
			get { return m_UnknownA; }
			set
			{
				m_UnknownA = value;
				OnPropertyChanged("UnknownA");
			}
		}
				

		private byte m_ClearColorB;
		public byte ClearColorB
		{ 
			get { return m_ClearColorB; }
			set
			{
				m_ClearColorB = value;
				OnPropertyChanged("ClearColorB");
			}
		}
				

		private byte m_RainingColorB;
		public byte RainingColorB
		{ 
			get { return m_RainingColorB; }
			set
			{
				m_RainingColorB = value;
				OnPropertyChanged("RainingColorB");
			}
		}
				

		private byte m_SnowingB;
		public byte SnowingB
		{ 
			get { return m_SnowingB; }
			set
			{
				m_SnowingB = value;
				OnPropertyChanged("SnowingB");
			}
		}
				

		private byte m_UnknownB;
		public byte UnknownB
		{ 
			get { return m_UnknownB; }
			set
			{
				m_UnknownB = value;
				OnPropertyChanged("UnknownB");
			}
		}
				


		// Constructor
		public EnvironmentLightingConditions(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_ClearColorA = stream.ReadByte(); 
			m_RainingColorA = stream.ReadByte(); 
			m_SnowingA = stream.ReadByte(); 
			m_UnknownA = stream.ReadByte(); 
			m_ClearColorB = stream.ReadByte(); 
			m_RainingColorB = stream.ReadByte(); 
			m_SnowingB = stream.ReadByte(); 
			m_UnknownB = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class EnvironmentLightingColors : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private WLinearColor m_ShadowColor;
		public WLinearColor ShadowColor
		{ 
			get { return m_ShadowColor; }
			set
			{
				m_ShadowColor = value;
				OnPropertyChanged("ShadowColor");
			}
		}
				

		private WLinearColor m_ActorAmbientColor;
		public WLinearColor ActorAmbientColor
		{ 
			get { return m_ActorAmbientColor; }
			set
			{
				m_ActorAmbientColor = value;
				OnPropertyChanged("ActorAmbientColor");
			}
		}
				

		private WLinearColor m_RoomLightColor;
		public WLinearColor RoomLightColor
		{ 
			get { return m_RoomLightColor; }
			set
			{
				m_RoomLightColor = value;
				OnPropertyChanged("RoomLightColor");
			}
		}
				

		private WLinearColor m_RoomAmbientColor;
		public WLinearColor RoomAmbientColor
		{ 
			get { return m_RoomAmbientColor; }
			set
			{
				m_RoomAmbientColor = value;
				OnPropertyChanged("RoomAmbientColor");
			}
		}
				

		private WLinearColor m_WaveColor;
		public WLinearColor WaveColor
		{ 
			get { return m_WaveColor; }
			set
			{
				m_WaveColor = value;
				OnPropertyChanged("WaveColor");
			}
		}
				

		private WLinearColor m_OceanColor;
		public WLinearColor OceanColor
		{ 
			get { return m_OceanColor; }
			set
			{
				m_OceanColor = value;
				OnPropertyChanged("OceanColor");
			}
		}
				

		private WLinearColor m_UnknownWhite1;
		public WLinearColor UnknownWhite1
		{ 
			get { return m_UnknownWhite1; }
			set
			{
				m_UnknownWhite1 = value;
				OnPropertyChanged("UnknownWhite1");
			}
		}
				

		private WLinearColor m_UnknownWhite2;
		public WLinearColor UnknownWhite2
		{ 
			get { return m_UnknownWhite2; }
			set
			{
				m_UnknownWhite2 = value;
				OnPropertyChanged("UnknownWhite2");
			}
		}
				

		private WLinearColor m_DoorBackfill;
		public WLinearColor DoorBackfill
		{ 
			get { return m_DoorBackfill; }
			set
			{
				m_DoorBackfill = value;
				OnPropertyChanged("DoorBackfill");
			}
		}
				

		private WLinearColor m_Unknown3;
		public WLinearColor Unknown3
		{ 
			get { return m_Unknown3; }
			set
			{
				m_Unknown3 = value;
				OnPropertyChanged("Unknown3");
			}
		}
				

		private WLinearColor m_FogColor;
		public WLinearColor FogColor
		{ 
			get { return m_FogColor; }
			set
			{
				m_FogColor = value;
				OnPropertyChanged("FogColor");
			}
		}
				

		private byte m_SkyboxColorIndex;
		public byte SkyboxColorIndex
		{ 
			get { return m_SkyboxColorIndex; }
			set
			{
				m_SkyboxColorIndex = value;
				OnPropertyChanged("SkyboxColorIndex");
			}
		}
				

		private byte m_Padding1;
		public byte Padding1
		{ 
			get { return m_Padding1; }
			set
			{
				m_Padding1 = value;
				OnPropertyChanged("Padding1");
			}
		}
				

		private byte m_Padding2;
		public byte Padding2
		{ 
			get { return m_Padding2; }
			set
			{
				m_Padding2 = value;
				OnPropertyChanged("Padding2");
			}
		}
				

		private float m_FogFarPlane;
		public float FogFarPlane
		{ 
			get { return m_FogFarPlane; }
			set
			{
				m_FogFarPlane = value;
				OnPropertyChanged("FogFarPlane");
			}
		}
				

		private float m_ForNearPlane;
		public float ForNearPlane
		{ 
			get { return m_ForNearPlane; }
			set
			{
				m_ForNearPlane = value;
				OnPropertyChanged("ForNearPlane");
			}
		}
				


		// Constructor
		public EnvironmentLightingColors(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_ShadowColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_ActorAmbientColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_RoomLightColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_RoomAmbientColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_WaveColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_OceanColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_UnknownWhite1 = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_UnknownWhite2 = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_DoorBackfill = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_Unknown3 = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_FogColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_SkyboxColorIndex = stream.ReadByte(); 
			m_Padding1 = stream.ReadByte(); Trace.Assert(m_Padding1 == 0xFF || m_Padding1== 0); // Padding
			m_Padding2 = stream.ReadByte(); Trace.Assert(m_Padding2 == 0xFF || m_Padding2== 0); // Padding
			m_FogFarPlane = stream.ReadSingle(); 
			m_ForNearPlane = stream.ReadSingle(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class EnvironmentLightingSkyboxColors : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private WLinearColor m_Unknown1;
		public WLinearColor Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				

		private WLinearColor m_Unknown2;
		public WLinearColor Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private WLinearColor m_Unknown3;
		public WLinearColor Unknown3
		{ 
			get { return m_Unknown3; }
			set
			{
				m_Unknown3 = value;
				OnPropertyChanged("Unknown3");
			}
		}
				

		private WLinearColor m_Unknown4;
		public WLinearColor Unknown4
		{ 
			get { return m_Unknown4; }
			set
			{
				m_Unknown4 = value;
				OnPropertyChanged("Unknown4");
			}
		}
				

		private WLinearColor m_HorizonCloudColor;
		public WLinearColor HorizonCloudColor
		{ 
			get { return m_HorizonCloudColor; }
			set
			{
				m_HorizonCloudColor = value;
				OnPropertyChanged("HorizonCloudColor");
			}
		}
				

		private WLinearColor m_CenterCloudColor;
		public WLinearColor CenterCloudColor
		{ 
			get { return m_CenterCloudColor; }
			set
			{
				m_CenterCloudColor = value;
				OnPropertyChanged("CenterCloudColor");
			}
		}
				

		private WLinearColor m_SkyColor;
		public WLinearColor SkyColor
		{ 
			get { return m_SkyColor; }
			set
			{
				m_SkyColor = value;
				OnPropertyChanged("SkyColor");
			}
		}
				

		private WLinearColor m_FalseSeaColor;
		public WLinearColor FalseSeaColor
		{ 
			get { return m_FalseSeaColor; }
			set
			{
				m_FalseSeaColor = value;
				OnPropertyChanged("FalseSeaColor");
			}
		}
				

		private WLinearColor m_HorizonColor;
		public WLinearColor HorizonColor
		{ 
			get { return m_HorizonColor; }
			set
			{
				m_HorizonColor = value;
				OnPropertyChanged("HorizonColor");
			}
		}
				

		private byte m_Padding1;
		public byte Padding1
		{ 
			get { return m_Padding1; }
			set
			{
				m_Padding1 = value;
				OnPropertyChanged("Padding1");
			}
		}
				

		private byte m_Padding2;
		public byte Padding2
		{ 
			get { return m_Padding2; }
			set
			{
				m_Padding2 = value;
				OnPropertyChanged("Padding2");
			}
		}
				

		private byte m_Padding3;
		public byte Padding3
		{ 
			get { return m_Padding3; }
			set
			{
				m_Padding3 = value;
				OnPropertyChanged("Padding3");
			}
		}
				


		// Constructor
		public EnvironmentLightingSkyboxColors(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Unknown1 = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
			m_Unknown2 = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
			m_Unknown3 = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
			m_Unknown4 = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
			m_HorizonCloudColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
			m_CenterCloudColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
			m_SkyColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_FalseSeaColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_HorizonColor = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f); 
			m_Padding1 = stream.ReadByte(); Trace.Assert(m_Padding1 == 0xFF || m_Padding1== 0); // Padding
			m_Padding2 = stream.ReadByte(); Trace.Assert(m_Padding2 == 0xFF || m_Padding2== 0); // Padding
			m_Padding3 = stream.ReadByte(); Trace.Assert(m_Padding3 == 0xFF || m_Padding3== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class EnvironmentLightingTimesOfDay : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private byte m_DawnA;
		public byte DawnA
		{ 
			get { return m_DawnA; }
			set
			{
				m_DawnA = value;
				OnPropertyChanged("DawnA");
			}
		}
				

		private byte m_MorningA;
		public byte MorningA
		{ 
			get { return m_MorningA; }
			set
			{
				m_MorningA = value;
				OnPropertyChanged("MorningA");
			}
		}
				

		private byte m_NoonA;
		public byte NoonA
		{ 
			get { return m_NoonA; }
			set
			{
				m_NoonA = value;
				OnPropertyChanged("NoonA");
			}
		}
				

		private byte m_AfternoonA;
		public byte AfternoonA
		{ 
			get { return m_AfternoonA; }
			set
			{
				m_AfternoonA = value;
				OnPropertyChanged("AfternoonA");
			}
		}
				

		private byte m_DuskA;
		public byte DuskA
		{ 
			get { return m_DuskA; }
			set
			{
				m_DuskA = value;
				OnPropertyChanged("DuskA");
			}
		}
				

		private byte m_NightA;
		public byte NightA
		{ 
			get { return m_NightA; }
			set
			{
				m_NightA = value;
				OnPropertyChanged("NightA");
			}
		}
				

		private byte m_DawnB;
		public byte DawnB
		{ 
			get { return m_DawnB; }
			set
			{
				m_DawnB = value;
				OnPropertyChanged("DawnB");
			}
		}
				

		private byte m_MorningB;
		public byte MorningB
		{ 
			get { return m_MorningB; }
			set
			{
				m_MorningB = value;
				OnPropertyChanged("MorningB");
			}
		}
				

		private byte m_NoonB;
		public byte NoonB
		{ 
			get { return m_NoonB; }
			set
			{
				m_NoonB = value;
				OnPropertyChanged("NoonB");
			}
		}
				

		private byte m_AfternoonB;
		public byte AfternoonB
		{ 
			get { return m_AfternoonB; }
			set
			{
				m_AfternoonB = value;
				OnPropertyChanged("AfternoonB");
			}
		}
				

		private byte m_DuskB;
		public byte DuskB
		{ 
			get { return m_DuskB; }
			set
			{
				m_DuskB = value;
				OnPropertyChanged("DuskB");
			}
		}
				

		private byte m_NightB;
		public byte NightB
		{ 
			get { return m_NightB; }
			set
			{
				m_NightB = value;
				OnPropertyChanged("NightB");
			}
		}
				


		// Constructor
		public EnvironmentLightingTimesOfDay(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_DawnA = stream.ReadByte(); 
			m_MorningA = stream.ReadByte(); 
			m_NoonA = stream.ReadByte(); 
			m_AfternoonA = stream.ReadByte(); 
			m_DuskA = stream.ReadByte(); 
			m_NightA = stream.ReadByte(); 
			m_DawnB = stream.ReadByte(); 
			m_MorningB = stream.ReadByte(); 
			m_NoonB = stream.ReadByte(); 
			m_AfternoonB = stream.ReadByte(); 
			m_DuskB = stream.ReadByte(); 
			m_NightB = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class MapEvent : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private byte m_Unknown1;
		public byte Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				

		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Unknown3;
		public byte Unknown3
		{ 
			get { return m_Unknown3; }
			set
			{
				m_Unknown3 = value;
				OnPropertyChanged("Unknown3");
			}
		}
				

		private byte m_Unknown4;
		public byte Unknown4
		{ 
			get { return m_Unknown4; }
			set
			{
				m_Unknown4 = value;
				OnPropertyChanged("Unknown4");
			}
		}
				

		private byte m_Unknown5;
		public byte Unknown5
		{ 
			get { return m_Unknown5; }
			set
			{
				m_Unknown5 = value;
				OnPropertyChanged("Unknown5");
			}
		}
				

		private byte m_RoomNumber;
		public byte RoomNumber
		{ 
			get { return m_RoomNumber; }
			set
			{
				m_RoomNumber = value;
				OnPropertyChanged("RoomNumber");
			}
		}
				

		private byte m_Padding1;
		public byte Padding1
		{ 
			get { return m_Padding1; }
			set
			{
				m_Padding1 = value;
				OnPropertyChanged("Padding1");
			}
		}
				

		private byte m_Padding2;
		public byte Padding2
		{ 
			get { return m_Padding2; }
			set
			{
				m_Padding2 = value;
				OnPropertyChanged("Padding2");
			}
		}
				

		private byte m_Padding3;
		public byte Padding3
		{ 
			get { return m_Padding3; }
			set
			{
				m_Padding3 = value;
				OnPropertyChanged("Padding3");
			}
		}
				


		// Constructor
		public MapEvent(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Unknown1 = stream.ReadByte(); 
			m_Name = stream.ReadString(15).Trim(new[] { ' ' }); 
			m_Unknown2 = stream.ReadByte(); 
			m_Unknown3 = stream.ReadByte(); 
			m_Unknown4 = stream.ReadByte(); 
			m_Unknown5 = stream.ReadByte(); 
			m_RoomNumber = stream.ReadByte(); 
			m_Padding1 = stream.ReadByte(); Trace.Assert(m_Padding1 == 0xFF || m_Padding1== 0); // Padding
			m_Padding2 = stream.ReadByte(); Trace.Assert(m_Padding2 == 0xFF || m_Padding2== 0); // Padding
			m_Padding3 = stream.ReadByte(); Trace.Assert(m_Padding3 == 0xFF || m_Padding3== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ExitData : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_MapName;
		public string MapName
		{ 
			get { return m_MapName; }
			set
			{
				m_MapName = value;
				OnPropertyChanged("MapName");
			}
		}
				

		private byte m_SpawnIndex;
		public byte SpawnIndex
		{ 
			get { return m_SpawnIndex; }
			set
			{
				m_SpawnIndex = value;
				OnPropertyChanged("SpawnIndex");
			}
		}
				

		private byte m_RoomIndex;
		public byte RoomIndex
		{ 
			get { return m_RoomIndex; }
			set
			{
				m_RoomIndex = value;
				OnPropertyChanged("RoomIndex");
			}
		}
				

		private byte m_FadeOutType;
		public byte FadeOutType
		{ 
			get { return m_FadeOutType; }
			set
			{
				m_FadeOutType = value;
				OnPropertyChanged("FadeOutType");
			}
		}
				

		private byte m_Padding;
		public byte Padding
		{ 
			get { return m_Padding; }
			set
			{
				m_Padding = value;
				OnPropertyChanged("Padding");
			}
		}
				


		// Constructor
		public ExitData(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_MapName = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_SpawnIndex = stream.ReadByte(); 
			m_RoomIndex = stream.ReadByte(); 
			m_FadeOutType = stream.ReadByte(); 
			m_Padding = stream.ReadByte(); Trace.Assert(m_Padding == 0xFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class CutsceneIndexBank : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private byte m_Unknown;
		public byte Unknown
		{ 
			get { return m_Unknown; }
			set
			{
				m_Unknown = value;
				OnPropertyChanged("Unknown");
			}
		}
				


		// Constructor
		public CutsceneIndexBank(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Unknown = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class LightVector : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private Vector3 m_Radius;
		public Vector3 Radius
		{ 
			get { return m_Radius; }
			set
			{
				m_Radius = value;
				OnPropertyChanged("Radius");
			}
		}
				

		private WLinearColor m_Color;
		public WLinearColor Color
		{ 
			get { return m_Color; }
			set
			{
				m_Color = value;
				OnPropertyChanged("Color");
			}
		}
				


		// Constructor
		public LightVector(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_Radius = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_Color = new WLinearColor(stream.ReadByte() / 255f, stream.ReadByte() / 255f, stream.ReadByte()/255f, stream.ReadByte()/255f); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class MemoryCO : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private byte m_Room;
		public byte Room
		{ 
			get { return m_Room; }
			set
			{
				m_Room = value;
				OnPropertyChanged("Room");
			}
		}
				

		private byte m_Entry;
		public byte Entry
		{ 
			get { return m_Entry; }
			set
			{
				m_Entry = value;
				OnPropertyChanged("Entry");
			}
		}
				


		// Constructor
		public MemoryCO(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Room = stream.ReadByte(); 
			m_Entry = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class RoomMemoryManagement : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private int m_SizeInBytes;
		public int SizeInBytes
		{ 
			get { return m_SizeInBytes; }
			set
			{
				m_SizeInBytes = value;
				OnPropertyChanged("SizeInBytes");
			}
		}
				


		// Constructor
		public RoomMemoryManagement(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_SizeInBytes = stream.ReadInt32(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class SpawnPoint : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private byte m_EventIndex;
		public byte EventIndex
		{ 
			get { return m_EventIndex; }
			set
			{
				m_EventIndex = value;
				OnPropertyChanged("EventIndex");
			}
		}
				

		private byte m_Unknown1;
		public byte Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				

		private byte m_SpawnType;
		public byte SpawnType
		{ 
			get { return m_SpawnType; }
			set
			{
				m_SpawnType = value;
				OnPropertyChanged("SpawnType");
			}
		}
				

		private byte m_Room;
		public byte Room
		{ 
			get { return m_Room; }
			set
			{
				m_Room = value;
				OnPropertyChanged("Room");
			}
		}
				

		private short m_Unknown2;
		public short Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Unknown3;
		public byte Unknown3
		{ 
			get { return m_Unknown3; }
			set
			{
				m_Unknown3 = value;
				OnPropertyChanged("Unknown3");
			}
		}
				

		private byte m_SpawnIndex;
		public byte SpawnIndex
		{ 
			get { return m_SpawnIndex; }
			set
			{
				m_SpawnIndex = value;
				OnPropertyChanged("SpawnIndex");
			}
		}
				

		private short m_Unknown4;
		public short Unknown4
		{ 
			get { return m_Unknown4; }
			set
			{
				m_Unknown4 = value;
				OnPropertyChanged("Unknown4");
			}
		}
				


		// Constructor
		public SpawnPoint(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_EventIndex = stream.ReadByte(); 
			m_Unknown1 = stream.ReadByte(); 
			m_SpawnType = stream.ReadByte(); 
			m_Room = stream.ReadByte(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_Unknown2 = stream.ReadInt16(); 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			m_Unknown3 = stream.ReadByte(); 
			m_SpawnIndex = stream.ReadByte(); 
			m_Unknown4 = stream.ReadInt16(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class RoomProperties : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private int m_Parameters;
		public int Parameters
		{ 
			get { return m_Parameters; }
			set
			{
				m_Parameters = value;
				OnPropertyChanged("Parameters");
			}
		}
				

		private float m_SkyboxYHeight;
		public float SkyboxYHeight
		{ 
			get { return m_SkyboxYHeight; }
			set
			{
				m_SkyboxYHeight = value;
				OnPropertyChanged("SkyboxYHeight");
			}
		}
				


		// Constructor
		public RoomProperties(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Parameters = stream.ReadInt32(); 
			m_SkyboxYHeight = stream.ReadSingle(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class RoomModelTranslation : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private Vector2 m_Translation;
		public Vector2 Translation
		{ 
			get { return m_Translation; }
			set
			{
				m_Translation = value;
				OnPropertyChanged("Translation");
			}
		}
				

		private byte m_Room;
		public byte Room
		{ 
			get { return m_Room; }
			set
			{
				m_Room = value;
				OnPropertyChanged("Room");
			}
		}
				

		private byte m_WaveHeightAddition;
		public byte WaveHeightAddition
		{ 
			get { return m_WaveHeightAddition; }
			set
			{
				m_WaveHeightAddition = value;
				OnPropertyChanged("WaveHeightAddition");
			}
		}
				


		// Constructor
		public RoomModelTranslation(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Translation = new OpenTK.Vector2(stream.ReadSingle(), stream.ReadSingle()); 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			m_Room = stream.ReadByte(); 
			m_WaveHeightAddition = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class RoomTable : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private int m_Offset;
		public int Offset
		{ 
			get { return m_Offset; }
			set
			{
				m_Offset = value;
				OnPropertyChanged("Offset");
			}
		}
				


		// Constructor
		public RoomTable(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Offset = stream.ReadInt32(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ScaleableObject : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private byte m_Parameter1;
		public byte Parameter1
		{ 
			get { return m_Parameter1; }
			set
			{
				m_Parameter1 = value;
				OnPropertyChanged("Parameter1");
			}
		}
				

		private byte m_Parameter2;
		public byte Parameter2
		{ 
			get { return m_Parameter2; }
			set
			{
				m_Parameter2 = value;
				OnPropertyChanged("Parameter2");
			}
		}
				

		private byte m_Parameter3;
		public byte Parameter3
		{ 
			get { return m_Parameter3; }
			set
			{
				m_Parameter3 = value;
				OnPropertyChanged("Parameter3");
			}
		}
				

		private byte m_Parameter4;
		public byte Parameter4
		{ 
			get { return m_Parameter4; }
			set
			{
				m_Parameter4 = value;
				OnPropertyChanged("Parameter4");
			}
		}
				

		private short m_AuxiliaryParameter;
		public short AuxiliaryParameter
		{ 
			get { return m_AuxiliaryParameter; }
			set
			{
				m_AuxiliaryParameter = value;
				OnPropertyChanged("AuxiliaryParameter");
			}
		}
				

		private short m_Unknown1;
		public short Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				

		private short m_Unknown2;
		public short Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Padding;
				


		// Constructor
		public ScaleableObject(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_Parameter1 = stream.ReadByte(); 
			m_Parameter2 = stream.ReadByte(); 
			m_Parameter3 = stream.ReadByte(); 
			m_Parameter4 = stream.ReadByte(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_AuxiliaryParameter = stream.ReadInt16(); 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			m_Unknown1 = stream.ReadInt16(); 
			m_Unknown2 = stream.ReadInt16(); 
			float xScale = stream.ReadByte() / 10f;Transform.LocalScale = new Vector3(xScale, Transform.LocalScale.Y, Transform.LocalScale.Z); 
			float yScale = stream.ReadByte() / 10f;Transform.LocalScale = new Vector3(Transform.LocalScale.X, yScale, Transform.LocalScale.Z); 
			float zScale = stream.ReadByte() / 10f;Transform.LocalScale = new Vector3(Transform.LocalScale.X, Transform.LocalScale.Y, zScale); 
			m_Padding = stream.ReadByte(); Trace.Assert(m_Padding == 0xFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class ShipSpawnPoint : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private float /*single axis rotation */ m_Rotation;
		public float /*single axis rotation */ Rotation
		{ 
			get { return m_Rotation; }
			set
			{
				m_Rotation = value;
				OnPropertyChanged("Rotation");
			}
		}
				

		private byte m_ShipId;
		public byte ShipId
		{ 
			get { return m_ShipId; }
			set
			{
				m_ShipId = value;
				OnPropertyChanged("ShipId");
			}
		}
				

		private byte m_Unknown1;
		public byte Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				


		// Constructor
		public ShipSpawnPoint(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_Rotation = stream.ReadInt16(); 
			m_ShipId = stream.ReadByte(); 
			m_Unknown1 = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class SoundEffect : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private byte m_Unknown1;
		public byte Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Unknown3;
		public byte Unknown3
		{ 
			get { return m_Unknown3; }
			set
			{
				m_Unknown3 = value;
				OnPropertyChanged("Unknown3");
			}
		}
				

		private byte m_SoundID;
		public byte SoundID
		{ 
			get { return m_SoundID; }
			set
			{
				m_SoundID = value;
				OnPropertyChanged("SoundID");
			}
		}
				

		private byte m_PathIndex;
		public byte PathIndex
		{ 
			get { return m_PathIndex; }
			set
			{
				m_PathIndex = value;
				OnPropertyChanged("PathIndex");
			}
		}
				

		private byte m_Padding1;
		public byte Padding1
		{ 
			get { return m_Padding1; }
			set
			{
				m_Padding1 = value;
				OnPropertyChanged("Padding1");
			}
		}
				

		private byte m_Padding2;
		public byte Padding2
		{ 
			get { return m_Padding2; }
			set
			{
				m_Padding2 = value;
				OnPropertyChanged("Padding2");
			}
		}
				

		private byte m_Padding3;
		public byte Padding3
		{ 
			get { return m_Padding3; }
			set
			{
				m_Padding3 = value;
				OnPropertyChanged("Padding3");
			}
		}
				


		// Constructor
		public SoundEffect(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_Unknown1 = stream.ReadByte(); 
			m_Unknown2 = stream.ReadByte(); 
			m_Unknown3 = stream.ReadByte(); 
			m_SoundID = stream.ReadByte(); 
			m_PathIndex = stream.ReadByte(); 
			m_Padding1 = stream.ReadByte(); Trace.Assert(m_Padding1 == 0xFF || m_Padding1== 0); // Padding
			m_Padding2 = stream.ReadByte(); Trace.Assert(m_Padding2 == 0xFF || m_Padding2== 0); // Padding
			m_Padding3 = stream.ReadByte(); Trace.Assert(m_Padding3 == 0xFF || m_Padding3== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class StageProperties : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private float m_ZDepthMin;
		public float ZDepthMin
		{ 
			get { return m_ZDepthMin; }
			set
			{
				m_ZDepthMin = value;
				OnPropertyChanged("ZDepthMin");
			}
		}
				

		private float m_ZDepthMax;
		public float ZDepthMax
		{ 
			get { return m_ZDepthMax; }
			set
			{
				m_ZDepthMax = value;
				OnPropertyChanged("ZDepthMax");
			}
		}
				

		private short m_StageID;
		public short StageID
		{ 
			get { return m_StageID; }
			set
			{
				m_StageID = value;
				OnPropertyChanged("StageID");
			}
		}
				

		private short m_Unk_ParticleBank;
		public short Unk_ParticleBank
		{ 
			get { return m_Unk_ParticleBank; }
			set
			{
				m_Unk_ParticleBank = value;
				OnPropertyChanged("Unk_ParticleBank");
			}
		}
				

		private short m_Unk_PropertyIndex;
		public short Unk_PropertyIndex
		{ 
			get { return m_Unk_PropertyIndex; }
			set
			{
				m_Unk_PropertyIndex = value;
				OnPropertyChanged("Unk_PropertyIndex");
			}
		}
				

		private byte m_Unknown1;
		public byte Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Unknown3;
		public byte Unknown3
		{ 
			get { return m_Unknown3; }
			set
			{
				m_Unknown3 = value;
				OnPropertyChanged("Unknown3");
			}
		}
				

		private byte m_Unknown4;
		public byte Unknown4
		{ 
			get { return m_Unknown4; }
			set
			{
				m_Unknown4 = value;
				OnPropertyChanged("Unknown4");
			}
		}
				

		private short m_Unk_DrawRange;
		public short Unk_DrawRange
		{ 
			get { return m_Unk_DrawRange; }
			set
			{
				m_Unk_DrawRange = value;
				OnPropertyChanged("Unk_DrawRange");
			}
		}
				


		// Constructor
		public StageProperties(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_ZDepthMin = stream.ReadSingle(); 
			m_ZDepthMax = stream.ReadSingle(); 
			m_StageID = stream.ReadInt16(); 
			m_Unk_ParticleBank = stream.ReadInt16(); 
			m_Unk_PropertyIndex = stream.ReadInt16(); 
			m_Unknown1 = stream.ReadByte(); 
			m_Unknown2 = stream.ReadByte(); 
			m_Unknown3 = stream.ReadByte(); 
			m_Unknown4 = stream.ReadByte(); 
			m_Unk_DrawRange = stream.ReadInt16(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Door_TGDR : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private int m_Params;
		public int Params
		{ 
			get { return m_Params; }
			set
			{
				m_Params = value;
				OnPropertyChanged("Params");
			}
		}
				

		private short m_RoomLoadingParams;
		public short RoomLoadingParams
		{ 
			get { return m_RoomLoadingParams; }
			set
			{
				m_RoomLoadingParams = value;
				OnPropertyChanged("RoomLoadingParams");
			}
		}
				

		private byte m_Arg1;
		public byte Arg1
		{ 
			get { return m_Arg1; }
			set
			{
				m_Arg1 = value;
				OnPropertyChanged("Arg1");
			}
		}
				

		private byte m_ShipId;
		public byte ShipId
		{ 
			get { return m_ShipId; }
			set
			{
				m_ShipId = value;
				OnPropertyChanged("ShipId");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_Unknown3;
		public byte Unknown3
		{ 
			get { return m_Unknown3; }
			set
			{
				m_Unknown3 = value;
				OnPropertyChanged("Unknown3");
			}
		}
				

		private byte m_Unknown4;
		public byte Unknown4
		{ 
			get { return m_Unknown4; }
			set
			{
				m_Unknown4 = value;
				OnPropertyChanged("Unknown4");
			}
		}
				


		// Constructor
		public Door_TGDR(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_Params = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_RoomLoadingParams = stream.ReadInt16(); 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			m_Arg1 = stream.ReadByte(); 
			m_ShipId = stream.ReadByte(); 
			m_Unknown2 = stream.ReadByte(); 
			m_Unknown3 = stream.ReadByte(); 
			float xScale = stream.ReadByte() / 10f;Transform.LocalScale = new Vector3(xScale, Transform.LocalScale.Y, Transform.LocalScale.Z); 
			float yScale = stream.ReadByte() / 10f;Transform.LocalScale = new Vector3(Transform.LocalScale.X, yScale, Transform.LocalScale.Z); 
			float zScale = stream.ReadByte() / 10f;Transform.LocalScale = new Vector3(Transform.LocalScale.X, Transform.LocalScale.Y, zScale); 
			m_Unknown4 = stream.ReadByte(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class TagObject : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private int m_Parameters;
		public int Parameters
		{ 
			get { return m_Parameters; }
			set
			{
				m_Parameters = value;
				OnPropertyChanged("Parameters");
			}
		}
				

		private short m_Padding;
		public short Padding
		{ 
			get { return m_Padding; }
			set
			{
				m_Padding = value;
				OnPropertyChanged("Padding");
			}
		}
				


		// Constructor
		public TagObject(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_Parameters = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			float xRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion xRotQ = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(xRot));Transform.Rotation = Transform.Rotation * xRotQ; 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			float zRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion zRotQ = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(zRot));Transform.Rotation = Transform.Rotation * zRotQ; 
			m_Padding = stream.ReadInt16(); Trace.Assert((ushort)m_Padding == 0xFFFF || m_Padding== 0); // Padding
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class TagScaleableObject : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private int m_Params1;
		public int Params1
		{ 
			get { return m_Params1; }
			set
			{
				m_Params1 = value;
				OnPropertyChanged("Params1");
			}
		}
				

		private short m_RoomLoadingParams;
		public short RoomLoadingParams
		{ 
			get { return m_RoomLoadingParams; }
			set
			{
				m_RoomLoadingParams = value;
				OnPropertyChanged("RoomLoadingParams");
			}
		}
				

		private int m_Params2;
		public int Params2
		{ 
			get { return m_Params2; }
			set
			{
				m_Params2 = value;
				OnPropertyChanged("Params2");
			}
		}
				


		// Constructor
		public TagScaleableObject(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_Params1 = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_RoomLoadingParams = stream.ReadInt16(); 
			float xRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion xRotQ = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(xRot));Transform.Rotation = Transform.Rotation * xRotQ; 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			float zRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion zRotQ = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(zRot));Transform.Rotation = Transform.Rotation * zRotQ; 
			m_Params2 = stream.ReadInt32(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class TreasureChest : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private string m_Name;
		public string Name
		{ 
			get { return m_Name; }
			set
			{
				m_Name = value;
				OnPropertyChanged("Name");
			}
		}
				

		private int m_Params;
		public int Params
		{ 
			get { return m_Params; }
			set
			{
				m_Params = value;
				OnPropertyChanged("Params");
			}
		}
				

		private short m_RoomIndex;
		public short RoomIndex
		{ 
			get { return m_RoomIndex; }
			set
			{
				m_RoomIndex = value;
				OnPropertyChanged("RoomIndex");
			}
		}
				

		private int m_Params2;
		public int Params2
		{ 
			get { return m_Params2; }
			set
			{
				m_Params2 = value;
				OnPropertyChanged("Params2");
			}
		}
				


		// Constructor
		public TreasureChest(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Name = stream.ReadString(8).Trim(new[] { ' ' }); 
			m_Params = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
			m_RoomIndex = stream.ReadInt16(); 
			float yRot = WMath.RotationShortToFloat(stream.ReadInt16());Quaternion yRotQ = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(yRot));Transform.Rotation = Transform.Rotation * yRotQ; 
			m_Params2 = stream.ReadInt32(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Path_v1 : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private short m_NumberofPoints;
		public short NumberofPoints
		{ 
			get { return m_NumberofPoints; }
			set
			{
				m_NumberofPoints = value;
				OnPropertyChanged("NumberofPoints");
			}
		}
				

		private short m_NextPathIndex;
		public short NextPathIndex
		{ 
			get { return m_NextPathIndex; }
			set
			{
				m_NextPathIndex = value;
				OnPropertyChanged("NextPathIndex");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_PathLoops;
		public byte PathLoops
		{ 
			get { return m_PathLoops; }
			set
			{
				m_PathLoops = value;
				OnPropertyChanged("PathLoops");
			}
		}
				

		private short m_Unknown4;
		public short Unknown4
		{ 
			get { return m_Unknown4; }
			set
			{
				m_Unknown4 = value;
				OnPropertyChanged("Unknown4");
			}
		}
				

		private int m_FirstEntryOffset;
		public int FirstEntryOffset
		{ 
			get { return m_FirstEntryOffset; }
			set
			{
				m_FirstEntryOffset = value;
				OnPropertyChanged("FirstEntryOffset");
			}
		}
				


		// Constructor
		public Path_v1(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_NumberofPoints = stream.ReadInt16(); 
			m_NextPathIndex = stream.ReadInt16(); 
			m_Unknown2 = stream.ReadByte(); 
			m_PathLoops = stream.ReadByte(); 
			m_Unknown4 = stream.ReadInt16(); 
			m_FirstEntryOffset = stream.ReadInt32(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class Path_v2 : SerializableDOMNode
	{
		// Auto-Generated Properties from Templates
		private short m_NumberofPoints;
		public short NumberofPoints
		{ 
			get { return m_NumberofPoints; }
			set
			{
				m_NumberofPoints = value;
				OnPropertyChanged("NumberofPoints");
			}
		}
				

		private short m_NextPathIndex;
		public short NextPathIndex
		{ 
			get { return m_NextPathIndex; }
			set
			{
				m_NextPathIndex = value;
				OnPropertyChanged("NextPathIndex");
			}
		}
				

		private byte m_Unknown2;
		public byte Unknown2
		{ 
			get { return m_Unknown2; }
			set
			{
				m_Unknown2 = value;
				OnPropertyChanged("Unknown2");
			}
		}
				

		private byte m_PathLoops;
		public byte PathLoops
		{ 
			get { return m_PathLoops; }
			set
			{
				m_PathLoops = value;
				OnPropertyChanged("PathLoops");
			}
		}
				

		private short m_Unknown4;
		public short Unknown4
		{ 
			get { return m_Unknown4; }
			set
			{
				m_Unknown4 = value;
				OnPropertyChanged("Unknown4");
			}
		}
				

		private int m_FirstEntryOffset;
		public int FirstEntryOffset
		{ 
			get { return m_FirstEntryOffset; }
			set
			{
				m_FirstEntryOffset = value;
				OnPropertyChanged("FirstEntryOffset");
			}
		}
				


		// Constructor
		public Path_v2(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_NumberofPoints = stream.ReadInt16(); 
			m_NextPathIndex = stream.ReadInt16(); 
			m_Unknown2 = stream.ReadByte(); 
			m_PathLoops = stream.ReadByte(); 
			m_Unknown4 = stream.ReadInt16(); 
			m_FirstEntryOffset = stream.ReadInt32(); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class PathPoint_v1 : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private int m_Unknown1;
		public int Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				


		// Constructor
		public PathPoint_v1(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Unknown1 = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
		}
	}

	// AUTO-GENERATED, MODIFICATIONS TO THIS FILE WILL BE LOST
	public partial class PathPoint_v2 : VisibleDOMNode
	{
		// Auto-Generated Properties from Templates
		private int m_Unknown1;
		public int Unknown1
		{ 
			get { return m_Unknown1; }
			set
			{
				m_Unknown1 = value;
				OnPropertyChanged("Unknown1");
			}
		}
				


		// Constructor
		public PathPoint_v2(string fourCC, WWorld world) : base(fourCC, world)
		{
		}

		override public void Load(EndianBinaryReader stream)
		{
			m_Unknown1 = stream.ReadInt32(); 
			Transform.Position = new OpenTK.Vector3(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle()); 
		}
	}


} // namespace WindEditor

