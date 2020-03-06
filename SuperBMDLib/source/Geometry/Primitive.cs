using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Geometry.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Geometry
{
    public class Primitive
    {
        public GXPrimitiveType PrimitiveType { get; private set; }
        public List<Vertex> Vertices { get; private set; }

        public Primitive()
        {
            PrimitiveType = GXPrimitiveType.Lines;
            Vertices = new List<Vertex>();
        }

        public Primitive(GXPrimitiveType primType)
        {
            PrimitiveType = primType;
            Vertices = new List<Vertex>();
        }

        public Primitive(EndianBinaryReader reader, ShapeVertexDescriptor activeAttribs)
        {
            Vertices = new List<Vertex>();

            PrimitiveType = (GXPrimitiveType)(reader.ReadByte() & 0xF8);
            int vertCount = reader.ReadInt16();

            for (int i = 0; i < vertCount; i++)
            {
                Vertex vert = new Vertex();

                foreach (GXVertexAttribute attrib in activeAttribs.Attributes.Keys)
                {
                    switch(activeAttribs.GetAttributeType(attrib))
                    {
                        case VertexInputType.Direct:
                            vert.SetAttributeIndex(attrib, attrib == GXVertexAttribute.PositionMatrixIdx ? (uint)(reader.ReadByte() / 3) : reader.ReadByte());
                            break;
                        case VertexInputType.Index8:
                            vert.SetAttributeIndex(attrib, reader.ReadByte());
                            break;
                        case VertexInputType.Index16:
                            vert.SetAttributeIndex(attrib, reader.ReadUInt16());
                            break;
                        case VertexInputType.None:
                            throw new Exception("Found \"None\" as vertex input type in Primitive(reader, activeAttribs)!");
                    }
                }

                Vertices.Add(vert);
            }
        }

        public void Write(EndianBinaryWriter writer, ShapeVertexDescriptor desc)
        {
            writer.Write((byte)PrimitiveType);
            writer.Write((short)Vertices.Count);

            foreach (Vertex vert in Vertices)
                vert.Write(writer, desc);
        }
    }
}
