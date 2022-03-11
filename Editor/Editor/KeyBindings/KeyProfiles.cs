using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindEditor.Editor.Managers;
using WindEditor.ViewModel;

namespace WindEditor.Editor.KeyBindings
{
    public class KeyCameraProfile
    {
        //Bindings
        private Vector3 m_movementAxis = new Vector3(0, 0, 0);
        private InputProfileManager inputProfileManager;
        private KeyInputProfile activeProfile;
        private Key accelerationKey;
        private float speed = 0.0f;

        public InputProfileManager InputProfileManager
        {
            set { inputProfileManager = value; }
        }

        public KeyCameraProfile()
        {
            if (inputProfileManager == null)
            {
                activeProfile = KeyInputProfile.NormalInputProfile;
                accelerationKey = Key.LeftShift;
            }
            else
            {
                SetInputSettings();
            }
        }

        public void SetInputSettings()
        {
            if (inputProfileManager != null)
            {
                activeProfile = inputProfileManager.InputProfileContainer.KeyInputProfileActive;
                accelerationKey = inputProfileManager.InputProfileContainer.AccelerationKey;
            }
        }

        public Vector3 Movement()
        {
            if (activeProfile == KeyInputProfile.NormalInputProfile)
            {
                if (WInput.GetMouseButton(1) && WInput.GetKey(Key.W) || WInput.GetMouseButton(1) && WInput.GetKey(Key.Up))
                {
                    m_movementAxis = new Vector3(0, 0, -1);
                }
                else
                {
                    if (m_movementAxis.Z < 0)
                        m_movementAxis.Z = 0;
                }
                if (WInput.GetMouseButton(1) && WInput.GetKey(Key.S) || WInput.GetMouseButton(1) && WInput.GetKey(Key.Down))
                {
                    m_movementAxis = new Vector3(0, 0, 1);
                }
                else
                {
                    if (m_movementAxis.Z > 0)
                        m_movementAxis.Z = 0;
                }
                if (WInput.GetMouseButton(1) && WInput.GetKey(Key.D) || WInput.GetMouseButton(1) && WInput.GetKey(Key.Right))
                {
                    m_movementAxis = new Vector3(1, 0, 0);
                }
                else
                {
                    if (m_movementAxis.X > 0)
                        m_movementAxis.X = 0;
                }
                if (WInput.GetMouseButton(1) && WInput.GetKey(Key.A) || WInput.GetMouseButton(1) && WInput.GetKey(Key.Left))
                {
                    m_movementAxis = new Vector3(-1, 0, 0);
                }
                else
                {
                    if (m_movementAxis.X < 0)
                        m_movementAxis.X = 0;
                }
            }
            else if (activeProfile == KeyInputProfile.SimpleInputProfile)
            {
                if (WInput.GetKey(Key.W) || WInput.GetKey(Key.Up))
                {
                    m_movementAxis = new Vector3(0, 0, -1);
                }
                else
                {
                    if (m_movementAxis.Z < 0)
                        m_movementAxis.Z = 0;
                }
                if (WInput.GetKey(Key.S) || WInput.GetKey(Key.Down))
                {
                    m_movementAxis = new Vector3(0, 0, 1);
                }
                else
                {
                    if (m_movementAxis.Z > 0)
                        m_movementAxis.Z = 0;
                }
                if (WInput.GetKey(Key.D) || WInput.GetKey(Key.Right))
                {
                    m_movementAxis = new Vector3(1, 0, 0);
                }
                else
                {
                    if (m_movementAxis.X > 0)
                        m_movementAxis.X = 0;
                }
                if (WInput.GetKey(Key.A) || WInput.GetKey(Key.Left))
                {
                    m_movementAxis = new Vector3(-1, 0, 0);
                }
                else
                {
                    if (m_movementAxis.X < 0)
                        m_movementAxis.X = 0;
                }
            }

            return m_movementAxis;
        }

        public float MovementSpeed(float StartSpeed)
        {
            speed += WInput.MouseScrollDelta * 10;
            speed = WMath.Clamp(speed, 1000, 25000);

            if (activeProfile == KeyInputProfile.NormalInputProfile)
            {
                if (WInput.GetMouseButton(1) && WInput.GetKey(Key.W) ||
                    WInput.GetMouseButton(1) && WInput.GetKey(Key.Up) ||
                    WInput.GetMouseButton(1) && WInput.GetKey(Key.S) ||
                    WInput.GetMouseButton(1) && WInput.GetKey(Key.Down) ||
                    WInput.GetMouseButton(1) && WInput.GetKey(Key.D) ||
                    WInput.GetMouseButton(1) && WInput.GetKey(Key.Right) ||
                    WInput.GetMouseButton(1) && WInput.GetKey(Key.A) ||
                    WInput.GetMouseButton(1) && WInput.GetKey(Key.Left)
                    )
                {
                    if (WInput.GetKey(accelerationKey)) 
                    {
                        return speed * 2;
                    }
                    else 
                    {
                        if (speed == 0){ return StartSpeed; }

                        return speed;
                    }
                }
            }
            else if (activeProfile == KeyInputProfile.SimpleInputProfile)
            {
                if (WInput.GetKey(Key.W) ||
                    WInput.GetKey(Key.Up) ||
                    WInput.GetKey(Key.S) ||
                    WInput.GetKey(Key.Down) ||
                    WInput.GetKey(Key.D) ||
                    WInput.GetKey(Key.Right) ||
                    WInput.GetKey(Key.A) ||
                    WInput.GetKey(Key.Left)
                    )
                {
                    if (WInput.GetKey(accelerationKey))
                    {
                        return speed * 2;
                    }
                    else
                    {
                        if (speed == 0) { return StartSpeed; }

                        return speed;
                    }
                }
            }

            return speed;
        }
    }
}