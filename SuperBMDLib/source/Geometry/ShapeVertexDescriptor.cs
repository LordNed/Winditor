using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using SuperBMDLib.Geometry.Enums;

namespace SuperBMDLib.Geometry
{
    public class ShapeVertexDescriptor
    {
        public SortedDictionary<GXVertexAttribute, Tuple<VertexInputType, int>> Attributes { get; private set; }

        public ShapeVertexDescriptor()
        {
            Attributes = new SortedDictionary<GXVertexAttribute, Tuple<VertexInputType, int>>();
        }

        public ShapeVertexDescriptor(EndianBinaryReader reader, int offset)
        {
            Attributes = new SortedDictionary<GXVertexAttribute, Tuple<VertexInputType, int>>();
            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);

            int index = 0;
            GXVertexAttribute attrib = (GXVertexAttribute)reader.ReadInt32();

            while (attrib != GXVertexAttribute.Null)
            {
                Attributes.Add(attrib, new Tuple<VertexInputType, int>((VertexInputType)reader.ReadInt32(), index));

                index++;
                attrib = (GXVertexAttribute)reader.ReadInt32();
            }
        }

        public bool CheckAttribute(GXVertexAttribute attribute)
        {
            return Attributes.ContainsKey(attribute);
        }

        public void SetAttribute(GXVertexAttribute attribute, VertexInputType inputType, int vertexIndex)
        {
            if (CheckAttribute(attribute))
                throw new Exception($"Attribute \"{ attribute }\" is already in the vertex descriptor!");

            Attributes.Add(attribute, new Tuple<VertexInputType, int>(inputType, vertexIndex));
        }

        public List<GXVertexAttribute> GetActiveAttributes()
        {
            List<GXVertexAttribute> attribs = new List<GXVertexAttribute>(Attributes.Keys);
            return attribs;
        }

        public int GetAttributeIndex(GXVertexAttribute attribute)
        {
            if (CheckAttribute(attribute))
                return Attributes[attribute].Item2;
            else
                throw new ArgumentException("attribute");
        }

        public VertexInputType GetAttributeType(GXVertexAttribute attribute)
        {
            if (CheckAttribute(attribute))
                return Attributes[attribute].Item1;
            else
                throw new ArgumentException("attribute");
        }

        public void Write(EndianBinaryWriter writer)
        {
            if (CheckAttribute(GXVertexAttribute.PositionMatrixIdx))
            {
                writer.Write((int)GXVertexAttribute.PositionMatrixIdx);
                writer.Write((int)Attributes[GXVertexAttribute.PositionMatrixIdx].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Position))
            {
                writer.Write((int)GXVertexAttribute.Position);
                writer.Write((int)Attributes[GXVertexAttribute.Position].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Normal))
            {
                writer.Write((int)GXVertexAttribute.Normal);
                writer.Write((int)Attributes[GXVertexAttribute.Normal].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Color0))
            {
                writer.Write((int)GXVertexAttribute.Color0);
                writer.Write((int)Attributes[GXVertexAttribute.Color0].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Color1))
            {
                writer.Write((int)GXVertexAttribute.Color1);
                writer.Write((int)Attributes[GXVertexAttribute.Color1].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex0))
            {
                writer.Write((int)GXVertexAttribute.Tex0);
                writer.Write((int)Attributes[GXVertexAttribute.Tex0].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex1))
            {
                writer.Write((int)GXVertexAttribute.Tex1);
                writer.Write((int)Attributes[GXVertexAttribute.Tex1].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex2))
            {
                writer.Write((int)GXVertexAttribute.Tex2);
                writer.Write((int)Attributes[GXVertexAttribute.Tex2].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex3))
            {
                writer.Write((int)GXVertexAttribute.Tex3);
                writer.Write((int)Attributes[GXVertexAttribute.Tex3].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex4))
            {
                writer.Write((int)GXVertexAttribute.Tex4);
                writer.Write((int)Attributes[GXVertexAttribute.Tex4].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex5))
            {
                writer.Write((int)GXVertexAttribute.Tex5);
                writer.Write((int)Attributes[GXVertexAttribute.Tex5].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex6))
            {
                writer.Write((int)GXVertexAttribute.Tex6);
                writer.Write((int)Attributes[GXVertexAttribute.Tex6].Item1);
            }

            if (CheckAttribute(GXVertexAttribute.Tex7))
            {
                writer.Write((int)GXVertexAttribute.Tex7);
                writer.Write((int)Attributes[GXVertexAttribute.Tex7].Item1);
            }

            // Null attribute
            writer.Write(255);
            writer.Write(0);
        }

        public int CalculateStride()
        {
            int stride = 0;

            foreach (Tuple<VertexInputType, int> tup in Attributes.Values)
            {
                switch(tup.Item1)
                {
                    case VertexInputType.Index16:
                        stride += 2;
                        break;
                    case VertexInputType.Index8:
                    case VertexInputType.Direct: // HACK: BMD usually uses this only for PositionMatrixIdx, which uses a byte, but we should really use the VAT/VTX1 to get the actual stride
                        stride += 1;
                        break;
                    case VertexInputType.None:
                        break;
                    default:
                        throw new Exception($"Unknown vertex input type\"{ tup.Item1 }\"");
                }
            }

            return stride;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ShapeVertexDescriptor))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            ShapeVertexDescriptor compObj = obj as ShapeVertexDescriptor;

            if (Attributes.Count != compObj.Attributes.Count)
                return false;

            for (int i = 0; i < Attributes.Count; i++)
            {
                KeyValuePair<GXVertexAttribute, Tuple<VertexInputType, int>> thisPair = Attributes.ElementAt(i);
                KeyValuePair<GXVertexAttribute, Tuple<VertexInputType, int>> otherPair = compObj.Attributes.ElementAt(i);

                if (thisPair.Key != otherPair.Key)
                    return false;

                if (thisPair.Value.Item1 != otherPair.Value.Item1)
                    return false;

                if (thisPair.Value.Item2 != otherPair.Value.Item2)
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int output = 0;

            foreach (KeyValuePair<GXVertexAttribute, Tuple<VertexInputType, int>> pair in Attributes)
            {
                output = (int)pair.Key + (int)pair.Value.Item1 + pair.Value.Item2;
            }

            return output;
        }

        public static bool operator==(ShapeVertexDescriptor left, ShapeVertexDescriptor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ShapeVertexDescriptor left, ShapeVertexDescriptor right)
        {
            return !left.Equals(right);
        }
    }
}
