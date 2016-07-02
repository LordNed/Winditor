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

        public WFrustum(Matrix4 fromMatrix)
        {
            m_planes = ExtractPlanes(ref fromMatrix, false);
        }

        /// <summary>
        /// Extract an array of 6 <see cref="WPlane"/>s representing the supplied camera matrix. 
        /// The planes are always in Left/Right/Top/Bottom/Near/Far order, and can optionally be
        /// normalized.
        /// 
        /// Note: The resulting planes will have normals that point to the inside of the viewing
        /// frustum. If it is desired that they point to the outside then the code must be modified
        /// to negate the coefficients X, Y, Z, and d of each plane equation.
        /// </summary>
        /// <param name="mvpMatrix">Camera matrix to extract planes from.</param>
        /// <param name="normalize">Optionally normalize the resulting planes.</param>
        /// <returns>Array of planes representing the supplied matrix.</returns>
        public static WPlane[] ExtractPlanes(ref Matrix4 mvpMatrix, bool normalize = true)
        {
            WPlane[] planes = new WPlane[6];

            // Left Clipping Plane
            planes[0].Normal.X = mvpMatrix.M41 + mvpMatrix.M11;
            planes[0].Normal.Y = mvpMatrix.M42 + mvpMatrix.M12;
            planes[0].Normal.Z = mvpMatrix.M43 + mvpMatrix.M13;
            planes[0].Distance = mvpMatrix.M44 + mvpMatrix.M14;

            // Right Clipping Plane
            planes[1].Normal.X = mvpMatrix.M41 - mvpMatrix.M11;
            planes[1].Normal.Y = mvpMatrix.M42 - mvpMatrix.M12;
            planes[1].Normal.Z = mvpMatrix.M43 - mvpMatrix.M13;
            planes[1].Distance = mvpMatrix.M44 - mvpMatrix.M14;

            // Top Clipping Plane
            planes[2].Normal.X = mvpMatrix.M41 - mvpMatrix.M21;
            planes[2].Normal.Y = mvpMatrix.M42 - mvpMatrix.M22;
            planes[2].Normal.Z = mvpMatrix.M43 - mvpMatrix.M23;
            planes[2].Distance = mvpMatrix.M44 - mvpMatrix.M24;

            // Bottom Clipping Plane
            planes[3].Normal.X = mvpMatrix.M41 + mvpMatrix.M21;
            planes[3].Normal.Y = mvpMatrix.M42 + mvpMatrix.M22;
            planes[3].Normal.Z = mvpMatrix.M43 + mvpMatrix.M23;
            planes[3].Distance = mvpMatrix.M44 + mvpMatrix.M24;

            // Near Clipping Plane
            planes[4].Normal.X = mvpMatrix.M41 + mvpMatrix.M31;
            planes[4].Normal.Y = mvpMatrix.M42 + mvpMatrix.M32;
            planes[4].Normal.Z = mvpMatrix.M43 + mvpMatrix.M33;
            planes[4].Distance = mvpMatrix.M44 + mvpMatrix.M34;

            // Far Clipping Plane
            planes[5].Normal.X = mvpMatrix.M41 - mvpMatrix.M31;
            planes[5].Normal.Y = mvpMatrix.M42 - mvpMatrix.M32;
            planes[5].Normal.Z = mvpMatrix.M43 - mvpMatrix.M33;
            planes[5].Distance = mvpMatrix.M44 - mvpMatrix.M34;

            if(normalize)
            {
                for (int i = 0; i < 6; i++)
                    planes[i].Normalize();
            }

            return planes;
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

            for(int i = 0; i < 6; i++)
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
            for(int p = 0; p < 6; p++)
            {
                int inCount = 8;
                int ptIn = 1;

                for(int j = 0; j < 8; j++)
                {
                    // Test the point against the planes
                    if(m_planes[p].SideOfPlane(corners[j]) == Halfspace.Negative)
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
