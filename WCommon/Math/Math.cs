using OpenTK;
using System;

namespace WindEditor
{
    public static class WMath
    {
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            if (value > max)
                value = max;

            return value;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                value = min;
            if (value > max)
                value = max;

            return value;
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * (float)(Math.PI / 180.0);
        }

        public static float RadiansToDegrees(float radians)
        {
            return radians * (float)(180.0 / Math.PI); 
        }

        public static float RotationShortToFloat(short rotation)
        {
            return rotation * (180 / 32768f);
        }

        public static short RotationFloatToShort(float rotation)
        {
            return (short)(rotation * (32768f / 180f));
        }

        public static bool RayIntersectsAABB(FRay ray, Vector3 aabbMin, Vector3 aabbMax, out float intersectionDistance)
        {
            Vector3 t_1 = new Vector3(), t_2 = new Vector3();

            float tNear = float.MinValue;
            float tFar = float.MaxValue;

            // Test infinite planes in each directin.
            for (int i = 0; i < 3; i++)
            {
                // Ray is parallel to planes in this direction.
                if (ray.Direction[i] == 0)
                {
                    if ((ray.Origin[i] < aabbMin[i]) || (ray.Origin[i] > aabbMax[i]))
                    {
                        // Parallel and outside of the box, thus no intersection is possible.
                        intersectionDistance = float.MinValue;
                        return false;
                    }
                }
                else
                {
                    t_1[i] = (aabbMin[i] - ray.Origin[i]) / ray.Direction[i];
                    t_2[i] = (aabbMax[i] - ray.Origin[i]) / ray.Direction[i];

                    // Ensure T_1 holds values for intersection with near plane.
                    if (t_1[i] > t_2[i])
                    {
                        Vector3 temp = t_2;
                        t_2 = t_1;
                        t_1 = temp;
                    }

                    if (t_1[i] > tNear)
                        tNear = t_1[i];

                    if (t_2[i] < tFar)
                        tFar = t_2[i];

                    if ((tNear > tFar) || (tFar < 0))
                    {
                        intersectionDistance = float.MinValue;
                        return false;
                    }
                }
            }

            intersectionDistance = tNear;
            return true;
        }

        public static bool RayIntersectsTriangle(FRay ray, Vector3 v1, Vector3 v2, Vector3 v3, out float intersectionDistance)
        {
            intersectionDistance = float.MinValue;

            // Find Vectors for two edges sharing v1
            Vector3 e1 = v2 - v1;
            Vector3 e2 = v3 - v1;

            // Begin calculating determinant - also used to calculate 'u' parameter
            Vector3 p = Vector3.Cross(ray.Direction, e2);

            // If determinant is near zero, ray lies in plane of triangle/ray is parallel to plane of triangle.
            float det = Vector3.Dot(e1, p);

            // No Collision
            if (det > -float.Epsilon && det < float.Epsilon)
                return false;

            float inv_det = 1f / det;

            // Calculate distance from V1 to ray origin
            Vector3 t = ray.Origin - v1;

            // Calculate 'u' parameter and test bound
            float u = Vector3.Dot(t, p) * inv_det;

            // No Collision - Intersection lies outside of the triangle.
            if (u < 0f || u > 1f)
                return false;

            // Prepare to test v parameter
            Vector3 q = Vector3.Cross(t, e1);

            // Calculate 'v' parameter and test bound
            float v = Vector3.Dot(ray.Direction, q) * inv_det;

            // No Collision - Intersection lies outside of the triangle.
            if (v < 0f || u + v > 1f)
                return false;

            float dist = Vector3.Dot(e2, q) * inv_det;
            if (dist > float.Epsilon)
            {
                intersectionDistance = dist;
                return true;
            }

            // No hit, no win.
            return false;
        }

        public static int Floor(float val)
        {
            return (int)Math.Floor(val);
        }
    }
}
