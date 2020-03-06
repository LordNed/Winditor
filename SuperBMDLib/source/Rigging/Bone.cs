using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;
using SuperBMDLib.Util;

namespace SuperBMDLib.Rigging
{
    public class Bone
    {
        public string Name { get; private set; }
        public Bone Parent { get; private set; }
        public List<Bone> Children { get; private set; }
        public Matrix4 InverseBindMatrix { get; private set; }
        public Matrix4 TransformationMatrix { get; private set; }
        public BoundingVolume Bounds { get; private set; }

        private short m_MatrixType;
        private byte m_UnknownIndex;
        private Vector3 m_Scale;
        private Quaternion m_Rotation;
        private Vector3 m_Translation;

        public Bone(string name)
        {
            Name = name;
            Children = new List<Bone>();
            Bounds = new BoundingVolume();
            m_Scale = Vector3.One;
        }

        public Bone(EndianBinaryReader reader, string name)
        {
            Children = new List<Bone>();

            Name = name;
            m_MatrixType = reader.ReadInt16();
            m_UnknownIndex = reader.ReadByte();

            reader.SkipByte();

            m_Scale = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            short xRot = reader.ReadInt16();
            short yRot = reader.ReadInt16();
            short zRot = reader.ReadInt16();

            float xConvRot = xRot * (180 / 32768f);
            float yConvRot = yRot * (180 / 32768f);
            float zConvRot = zRot * (180 / 32768f);

            Vector3 rotFull = new Vector3(xConvRot * (float)(Math.PI / 180f), yConvRot * (float)(Math.PI / 180f), zConvRot * (float)(Math.PI / 180f));

            m_Rotation = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), rotFull.Z) *
                         Quaternion.FromAxisAngle(new Vector3(0, 1, 0), rotFull.Y) *
                         Quaternion.FromAxisAngle(new Vector3(1, 0, 0), rotFull.X);

            reader.SkipInt16();

            m_Translation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            TransformationMatrix = Matrix4.CreateScale(m_Scale) *
                                   Matrix4.CreateFromQuaternion(m_Rotation) *
                                   Matrix4.CreateTranslation(m_Translation);

            Bounds = new BoundingVolume(reader);
        }

        public Bone(Assimp.Node node, Rigging.Bone parent)
        {
            Children = new List<Bone>();

            m_MatrixType = 1;
            Name = node.Name;
            Parent = parent;

            TransformationMatrix = new Matrix4(
                node.Transform.A1, node.Transform.B1, node.Transform.C1, node.Transform.D1,
                node.Transform.A2, node.Transform.B2, node.Transform.C2, node.Transform.D2,
                node.Transform.A3, node.Transform.B3, node.Transform.C3, node.Transform.D3,
                node.Transform.A4, node.Transform.B4, node.Transform.C4, node.Transform.D4);

            m_Scale = TransformationMatrix.ExtractScale();
            m_Rotation = TransformationMatrix.ExtractRotation();
            m_Translation = TransformationMatrix.ExtractTranslation();

            Bounds = new BoundingVolume();
        }

        public void SetInverseBindMatrix(Matrix4 matrix)
        {
            InverseBindMatrix = matrix;
        }

        public byte[] ToBytes()
        {
            List<byte> outList = new List<byte>();

            using (System.IO.MemoryStream mem = new System.IO.MemoryStream())
            {
                EndianBinaryWriter writer = new EndianBinaryWriter(mem, Endian.Big);

                writer.Write(m_MatrixType);
                writer.Write(m_UnknownIndex);
                writer.Write((sbyte)-1);

                ushort[] compressRot = J3DUtility.CompressRotation(m_Rotation.ToEulerAngles());

                writer.Write(m_Scale);
                writer.Write(compressRot[0]);
                writer.Write(compressRot[1]);
                writer.Write(compressRot[2]);
                writer.Write((short)-1);
                writer.Write(m_Translation);

                Bounds.Write(writer);

                outList.AddRange(mem.ToArray());
            }

            return outList.ToArray();
        }
    }
}
