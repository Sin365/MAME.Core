using MAME.Core.run_interface;
using UnityEngine;

public class UniLog : ILog
{
    public void Log(string msg)
    {
        Debug.Log(msg);
    }
}
