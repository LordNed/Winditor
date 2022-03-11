using OpenTK;
using System.Runtime.InteropServices;
using System.Windows;
using System;
using NodeNetwork;

namespace WindEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
       {
            base.OnStartup(e);
            NNViewRegistrar.RegisterSplat();
        }

        /// <summary>
        /// Returns the dimensions of the user's primary screen.
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetScreenGeometry()
        {
            return new Vector2((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);
        }

        /// <summary>
        /// Returns the absolute cursor position on the desktop, not limited to the application.
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return new Vector2(lpPoint.X, lpPoint.Y);
        }

        public static void SetCursorPosition(float x, float y)
        {

            SetCursorPos((int)x, (int)y);
        }

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }


        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("User32.Dll")]
        private static extern long SetCursorPos(int x, int y);
    }
}
