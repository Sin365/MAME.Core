﻿namespace MAME.Core.Common
{
    public class konami68000Motion
    {
        private int locationX, locationY;

        #region
        public bool cbT0 = false;
        public bool cbT1 = false;
        public bool cbT2 = false;
        public bool cbSprite = false;
        public string tbSprite;
        #endregion
        public konami68000Motion()
        {
            tbSprite = "0000-4000";
        }
    }
}