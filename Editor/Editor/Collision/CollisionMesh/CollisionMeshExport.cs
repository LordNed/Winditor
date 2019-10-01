using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Assimp;
using GameFormatReader.Common;
using OpenTK;

namespace WindEditor.Collision
{
    public partial class WCollisionMesh
    {
        public void ToDZBFile(string file_name)
        {
            using (FileStream s = new FileStream(file_name, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ToDZBArray();
                s.Write(data, 0, data.Length);

                s.Flush();
            }
        }

        public byte[] ToDZBArray()
        {
            byte[] data = null;

            foreach (CollisionGroupNode n in m_Nodes)
            {
                if (n.Triangles.Count > 0)
                {
                    n.CalculateBounds();
                }
            }

            using (MemoryStream m = new MemoryStream())
            {
                EndianBinaryWriter writer = new EndianBinaryWriter(m, Endian.Big);
                WriteDZB(writer);

                data = m.ToArray();
            }

            return data;
        }

        public void ToOBJFile(string file_name)
        {
            using (FileStream s = new FileStream(file_name, FileMode.Create, FileAccess.Write))
            {
                using (StringWriter sw = new StringWriter())
                {
                    foreach (Vector3 v in m_Vertices)
                    {
                        sw.WriteLine($"v {v.X} {v.Y} {v.Z}");
                    }

                    sw.WriteLine();

                    RootNode.ToOBJFile(sw, m_Vertices);

                    byte[] file_bytes = System.Text.Encoding.ASCII.GetBytes(sw.ToString());

                    s.Write(file_bytes, 0, file_bytes.Length);
                    s.Flush();
                }
            }
        }

        public void ToDAEFile(string file_name)
        {
            AssimpContext cont = new AssimpContext();
            cont.ExportFile(BuildDAEScene(), file_name, "collada", 0);
        }

        private Scene BuildDAEScene()
        {
            Scene sc = new Scene();

            sc.Materials.Add(new Material());
            sc.Meshes.AddRange(GetAssimpMeshes());
            sc.RootNode = new Node("col_root");
            sc.RootNode.Children.Add(RootNode.GetAssimpNodesRecursive(sc.Meshes));

            return sc;
        }

        private List<Mesh> GetAssimpMeshes()
        {
            List<Mesh> meshes = new List<Mesh>();

            foreach (CollisionGroupNode n in m_Nodes)
            {
                if (n.Triangles.Count <= 0)
                {
                    continue;
                }

                Mesh m = new Mesh(n.Name);
                m.PrimitiveType = Assimp.PrimitiveType.Triangle;

                foreach (CollisionTriangle t in n.Triangles)
                {
                    Vector3D v1 = new Vector3D(t.Vertices[0].X, t.Vertices[0].Y, t.Vertices[0].Z);
                    Vector3D v2 = new Vector3D(t.Vertices[1].X, t.Vertices[1].Y, t.Vertices[1].Z);
                    Vector3D v3 = new Vector3D(t.Vertices[2].X, t.Vertices[2].Y, t.Vertices[2].Z);

                    if (!m.Vertices.Contains(v1))
                        m.Vertices.Add(v1);
                    if (!m.Vertices.Contains(v2))
                        m.Vertices.Add(v2);
                    if (!m.Vertices.Contains(v3))
                        m.Vertices.Add(v3);

                    m.Faces.Add(new Face(new int[] { m.Vertices.IndexOf(v1), m.Vertices.IndexOf(v2), m.Vertices.IndexOf(v3) }));
                }

                meshes.Add(m);
            }

            return meshes;
        }

        private void WriteDZB(EndianBinaryWriter writer)
        {
            List<Vector3> unique_verts = GetUniqueVertices();
            List<CollisionProperty> unique_properties = GetUniqueProperties();
            List<OctreeNode> octree_roots = GetOctreeRoots();
            List<OctreeNode> flattened_octrees = FlattenOctrees(octree_roots);
            List<CollisionTriangle> octree_arranged_tris = ArrangeTriangles(flattened_octrees);
            List<CollisionTriangle> OctreeTrisForIndexing = GetTrianglesForOctreeIndex(flattened_octrees);
            List<Vector3> arrange_verts = ArrangeVertices(octree_arranged_tris);
            RootNode.DeflateHierarchyRecursive(null, m_Nodes);

            WriteDZBHeader(writer);

            WriteVertexData(writer, arrange_verts);
            WriteTriangleData(writer, octree_arranged_tris, arrange_verts, unique_properties);
            WriteOctreeNodeData(writer, flattened_octrees, OctreeTrisForIndexing);
            WritePropertyData(writer, unique_properties);
            WriteOctreeIndexData(writer, flattened_octrees, OctreeTrisForIndexing);
            WriteGroupData(writer, flattened_octrees);
        }

        private void WriteDZBHeader(EndianBinaryWriter writer)
        {
            writer.Write((int)0); // Vertex count
            writer.Write((int)0); // Vertex offset

            writer.Write((int)Triangles.Count);
            writer.Write((int)0); // Triangle offset

            writer.Write((int)0); // Octree index count
            writer.Write((int)0); // Octree index offset

            writer.Write((int)0); // Octree node count
            writer.Write((int)0); // Octree node offset

            writer.Write((int)m_Nodes.Count);
            writer.Write((int)0); // Group offset

            writer.Write((int)0); // Property count
            writer.Write((int)0); // Property offset

            writer.Write((int)0); // Padding
        }

        private void WriteVertexData(EndianBinaryWriter writer, List<Vector3> vertices)
        {
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
            writer.Write(vertices.Count);
            writer.Write((int)writer.BaseStream.Length);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (Vector3 v in vertices)
            {
                writer.Write(v.X);
                writer.Write(v.Y);
                writer.Write(v.Z);
            }
        }

        private void WriteTriangleData(EndianBinaryWriter writer, List<CollisionTriangle> octree_tris, List<Vector3> vertices, List<CollisionProperty> properties)
        {
            writer.BaseStream.Seek(8, SeekOrigin.Begin);
            writer.Write(octree_tris.Count);
            writer.Write((int)writer.BaseStream.Length);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (CollisionTriangle t in octree_tris)
            {
                t.ToDZBFile(writer, vertices, m_Nodes, properties);
            }
        }

        private void WriteOctreeIndexData(EndianBinaryWriter writer, List<OctreeNode> nodes, List<CollisionTriangle> octree_tris)
        {
            int index_count = 0;

            writer.BaseStream.Seek(0x14, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (OctreeNode n in nodes)
            {
                if (n.IsLeaf)
                {
                    writer.Write((short)octree_tris.IndexOf(n.Triangles[0]));
                    index_count++;
                }
            }

            writer.Write((short)-1);
            //writer.Write((short)-1);

            writer.BaseStream.Seek(0x10, SeekOrigin.Begin);
            writer.Write(index_count);
            writer.BaseStream.Seek(0, SeekOrigin.End);
        }

        private void WriteOctreeNodeData(EndianBinaryWriter writer, List<OctreeNode> nodes, List<CollisionTriangle> octree_tris)
        {
            writer.BaseStream.Seek(0x18, SeekOrigin.Begin);
            writer.Write(nodes.Count);
            writer.Write((int)writer.BaseStream.Length);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (OctreeNode n in nodes)
            {
                n.ToDZBFile(writer, nodes, octree_tris);
            }
        }

        private void WriteGroupData(EndianBinaryWriter writer, List<OctreeNode> octree)
        {
            long cur_offset = writer.BaseStream.Position;

            writer.BaseStream.Seek(0x24, SeekOrigin.Begin);
            writer.Write((int)cur_offset);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            // Group data
            foreach (CollisionGroupNode n in m_Nodes)
            {
                n.ToDZBFile(writer, octree, m_Vertices);
            }

            // Group names
            for (int i = 0; i < m_Nodes.Count; i++)
            {
                writer.BaseStream.Seek(cur_offset + (i * 52), SeekOrigin.Begin);
                writer.Write((int)writer.BaseStream.Length);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                writer.Write(m_Nodes[i].Name.ToCharArray());
                writer.Write((byte)0);
            }
        }

        private void WritePropertyData(EndianBinaryWriter writer, List<CollisionProperty> properties)
        {
            writer.BaseStream.Seek(0x28, SeekOrigin.Begin);
            writer.Write(properties.Count);
            writer.Write((int)writer.BaseStream.Length);
            writer.BaseStream.Seek(0, SeekOrigin.End);

            foreach (CollisionProperty p in properties)
            {
                p.ToDZBFile(writer);
            }
        }

        private List<Vector3> GetUniqueVertices()
        {
            List<Vector3> unique_verts = new List<Vector3>();

            foreach (CollisionTriangle tri in Triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!unique_verts.Contains(tri.Vertices[i]))
                    {
                        unique_verts.Add(tri.Vertices[i]);
                    }
                }
            }

            return unique_verts;
        }

