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
            Debug.Assert(min.X <= max.X);
            Debug.Assert(min.Y <= max.Y);
            Debug.Assert(min.Z <= max.Z);

            Min = min;
            Max = max;

            Center = (max + min) / 2;
        }

        public bool Contains(Vector3 point)
        {
            return point.X >= Min.X && point.X <= Max.X &&
                point.Y >= Min.Y && point.Y <= Max.Y &&
                point.Z >= Min.Z && point.Z <= Max.Z;
        }

        /// <summary>
        /// Scales the <see cref="AABox"/> by the specified amount in each direction. This function will
        /// not change the center of the <see cref="AABox"/>, only the Min and Max extents.
        /// </summary>
        /// <param name="amount">Amount to uniformly scale the X, Y and Z axes by.</param>
        public void ScaleBy(float amount)
        {
            ScaleBy(new Vector3(amount, amount, amount));
        }

        /// <summary>
        /// Scales the <see cref="AABox"/> by the specified amount in each direction. This function will
        /// not change the center of the <see cref="AABox"/>, only the Min and Max extents.
        /// </summary>
        /// <param name="amount">Amounts to scale the min/max by on their X, Y, and Z axes.</param>
        public void ScaleBy(Vector3 amount)
        {
            Vector3 extents = (Max - Min);
            extents.X *= amount.X;
            extents.Y *= amount.Y;
            extents.Z *= amount.Z;

            Vector3 newCenter = Center;
            newCenter.X *= amount.X;
            newCenter.Y *= amount.Y;
            newCenter.Z *= amount.Z;

            Center = newCenter;

            Vector3 halfExtents = extents / 2;
            Max = Center + halfExtents;
            Min = Center - halfExtents;
        }
    }
}
