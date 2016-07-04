using GameFormatReader.Common;
using Newtonsoft.Json;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WindEditor
{
    public enum PropertyValueType
    {
        Byte,
        Short,
        Int,
        Float,
        Bool,
        String,
        FixedLengthString,
        Vector2,
        Vector3,
        XRotation,
        YRotation,
        ZRotation,
        Color24,
        Color32,
    }

    class SceneDataLoader
    {
        struct ChunkHeader
        {
            /// <summary> FourCC Tag of the Chunk </summary>
            public string FourCC;
            /// <summary> How many elements of this type exist. </summary>
            public int ElementCount;
            /// <summary> Offset from the start of the file to the chunk data. </summary>
            public int ChunkOffset;

            /// <summary>
            // Used to fix up ACTR, TRES, and SCOB which can support up to 12 layers (+base)
            // this is resolved at chunk load time and then stored in the chunk and passed
            // to the entities being created.
            /// </summary>
            public MapLayer Layer;

            public override string ToString()
            {
                return string.Format("[{0}] #{1}", FourCC, ElementCount);
            }

            public ChunkHeader(string fourCC, int elementCount, int chunkOffset)
            {
                // ACTR, SCOB, and TRES support multiple layers in the form of the first three letters of
                // the entity type, and then [0-9, A, B] as the last one.
                FourCC = fourCC;
                Layer = MapLayer.Default;
                ElementCount = elementCount;
                ChunkOffset = chunkOffset;

                if (FourCC.StartsWith("ACT") || FourCC.StartsWith("SCO") || FourCC.StartsWith("TRE"))
                {
                    char lastChar = FourCC[3];
                    switch (lastChar)
                    {
                        case '1': Layer = MapLayer.Layer1; break;
                        case '2': Layer = MapLayer.Layer2; break;
                        case '3': Layer = MapLayer.Layer3; break;
                        case '4': Layer = MapLayer.Layer4; break;
                        case '5': Layer = MapLayer.Layer5; break;
                        case '6': Layer = MapLayer.Layer6; break;
                        case '7': Layer = MapLayer.Layer7; break;
                        case '8': Layer = MapLayer.Layer8; break;
                        case '9': Layer = MapLayer.Layer9; break;
                        case 'a': Layer = MapLayer.LayerA; break;
                        case 'b': Layer = MapLayer.LayerB; break;
                        case '0': Layer = MapLayer.Layer0; break;
                    }

                    // Fix up their FourCC names.
                    if (FourCC.StartsWith("ACT")) FourCC = "ACTR";
                    if (FourCC.StartsWith("TRE")) FourCC = "TRES";
                    if (FourCC.StartsWith("SCO")) FourCC = "SCOB";
                }
            }
        }

        public struct MemoryAlloc
        {
            public byte RoomIndex;
            public int MemorySize;

            public MemoryAlloc(byte roomIndex, int memorySize)
            {
                RoomIndex = roomIndex;
                MemorySize = memorySize;
            }

            public override string ToString()
            {
                return string.Format("Room Index: {0} Size: {1} bytes", RoomIndex, MemorySize);
            }
        }
#pragma warning disable 0649
        public class MapActorDescriptor
        {
            public string FourCC;
            public List<DataDescriptorField> Fields;
        }

        public class DataDescriptorField
        {
            [JsonProperty("Name")]
            public string FieldName;

            [JsonProperty("Type")]
            public PropertyValueType FieldType;

            public uint Length;
        }
#pragma warning restore 0649

        private static List<MapActorDescriptor> m_sActorDescriptors;

        private List<ChunkHeader> m_chunkList;
        private EndianBinaryReader m_reader;

        public SceneDataLoader(string fileName)
        {
            if (m_sActorDescriptors == null)
            {
                // Load the Actor Descriptors from disk.
                m_sActorDescriptors = new List<MapActorDescriptor>();
                foreach (var file in Directory.GetFiles("resources/templates/"))
                {
                    MapActorDescriptor descriptor = JsonConvert.DeserializeObject<MapActorDescriptor>(File.ReadAllText(file));
                    m_sActorDescriptors.Add(descriptor);
                }
            }

            m_reader = new EndianBinaryReader(File.ReadAllBytes(fileName), System.Text.Encoding.ASCII, Endian.Big);
            m_chunkList = new List<ChunkHeader>();
            int chunkCount = m_reader.ReadInt32();

            for (int i = 0; i < chunkCount; i++)
            {
                ChunkHeader chunk = new ChunkHeader(m_reader.ReadString(4), m_reader.ReadInt32(), m_reader.ReadInt32());
                m_chunkList.Add(chunk);
            }
        }

        ~SceneDataLoader()
        {
            if (m_reader != null)
                m_reader.Dispose();
            m_reader = null;
        }

        public List<WRoomTable> GetRoomTable()
        {
            List<WRoomTable> roomTables = new List<WRoomTable>();

            int rtblIndex = m_chunkList.FindIndex(x => x.FourCC == "RTBL");
            if (rtblIndex >= 0)
            {
                ChunkHeader rtbl = m_chunkList[rtblIndex];
                m_reader.BaseStream.Position = rtbl.ChunkOffset;

                int[] rtableOffsets = new int[rtbl.ElementCount];
                for (int i = 0; i < rtableOffsets.Length; i++)
                    rtableOffsets[i] = m_reader.ReadInt32();

                // Jump to the RTBL entries.
                for (int i = 0; i < rtableOffsets.Length; i++)
                {
                    m_reader.BaseStream.Position = rtableOffsets[i];

                    WRoomTable roomTable = new WRoomTable();
                    roomTables.Add(roomTable);

                    byte numRooms = m_reader.ReadByte();
                    roomTable.ReverbAmount = m_reader.ReadByte();
                    roomTable.TimePass = m_reader.ReadByte();
                    roomTable.Unknown1 = m_reader.ReadByte();

                    int tableOffset = m_reader.ReadInt32();
                    Console.WriteLine("i: {4} numRooms: {0} reverbAmount: {1} TimePass: {2} unknown1: {3}:", numRooms, roomTable.ReverbAmount, roomTable.TimePass, roomTable.Unknown1, i);

                    m_reader.BaseStream.Position = tableOffset;
                    for (int j = 0; j < numRooms; j++)
                    {
                        byte val = m_reader.ReadByte();

                        bool loadRoom = ((val & 0x80) >> 7) == 1;
                        bool unknownBit = ((val & 0x7F) >> 6) == 1;
                        byte roomId = (byte)(val & 0x3F);

                        roomTable.AdjacentRooms.Add(new WRoomTable.AdjacentRoom(loadRoom, unknownBit, roomId));
                        Console.WriteLine("\tLoad Room: {0} Unknown Bit: {1} Room: {2}", loadRoom, unknownBit, roomId);
                    }
                }
            }

            return roomTables;
        }

        public List<WRoomTransform> GetRoomTransformTable()
        {
            List<WRoomTransform> roomTransforms = new List<WRoomTransform>();

            int multIndex = m_chunkList.FindIndex(x => x.FourCC == "MULT");
            if (multIndex >= 0)
            {
                ChunkHeader rtbl = m_chunkList[multIndex];
                m_reader.BaseStream.Position = rtbl.ChunkOffset;

                for(int i = 0; i < rtbl.ElementCount; i++)
                {
                    WRoomTransform roomTransform = new WRoomTransform(new Vector2(m_reader.ReadSingle(), m_reader.ReadSingle()), WMath.RotationShortToFloat(m_reader.ReadInt16()), m_reader.ReadByte(), m_reader.ReadByte());
                    roomTransforms.Add(roomTransform);
                }
            }

            return roomTransforms;
        }

        public List<MemoryAlloc> GetRoomMemAllocTable()
        {
            List<MemoryAlloc> memAllocTable = new List<MemoryAlloc>();

            int mecoIndex = m_chunkList.FindIndex(x => x.FourCC == "MECO");
            int memaIndex = m_chunkList.FindIndex(x => x.FourCC == "MEMA");
            if(mecoIndex >= 0 && memaIndex >= 0)
            {
                ChunkHeader meco = m_chunkList[mecoIndex];
                ChunkHeader mema = m_chunkList[memaIndex];

                int[] memaEntries = new int[mema.ElementCount];
                m_reader.BaseStream.Position = mema.ChunkOffset;
                for (int i = 0; i < mema.ElementCount; i++)
                    memaEntries[i] = m_reader.ReadInt32();

                m_reader.BaseStream.Position = meco.ChunkOffset;
                for(int i = 0; i < meco.ElementCount; i++)
                {
                    MemoryAlloc memAlloc = new MemoryAlloc(m_reader.ReadByte(), memaEntries[m_reader.ReadByte()]);
                    memAllocTable.Add(memAlloc);
                }
            }

            return memAllocTable;
        }

        public List<WActorNode> GetMapEntities()
        {
            var loadedActors = new List<WActorNode>();
            foreach (var chunk in m_chunkList)
            {
                m_reader.BaseStream.Position = chunk.ChunkOffset;
                MapActorDescriptor template = m_sActorDescriptors.Find(x => x.FourCC == chunk.FourCC);
                if (template == null)
                {
                    Console.WriteLine("Unsupported FourCC: {0}", chunk.FourCC);
                    continue;
                }

                switch (chunk.FourCC)
                {
                    // Don't turn these into map actors, as they will be handled elsewhere.
                    //case "RTBL":
                    case "MECO":
                    case "MEMA":
                        break;
                    default:
                        for (int i = 0; i < chunk.ElementCount; i++)
                        {
                            var newActor = LoadActorFromChunk(chunk.FourCC, template);
                            newActor.Layer = chunk.Layer;

                            loadedActors.Add(newActor);
                        }
                        break;
                }

            }

            return loadedActors;
        }

        private WActorNode LoadActorFromChunk(string fourCC, MapActorDescriptor template)
        {
            var newActor = new WActorNode(fourCC);
            foreach (var field in template.Fields)
            {
                IPropertyValue propValue = null;

                switch (field.FieldType)
                {
                    case PropertyValueType.Bool:
                        propValue = new TBoolPropertyValue(m_reader.ReadBoolean(), field.FieldName);
                        break;
                    case PropertyValueType.Byte:
                        propValue = new TBytePropertyValue(m_reader.ReadByte(), field.FieldName);
                        break;
                    case PropertyValueType.Short:
                        propValue = new TShortPropertyValue(m_reader.ReadInt16(), field.FieldName);
                        break;
                    case PropertyValueType.Int:
                        propValue = new TIntPropertyValue(m_reader.ReadInt32(), field.FieldName);
                        break;
                    case PropertyValueType.Float:
                        propValue = new TFloatPropertyValue(m_reader.ReadSingle(), field.FieldName);
                        break;
                    case PropertyValueType.FixedLengthString:
                    case PropertyValueType.String:
                        string stringVal = (field.Length == 0) ? m_reader.ReadStringUntil('\0') : m_reader.ReadString(field.Length);
                        stringVal = stringVal.Trim(new[] { '\0' });
                        propValue = new TStringPropertyValue(stringVal, field.FieldName);
                        break;
                    case PropertyValueType.Vector2:
                        propValue = new TVector2PropertyValue(new OpenTK.Vector2(m_reader.ReadSingle(), m_reader.ReadSingle()), field.FieldName);
                        break;
                    case PropertyValueType.Vector3:
                        propValue = new TVector3PropertyValue(new OpenTK.Vector3(m_reader.ReadSingle(), m_reader.ReadSingle(), m_reader.ReadSingle()), field.FieldName);
                        break;
                    case PropertyValueType.XRotation:
                    case PropertyValueType.YRotation:
                    case PropertyValueType.ZRotation:
                        propValue = new TShortPropertyValue(m_reader.ReadInt16(), field.FieldName);
                        break;
                    case PropertyValueType.Color24:
                        propValue = new TLinearColorPropertyValue(new WLinearColor(m_reader.ReadByte() / 255f, m_reader.ReadByte() / 255f, m_reader.ReadByte() / 255f), field.FieldName);
                        break;
                    case PropertyValueType.Color32:
                        propValue = new TLinearColorPropertyValue(new WLinearColor(m_reader.ReadByte() / 255f, m_reader.ReadByte() / 255f, m_reader.ReadByte() / 255f, m_reader.ReadByte() / 255f), field.FieldName);
                        break;
                    default:
                        Console.WriteLine("Unsupported PropertyValueType: {0}", field.FieldType);
                        break;
                }

                // Post Fixup...
                if (string.Compare(field.FieldName, "Position", true) == 0)
                {
                    newActor.Transform.Position = (OpenTK.Vector3)propValue.GetValue();
                }
                else if (string.Compare(field.FieldName, "X Rotation", true) == 0)
                {
                    float xRotation = WMath.RotationShortToFloat((short)propValue.GetValue());
                    OpenTK.Quaternion xAxis = OpenTK.Quaternion.FromAxisAngle(new OpenTK.Vector3(1, 0, 0), WMath.DegreesToRadians(xRotation));
                    newActor.Transform.Rotation *= xAxis;
                }
                else if (string.Compare(field.FieldName, "Y Rotation", true) == 0)
                {
                    float yRotation = WMath.RotationShortToFloat((short)propValue.GetValue());
                    OpenTK.Quaternion yAxis = OpenTK.Quaternion.FromAxisAngle(new OpenTK.Vector3(0, 1, 0), WMath.DegreesToRadians(yRotation));
                    newActor.Transform.Rotation *= yAxis;
                }
                else if (string.Compare(field.FieldName, "Z Rotation", true) == 0)
                {
                    float zRotation = WMath.RotationShortToFloat((short)propValue.GetValue());
                    OpenTK.Quaternion zAxis = OpenTK.Quaternion.FromAxisAngle(new OpenTK.Vector3(0, 0, 1), WMath.DegreesToRadians(zRotation));
                    newActor.Transform.Rotation *= zAxis;
                }
                else if (string.Compare(field.FieldName, "X Scale", true) == 0)
                {
                    float xScale = (byte)propValue.GetValue();
                    var curScale = newActor.Transform.LocalScale;
                    newActor.Transform.LocalScale = new OpenTK.Vector3(xScale, curScale.Y, curScale.Z);
                }
                else if (string.Compare(field.FieldName, "Y Scale", true) == 0)
                {
                    float yScale = (byte)propValue.GetValue();
                    var curScale = newActor.Transform.LocalScale;
                    newActor.Transform.LocalScale = new OpenTK.Vector3(curScale.X, yScale, curScale.Z);
                }
                else if (string.Compare(field.FieldName, "Z Scale", true) == 0)
                {
                    float zScale = (byte)propValue.GetValue();
                    var curScale = newActor.Transform.LocalScale;
                    newActor.Transform.LocalScale = new OpenTK.Vector3(curScale.X, curScale.Y, zScale);
                }
                else
                {
                    newActor.Properties.Add(propValue);
                }
            }

            newActor.PostFinishedLoad();
            return newActor;
        }
    }
}