        private List<CollisionProperty> GetUniqueProperties()
        {
            List<CollisionProperty> unique_verts = new List<CollisionProperty>();

            foreach (CollisionTriangle tri in Triangles)
            {
                if (!unique_verts.Contains(tri.Properties))
                {
                    unique_verts.Add(tri.Properties);
                }
            }

            return unique_verts;
        }

        private List<OctreeNode> GetOctreeRoots()
        {
            List<OctreeNode> nodes = new List<OctreeNode>();

            foreach (CollisionGroupNode n in m_Nodes)
            {
                if (n.Triangles.Count > 0)
                {
                    nodes.Add(new OctreeNode(n.Bounds, n, n.Triangles));
                }
            }

            return nodes;
        }

        private List<OctreeNode> FlattenOctrees(List<OctreeNode> roots)
        {
            List<OctreeNode> flat = new List<OctreeNode>();

            foreach (OctreeNode n in roots)
            {
                n.FlattenHierarchyRecursive(flat);
            }

            return flat;
        }

        private List<CollisionTriangle> ArrangeTriangles(List<OctreeNode> octree)
        {
            List<CollisionTriangle> arranged_tris = new List<CollisionTriangle>();

            foreach (OctreeNode n in octree)
            {
                if (n.IsLeaf)
                {
                    arranged_tris.AddRange(n.Triangles);
                }
            }

            return arranged_tris;
        }

        private List<Vector3> ArrangeVertices(List<CollisionTriangle> triangles)
        {
            List<Vector3> vecs = new List<Vector3>();

            foreach (CollisionGroupNode n in m_Nodes)
            {
                if (n.Triangles.Count < 0)
                {
                    continue;
                }

                List<Vector3> local_list = new List<Vector3>();

                n.FirstVertexIndex = (short)vecs.Count;

                foreach (CollisionTriangle t in n.Triangles)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!local_list.Contains(t.Vertices[i]))
                        {
                            local_list.Add(t.Vertices[i]);
                        }
                    }
                }

                vecs.AddRange(local_list);
            }

            return vecs;
        }

        public List<CollisionTriangle> GetTrianglesForOctreeIndex(List<OctreeNode> nodes)
        {
            List<CollisionTriangle> tris = new List<CollisionTriangle>();

            foreach (OctreeNode n in nodes)
            {
                if (n.IsLeaf)
                {
                    tris.Add(n.Triangles[0]);
                }
            }

            return tris;
        }
    }
}
