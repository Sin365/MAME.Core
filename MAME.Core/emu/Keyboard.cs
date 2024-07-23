using Microsoft.DirectX.DirectInput;
using System.Runtime.InteropServices;
using System;
using ui;
using DIDevice = Microsoft.DirectX.DirectInput.Device;

namespace mame
{
    public class Keyboard
    {
        public static bool bF10;
        public static DIDevice dIDevice;
        // 需要引入的Windows API函数声明（此处省略具体实现）  

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, string Enumerator, IntPtr hwndParent, uint Flags);


        public static void InitializeInput(mainForm form1)
        {
            dIDevice = new DIDevice(SystemGuid.Keyboard);
            dIDevice.SetCooperativeLevel(form1, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
            dIDevice.Acquire();
        }
        struct KeyState
        {
            public bool IsPressed;
            public bool IsTriggered;
            public bool WasPressed;
        };
        private static KeyState[] m_KeyStates = new KeyState[256];
        public static bool IsPressed(Key key)
        {
            return m_KeyStates[(int)key].IsPressed;
        }
        public static bool IsTriggered(Key key)
        {
            return m_KeyStates[(int)key].IsTriggered;
        }
        public static void Update()
        {
            for (int i = 0; i < 256; i++)
            {
                m_KeyStates[i].IsPressed = false;
            }
            foreach (Key key in dIDevice.GetPressedKeys())
            {
                m_KeyStates[(int)key].IsPressed = true;
            }
            for (int i = 0; i < 256; i++)
            {
                if (m_KeyStates[i].IsPressed)
                {
                    if (m_KeyStates[i].WasPressed)
                    {
                        m_KeyStates[i].IsTriggered = false;
                    }
                    else
                    {
                        m_KeyStates[i].WasPressed = true;
                        m_KeyStates[i].IsTriggered = true;
                    }
                }
                else
                {
                    m_KeyStates[i].WasPressed = false;
                    m_KeyStates[i].IsTriggered = false;
                }
            }
        }
    }
}