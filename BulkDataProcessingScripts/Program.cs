using OpenTK;
using System;
using WindEditor;

namespace BulkDataProcessingScripts
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ArchiveResourceToCSV(@"E:\New_Data_Drive\WindwakerModding\new_object_extract\res\extracted_archives\Object");
            //new ArcExtractorDebugging(@"E:\New_Data_Drive\WindwakerModding\root\res\Object\ff.arc");
            //Quaternion negQuat = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), WMath.DegreesToRadians(-0)) * Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(-179)) * Quaternion.FromAxisAngle(new Vector3(0, 1, 0), -3.1415f) * Quaternion.FromAxisAngle(new Vector3(0, 0, 1), WMath.DegreesToRadians(-179));
            //Quaternion negQuat = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(-165));
            //Console.WriteLine("Yaw: {0}", WMath.RadiansToDegrees(YawFromQuat(negQuat)));

            for(int i = 180; i >= -180; i-=5)
            {
                //Quaternion quat = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(i));
                Quaternion xQuat = FromEulerAngles(new Vector3(i, 0, 0));
                Quaternion yQuat = FromEulerAngles(new Vector3(0, i, 0));
                Quaternion zQuat = FromEulerAngles(new Vector3(0, 0, i));
                //Console.WriteLine("In: {0} Out: {1:0},{2:0},{3:0}", i, WMath.RadiansToDegrees(PitchFromQuat(zQuat)), WMath.RadiansToDegrees(YawFromQuat(yQuat)), WMath.RadiansToDegrees(RollFromQuat(xQuat)));
                Console.WriteLine("In: {0} Out: {1:0},{2:0},{3:0}", i, WMath.RadiansToDegrees(PitchFromQuat(yQuat)), WMath.RadiansToDegrees(YawFromQuat(yQuat)), WMath.RadiansToDegrees(RollFromQuat(yQuat)));
            }

            //negQuat = Quaternion.FromAxisAngle(new Vector3(0, 1, 0), WMath.DegreesToRadians(28));
            //Console.WriteLine("Yaw: {0}", WMath.RadiansToDegrees(YawFromQuat(negQuat)));

            //Vector3 eulAngles = EulerAnglesFromQuat(negQuat);
            //Console.WriteLine("Euler Angles from Quat: {0},{1},{2}", WMath.RadiansToDegrees(eulAngles.X), WMath.RadiansToDegrees(eulAngles.Y), WMath.RadiansToDegrees(eulAngles.Z));
            //Vector3 negEuler = new Vector3(WMath.RadiansToDegrees(PitchFromQuat(negQuat)), WMath.RadiansToDegrees(YawFromQuat(negQuat)), WMath.RadiansToDegrees(RollFromQuat(negQuat)));
            //Vector3 posEuler = new Vector3(WMath.RadiansToDegrees(PitchFromQuat(posQuat)), WMath.RadiansToDegrees(YawFromQuat(posQuat)), WMath.RadiansToDegrees(RollFromQuat(posQuat)));
            //Console.WriteLine("neg: {0} pos: {1}", negEuler, posEuler);
        }   

        // Gets X angle in radians from a quaternion
        private static float PitchFromQuat(Quaternion q)
        {
            //return (float)Math.Atan((2f * (q.Y * q.Z + q.W * q.X)) / (q.W * q.W - q.X * q.X - q.Y * q.Y + q.Z * q.Z));
            //return (float)Math.Atan2(-2f * (q.Y * q.Z - q.W * q.X), q.W * q.W - q.X * q.X - q.Y * q.Y + q.Z * q.Z);
            return (float)Math.Atan2(2f * (q.W * q.X + q.Y * q.Z), 1 - (2 * (Math.Pow(q.X, 2) + Math.Pow(q.Y, 2))));
        }

        // Gets Y angle in radians from a quaternion
        // Appears to be broken?
        private static float YawFromQuat(Quaternion q)
        {
            //return (float)Math.Asin(WMath.Clamp((2f) * (q.X * q.Z - q.W * q.Y), -1f, 1f));
            //return (float)Math.Asin(2 * (q.X * q.Z - q.Y * q.W));
            //return (float)Math.Atan2(-2f * (q.X * q.Y + q.Z * q.W), (q.X * q.X - q.Y * q.Y - q.Z * q.Z + q.W * q.W));

            //return (float)Math.Asin(2f * (q.W * q.Y - q.Z*q.X));
            //return (float)Math.Atan2(2.0 * (q.X * q.Y + q.Z * q.W), (q.X * q.X) - (q.Y * q.Y) - (q.Z * q.Z) + (q.W * q.W));
            return (float)Math.Asin(2f * (q.W * q.Y - q.X * q.Z));
        }

        // Gets Z angle in radians from a quaternion
        private static float RollFromQuat(Quaternion q)
        {
            //return (float)Math.Atan((2f * (q.X * q.Y + q.W * q.Z)) / (q.W * q.W + q.X * q.X - q.Y * q.Y - q.Z * q.Z));
            //return (float)Math.Atan2(2 * (q.W * q.Z + q.X * q.Y), 1 - (2 * (Math.Pow(q.Y, 2) + Math.Pow(q.Z, 2))));
            return (float)Math.Atan2(2 * (q.W * q.Z + q.X * q.Y), 1 - (2 * (Math.Pow(q.Y, 2) + Math.Pow(q.Z, 2))));
        }

        private static Quaternion FromEulerAngles(Vector3 e)
        {
            e.X = WMath.DegreesToRadians(e.X);
            e.Y = WMath.DegreesToRadians(e.Y);
            e.Z = WMath.DegreesToRadians(e.Z);

            double c1 = Math.Cos(e.Y / 2f);
            double s1 = Math.Sin(e.Y / 2f);
            double c2 = Math.Cos(e.X / 2f);
            double s2 = Math.Sin(e.X / 2f);
            double c3 = Math.Cos(e.Z / 2f);
            double s3 = Math.Sin(e.Z / 2f);
            double c1c2 = c1 * c2;
            double s1s2 = s1 * s2;

            float w = (float)(c1c2 * c3 - s1s2 * s3);
            float x = (float)(c1c2 * s3 + s1s2 * c3);
            float y = (float)(s1 * c2 * c3 + c1 * s2 * s3);
            float z = (float)(c1 * s2 * c3 - s1 * c2 * s3);
            return new Quaternion(x, y, z, w);
        }

        // Ignore, doesn't work. Bad.
        private static Vector3 EulerAnglesFromQuat(Quaternion q)
        {
            float singularityTest = q.Z * q.X - q.W * q.Y;
            float yawY = 2f * (q.W * q.Z + q.X * q.Y);
            float yawX = (float)(1f - 2f * (Math.Pow(q.Y, 2) + Math.Pow(q.Z, 2)));
            float singularityThreshold = 0.4999995f;

            Vector3 eulerAngles = Vector3.Zero;

            if(singularityTest < singularityThreshold)
            {
                eulerAngles.X = -90f;
                eulerAngles.Y = (float)Math.Atan2(yawY, yawX);
                eulerAngles.Z = WMath.Clamp(-eulerAngles.Y - (2f * (float)Math.Atan2(q.X, q.W)), (float)-Math.PI, (float)Math.PI);
            }
            else if(singularityTest > singularityThreshold)
            {
                eulerAngles.X = 90;
                eulerAngles.Y = (float)Math.Atan2(yawY, yawX);
                eulerAngles.Z = WMath.Clamp(eulerAngles.Y - (2f * (float)Math.Atan2(q.X, q.W)), (float)-Math.PI, (float)Math.PI);
            }
            else
            {
                eulerAngles.X = (float)Math.Asin(2f * (singularityTest));
                eulerAngles.Y = (float)Math.Atan2(yawY, yawX);
                eulerAngles.Z = (float)Math.Atan2(-2f * (q.W * q.X + q.Y * q.Z), (1f - 2f * (Math.Pow(q.X, 2) + Math.Pow(q.Y, 2))));
            }

            return eulerAngles;
        }
    }
}
