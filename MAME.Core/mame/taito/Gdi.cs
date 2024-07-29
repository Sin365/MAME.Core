namespace mame
{
    public partial class Taito
    {
        public static bool bBg, bFg, bSprite;
        private static byte[,] flagsmapBG, flagsmapFG;
        private static byte[,] pen_to_flagsBG, pen_to_flagsFG;
        public static byte[,] priority_bitmapG;
        public static void GDIInit()
        {
            flagsmapBG = new byte[0x200, 0x200];
            flagsmapFG = new byte[0x200, 0x200];
            priority_bitmapG = new byte[0x140, 0x100];
        }
    }
}