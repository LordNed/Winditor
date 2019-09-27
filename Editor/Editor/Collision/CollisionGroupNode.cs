using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GameFormatReader.Common;

namespace WindEditor.Collision
{
    public enum TerrainType
    {
        Solid,
        Water,
        Lava
    }

    public class CollisionGroupNode : INotifyPropertyChanged
    {
        private string m_Name;
        private ObservableCollection<CollisionGroupNode> m_Children;

        private Vector3 m_Translation;
        private Quaternion m_Rotation;
        private Vector3 m_Scale;

        private short m_ParentIndex;
        private short m_NextSiblingIndex;
        private short m_FirstChildIndex;

        private TerrainType m_Terrain;

        private int m_RoomTableIndex;

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

        public void InflateHierarchyRecursive(CollisionGroupNode last_parent, CollisionGroupNode[] flat_hierarchy)
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

        public void SelectRecursive()
        {
            foreach (CollisionTriangle t in Triangles)
            {
                t.Select();
            }

            foreach (CollisionGroupNode c in Children)
            {
                c.SelectRecursive();
            }
        }

        public void DeselectRecursive()
        {
            foreach (CollisionTriangle t in Triangles)
            {
                t.Deselect();
            }

            foreach (CollisionGroupNode c in Children)
            {
                c.DeselectRecursive();
            }
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
