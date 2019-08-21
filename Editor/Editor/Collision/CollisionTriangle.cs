using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;

namespace WindEditor.Collision
{
    public class CollisionTriangle
    {
        public Vector3[] Vertices { get; private set; }
        public WLinearColor VertexColor { get; private set; }

        public CollisionGroupNode ParentGroup { get; set; }
        public CollisionProperty Properties { get; set; }

        public CollisionTriangle(EndianBinaryReader reader, Vector3[] positions,
            CollisionGroupNode[] nodes, CollisionProperty[] properties)
        {
            Vertices = new Vector3[3];

            Vertices[0] = positions[reader.ReadInt16()];
            Vertices[1] = positions[reader.ReadInt16()];
            Vertices[2] = positions[reader.ReadInt16()];

            Properties = properties[reader.ReadInt16()];
            ParentGroup = nodes[reader.ReadInt16()];
            ParentGroup.Triangles.Add(this);
        }
    }
}
