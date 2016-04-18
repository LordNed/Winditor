using OpenTK;
using System;

namespace Editor
{
    class WCamera : WActor
    {
        /// <summary> The far clipping plane distance. </summary>
        public float FarClipPlane
        {
            get { return m_farClipPlane; }
            set
            {
                m_farClipPlane = value;
                CalculateProjectionMatrix();
            }
        }

        /// <summary> The nera clipping plane distance. </summary>
        public float NearClipPlane
        {
            get { return m_nearClipPlane; }
            set
            {
                m_nearClipPlane = value;
                CalculateProjectionMatrix();
            }
        }

        /// <summary> Vertical field of view in degrees. </summary>
        public float FieldOfView
        {
            get { return m_fieldOfView; }
            set
            {
                m_fieldOfView = value;
                CalculateProjectionMatrix();
            }
        }

        public float AspectRatio
        {
            get { return m_aspectRatio; }
            set
            {
                m_aspectRatio = value;
            }
        }

        /// <summary> Current Projection Matrix of camera. </summary>
        public Matrix4 ProjectionMatrix { get { return m_projectionMatrix; } }

        /// <summary> View Matrix of the Camera. Calculated every frame as there's no way to see when a Transform has been dirtied. </summary>
        public Matrix4 ViewMatrix
        {
            get
            {
                Matrix4 rhView = Matrix4.LookAt(Transform.Position, Transform.Position - Transform.Forward, Vector3.UnitY);
                return rhView;
            }
        }

        private float m_nearClipPlane = 100f;
        private float m_farClipPlane = 100000f;
        private float m_fieldOfView = 45f;
        private float m_aspectRatio = 16 / 9f;
        private Matrix4 m_projectionMatrix;


        public override void Tick(float deltaTime)
        {
            Console.WriteLine("tck tck tck");
        }

        public WRay ViewportPointToRay(Vector3 mousePos, Vector2 screenSize)
        {
            Vector3 mousePosA = new Vector3(mousePos.X, mousePos.Y, 0f);
            Vector3 mousePosB = new Vector3(mousePos.X, mousePos.Y, 1f);


            Vector4 nearUnproj = UnProject(ProjectionMatrix, ViewMatrix, mousePosA, screenSize);
            Vector4 farUnproj = UnProject(ProjectionMatrix, ViewMatrix, mousePosB, screenSize);

            Vector3 dir = farUnproj.Xyz - nearUnproj.Xyz;
            dir.Normalize();

            return new WRay(nearUnproj.Xyz, dir);
        }


        public Vector4 UnProject(Matrix4 projection, Matrix4 view, Vector3 mousePos, Vector2 screenSize)
        {
            Vector4 vec = new Vector4();

            vec.X = 2.0f * mousePos.X / screenSize.X - 1;
            vec.Y = -(2.0f * mousePos.Y / screenSize.Y - 1);
            vec.Z = mousePos.Z;
            vec.W = 1.0f;

            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref vec, ref projInv, out vec);
            Vector4.Transform(ref vec, ref viewInv, out vec);

            if (vec.W > float.Epsilon || vec.W < float.Epsilon)
            {
                vec.X /= vec.W;
                vec.Y /= vec.W;
                vec.Z /= vec.W;
            }

            return vec;
        }

        private void CalculateProjectionMatrix()
        {
            m_projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FieldOfView), AspectRatio, m_nearClipPlane, FarClipPlane);
        }
    }
}
