using AxiReplay;
using MAME.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class KeyCodeCore : IKeyboard
{
    public Dictionary<KeyCode, MotionKey> dictKeyCfgs = new Dictionary<KeyCode, MotionKey>();
    public KeyCode[] CheckList;
    public MotionKey[] mCurrKey = new MotionKey[0];
    public ulong CurryInpuAllData = 0;
    List<MotionKey> temp = new List<MotionKey>();
    ulong tempInputAllData = 0;
    UniKeyboard mUniKeyboard;
    bool bReplayMode;
    List<MotionKey> ReplayCheckKey = new List<MotionKey>();

    public MotionKey[] GetPressedKeys()
    {
        if (!bReplayMode)
        {
            //UMAME.instance.mReplayWriter.NextFrame(CurryInpuAllData);
            UMAME.instance.mReplayWriter.NextFramebyFrameIdx((int)UMAME.instance.mUniVideoPlayer.mFrame, CurryInpuAllData);
            return mCurrKey;
        }
        else
        {
            //有变化

            //if (UMAME.instance.mReplayReader.NextFrame(out AxiReplay.ReplayStep stepData))
            if (UMAME.instance.mReplayReader.NextFramebyFrameIdx((int)UMAME.instance.mUniVideoPlayer.mFrame, out AxiReplay.ReplayStep stepData))
            {
                temp.Clear();
                //有数据
                for (int i = 0; i < ReplayCheckKey.Count; i++)
                {
                    if ((stepData.InPut & (ulong)ReplayCheckKey[i]) > 0)
                        temp.Add(ReplayCheckKey[i]);
                }
                mCurrKey = temp.ToArray();
            }

#if UNITY_EDITOR
            string TempStr = "";
            foreach (var item in mCurrKey)
            {
                TempStr += $"{item.ToString()}|";
            }
            if (!string.IsNullOrEmpty(TempStr))
                Debug.Log($"Input-》{CurryInpuAllData} => {TempStr}");
#endif
            return mCurrKey;
        }
    }

    public void SetRePlay(bool IsReplay)
    {
        bReplayMode = IsReplay;
    }
    public void Init(UniKeyboard uniKeyboard, bool IsReplay)
    {
        bReplayMode = IsReplay;
        mUniKeyboard = uniKeyboard;
        foreach (MotionKey mkey in Enum.GetValues(typeof(MotionKey)))
        {
            ReplayCheckKey.Add(mkey);
        }

        dictKeyCfgs.Clear();
        dictKeyCfgs.Add(KeyCode.P, MotionKey.EMU_PAUSED);
        dictKeyCfgs.Add(KeyCode.Alpha1, MotionKey.P1_GAMESTART);
        dictKeyCfgs.Add(KeyCode.Alpha5, MotionKey.P1_INSERT_COIN);
        dictKeyCfgs.Add(KeyCode.W, MotionKey.P1_UP);
        dictKeyCfgs.Add(KeyCode.S, MotionKey.P1_DOWN);
        dictKeyCfgs.Add(KeyCode.A, MotionKey.P1_LEFT);
        dictKeyCfgs.Add(KeyCode.D, MotionKey.P1_RIGHT);
        dictKeyCfgs.Add(KeyCode.J, MotionKey.P1_BTN_1);
        dictKeyCfgs.Add(KeyCode.K, MotionKey.P1_BTN_2);
        dictKeyCfgs.Add(KeyCode.L, MotionKey.P1_BTN_3);
        dictKeyCfgs.Add(KeyCode.U, MotionKey.P1_BTN_4);
        dictKeyCfgs.Add(KeyCode.KeypadDivide, MotionKey.P2_GAMESTART);
        dictKeyCfgs.Add(KeyCode.KeypadMultiply, MotionKey.P2_INSERT_COIN);
        dictKeyCfgs.Add(KeyCode.UpArrow, MotionKey.P2_UP);
        dictKeyCfgs.Add(KeyCode.DownArrow, MotionKey.P2_DOWN);
        dictKeyCfgs.Add(KeyCode.LeftArrow, MotionKey.P2_LEFT);
        dictKeyCfgs.Add(KeyCode.RightArrow, MotionKey.P2_RIGHT);
        dictKeyCfgs.Add(KeyCode.Keypad1, MotionKey.P2_BTN_1);
        dictKeyCfgs.Add(KeyCode.Keypad2, MotionKey.P2_BTN_2);
        dictKeyCfgs.Add(KeyCode.Keypad3, MotionKey.P2_BTN_3);
        dictKeyCfgs.Add(KeyCode.Keypad4, MotionKey.P2_BTN_4);
        CheckList = dictKeyCfgs.Keys.ToArray();


        mUniKeyboard.btnP1.Key = new long[] { (long)MotionKey.P1_GAMESTART };
        mUniKeyboard.btnCoin1.Key = new long[] { (long)MotionKey.P1_INSERT_COIN };
        mUniKeyboard.btnA.Key = new long[] { (long)MotionKey.P1_BTN_1 };
        mUniKeyboard.btnB.Key = new long[] { (long)MotionKey.P1_BTN_2 };
        mUniKeyboard.btnC.Key = new long[] { (long)MotionKey.P1_BTN_3 };
        mUniKeyboard.btnD.Key = new long[] { (long)MotionKey.P1_BTN_4 };
        //mUniKeyboard.btnE.Key = new long[] { (long)MotionKey.P1_BTN_5 };
        //mUniKeyboard.btnF.Key = new long[] { (long)MotionKey.P1_BTN_6 };
        mUniKeyboard.btnAB.Key = new long[] { (long)MotionKey.P1_BTN_1, (long)MotionKey.P1_BTN_2 };
        mUniKeyboard.btnCD.Key = new long[] { (long)MotionKey.P1_BTN_3, (long)MotionKey.P1_BTN_4 };
        mUniKeyboard.btnABC.Key = new long[] { (long)MotionKey.P1_BTN_1, (long)MotionKey.P1_BTN_2, (long)MotionKey.P1_BTN_3 };
    }

    public void UpdateLogic()
    {
        if (bReplayMode) return;
        tempInputAllData = 0;
        temp.Clear();
        for (int i = 0; i < CheckList.Length; i++)
        {
            if (Input.GetKey(CheckList[i]))
            {
                MotionKey mk = dictKeyCfgs[CheckList[i]];
                temp.Add(mk);
                tempInputAllData |= (ulong)mk;
            }
        }

        for (int i = 0; i < mUniKeyboard.mUIBtns.Count; i++)
        {
            if (mUniKeyboard.mUIBtns[i].bHotKey)
            {
                for (int j = 0; j < mUniKeyboard.mUIBtns[i].Key.Length; j++)
                {
                    MotionKey mk = (MotionKey)mUniKeyboard.mUIBtns[i].Key[j];
                    temp.Add(mk);
                    tempInputAllData |= (ulong)mk;
                }
            }
        }

        Vector2Int inputV2 = mUniKeyboard.mJoystick.RawInputV2;
        //Debug.Log($"{inputV2.x},{inputV2.y}");
        if (inputV2.x > 0)
        {
            temp.Add(MotionKey.P1_RIGHT);
            tempInputAllData |= (ulong)MotionKey.P1_RIGHT;
        }
        else if (inputV2.x < 0)
        {
            temp.Add(MotionKey.P1_LEFT);
            tempInputAllData |= (ulong)MotionKey.P1_LEFT;
        }
        if (inputV2.y > 0)
        {
            temp.Add(MotionKey.P1_UP);
            tempInputAllData |= (ulong)MotionKey.P1_UP;
        }
        else if (inputV2.y < 0)
        {
            temp.Add(MotionKey.P1_DOWN);
            tempInputAllData |= (ulong)MotionKey.P1_DOWN;
        }
        CurryInpuAllData = tempInputAllData;
        mCurrKey = temp.ToArray();

    }
}