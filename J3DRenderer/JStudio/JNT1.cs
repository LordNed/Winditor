using System.Collections.Generic;
using GameFormatReader.Common;
using System.Diagnostics;
using OpenTK;
using WindEditor;
using System;

namespace J3DRenderer.JStudio
{
    public class SkeletonJoint
    {
        public string Name { get; internal set; }
        public ushort Unknown1 { get; internal set; }
        public ushort Unknown2 { get; internal set; }
        public Vector3 Scale { get; internal set; }
        public Quaternion Rotation { get; internal set; }
        public Vector3 Translation { get; internal set; }
        public float BoundingSphereDiameter { get; internal set; }
        public AABox BoundingBox { get; internal set; }

        // Useful for easier traversal
        public int ParentId { get; internal set; }
    }

    public class JNT1
    {
        public List<short> JointRemapTable;
        public List<SkeletonJoint> Joints;

        public void LoadJNT1FromStream(EndianBinaryReader reader, long tagStart)
        {
            ushort numJoints = reader.ReadUInt16();
            Trace.Assert(reader.ReadUInt16() == 0xFFFF); // Padding

            int jointDataOffset = reader.ReadInt32();
            int jointRemapDataOffset = reader.ReadInt32();
            int stringTableOffset = reader.ReadInt32();

            // Joint Names
            reader.BaseStream.Position = tagStart + stringTableOffset;
            StringTable nameTable = StringTable.FromStream(reader);

            // Joint Index Remap
            reader.BaseStream.Position = tagStart + jointRemapDataOffset;
            JointRemapTable = new List<short>();
            for (int i = 0; i < numJoints; i++)
                JointRemapTable.Add(reader.ReadInt16());

            // Joint Data
            reader.BaseStream.Position = tagStart + jointDataOffset;
            Joints = new List<SkeletonJoint>();
            for(int j = 0; j < numJoints; j++)
            {
                SkeletonJoint joint = new SkeletonJoint();
                Joints.Add(joint);

                joint.Name = nameTable[j];
                joint.Unknown1 = reader.ReadUInt16();
                joint.Unknown2 = reader.ReadUInt16();
                joint.Scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                Vector3 eulerRot = new Vector3();
                for (int e = 0; e < 3; e++)
                    eulerRot[e] = reader.ReadInt16() * (180 / 32786f); // [-32786, 32786] to [-180, 180]

                // ZYX order
                joint.Rotation = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(eulerRot.Z)) * 
                                 Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(eulerRot.Y)) * 
                                 Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(eulerRot.X));

                Trace.Assert(reader.ReadUInt16() == 0xFFFF); // Padding
                joint.Translation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                joint.BoundingSphereDiameter = reader.ReadSingle();
                joint.BoundingBox = new AABox(new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()), new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));


                joint.ParentId = -1;
            }
        }

        public void CalculateInverseBindPose(HierarchyNode hierarchyRoot, WLineBatcher lineBatcher)
        {
            List<SkeletonJoint> processedJoints = new List<SkeletonJoint>();
            IterateHierarchyForSkeletonRecursive(hierarchyRoot, processedJoints, -1, lineBatcher);
        }

        private void IterateHierarchyForSkeletonRecursive(HierarchyNode curNode, List<SkeletonJoint> processedJoints, int parentIndex, WLineBatcher lineBatcher)
        {
            switch (curNode.Type)
            {
                case HierarchyDataType.NewNode: parentIndex = processedJoints.Count - 1; break;
                case HierarchyDataType.Joint:
                    SkeletonJoint joint = Joints[JointRemapTable[curNode.Value]];
                    joint.ParentId = parentIndex;

                    if (joint.ParentId >= 0)
                    {
                        SkeletonJoint parentJoint = processedJoints[parentIndex];

                        Vector3 worldPos = parentJoint.Translation +  Vector3.Transform(joint.Translation, parentJoint.Rotation);
                        Quaternion worldRot = (parentJoint.Rotation * joint.Rotation).Normalized(); // ToDo: Is the Normalized needed?

                        // We store away a clone of the existing joint for the purposes of not having to walk the entire chain again
                        // for each bone. This lets us store the world-position and rotation of the parent joint for multiplication above.
                        SkeletonJoint worldJoint = new SkeletonJoint();
                        worldJoint.Name = joint.Name;
                        worldJoint.Translation = worldPos;
                        worldJoint.Rotation = worldRot;
                        processedJoints.Add(worldJoint);

                        // Debug Drawing
                        //lineBatcher.DrawLine(parentJoint.Translation, worldPos, WLinearColor.Red, 5, 300f);
                    }
                    else
                    {
                        processedJoints.Add(joint);
                    }

                    break;
            }

            foreach (var child in curNode.Children)
                IterateHierarchyForSkeletonRecursive(child, processedJoints, parentIndex, lineBatcher);
        }
    }
}
