using MAME.Core.run_interface;

namespace mame
{
    public class Keyboard
    {
        public static bool bF10;

        static IKeyboard mKeyboard;

        struct KeyState
        {
            public bool IsPressed;
            public bool IsTriggered;
            public bool WasPressed;
        };
        private static KeyState[] m_KeyStates = new KeyState[(byte)MotionKey.FinalKey];

        MotionKey[] mKeyName;
        public static void InitializeInput(IKeyboard ikb)
        {
            mKeyboard = ikb;
        }

        public static bool IsPressed(MotionKey key)
        {
            return m_KeyStates[(int)key].IsPressed;
        }
        public static bool IsTriggered(MotionKey key)
        {
            return m_KeyStates[(int)key].IsTriggered;
        }

        public static void Update()
        {
            byte finalIndex = (byte)MotionKey.FinalKey;
            for (byte i = 0; i < finalIndex; i++)
            {
                m_KeyStates[i].IsPressed = false;
            }
            foreach (MotionKey key in mKeyboard.GetPressedKeys())
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