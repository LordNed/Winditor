using OpenTK;
using System.Windows.Input;

namespace WindEditor
{
    public static class WInput
    {
        /// <summary> Mouse position in pixel coordinates. Read only. </summary>
        public static Vector2 MousePosition { get; private set; }
        /// <summary> Delta position in pixel coordinates between frames. Read only. </summary>
        public static Vector2 MouseDelta { get; private set; }
        /// <summary> Delta of the scroll wheel, one int per notch on wheel. </summary>
        public static int MouseScrollDelta { get; private set; }

        /// <summary> Keys currently down this frame. </summary>
        private static readonly bool[] m_keysDown = new bool[256];
        /// <summary> Keys that were down last frame. </summary>
        private static readonly bool[] m_prevKeysDown = new bool[256];

        private static readonly bool[] m_mouseBtnsDown = new bool[3];
        private static readonly bool[] m_prevMouseBtnsDown = new bool[3];
        private static Vector2 m_prevMousePos;

        public static bool GetKey(Key key)
        {
            return m_keysDown[(int)key];
        }

        public static bool GetKeyDown(Key key)
        {
            return m_keysDown[(int)key] && !m_prevKeysDown[(int)key];
        }

        public static bool GetKeyUp(Key key)
        {
            return m_prevKeysDown[(int)key] && !m_keysDown[(int)key];
        }

        public static bool GetMouseButton(int button)
        {
            return m_mouseBtnsDown[button];
        }

        public static bool GetMouseButtonDown(int button)
        {
            return m_mouseBtnsDown[button] && !m_prevMouseBtnsDown[button];
        }

        public static bool GetMouseButtonUp(int button)
        {
            return m_prevMouseBtnsDown[button] && !m_mouseBtnsDown[button];
        }

        internal static void Internal_UpdateInputState()
        {
            for (int i = 0; i < 256; i++)
                m_prevKeysDown[i] = m_keysDown[i];

            for (int i = 0; i < 3; i++)
                m_prevMouseBtnsDown[i] = m_mouseBtnsDown[i];

            MouseDelta = MousePosition - m_prevMousePos;
            m_prevMousePos = MousePosition;
            MouseScrollDelta = 0;
        }

        public static void SetKeyboardState(Key keyCode, bool bPressed)
        {
            m_keysDown[(int)keyCode] = bPressed;
        }

        public static void SetMouseState(MouseButton button, bool bPressed)
        {
            m_mouseBtnsDown[MouseButtonEnumToInt(button)] = bPressed;
        }

        public static void SetMousePosition(Vector2 mousePos)
        {
            MousePosition = new Vector2(mousePos.X, mousePos.Y);
        }

        public static void SetMouseScrollDelta(int delta)
        {
            MouseScrollDelta = delta;
        }

        public static void ClearInput()
        {
            for (int i = 0; i < 256; i++)
            {
                m_keysDown[i] = false;
                m_prevKeysDown[i] = false;
            }

            for (int i = 0; i < 3; i++)
            {
                m_mouseBtnsDown[i] = false;
                m_prevMouseBtnsDown[i] = false;
            }
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