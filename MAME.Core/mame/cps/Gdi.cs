using Bitmap = MAME.Core.AxiBitmap.AxiBitmap;
using Color = MAME.Core.AxiBitmap.AxiColor;

namespace mame
{
    public partial class CPS
    {
        private static string[] sde2;
        private static int base_cps1_objG;
        private static int[] maskG;
        private static int[] mapsizeG;
        public static Color[] cc1G;
        public static Color m_ColorG;
        public static int nColorG, nSpriteG;
        private static byte[,] flagsmap0G, flagsmap1G, flagsmap2G;
        private static byte[,] pen_to_flags0G, pen_to_flags1G, pen_to_flags2G;
        public static byte[,] priority_bitmapG;
        private static int videocontrolG;
        public static int scrollx0, scrolly0;
        public static int scrollx1, scrolly1;
        public static int scrollx2, scrolly2;
        public static int scrollxSG, scrollySG;
        public static int[] iiCutColorG;
        public static int l0G, l1G, l2G, l3G;
        public static bool bRender0G, bRender1G, bRender2G, bRender3G;
        private static bool enable0G, enable1G, enable2G;
        private static int baseTilemap0G, baseTilemap1G, baseTilemap2G, basePaletteG;
        public static byte[] bbPaletteG;
        private static int layercontrolG, scrollrows1G;
        private static int[] rowscroll1G;
        public static int[] cps1_scrollxG, cps1_scrollyG;
        public delegate Bitmap gettileDelegateG();
        public static gettileDelegateG[] gettileDelegatesG;
        public delegate void gethighDelegateG();
        public static gethighDelegateG[] gethighDelegatesG;
        public static void GDIInit()
        {
            maskG = new int[4];
            mapsizeG = new int[3] { 0x200, 0x400, 0x800 };
            nColorG = 0xc00;
            sde2 = new string[] { "," };
            scrollxSG = 0;
            scrollySG = 0;
            bbPaletteG = new byte[nColorG * 2];
            rowscroll1G = new int[1024];
            cps1_scrollxG = new int[3];
            cps1_scrollyG = new int[3];
            gettileDelegatesG = new gettileDelegateG[]{
            };
            gethighDelegatesG = new gethighDelegateG[]{
                null,
            };
            flagsmap0G = new byte[0x200, 0x200];
            flagsmap1G = new byte[0x400, 0x400];
            flagsmap2G = new byte[0x800, 0x800];
            pen_to_flags0G = new byte[4, 16];
            pen_to_flags1G = new byte[4, 16];
            pen_to_flags2G = new byte[4, 16];
            priority_bitmapG = new byte[0x200, 0x200];
            cc1G = new Color[nColorG];
        }
    }
}
