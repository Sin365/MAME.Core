using MAME.Core.Common;
using MAME.Core.run_interface;

namespace mame
{
    public class Mouse
    {
        public static int deltaX, deltaY, oldX, oldY;
        public static byte[] buttons;
        static IMouse iMouse;
        public static void InitialMouse(mainMotion form1, IMouse im)
        {
            iMouse = im;
        }

        public static void Update()
        {
            int X, Y;
            iMouse.MouseXY(out X, out Y);
            deltaX = X - oldX;
            deltaY = Y - oldY;
            oldX = X;
            oldY = Y;
            //TODO
            //MouseState mouseState = mouseDevice.CurrentMouseState;
            //deltaX = mouseState.X - oldX;
            //deltaY = mouseState.Y - oldY;
            //oldX = mouseState.X;
            //oldY = mouseState.Y;
            //buttons = mouseState.GetMouseButtons();
        }
    }
}
