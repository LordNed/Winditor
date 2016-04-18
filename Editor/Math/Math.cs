using System;

namespace Editor
{
    static class WMath
    {
        public const float Rad2Deg = (float)(180.0 / Math.PI);
        public const float Deg2Rad = (float)(Math.PI / 180.0);

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
    }
}
