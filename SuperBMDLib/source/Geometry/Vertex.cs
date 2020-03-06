using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Geometry.Enums;
using SuperBMDLib.Rigging;
using GameFormatReader.Common;

namespace SuperBMDLib.Geometry
{
    public class Vertex
    {
        public uint PositionMatrixIDxIndex { get; private set; }
        public uint PositionIndex { get; private set; }
        public uint NormalIndex { get; private set; }
        public uint Color0Index { get; private set; }
        public uint Color1Index { get; private set; }
        public uint TexCoord0Index { get; private set; }
        public uint TexCoord1Index { get; private set; }
        public uint TexCoord2Index { get; private set; }
        public uint TexCoord3Index { get; private set; }
        public uint TexCoord4Index { get; private set; }
        public uint TexCoord5Index { get; private set; }
        public uint TexCoord6Index { get; private set; }
        public uint TexCoord7Index { get; private set; }

        public uint Tex0MtxIndex { get; private set; }
        public uint Tex1MtxIndex { get; private set; }
        public uint Tex2MtxIndex { get; private set; }
        public uint Tex3MtxIndex { get; private set; }
        public uint Tex4MtxIndex { get; private set; }
        public uint Tex5MtxIndex { get; private set; }
        public uint Tex6MtxIndex { get; private set; }
        public uint Tex7MtxIndex { get; private set; }

        public uint PositionMatrixIndex { get; set; }
        public uint NormalMatrixIndex { get; set; }

        public Weight VertexWeight { get; private set; }

        public Vertex()
        {
            PositionMatrixIDxIndex = uint.MaxValue;
            PositionIndex = uint.MaxValue;
            NormalIndex = uint.MaxValue;
            Color0Index = uint.MaxValue;
            Color1Index = uint.MaxValue;
            TexCoord0Index = uint.MaxValue;
            TexCoord1Index = uint.MaxValue;
            TexCoord2Index = uint.MaxValue;
            TexCoord3Index = uint.MaxValue;
            TexCoord4Index = uint.MaxValue;
            TexCoord5Index = uint.MaxValue;
            TexCoord6Index = uint.MaxValue;
            TexCoord7Index = uint.MaxValue;

            Tex0MtxIndex = uint.MaxValue;
            Tex1MtxIndex = uint.MaxValue;
            Tex2MtxIndex = uint.MaxValue;
            Tex3MtxIndex = uint.MaxValue;
            Tex4MtxIndex = uint.MaxValue;
            Tex5MtxIndex = uint.MaxValue;
            Tex6MtxIndex = uint.MaxValue;
            Tex7MtxIndex = uint.MaxValue;

            VertexWeight = new Weight();
        }

        public Vertex(Vertex src)
        {
            // The position matrix index index is specific to the packet the vertex is in.
            // So if copying a vertex across different packets, this value will be wrong and it needs to be recalculated manually.
            PositionMatrixIDxIndex = src.PositionMatrixIDxIndex;

            PositionIndex = src.PositionIndex;
            NormalIndex = src.NormalIndex;
            Color0Index = src.Color0Index;
            Color1Index = src.Color1Index;
            TexCoord0Index = src.TexCoord0Index;
            TexCoord1Index = src.TexCoord1Index;
            TexCoord2Index = src.TexCoord2Index;
            TexCoord3Index = src.TexCoord3Index;
            TexCoord4Index = src.TexCoord4Index;
            TexCoord5Index = src.TexCoord5Index;
            TexCoord6Index = src.TexCoord6Index;
            TexCoord7Index = src.TexCoord7Index;

            Tex0MtxIndex = src.Tex0MtxIndex;
            Tex1MtxIndex = src.Tex1MtxIndex;
            Tex2MtxIndex = src.Tex2MtxIndex;
            Tex3MtxIndex = src.Tex3MtxIndex;
            Tex4MtxIndex = src.Tex4MtxIndex;
            Tex5MtxIndex = src.Tex5MtxIndex;
            Tex6MtxIndex = src.Tex6MtxIndex;
            Tex7MtxIndex = src.Tex7MtxIndex;

            VertexWeight = src.VertexWeight;
        }

        public uint GetAttributeIndex(GXVertexAttribute attribute)
        {
            switch (attribute)
            {
                case GXVertexAttribute.PositionMatrixIdx:
                    return PositionMatrixIDxIndex;
                case GXVertexAttribute.Position:
                    return PositionIndex;
                case GXVertexAttribute.Normal:
                    return NormalIndex;
                case GXVertexAttribute.Color0:
                    return Color0Index;
                case GXVertexAttribute.Color1:
                    return Color1Index;
                case GXVertexAttribute.Tex0:
                    return TexCoord0Index;
                case GXVertexAttribute.Tex1:
                    return TexCoord1Index;
                case GXVertexAttribute.Tex2:
                    return TexCoord2Index;
                case GXVertexAttribute.Tex3:
                    return TexCoord3Index;
                case GXVertexAttribute.Tex4:
                    return TexCoord4Index;
                case GXVertexAttribute.Tex5:
                    return TexCoord5Index;
                case GXVertexAttribute.Tex6:
                    return TexCoord6Index;
                case GXVertexAttribute.Tex7:
                    return TexCoord7Index;
                case GXVertexAttribute.Tex0Mtx:
                    return Tex0MtxIndex;
                case GXVertexAttribute.Tex1Mtx:
                    return Tex1MtxIndex;
                case GXVertexAttribute.Tex2Mtx:
                    return Tex2MtxIndex;
                case GXVertexAttribute.Tex3Mtx:
                    return Tex3MtxIndex;
                case GXVertexAttribute.Tex4Mtx:
                    return Tex4MtxIndex;
                case GXVertexAttribute.Tex5Mtx:
                    return Tex5MtxIndex;
                case GXVertexAttribute.Tex6Mtx:
                    return Tex6MtxIndex;
                case GXVertexAttribute.Tex7Mtx:
                    return Tex7MtxIndex;
                default:
                    throw new ArgumentException(String.Format("attribute {0}", attribute));
            }
        }

