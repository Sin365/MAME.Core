using MAME.Core.AxiBitmap;
using System;
using Bitmap = MAME.Core.AxiBitmap.AxiBitmap;
using Color = MAME.Core.AxiBitmap.AxiColor;
using Rectangle = MAME.Core.AxiBitmap.Rectangle;

namespace mame
{
    partial class Video
    {
        //public delegate Bitmap drawcrosshairdelegate(Bitmap bm1);
        //public static drawcrosshairdelegate drawcrosshair;


        public delegate int[] drawcrosshairdelegate(int[] bm1);
        public static drawcrosshairdelegate drawcrosshair;

        //public static Bitmap drawcrosshair_null(Bitmap bm1)
        //{
        //    Bitmap bm2 = bm1;
        //    return bm2;
        //}

        public static int[] drawcrosshair_null(int[] bm1)
        {
            return bm1;
        }
        //public static Bitmap drawcrosshair_opwolf(Bitmap bm1)
        //{
        //    Bitmap bm2 = bm1;
        //    Graphics g = Graphics.FromImage(bm2);
        //    g.DrawImage(MultiplyAlpha(Crosshair.global.bitmap[0], (float)Crosshair.global.fade / 0xff), new Rectangle(Crosshair.global.x[0] - 10, Crosshair.global.y[0] - 10, 20, 20), new Rectangle(0, 0, 100, 100), GraphicsUnit.Pixel);
        //    g.Dispose();
        //    return bm2;
        //}

        public static Bitmap drawcrosshair_opwolf(Bitmap bm1)
        {
            Bitmap bm2 = bm1;
            bm2.DrawImage(
                MultiplyAlpha(Crosshair.global.bitmap[0], (float)Crosshair.global.fade / 0xff),
                new Rectangle(Crosshair.global.x[0] - 10,
                Crosshair.global.y[0] - 10, 20, 20),
                new Rectangle(0, 0, 100, 100));
            return bm2;
        }

        public static int[] drawcrosshair_opwolf(int[] bm1)
        {
            int[] bm2 = bm1;
            //TODO
            return bm2;
        }

        /* 替换代码
         * public static Bitmap MultiplyAlpha(Bitmap bitmap, float factor)
        {
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix33 = factor;
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix);
                graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            return result;
        }*/

        /// <summary>
        ///
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Bitmap MultiplyAlpha(Bitmap bitmap, float factor)
        {
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color originalColor = bitmap.GetPixel(x, y);
                    byte newAlpha = (byte)Math.Min(255, (int)(originalColor.a * factor));
                    Color newColor = new Color
                    {
                        r = originalColor.r,
                        g = originalColor.g,
                        b = originalColor.b,
                        a = newAlpha
                    };
                    result.SetPixel(x, y, newColor);
                }
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static int[] MultiplyAlpha(int[] bitmap, float factor)
        {
            int[] result = (int[])bitmap.Clone();
            for (int i = 0; i < result.Length; i++)
            {
                Color originalColor = AxiColor.FromArgb(result[i]);
                byte newAlpha = (byte)Math.Min(255, (int)(originalColor.a * factor));
                Color newColor = new Color
                {
                    r = originalColor.r,
                    g = originalColor.g,
                    b = originalColor.b,
                    a = newAlpha
                };
                result[i] = AxiColor.ToArgb(newColor);
            }
            return result;
        }

