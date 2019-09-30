using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFormatReader.Common;
using OpenTK;
using Collada141;

namespace WindEditor.Collision
{
    public partial class WCollisionMesh
    {
        #region Loading from DZB
        public void Load(EndianBinaryReader stream)
        {
            int vertexCount = stream.ReadInt32();
            int vertexOffset = stream.ReadInt32();
            int triangleCount = stream.ReadInt32();
            int triangleOffset = stream.ReadInt32();
            stream.SkipInt32(); // Number of octree indices
            stream.SkipInt32(); // Octree indices
            stream.SkipInt32(); // Number of octree nodes
            stream.SkipInt32(); // Octree nodes
            int groupCount = stream.ReadInt32();
            int groupOffset = stream.ReadInt32();
            int propertyCount = stream.ReadInt32();
            int propertyOffset = stream.ReadInt32();

            LoadVertices(stream, vertexOffset, vertexCount);
            LoadGroups(stream, groupOffset, groupCount);
            LoadProperties(stream, propertyOffset, propertyCount);
            LoadTriangles(stream, triangleOffset, triangleCount);

            m_triangleCount = triangleCount;
            FinalizeLoad();
        }

        private void LoadVertices(EndianBinaryReader reader, int offset, int count)
        {
            m_Vertices = new Vector3[count];
            reader.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                m_Vertices[i] = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }

            m_aaBox = CalculateAABB(m_Vertices);
        }

        private void LoadGroups(EndianBinaryReader reader, int offset, int count)
        {
            reader.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                m_Nodes.Add(new CollisionGroupNode(reader));
            }

