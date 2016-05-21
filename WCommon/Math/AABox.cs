﻿using OpenTK;
using System.Diagnostics;

namespace WindEditor
{
    public struct AABox
    {
        public Vector3 Min { get { return m_min; } }
        public Vector3 Max { get { return m_max; } }
        public Vector3 Center { get { return (Max + Min) / 2; } }

        private Vector3 m_min;
        private Vector3 m_max;

        public AABox(Vector3 min, Vector3 max)
        {
            Debug.Assert(min.X <= max.X);
            Debug.Assert(min.Y <= max.Y);
            Debug.Assert(min.Z <= max.Z);

            m_min = min;
            m_max = max;
        }

        public bool Contains(Vector3 point)
        {
            return point.X >= Min.X && point.X <= Max.X &&
                point.Y >= Min.Y && point.Y <= Max.Y &&
                point.Z >= Min.Z && point.Z <= Max.Z;
        }

        /// <summary>
        /// Scales the <see cref="AABox"/> by the specified amount in each direction. This function changes
        /// the center of the <see cref="AABox"/>.
        /// </summary>
        /// <param name="amount">Amount to uniformly scale the X, Y and Z axes by.</param>
        public void ScaleBy(float amount)
        {
            ScaleBy(new Vector3(amount, amount, amount));
        }

        /// <summary>
        /// Scales the <see cref="AABox"/> by the specified amount in each direction. This function changes
        /// the center of the <see cref="AABox"/>.
        /// </summary>
        /// <param name="amount">Amounts to scale the min/max by on their X, Y, and Z axes.</param>
        public void ScaleBy(Vector3 amount)
        {
            Vector3 extents = (Max - Min);
            extents.X *= amount.X;
            extents.Y *= amount.Y;
            extents.Z *= amount.Z;

            Vector3 halfExtents = extents / 2;
            Vector3 oldCenter = Center;
            oldCenter.X *= amount.X;
            oldCenter.Y *= amount.Y;
            oldCenter.Z *= amount.Z;

            m_min = oldCenter + halfExtents;
            m_max = oldCenter - halfExtents;
        }


        public void Encapsulate(Vector3 point)
        {
            if (point.X < m_min.X)
                m_min.X = point.X;
            if (point.X > m_max.X)
                m_max.X = point.X;

            if (point.Y < m_min.Y)
                m_min.Y = point.Y;
            if (point.Y > m_max.Y)
                m_max.Y = point.Y;

            if (point.Z < m_min.Z)
                m_min.Z = point.Z;
            if (point.Z > m_max.Z)
                m_max.Z = point.Z;
        }

        public override string ToString()
        {
            return string.Format("Min: {0} Max: {1} Center: {2}", Min, Max, Center);
        }
    }
}
