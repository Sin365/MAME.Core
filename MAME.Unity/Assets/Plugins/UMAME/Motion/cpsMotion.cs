using System.Collections.Generic;

namespace MAME.Core
{
    public partial class CpsMotion
    {
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
        public CpsMotion()
        {
        }
    }
}
