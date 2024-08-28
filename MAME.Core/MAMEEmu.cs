using System;
using System.Collections.Generic;

namespace MAME.Core
{
    public class MAMEEmu : IDisposable
    {
        MameMainMotion mameMainMotion;
        public MAMEEmu()
        {
            mameMainMotion = new MameMainMotion();
        }

        public bool bRom => mameMainMotion.bRom;

        public void Init(
            string RomDir,
            ILog ilog,
            IResources iRes,
            IVideoPlayer ivp,
            ISoundPlayer isp,
            IKeyboard ikb,
            IMouse imou
            ) => mameMainMotion.Init(RomDir, ilog, iRes, ivp, isp, ikb, imou);
        public Dictionary<string, RomInfo> GetGameList() => mameMainMotion.GetGameList();
        public void LoadRom(string Name) => mameMainMotion.LoadRom(Name);
        public void GetGameScreenSize(out int _width, out int _height, out IntPtr _framePtr) => mameMainMotion.GetGameScreenSize(out _width, out _height, out _framePtr);
        public void StartGame() => mameMainMotion.StartGame();
        public void StopGame() => mameMainMotion.StopGame();

        public void Dispose()
        {
            mameMainMotion.StopGame();
            mameMainMotion = null;
            GC.Collect();
        }
    }
}
