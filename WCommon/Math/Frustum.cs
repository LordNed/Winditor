using OpenTK;
using System;

// Plane Extraction from Matrix: http://gamedevs.org/uploads/fast-extraction-viewing-frustum-planes-from-world-view-projection-matrix.pdf
// AABB vs Frustrum & Sphere vs Frustrum: http://www.flipcode.com/archives/Frustum_Culling.shtml
namespace WindEditor
{
    public class WFrustum
    {
        public WPlane[] m_planes;

        public WFrustum(WPlane[] cameraPlanes)
        {
            if (cameraPlanes.Length != 6)
                throw new ArgumentException("A frustum must be built from 6 planes!", "cameraPlanes");

            m_planes = cameraPlanes;
        }

        public WFrustum(Vector3[] frustumPoints)
        {
            if (frustumPoints.Length != 8)
                throw new ArgumentException("A frustum must be built from the 8 corners of the frustum!", "frustumPoints");

            // Construct planes out of the given points.
            m_planes = new WPlane[6];
            m_planes[0] = new WPlane(frustumPoints[0], frustumPoints[2], frustumPoints[4]); // Left
            m_planes[1] = new WPlane(frustumPoints[5], frustumPoints[7], frustumPoints[1]); // Right
            m_planes[2] = new WPlane(frustumPoints[4], frustumPoints[1], frustumPoints[0]); // Top
            m_planes[3] = new WPlane(frustumPoints[3], frustumPoints[7], frustumPoints[2]); // Down
            m_planes[4] = new WPlane(frustumPoints[1], frustumPoints[3], frustumPoints[0]); // Near
            m_planes[5] = new WPlane(frustumPoints[7], frustumPoints[5], frustumPoints[4]); // Far
        }

        /// <summary>
        /// Determines if the specified sphere lies fully inside, outside, or intersects with 
        /// this frustum.
        /// </summary>
        /// <param name="center">Center of the sphere to test against.</param>
        /// <param name="radius">Radius of the sphere to test against.</param>
        /// <returns><see cref="Halfspace"/> indicating inside, outside, or intersecting.</returns>
        public Halfspace ContainsSphere(Vector3 center, float radius)
        {
            // The way this one works is that we check the sphere against each plane of the frustum.
            // If the sphere lies entirely outside of one plane we know it can't intersect with any other
            // plane, thus it can early out.

            // ToDo: If we intersect with a given plane, how do we early out? 
            // Isn't it still potentially entirely outside of another plane?

            float distFromPlane;

            for (int i = 0; i < 6; i++)
            {
                // Distance from the sphere to this plane.
                distFromPlane = Vector3.Dot(m_planes[i].Normal, center) + m_planes[i].Distance;

                // If the distance is < -radius, then we're outside this plane.
                if (distFromPlane < -radius)
                    return Halfspace.Negative;

                // However, if the distance is between +/- radius, then we're intersecting.
                if (Math.Abs(distFromPlane) < radius)
                    return Halfspace.Intersect;
            }

            // If we hit this point then we're fully in view.
            return Halfspace.Positive;
        }

        public Halfspace ContainsAAB(AABox box)
        {
            Vector3[] corners = box.GetVertices();
            int totalPointsIn = 0;

            // Test all 8 corners of the AABox against the 6 planes of the frustum. If all
            // points are behind 1 specific plane, then we're fully out. If all points are in
            // then the AABox is fully within the frustum.
            for (int p = 0; p < 6; p++)
            {
                int inCount = 8;
                int ptIn = 1;

                for (int j = 0; j < 8; j++)
                {
                    // Test the point against the planes
                    if (m_planes[p].SideOfPlane(corners[j]) == Halfspace.Negative)
                    {
                        ptIn = 0;
                        inCount--;
                    }
                }

                // If all points of the AABox were outside of this particular plane p,
                // we know the AABox lies entirely outside and can early out.
                if (inCount == 0)
                    return Halfspace.Negative;

                // Check if they're all in the correct side of the plane
                totalPointsIn += ptIn;
            }

            // If the totalPointsIn is 6, then they're inside the view.
            if (totalPointsIn == 6)
                return Halfspace.Positive;

            // Otherwise, they're partially in.
            return Halfspace.Intersect;
        }
    }
}
