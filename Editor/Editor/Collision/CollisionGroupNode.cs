using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GameFormatReader.Common;
using System.IO;
using Assimp;

namespace WindEditor.Collision
{
    public enum TerrainType
    {
        Solid,
        Water,
        Lava
    }

    [HideCategories()]
    public class CollisionGroupNode : INotifyPropertyChanged
    {
        private string m_Name;
        private ObservableCollection<CollisionGroupNode> m_Children;

        private Vector3 m_Translation;
        private OpenTK.Quaternion m_Rotation;
        private Vector3 m_Scale;

        private short m_ParentIndex;
        private short m_NextSiblingIndex;
        private short m_FirstChildIndex;

        private TerrainType m_Terrain;

        private int m_RoomTableIndex;

        [WProperty("Group Settings", "Terrain Type", true, "Whether the surface is solid, water, or lava.")]
        public TerrainType Terrain
        {
            get { return m_Terrain; }
            set
            {
                if (value != m_Terrain)
                {
                    m_Terrain = value;
                    OnPropertyChanged("Terrain");
                }
            }
        }

        [WProperty("Group Settings", "Room Table Index", true, "Unknown")]
        public int RoomTableIndex
        {
            get { return m_RoomTableIndex; }
            set
            {
                if (value != m_RoomTableIndex)
                {
                    m_RoomTableIndex = value;
                    OnPropertyChanged("RoomTableIndex");
                }
            }
        }

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value != m_Name)
                {
                    m_Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public CollisionGroupNode Parent { get; private set; }
        public ObservableCollection<CollisionGroupNode> Children
        {
            get { return m_Children; }
            set
            {
                if (value != m_Children)
                {
                    m_Children = value;
                    OnPropertyChanged("Children");
                }
            }
        }

        public List<CollisionTriangle> Triangles { get; private set; }

        public CollisionGroupNode(EndianBinaryReader reader)
        {
            Children = new ObservableCollection<CollisionGroupNode>();
            Triangles = new List<CollisionTriangle>();

            int name_offset = reader.ReadInt32();
            long cur_offset = reader.BaseStream.Position;

            reader.BaseStream.Seek(name_offset, System.IO.SeekOrigin.Begin);
            Name = Encoding.ASCII.GetString(reader.ReadBytesUntil(0));
            reader.BaseStream.Seek(cur_offset, System.IO.SeekOrigin.Begin);

            m_Scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            //m_Rotation =
            reader.SkipInt16();
            reader.SkipInt16();
            reader.SkipInt16();
            reader.SkipInt16();
            m_Translation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            m_ParentIndex = reader.ReadInt16();
            m_NextSiblingIndex = reader.ReadInt16();
            m_FirstChildIndex = reader.ReadInt16();

            reader.SkipInt16(); // Unknown 1
            reader.SkipInt16(); // Unknown 2
            reader.SkipInt16(); // Octree index, we don't need it for loading from dzb
            reader.SkipInt16(); // unknown 3

            Terrain = (TerrainType)reader.ReadByte();
            m_RoomTableIndex = reader.ReadByte();
        }

        public CollisionGroupNode(string name)
        {
            Name = name;
            Triangles = new List<CollisionTriangle>();
            Children = new ObservableCollection<CollisionGroupNode>();
        }

        public void InflateHierarchyRecursive(CollisionGroupNode last_parent, List<CollisionGroupNode> flat_hierarchy)
        {
            if (last_parent != null)
            {
                Parent = last_parent;
                last_parent.Children.Add(this);
            }

            if (m_FirstChildIndex != -1)
                flat_hierarchy[m_FirstChildIndex].InflateHierarchyRecursive(this, flat_hierarchy);

            if (m_NextSiblingIndex != -1)
                flat_hierarchy[m_NextSiblingIndex].InflateHierarchyRecursive(last_parent, flat_hierarchy);
        }

        public List<CollisionTriangle> GetTrianglesRecursive()
        {
            List<CollisionTriangle> out_list = new List<CollisionTriangle>();

            out_list.AddRange(Triangles);

            foreach (CollisionGroupNode node in Children)
            {
                out_list.AddRange(node.GetTrianglesRecursive());
            }

            return out_list;
        }

        public int GatherTriangles(List<CollisionTriangle> out_list)
        {
            Stack<CollisionGroupNode> stack = new Stack<CollisionGroupNode>();
            stack.Push(this);
            int capacity = 0;

            while (stack.Count > 0)
            {
                CollisionGroupNode node = stack.Pop();

                if (out_list != null)
                {
                    out_list.AddRange(node.Triangles);
                }

                capacity += node.Triangles.Count;

                foreach (var child in node.Children)
                {
                    stack.Push(child);
                }
            }

            return capacity;
        }

        public void ToOBJFile(StringWriter writer, Vector3[] vertices)
        {
            if (Triangles.Count > 0)
            {
                writer.WriteLine($"g { Name }");

                foreach (CollisionTriangle t in Triangles)
                {
                    writer.WriteLine($"f { Array.IndexOf(vertices, t.Vertices[0]) + 1 } {Array.IndexOf(vertices, t.Vertices[1]) + 1} {Array.IndexOf(vertices, t.Vertices[2]) + 1}");
                }

                writer.WriteLine();
            }

            foreach (CollisionGroupNode n in Children)
            {
                n.ToOBJFile(writer, vertices);
            }
        }

        public Node GetAssimpNodesRecursive(List<Mesh> meshes)
        {
            Node cur_node = new Node(Name);

            if (Triangles.Count > 0)
            {
                int index = meshes.IndexOf(meshes.Find(x => x.Name == Name));
                cur_node.MeshIndices.Add(index);
            }

            foreach (CollisionGroupNode n in Children)
            {
                cur_node.Children.Add(n.GetAssimpNodesRecursive(meshes));
            }

            return cur_node;
        }

        public override string ToString()
        {
            return Name;
        }

        #region INotifyPropertyChanged Support
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
