using cpu.z80;
using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace MAME.Core.Common
{
    public enum CPUState
    {
        NONE = 0,
        RUN,
        STEP,
        STEP2,
        STEP3,
        STOP,
    }
    public partial class z80Form
    {
        private mainForm _myParentForm;
        private Disassembler disassembler;
        private bool bLogNew;
        public static int iStatus;
        private int PPCTill;
        private ulong CyclesTill;
        private List<ushort> lPPC = new List<ushort>();

        #region
        string mTx_tbAF = string.Empty;
        string mTx_tbBC = string.Empty;
        string mTx_tbDE = string.Empty;
        string mTx_tbHL = string.Empty;
        string mTx_tbShadowAF = string.Empty;
        string mTx_tbShadowBC = string.Empty;
        string mTx_tbShadowDE = string.Empty;
        string mTx_tbShadowHL = string.Empty;
        string mTx_tbI  = string.Empty;
        string mTx_tbR  = string.Empty;
        string mTx_tbIX  = string.Empty;
        string mTx_tbIY  = string.Empty;
        string mTx_tbSP  = string.Empty;
        string mTx_tbRPC = string.Empty;
        string mTx_tbPPC = string.Empty;
        string mTx_tbR2  = string.Empty;
        string mTx_tbWZ  = string.Empty;
        string mTx_tbCycles = string.Empty;
        string mTx_tbDisassemble = string.Empty;

        public string mTx_tsslStatus = string.Empty;
        List<string> mTxList_tbResult = new List<string>();
        #endregion
        public enum Z80AState
        {
            Z80A_NONE = 0,
            Z80A_RUN,
            Z80A_STEP,
            Z80A_STEP2,
            Z80A_STEP3,
            Z80A_STOP,
        }
        public static Z80AState z80State, z80FState;
        public z80Form(mainForm form)
        {
            this._myParentForm = form;
            disassembler = new Disassembler();
            Disassembler.GenerateOpcodeSizes();
        }

        public void GetData()
        {
            string sDisassemble;
            mTx_tbAF = Z80A.zz1[0].RegisterAF.ToString("X4");
            mTx_tbBC = Z80A.zz1[0].RegisterBC.ToString("X4");
            mTx_tbDE = Z80A.zz1[0].RegisterDE.ToString("X4");
            mTx_tbHL = Z80A.zz1[0].RegisterHL.ToString("X4");
            mTx_tbShadowAF = Z80A.zz1[0].RegisterShadowAF.ToString("X4");
            mTx_tbShadowBC = Z80A.zz1[0].RegisterShadowBC.ToString("X4");
            mTx_tbShadowDE = Z80A.zz1[0].RegisterShadowDE.ToString("X4");
            mTx_tbShadowHL = Z80A.zz1[0].RegisterShadowHL.ToString("X4");
            mTx_tbI = Z80A.zz1[0].RegisterI.ToString("X2");
            mTx_tbR = Z80A.zz1[0].RegisterR.ToString("X2");
            mTx_tbIX = Z80A.zz1[0].RegisterIX.ToString("X4");
            mTx_tbIY = Z80A.zz1[0].RegisterIY.ToString("X4");
            mTx_tbSP = Z80A.zz1[0].RegisterSP.ToString("X4");
            mTx_tbRPC = Z80A.zz1[0].RegisterPC.ToString("X4");
            mTx_tbPPC = Z80A.zz1[0].PPC.ToString("X4");
            mTx_tbR2 = Z80A.zz1[0].RegisterR2.ToString("X2");
            mTx_tbWZ = Z80A.zz1[0].RegisterWZ.ToString("X4");
            mTx_tbCycles = Z80A.zz1[0].TotalExecutedCycles.ToString("X16");
            sDisassemble = disassembler.GetDisassembleInfo(Z80A.zz1[0].PPC);
            mTx_tbDisassemble = sDisassemble;
            mTxList_tbResult.Add(sDisassemble + "\r\n");
        }
        public void z80_start_debug()
        {
            if (bLogNew && lPPC.IndexOf(Z80A.zz1[0].PPC) < 0)
            {
                z80FState = z80State;
                z80State = Z80AState.Z80A_STOP;
                lPPC.Add(Z80A.zz1[0].PPC);
                mTxList_tbResult.Add(Z80A.zz1[0].PPC.ToString("X4") + ": " + disassembler.GetDisassembleInfo(Z80A.zz1[0].PPC) + "\r\n");
                z80State = z80FState;
            }
            if (z80State == Z80AState.Z80A_STEP2)
            {
                if (Z80A.zz1[0].PPC == PPCTill)
                {
                    z80State = Z80AState.Z80A_STOP;
                }
            }
            if (z80State == Z80AState.Z80A_STEP3)
            {
                if (Z80A.zz1[0].TotalExecutedCycles >= CyclesTill)
                {
                    z80State = Z80AState.Z80A_STOP;
                }
            }
            if (z80State == Z80AState.Z80A_STOP)
            {
                GetData();
                mTx_tsslStatus = "z80 stop";
            }
            while (z80State == Z80AState.Z80A_STOP)
            {

            }
        }
        public void z80_stop_debug()
        {
            if (z80State == Z80AState.Z80A_STEP)
            {
                z80State = Z80AState.Z80A_STOP;
                mTx_tsslStatus = "z80 stop";
            }
        }
    }
}