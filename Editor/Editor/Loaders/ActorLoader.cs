using GameFormatReader.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindEditor
{
    public enum MapLayer
    {
        Default,
        Layer0,
        Layer1,
        Layer2,
        Layer3,
        Layer4,
        Layer5,
        Layer6,
        Layer7,
        Layer8,
        Layer9,
        LayerA,
        LayerB,
    }

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
        YRotation,
    }

    class ActorLoader
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

                if(FourCC.StartsWith("ACT") || FourCC.StartsWith("SCO") || FourCC.StartsWith("TRE"))
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

        private List<MapActorDescriptor> m_actorDescriptors;

        public ActorLoader()
        {
            // Load the Actor Descriptors from disk.
            m_actorDescriptors = new List<MapActorDescriptor>();
            foreach(var file in Directory.GetFiles("resources/templates/"))
            {
                MapActorDescriptor descriptor = JsonConvert.DeserializeObject<MapActorDescriptor>(File.ReadAllText(file));
                m_actorDescriptors.Add(descriptor);
            }
        }

        public List<WMapActor> LoadFromFile(string fileName)
        {
            List<WMapActor> loadedActors = new List<WMapActor>();

            using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(fileName), Endian.Big))
            {
                int chunkCount = reader.ReadInt32();

                List<ChunkHeader> chunkHeaders = new List<ChunkHeader>();
                for(int i = 0; i < chunkCount; i++)
                {
                    ChunkHeader chunk = new ChunkHeader(reader.ReadString(4), reader.ReadInt32(), reader.ReadInt32());
                    chunkHeaders.Add(chunk);
                }

                foreach(var chunk in chunkHeaders)
                {
                    reader.BaseStream.Position = chunk.ChunkOffset;
                    MapActorDescriptor template = m_actorDescriptors.Find(x => x.FourCC == chunk.FourCC);
                    if(template == null)
                    {
                        Console.WriteLine("Unsupported FourCC: {0}", chunk.FourCC);
                        continue;
                    }

                    for(int i = 0; i < chunk.ElementCount; i++)
                    {
                        WMapActor newActor = LoadActorFromChunk(chunk.FourCC, reader, template);
                        newActor.Layer = chunk.Layer;

                        loadedActors.Add(newActor);
                    }
                }
            }

            return loadedActors;
        }

        private WMapActor LoadActorFromChunk(string fourCC, EndianBinaryReader reader, MapActorDescriptor template)
        {
            WMapActor newActor = new WMapActor();
            foreach(var field in template.Fields)
            {
                IPropertyValue propValue = null;

                switch (field.FieldType)
                {
                    case PropertyValueType.Bool:
                        propValue = new TBoolPropertyValue(reader.ReadBoolean(), field.FieldName);
                        break;
                    case PropertyValueType.Byte:
                        propValue = new TBytePropertyValue(reader.ReadByte(), field.FieldName);
                        break;
                    case PropertyValueType.Short:
                        propValue = new TShortPropertyValue(reader.ReadInt16(), field.FieldName);
                        break;
                    case PropertyValueType.Int:
                        propValue = new TIntPropertyValue(reader.ReadInt32(), field.FieldName);
                        break;
                    case PropertyValueType.Float:
                        propValue = new TFloatPropertyValue(reader.ReadSingle(), field.FieldName);
                        break;
                    case PropertyValueType.FixedLengthString:
                    case PropertyValueType.String:
                        string stringVal = (field.Length == 0) ? reader.ReadStringUntil('\0') : reader.ReadString(field.Length);
                        stringVal = stringVal.Trim(new[] { '\0' });
                        propValue = new TStringPropertyValue(stringVal, field.FieldName);
                        break;
                    case PropertyValueType.Vector2:
                        propValue = new TVector2PropertyValue(new OpenTK.Vector2(reader.ReadSingle(), reader.ReadSingle()), field.FieldName);
                        break;
                    case PropertyValueType.Vector3:
                        propValue = new TVector3PropertyValue(new OpenTK.Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()), field.FieldName);
                        break;
                    case PropertyValueType.YRotation:
                        propValue = new TFloatPropertyValue(reader.ReadInt16(), field.FieldName);
                        break;
                    default:
                        break;
                }

                // Post Fixup...
                if(string.Compare(field.FieldName, "Position", true) == 0)
                {
                    newActor.Transform.Position = (OpenTK.Vector3)propValue.GetValue();
                }
                else if(string.Compare(field.FieldName, "Y Rotation", true) == 0)
                {
                    float yRotation = (float)propValue.GetValue() * (180 / 32786f);
                    OpenTK.Quaternion yAxis = OpenTK.Quaternion.FromAxisAngle(new OpenTK.Vector3(0, 1, 0), WMath.DegreesToRadians(yRotation));
                    newActor.Transform.Rotation = yAxis;
                }
                else
                {
                    newActor.Properties.Add(propValue);
                }
            }

            return newActor;
        }
    }
}
