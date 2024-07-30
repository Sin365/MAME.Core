using MAME.Core.run_interface;
using UnityEngine;

public class UniMouse : MonoBehaviour,IMouse
{
    static int mX, mY;
    void Update()
    {
        mX = (int)Input.mousePosition.x;
        mY = (int)Input.mousePosition.y;
    }

    public void MouseXY(out int X, out int Y)
    {
        X = mX;
        Y = mY;
    }
}
