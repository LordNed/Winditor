using OpenTK;
using System.Diagnostics;

namespace Editor
{
    struct AABox
    {
        public Vector3 Min { get; private set; }
        public Vector3 Max { get; private set; }
        public Vector3 Center { get; private set; }

        public AABox(Vector3 min, Vector3 max)
        {
            Debug.Assert(min.X < max.X);
            Debug.Assert(min.Y < max.Y);
            Debug.Assert(min.Z < max.Z);

            Min = min;
            Max = max;

            Center = (max - min) / 2;
        }

        public bool Contains(Vector3 point)
        {
            return point.X >= Min.X && point.X <= Max.X &&
                point.Y >= Min.Y && point.Y <= Max.Y &&
                point.Z >= Min.Z && point.Z <= Max.Z;
        }
    }
}
