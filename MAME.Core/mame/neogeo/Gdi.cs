using System.Collections.Generic;
using Color = MAME.Core.AxiBitmap.AxiColor;

namespace mame
{
    public partial class Neogeo
    {
        private static string[] sde2 = new string[] { "," }, sde6 = new string[] { "-" };
        public static Color m_ColorG;
        private static List<int> lSprite;
        public static bool bRender0G, bRender1G;
        public static void GDIInit()
        {
            //m_ColorG = Color.Magenta;
            lSprite = new List<int>();
        }
    }
}
