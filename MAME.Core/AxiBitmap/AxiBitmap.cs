using System;

namespace MAME.Core.AxiBitmap
{
    public struct AxiColor
    {
        public byte r, g, b, a;
        // 构造函数  
        public AxiColor(byte _r, byte _g, byte _b)
        {
            r = _r;
            g = _g;
            b = _b;
            a = 255;
        }

        public AxiColor(byte _r, byte _g, byte _b, byte _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }
        public static AxiColor FromArgb(int argb)
        {
            byte a = (byte)((argb >> 24) & 0xFF);
            byte r = (byte)((argb >> 16) & 0xFF);
            byte g = (byte)((argb >> 8) & 0xFF);
            byte b = (byte)(argb & 0xFF);
            return new AxiColor { r = r, g = g, b = b, a = a };
        }
        public static int ToArgb(AxiColor color)
        {
            return (color.a << 24) | (color.r << 16) | (color.g << 8) | color.b;
        }

        #region 颜色定义
        public static AxiColor Transparent => new AxiColor(0, 0, 0, 0);
        public static AxiColor AliceBlue => new AxiColor(240, 248, 255);
        public static AxiColor AntiqueWhite => new AxiColor(250, 235, 215);
        public static AxiColor Aqua => new AxiColor(0, 255, 255);
        public static AxiColor Aquamarine => new AxiColor(127, 255, 212);
        public static AxiColor Azure => new AxiColor(240, 255, 255);
        public static AxiColor Beige => new AxiColor(245, 245, 220);
        public static AxiColor Bisque => new AxiColor(255, 228, 196);
        public static AxiColor Black => new AxiColor(0, 0, 0);
        public static AxiColor BlanchedAlmond => new AxiColor(255, 235, 205);
        public static AxiColor Blue => new AxiColor(0, 0, 255);
        public static AxiColor BlueViolet => new AxiColor(138, 43, 226);
        public static AxiColor Brown => new AxiColor(165, 42, 42);
        public static AxiColor BurlyWood => new AxiColor(222, 184, 135);
        public static AxiColor CadetBlue => new AxiColor(95, 158, 160);
        public static AxiColor Chartreuse => new AxiColor(127, 255, 0);
        public static AxiColor Chocolate => new AxiColor(210, 105, 30);
        public static AxiColor Coral => new AxiColor(255, 127, 80);
        public static AxiColor CornflowerBlue => new AxiColor(100, 149, 237);
        public static AxiColor Cornsilk => new AxiColor(255, 248, 220);
        public static AxiColor Crimson => new AxiColor(220, 20, 60);
        public static AxiColor Cyan => new AxiColor(0, 255, 255);
        public static AxiColor DarkBlue => new AxiColor(0, 0, 139);
        public static AxiColor DarkCyan => new AxiColor(0, 139, 139);
        public static AxiColor DarkGoldenrod => new AxiColor(184, 134, 11);
        public static AxiColor DarkGray => new AxiColor(169, 169, 169);
        public static AxiColor DarkGreen => new AxiColor(0, 100, 0);
        public static AxiColor DarkKhaki => new AxiColor(189, 183, 107);
        public static AxiColor DarkMagenta => new AxiColor(139, 0, 139);
        public static AxiColor DarkOliveGreen => new AxiColor(85, 107, 47);
        public static AxiColor DarkOrange => new AxiColor(255, 140, 0);
        public static AxiColor DarkOrchid => new AxiColor(153, 50, 204);
        public static AxiColor DarkRed => new AxiColor(139, 0, 0);
        public static AxiColor DarkSalmon => new AxiColor(233, 150, 122);
        public static AxiColor DarkSeaGreen => new AxiColor(143, 188, 143);
        public static AxiColor DarkSlateBlue => new AxiColor(72, 61, 139);
        public static AxiColor DarkSlateGray => new AxiColor(47, 79, 79);
        public static AxiColor DarkViolet => new AxiColor(148, 0, 211);
        public static AxiColor DeepPink => new AxiColor(255, 20, 147);
        public static AxiColor DeepSkyBlue => new AxiColor(0, 191, 255);
        public static AxiColor DimGray => new AxiColor(105, 105, 105);
        public static AxiColor DodgerBlue => new AxiColor(30, 144, 255);
        public static AxiColor FireBrick => new AxiColor(178, 34, 34);
        public static AxiColor FloralWhite => new AxiColor(255, 250, 240);
        public static AxiColor ForestGreen => new AxiColor(34, 139, 34);
        public static AxiColor Fuchsia => new AxiColor(255, 0, 255);
        public static AxiColor Gainsboro => new AxiColor(220, 220, 220);
        public static AxiColor GhostWhite => new AxiColor(248, 248, 255);
        public static AxiColor Gold => new AxiColor(255, 215, 0);
        public static AxiColor Goldenrod => new AxiColor(218, 165, 32);
        public static AxiColor Gray => new AxiColor(128, 128, 128);
        public static AxiColor Green => new AxiColor(0, 128, 0);
        public static AxiColor GreenYellow => new AxiColor(173, 255, 47);
        public static AxiColor Honeydew => new AxiColor(240, 255, 240);
        public static AxiColor HotPink => new AxiColor(255, 105, 180);
        public static AxiColor IndianRed => new AxiColor(205, 92, 92);
        public static AxiColor Indigo => new AxiColor(75, 0, 130);
        public static AxiColor Ivory => new AxiColor(255, 255, 240);
        public static AxiColor Khaki => new AxiColor(240, 230, 140);
        public static AxiColor Lavender => new AxiColor(230, 230, 250);
        public static AxiColor LavenderBlush => new AxiColor(255, 240, 245);
        public static AxiColor LawnGreen => new AxiColor(124, 252, 0);
        public static AxiColor LemonChiffon => new AxiColor(255, 250, 205);
        public static AxiColor LightBlue => new AxiColor(173, 216, 230);
        public static AxiColor LightCoral => new AxiColor(240, 128, 128);
        public static AxiColor LightCyan => new AxiColor(224, 255, 255);
        public static AxiColor LightGoldenrodYellow => new AxiColor(250, 250, 210);
        public static AxiColor LightGray => new AxiColor(211, 211, 211);
        public static AxiColor LightGreen => new AxiColor(144, 238, 144);
        public static AxiColor LightPink => new AxiColor(255, 182, 193);
        public static AxiColor LightSalmon => new AxiColor(255, 160, 122);
        public static AxiColor LightSeaGreen => new AxiColor(32, 178, 170);
        public static AxiColor LightSkyBlue => new AxiColor(135, 206, 250);
        public static AxiColor LightSlateGray => new AxiColor(119, 136, 153);
        public static AxiColor LightYellow => new AxiColor(255, 255, 224);
        public static AxiColor Lime => new AxiColor(0, 255, 0);
        public static AxiColor LimeGreen => new AxiColor(50, 205, 50);
        public static AxiColor Linen => new AxiColor(250, 240, 230);
        public static AxiColor Magenta => new AxiColor(255, 0, 255);
        public static AxiColor Maroon => new AxiColor(176, 48, 96);
        public static AxiColor MediumAquamarine => new AxiColor(102, 205, 170);
        public static AxiColor MediumBlue => new AxiColor(0, 0, 205);
        public static AxiColor MediumOrchid => new AxiColor(186, 85, 211);
        public static AxiColor MediumPurple => new AxiColor(147, 112, 219);
        public static AxiColor MediumSeaGreen => new AxiColor(60, 179, 113);
        public static AxiColor MediumSlateBlue => new AxiColor(123, 104, 238);
        public static AxiColor MediumSpringGreen => new AxiColor(0, 250, 154);
        public static AxiColor MediumTurquoise => new AxiColor(72, 209, 204);
        public static AxiColor MediumVioletRed => new AxiColor(199, 21, 133);
        public static AxiColor MidnightBlue => new AxiColor(25, 25, 112);
        public static AxiColor MintCream => new AxiColor(245, 255, 250);
        public static AxiColor MistyRose => new AxiColor(255, 228, 225);
        public static AxiColor Moccasin => new AxiColor(255, 228, 181);
        public static AxiColor NavajoWhite => new AxiColor(255, 222, 173);
        public static AxiColor Navy => new AxiColor(0, 0, 128);
        public static AxiColor OldLace => new AxiColor(253, 245, 230);
        public static AxiColor Olive => new AxiColor(128, 128, 0);
        public static AxiColor OliveDrab => new AxiColor(107, 142, 35);
        public static AxiColor Orange => new AxiColor(255, 165, 0);
        public static AxiColor OrangeRed => new AxiColor(255, 69, 0);
        public static AxiColor Orchid => new AxiColor(218, 112, 214);
        public static AxiColor PaleGoldenrod => new AxiColor(238, 232, 170);
        public static AxiColor PaleGreen => new AxiColor(152, 251, 152);
        public static AxiColor PaleTurquoise => new AxiColor(175, 238, 238);
        public static AxiColor PaleVioletRed => new AxiColor(219, 112, 147);
        public static AxiColor PapayaWhip => new AxiColor(255, 239, 213);
        public static AxiColor PeachPuff => new AxiColor(255, 218, 185);
        public static AxiColor Peru => new AxiColor(205, 133, 63);
        public static AxiColor Pink => new AxiColor(255, 192, 203);
        public static AxiColor Plum => new AxiColor(221, 160, 221);
        public static AxiColor PowderBlue => new AxiColor(176, 224, 230);
        public static AxiColor Purple => new AxiColor(160, 32, 240);
        public static AxiColor Red => new AxiColor(255, 0, 0);
        public static AxiColor RosyBrown => new AxiColor(188, 143, 143);
        public static AxiColor RoyalBlue => new AxiColor(65, 105, 225);
        public static AxiColor SaddleBrown => new AxiColor(139, 69, 19);
        public static AxiColor Salmon => new AxiColor(250, 128, 114);
        public static AxiColor SandyBrown => new AxiColor(244, 164, 96);
        public static AxiColor SeaGreen => new AxiColor(46, 139, 87);
        public static AxiColor SeaShell => new AxiColor(255, 245, 238);
        public static AxiColor Sienna => new AxiColor(160, 82, 45);
        public static AxiColor Silver => new AxiColor(192, 192, 192);
        public static AxiColor SkyBlue => new AxiColor(135, 206, 235);
        public static AxiColor SlateBlue => new AxiColor(106, 90, 205);
        public static AxiColor SlateGray => new AxiColor(112, 128, 144);
        public static AxiColor Snow => new AxiColor(255, 250, 250);
        public static AxiColor SpringGreen => new AxiColor(0, 255, 127);
        public static AxiColor SteelBlue => new AxiColor(70, 130, 180);
        public static AxiColor Tan => new AxiColor(210, 180, 140);
        public static AxiColor Teal => new AxiColor(0, 128, 128);
        public static AxiColor Thistle => new AxiColor(216, 191, 216);
        public static AxiColor Tomato => new AxiColor(255, 99, 71);
        public static AxiColor Turquoise => new AxiColor(64, 224, 208);
        public static AxiColor Violet => new AxiColor(238, 130, 238);
        public static AxiColor Wheat => new AxiColor(245, 222, 179);
        public static AxiColor White => new AxiColor(255, 255, 255);
        public static AxiColor WhiteSmoke => new AxiColor(245, 245, 245);
        public static AxiColor Yellow => new AxiColor(255, 255, 0);
        public static AxiColor YellowGreen => new AxiColor(154, 205, 50);
        #endregion
    }

    public static class AxiBitmapEx
    {
        public static int ToArgb(this AxiColor color)
        {
            return (color.a << 24) | (color.r << 16) | (color.g << 8) | color.b;
        }
    }
}
