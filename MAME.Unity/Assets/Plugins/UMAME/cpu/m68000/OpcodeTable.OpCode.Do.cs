namespace cpu.m68000
{
    partial class MC68000
    {
        /// <summary>
        /// 重写的68000指令调度
        /// </summary>
        /// <param name="op"></param>
        void DoOpCode(ushort op)
        {
            MC68000Code opType = MC68000CodeArr[op];
            switch (opType)
            {
                case MC68000Code.ORI: ORI(); break;
                case MC68000Code.ILL: ILL(); break;
                case MC68000Code.ORI_CCR: ORI_CCR(); break;
                case MC68000Code.ORI_SR: ORI_SR(); break;
                case MC68000Code.BTSTr: BTSTr(); break;
                case MC68000Code.MOVEP: MOVEP(); break;
                case MC68000Code.BCHGr: BCHGr(); break;
                case MC68000Code.BCLRr: BCLRr(); break;
                case MC68000Code.BSETr: BSETr(); break;
                case MC68000Code.ANDI: ANDI(); break;
                case MC68000Code.ANDI_CCR: ANDI_CCR(); break;
                case MC68000Code.ANDI_SR: ANDI_SR(); break;
                case MC68000Code.SUBI: SUBI(); break;
                case MC68000Code.ADDI: ADDI(); break;
                case MC68000Code.BTSTi: BTSTi(); break;
                case MC68000Code.BCHGi: BCHGi(); break;
                case MC68000Code.BCLRi: BCLRi(); break;
                case MC68000Code.BSETi: BSETi(); break;
                case MC68000Code.EORI: EORI(); break;
                case MC68000Code.EORI_CCR: EORI_CCR(); break;
                case MC68000Code.EORI_SR: EORI_SR(); break;
                case MC68000Code.CMPI: CMPI(); break;
                case MC68000Code.MOVE: MOVE(); break;
                case MC68000Code.MOVEA: MOVEA(); break;
                case MC68000Code.NEGX: NEGX(); break;
                case MC68000Code.MOVEfSR: MOVEfSR(); break;
                case MC68000Code.CHK: CHK(); break;
                case MC68000Code.LEA: LEA(); break;
                case MC68000Code.CLR: CLR(); break;
                case MC68000Code.NEG: NEG(); break;
                case MC68000Code.MOVECCR: MOVECCR(); break;
                case MC68000Code.NOT: NOT(); break;
                case MC68000Code.MOVEtSR: MOVEtSR(); break;
                case MC68000Code.NBCD: NBCD(); break;
                case MC68000Code.SWAP: SWAP(); break;
                case MC68000Code.PEA: PEA(); break;
                case MC68000Code.EXT: EXT(); break;
                case MC68000Code.MOVEM0: MOVEM0(); break;
                case MC68000Code.TST: TST(); break;
                case MC68000Code.TAS: TAS(); break;
                case MC68000Code.ILLEGAL: ILLEGAL(); break;
                case MC68000Code.MOVEM1: MOVEM1(); break;
                case MC68000Code.TRAP: TRAP(); break;
                case MC68000Code.LINK: LINK(); break;
                case MC68000Code.UNLK: UNLK(); break;
                case MC68000Code.MOVEUSP: MOVEUSP(); break;
                case MC68000Code.RESET: RESET(); break;
                case MC68000Code.NOP: NOP(); break;
                case MC68000Code.STOP: STOP(); break;
                case MC68000Code.RTE: RTE(); break;
                case MC68000Code.RTS: RTS(); break;
                case MC68000Code.TRAPV: TRAPV(); break;
                case MC68000Code.RTR: RTR(); break;
                case MC68000Code.JSR: JSR(); break;
                case MC68000Code.JMP: JMP(); break;
                case MC68000Code.ADDQ: ADDQ(); break;
                case MC68000Code.Scc: Scc(); break;
                case MC68000Code.DBcc: DBcc(); break;
                case MC68000Code.SUBQ: SUBQ(); break;
                case MC68000Code.BRA: BRA(); break;
                case MC68000Code.BSR: BSR(); break;
                case MC68000Code.Bcc: Bcc(); break;
                case MC68000Code.MOVEQ: MOVEQ(); break;
                case MC68000Code.OR0: OR0(); break;
                case MC68000Code.DIVU: DIVU(); break;
                case MC68000Code.SBCD0: SBCD0(); break;
                case MC68000Code.SBCD1: SBCD1(); break;
                case MC68000Code.OR1: OR1(); break;
                case MC68000Code.DIVS: DIVS(); break;
                case MC68000Code.SUB0: SUB0(); break;
                case MC68000Code.SUBA: SUBA(); break;
                case MC68000Code.SUBX0: SUBX0(); break;
                case MC68000Code.SUBX1: SUBX1(); break;
                case MC68000Code.SUB1: SUB1(); break;
                case MC68000Code.CMP: CMP(); break;
                case MC68000Code.CMPA: CMPA(); break;
                case MC68000Code.EOR: EOR(); break;
                case MC68000Code.CMPM: CMPM(); break;
                case MC68000Code.AND0: AND0(); break;
                case MC68000Code.MULU: MULU(); break;
                case MC68000Code.ABCD0: ABCD0(); break;
                case MC68000Code.ABCD1: ABCD1(); break;
                case MC68000Code.AND1: AND1(); break;
                case MC68000Code.EXGdd: EXGdd(); break;
                case MC68000Code.EXGaa: EXGaa(); break;
                case MC68000Code.EXGda: EXGda(); break;
                case MC68000Code.MULS: MULS(); break;
                case MC68000Code.ADD0: ADD0(); break;
                case MC68000Code.ADDA: ADDA(); break;
                case MC68000Code.ADDX0: ADDX0(); break;
                case MC68000Code.ADDX1: ADDX1(); break;
                case MC68000Code.ADD1: ADD1(); break;
                case MC68000Code.ASRd: ASRd(); break;
                case MC68000Code.LSRd: LSRd(); break;
                case MC68000Code.ROXRd: ROXRd(); break;
                case MC68000Code.RORd: RORd(); break;
                case MC68000Code.ASRd0: ASRd0(); break;
                case MC68000Code.ASLd: ASLd(); break;
                case MC68000Code.LSLd: LSLd(); break;
                case MC68000Code.ROXLd: ROXLd(); break;
                case MC68000Code.ROLd: ROLd(); break;
                case MC68000Code.ASLd0: ASLd0(); break;
                case MC68000Code.LSRd0: LSRd0(); break;
                case MC68000Code.LSLd0: LSLd0(); break;
                case MC68000Code.ROXRd0: ROXRd0(); break;
                case MC68000Code.ROXLd0: ROXLd0(); break;
                case MC68000Code.RORd0: RORd0(); break;
                case MC68000Code.ROLd0: ROLd0(); break;

            }
        }
    }
}