            RootNode = m_Nodes[0];
            RootNode.InflateHierarchyRecursive(null, m_Nodes);
        }

        private void LoadProperties(EndianBinaryReader reader, int offset, int count)
        {
            m_Properties = new CollisionProperty[count];
            reader.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                CollisionProperty new_prop = new CollisionProperty(reader);
                m_Properties[i] = new_prop;
            }
        }

        private void LoadTriangles(EndianBinaryReader reader, int offset, int count)
        {
            reader.BaseStream.Position = offset;

            for (int i = 0; i < count; i++)
            {
                CollisionTriangle new_tri = new CollisionTriangle(reader, m_Vertices, m_Nodes, m_Properties);
                Triangles.Add(new_tri);
            }
        }
        #endregion

        #region Loading from COLLADA
        public static WCollisionMesh FromDAEFile(WWorld world, string file_name)
        {
            COLLADA dae_file = COLLADA.Load(file_name);
            return new WCollisionMesh(world, dae_file);
        }

        private void LoadFromCollada(COLLADA dae)
        {
            m_UpAxis = dae.asset.up_axis;

            library_geometries geo = (library_geometries)Array.Find(dae.Items, x => x.GetType() == typeof(library_geometries));
            library_visual_scenes vis = (library_visual_scenes)Array.Find(dae.Items, x => x.GetType() == typeof(library_visual_scenes));

            visual_scene scene = vis.visual_scene[0];

            RootNode = GetHierarchyFromColladaRecursive(null, scene.node[0], geo.geometry);

            m_triangleCount = Triangles.Count;
            FinalizeLoad();
        }

        private CollisionGroupNode GetHierarchyFromColladaRecursive(CollisionGroupNode root, node template, geometry[] meshes)
        {
            CollisionGroupNode new_node = new CollisionGroupNode(root, template.name);
            m_Nodes.Add(new_node);

            if (template.instance_geometry != null)
            {
                string mesh_id = template.instance_geometry[0].url.Trim('#');
                geometry node_geo = (geometry)Array.Find(meshes, x => x.id == mesh_id);

                ParseGeometry(new_node, node_geo);
            }

            if (template.node1 != null)
            {
                foreach (node n in template.node1)
                {
                    new_node.Children.Add(GetHierarchyFromColladaRecursive(new_node, n, meshes));
                }
            }

            return new_node;
        }

        private void ParseGeometry(CollisionGroupNode parent, geometry geo)
        {
            mesh m = geo.Item as mesh;
            InputLocal pos_input = Array.Find(m.vertices.input, x => x.semantic == "POSITION");
            source pos_src = Array.Find(m.source, x => x.id == pos_input.source.Trim('#'));
            float_array pos_arr = pos_src.Item as float_array;

            triangles tris = m.Items[0] as triangles;
            string[] indices = tris.p.Trim(' ').Split(' ');
            int stride = tris.input.Length;

            for (int i = 0; i < indices.Length; i += stride * 3)
            {
                int vec1_index = Convert.ToInt32(indices[i]);
                int vec2_index = Convert.ToInt32(indices[i + stride]);
                int vec3_index = Convert.ToInt32(indices[i + (stride * 2)]);

                Vector3 vec1 = new Vector3((float)pos_arr.Values[vec1_index * 3],
                                           (float)pos_arr.Values[(vec1_index * 3) + 1],
                                           (float)pos_arr.Values[(vec1_index * 3) + 2]);
                Vector3 vec2 = new Vector3((float)pos_arr.Values[vec2_index * 3],
                                           (float)pos_arr.Values[(vec2_index * 3) + 1],
                                           (float)pos_arr.Values[(vec2_index * 3) + 2]);
                Vector3 vec3 = new Vector3((float)pos_arr.Values[vec3_index * 3],
                                           (float)pos_arr.Values[(vec3_index * 3) + 1],
                                           (float)pos_arr.Values[(vec3_index * 3) + 2]);

                if (m_UpAxis != UpAxisType.Y_UP)
                {
                    vec1 = SwapYZ(vec1);
                    vec2 = SwapYZ(vec2);
                    vec3 = SwapYZ(vec3);
                }

                CollisionTriangle new_tri = new CollisionTriangle(vec1, vec2, vec3, parent);

                parent.Triangles.Add(new_tri);
                Triangles.Add(new_tri);
            }
        }

        private Vector3 SwapYZ(Vector3 vec)
        {
            Vector3 new_vec = vec;

            float temp = -new_vec.Y;
            new_vec.Y = new_vec.Z;
            new_vec.Z = temp;

            return new_vec;
        }
        #endregion

        private void FinalizeLoad()
        {

            foreach (CollisionGroupNode n in m_Nodes)
            {
                if (n.Triangles.Count > 0)
                {
                    n.CalculateBounds();
                }
            }

            int vertex_count = m_triangleCount * 3;

            m_Vertices = new Vector3[vertex_count];
            m_Indices = new int[vertex_count];
            m_Colors_Black = new Vector4[vertex_count];

            for (int i = 0; i < m_Colors_Black.Length; i++)
            {
                m_Colors_Black[i] = new Vector4(0, 0, 0, 1);
            }

            m_Colors = GetVertexColors();

            for (int i = 0; i < Triangles.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int cur_index = (i * 3) + j;
                    m_Indices[cur_index] = cur_index;
                    m_Vertices[cur_index] = Triangles[i].Vertices[j];
                }
            }

            SetupGL();
        }

        private FAABox CalculateAABB(Vector3[] vertices)
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].X < min.X) min.X = vertices[i].X;
                if (vertices[i].Y < min.Y) min.Y = vertices[i].Y;
                if (vertices[i].Z < min.Z) min.Z = vertices[i].Z;

                if (vertices[i].X > max.X) max.X = vertices[i].X;
                if (vertices[i].Y > max.Y) max.Y = vertices[i].Y;
                if (vertices[i].Z > max.Z) max.Z = vertices[i].Z;
            }

            return new FAABox(min, max);
        }

        private Vector4[] GetVertexColors()
        {
            List<Vector4> colors = new List<Vector4>();

            foreach (CollisionTriangle tri in Triangles)
            {
                Vector4 tri_color = new Vector4(tri.VertexColor.R, tri.VertexColor.G, tri.VertexColor.B, tri.VertexColor.A);

                colors.Add(tri_color);
                colors.Add(tri_color);
                colors.Add(tri_color);
            }

            return colors.ToArray();
        }
    }
}
