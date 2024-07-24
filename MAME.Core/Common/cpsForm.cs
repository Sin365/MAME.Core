using mame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace MAME.Core.Common
{
    public partial class cpsForm
    {
        private mainForm _myParentForm;
        private string[] sde2 = new string[] { "," };
        private int locationX, locationY;

        #region
        public bool cbLockpal = false;
        public bool cbRowscroll = false;
        public string tbInput = string.Empty;
        public bool cbL0 = false;
        public bool cbL1 = false;
        public bool cbL2 = false;
        public bool cbL3 = false;
        public string tbFile = string.Empty;
        public string tbPoint = string.Empty;
        public int cbLayer = 0;
        public string tbCode = string.Empty;
        public string tbColor = string.Empty;

        public string tbScroll1x = string.Empty;
        public string tbScroll1y = string.Empty;
        public string tbScroll2x = string.Empty;
        public string tbScroll2y = string.Empty;
        public string tbScroll3x = string.Empty;
        public string tbScroll3y = string.Empty;
        public string tbScrollsx = string.Empty;
        public string tbScrollsy = string.Empty;
        public List<string> tbResult = new List<string>();
        #endregion
        public cpsForm(mainForm form)
        {
            this._myParentForm = form;
        }
        private void cpsForm_Load(object sender, EventArgs e)
        {
            cbL0 = true;
            cbL1 = true;
            cbL2 = true;
            cbL3 = true;
            tbFile = "00";
            tbPoint = "512,512,0,256";
            cbLayer = 0;
            tbCode = "0000";
            tbColor = "00";
        }
        private void GetData()
        {
            tbScroll1x = CPS.scroll1x.ToString("X4");
            tbScroll1y = CPS.scroll1y.ToString("X4");
            tbScroll2x = CPS.scroll2x.ToString("X4");
            tbScroll2y = CPS.scroll2y.ToString("X4");
            tbScroll3x = CPS.scroll3x.ToString("X4");
            tbScroll3y = CPS.scroll3y.ToString("X4");
            tbScrollsx = CPS.scrollxSG.ToString();
            tbScrollsy = CPS.scrollySG.ToString();
        }
        private Bitmap GetTile0(int gfxset, int code1, int iColor)
        {
            int i1, i2, i3, i4, i5, i6;
            int iCode, iByte, cols, rows;
            int tilewidth, tileheight;
            int iOffset;
            int idx = 0;
            Color c1;
            Bitmap bm1;
            int width, height;
            int x0, y0, dx0, dy0;
            int ratio = 4;
            width = 0x200;
            height = 0x200;
            tilewidth = 8;
            tileheight = 8;
            cols = width / tilewidth / ratio;
            rows = height / tileheight / ratio;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iCode = code1 + i4 * 0x10 + i3;
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = iCode * 0x80 + gfxset * 8 + i1 + i2 * 16;
                                iByte = CPS.gfx1rom[iOffset];
                                idx = iColor * 0x10 + iByte;
                                c1 = CPS.cc1G[idx];
                                for (i5 = 0; i5 < ratio; i5++)
                                {
                                    for (i6 = 0; i6 < ratio; i6++)
                                    {
                                        ptr2 = ptr + (((y0 + dy0 * i2) * ratio + i6) * width + ((x0 + dx0 * i1) * ratio + i5)) * 4;
                                        *ptr2 = c1.B;
                                        *(ptr2 + 1) = c1.G;
                                        *(ptr2 + 2) = c1.R;
                                        *(ptr2 + 3) = c1.A;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        private Bitmap GetTile1(int code1, int iColor)
        {
            int i1, i2, i3, i4, i5, i6;
            int iCode, iByte, cols, rows;
            int tilewidth, tileheight;
            int iOffset;
            int idx = 0;
            Color c1;
            Bitmap bm1;
            int width, height;
            int x0, y0, dx0, dy0;
            int ratio = 2;
            width = 0x200;
            height = 0x200;
            tilewidth = 16;
            tileheight = 16;
            cols = width / tilewidth / ratio;
            rows = height / tileheight / ratio;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iCode = code1 + i4 * 0x10 + i3;
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = iCode * 0x40 * 4 + i1 + i2 * 16;
                                iByte = CPS.gfx1rom[iOffset];
                                idx = iColor * 0x10 + iByte;
                                c1 = CPS.cc1G[idx];
                                for (i5 = 0; i5 < ratio; i5++)
                                {
                                    for (i6 = 0; i6 < ratio; i6++)
                                    {
                                        ptr2 = ptr + (((y0 + dy0 * i2) * ratio + i6) * width + ((x0 + dx0 * i1) * ratio + i5)) * 4;
                                        *ptr2 = c1.B;
                                        *(ptr2 + 1) = c1.G;
                                        *(ptr2 + 2) = c1.R;
                                        *(ptr2 + 3) = c1.A;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        private Bitmap GetTile2(int code1, int iColor)
        {
            int i1, i2, i3, i4, i5, i6;
            int iCode, iByte, cols, rows;
            int tilewidth, tileheight;
            int iOffset;
            int idx = 0;
            Color c1;
            Bitmap bm1;
            int width, height;
            int x0, y0, dx0, dy0;
            int ratio = 1;
            width = 0x200;
            height = 0x200;
            tilewidth = 32;
            tileheight = 32;
            cols = width / tilewidth / ratio;
            rows = height / tileheight / ratio;
            bm1 = new Bitmap(width, height);
            BitmapData bmData;
            bmData = bm1.LockBits(new Rectangle(0, 0, bm1.Width, bm1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* ptr = (byte*)(bmData.Scan0);
                byte* ptr2 = (byte*)0;
                for (i3 = 0; i3 < cols; i3++)
                {
                    for (i4 = 0; i4 < rows; i4++)
                    {
                        iCode = code1 + i4 * 0x10 + i3;
                        x0 = tilewidth * i3;
                        y0 = tileheight * i4;
                        dx0 = 1;
                        dy0 = 1;
                        for (i1 = 0; i1 < tilewidth; i1++)
                        {
                            for (i2 = 0; i2 < tileheight; i2++)
                            {
                                iOffset = iCode * 0x40 * 16 + i1 + i2 * 16 * 2;
                                iByte = CPS.gfx1rom[iOffset];
                                idx = iColor * 0x10 + iByte;
                                c1 = CPS.cc1G[idx];
                                for (i5 = 0; i5 < ratio; i5++)
                                {
                                    for (i6 = 0; i6 < ratio; i6++)
                                    {
                                        ptr2 = ptr + (((y0 + dy0 * i2) * ratio + i6) * width + ((x0 + dx0 * i1) * ratio + i5)) * 4;
                                        *ptr2 = c1.B;
                                        *(ptr2 + 1) = c1.G;
                                        *(ptr2 + 2) = c1.R;
                                        *(ptr2 + 3) = c1.A;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            bm1.UnlockBits(bmData);
            return bm1;
        }
        private void DumpRam()
        {
            BinaryWriter bw1 = new BinaryWriter(new FileStream("dump1.dat", FileMode.Create));
            bw1.Write(Memory.mainram, 0, 0x10000);
            bw1.Close();
            BinaryWriter bw2 = new BinaryWriter(new FileStream("dump2.dat", FileMode.Create));
            bw2.Write(CPS.gfxram, 0, 0x30000);
            bw2.Close();
            BinaryWriter bw3 = new BinaryWriter(new FileStream("dump3.dat", FileMode.Create));
            int i;
            for (i = 0; i < 0x20; i++)
            {
                bw3.Write(CPS.cps_a_regs[i]);
            }
            for (i = 0; i < 0x20; i++)
            {
                bw3.Write(CPS.cps_b_regs[i]);
            }
            bw3.Close();
        }
        private void WriteRam()
        {
            byte[] bb1 = new byte[0x10000], bb2 = new byte[0x30000], bb3 = new byte[0x80];
            BinaryReader br1 = new BinaryReader(new FileStream("dump1.dat", FileMode.Open));
            br1.Read(bb1, 0, 0x10000);
            br1.Close();
            Array.Copy(bb1, Memory.mainram, 0x10000);
            BinaryReader br2 = new BinaryReader(new FileStream("dump2.dat", FileMode.Open));
            br2.Read(bb2, 0, 0x30000);
            br2.Close();
            Array.Copy(bb2, CPS.gfxram, 0x30000);
            BinaryReader br3 = new BinaryReader(new FileStream("dump3.dat", FileMode.Open));
            br3.Read(bb3, 0, 0x80);
            br3.Close();
            int i;
            for (i = 0; i < 0x20; i++)
            {
                CPS.cps_a_regs[i] = (ushort)(bb3[i * 2] + bb3[i * 2 + 1] * 0x100);
            }
            for (i = 0; i < 0x20; i++)
            {
                CPS.cps_b_regs[i] = (ushort)(bb3[0x40 + i * 2] + bb3[0x40 + i * 2 + 1] * 0x100);
            }
        }
    }
}