        //public static void GDIDraw()
        //{
        //    try
        //    {
        //        bitmapData = bitmapGDI.LockBits(new Rectangle(0, 0, Video.fullwidth, Video.fullheight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        //        Marshal.Copy(Video.bitmapcolor, 0, bitmapData.Scan0, Video.fullwidth * Video.fullheight);
        //        bitmapGDI.UnlockBits(bitmapData);
        //        if (Wintime.osd_ticks() < popup_text_end)
        //        {
        //            Machine.FORM.tsslStatus = sDrawText;
        //        }
        //        else
        //        {
        //            popup_text_end = 0;
        //            if (Mame.paused)
        //            {
        //                Machine.FORM.tsslStatus = "pause";
        //            }
        //            else
        //            {
        //                switch (Mame.playState)
        //                {
        //                    case Mame.PlayState.PLAY_RECORDRUNNING:
        //                        Machine.FORM.tsslStatus = "record";
        //                        break;
        //                    case Mame.PlayState.PLAY_REPLAYRUNNING:
        //                        Machine.FORM.tsslStatus = "replay";
        //                        break;
        //                    default:
        //                        Machine.FORM.tsslStatus = "run";
        //                        break;
        //                }
        //            }
        //        }
        //        //bbmp[iMode] = drawcrosshair((Bitmap)bitmapGDI.Clone(new Rectangle(offsetx, offsety, width, height), PixelFormat.Format32bppArgb));
        //        bbmp[iMode] = drawcrosshair(bitmapGDI.Clone(new Rectangle(offsetx, offsety, width, height)));
        //        switch (Machine.sDirection)
        //        {
        //            case "":
        //                break;
        //            case "90":
        //                bbmp[iMode].RotateFlip(RotateFlipType.Rotate90FlipNone);
        //                break;
        //            case "180":
        //                bbmp[iMode].RotateFlip(RotateFlipType.Rotate180FlipNone);
        //                break;
        //            case "270":
        //                bbmp[iMode].RotateFlip(RotateFlipType.Rotate270FlipNone);
        //                break;
        //        }
        //        //Machine.FORM.pictureBox1.Image = bbmp[iMode];
        //        SubmitVideo(bbmp[iMode]);
        //    }
        //    catch
        //    {

        //    }
        //}


        public static void GDIDraw()
        {
            try
            {
                int[] TempData = AxiBitmapEx.CloneIntColorArr(Video.bitmapcolor, Video.fullwidth, Video.fullheight, new Rectangle(offsetx, offsety, width, height));
                drawcrosshair(TempData);
                //bbmp[iMode] = drawcrosshair(bitmapGDI.Clone(new Rectangle(offsetx, offsety, width, height)));
                switch (Machine.sDirection)
                {
                    case "":
                        break;
                    case "90":
                        bbmp[iMode].RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case "180":
                        bbmp[iMode].RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case "270":
                        bbmp[iMode].RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
                //Machine.FORM.pictureBox1.Image = bbmp[iMode];
                SubmitVideo(Video.bitmapcolor);
            }
            catch (Exception ex)
            {

            }
        }

        public static void CopyDataFrom(AxiBitmap destBitmap, byte[] sourceData, int sourceStride)
        {
            int pixelCount = destBitmap.Width * destBitmap.Height;
            if (sourceData.Length < pixelCount * 4) // 假设每个AxiColor是4字节  
                throw new ArgumentException("Source data is too small.");

            // 注意：这里我们假设sourceStride是源数据的每行字节数，它可能包含额外的填充字节  
            // 以确保每行的开始都是4的倍数。但是，对于紧密打包的AxiColor数组，我们可以忽略它。  

            for (int y = 0; y < destBitmap.Height; y++)
            {
                int sourceOffset = y * sourceStride; // 但对于紧密打包的AxiColor，这将是 y * (destBitmap.Width * 4)  
                                                     // 然而，由于我们知道sourceData是直接对应于AxiColor的，我们可以简化为：  
                sourceOffset = y * destBitmap.Width * 4; // 假设没有行填充  

                for (int x = 0; x < destBitmap.Width; x++)
                {
                    int index = y * destBitmap.Width + x;
                    int sourceIndex = sourceOffset + x * 4;

                    byte r = sourceData[sourceIndex];
                    byte g = sourceData[sourceIndex + 1];
                    byte b = sourceData[sourceIndex + 2];
                    byte a = sourceData[sourceIndex + 3];

                    destBitmap.mData[index] = new AxiColor { r = r, g = g, b = b, a = a };
                }
            }
        }



    }
}