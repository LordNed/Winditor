using GameFormatReader.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WindEditor
{
    public enum PropertyValueType
    {
        Byte,
        Bool,
        Short,
        Int,
        Float,
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

    struct ChunkHeader
    {
        /// <summary> FourCC Tag of the Chunk </summary>
        public FourCC FourCC;
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

        public ChunkHeader(FourCC fourCC, int elementCount, int chunkOffset)
        {
            Layer = MapLayer.Default;
            FourCC = fourCC;
            ElementCount = elementCount;
            ChunkOffset = chunkOffset;
        }

        // ACTR, SCOB, and TRES support multiple layers in the form of the first three letters of
        // the entity type, and then [0-9, A, B] as the last one.
        public static MapLayer FourCCToLayer(ref string fourCC)
        {
            MapLayer layer = MapLayer.Default;
            if (fourCC.StartsWith("ACT") || fourCC.StartsWith("SCO") || fourCC.StartsWith("TRE"))
            {
                char lastChar = fourCC[3];
                switch (lastChar)
                {
                    case '0': layer = MapLayer.Layer0; break;
                    case '1': layer = MapLayer.Layer1; break;
                    case '2': layer = MapLayer.Layer2; break;
                    case '3': layer = MapLayer.Layer3; break;
                    case '4': layer = MapLayer.Layer4; break;
                    case '5': layer = MapLayer.Layer5; break;
                    case '6': layer = MapLayer.Layer6; break;
                    case '7': layer = MapLayer.Layer7; break;
                    case '8': layer = MapLayer.Layer8; break;
                    case '9': layer = MapLayer.Layer9; break;
                    case 'a': layer = MapLayer.LayerA; break;
                    case 'b': layer = MapLayer.LayerB; break;
                }

                // Fix up their FourCC names.
                if (fourCC.StartsWith("ACT")) fourCC = "ACTR";
                if (fourCC.StartsWith("TRE")) fourCC = "TRES";
                if (fourCC.StartsWith("SCO")) fourCC = "SCOB";
            }

            return layer;
        }

        public static string LayerToFourCC(string fourCC, MapLayer layer)
        {
            if (fourCC.StartsWith("ACT") || fourCC.StartsWith("SCO") || fourCC.StartsWith("TRE"))
            {
                string firstThree = fourCC.Substring(0, 3);
                switch (layer)
                {
                    default:
                    case MapLayer.Default: break;
                    case MapLayer.Layer0: fourCC = firstThree + '0'; break;
                    case MapLayer.Layer1: fourCC = firstThree + '1'; break;
                    case MapLayer.Layer2: fourCC = firstThree + '2'; break;
                    case MapLayer.Layer3: fourCC = firstThree + '3'; break;
                    case MapLayer.Layer4: fourCC = firstThree + '4'; break;
                    case MapLayer.Layer5: fourCC = firstThree + '5'; break;
                    case MapLayer.Layer6: fourCC = firstThree + '6'; break;
                    case MapLayer.Layer7: fourCC = firstThree + '7'; break;
                    case MapLayer.Layer8: fourCC = firstThree + '8'; break;
                    case MapLayer.Layer9: fourCC = firstThree + '9'; break;
                    case MapLayer.LayerA: fourCC = firstThree + 'a'; break;
                    case MapLayer.LayerB: fourCC = firstThree + 'b'; break;
                }
            }

            return fourCC;
        }
    }

#pragma warning disable 0649
    public class MapActorDescriptor
    {
        public FourCC FourCC;
		public string ClassName;
		public string ParentClassOverride;
        public DataDescriptorField[] Fields;
    }

    public class DataDescriptorField
    {
        [JsonProperty("Name")]
        public string FieldName { get; set; }

        [JsonProperty("Type")]
        public PropertyValueType FieldType { get; set; }

		[JsonProperty("Hidden")]
		public bool Hidden { get; set; }

        [JsonProperty("Category")]
        public string CategoryName { get; set; }

        [JsonProperty("Editable")]
        public bool IsEditable { get; set; }

        public uint Length;

        [JsonConstructor]
        public DataDescriptorField(string Name, PropertyValueType Type, bool IsHidden, string Category, bool Editable)
        {
            FieldName = Name;
            FieldType = Type;
            CategoryName = Category;
            Hidden = IsHidden;
            IsEditable = Editable;
        }
    }
#pragma warning restore 0649

    class SceneDataLoader
    {
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

        private List<ChunkHeader> m_chunkList;
        private EndianBinaryReader m_reader;
        private WWorld m_world;

        public SceneDataLoader(string fileName, WWorld world)
        {
            m_world = world;
            m_reader = new EndianBinaryReader(File.ReadAllBytes(fileName), System.Text.Encoding.ASCII, Endian.Big);
            m_chunkList = new List<ChunkHeader>();
            int chunkCount = m_reader.ReadInt32();

            for (int i = 0; i < chunkCount; i++)
            {
                string fourCC = m_reader.ReadString(4);
                MapLayer layer = ChunkHeader.FourCCToLayer(ref fourCC);

                FourCC enumFourCC = FourCCConversion.GetEnumFromString(fourCC);
                ChunkHeader chunk = new ChunkHeader(enumFourCC, m_reader.ReadInt32(), m_reader.ReadInt32());
                chunk.Layer = layer;

                m_chunkList.Add(chunk);
            }

            var sortedList = m_chunkList.OrderBy(x => x.ChunkOffset);
            m_chunkList = new List<ChunkHeader>(sortedList);
        }

        ~SceneDataLoader()
        {
            if (m_reader != null)
                m_reader.Dispose();
            m_reader = null;
        }

        public List<WDOMNode> GetMapEntities()
        {
            var loadedActors = new List<WDOMNode>();
            foreach (var chunk in m_chunkList)
            {
                m_reader.BaseStream.Position = chunk.ChunkOffset;
                MapActorDescriptor template = Globals.ActorDescriptors.Find(x => x.FourCC == chunk.FourCC);
                if (template == null)
                {
                    Console.WriteLine("Unsupported FourCC: {0}", chunk.FourCC);
                    continue;
                }

                switch (chunk.FourCC)
                {
                    // Don't turn these into map actors, as they will be handled elsewhere.
                    //case "RTBL":
                    case FourCC.MECO:
                    case FourCC.MEMA:
                        break;
                    default:
                        for (int i = 0; i < chunk.ElementCount; i++)
                        {
							Type actorType = Type.GetType($"WindEditor.{template.ClassName}");
							SerializableDOMNode entity = (SerializableDOMNode)Activator.CreateInstance(actorType, chunk.FourCC, m_world);
							entity.Load(m_reader);
							entity.PostLoad();
							entity.Layer = chunk.Layer;

                            loadedActors.Add(entity);
                        }
                        break;
                }
            }

            // var dict = new Dictionary<string, List<WDOMNode>>();
            // foreach(var actor in loadedActors)
            // {
            //     if (!dict.ContainsKey(actor.FourCC))
            //         dict[actor.FourCC] = new List<WDOMNode>();
            //     dict[actor.FourCC].Add(actor);
            // }
            // 
            // string[] nodes = new[] { "EnvR", "Pale", "Virt", "Colo" };
            // foreach(var node in nodes)
            // {
            //     if (dict.ContainsKey(node))
            //         Console.WriteLine("{0} Count: {1}", node, dict[node].Count);
            // 
            // }

            AssignPaths(loadedActors);

            return loadedActors;
        }

        public void AssignPaths(List<WDOMNode> loaded_actors)
        {
            List<WDOMNode> v1_paths = loaded_actors.FindAll(x => x.GetType() == typeof(Path_v1));
            List<WDOMNode> v1_points = loaded_actors.FindAll(x => x.GetType() == typeof(PathPoint_v1));

            foreach (WDOMNode path_v1 in v1_paths)
            {
                Path_v1 cur_path = (Path_v1)path_v1;
                cur_path.SetNodes(v1_points);
            }

            List<WDOMNode> v2_paths = loaded_actors.FindAll(x => x.GetType() == typeof(Path_v2));
            List<WDOMNode> v2_points = loaded_actors.FindAll(x => x.GetType() == typeof(PathPoint_v2));

            foreach (WDOMNode path_v2 in v2_paths)
            {
                Path_v2 cur_path = (Path_v2)path_v2;
                cur_path.SetNodes(v2_points);
            }
        }

        public List<WRoomTable> GetRoomTable()
        {
            List<WRoomTable> roomTables = new List<WRoomTable>();

            int rtblIndex = m_chunkList.FindIndex(x => x.FourCC == FourCC.RTBL);
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

            int multIndex = m_chunkList.FindIndex(x => x.FourCC == FourCC.MULT);
            if (multIndex >= 0)
            {
                ChunkHeader rtbl = m_chunkList[multIndex];
                m_reader.BaseStream.Position = rtbl.ChunkOffset;

                for (int i = 0; i < rtbl.ElementCount; i++)
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

            int mecoIndex = m_chunkList.FindIndex(x => x.FourCC == FourCC.MECO);
            int memaIndex = m_chunkList.FindIndex(x => x.FourCC == FourCC.MEMA);
            if (mecoIndex >= 0 && memaIndex >= 0)
            {
                ChunkHeader meco = m_chunkList[mecoIndex];
                ChunkHeader mema = m_chunkList[memaIndex];

                int[] memaEntries = new int[mema.ElementCount];
                m_reader.BaseStream.Position = mema.ChunkOffset;
                for (int i = 0; i < mema.ElementCount; i++)
                    memaEntries[i] = m_reader.ReadInt32();

                m_reader.BaseStream.Position = meco.ChunkOffset;
                for (int i = 0; i < meco.ElementCount; i++)
                {
                    MemoryAlloc memAlloc = new MemoryAlloc(m_reader.ReadByte(), memaEntries[m_reader.ReadByte()]);
                    memAllocTable.Add(memAlloc);
                }
            }

            return memAllocTable;
        }
    }
}