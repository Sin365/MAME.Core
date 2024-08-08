using cpu.m68000;
using System;
using System.Collections.Generic;

namespace MAME.Core.Motion
{
    public class M68000Motion
    {
        private string[] sde6 = new string[1] { "," }, sde7 = new string[1] { ";" }, sde9 = new string[1] { "$" }, sde10 = new string[] { "+" };
        private bool bLogNew, bNew;
        public static int iStatus, iRAddress, iWAddress, iROp, iWOp, iValue;
        private int PPCTill, PPC, Addr;
        private ulong CyclesTill, TotalExecutedCycles;
        private List<int> lsAddr = new List<int>();
        private List<int> lsPPC = new List<int>();


        #region
        string[] mTxList_tbDs;
        string[] mTxList_tbAs;
        Boolean[] mBList_cbDs;
        Boolean[] mBList_cbAs;
        List<Boolean> mBList_lsCB;

        string mTx_tbPPC = string.Empty;
        string mTx_tbOP = string.Empty;
        Boolean b_cbS = false;
        Boolean b_cbM = false;
        Boolean b_cbX = false;
        Boolean b_cbN = false;
        Boolean b_cbZ = false;
        Boolean b_cbV = false;
        Boolean b_cbC = false;
        Boolean b_cbPC = false;
        Boolean b_cbTotal = false;
        Boolean b_cbLog = false;
        string mTx_tbIML = string.Empty;
        string mTx_tbUSP = string.Empty;
        string mTx_tbSSP = string.Empty;
        string mTx_tbCycles = string.Empty;
        string mTx_tbPC = string.Empty;
        string mTx_tbDisassemble = string.Empty;
        List<string> mTxList_tbResult = new List<string>();
        public string mTx_tsslStatus = string.Empty;
        #endregion

        public enum M68000State
        {
            M68000_NONE = 0,
            M68000_RUN,
            M68000_STEP,
            M68000_STEP2,
            M68000_STEP3,
            M68000_STOP,
        }
        public static M68000State m68000State, m68000FState;
        public M68000Motion()
        {
            int i;
            mTxList_tbDs = new string[8];
            mTxList_tbAs = new string[8];
            mBList_cbDs = new bool[8];
            mBList_cbAs = new bool[8];
            for (i = 0; i < 8; i++)
            {
                mTxList_tbDs[i] = string.Empty;
                mBList_cbDs[i] = false;
                mTxList_tbAs[i] = string.Empty;
                mBList_cbAs[i] = false;
            }

            b_cbPC = false;
            b_cbTotal = false;
            mBList_lsCB = new List<bool>();
            for (i = 0; i < 8; i++)
            {
                mBList_lsCB.Add(mBList_cbDs[i]);
                mBList_lsCB.Add(mBList_cbAs[i]);
            }
            mBList_lsCB.Add(b_cbPC);
        }
        public void GetData()
        {
            int i;
            string sDisassemble, sDisassemble2 = "";
            for (i = 0; i < 8; i++)
            {
                mTxList_tbDs[i] = MC68000.m1.D[i].u32.ToString("X8");
                mTxList_tbAs[i] = MC68000.m1.A[i].u32.ToString("X8");
            }
            mTx_tbPPC = MC68000.m1.PPC.ToString("X6");
            mTx_tbOP = MC68000.m1.op.ToString("X4");
            b_cbS = MC68000.m1.S;
            b_cbM = MC68000.m1.M;
            b_cbX = MC68000.m1.X;
            b_cbN = MC68000.m1.N;
            b_cbZ = MC68000.m1.Z;
            b_cbV = MC68000.m1.V;
            b_cbC = MC68000.m1.C;
            mTx_tbIML = MC68000.m1.InterruptMaskLevel.ToString();
            mTx_tbUSP = MC68000.m1.usp.ToString("X8");
            mTx_tbSSP = MC68000.m1.ssp.ToString("X8");
            mTx_tbCycles = MC68000.m1.TotalExecutedCycles.ToString("X16");
            mTx_tbPC = MC68000.m1.PC.ToString("X6");
            sDisassemble = MC68000.m1.Disassemble(MC68000.m1.PPC).ToString();
            mTx_tbDisassemble = sDisassemble;
            sDisassemble2 = sDisassemble;
            foreach (bool cb in mBList_lsCB)
            {
                if (cb)
                {
                    //sDisassemble2 += " " + cb + cb.TB.Text;
                    sDisassemble2 += " " + cb + cb;
                }
            }
            if (b_cbTotal)
            {
                sDisassemble2 = MC68000.m1.TotalExecutedCycles.ToString("X") + " " + sDisassemble2;
            }
            mTxList_tbResult.Add(sDisassemble2 + "\r\n");
        }
        public void m68000_start_debug()
        {
            if (bLogNew && lsPPC.IndexOf(MC68000.m1.PPC) < 0)
            {
                m68000FState = m68000State;
                m68000State = M68000State.M68000_STOP;
                lsPPC.Add(MC68000.m1.PPC);
                mTxList_tbResult.Add(MC68000.m1.Disassemble(MC68000.m1.PPC).ToString() + "\r\n");
                m68000State = m68000FState;
            }
            PPC = MC68000.m1.PPC;
            TotalExecutedCycles = MC68000.m1.TotalExecutedCycles;
            if (iStatus == 1)
            {
                iStatus = 0;
            }
            if (m68000State == M68000State.M68000_STEP2)
            {
                if (MC68000.m1.PPC == PPCTill)
                {
                    m68000State = M68000State.M68000_STOP;
                }
            }
            if (m68000State == M68000State.M68000_STEP3)
            {
                if (MC68000.m1.TotalExecutedCycles >= CyclesTill)
                {
                    m68000State = M68000State.M68000_STOP;
                }
            }
            if (b_cbLog == true && (m68000State == M68000State.M68000_STEP2 || m68000State == M68000State.M68000_STEP3))
            {
                GetData();
            }
            if (m68000State == M68000State.M68000_STOP)
            {
                GetData();
                mTx_tsslStatus = "m68000 stop";
            }
            while (m68000State == M68000State.M68000_STOP)
            {

            }
        }
        public void m68000_stop_debug()
        {
            if (iStatus == 1)
            {
                GetData();
                m68000State = M68000State.M68000_STOP;
                mTx_tsslStatus = "m68000 stop";
                iStatus = 2;
            }
            if (m68000State == M68000State.M68000_STEP)
            {
                m68000State = M68000State.M68000_STOP;
                mTx_tsslStatus = "m68000 stop";
            }
            if (iStatus == 0)
            {
                /*if(Memory.mainram[0xd1b]==0x05)
                {
                    iStatus = 1;
                    GetData();
                    m68000State = M68000State.M68000_STOP;
                    tsslStatus.Text = "m68000 stop";
                }*/
            }
        }
    }
}
