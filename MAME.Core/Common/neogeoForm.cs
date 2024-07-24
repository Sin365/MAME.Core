using mame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace MAME.Core.Common
{
    public partial class neogeoForm
    {
        private mainForm _myParentForm;
        private string[] sde2 = new string[] { "," };
        private int locationX, locationY;
        public List<string> tbResult;
        public string tbSprite = string.Empty;
        public string tbPoint = string.Empty;
        public string tbFile = string.Empty;
        public string tbSOffset = string.Empty;
        public string tbPensoffset = string.Empty;
        public Bitmap pictureBox1;

        #region
        bool cbL0 = false;
        bool cbL1 = false;
        #endregion
        public neogeoForm(mainForm form)
        {
            this._myParentForm = form;
            tbResult = new List<string>();
            neogeoForm_Load();
        }
        private void neogeoForm_Load()
        {
            cbL0 = true;
            cbL1 = true;
            tbSprite = "000-17C";
            tbFile = "00";
            tbPoint = "350,240,30,16";
            tbSOffset = "01cb0600";
            tbPensoffset = "ed0";
        }
        private void DumpRam()
        {
            int i, j;
            BinaryWriter bw1 = new BinaryWriter(new FileStream("dump1.dat", FileMode.Create));
            bw1.Write(Memory.mainram, 0, 0x10000);
            bw1.Close();
            BinaryWriter bw2 = new BinaryWriter(new FileStream("dump2.dat", FileMode.Create));
            bw2.Write(Neogeo.mainram2, 0, 0x10000);
            bw2.Close();
            BinaryWriter bw3 = new BinaryWriter(new FileStream("dump3.dat", FileMode.Create));
            for (i = 0; i < 0x10000; i++)
            {
                bw3.Write(Neogeo.neogeo_videoram[i]);
            }
            BinaryWriter bw4 = new BinaryWriter(new FileStream("dump4.dat", FileMode.Create));
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 0x1000; j++)
                {
                    bw4.Write(Neogeo.palettes[i, j]);
                }
            }
        }
        private void WriteRam()
        {
            int i, j;
            byte[] bb1 = new byte[0x10000], bb2 = new byte[0x10000], bb3 = new byte[0x20000], bb4 = new byte[0x4000];
            BinaryReader br1 = new BinaryReader(new FileStream("dump1.dat", FileMode.Open));
            br1.Read(bb1, 0, 0x10000);
            br1.Close();
            Array.Copy(bb1, Memory.mainram, 0x10000);
            BinaryReader br2 = new BinaryReader(new FileStream("dump2.dat", FileMode.Open));
            br2.Read(bb2, 0, 0x10000);
            br2.Close();
            Array.Copy(bb2, Neogeo.mainram2, 0x10000);
            BinaryReader br3 = new BinaryReader(new FileStream("dump3.dat", FileMode.Open));
            br3.Read(bb3, 0, 0x20000);
            br3.Close();
            for (i = 0; i < 0x10000; i++)
            {
                Neogeo.neogeo_videoram[i] = (ushort)(bb3[i * 2] + bb3[i * 2 + 1] * 0x100);
            }
            BinaryReader br4 = new BinaryReader(new FileStream("dump4.dat", FileMode.Open));
            br4.Read(bb4, 0, 0x4000);
            br4.Close();
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 0x1000; j++)
                {
                    Neogeo.palettes[i, j] = (ushort)(bb4[i * 0x2000 + j * 2] + bb4[i * 0x2000 + j * 2 + 1] * 0x100);
                }
            }
        }
    }
}
