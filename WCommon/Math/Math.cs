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

        public static bool RayIntersectsAABB(WRay ray, Vector3 aabbMin, Vector3 aabbMax, out float intersectionDistance)
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

        public static int Floor(float val)
        {
            return (int)Math.Floor(val);
        }
    }
}
