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
            YX, YZ, XZ
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
                WRay fakeRay = new WRay(new Vector3(6, 6, 0), new Vector3(1, 0, 0));

                if (CheckSelectedAxes(fakeRay))
                {
                    Console.WriteLine("TranslationGizmo clicked. Selected Axes: {0}", m_selectedAxes);
                    m_isSelected = true;
                }
            }

            if(WInput.GetMouseButtonUp(0))
            {
                m_isSelected = false;
            }

            if(m_isSelected)
            {
                // Raytrace per frame, aaah.
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
                    new AABox(new Vector3(-2, 0, 0), new Vector3(2, boxHalfWidth*6, boxHalfWidth*6)),
                    // XZ Axes
                    new AABox(new Vector3(0, -2, 0), new Vector3(boxHalfWidth*6, 2, boxHalfWidth*6))
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
                    m_lineBatcher.DrawBox(translationAABB[i].Min, translationAABB[i].Max, gizmoColors[i], 25, 15);


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


        public override void ReleaseResources()
        {
        }

        public override void Render(Matrix4 viewMatrix, Matrix4 projMatrix)
        {
        }
    }
}
