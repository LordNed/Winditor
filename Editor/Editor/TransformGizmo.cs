using System;
using OpenTK;
using System.Collections.Generic;

namespace Editor
{
    class WTransformGizmo : PrimitiveComponent
    {
        enum TransformMode
        {
            None,
            Translation,
            Rotation,
            Scale
        }

        enum SelectedAxes
        {
            None,
            X, Y, Z,
            XY, XZ, YZ
        }

        struct AxisDistanceResult
        {
            public SelectedAxes Axis;
            public float Distance;

            public AxisDistanceResult(SelectedAxes axis, float intersectDist)
            {
                Axis = axis;
                Distance = intersectDist;
            }
        }

        private WTransform m_transform;
        private TransformMode m_mode;
        private SelectedAxes m_selectedAxes;

        private bool m_isSelected;
        private WPlane m_translationPlane;
        private bool m_hasTransformed;
        private bool m_hasSetMouseOffset;

        // Delta Transforms
        private Vector3 m_deltaTranslation;
        private Vector3 m_totalTranslation;
        private Vector3 m_translateOffset;

        // hack...
        private WLineBatcher m_lineBatcher;

        public WTransformGizmo(WLineBatcher lines)
        {
            // hack...
            m_lineBatcher = lines;


            m_transform = new WTransform();
            m_mode = TransformMode.Translation;
            m_selectedAxes = SelectedAxes.None;
        }


        public override void Tick(float deltaTime)
        {
            if (WInput.GetMouseButtonDown(0))
            {
                WRay mouseRay = WSceneView.DeprojectScreenToWorld(WInput.MousePosition);
                m_lineBatcher.DrawLine(mouseRay.Origin, mouseRay.Origin + (mouseRay.Direction * 1000), WLinearColor.Black, 25, 25);
                if (CheckSelectedAxes(mouseRay))
                {
                    Console.WriteLine("TranslationGizmo clicked. Selected Axes: {0}", m_selectedAxes);
                    m_isSelected = true;
                }
            }

            if(WInput.GetMouseButtonUp(0))
            {
                m_isSelected = false;
                m_hasSetMouseOffset = false;
            }

            if(m_isSelected)
            {
                // Raytrace per frame, aaah.
                WRay mouseRay = WSceneView.DeprojectScreenToWorld(WInput.MousePosition);
                Vector3 cameraPos = WSceneView.GetCameraPos();
                TransformFromInput(mouseRay, cameraPos);
            }


            float boxLength = 75f;
            float boxHalfWidth = 5;

            AABox[] translationAABB = new[]
            {
                    // X Axis
                    new AABox(new Vector3(0, -boxHalfWidth, -boxHalfWidth), new Vector3(boxLength, boxHalfWidth, boxHalfWidth)),
                    // Y Axis
                    new AABox(new Vector3(-boxHalfWidth, 0, -boxHalfWidth), new Vector3(boxHalfWidth, boxLength, boxHalfWidth)),
                    // Z Axis
                    new AABox(new Vector3(-boxHalfWidth, -boxHalfWidth, 0), new Vector3(boxHalfWidth, boxHalfWidth, boxLength)),
                    // XY Axes
                    new AABox(new Vector3(0, 0, -2), new Vector3(boxHalfWidth*6, boxHalfWidth*6, 2)),
                    // XZ Axes
                    new AABox(new Vector3(0, -2, 0), new Vector3(boxHalfWidth*6, 2, boxHalfWidth*6)),
                    // YZ Axes
                    new AABox(new Vector3(-2, 0, 0), new Vector3(2, boxHalfWidth*6, boxHalfWidth*6)),
                };

            WLinearColor[] gizmoColors = new[]
            {
                    WLinearColor.Red,
                    WLinearColor.Green,
                    WLinearColor.Blue,
                    WLinearColor.Blue,
                    WLinearColor.Red,
                    WLinearColor.Green
                };

            List<AxisDistanceResult> results = new List<AxisDistanceResult>();

            for (int i = 0; i < translationAABB.Length; i++)
            {
                m_lineBatcher.DrawBox(translationAABB[i].Min + m_transform.Position, translationAABB[i].Max + m_transform.Position, gizmoColors[i], 25, 0f);
            }
        }

