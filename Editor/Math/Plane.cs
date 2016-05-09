using System;
using OpenTK;

namespace WindEditor
{
    struct WPlane
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
    }
}
