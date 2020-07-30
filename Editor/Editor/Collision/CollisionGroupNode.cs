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
    [HideCategories()]
    public class CollisionGroupNode : INotifyPropertyChanged
    {
        private string m_Name;
        private ObservableCollection<CollisionGroupNode> m_Children;

        private Vector3 m_Scale;
        private OpenTK.Quaternion m_Rotation;
        private ushort m_Unknown1;
        private Vector3 m_Translation;

        private short m_ParentIndex;
        private short m_NextSiblingIndex;
        private short m_FirstChildIndex;

        public short FirstVertexIndex { get; set; }

        private short m_RoomNum = -1;

        private int m_Bitfield;

        [WProperty("Group Settings", "Is Water", true, "Whether the surface is water or not.")]
        public bool IsWater
        {
            get { return (m_Bitfield & 0x00000100) != 0 ? true : false; }
            set
            {
                int value_as_int = value ? 1 : 0;
                m_Bitfield = (int)(m_Bitfield & ~0x00000100 | (value_as_int << 8 & 0x00000100));
                OnPropertyChanged("IsWater");
            }
        }

        [WProperty("Group Settings", "Is Lava", true, "Whether the surface is lava or not.")]
        public bool IsLava
        {
            get { return (m_Bitfield & 0x00000200) != 0 ? true : false; }
            set
            {
                int value_as_int = value ? 1 : 0;
                m_Bitfield = (int)(m_Bitfield & ~0x00000200 | (value_as_int << 9 & 0x00000200));
                OnPropertyChanged("IsLava");
            }
        }

        [WProperty("Group Settings", "Room Number", true, "Which room to consider the current room when the player is above this surface.")]
        public int RoomNumber
        {
            get { return m_RoomNum; }
            set
            {
                if (value != m_RoomNum)
                {
                    m_RoomNum = (short)value;
                    OnPropertyChanged("RoomNumber");
                }
            }
        }

        [WProperty("Group Settings", "Room Table Index", true, "The index in the RTBL list to decide what rooms to load when the player is above this surface.")]
        public int RoomTableIndex
        {
            get {
                int value_as_int = (int)((m_Bitfield & 0x000000FF) >> 0);
                return value_as_int;
            }
            set
            {
                int value_as_int = value;
                m_Bitfield = (int)(m_Bitfield & ~0x000000FF | (value_as_int << 0 & 0x000000FF));
                OnPropertyChanged("RoomTableIndex");
            }
        }

        [WProperty("Group Settings", "Sea Floor Audio Room Number", true, "For the sea floor, this controls which room/sector the audio system considers this group to be a part of.\nShould be 0 if this group is not part of the sea floor.")]
        public int SeaFloorAudioRoomNumber
        {
            get
            {
                int value_as_int = (int)((m_Bitfield & 0x0001F800) >> 11);
                return value_as_int;
            }
            set
            {
                int value_as_int = value;
                m_Bitfield = (int)(m_Bitfield & ~0x0001F800 | (value_as_int << 11 & 0x0001F800));
                OnPropertyChanged("SeaFloorAudioRoomNumber");
            }
        }

        [WProperty("Group Settings", "Is Inner Sea Floor", true, "Whether this group is the inner part of the sea floor for this sector (the large central part).\nNot to be confused with the inner border of the sea floor around the sector.\nUsed by the audio system.")]
        public bool IsInnerSeaFloor
        {
            get { return (m_Bitfield & 0x00020000) != 0 ? true : false; }
            set
            {
                int value_as_int = value ? 1 : 0;
                m_Bitfield = (int)(m_Bitfield & ~0x00020000 | (value_as_int << 17 & 0x00020000));
                OnPropertyChanged("IsInnerSeaFloor");
            }
        }

        [WProperty("Group Settings", "Is Outer Edge of Sea Floor", true, "Whether this group is the outer border part of the sea floor around this sector.\nNot to be confused with the inner border of the sea floor around the sector.\nUsed by the audio system.")]
        public bool IsOuterEdgeOfSeaFloor
        {
            get { return (m_Bitfield & 0x00040000) != 0 ? true : false; }
            set
            {
                int value_as_int = value ? 1 : 0;
                m_Bitfield = (int)(m_Bitfield & ~0x00040000 | (value_as_int << 18 & 0x00040000));
                OnPropertyChanged("IsOuterEdgeOfSeaFloor");
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

        public FAABox Bounds { get; private set; }

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

            Vector3 eulerRot = new Vector3();
            for (int e = 0; e < 3; e++)
                eulerRot[e] = WMath.RotationShortToFloat(reader.ReadInt16());
            // ZYX order
            m_Rotation = OpenTK.Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(eulerRot.Z)) *
                         OpenTK.Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(eulerRot.Y)) *
                         OpenTK.Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(eulerRot.X));

            m_Unknown1 = reader.ReadUInt16();

            m_Translation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            m_ParentIndex = reader.ReadInt16();
            m_NextSiblingIndex = reader.ReadInt16();
            m_FirstChildIndex = reader.ReadInt16();

            m_RoomNum = reader.ReadInt16();

            FirstVertexIndex = reader.ReadInt16();

            reader.SkipInt16(); // Octree index, we don't need it for loading from dzb

            m_Bitfield = reader.ReadInt32();
        }

        public CollisionGroupNode(CollisionGroupNode root, string name)
        {
            Name = name;
            Triangles = new List<CollisionTriangle>();
            Children = new ObservableCollection<CollisionGroupNode>();
            Parent = root;
            m_Scale = Vector3.One;
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

        public void DeflateHierarchyRecursive(CollisionGroupNode last_parent, List<CollisionGroupNode> flat_hierarchy)
        {
            if (last_parent == null)
            {
                m_ParentIndex = -1;
                m_NextSiblingIndex = -1;
            }
            else
            {
                m_ParentIndex = (short)flat_hierarchy.IndexOf(last_parent);
            }

            if (Children.Count > 0)
            {
                m_FirstChildIndex = (short)flat_hierarchy.IndexOf(Children[0]);

                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].DeflateHierarchyRecursive(this, flat_hierarchy);

                    if (Children[i] == Children.Last())
                    {
                        Children[i].m_NextSiblingIndex = -1;
                    }
                    else
                    {
                        Children[i].m_NextSiblingIndex = (short)flat_hierarchy.IndexOf(Children[i + 1]);
                    }
                }
            }
            else
            {
                m_FirstChildIndex = -1;
            }
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

        public void ToDZBFile(EndianBinaryWriter writer, List<OctreeNode> octree, Vector3[] verts)
        {
            writer.Write((int)0); // Name offset
            writer.Write(m_Scale.X);
            writer.Write(m_Scale.Y);
            writer.Write(m_Scale.Z);

            Vector3 rotation = m_Rotation.ToEulerAngles();
            writer.Write(WMath.RotationFloatToShort(rotation.X));
            writer.Write(WMath.RotationFloatToShort(rotation.Y));
            writer.Write(WMath.RotationFloatToShort(rotation.Z));

            writer.Write(m_Unknown1);

            writer.Write(m_Translation.X);
            writer.Write(m_Translation.Y);
            writer.Write(m_Translation.Z);

            writer.Write(m_ParentIndex);
            writer.Write(m_NextSiblingIndex);
            writer.Write(m_FirstChildIndex);

            if (Parent == null)
            {
                writer.Write(m_RoomNum);
            }
            else
            {
                writer.Write((short)-1);
            }

            if (Triangles.Count > 0)
            {
                writer.Write(FirstVertexIndex); // Unknown 2
            }
            else
            {
                writer.Write((short)0);
            }

            // The first octree object to have this group assigned to it is the root of its octree.
            writer.Write((short)octree.IndexOf(octree.Find(x => x.Group == this)));

            writer.Write(m_Bitfield);
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

        public void CalculateBounds()
        {
            float min_x = float.MaxValue;
            float min_y = float.MaxValue;
            float min_z = float.MaxValue;
            float max_x = float.MinValue;
            float max_y = float.MinValue;
            float max_z = float.MinValue;

            foreach (CollisionTriangle t in Triangles)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (min_x > t.Vertices[i].X)
                    {
                        min_x = t.Vertices[i].X;
                    }
                    if (min_y > t.Vertices[i].Y)
                    {
                        min_y = t.Vertices[i].Y;
                    }
                    if (min_z > t.Vertices[i].Z)
                    {
                        min_z = t.Vertices[i].Z;
                    }

                    if (max_x < t.Vertices[i].X)
                    {
                        max_x = t.Vertices[i].X;
                    }
                    if (max_y < t.Vertices[i].Y)
                    {
                        max_y = t.Vertices[i].Y;
                    }
                    if (max_z < t.Vertices[i].Z)
                    {
                        max_z = t.Vertices[i].Z;
                    }
                }
            }

            Bounds = new FAABox(new Vector3(min_x, min_y, min_z), new Vector3(max_x, max_y, max_z));
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
