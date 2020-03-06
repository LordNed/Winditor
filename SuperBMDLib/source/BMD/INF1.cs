using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib.Scenegraph;
using SuperBMDLib.Scenegraph.Enums;
using GameFormatReader.Common;
using Assimp;
using SuperBMDLib.Util;

namespace SuperBMDLib.BMD
{
    public class INF1
    {
        public List<SceneNode> FlatNodes { get; private set; }
        public SceneNode Root { get; private set; }

        public INF1(EndianBinaryReader reader, int offset)
        {
            FlatNodes = new List<SceneNode>();

            reader.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
            reader.SkipInt32();
            int inf1Size = reader.ReadInt32();
            int unk1 = reader.ReadInt16();
            reader.SkipInt16();

            int packetCount = reader.ReadInt32();
            int vertexCount = reader.ReadInt32();
            int hierarchyOffset = reader.ReadInt32();

            SceneNode parent = new SceneNode(reader, null);
            SceneNode node = null;

            Root = parent;
            FlatNodes.Add(parent);

            do
            {
                node = new SceneNode(reader, parent);

                FlatNodes.Add(node);

                if (node.Type == NodeType.OpenChild)
                {
                    SceneNode newNode = new SceneNode(reader, node.Parent);
                    FlatNodes.Add(newNode);
                    parent = newNode;
                }
                else if (node.Type == NodeType.CloseChild)
                    parent = node.Parent;

            } while (node.Type != NodeType.Terminator);

            reader.BaseStream.Seek(offset + inf1Size, System.IO.SeekOrigin.Begin);
        }

        public INF1(Scene scene, JNT1 skeleton)
        {
            FlatNodes = new List<SceneNode>();
            Root = new SceneNode(NodeType.Joint, 0, null);
            FlatNodes.Add(Root);

            int downNodeCount = 0;

            // First add objects that should be the direct children of the root bone.
            // This includes any objects that are weighted to multiple bones, as well as objects weighted to only the root bone itself.
            for (int mat_index = 0; mat_index < scene.MaterialCount; mat_index++)
            {
                for (int i = 0; i < scene.MeshCount; i++)
                {
                    if (scene.Meshes[i].MaterialIndex != mat_index)
                        continue;
                    if (scene.Meshes[i].BoneCount == 1 && scene.Meshes[i].Bones[0].Name != skeleton.FlatSkeleton[0].Name)
                        continue;
                    
                    SceneNode downNode1 = new SceneNode(NodeType.OpenChild, 0, Root);
                    SceneNode matNode = new SceneNode(NodeType.Material, scene.Meshes[i].MaterialIndex, Root);
                    SceneNode downNode2 = new SceneNode(NodeType.OpenChild, 0, Root);
                    SceneNode shapeNode = new SceneNode(NodeType.Shape, i, Root);

                    FlatNodes.Add(downNode1);
                    FlatNodes.Add(matNode);
                    FlatNodes.Add(downNode2);
                    FlatNodes.Add(shapeNode);

                    downNodeCount += 2;
                }
            }

            // Next add objects as children of specific bones, if those objects are weighted to only a single bone.
            if (skeleton.FlatSkeleton.Count > 1)
            {
                SceneNode rootChildDown = new SceneNode(NodeType.OpenChild, 0, Root);
                FlatNodes.Add(rootChildDown);

                foreach (Rigging.Bone bone in skeleton.SkeletonRoot.Children)
                {
                    GetNodesRecursive(bone, skeleton.FlatSkeleton, Root, scene.Meshes, scene.Materials);
                }

                SceneNode rootChildUp = new SceneNode(NodeType.CloseChild, 0, Root);
                FlatNodes.Add(rootChildUp);
            }

            for (int i = 0; i < downNodeCount; i++)
                FlatNodes.Add(new SceneNode(NodeType.CloseChild, 0, Root));

            FlatNodes.Add(new SceneNode(NodeType.Terminator, 0, Root));
        }

