using GameFormatReader.Common;
using Newtonsoft.Json;
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

        public List<WActorNode> LoadFromFile(string fileName)
        {
            var loadedActors = new List<WActorNode>();

            using (EndianBinaryReader reader = new EndianBinaryReader(File.ReadAllBytes(fileName), System.Text.Encoding.ASCII, Endian.Big))
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

                    switch(chunk.FourCC)
                    {
                        case "RTBL":
                            {
                                Console.WriteLine("file: {0} rtblCount: {1}", Path.GetDirectoryName(fileName), chunk.ElementCount);
                                int[] rtableOffsets = new int[chunk.ElementCount];
                                for (int i = 0; i < rtableOffsets.Length; i++)
                                    rtableOffsets[i] = reader.ReadInt32();

                                // Jump to the RTBL entries.
                                for(int i = 0; i < rtableOffsets.Length; i++)
                                {
                                    reader.BaseStream.Position = rtableOffsets[i];
                                    byte numRooms = reader.ReadByte();
                                    byte timePass = reader.ReadByte();
                                    byte unknown0 = reader.ReadByte();
                                    byte unknown1 = reader.ReadByte();

                                    int tableOffset = reader.ReadInt32();

                                    Console.WriteLine("numRooms: {0} timePass: {1} unknown: {2} unknown2: {3}:", numRooms, timePass, unknown0, unknown1);

                                    reader.BaseStream.Position = tableOffset;
                                    for(int j = 0; j < numRooms; j++)
                                    {
                                        byte val = reader.ReadByte();
                                        byte msb = (byte)((val & 0x80) >> 7);
                                        byte secondMsb = (byte)((val & 0x7F) >> 6);
                                        byte roomId = (byte)(val & 0x3F);

                                        Console.WriteLine("\tMSB: {0} MSB2: {1} Room: {2}", msb, secondMsb, roomId);
                                    }
                                }

                            }
                            break;
                        default:
                            for (int i = 0; i < chunk.ElementCount; i++)
                            {
                                var newActor = LoadActorFromChunk(chunk.FourCC, reader, template);
                                newActor.Layer = chunk.Layer;

                                loadedActors.Add(newActor);
                            }
                            break;
                    }

                }
            }

            return loadedActors;
        }

        private WActorNode LoadActorFromChunk(string fourCC, EndianBinaryReader reader, MapActorDescriptor template)
        {
            var newActor = new WActorNode(fourCC);
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
                    case PropertyValueType.XRotation:
                    case PropertyValueType.YRotation:
                    case PropertyValueType.ZRotation:
                        propValue = new TShortPropertyValue(reader.ReadInt16(), field.FieldName);
                        break;
                    case PropertyValueType.Color24:
                        propValue = new TLinearColorPropertyValue(new WLinearColor(reader.ReadByte() / 255f, reader.ReadByte() / 255f, reader.ReadByte() / 255f), field.FieldName);
                        break;
                    case PropertyValueType.Color32:
                        propValue = new TLinearColorPropertyValue(new WLinearColor(reader.ReadByte() / 255f, reader.ReadByte() / 255f, reader.ReadByte() / 255f, reader.ReadByte() / 255f), field.FieldName);
                        break;
                    default:
                        Console.WriteLine("Unsupported PropertyValueType: {0}", field.FieldType);
                        break;
                }

                // Post Fixup...
                if(string.Compare(field.FieldName, "Position", true) == 0)
                {
                    newActor.Transform.Position = (OpenTK.Vector3)propValue.GetValue();
                }
                else if(string.Compare(field.FieldName, "X Rotation", true) == 0)
                {
                    float xRotation = WMath.RotationShortToFloat((short)propValue.GetValue());
                    OpenTK.Quaternion xAxis = OpenTK.Quaternion.FromAxisAngle(new OpenTK.Vector3(1, 0, 0), WMath.DegreesToRadians(xRotation));
                    newActor.Transform.Rotation *= xAxis;
                }
                else if(string.Compare(field.FieldName, "Y Rotation", true) == 0)
                {
                    float yRotation = WMath.RotationShortToFloat((short)propValue.GetValue());
                    OpenTK.Quaternion yAxis = OpenTK.Quaternion.FromAxisAngle(new OpenTK.Vector3(0, 1, 0), WMath.DegreesToRadians(yRotation));
                    newActor.Transform.Rotation *= yAxis;
                }
                else if(string.Compare(field.FieldName, "Z Rotation", true) == 0)
                {
                    float zRotation = WMath.RotationShortToFloat((short)propValue.GetValue());
                    OpenTK.Quaternion zAxis = OpenTK.Quaternion.FromAxisAngle(new OpenTK.Vector3(0, 0, 1), WMath.DegreesToRadians(zRotation));
                    newActor.Transform.Rotation *= zAxis;
                }
                else if(string.Compare(field.FieldName, "X Scale", true) == 0)
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

            return newActor;
        }
    }
}
