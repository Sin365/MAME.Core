using Color = MAME.Core.AxiBitmap.AxiColor;
namespace mame
{
    public partial class PGM
    {
        public static Color m_ColorG;
        public static bool bRender0G, bRender1G, bRender2G, bRender3G;

        public static void GDIInit()
        {
            //m_ColorG = Color.Magenta;
        }
    }
}
