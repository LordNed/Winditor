using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using GameFormatReader.Common;

namespace SuperBMDLib.Util
{
    public class BoundingVolume
    {
        public float SphereRadius { get; private set; }
        public Vector3 MinBounds { get; private set; }
        public Vector3 MaxBounds { get; private set; }

        public Vector3 Center { get; private set; }

        public BoundingVolume()
        {
            MinBounds = new Vector3();
            MaxBounds = new Vector3();
        }

        public BoundingVolume(EndianBinaryReader reader)
        {
            SphereRadius = reader.ReadSingle();

            MinBounds = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            MaxBounds = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public void GetBoundsValues(List<Vector3> positions)
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float minZ = float.MaxValue;

            float maxX = float.MinValue;
            float maxY = float.MinValue;
            float maxZ = float.MinValue;

            foreach (Vector3 vec in positions)
            {
                if (vec.X > maxX)
                    maxX = vec.X;
                if (vec.Y > maxY)
                    maxY = vec.Y;
                if (vec.Z > maxZ)
                    maxZ = vec.Z;

                if (vec.X < minX)
                    minX = vec.X;
                if (vec.Y < minY)
                    minY = vec.Y;
                if (vec.Z < minZ)
                    minZ = vec.Z;
            }

            MinBounds = new Vector3(minX, minY, minZ);
            MaxBounds = new Vector3(maxX, maxY, maxZ);
            Center = (MaxBounds + MinBounds) / 2;
            SphereRadius = (MaxBounds - Center).Length;
        }

        public void Write(EndianBinaryWriter writer)
        {
            writer.Write(SphereRadius);
            writer.Write(MinBounds);
            writer.Write(MaxBounds);
        }
    }
}
