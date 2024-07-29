using MAME.Core.Common;
using MAME.Core.run_interface;

namespace mame
{
    public class Keyboard
    {
        public static bool bF10;

        static IKeyboard mKeyboard;

        public static void InitializeInput(mainMotion form1, IKeyboard ikb)
        {
            mKeyboard = ikb;
        }

        public static bool IsPressed(Key key)
        {
            return mKeyboard.IsPressed(key);
        }
        public static bool IsTriggered(Key key)
        {
            return mKeyboard.IsTriggered(key);
        }

        public static void Update()
        {
            //TODO
            /*for (int i = 0; i < 256; i++)
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
            */
        }
    }
}