using cpu.m6809;
using mame;
using System.Collections.Generic;

namespace MAME.Core.Common
{

    public partial class m6809Motion
    {
        //private Disassembler disassembler;
        private bool bLogNew;
        public static int iStatus;
        private int PPCTill;
        private ulong CyclesTill;
        private List<ushort> lPPC = new List<ushort>();
        public static CPUState m6809State, m6809FState;

        #region
        public List<string> tbResult = new List<string>();
        public string tsslStatus = string.Empty;
        public string tbD = string.Empty;
        public string tbDp = string.Empty;
        public string tbU = string.Empty;
        public string tbS = string.Empty;
        public string tbX = string.Empty;
        public string tbY = string.Empty;
        public string tbCc = string.Empty;
        public string tbIrqstate0 = string.Empty;
        public string tbIrqstate1 = string.Empty;
        public string tbIntstate = string.Empty;
        public string tbNmistate = string.Empty;
        public string tbPPC = string.Empty;
        public string tbCycles = string.Empty;
        public string tbDisassemble = string.Empty;
        #endregion

        public m6809Motion()
        {
        }
        public void GetData()
        {
            int reg, offset;
            reg = M6809.mm1[0].PPC.LowWord / 0x2000;
            offset = M6809.mm1[0].PPC.LowWord & 0x1fff;
            tbD = M6809.mm1[0].D.LowWord.ToString("X4");
            tbDp = M6809.mm1[0].DP.LowWord.ToString("X4");
            tbU = M6809.mm1[0].U.LowWord.ToString("X4");
            tbS = M6809.mm1[0].S.LowWord.ToString("X4");
            tbX = M6809.mm1[0].X.LowWord.ToString("X4");
            tbY = M6809.mm1[0].Y.LowWord.ToString("X4");
            tbCc = M6809.mm1[0].CC.ToString("X2");
            tbIrqstate0 = M6809.mm1[0].irq_state[0].ToString("X2");
            tbIrqstate1 = M6809.mm1[0].irq_state[1].ToString("X2");
            tbIntstate = M6809.mm1[0].int_state.ToString("X2");
            tbNmistate = M6809.mm1[0].nmi_state.ToString("X2");
            tbPPC = (Namcos1.user1rom_offset[0, reg] + offset).ToString("X6");
            tbCycles = M6809.mm1[0].TotalExecutedCycles.ToString("X16");
            tbDisassemble = M6809.mm1[0].m6809_dasm(M6809.mm1[0].PPC.LowWord);

        }
        public void m6809_start_debug()
        {
            if (bLogNew && lPPC.IndexOf(M6809.mm1[0].PPC.LowWord) < 0)
            {
                m6809FState = m6809State;
                m6809State = CPUState.STOP;
                lPPC.Add(M6809.mm1[0].PPC.LowWord);
                tbResult.Add(M6809.mm1[0].PPC.LowWord.ToString("X4") + ": ");
                m6809State = m6809FState;
            }
            if (m6809State == CPUState.STEP2)
            {
                if (M6809.mm1[0].PPC.LowWord == PPCTill)
                {
                    m6809State = CPUState.STOP;
                }
            }
            if (m6809State == CPUState.STEP3)
            {
                if (M6809.mm1[0].TotalExecutedCycles >= CyclesTill)
                {
                    m6809State = CPUState.STOP;
                }
            }
            if (m6809State == CPUState.STOP)
            {
                GetData();
                tsslStatus = "m6809 stop";
            }
            while (m6809State == CPUState.STOP)
            {
                Video.video_frame_update();
                //??
                //Application.DoEvents();
            }
        }
        public void m6809_stop_debug()
        {
            if (m6809State == CPUState.STEP)
            {
                m6809State = CPUState.STOP;
                tsslStatus = "m6809 stop";
            }
        }
    }
}
