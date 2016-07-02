using System;
using OpenTK;

namespace WindEditor
{
    public enum Halfspace
    {
        Negative = -1,  // Entirely behind the Normal
        Intersect = 0,  // Intersects the Plane
        Positive = 1    // Entirely above the Normal
    }

    public struct WPlane
    {
        public Vector3 Normal;
        public float Distance;

        public WPlane(Vector3 normal, float distance)
        {
            Normal = normal;
            Distance = distance;
        }

        public WPlane(Vector3 normal, Vector3 pointOnPlane)
        {
            Normal = normal.Normalized();
            Distance = -Vector3.Dot(normal, pointOnPlane);
        }

        public WPlane(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 d12 = p2 - p1;
            Vector3 d13 = p3 - p1;
            Vector3 normal = Vector3.Cross(d12, d13);
            Normal = normal.Normalized();
            Distance = -Vector3.Dot(Normal, p1);
        }

        /// <summary>
        /// Normalize the plane so that the <see cref="Normal"/> becomes a unit vector. This modifies
        /// the plane equation as the <see cref="Distance"/> will also change while normalizing.
        /// </summary>
        public void Normalize()
        {
            float magnitude = (float)Math.Sqrt(Normal.X * Normal.X * +Normal.Y * Normal.Y + Normal.Z * Normal.Z);
            Normal.X /= magnitude;
            Normal.Y /= magnitude;
            Normal.Z /= magnitude;
            Distance /= magnitude;
        }

        public bool RayIntersectsPlane(WRay ray, out float intersectDist)
        {
            float a = Vector3.Dot(ray.Direction, Normal);
            float num = -Vector3.Dot(ray.Origin, Normal) - Distance;

            if (Math.Abs(a) < float.Epsilon)
            {
                intersectDist = 0f;
                return false;
            }
            intersectDist = num / a;
            return intersectDist > 0f;
        }

        public Halfspace SideOfPlane(Vector3 point)
        {
            float d = Normal.X * point.X + Normal.Y * point.Y + Normal.Z * point.Z + Distance;
            if (d < 0) return Halfspace.Negative;
            if (d > 0) return Halfspace.Positive;

            return Halfspace.Intersect;
        }

        public override string ToString()
        {
            return string.Format("Normal: {0} Distance: {1}", Normal, Distance);
        }
    }
}
