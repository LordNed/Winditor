using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return ToEulerAnglesRobust(quat.ToDoublePrecision(), "ZYX")[0];
        }

        public static Vector3 ToEulerAngles(this Quaterniond quat)
        {
            return ToEulerAnglesRobust(quat, "ZYX")[0];
        }

        /// <summary>
        /// Convert a Quaternion to all possible ways it can be represented as Euler Angles.
        /// Returns the angles in [-180, 180] space in degrees.
        /// </summary>
        public static List<Vector3> ToEulerAnglesRobust(this Quaterniond quat, string rotationOrder)
        {
            var representations = new List<Vector3>();

            var qx = quat.X;
            var qy = quat.Y;
            var qz = quat.Z;
            var qw = quat.W;

            // Convert the quaternion to a 3x3 matrix.
            // We don't use OpenTK's Matrix3 class because it stores the values as single-precision floats, which loses information.
            // By manually calculating the matrix elements as doubles, we can get the maximum amount of accuracy.
            double m11 = 1.0 - 2.0 * (qy * qy + qz * qz);
            double m12 =       2.0 * (qx * qy - qz * qw);
            double m13 =       2.0 * (qx * qz + qy * qw);
            double m21 =       2.0 * (qx * qy + qz * qw);
            double m22 = 1.0 - 2.0 * (qx * qx + qz * qz);
            double m23 =       2.0 * (qy * qz - qx * qw);
            double m31 =       2.0 * (qx * qz - qy * qw);
            double m32 =       2.0 * (qy * qz + qx * qw);
            double m33 = 1.0 - 2.0 * (qx * qx + qy * qy);

            double x, y, z;

            switch (rotationOrder)
            {
                case "ZYX":
                    y = Math.Asin(-Math.Min(1, Math.Max(-1, m31)));
                    if (Math.Abs(m31) < 0.999999)
                    {
                        x = Math.Atan2(m32, m33);
                        z = Math.Atan2(m21, m11);
                    }
                    else
                    {
                        x = Math.Atan2(-m12, m22);
                        z = 0f;
                    }
                    representations.Add(new Vector3(
                        WMath.RadiansToDegrees((float)x),
                        WMath.RadiansToDegrees((float)y),
                        WMath.RadiansToDegrees((float)z)
                    ));
                
                    y = CopySign(Math.PI, y) - y;
                    x = x - CopySign(Math.PI, x);
                    z = z - CopySign(Math.PI, z);
                    representations.Add(new Vector3(
                        WMath.RadiansToDegrees((float)x),
                        WMath.RadiansToDegrees((float)y),
                        WMath.RadiansToDegrees((float)z)
                    ));
                    break;
                case "YXZ":
                    x = Math.Asin(-Math.Min(1, Math.Max(-1, m23)));
                    if (Math.Abs(m23) < 0.999999)
                    {
                        y = Math.Atan2(m13, m33);
                        z = Math.Atan2(m21, m22);
                    }
                    else
                    {
                        y = Math.Atan2(-m31, m11);
                        z = 0f;
                    }
                    representations.Add(new Vector3(
                        WMath.RadiansToDegrees((float)x),
                        WMath.RadiansToDegrees((float)y),
                        WMath.RadiansToDegrees((float)z)
                    ));
                
                    x = CopySign(Math.PI, x) - x;
                    y = y - CopySign(Math.PI, y);
                    z = z - CopySign(Math.PI, z);
                    representations.Add(new Vector3(
                        WMath.RadiansToDegrees((float)x),
                        WMath.RadiansToDegrees((float)y),
                        WMath.RadiansToDegrees((float)z)
                    ));
                    break;
                default:
                    throw new NotImplementedException($"Quaternion to euler rotation conversion not implemented for rotation order: {rotationOrder}");
            }

            return representations;
        }

        public static Vector3 ToIdealEulerAngles(this Quaterniond quat, string rotationOrder, bool usesX, bool usesY, bool usesZ)
        {
            // First get all possible euler representations of this quaternion rotation.
            List<Vector3> eulerReps = quat.ToEulerAnglesRobust(rotationOrder);

            // Then sort the representations by how close to ideal they are for storing.
            var eulerRepsSorted = eulerReps.OrderBy(rep =>
            {
                // First sort by how little the representation would change upon being save and reloaded.
                // e.g. (0, -135, 0) and (180, -45, 180) might be visually the same, but if we can't store the Z axis we want to use the former.
                var storableRep = new Vector3(
                    usesX ? rep.X : 0,
                    usesY ? rep.Y : 0,
                    usesZ ? rep.Z : 0
                );
                var diff = storableRep - rep;
                return diff.Length;
            });
            eulerRepsSorted = eulerRepsSorted.ThenBy(rep =>
            {
                // Next sort by how few axes are nonzero.
                // e.g. Between (0, 45, 90) and (-180, 135, -90), prefer the former because it looks neater and is more likely to be the original rotation read.
                var numUsedAxes = 0;
                if (Math.Abs(rep.X) >= 0.000001) { numUsedAxes += 1; }
                if (Math.Abs(rep.Y) >= 0.000001) { numUsedAxes += 1; }
                if (Math.Abs(rep.Z) >= 0.000001) { numUsedAxes += 1; }
                return numUsedAxes;
            });

            // Return the most ideal representation.
            return eulerRepsSorted.First();
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

        public static Quaterniond FromEulerAnglesRobust(this Quaterniond quat, Vector3 eulerAngles, string rotationOrder, bool usesX, bool usesY, bool usesZ)
        {
            quat = Quaterniond.Identity;

            foreach (var axis in rotationOrder)
            {
                int axisIndex = "XYZ".IndexOf(axis);
                if (new[] { usesX, usesY, usesZ }[axisIndex])
                {
                    float thisAxisRot = new[] { eulerAngles.X, eulerAngles.Y, eulerAngles.Z }[axisIndex];
                    Vector3d axisUnitVector = new Vector3d(axis == 'X' ? 1 : 0, axis == 'Y' ? 1 : 0, axis == 'Z' ? 1 : 0);
                    Quaterniond thisAxisRotQ = Quaterniond.FromAxisAngle(axisUnitVector, WMath.DegreesToRadians(thisAxisRot));
                    quat *= thisAxisRotQ;
                };
            }

            return quat;
        }

        /// <summary>
        /// Returns a value with the sign of signValue and the magnitude of magnitudeValue.
        /// Simply doing (Math.Sign(signValue) * magnitudeValue) doesn't work when signValue is zero, that would return zero regardless of magnitude.
        /// </summary>
        private static double CopySign(double magnitudeValue, double signValue)
        {
            return signValue < 0 ? -magnitudeValue : magnitudeValue;
        }

		public static float FindQuaternionTwist(this Quaternion quat, Vector3 axis)
		{
			axis.Normalize();

			// Get the plane the axis is a normal of
			Vector3 orthoNormal1, orthoNormal2;
			WMath.FindOrthoNormals(axis, out orthoNormal1, out orthoNormal2);
			Vector3 transformed = Vector3.Transform(orthoNormal1, quat);

			// Project transformed vector onto a plane
			Vector3 flattened = transformed - (Vector3.Dot(transformed, axis) * axis);
			flattened.Normalize();

			// Get the angle between the original vector and projected transform to get angle around normal
			float a = (float)Math.Acos((double)Vector3.Dot(orthoNormal1, flattened));
			return WMath.RadiansToDegrees(a);
        }

        public static Quaternion ToSinglePrecision(this Quaterniond quat)
        {
            return new Quaternion((float)quat.X, (float)quat.Y, (float)quat.Z, (float)quat.W);
        }

        public static Quaterniond ToDoublePrecision(this Quaternion quat)
        {
            return new Quaterniond(quat.X, quat.Y, quat.Z, quat.W);
        }
    }
}
