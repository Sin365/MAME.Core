using System.Collections.Generic;
using Bitmap = MAME.Core.AxiBitmap.AxiBitmap;

namespace mame
{
    public partial class Konami68000
    {
        private static string[] sde2 = new string[] { "," }, sde6 = new string[] { "-" };
        public static bool bTile0, bTile1, bTile2, bSprite;
        public delegate Bitmap GetBitmap();
        public static GetBitmap[] GetTilemaps;
        public static GetBitmap GetSprite;
        private static List<int> lSprite;
        public static int min_priority, max_priority;
        public static void GDIInit()
        {
            lSprite = new List<int>();
        }
    }
}
