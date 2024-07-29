﻿namespace MAME.Core.run_interface
{
    public enum MotionKey : byte
    {
        EMU_PAUSED = 0,
        P1_INSERT_COIN,
        P1_GAMESTART,
        P1_UP,
        P1_DOWN,
        P1_LEFT,
        P1_RIGHT,
        P1_BTN_1,
        P1_BTN_2,
        P1_BTN_3,
        P1_BTN_4,
        P1_BTN_5,
        P1_BTN_6,
        P1_UNKNOW_E,
        P1_UNKNOW_F,
        P2_INSERT_COIN,
        P2_GAMESTART,
        P2_UP,
        P2_DOWN,
        P2_LEFT,
        P2_RIGHT,
        P2_BTN_1,
        P2_BTN_2,
        P2_BTN_3,
        P2_BTN_4,
        P2_BTN_5,
        P2_BTN_6,
        P2_UNKNOW_E,
        P2_UNKNOW_F,
        UNKNOW_Q,
        UNKNOW_N,
        UNKNOW_R,
        UNKNOW_T,
        UNKNOW_M,
        UNKNOW_V,
        UNKNOW_B,
        F10,
        F9,
        F8,
        F7,
        F6,
        F5,
        F4,
        F3,
        F2,
        F1,
        Escape,
        LeftShift,
        RightShift,
        /// <summary>
        /// 用于标记最后一个
        /// </summary>
        FinalKey
    }
}
