using MAME.Core;
using System;
using System.Collections.Generic;

namespace MAME.Core
{
    public class Keyboard
    {
        public static bool bF10;

        static IKeyboard mKeyboard;

        //const int CheckMaxEnumIdx = 33;

        class KeyState
        {
            public bool IsPressed;
            public bool IsTriggered;
            public bool WasPressed;
        };
        private static Dictionary<MotionKey, KeyState> m_KeyStates = new Dictionary<MotionKey, KeyState>();

        static MotionKey[] mKeyName;
        public static void InitializeInput(IKeyboard ikb)
        {
            mKeyboard = ikb;
            List<MotionKey> temp = new List<MotionKey>();
            foreach (MotionKey mkey in Enum.GetValues(typeof(MotionKey)))
            {
                //if (mkey > MotionKey.FinalKey)
                //    break;
                m_KeyStates[mkey] = new KeyState();
                temp.Add(mkey);
            }
            mKeyName = temp.ToArray();
        }

        public static bool IsPressed(MotionKey key)
        {
            return m_KeyStates[key].IsPressed;
        }
        public static bool IsTriggered(MotionKey key)
        {
            return m_KeyStates[key].IsTriggered;
        }

        public static void Update()
        {
            for (byte i = 0; i < mKeyName.Length; i++)
            {
                m_KeyStates[mKeyName[i]].IsPressed = false;
            }
            foreach (MotionKey key in mKeyboard.GetPressedKeys())
            {
                m_KeyStates[key].IsPressed = true;
            }
            for (int i = 0; i < mKeyName.Length; i++)
            {
                MotionKey key =  mKeyName[i];
                if (m_KeyStates[key].IsPressed)
                {
                    if (m_KeyStates[key].WasPressed)
                    {
                        m_KeyStates[key].IsTriggered = false;
                    }
                    else
                    {
                        m_KeyStates[key].WasPressed = true;
                        m_KeyStates[key].IsTriggered = true;
                    }
                }
                else
                {
                    m_KeyStates[key].WasPressed = false;
                    m_KeyStates[key].IsTriggered = false;
                }
            }
            //byte finalIndex = CheckMaxEnumIdx;
            //for (byte i = 0; i < finalIndex; i++)
            //{
            //    m_KeyStates[i].IsPressed = false;
            //}
            //foreach (MotionKey key in mKeyboard.GetPressedKeys())
            //{
            //    m_KeyStates[(int)key].IsPressed = true;
            //}
            //for (int i = 0; i < finalIndex; i++)
            //{
            //    if (m_KeyStates[i].IsPressed)
            //    {
            //        if (m_KeyStates[i].WasPressed)
            //        {
            //            m_KeyStates[i].IsTriggered = false;
            //        }
            //        else
            //        {
            //            m_KeyStates[i].WasPressed = true;
            //            m_KeyStates[i].IsTriggered = true;
            //        }
            //    }
            //    else
            //    {
            //        m_KeyStates[i].WasPressed = false;
            //        m_KeyStates[i].IsTriggered = false;
            //    }
            //}

        }
    }
}