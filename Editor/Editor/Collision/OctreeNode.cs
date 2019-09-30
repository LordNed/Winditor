using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;

namespace WindEditor.Collision
{
    public class OctreeNode
    {
        public List<CollisionTriangle> Triangles { get; private set; }
        public OctreeNode Parent { get; private set; }
        public OctreeNode[] Children { get; private set; }

        public FAABox Bounds { get; private set; }

        public bool IsLeaf { get; private set; }

        public CollisionGroupNode Group { get; private set; }

        public OctreeNode()
        {
            Triangles = new List<CollisionTriangle>();
            Children = new OctreeNode[8];
        }

        public OctreeNode(FAABox bounds, CollisionGroupNode node, List<CollisionTriangle> triangles)
        {
            Triangles = triangles;
            Bounds = bounds;
            Group = node;

            Children = new OctreeNode[8];

            SortTriangles();
        }

        public void FlattenHierarchyRecursive(List<OctreeNode> list)
        {
            list.Add(this);

            foreach (OctreeNode n in Children)
            {
                if (n != null)
                {
                    n.FlattenHierarchyRecursive(list);
                }
            }
        }

        public void ToDZBFile(EndianBinaryWriter writer, List<OctreeNode> flat, List<CollisionTriangle> triangles)
        {
            writer.Write((byte)1); // Constant value
            
            // Leaf boolean
            if (IsLeaf)
            {
                writer.Write((byte)1);
            }
            else
            {
                writer.Write((byte)0);
            }

            // Parent index
            if (Parent != null)
            {
                writer.Write((short)flat.IndexOf(Parent));
            }
            else
            {
                writer.Write((short)-1);
            }

            if (IsLeaf)
            {
                writer.Write((short)triangles.IndexOf(Triangles[0]));

                for (int i = 0; i < 7; i++)
                {
                    writer.Write((short)-1);
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Children[i] != null)
                    {
                        writer.Write((short)flat.IndexOf(Children[i]));
                    }
                    else
                    {
                        writer.Write((short)-1);
                    }
                }
            }
        }

        private void SortTriangles()
        {
            List<CollisionTriangle> contained_tris = new List<CollisionTriangle>();

            foreach (CollisionTriangle tri in Triangles)
            {
                if (Bounds.Contains(tri.Center) && !tri.Taken)
                {
                    contained_tris.Add(tri);
                }
            }

            Triangles = contained_tris;

            if (Triangles.Count <= 10)
            {
                foreach (CollisionTriangle t in Triangles)
                {
                    t.Taken = true;
                }

                IsLeaf = true;
                return;
            }

            FAABox[] new_octants = SubdivideBounds();

            for (int i = 0; i < 8; i++)
            {
                Children[i] = new OctreeNode(new_octants[i], Group, Triangles);
                Children[i].Parent = this;

                if (Children[i].Triangles.Count == 0)
                {
                    Children[i] = null;
                }
            }
        }

        private FAABox[] SubdivideBounds()
        {
            Vector3 center = Bounds.Min + Bounds.Extents;
            FAABox[] new_octants = new FAABox[8];

            new_octants[0] = new FAABox(Bounds.Min, center);
            new_octants[1] = new FAABox(new Vector3(center.X, Bounds.Min.Y, Bounds.Min.Z), new Vector3(Bounds.Max.X, center.Y, center.Z));
            new_octants[2] = new FAABox(new Vector3(Bounds.Min.X, center.Y, Bounds.Min.Z), new Vector3(center.X, Bounds.Max.Y, center.Z));
            new_octants[3] = new FAABox(new Vector3(center.X, center.Y, Bounds.Min.Z), new Vector3(Bounds.Max.X, Bounds.Max.Y, center.Z));
            new_octants[4] = new FAABox(new Vector3(Bounds.Min.X, center.Y, center.Z), new Vector3(center.X, Bounds.Max.Y, Bounds.Max.Z));
            new_octants[5] = new FAABox(center, Bounds.Max);
            new_octants[6] = new FAABox(new Vector3(Bounds.Min.X, Bounds.Min.Y, center.Z), new Vector3(center.X, center.Y, Bounds.Max.Z));
            new_octants[7] = new FAABox(new Vector3(center.X, Bounds.Min.Y, center.Z), new Vector3(Bounds.Max.X, center.Y, Bounds.Max.Z));

            return new_octants;
        }
    }
}
