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
        private static List<MapActorDescriptor> m_sActorDescriptors;

        public SceneDataExporter()
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
        }

        public void ExportToStream(EndianBinaryWriter writer, WScene scene)
        {
            // Build a dictionary which lists unique FourCC's and a list of all relevant actors.
            var actorCategories = new Dictionary<string, List<WActorNode>>();
            foreach(var child in scene)
            {
                WActorNode actor = child as WActorNode;
                if(actor != null)
                {
                    string fixedFourCC = ChunkHeader.LayerToFourCC(actor.FourCC, actor.Layer);

                    if (!actorCategories.ContainsKey(fixedFourCC))
                        actorCategories[fixedFourCC] = new List<WActorNode>();

                    actorCategories[fixedFourCC].Add(actor);
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
            List<WActorNode>[] dictionaryData = new List<WActorNode>[actorCategories.Count];
            actorCategories.Values.CopyTo(dictionaryData, 0);

            for(int i = 0; i < chunkHeaders.Count; i++)
            {
                ChunkHeader header = chunkHeaders[i];
                chunkHeaders[i] = new ChunkHeader(header.FourCC, header.ElementCount, (int)(writer.BaseStream.Position - chunkStart));

                List<WActorNode> actors = dictionaryData[i];
                foreach (var actor in actors)
                {
                    MapActorDescriptor template = m_sActorDescriptors.Find(x => x.FourCC == actor.FourCC);
                    if (template == null)
                    {
                        Console.WriteLine("Unsupported FourCC (\"{0}\") for exporting!", actor.FourCC);
                        continue;
                    }

                    WriteActorToChunk(actor, template, writer);
                }
            }

            // Now that we've written every actor to file we can go back and re-write the headers now that we know their offsets.
            writer.BaseStream.Position = chunkStart + 0x4; // 0x4 is the offset to the Chunk Headers
            foreach (var header in chunkHeaders)
            {
                writer.WriteFixedString(header.FourCC, 4); // FourCC
                writer.Write(header.ElementCount); // Number of Entries
                writer.Write(header.ChunkOffset);   // Offset from start of file.
            }

            // Seek to the end of the file for good measure.
            writer.BaseStream.Seek(0, SeekOrigin.End);

            Pad32(writer);
        }

        // Gets X angle in radians from a quaternion
        private float PitchFromQuat(Quaternion q)
        {
            return (float)Math.Atan((2f * (q.Y * q.Z + q.W * q.X)) / (q.W * q.W - q.X * q.X - q.Y * q.Y + q.Z * q.Z)); 
        }

        // Gets Y angle in radians from a quaternion
        // Appears to be broken?
        private float YawFromQuat(Quaternion q)
        {
            return (float)Math.Asin(WMath.Clamp((-2f) * (q.X * q.Z - q.W * q.Y), -1f, 1f));
        }

        // Gets Z angle in radians from a quaternion
        private float RollFromQuat(Quaternion q)
        {
            return (float)Math.Atan((2f * (q.X * q.Y + q.W * q.Z)) / (q.W * q.W + q.X * q.X - q.Y * q.Y - q.Z * q.Z));
        }

        private void WriteActorToChunk(WActorNode actor, MapActorDescriptor template, EndianBinaryWriter writer)
        {
            foreach(var field in template.Fields)
            {
                IPropertyValue propValue = actor.Properties.Find(x => x.Name == field.FieldName);
                if (field.FieldName == "Position")
                    propValue = new TVector3PropertyValue(actor.Transform.Position, "Position");
                else if(field.FieldName == "X Rotation")
                {
                    float xRot = WMath.RadiansToDegrees(PitchFromQuat(actor.Transform.Rotation));
                    short xRotShort = WMath.RotationFloatToShort(xRot);
                    propValue = new TShortPropertyValue(xRotShort, "X Rotation");
                }
                else if (field.FieldName == "Y Rotation")
                {
                    float yRot = WMath.RadiansToDegrees(YawFromQuat(actor.Transform.Rotation));
                    short yRotShort = WMath.RotationFloatToShort(yRot);
                    propValue = new TShortPropertyValue(yRotShort, "Y Rotation");
                }
                else if (field.FieldName == "Z Rotation")
                {
                    float zRot = WMath.RadiansToDegrees(RollFromQuat(actor.Transform.Rotation));
                    short zRotShort = WMath.RotationFloatToShort(zRot);
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
                        writer.Write((byte)color24.R);
                        writer.Write((byte)color24.G);
                        writer.Write((byte)color24.B);
                        break;
                    case PropertyValueType.Color32:
                        WLinearColor color32 = (WLinearColor)propValue.GetValue();
                        writer.Write((byte)color32.R);
                        writer.Write((byte)color32.G);
                        writer.Write((byte)color32.B);
                        writer.Write((byte)color32.A);
                        break;
                    default:
                        Console.WriteLine("Unsupported PropertyValueType: {0}", field.FieldType);
                        break;
                }
            }
        }

        private void Pad32(EndianBinaryWriter writer)
        {
            // Pad up to a 32 byte alignment
            // Formula: (x + (n-1)) & ~(n-1)
            long nextAligned = (writer.BaseStream.Length + 0x1F) & ~0x1F;

            long delta = nextAligned - writer.BaseStream.Length;
            writer.BaseStream.Position = writer.BaseStream.Length;
            for (int i = 0; i < delta; i++)
            {
                writer.Write((byte)0xFF);
            }
        }
    }
}
