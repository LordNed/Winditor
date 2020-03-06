using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Scenegraph.Enums;
using GameFormatReader.Common;

namespace SuperBMDLib.Scenegraph
{
    public class SceneNode
    {
        public SceneNode Parent { get; private set; }
        public List<SceneNode> Children { get; private set; }

        public NodeType Type { get; private set; }
        public int Index { get; private set; }

        public SceneNode(EndianBinaryReader reader, SceneNode parent)
        {
            Children = new List<SceneNode>();
            Parent = parent;

            Type = (NodeType)reader.ReadInt16();
            Index = reader.ReadInt16();
        }

        public SceneNode(NodeType type, int index, SceneNode parent)
        {
            Type = type;
            Index = index;
            Parent = parent;

            if (Parent != null)
                Parent.Children.Add(this);

            Children = new List<SceneNode>();
        }

        public override string ToString()
        {
            return $"{ Type } : { Index }";
        }
    }
}
