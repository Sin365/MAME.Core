using mame;
using System;
using System.Collections.Generic;
using System.IO;

namespace MAME.Core.Motion
{
    public partial class NeogeoMotion
    {
        private string[] sde2 = new string[] { "," };
        private int locationX, locationY;
        public List<string> tbResult;
        public string tbSprite = string.Empty;
        public string tbPoint = string.Empty;
        public string tbFile = string.Empty;
        public string tbSOffset = string.Empty;
        public string tbPensoffset = string.Empty;

        #region
        bool cbL0 = false;
        bool cbL1 = false;
        #endregion
        public NeogeoMotion()
        {
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
    }
}