        public void SetAttributeIndex(GXVertexAttribute attribute, uint index)
        {
            switch (attribute)
            {
                case GXVertexAttribute.PositionMatrixIdx:
                    PositionMatrixIDxIndex = index;
                    break;
                case GXVertexAttribute.Position:
                    PositionIndex = index;
                    break;
                case GXVertexAttribute.Normal:
                    NormalIndex = index;
                    break;
                case GXVertexAttribute.Color0:
                    Color0Index = index;
                    break;
                case GXVertexAttribute.Color1:
                    Color1Index = index;
                    break;
                case GXVertexAttribute.Tex0:
                    TexCoord0Index = index;
                    break;
                case GXVertexAttribute.Tex1:
                    TexCoord1Index = index;
                    break;
                case GXVertexAttribute.Tex2:
                    TexCoord2Index = index;
                    break;
                case GXVertexAttribute.Tex3:
                    TexCoord3Index = index;
                    break;
                case GXVertexAttribute.Tex4:
                    TexCoord4Index = index;
                    break;
                case GXVertexAttribute.Tex5:
                    TexCoord5Index = index;
                    break;
                case GXVertexAttribute.Tex6:
                    TexCoord6Index = index;
                    break;
                case GXVertexAttribute.Tex7:
                    TexCoord7Index = index;
                    break;
                case GXVertexAttribute.Tex0Mtx:
                    Tex0MtxIndex = index;
                    break;
                case GXVertexAttribute.Tex1Mtx:
                    Tex1MtxIndex = index;
                    break;
                case GXVertexAttribute.Tex2Mtx:
                    Tex2MtxIndex = index;
                    break;
                case GXVertexAttribute.Tex3Mtx:
                    Tex3MtxIndex = index;
                    break;
                case GXVertexAttribute.Tex4Mtx:
                    Tex4MtxIndex = index;
                    break;
                case GXVertexAttribute.Tex5Mtx:
                    Tex5MtxIndex = index;
                    break;
                case GXVertexAttribute.Tex6Mtx:
                    Tex6MtxIndex = index;
                    break;
                case GXVertexAttribute.Tex7Mtx:
                    Tex7MtxIndex = index;
                    break;
                default:
                    throw new ArgumentException(String.Format("attribute {0}", attribute));
            }
        }

        public void SetWeight(Weight weight)
        {
            VertexWeight = weight;
        }

        public void Write(EndianBinaryWriter writer, ShapeVertexDescriptor desc)
        {
            if (desc.CheckAttribute(GXVertexAttribute.PositionMatrixIdx))
            {
                WriteAttributeIndex(writer, PositionMatrixIDxIndex * 3, desc.Attributes[GXVertexAttribute.PositionMatrixIdx].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Position))
            {
                WriteAttributeIndex(writer, PositionIndex, desc.Attributes[GXVertexAttribute.Position].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Normal))
            {
                WriteAttributeIndex(writer, NormalIndex, desc.Attributes[GXVertexAttribute.Normal].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Color0))
            {
                WriteAttributeIndex(writer, Color0Index, desc.Attributes[GXVertexAttribute.Color0].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Color1))
            {
                WriteAttributeIndex(writer, Color1Index, desc.Attributes[GXVertexAttribute.Color1].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex0))
            {
                WriteAttributeIndex(writer, TexCoord0Index, desc.Attributes[GXVertexAttribute.Tex0].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex1))
            {
                WriteAttributeIndex(writer, TexCoord1Index, desc.Attributes[GXVertexAttribute.Tex1].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex2))
            {
                WriteAttributeIndex(writer, TexCoord2Index, desc.Attributes[GXVertexAttribute.Tex2].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex3))
            {
                WriteAttributeIndex(writer, TexCoord3Index, desc.Attributes[GXVertexAttribute.Tex3].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex4))
            {
                WriteAttributeIndex(writer, TexCoord4Index, desc.Attributes[GXVertexAttribute.Tex4].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex5))
            {
                WriteAttributeIndex(writer, TexCoord5Index, desc.Attributes[GXVertexAttribute.Tex5].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex6))
            {
                WriteAttributeIndex(writer, TexCoord6Index, desc.Attributes[GXVertexAttribute.Tex6].Item1);
            }

            if (desc.CheckAttribute(GXVertexAttribute.Tex7))
            {
                WriteAttributeIndex(writer, TexCoord7Index, desc.Attributes[GXVertexAttribute.Tex7].Item1);
            }
        }

        private void WriteAttributeIndex(EndianBinaryWriter writer, uint value, VertexInputType type)
        {
            switch (type)
            {
                case VertexInputType.Direct:
                case VertexInputType.Index8:
                    writer.Write((byte)value);
                    break;
                case VertexInputType.Index16:
                    writer.Write((short)value);
                    break;
                case VertexInputType.None:
                default:
                    throw new ArgumentException("vertex input type");
            }
        }
    }
}
