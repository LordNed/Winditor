using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;
using System.Drawing;

namespace WindEditor.Collision
{
    public class CollisionTriangle
    {
        public Vector3[] Vertices { get; private set; }
        public Vector3 Center { get; private set; }
        public WLinearColor VertexColor { get; private set; }

        public CollisionGroupNode ParentGroup { get; set; }
        public CollisionProperty Properties { get; set; }

        public bool IsSelected { get; private set; }
        public bool Taken { get; set; }

        public CollisionTriangle(EndianBinaryReader reader, List<Vector3> positions,
            List<CollisionGroupNode> nodes, CollisionProperty[] properties)
        {
            Vertices = new Vector3[3];

            Vertices[0] = positions[reader.ReadInt16()];
            Vertices[1] = positions[reader.ReadInt16()];
            Vertices[2] = positions[reader.ReadInt16()];

            VertexColor = WLinearColor.FromHexString("0xB0E0C0FF");

            Properties = properties[reader.ReadInt16()].Clone();
            ParentGroup = nodes[reader.ReadInt16()];
            ParentGroup.Triangles.Add(this);

            CalculateCenter();
        }

        public CollisionTriangle(Vector3 v1, Vector3 v2, Vector3 v3, CollisionGroupNode parent)
        {
            Vertices = new Vector3[] { v1, v2, v3 };
            VertexColor = WLinearColor.FromHexString("0xB0E0C0FF");

            Properties = new CollisionProperty();
            ParentGroup = parent;

            CalculateCenter();
        }

        public void Select()
        {
            VertexColor = WLinearColor.FromHexString("0xFF0000FF");
            IsSelected = true;
        }

        public void Deselect()
        {
            VertexColor = WLinearColor.FromHexString("0xB0E0C0FF");
            IsSelected = false;
        }

        public void ToDZBFile(EndianBinaryWriter writer, List<Vector3> verts, List<CollisionGroupNode> groups, List<CollisionProperty> properties)
        {
            writer.Write((short)verts.IndexOf(Vertices[0], ParentGroup.FirstVertexIndex));
            writer.Write((short)verts.IndexOf(Vertices[1], ParentGroup.FirstVertexIndex));
            writer.Write((short)verts.IndexOf(Vertices[2], ParentGroup.FirstVertexIndex));
            writer.Write((short)properties.IndexOf(Properties));
            writer.Write((short)groups.IndexOf(ParentGroup));
        }

        public void CalculateCenter()
        {
            Center = new Vector3((Vertices[0].X + Vertices[1].X + Vertices[2].X) / 3,
                                 (Vertices[0].Y + Vertices[1].Y + Vertices[2].Y) / 3,
                                 (Vertices[0].Z + Vertices[1].Z + Vertices[2].Z) / 3);
        }
    }
}
