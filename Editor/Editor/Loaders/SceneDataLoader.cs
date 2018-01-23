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
        public List<DataDescriptorField> Fields;
    }

    public class DataDescriptorField
    {
        [JsonProperty("Name")]
        public string FieldName;

        [JsonProperty("Type")]
        public PropertyValueType FieldType;

		[JsonProperty("Hidden")]
		public bool Hidden;

        public uint Length;
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

        private static List<MapActorDescriptor> m_sActorDescriptors;

        private List<ChunkHeader> m_chunkList;
        private EndianBinaryReader m_reader;
        private WWorld m_world;

        public SceneDataLoader(string fileName, WWorld world)
        {
            m_world = world;

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

            return loadedActors;
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

        public List<EnvironmentLighting> GetLightingData()
        {
			return new List<EnvironmentLighting>();
            // Bah...
            //var loadedActors = new List<WActorNode>();
            //foreach (var chunk in m_chunkList)
            //{
            //    m_reader.BaseStream.Position = chunk.ChunkOffset;
            //    MapActorDescriptor template = m_sActorDescriptors.Find(x => x.FourCC == chunk.FourCC);
            //    if (template == null)
            //    {
            //        Console.WriteLine("Unsupported FourCC: {0}", chunk.FourCC);
            //        continue;
            //    }

            //    switch (chunk.FourCC)
            //    {
            //        // We're only going to re-load the lighting-based actors...
            //        case "EnvR":
            //        case "Colo":
            //        case "Pale":
            //        case "Virt":
            //            for (int i = 0; i < chunk.ElementCount; i++)
            //            {
            //                var newActor = LoadActorFromChunk(chunk.FourCC, template);
            //                newActor.Layer = chunk.Layer;

            //                loadedActors.Add(newActor);
            //            }
            //            break;
            //    }
            //}

            //var dict = new Dictionary<string, List<WActorNode>>();
            //foreach (var actor in loadedActors)
            //{
            //    if (!dict.ContainsKey(actor.FourCC))
            //        dict[actor.FourCC] = new List<WActorNode>();
            //    dict[actor.FourCC].Add(actor);
            //}

            //// Load Skybox Lighting Data
            //var virtList = new List<LightingSkyboxColors>();
            //var paleList = new List<LightingPalette>();
            //var coloList = new List<LightingTimePreset>();

            //if (dict.ContainsKey("Virt") && dict.ContainsKey("Pale") && dict.ContainsKey("Colo"))
            //{
            //    foreach (var virt in dict["Virt"])
            //    {
            //        WLinearColor unknown1, unknown2, unknown3, unknown4, horizonCloud, centerCloud, sky, falseSea, horizon;

            //        virt.TryGetValue("Unknown 1", out unknown1);
            //        virt.TryGetValue("Unknown 2", out unknown2);
            //        virt.TryGetValue("Unknown 3", out unknown3);
            //        virt.TryGetValue("Unknown 4", out unknown4);
            //        virt.TryGetValue("Horizon Cloud Color", out horizonCloud);
            //        virt.TryGetValue("Center Cloud Color", out centerCloud);
            //        virt.TryGetValue("Sky Color", out sky);
            //        virt.TryGetValue("False Sea Color", out falseSea);
            //        virt.TryGetValue("Horizon Color", out horizon);

            //        LightingSkyboxColors virtEntry = new LightingSkyboxColors
            //        {
            //            Unknown1 = unknown1,
            //            Unknown2 = unknown2,
            //            Unknown3 = unknown3,
            //            Unknown4 = unknown4,
            //            HorizonCloud = horizonCloud,
            //            CenterCloud = centerCloud,
            //            Sky = sky,
            //            FalseSea = falseSea,
            //            Horizon = horizon
            //        };

            //        virtList.Add(virtEntry);
            //    }

            //    foreach (var pale in dict["Pale"])
            //    {
            //        WLinearColor shadow, actorAmbient, roomLight, roomAmbient, wave, ocean, unknown1, unknown2, doorway, unknown3, fog;
            //        byte virtIndex; float fogFarPlane, fogNearPlane;
            //        pale.TryGetValue("Shadow Color", out shadow);
            //        pale.TryGetValue("Actor Ambient Color", out actorAmbient);
            //        pale.TryGetValue("Room Light Color", out roomLight);
            //        pale.TryGetValue("Room Ambient Color", out roomAmbient);
            //        pale.TryGetValue("Wave Color", out wave);
            //        pale.TryGetValue("Ocean Color", out ocean);
            //        pale.TryGetValue("Unknown White 1", out unknown1);
            //        pale.TryGetValue("Unknown White 2", out unknown2);
            //        pale.TryGetValue("Door Backfill", out doorway);
            //        pale.TryGetValue("Unknown 3", out unknown3);
            //        pale.TryGetValue("Fog Color", out fog);
            //        pale.TryGetValue("Skybox Color Index", out virtIndex);
            //        pale.TryGetValue("Fog Far Plane", out fogFarPlane);
            //        pale.TryGetValue("Fog Near Plane", out fogNearPlane);

            //        LightingPalette lightPalette = new LightingPalette
            //        {
            //            Shadow = shadow,
            //            ActorAmbient = actorAmbient,
            //            RoomLight = roomLight,
            //            RoomAmbient = roomAmbient,
            //            WaveColor = wave,
            //            OceanColor = ocean,
            //            UnknownWhite1 = unknown1,
            //            UnknownWhite2 = unknown2,
            //            Doorway = doorway,
            //            UnknownColor3 = unknown3,
            //            Skybox = virtList[virtIndex],
            //            Fog = fog,
            //            FogNearPlane = fogNearPlane,
            //            FogFarPlane = fogFarPlane,
            //        };

            //        paleList.Add(lightPalette);
            //    }

            //    foreach (var colo in dict["Colo"])
            //    {
            //        byte[] setA = new byte[6];
            //        byte[] setB = new byte[6];

            //        colo.TryGetValue("Dawn A", out setA[0]);
            //        colo.TryGetValue("Morning A", out setA[1]);
            //        colo.TryGetValue("Noon A", out setA[2]);
            //        colo.TryGetValue("Afternoon A", out setA[3]);
            //        colo.TryGetValue("Dusk A", out setA[4]);
            //        colo.TryGetValue("Night A", out setA[5]);

            //        colo.TryGetValue("Dawn B", out setB[0]);
            //        colo.TryGetValue("Morning B", out setB[1]);
            //        colo.TryGetValue("Noon B", out setB[2]);
            //        colo.TryGetValue("Afternoon B", out setB[3]);
            //        colo.TryGetValue("Dusk B", out setB[4]);
            //        colo.TryGetValue("Night B", out setB[5]);

            //        LightingTimePreset timePreset = new LightingTimePreset();
            //        for (int i = 0; i < 6; i++)
            //            timePreset.TimePresetA[i] = paleList[setA[i]];

            //        //for (int i = 0; i < 6; i++)
            //        //timePreset.TimePresetB[i] = paleList[setB[i]];
            //        coloList.Add(timePreset);
            //    }
            //}
            

            //var envrList = new List<EnvironmentLighting>();
            //if(paleList.Count > 0 && coloList.Count > 0)
            //{
            //    foreach (var envr in dict["EnvR"])
            //    {
            //        byte[] setA = new byte[4];
            //        byte[] setB = new byte[4];

            //        envr.TryGetValue("Clear Color A", out setA[0]);
            //        envr.TryGetValue("Raining Color A", out setA[1]);
            //        envr.TryGetValue("Snowing A", out setA[2]);
            //        envr.TryGetValue("Unknown A", out setA[3]);

            //        envr.TryGetValue("Clear Color B", out setB[0]);
            //        envr.TryGetValue("Raining Color B", out setB[1]);
            //        envr.TryGetValue("Snowing B", out setB[2]);
            //        envr.TryGetValue("Unknown B", out setB[3]);

            //        EnvironmentLighting envrPreset = new EnvironmentLighting();
            //        for (int i = 0; i < 4; i++)
            //            envrPreset.WeatherA[i] = coloList[setA[i]];

            //        for (int i = 0; i < 4; i++)
            //            envrPreset.WeatherB[i] = coloList[setB[i]];
            //        envrList.Add(envrPreset);
            //    }
            //}

            // return envrList;
        }
    }
}