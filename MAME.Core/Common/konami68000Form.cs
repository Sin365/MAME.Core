using mame;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace MAME.Core.Common
{
    public class konami68000Form
    {
        private mainForm _myParentForm;
        private int locationX, locationY;

        #region
        public bool cbT0 = false;
        public bool cbT1 = false;
        public bool cbT2 = false;
        public bool cbSprite = false;
        public Bitmap pictureBox1;
        public string tbSprite;
        #endregion
        public konami68000Form(mainForm form)
        {
            this._myParentForm = form;
            tbSprite = "0000-4000";
        }
    }
}
