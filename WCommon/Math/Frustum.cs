using OpenTK;
using System;

// Plane Extraction from Matrix: http://gamedevs.org/uploads/fast-extraction-viewing-frustum-planes-from-world-view-projection-matrix.pdf
// AABB vs Frustrum & Sphere vs Frustrum: http://www.flipcode.com/archives/Frustum_Culling.shtml
namespace WindEditor
{
    public class FFrustum
    {
        public FPlane[] m_planes;

        public FFrustum(ref Matrix4 viewMatrix,ref Matrix4 projMatrix)
        {
            Vector3[] frustumPoints = new Vector3[8];
            frustumPoints[0] = UnProject(projMatrix, viewMatrix, new Vector3(0, 0, 0)).Xyz; // Upper Left (Near)
            frustumPoints[1] = UnProject(projMatrix, viewMatrix, new Vector3(1, 0, 0)).Xyz; // Upper Right (Near)
            frustumPoints[2] = UnProject(projMatrix, viewMatrix, new Vector3(0, 1, 0)).Xyz; // Bottom Left (Near)
            frustumPoints[3] = UnProject(projMatrix, viewMatrix, new Vector3(1, 1, 0)).Xyz; // Bottom Right (Near)

            frustumPoints[4] = UnProject(projMatrix, viewMatrix, new Vector3(0, 0, 1)).Xyz; // Upper Left (Far)
            frustumPoints[5] = UnProject(projMatrix, viewMatrix, new Vector3(1, 0, 1)).Xyz; // Upper Right (Far)
            frustumPoints[6] = UnProject(projMatrix, viewMatrix, new Vector3(0, 1, 1)).Xyz; // Bottom Left (Far)
            frustumPoints[7] = UnProject(projMatrix, viewMatrix, new Vector3(1, 1, 1)).Xyz; // Bottom Right (Far)

            PlanesFromPoints(frustumPoints);
        }

        public FFrustum(FPlane[] cameraPlanes)
        {
            if (cameraPlanes.Length != 6)
                throw new ArgumentException("A frustum must be built from 6 planes!", "cameraPlanes");

            m_planes = cameraPlanes;
        }

        public FFrustum(Vector3[] frustumPoints)
        {
            PlanesFromPoints(frustumPoints);
        }

        private void PlanesFromPoints(Vector3[] points)
        {
            if (points.Length != 8)
                throw new ArgumentException("A frustum must be built from the 8 corners of the frustum!", "points");

            // Construct planes out of the given points.
            m_planes = new FPlane[6];
            m_planes[0] = new FPlane(points[0], points[2], points[4]); // Left
            m_planes[1] = new FPlane(points[5], points[7], points[1]); // Right
            m_planes[2] = new FPlane(points[0], points[4], points[1]); // Top
            m_planes[3] = new FPlane(points[3], points[7], points[2]); // Down
            m_planes[4] = new FPlane(points[1], points[3], points[0]); // Near
            m_planes[5] = new FPlane(points[7], points[5], points[4]); // Far

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
                Vector3.Dot(ref m_planes[i].Normal, ref center, out distFromPlane);
                distFromPlane += m_planes[i].Distance;

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

        public Halfspace ContainsAAB(FAABox box)
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

        public static Vector4 UnProject(Matrix4 projection, Matrix4 view, Vector3 viewportPoint)
        {
            Vector4 clip = new Vector4();

            // Convert from Viewport Space ([0,1]) to Clip Space ([-1, 1])
            clip.X = (2.0f * viewportPoint.X) - 1;
            clip.Y = -((2.0f * viewportPoint.Y) - 1);
            clip.Z = viewportPoint.Z;
            clip.W = 1.0f;

            if (float.IsNaN(view.Determinant))
            {
                // This happens when the user points the camera directly down/up. For some reason this makes the program crash.
                // This is a hack to just stop it from crashing, but it won't actually render correctly either.
                // This hack causes it to just be a blank screen as long as the camera is pointed down/up.
                view = Matrix4.Identity;
            }
            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref clip, ref projInv, out clip);
            Vector4.Transform(ref clip, ref viewInv, out clip);

            if (clip.W > float.Epsilon || clip.W < float.Epsilon)
            {
                clip.X /= clip.W;
                clip.Y /= clip.W;
                clip.Z /= clip.W;
            }

            return clip;
        }
    }
}
