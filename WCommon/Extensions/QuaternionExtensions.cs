using OpenTK;
using System;

namespace WindEditor
{
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Convert a Quaternion to Euler Angles. Returns the angles in [-180, 180] space in degrees.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="quat"></param>
        /// <returns></returns>
        public static Vector3 ToEulerAngles(this Quaternion quat)
        {
            return new Vector3(WMath.RadiansToDegrees(PitchFromQuat(quat)), WMath.RadiansToDegrees(YawFromQuat(quat)), WMath.RadiansToDegrees(RollFromQuat(quat)));
        }

        /// <summary>
        /// Create a Quaternion from Euler Angles. These should be in degrees in [-180, 180] space.
        /// </summary>
        public static Quaternion FromEulerAngles(this Quaternion quat, Vector3 eulerAngles)
        {
            eulerAngles.X = WMath.DegreesToRadians(eulerAngles.X);
            eulerAngles.Y = WMath.DegreesToRadians(eulerAngles.Y);
            eulerAngles.Z = WMath.DegreesToRadians(eulerAngles.Z);

            double c1 = Math.Cos(eulerAngles.Y / 2f);
            double s1 = Math.Sin(eulerAngles.Y / 2f);
            double c2 = Math.Cos(eulerAngles.X / 2f);
            double s2 = Math.Sin(eulerAngles.X / 2f);
            double c3 = Math.Cos(eulerAngles.Z / 2f);
            double s3 = Math.Sin(eulerAngles.Z / 2f);
            double c1c2 = c1 * c2;
            double s1s2 = s1 * s2;

            float w = (float)(c1c2 * c3 - s1s2 * s3);
            float x = (float)(c1c2 * s3 + s1s2 * c3);
            float y = (float)(s1 * c2 * c3 + c1 * s2 * s3);
            float z = (float)(c1 * s2 * c3 - s1 * c2 * s3);
            return new Quaternion(x, y, z, w);
        }

        private static float PitchFromQuat(Quaternion q)
        {
            return (float)Math.Atan2(2f * (q.W * q.X + q.Y * q.Z), 1 - (2 * (Math.Pow(q.X, 2) + Math.Pow(q.Y, 2))));
        }

        private static float YawFromQuat(Quaternion q)
        {
            return (float)Math.Asin(2f * (q.W * q.Y - q.X * q.Z));
        }

        private static float RollFromQuat(Quaternion q)
        {
            return (float)Math.Atan2(2 * (q.W * q.Z + q.X * q.Y), 1 - (2 * (Math.Pow(q.Y, 2) + Math.Pow(q.Z, 2))));
        }
    }
}
