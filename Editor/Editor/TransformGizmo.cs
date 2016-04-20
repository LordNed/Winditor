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

        public WTransformGizmo()
        {
            m_transform = new WTransform();
            m_mode = TransformMode.Translation;
            m_selectedAxes = SelectedAxes.None;
        }


        public override void Tick(float deltaTime)
        {
            if(WInput.GetMouseButtonDown(0))
            {
                WRay fakeRay = new WRay(new Vector3(25, 0, 0), new Vector3(1, 0, 0));

                if(CheckSelectedAxes(fakeRay))
                {

                }
            }
        }

        private bool CheckSelectedAxes(WRay ray)
        {
            WRay localRay = new WRay();
            localRay.Direction = Vector3.Transform(ray.Direction, m_transform.Rotation.Inverted());
            localRay.Origin = ray.Origin - m_transform.Position;

            if (m_mode == TransformMode.Translation)
            {
                float boxLength = 50f;
                float boxHalfWidth = 5;

                AABox[] translationAABB = new[]
                {
                    // X Axis
                    new AABox(new Vector3(0, -boxHalfWidth, -boxHalfWidth), new Vector3(boxLength, boxHalfWidth, boxHalfWidth)),
                    // Y Axis
                    new AABox(new Vector3(-boxHalfWidth, 0, -boxHalfWidth), new Vector3(boxHalfWidth, boxLength, boxHalfWidth)),
                    // Z Axis
                    new AABox(new Vector3(-boxHalfWidth, -boxHalfWidth, -boxLength), new Vector3(boxHalfWidth, boxHalfWidth, 0))
                };

                List<AxisDistanceResult> results = new List<AxisDistanceResult>();

                for (int i = 0; i < 3; i++)
                {
                    float intersectDist;
                    if(WMath.RayIntersectsAABB(localRay, translationAABB[i].Min, translationAABB[i].Max, out intersectDist))
                    {
                        results.Add(new AxisDistanceResult((SelectedAxes)(i + 1), intersectDist));
                    }
                }

                if(results.Count == 0)
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