        private bool CheckSelectedAxes(WRay ray)
        {
            WRay localRay = new WRay();
            localRay.Direction = Vector3.Transform(ray.Direction, m_transform.Rotation.Inverted());
            localRay.Origin = ray.Origin - m_transform.Position;

            if (m_mode == TransformMode.Translation)
            {
                float boxLength = 75f;
                float boxHalfWidth = 5;

                AABox[] translationAABB = new[]
                {
                    // X Axis
                    new AABox(new Vector3(0, -boxHalfWidth, -boxHalfWidth), new Vector3(boxLength, boxHalfWidth, boxHalfWidth)),
                    // Y Axis
                    new AABox(new Vector3(-boxHalfWidth, 0, -boxHalfWidth), new Vector3(boxHalfWidth, boxLength, boxHalfWidth)),
                    // Z Axis
                    new AABox(new Vector3(-boxHalfWidth, -boxHalfWidth, 0), new Vector3(boxHalfWidth, boxHalfWidth, boxLength)),
                    // YX Axes
                    new AABox(new Vector3(0, 0, -2), new Vector3(boxHalfWidth*6, boxHalfWidth*6, 2)),
                    // YZ Axes
                    new AABox(new Vector3(0, -2, 0), new Vector3(boxHalfWidth*6, 2, boxHalfWidth*6)),
                    // XZ Axes
                    new AABox(new Vector3(-2, 0, 0), new Vector3(2, boxHalfWidth*6, boxHalfWidth*6)),
                };

                List<AxisDistanceResult> results = new List<AxisDistanceResult>();

                for (int i = 0; i < translationAABB.Length; i++)
                {
                    float intersectDist;
                    if (WMath.RayIntersectsAABB(localRay, translationAABB[i].Min, translationAABB[i].Max, out intersectDist))
                    {
                        results.Add(new AxisDistanceResult((SelectedAxes)(i + 1), intersectDist));
                    }
                }

                if (results.Count == 0)
                {
                    m_selectedAxes = SelectedAxes.None;
                    return false;
                }

                // If we get an intersection, sort them by the closest intersection distance.
                results.Sort((x, y) => x.Distance.CompareTo(y.Distance));

                m_selectedAxes = results[0].Axis;
                return true;
            }

            return false;
        }

        private bool TransformFromInput(WRay ray, Vector3 cameraPos)
        {
            if (m_mode == TransformMode.Translation)
            {
                // Create a Translation Plane
                Vector3 axisA, axisB;
                int numAxis = (m_selectedAxes == SelectedAxes.X || m_selectedAxes == SelectedAxes.Y || m_selectedAxes == SelectedAxes.Z) ? 1 : 2;

                if (numAxis == 1)
                {
                    if (m_selectedAxes == SelectedAxes.X)
                        axisB = Vector3.UnitX;
                    else if (m_selectedAxes == SelectedAxes.Y)
                        axisB = Vector3.UnitY;
                    else
                        axisB = Vector3.UnitZ;

                    Vector3 dirToCamera = (m_transform.Position - cameraPos).Normalized();
                    axisA = Vector3.Cross(axisB, dirToCamera);
                }
                else //if(numAxis == 2)
                {
                    axisA = ContainsAxis(m_selectedAxes, SelectedAxes.X) ? Vector3.UnitX : Vector3.UnitZ;
                    axisB = ContainsAxis(m_selectedAxes, SelectedAxes.Y) ? Vector3.UnitY : Vector3.UnitZ;
                }

                Vector3 planeNormal = Vector3.Cross(axisA, axisB).Normalized();
                m_translationPlane = new WPlane(planeNormal, m_transform.Position);
                float intersectDist;
                bool bIntersectsPlane = m_translationPlane.RayIntersectsPlane(ray, out intersectDist);
                m_lineBatcher.DrawLine(m_transform.Position, m_transform.Position + (planeNormal * 500), WLinearColor.Yellow, 5, 0);

                //Console.WriteLine("planeNormal: {0} axisA: {1}, axisB: {2} numAxis: {3} selectedAxes: {4} intersectsPlane: {5} @dist: {6}", planeNormal, axisA, axisB, numAxis, m_selectedAxes, bIntersectsPlane, intersectDist);
                if (bIntersectsPlane)
                {
                    Vector3 hitPos = ray.Origin + (ray.Direction * intersectDist);
                    Vector3 localOffset = Vector3.Transform(hitPos - m_transform.Position, m_transform.Rotation.Inverted());

                    // Calculate a new position
                    Vector3 newPos = m_transform.Position;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.X))
                        newPos += Vector3.UnitX * localOffset.X;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.Y))
                        newPos += Vector3.UnitY * localOffset.Y;
                    if (ContainsAxis(m_selectedAxes, SelectedAxes.Z))
                        newPos += Vector3.UnitZ * localOffset.Z;


