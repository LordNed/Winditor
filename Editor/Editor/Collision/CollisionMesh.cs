using GameFormatReader.Common;
using JStudio.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Assimp;
using Collada141;

namespace WindEditor.Collision
{
    public class WCollisionMesh : WDOMNode, IRenderable
    {
        private int m_vbo, m_cbo, m_ebo;
        private Shader m_primitiveShader;
        private int m_triangleCount;
        private UpAxisType m_UpAxis;

        private FAABox m_aaBox;

        private Vector3[] m_Vertices;
        private Vector4[] m_Colors;
        private Vector4[] m_Colors_Black;
        private int[] m_Indices;
        private List<CollisionGroupNode> m_Nodes;
        private CollisionGroupNode m_RootNode;
        public CollisionProperty[] m_Properties;

        public string FileName { get; set; }

        public CollisionGroupNode RootNode
        {
            get { return m_RootNode; }
            set
            {
                if (value != m_RootNode)
                {
                    m_RootNode = value;
                    OnPropertyChanged("RootNode");
                }
            }
        }

        public List<CollisionTriangle> Triangles { get; private set; }

        public bool IsDirty { get; set; }

        public WCollisionMesh(WWorld world) : base(world)
        {
            m_UpAxis = UpAxisType.Y_UP;
            Triangles = new List<CollisionTriangle>();
            m_Nodes = new List<CollisionGroupNode>();
            IsRendered = true;
            CreateShader();
        }

        public WCollisionMesh(WWorld world, COLLADA dae) :base(world)
        {
            m_UpAxis = UpAxisType.Y_UP;
            m_Nodes = new List<CollisionGroupNode>();

            IsDirty = true;
            Triangles = new List<CollisionTriangle>();
            IsRendered = true;

            CreateShader();
            LoadFromCollada(dae);
        }

        private void CreateShader()
        {
            m_primitiveShader = new Shader("UnselectedCollision");
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnselectedCollision.vert"), ShaderType.VertexShader);
            m_primitiveShader.CompileSource(File.ReadAllText("resources/shaders/UnselectedCollision.frag"), ShaderType.FragmentShader);
            m_primitiveShader.LinkShader();
        }

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
            ToDZBFile("D:\\Github\\Winditor\\test.dzb");
            SetupGL();
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

        private void SetupGL()
        {
            GL.GenBuffers(1, out m_vbo);
            GL.GenBuffers(1, out m_ebo);
            GL.GenBuffers(1, out m_cbo);

            // Upload Verts
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(12 * m_Vertices.Length), m_Vertices, BufferUsageHint.StaticDraw);

            // Upload initial colors
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * m_Colors.Length), m_Colors, BufferUsageHint.DynamicDraw);

            // Upload eBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(4 * m_Indices.Length), m_Indices, BufferUsageHint.StaticDraw);
        }

        public void ReleaseResources()
        {
            m_primitiveShader.Dispose();
            GL.DeleteBuffer(m_ebo);
            GL.DeleteBuffer(m_vbo);
            GL.DeleteBuffer(m_cbo);
        }

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

                    m.Faces.Add(new Face(new int[]{ m.Vertices.IndexOf(v1), m.Vertices.IndexOf(v2), m.Vertices.IndexOf(v3) }));
                }

                meshes.Add(m);
            }

            return meshes;
        }

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

                n.FirstVertex = (short)vecs.Count;

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

        public override string ToString()
        {
            return FileName;
        }

        #region IRenderable
        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            m_Colors = GetVertexColors();

            Matrix4 modelMatrix = Matrix4.Identity;
            Matrix4 viewMatrix = view.ViewMatrix;
            Matrix4 projMatrix = view.ProjMatrix;

            m_primitiveShader.Bind();
            GL.UniformMatrix4(m_primitiveShader.UniformModelMtx, false, ref modelMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformViewMtx, false, ref viewMatrix);
            GL.UniformMatrix4(m_primitiveShader.UniformProjMtx, false, ref projMatrix);

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_vbo);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Position, 3, VertexAttribPointerType.Float, false, 12, 0);

            // CBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.EnableVertexAttribArray((int)ShaderAttributeIds.Color0);
            GL.VertexAttribPointer((int)ShaderAttributeIds.Color0, 4, VertexAttribPointerType.Float, false, 16, 0);

            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_ebo);

            GL.DepthMask(false);

            DrawTris();
            DrawLines();

            GL.DepthMask(true);

            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Position);
            GL.DisableVertexAttribArray((int)ShaderAttributeIds.Color0);
        }

        private void DrawTris()
        {
            // Enable stuff
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.Enable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(-5f, 1f);

            // CBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * m_Colors.Length), m_Colors, BufferUsageHint.DynamicDraw);

            // Draw!
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.DrawElements(BeginMode.Triangles, m_triangleCount * 3, DrawElementsType.UnsignedInt, 0);

            // Disable stuff
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);

            GL.Disable(EnableCap.PolygonOffsetFill);
            GL.PolygonOffset(0f, 0f);
        }

        private void DrawLines()
        {
            // Enable stuff
            GL.Disable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);

            GL.Enable(EnableCap.PolygonOffsetLine);
            GL.PolygonOffset(-10f, 1f);

            // CBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, m_cbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(16 * m_Colors_Black.Length), m_Colors_Black, BufferUsageHint.DynamicDraw);

            // Draw!
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.DrawElements(BeginMode.Triangles, m_triangleCount * 3, DrawElementsType.UnsignedInt, 0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            // Disable stuff
            GL.Disable(EnableCap.CullFace);

            GL.Disable(EnableCap.PolygonOffsetLine);
            GL.PolygonOffset(0f, 0f);
        }

        Vector3 IRenderable.GetPosition()
        {
            return m_aaBox.Center;
        }

        float IRenderable.GetBoundingRadius()
        {
            return m_aaBox.Max.Length; // ToDo: This isn't correct.
        }
        #endregion
    }
}
