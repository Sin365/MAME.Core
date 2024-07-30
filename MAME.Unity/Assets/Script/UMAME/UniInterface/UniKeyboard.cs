using MAME.Core.run_interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UniKeyboard : MonoBehaviour, IKeyboard
{
    Dictionary<KeyCode, MotionKey> dictKeyCfgs = new Dictionary<KeyCode, MotionKey>();
    KeyCode[] CheckList;
    MotionKey[] mCurrKey = new MotionKey[0];

    #region
    public UILongClickButton btnP1;
    public UILongClickButton btnCoin1;
    public UILongClickButton btnA;
    public UILongClickButton btnB;
    public UILongClickButton btnC;
    public UILongClickButton btnD;
    public UILongClickButton btnE;
    public UILongClickButton btnF;
    public UILongClickButton btnAB;
    public UILongClickButton btnCD;

    FloatingJoystick mJoystick;
    #endregion

    List<UILongClickButton> mUIBtns = new List<UILongClickButton>();

    void Awake()
    {
        mJoystick = GameObject.Find("tfJoystick").GetComponent<FloatingJoystick>();

        btnP1 = GameObject.Find("btnP1").GetComponent<UILongClickButton>();
        btnCoin1 = GameObject.Find("btnCoin1").GetComponent<UILongClickButton>();
        btnA = GameObject.Find("btnA").GetComponent<UILongClickButton>();
        btnB = GameObject.Find("btnB").GetComponent<UILongClickButton>();
        btnC = GameObject.Find("btnC").GetComponent<UILongClickButton>();
        btnD = GameObject.Find("btnD").GetComponent<UILongClickButton>();
        btnE = GameObject.Find("btnE").GetComponent<UILongClickButton>();
        btnF = GameObject.Find("btnF").GetComponent<UILongClickButton>();
        btnAB = GameObject.Find("btnAB").GetComponent<UILongClickButton>();
        btnCD = GameObject.Find("btnCD").GetComponent<UILongClickButton>();

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

        btnP1.Key = new MotionKey[] { MotionKey.P1_GAMESTART };
        btnCoin1.Key = new MotionKey[] { MotionKey.P1_INSERT_COIN };
        btnA.Key = new MotionKey[] { MotionKey.P1_BTN_1 };
        btnB.Key = new MotionKey[] { MotionKey.P1_BTN_2 };
        btnC.Key = new MotionKey[] { MotionKey.P1_BTN_3 };
        btnD.Key = new MotionKey[] { MotionKey.P1_BTN_4 };
        btnE.Key = new MotionKey[] { MotionKey.P1_BTN_5 };
        btnF.Key = new MotionKey[] { MotionKey.P1_BTN_6 };

        btnAB.Key = new MotionKey[] { MotionKey.P1_BTN_1, MotionKey.P1_BTN_2 };
        btnCD.Key = new MotionKey[] { MotionKey.P1_BTN_3, MotionKey.P1_BTN_4 };

        mUIBtns.Add(btnP1);
        mUIBtns.Add(btnCoin1);
        mUIBtns.Add(btnA);
        mUIBtns.Add(btnB);
        mUIBtns.Add(btnC);
        mUIBtns.Add(btnD);
        mUIBtns.Add(btnE);
        mUIBtns.Add(btnF);
        mUIBtns.Add(btnAB);
        mUIBtns.Add(btnCD);
    }

    void OnEnable()
    {
    }

    public MotionKey[] GetPressedKeys()
    {
        return mCurrKey;
    }

    void Update()
    {
        List<MotionKey> temp = new List<MotionKey>();
        for (int i = 0; i < CheckList.Length; i++)
        {
            if (Input.GetKey(CheckList[i]))
                temp.Add(dictKeyCfgs[CheckList[i]]);
        }

        for (int i = 0; i < mUIBtns.Count; i++)
        {
            if (mUIBtns[i].bHotKey)
            {
                for (int j = 0; j < mUIBtns[i].Key.Length; j++)
                    temp.Add(mUIBtns[i].Key[j]);
            }
        }

        Vector2Int inputV2 = mJoystick.RawInputV2;
        //Debug.Log($"{inputV2.x},{inputV2.y}");
        if (inputV2.x > 0) temp.Add(MotionKey.P1_RIGHT); else if (inputV2.x < 0) temp.Add(MotionKey.P1_LEFT);
        if (inputV2.y > 0) temp.Add(MotionKey.P1_UP); else if (inputV2.y < 0) temp.Add(MotionKey.P1_DOWN);

        mCurrKey = temp.ToArray();
    }
}