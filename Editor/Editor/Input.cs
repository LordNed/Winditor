using OpenTK;
using System.Windows.Input;

namespace Editor
{
    public class WInput
    {
        /// <summary> Mouse position in pixel coordinates. Read only. </summary>
        public Vector3 MousePosition { get; private set; }
        /// <summary> Delta position in pixel coordinates between frames. Read only. </summary>
        public Vector3 MouseDelta { get; private set; }
        /// <summary> Delta of the scroll wheel, one int per notch on wheel. </summary>
        public int MouseScrollDelta { get; private set; }

        /// <summary> Keys currently down this frame. </summary>
        private readonly bool[] m_keysDown = new bool[256];
        /// <summary> Keys that were down last frame. </summary>
        private readonly bool[] m_prevKeysDown = new bool[256];

        private readonly bool[] m_mouseBtnsDown = new bool[3];
        private readonly bool[] m_prevMouseBtnsDown = new bool[3];
        private Vector3 _prevMousePos;

        public bool GetKey(Key key)
        {
            return m_keysDown[(int)key];
        }

        public bool GetKeyDown(Key key)
        {
            return m_keysDown[(int)key] && !m_prevKeysDown[(int)key];
        }

        public bool GetKeyUp(Key key)
        {
            return m_prevKeysDown[(int)key] && !m_keysDown[(int)key];
        }

        public bool GetMouseButton(int button)
        {
            return m_mouseBtnsDown[button];
        }

        public bool GetMouseButtonDown(int button)
        {
            return m_mouseBtnsDown[button] && !m_prevMouseBtnsDown[button];
        }

        public bool GetMouseButtonUp(int button)
        {
            return m_prevMouseBtnsDown[button] && !m_mouseBtnsDown[button];
        }

        internal void Internal_UpdateInputState()
        {
            for (int i = 0; i < 256; i++)
                m_prevKeysDown[i] = m_keysDown[i];

            for (int i = 0; i < 3; i++)
                m_prevMouseBtnsDown[i] = m_mouseBtnsDown[i];

            MouseDelta = MousePosition - _prevMousePos;
            _prevMousePos = MousePosition;
            MouseScrollDelta = 0;
        }

        public void SetKeyboardState(Key keyCode, bool bPressed)
        {
            m_keysDown[(int)keyCode] = bPressed;
        }

        public void SetMouseState(MouseButton button, bool bPressed)
        {
            m_mouseBtnsDown[MouseButtonEnumToInt(button)] = bPressed;
        }

        public void SetMousePosition(Vector2 mousePos)
        {
            MousePosition = new Vector3(mousePos.X, mousePos.Y, 0);
        }

        public void SetMouseScrollDelta(int delta)
        {
            MouseScrollDelta = delta;
        }

        private static int MouseButtonEnumToInt(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return 0;
                case MouseButton.Right:
                    return 1;
                case MouseButton.Middle:
                    return 2;
            }

            System.Console.WriteLine("Unknown Mouse Button enum {0}, returning Left!", button);
            return 0;
        }
    }
}