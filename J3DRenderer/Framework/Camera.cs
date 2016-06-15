using OpenTK;
using System;

namespace WindEditor
{
    public enum CameraMode
    {
        Flycam,
        Orbit
    }

    class WCamera
    {
        public WTransform Transform = new WTransform();
        public float MoveSpeed = 1500f;
        public float MouseSensitivity = 20f;
        
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
                CalculateProjectionMatrix();
            }
        }

        public CameraMode MoveType = CameraMode.Flycam;

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

        private float m_nearClipPlane = 10f;
        private float m_farClipPlane = 100000f;
        private float m_fieldOfView = 45f;
        private float m_aspectRatio = 16 / 9f;
        private float m_orbitCameraDistance = 500f;
        private Vector3 m_orbitPivot;

        private Matrix4 m_projectionMatrix;


        public void Tick(float deltaTime)
        {
            if (WInput.GetKeyDown(System.Windows.Input.Key.OemTilde))
                MoveType = MoveType == CameraMode.Flycam ? CameraMode.Orbit : CameraMode.Flycam;

            if (!WInput.GetMouseButton(1))
                return;

            if (MoveType == CameraMode.Flycam)
                DoFlycamUpdate(deltaTime);
            else
                DoOrbitcamUpdate(deltaTime);
        }

        private void DoOrbitcamUpdate(float deltaTime)
        {
            float orbitSensitivityScale = 0.02f; // Angle Axis expects rads, but we have... pixels, so we just move it way down by a factor.
            Quaternion deltaRot = Quaternion.FromAxisAngle(new Vector3(1, 0, 0), -WInput.MouseDelta.Y * MouseSensitivity * deltaTime * orbitSensitivityScale) * Quaternion.FromAxisAngle(new Vector3(0, 1, 0), -WInput.MouseDelta.X * MouseSensitivity * deltaTime  * orbitSensitivityScale);
            Transform.Rotation *= deltaRot;

            Vector3 moveDir = Vector3.Zero;
            if (WInput.GetKey(System.Windows.Input.Key.Q))
            {
                moveDir -= Vector3.UnitY;
            }
            if (WInput.GetKey(System.Windows.Input.Key.E))
            {
                moveDir += Vector3.UnitY;
            }

            if(moveDir.Length > 0)
            {
                moveDir.Normalize();
                m_orbitPivot += moveDir * (MoveSpeed/2)* deltaTime;
            }

            m_orbitCameraDistance += -WInput.MouseScrollDelta * 50 * deltaTime;
            m_orbitCameraDistance = WMath.Clamp(m_orbitCameraDistance, 100, 8000);

            Transform.Position = m_orbitPivot + Vector3.Transform(new Vector3(0, 0, m_orbitCameraDistance), Transform.Rotation);
        }

        private void DoFlycamUpdate(float deltaTime)
        {
            Vector3 moveDir = Vector3.Zero;
            if (WInput.GetKey(System.Windows.Input.Key.W))
            {
                moveDir -= Vector3.UnitZ;
            }
            if (WInput.GetKey(System.Windows.Input.Key.S))
            {
                moveDir += Vector3.UnitZ;
            }
            if (WInput.GetKey(System.Windows.Input.Key.D))
            {
                moveDir += Vector3.UnitX;
            }
            if (WInput.GetKey(System.Windows.Input.Key.A))
            {
                moveDir -= Vector3.UnitX;
            }

            // If they're holding down the shift key adjust their FOV when they scroll, otherwise adjust move speed.
            MoveSpeed += WInput.MouseScrollDelta * 100 * deltaTime;
            MoveSpeed = WMath.Clamp(MoveSpeed, 100, 8000);

            if (WInput.GetMouseButton(1))
            {
                Rotate(deltaTime, WInput.MouseDelta.X, WInput.MouseDelta.Y);
            }

            float moveSpeed = WInput.GetKey(System.Windows.Input.Key.LeftShift) ? MoveSpeed * 3f : MoveSpeed;

            // Make it relative to the current rotation.
            moveDir = Vector3.Transform(moveDir, Transform.Rotation);

            // Do Q and E after we transform the moveDir so they're always in worldspace.
            if (WInput.GetKey(System.Windows.Input.Key.Q))
            {
                moveDir -= Vector3.UnitY;
            }
            if (WInput.GetKey(System.Windows.Input.Key.E))
            {
                moveDir += Vector3.UnitY;
            }

            // Normalize the move direction
            moveDir.NormalizeFast();

            // Early out if we're not moving this frame.
            if (moveDir.LengthFast < 0.1f)
                return;

            Transform.Position += Vector3.Multiply(moveDir, moveSpeed * deltaTime);
        }

        private void Rotate(float deltaTime, float x, float y)
        {
            Transform.Rotate(Vector3.UnitY, -x * deltaTime * MouseSensitivity);
            Transform.Rotate(Transform.Right, -y * deltaTime * MouseSensitivity);

            // Clamp them from looking over the top point.
            Vector3 up = Vector3.Cross(Transform.Forward, Transform.Right);
            if (Vector3.Dot(up, Vector3.UnitY) < 0.01f)
            {
                Transform.Rotate(Transform.Right, y * deltaTime * MouseSensitivity);
            }
        }

        private void CalculateProjectionMatrix()
        {
            m_projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FieldOfView), AspectRatio, m_nearClipPlane, FarClipPlane);
        }
    }
}
