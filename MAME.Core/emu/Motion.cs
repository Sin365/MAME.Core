using MAME.Core.Common;
using MAME.Core.run_interface;

namespace mame
{
    /// <summary>
    /// 原依赖Form的内容
    /// </summary>
    public class Motion
    {
        private static uint UI_FILLCOLOR = Palette.make_argb(0xe0, 0x10, 0x10, 0x30);
        public delegate void motion_delegate();
        public static motion_delegate motion_handler_callback, motion_update_callback;
        public static bool single_step;
        //public static mainMotion mainmotion;
        public static void init()
        {
            //mainmotion = motion;
        }
        public static void ui_update_and_render()
        {
            motion_update_callback();
            motion_handler_callback();
        }
        public static void ui_updateC()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void ui_updateTehkan()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    if (Video.bitmapbase[Video.curbitmap][i] < 0x100)
                    {
                        red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                        green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                        blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                        Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                    }
                    else
                    {
                        int i1 = 1;
                    }
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    if (Video.bitmapbase[Video.curbitmap][i] < 0x100)
                    {
                        Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                    }
                    else
                    {
                        Video.bitmapcolor[i] = (int)Palette.entry_color[0];
                    }
                }
            }
        }
        public static void ui_updateN()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = ((Video.bitmapbaseN[Video.curbitmap][i] & 0xff0000) >> 16) * bright / 0xff;
                    green = ((Video.bitmapbaseN[Video.curbitmap][i] & 0xff00) >> 8) * bright / 0xff;
                    blue = (Video.bitmapbaseN[Video.curbitmap][i] & 0xff) * bright / 0xff;
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)(0xff000000 | (uint)Video.bitmapbaseN[Video.curbitmap][i]);
                }
            }
        }
        public static void ui_updateNa()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void ui_updateIGS011()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void ui_updatePGM()
        {
            int i;
            int red, green, blue;
            if (single_step || Mame.paused)
            {
                byte bright = 0xa7;
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    red = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff0000) >> 16) * bright / 0xff);
                    green = (int)(((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff00) >> 8) * bright / 0xff);
                    blue = (int)((Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]] & 0xff) * bright / 0xff);
                    Video.bitmapcolor[i] = (int)Palette.make_argb(0xff, red, green, blue);
                }
            }
            else
            {
                for (i = 0; i < Video.fullwidth * Video.fullheight; i++)
                {
                    Video.bitmapcolor[i] = (int)Palette.entry_color[Video.bitmapbase[Video.curbitmap][i]];
                }
            }
        }
        public static void handler_ingame()
        {
            //Mame.handle2 = GetForegroundWindow();
            //if (Mame.handle1 == Mame.handle2)
            //{
            //    Mame.is_foreground = true;
            //}
            //else
            //{
            //    Mame.is_foreground = false;
            //}
            Mame.is_foreground = true;
            bool is_paused = Mame.mame_is_paused();
            if (single_step)
            {
                Mame.mame_pause(true);
                single_step = false;
            }
            if (Mame.is_foreground)
            {
                if (Keyboard.IsPressed(Corekey.F3))
                {
                    cpurun();
                    Mame.playState = Mame.PlayState.PLAY_RESET;
                }
                if (Keyboard.IsTriggered(Corekey.F7))
                {
                    cpurun();
                    if (Keyboard.IsPressed(Corekey.LeftShift) || Keyboard.IsPressed(Corekey.RightShift))
                    {
                        Mame.playState = Mame.PlayState.PLAY_SAVE;
                    }
                    else
                    {
                        Mame.playState = Mame.PlayState.PLAY_LOAD;
                    }
                    return;
                }
                if (Keyboard.IsTriggered(Corekey.F8))
                {
                    cpurun();
                    if (Keyboard.IsPressed(Corekey.LeftShift) || Keyboard.IsPressed(Corekey.RightShift))
                    {
                        if (Mame.playState == Mame.PlayState.PLAY_RECORDRUNNING)
                        {
                            Mame.playState = Mame.PlayState.PLAY_RECORDEND;
                        }
                        else
                        {
                            Mame.playState = Mame.PlayState.PLAY_RECORDSTART;
                        }
                    }
                    else
                    {
                        Mame.playState = Mame.PlayState.PLAY_REPLAYSTART;
                    }
                    return;
                }
                if (Keyboard.IsTriggered(Corekey.P))
                {
                    if (is_paused && (Keyboard.IsPressed(Corekey.LeftShift) || Keyboard.IsPressed(Corekey.RightShift)))
                    {
                        single_step = true;
                        Mame.mame_pause(false);
                    }
                    else
                    {
                        Mame.mame_pause(!Mame.mame_is_paused());
                    }
                }
                if (Keyboard.IsTriggered(Corekey.F10))
                {
                    Keyboard.bF10 = true;
                    bool b1 = Video.global_throttle;
                    Video.global_throttle = !b1;
                }
            }
        }
        public static void cpurun()
        {
            m68000Motion.m68000State = m68000Motion.M68000State.M68000_RUN;
            Machine.FORM.m68000motion.mTx_tsslStatus = "run";
            z80Motion.z80State = z80Motion.Z80AState.Z80A_RUN;
            Machine.FORM.z80motion.mTx_tsslStatus = "run";
        }
        private static double ui_get_line_height()
        {
            int raw_font_pixel_height = 0x0b;
            int target_pixel_height = 0xff;
            double one_to_one_line_height;
            double scale_factor;
            one_to_one_line_height = (double)raw_font_pixel_height / (double)target_pixel_height;
            scale_factor = 1.0;
            return scale_factor * one_to_one_line_height;
        }
    }
}
