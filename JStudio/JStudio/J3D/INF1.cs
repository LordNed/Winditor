using GameFormatReader.Common;
using System;
using System.Collections.Generic;

namespace JStudio.J3D
{
    public enum HierarchyDataType
    {
        Finish = 0x0,       // Indicates Final Node in List
        NewNode = 0x01,     // Start Child
        EndNode = 0x02,     // Close child
        Joint = 0x10,       // A Skeletal Joint
        Material = 0x11,    // A Materal
        Batch = 0x12,       // A Mesh Batch
    }

    public sealed class HierarchyNode
    {
        public List<HierarchyNode> Children { get; private set; }
        public HierarchyDataType Type { get; set; }
        public ushort Value { get; set; }

        public HierarchyNode(HierarchyDataType type, ushort value)
        {
            Children = new List<HierarchyNode>();
            Type = type;
            Value = value;
        }

        public HierarchyNode()
        {
            Children = new List<HierarchyNode>();
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Type, Value);
        }
    }


    public class INF1
    {
        public ushort Unknown1 { get; set; }
        public uint PacketCount { get; set; }
        public uint VertexCount { get; set; }
        public HierarchyNode HierarchyRoot { get { return m_hierarchy; } }


        private HierarchyNode m_hierarchy;

        public sealed class InfoNode
        {
            // What type of Node is this
            public HierarchyDataType Type { get; set; }

            // What is it's index into the associated Joint, Material, or Shape table.
            public ushort Value { get; set; }

            public InfoNode(HierarchyDataType type, ushort value)
            {
                Type = type;
                Value = value;
            }
        }

        public void LoadINF1FromStream(EndianBinaryReader reader, long chunkStart)
        {
            Unknown1 = reader.ReadUInt16(); // 
            reader.Skip(2); // Padding
            PacketCount = reader.ReadUInt32(); // Total Number of Packets across all Batches in file.
            VertexCount = reader.ReadUInt32(); // Total Number of Vertices across all batches within file.
            uint hierarchyDataOffset = reader.ReadUInt32();

            reader.BaseStream.Position = chunkStart + hierarchyDataOffset;

            List<InfoNode> infoNodes = new List<InfoNode>();
            InfoNode curNode = null;

            do
            {
                curNode = new InfoNode((HierarchyDataType)reader.ReadUInt16(), reader.ReadUInt16());
                infoNodes.Add(curNode);
            }
            while (curNode.Type != HierarchyDataType.Finish);

            m_hierarchy = new HierarchyNode();
            BuildSceneGraphFromInfoNodes(ref m_hierarchy, infoNodes, 0);
        }

        private int BuildSceneGraphFromInfoNodes(ref HierarchyNode parent, List<InfoNode> allNodes, int currentListIndex)
        {
            for (int i = currentListIndex; i < allNodes.Count; i++)
            {
                InfoNode curNode = allNodes[i];
                HierarchyNode newNode = null;

                switch (curNode.Type)
                {
                    case HierarchyDataType.NewNode:
                        // Increase the depth of the hierarchy by getting the latest child we have added to the mesh, and then processing the next
                        // nodes as its children. This function is recursive and will return the integer value of how many nodes it processed, this 
                        // allows us to skip the list forward that many now that they've been handled.
                        HierarchyNode latestChild = parent.Children[parent.Children.Count - 1];
                        i += BuildSceneGraphFromInfoNodes(ref latestChild, allNodes, i + 1);
                        break;

                    case HierarchyDataType.EndNode:
                        // Alternatively, if it's a EndNode, that's our signal to go up a level. We return the number of nodes that were processed between the last NewNode
                        // and this EndNode at this depth in the hierarchy.
                        return i - currentListIndex + 1;

                    case HierarchyDataType.Material:
                    case HierarchyDataType.Joint:
                    case HierarchyDataType.Batch:
                    case HierarchyDataType.Finish:

                        // If it's any of the above we simply create a node for them. We create and pull from a different InfoNode because
                        // Hitting a NewNode can modify the value of i so curNode is now no longer valid.
                        InfoNode thisNode = allNodes[i];
                        newNode = new HierarchyNode(thisNode.Type, thisNode.Value);
                        parent.Children.Add(newNode);
                        break;

                    default:
                        Console.WriteLine("Unsupported HierarchyDataType \"{0}\" in model!", curNode.Type);
                        break;
                }
            }

            return 0;
        }
    }
}