        private void GetNodesRecursive(Rigging.Bone bone, List<Rigging.Bone> skeleton, SceneNode parent, List<Mesh> meshes, List<Material> materials)
        {
            SceneNode node = new SceneNode(NodeType.Joint, skeleton.IndexOf(bone), parent);
            FlatNodes.Add(node);

            int downNodeCount = 0;

            for (int mat_index = 0; mat_index < materials.Count; mat_index++)
            {
                foreach (Mesh mesh in meshes)
                {
                    if (mesh.MaterialIndex != mat_index)
                        continue;
                    if (mesh.BoneCount != 1 || mesh.Bones[0].Name != bone.Name)
                        continue;

                    SceneNode downNode1 = new SceneNode(NodeType.OpenChild, 0, Root);
                    SceneNode matNode = new SceneNode(NodeType.Material, mesh.MaterialIndex, Root);
                    SceneNode downNode2 = new SceneNode(NodeType.OpenChild, 0, Root);
                    SceneNode shapeNode = new SceneNode(NodeType.Shape, meshes.IndexOf(mesh), Root);

                    FlatNodes.Add(downNode1);
                    FlatNodes.Add(matNode);
                    FlatNodes.Add(downNode2);
                    FlatNodes.Add(shapeNode);

                    downNodeCount += 2;
                }
            }

            if (bone.Children.Count > 0)
            {
                SceneNode downNode = new SceneNode(NodeType.OpenChild, 0, parent);
                FlatNodes.Add(downNode);

                foreach (Rigging.Bone child in bone.Children)
                {
                    GetNodesRecursive(child, skeleton, node, meshes, materials);
                }

                SceneNode upNode = new SceneNode(NodeType.CloseChild, 0, parent);
                FlatNodes.Add(upNode);
            }

            for (int i = 0; i < downNodeCount; i++)
                FlatNodes.Add(new SceneNode(NodeType.CloseChild, 0, Root));
        }

        public void FillScene(Scene scene, List<Rigging.Bone> flatSkeleton, bool useSkeletonRoot)
        {
            Node root = scene.RootNode;

            if (useSkeletonRoot)
                root = new Node("skeleton_root");

            SceneNode curRoot = Root;
            SceneNode lastNode = Root;

            Node curAssRoot = new Node(flatSkeleton[0].Name, root);
            Node lastAssNode = curAssRoot;
            root.Children.Add(curAssRoot);

            for (int i = 1; i < FlatNodes.Count; i++)
            {
                SceneNode curNode = FlatNodes[i];

                if (curNode.Type == NodeType.OpenChild)
                {
                    curRoot = lastNode;
                    curAssRoot = lastAssNode;
                }
                else if (curNode.Type == NodeType.CloseChild)
                {
                    curRoot = curRoot.Parent;
                    curAssRoot = curAssRoot.Parent;
                }
                else if (curNode.Type == NodeType.Joint)
                {
                    Node assCurNode = new Node(flatSkeleton[curNode.Index].Name, curAssRoot);
                    assCurNode.Transform = flatSkeleton[curNode.Index].TransformationMatrix.ToMatrix4x4();
                    curAssRoot.Children.Add(assCurNode);

                    lastNode = curNode;
                    lastAssNode = assCurNode;
                }
                else if (curNode.Type == NodeType.Terminator)
                    break;
                else
                {
                    Node assCurNode = new Node($"delete", curAssRoot);
                    curAssRoot.Children.Add(assCurNode);

                    lastNode = curNode;
                    lastAssNode = assCurNode;
                }
            }

            DeleteNodesRecursive(root);

            if (useSkeletonRoot)
            {
                scene.RootNode.Children.Add(root);
            }
        }

        private void DeleteNodesRecursive(Node assNode)
        {
            if (assNode.Name == "delete")
            {
                for (int i = 0; i < assNode.Children.Count; i++)
                {
                    Node newChild = new Node(assNode.Children[i].Name, assNode.Parent);
                    newChild.Transform = assNode.Children[i].Transform;

                    for (int j = 0; j < assNode.Children[i].Children.Count; j++)
                        newChild.Children.Add(assNode.Children[i].Children[j]);

                    assNode.Children[i] = newChild;
                    assNode.Parent.Children.Add(assNode.Children[i]);
                }

                assNode.Parent.Children.Remove(assNode);
            }

            for (int i = 0; i < assNode.Children.Count; i++)
                DeleteNodesRecursive(assNode.Children[i]);
        }

        public void CorrectMaterialIndices(Scene scene, MAT3 materials)
        {
            foreach (SceneNode node in FlatNodes)
            {
                if (node.Type == NodeType.Shape)
                {
                    if (node.Index < scene.Meshes.Count)
                    {
                        int matIndex = node.Parent.Index;
                        scene.Meshes[node.Index].MaterialIndex = matIndex;
                    }
                }
            }
        }

        public void Write(EndianBinaryWriter writer, int packetCount, int vertexCount)
        {
            long start = writer.BaseStream.Position;

            writer.Write("INF1".ToCharArray());
            writer.Write(0); // Placeholder for section size
            writer.Write((short)1);
            writer.Write((short)-1);

            writer.Write(packetCount); // Number of packets
            writer.Write(vertexCount); // Number of vertex positions
            writer.Write(0x18);

            foreach (SceneNode node in FlatNodes)
            {
                writer.Write((short)node.Type);
                writer.Write((short)node.Index);
            }

            Util.StreamUtility.PadStreamWithString(writer, 32);

            long end = writer.BaseStream.Position;
            long length = (end - start);

            writer.Seek((int)start + 4, System.IO.SeekOrigin.Begin);
            writer.Write((int)length);
            writer.Seek((int)end, System.IO.SeekOrigin.Begin);
        }
    }
}
