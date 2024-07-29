﻿using MAME.Core.run_interface;
using System;

namespace mame
{
    public static class EmuLogger
    {

        #region 抽象出去
        static Action<string> Act_Log;

        public static void BindFunc(ILog ilog)
        {
            Act_Log -= Act_Log;

            Act_Log += ilog.Log;
        }

        public static void Log(string msg)
        {
            Act_Log?.Invoke(msg);
        }
        #endregion
    }
}
