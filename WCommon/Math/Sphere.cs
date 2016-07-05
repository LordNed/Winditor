using OpenTK;
using System;

namespace WindEditor
{
    public struct WSphere
    {
        public Vector3 Center;
        public float Radius;

        public WSphere(Vector3 center, float radius)
        {
            if (radius < 0)
                throw new ArgumentException("Sphere must have a non-negative radius.", "radius");

            Center = center;
            Radius = radius;
        }
    }
}