                    // Check the new location to see if it's skyrocked off into the distance due to near-plane raytracing issues.
                    Vector3 newPosDirToCamera = (newPos - cameraPos).Normalized();
                    float dot = Math.Abs(Vector3.Dot(planeNormal, newPosDirToCamera));

                    //Console.WriteLine("hitPos: {0} localOffset: {1} newPos: {2}, dotResult: {3}", hitPos, localOffset, newPos, dot);
                    if (dot < 0.02f)
                        return false;

                    // This is used to set the offset to the gizmo the mouse cursor is from the origin of the gizmo on the first frame
                    // that you click on the gizmo.
                    if(!m_hasSetMouseOffset)
                    {
                        m_translateOffset = m_transform.Position - newPos;
                        m_deltaTranslation = Vector3.Zero;
                        m_hasSetMouseOffset = true;
                        return false;
                    }
                    else
                    {
                        // Apply Translation
                        m_deltaTranslation = Vector3.Transform(newPos - m_transform.Position + m_translateOffset, m_transform.Rotation.Inverted());
                        //Console.WriteLine("deltaTranslation: {0}", m_deltaTranslation);
                        if (!ContainsAxis(m_selectedAxes, SelectedAxes.X)) m_deltaTranslation.X = 0f;
                        if (!ContainsAxis(m_selectedAxes, SelectedAxes.Y)) m_deltaTranslation.Y = 0f;
                        if (!ContainsAxis(m_selectedAxes, SelectedAxes.Z)) m_deltaTranslation.Z = 0f;

                        m_totalTranslation += m_deltaTranslation;
                        m_transform.Position += Vector3.Transform(m_deltaTranslation, m_transform.Rotation);

                        if (!m_hasTransformed && (m_deltaTranslation != Vector3.Zero))
                            m_hasTransformed = true;

                        return m_hasTransformed;
                    }
                }
                else
                {
                    // Our raycast missed the plane
                    m_deltaTranslation = Vector3.Zero;
                    return false;
                }
            }
            else if (m_mode == TransformMode.Rotation)
            {

            }
            else if (m_mode == TransformMode.Scale)
            {

            }

            return false;
        }


        public override void ReleaseResources()
        {
        }

        public override void Render(Matrix4 viewMatrix, Matrix4 projMatrix)
        {
        }

        private bool ContainsAxis(SelectedAxes valToCheck, SelectedAxes majorAxis)
        {
            if (!(majorAxis == SelectedAxes.X || majorAxis == SelectedAxes.Y || majorAxis == SelectedAxes.Z))
                throw new ArgumentException("Only use X, Y, or Z here.", "majorAxis");

            switch (majorAxis)
            {
                case SelectedAxes.X:
                    return valToCheck == SelectedAxes.X || valToCheck == SelectedAxes.XY || valToCheck == SelectedAxes.XZ;
                case SelectedAxes.Y:
                    return valToCheck == SelectedAxes.Y || valToCheck == SelectedAxes.XY || valToCheck == SelectedAxes.YZ;
                case SelectedAxes.Z:
                    return valToCheck == SelectedAxes.Z || valToCheck == SelectedAxes.XZ || valToCheck == SelectedAxes.YZ;
            }

            return false;
        }
    }
}
