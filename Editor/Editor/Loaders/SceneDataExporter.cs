using GameFormatReader.Common;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
using OpenTK;

namespace WindEditor
{
    public class SceneDataExporter
    {
        public SceneDataExporter()
        {
        }

        public void ExportToStream(EndianBinaryWriter writer, WScene scene)
        {
            // Build a dictionary which lists unique FourCC's and a list of all relevant actors.
            var actorCategories = new Dictionary<FourCC, List<SerializableDOMNode>>();
            foreach(var child in scene)
            {
                var groupNode = child as WDOMGroupNode;
                if (groupNode == null)
                    continue;

                // If this is an ACTR, SCOB, or TRES group node, we have to dig into it to get the layers.
                if (groupNode.FourCC == FourCC.ACTR || groupNode.FourCC == FourCC.SCOB || groupNode.FourCC == FourCC.TRES)
                {
                    foreach (var layer in groupNode.Children)
                    {
                        foreach (var obj in layer.Children)
                        {
                            var actor = obj as SerializableDOMNode;

                            if (actor != null)
                            {
                                AddObjectToDictionary(actor, actorCategories);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var obj in groupNode.Children)
                    {
                        var actor = obj as SerializableDOMNode;

                        if (actor != null)
                        {
                            AddObjectToDictionary(actor, actorCategories);
                        }
                    }
                }
            }

            // Create a chunk header for each one.
            var chunkHeaders = new List<ChunkHeader>();
            foreach(var kvp in actorCategories)
            {
                ChunkHeader header = new ChunkHeader();
                header.FourCC = kvp.Key;
                header.ElementCount = kvp.Value.Count;

                chunkHeaders.Add(header);
            }

            long chunkStart = writer.BaseStream.Position;

            // Write the Header
            writer.Write(chunkHeaders.Count);
            for (int i = 0; i < chunkHeaders.Count; i++)
            {
                writer.Write((int)0); // Dummy Placeholder values for the Chunk Header.
                writer.Write((int)0);
                writer.Write((int)0);
            }

            // For each chunk, write the data for that chunk. Before writing the data, get the current offset and update the header.
            List<SerializableDOMNode>[] dictionaryData = new List<SerializableDOMNode>[actorCategories.Count];
            actorCategories.Values.CopyTo(dictionaryData, 0);

            for(int i = 0; i < chunkHeaders.Count; i++)
            {
                ChunkHeader header = chunkHeaders[i];
                chunkHeaders[i] = new ChunkHeader(header.FourCC, header.ElementCount, (int)(writer.BaseStream.Position - chunkStart));

                List<SerializableDOMNode> actors = dictionaryData[i];
                foreach (var actor in actors)
                {
                    MapActorDescriptor template = Globals.ActorDescriptors.Find(x => x.FourCC == actor.FourCC);
                    if (template == null)
                    {
                        Console.WriteLine("Unsupported FourCC (\"{0}\") for exporting!", actor.FourCC);
                        continue;
                    }

                    actor.PreSave();

                    actor.Save(writer);
                    //WriteActorToChunk(actor, template, writer);
                }
            }

            // Now that we've written every actor to file we can go back and re-write the headers now that we know their offsets.
            writer.BaseStream.Position = chunkStart + 0x4; // 0x4 is the offset to the Chunk Headers
            foreach (var header in chunkHeaders)
            {
                writer.WriteFixedString(FourCCConversion.GetStringFromEnum(header.FourCC), 4); // FourCC
                writer.Write(header.ElementCount); // Number of Entries
                writer.Write(header.ChunkOffset);   // Offset from start of file.
            }

            // Seek to the end of the file, and then pad us to 32-byte alignment.
            writer.BaseStream.Seek(0, SeekOrigin.End);
            int delta = WMath.Pad32Delta(writer.BaseStream.Position);
            for (int i = 0; i < delta; i++)
                writer.Write(0xFF);
        }

        public void AddObjectToDictionary(SerializableDOMNode actor, Dictionary<FourCC, List<SerializableDOMNode>> actorCategories)
        {
            string rawFourCC = FourCCConversion.GetStringFromEnum(actor.FourCC);
            string fixedFourCC = ChunkHeader.LayerToFourCC(rawFourCC, actor.Layer);
            FourCC fixedFourCCEnum = FourCCConversion.GetEnumFromString(fixedFourCC);

            if (!actorCategories.ContainsKey(fixedFourCCEnum))
                actorCategories[fixedFourCCEnum] = new List<SerializableDOMNode>();

            actorCategories[fixedFourCCEnum].Add(actor);
        }

        /*private void WriteActorToChunk(SerializableDOMNode actor, MapActorDescriptor template, EndianBinaryWriter writer)
        {
            // Just convert their rotation to Euler Angles now instead of doing it in parts later.
            Vector3 eulerRot = actor.Transform.Rotation.ToEulerAngles();

            foreach(var field in template.Fields)
            {
                IPropertyValue propValue = actor.Properties.Find(x => x.Name == field.FieldName);
                if (field.FieldName == "Position")
                    propValue = new TVector3PropertyValue(actor.Transform.Position, "Position");
                else if(field.FieldName == "X Rotation")
                {
                    short xRotShort = WMath.RotationFloatToShort(eulerRot.X);
                    propValue = new TShortPropertyValue(xRotShort, "X Rotation");
                }
                else if (field.FieldName == "Y Rotation")
                {
                    short yRotShort = WMath.RotationFloatToShort(eulerRot.Y);
                    propValue = new TShortPropertyValue(yRotShort, "Y Rotation");
                }
                else if (field.FieldName == "Z Rotation")
                {
                    short zRotShort = WMath.RotationFloatToShort(eulerRot.Z);
                    propValue = new TShortPropertyValue(zRotShort, "Z Rotation");
                }
                else if (field.FieldName == "X Scale")
                {
                    float xScale = actor.Transform.LocalScale.X;
                    propValue = new TBytePropertyValue((byte)(xScale * 10), "X Scale");
                }
                else if (field.FieldName == "Y Scale")
                {
                    float yScale = actor.Transform.LocalScale.Y;
                    propValue = new TBytePropertyValue((byte)(yScale * 10), "Y Scale");
                }
                else if (field.FieldName == "Z Scale")
                {
                    float zScale = actor.Transform.LocalScale.Z;
                    propValue = new TBytePropertyValue((byte)(zScale * 10), "Z Scale");
                }

                switch (field.FieldType)
                {
                    case PropertyValueType.Byte:
                        writer.Write((byte)propValue.GetValue()); break;
                    case PropertyValueType.Bool:
                        writer.Write((bool)propValue.GetValue()); break;
                    case PropertyValueType.Short:
                        writer.Write((short)propValue.GetValue()); break;
                    case PropertyValueType.Int:
                        writer.Write((int)propValue.GetValue()); break;
                    case PropertyValueType.Float:
                        writer.Write((float)propValue.GetValue()); break;
                    case PropertyValueType.String:
                        writer.Write(System.Text.Encoding.ASCII.GetBytes((string)propValue.GetValue())); break;
                    case PropertyValueType.FixedLengthString:
                        string fixedLenStr = (string)propValue.GetValue();
                        for (int i = 0; i < field.Length; i++)
                            writer.Write(i < fixedLenStr.Length ? (byte)fixedLenStr[i] : (byte)0);
                        //writer.WriteFixedString((string)propValue.GetValue(), (int)field.Length); break;
                        break;
                    case PropertyValueType.Vector2:
                        Vector2 vec2Val = (Vector2)propValue.GetValue();
                        writer.Write(vec2Val.X);
                        writer.Write(vec2Val.Y);
                        break;
                    case PropertyValueType.Vector3:
                        Vector3 vec3Val = (Vector3)propValue.GetValue();
                        writer.Write(vec3Val.X);
                        writer.Write(vec3Val.Y);
                        writer.Write(vec3Val.Z);
                        break;
                    case PropertyValueType.XRotation:
                    case PropertyValueType.YRotation:
                    case PropertyValueType.ZRotation:
                        writer.Write((short)propValue.GetValue()); break;
                    case PropertyValueType.Color24:
                        WLinearColor color24 = (WLinearColor)propValue.GetValue();
                        writer.Write((byte)(color24.R * 255));
                        writer.Write((byte)(color24.G * 255));
                        writer.Write((byte)(color24.B * 255));
                        break;
                    case PropertyValueType.Color32:
                        WLinearColor color32 = (WLinearColor)propValue.GetValue();
                        writer.Write((byte)(color32.R * 255));
                        writer.Write((byte)(color32.G * 255));
                        writer.Write((byte)(color32.B * 255));
                        writer.Write((byte)(color32.A * 255));
                        break;
                    default:
                        Console.WriteLine("Unsupported PropertyValueType: {0}", field.FieldType);
                        break;
                }
            }
        }*/
    }
}
