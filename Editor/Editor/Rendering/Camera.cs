using OpenTK;
using System;
using WindEditor.Editor.KeyBindings;
using WindEditor.ViewModel;

namespace WindEditor
{
    public class WCamera
    {
        public float MouseSensitivity = 20f;
        public WTransform Transform;
        private readonly float startMoveSpeed = 2000f;
        private int speedMultiplier;

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

        public bool bEnableUpdates { get; set; }

        private float m_nearClipPlane = 10f;
        private float m_farClipPlane = 100000000f;
        private float m_fieldOfView = 60f;
        private float m_aspectRatio = 16 / 9f;
        private Matrix4 m_projectionMatrix;

        //Key Library
        private KeyProfilesLibrary m_keyProfilesLib;
        private KeyCameraProfile m_camProfile;

        public WCamera(KeyProfilesLibrary lib)
        {
            m_keyProfilesLib = lib;
            m_camProfile = m_keyProfilesLib.CameraProfiles;

            Transform = new WTransform();
            bEnableUpdates = true;
        }

        public void Tick(float deltaTime)
        {
            if (!bEnableUpdates)
                return;

            // Set correct movement profile
            m_camProfile.SetInputSettings();

            // Get movement axis
            Vector3 movementAxis = m_camProfile.Movement();

            // Vars to calculate
            Vector3 moveDir = Vector3.Zero;
            float moveSpeed = 0.0f;

            moveDir += movementAxis;

            if (WInput.GetMouseButton(1))
            {
                Rotate(deltaTime, WInput.MouseDelta.X, WInput.MouseDelta.Y);
            }

            moveSpeed = m_camProfile.MovementSpeed(startMoveSpeed);

            // Make it relative to the current rotation.
            moveDir = Vector3.Transform(moveDir, Transform.Rotation.ToSinglePrecision());

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
