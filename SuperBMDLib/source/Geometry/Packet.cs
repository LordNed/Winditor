using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;

namespace SuperBMDLib.Geometry
{
    public class Packet
    {
        public List<Primitive> Primitives { get; private set; }
        public List<int> MatrixIndices { get; private set; }

        private int m_Size;
        private int m_Offset;

        public Packet()
        {
            Primitives = new List<Primitive>();
            MatrixIndices = new List<int>();
        }

        public Packet(int size, int offset, int[] matrixIndices)
        {
            m_Size = size;
            m_Offset = offset;
            Primitives = new List<Primitive>();
            MatrixIndices = new List<int>();
            MatrixIndices.AddRange(matrixIndices);
        }

        public void ReadPrimitives(EndianBinaryReader reader, ShapeVertexDescriptor desc)
        {
            reader.BaseStream.Seek(m_Offset, System.IO.SeekOrigin.Begin);

            while (true)
            {
                Primitive prim = new Primitive(reader, desc);
                Primitives.Add(prim);

                if (reader.PeekReadByte() == 0 || reader.BaseStream.Position >= m_Size + m_Offset)
                    break;
            }
        }
    }
}
