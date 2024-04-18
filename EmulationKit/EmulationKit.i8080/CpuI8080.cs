using EmulationKit.Abstractions;
using EmulationKit.i8080.Commands;
using EmulationKit.i8080.Commands.Call;
using EmulationKit.i8080.Commands.Jp;
using EmulationKit.i8080.Commands.Logic;
using EmulationKit.i8080.Commands.Ret;
using System;
using System.Collections.Generic;

namespace EmulationKit.i8080
{
    public class CpuI8080 : ICpu
    {

        internal long Cycle { get; set; }

        //Registers
        internal ushort SP { get; set; }
        internal ushort PC { get; set; }

        internal byte A { get; set; }
        internal byte F
        {
            get {
                return (byte)((FlagS ? 0b10000000 : 0) |
                    (FlagZ ? 0b01000000 : 0) |
                    (FlagAC ? 0b00010000 : 0) |
                    (FlagP ? 0b00000100 : 0) |
                    0b00000010 |
                    (FlagCY ? 0b00000001 : 0));
            }
            set
            {
                FlagS = (value & 0b10000000) == 0b10000000;
                FlagZ = (value & 0b01000000) == 0b01000000;
                FlagAC = (value & 0b00010000) == 0b00010000;
                FlagP = (value & 0b00000100) == 0b00000100;
                FlagCY = (value & 0b00000001) == 0b00000001;
            }
        }
        internal byte B { get; set; }
        internal byte C { get; set; }
        internal byte D { get; set; }
        internal byte E { get; set; }
        internal byte H { get; set; }
        internal byte L { get; set; }

        internal ushort AF
        {
            get { return (ushort)(A * 256 + F); }
            set
            {
                A = (byte)(value / 256);
                F = (byte)(value % 256);
            }

        }
        internal ushort BC 
        { 
            get { return (ushort)(B * 256 + C); } 
            set 
            { 
                B = (byte)(value / 256);
                C = (byte)(value % 256);
            }
        }

        internal ushort DE
        {
            get { return (ushort)(D * 256 + E); }
            set
            {
                D = (byte)(value / 256);
                E = (byte)(value % 256);
            }
        }

        internal ushort HL
        {
            get { return (ushort)(H * 256 + L);  }
            set
            {
                H = (byte)(value / 256);
                L = (byte)(value % 256);
            }
        }


        //Flags
        internal bool FlagS { get; set; } //7
        internal bool FlagZ { get; set; } //6
        internal bool FlagAC { get; set; } //4
        internal bool FlagP { get; set; } //2
        internal bool FlagCY { get; set; } //0
        internal bool Halted { get; set; }
        internal bool InterruptsEnabled { get; set; } = true;

        public long CommandCounterStart { get; set; }

        private readonly IMemory _memory;
        private readonly IPorts _ports;


        public CpuI8080(IMemory memory, IPorts ports)
        {
            _memory = memory;
            _ports = ports;
            _cpuCommands = new Dictionary<byte, ICpuCommand>()
                {
                    {OpcodesI8080.NOP, new Nop(1, IncPC, IncCycle) },
                    {OpcodesI8080.LXI_B_d16, new Lxi(_memory, 10, IncPC, IncCycle, GetPC, SetBC) },
                    {OpcodesI8080.STAX_B, new Stax(7, IncPC, IncCycle, GetA, (val) => {_memory.Write8(BC, val); }  ) },
                    {OpcodesI8080.INX_B, new Inx(5, IncPC, IncCycle, GetBC, SetBC) },
                    {OpcodesI8080.INR_B, new Inr(5, IncPC, IncCycle, GetB, SetB, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_B, new Dcr(5, IncPC, IncCycle, GetB, SetB, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_B_d8, new Mvi(_memory, 7, IncPC, IncCycle, GetPC, SetB) },
                    {OpcodesI8080.RLC, new Rlc(4, IncPC, IncCycle, GetA, SetA, SetCY) },
                    {OpcodesI8080.DAD_B, new Dad(10, IncPC, IncCycle, GetHL, GetBC, SetHL, SetCY) },
                    {OpcodesI8080.LDAX_B, new Ldax(_memory, 7, IncPC, IncCycle, GetBC, SetA) },
                    {OpcodesI8080.DCX_B, new Dcx(5, IncPC, IncCycle, GetBC, SetBC) },
                    {OpcodesI8080.INR_C, new Inr(5, IncPC, IncCycle, GetC, SetC, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_C, new Dcr(5, IncPC, IncCycle, GetC, SetC, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_C_d8, new Mvi(_memory, 7, IncPC, IncCycle, GetPC, SetC) },
                    {OpcodesI8080.RRC, new Rrc(4, IncPC, IncCycle, GetA, SetA, SetCY) },

                    {OpcodesI8080.LXI_D_d16, new Lxi(_memory, 10, IncPC, IncCycle, GetPC, SetDE) },
                    {OpcodesI8080.STAX_D, new Stax(7, IncPC, IncCycle, GetA, (val) => {_memory.Write8(DE, val); }  ) },
                    {OpcodesI8080.INX_D, new Inx(5, IncPC, IncCycle, GetDE, SetDE) },
                    {OpcodesI8080.INR_D, new Inr(5, IncPC, IncCycle, GetD, SetD, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_D, new Dcr(5, IncPC, IncCycle, GetD, SetD, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_D_d8, new Mvi(_memory, 7, IncPC, IncCycle, GetPC, SetD) },
                    {OpcodesI8080.RAL, new Ral(4,IncPC, IncCycle, GetA, SetA, GetCY, SetCY) },
                    {OpcodesI8080.DAD_D, new Dad(10, IncPC, IncCycle, GetHL, GetDE, SetHL, SetCY) },
                    {OpcodesI8080.LDAX_D, new Ldax(_memory, 7, IncPC, IncCycle, GetDE, SetA) },
                    {OpcodesI8080.DCX_D, new Dcx(5, IncPC, IncCycle, GetDE, SetDE) },
                    {OpcodesI8080.INR_E, new Inr(5, IncPC, IncCycle, GetE, SetE, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_E, new Dcr(5, IncPC, IncCycle, GetE, SetE, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_E_d8, new Mvi(_memory, 7, IncPC, IncCycle, GetPC, SetE) },
                    {OpcodesI8080.RAR, new Rar(4, IncPC, IncCycle, GetA, SetA, GetCY, SetCY) },

                    {OpcodesI8080.LXI_H_d16, new Lxi(_memory, 10, IncPC, IncCycle, GetPC, SetHL) },
                    {OpcodesI8080.SHLD_a16, new Shld(_memory, 16, IncPC, IncCycle, GetPC, GetHL) },
                    {OpcodesI8080.INX_H, new Inx(5, IncPC, IncCycle, GetHL, SetHL) },
                    {OpcodesI8080.INR_H, new Inr(5, IncPC, IncCycle, GetH, SetH, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_H, new Dcr(5, IncPC, IncCycle, GetH, SetH, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_H_d8, new Mvi(_memory, 7, IncPC, IncCycle, GetPC, SetH) },
                    {OpcodesI8080.DAA, new Daa(4, IncPC, IncCycle, GetA, SetA, GetCY, SetCY, GetAC, SetAC, SetS, SetZ, SetP) },
                    {OpcodesI8080.DAD_H, new Dad(10, IncPC, IncCycle, GetHL, GetHL, SetHL, SetCY) },
                    {OpcodesI8080.LHLD_a16, new Lhld(_memory, 16, IncPC, IncCycle, GetPC, SetHL) },
                    {OpcodesI8080.DCX_H, new Dcx(5, IncPC, IncCycle, GetHL, SetHL) },
                    {OpcodesI8080.INR_L, new Inr(5, IncPC, IncCycle, GetL, SetL, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_L, new Dcr(5, IncPC, IncCycle, GetL, SetL, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_L_d8, new Mvi(_memory, 7, IncPC, IncCycle, GetPC, SetL) },
                    {OpcodesI8080.CMA, new Cma(4, IncPC, IncCycle, GetA, SetA) },
                    {OpcodesI8080.LXI_SP_d16, new Lxi(_memory, 10, IncPC, IncCycle, GetPC, SetSP) },
                    {OpcodesI8080.STA_a16, new Sta(_memory, 13, IncPC, GetPC, IncCycle, GetA)},
                    {OpcodesI8080.INX_SP, new Inx(5, IncPC, IncCycle, GetSP, SetSP) },
                    {OpcodesI8080.INR_M, new Inr(10, IncPC, IncCycle, GetM, SetM, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_M, new Dcr(5, IncPC, IncCycle, GetM, SetM, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_M_d8, new Mvi(_memory, 10, IncPC, IncCycle, GetPC, SetM) },
                    {OpcodesI8080.STC, new Stc(4, IncPC, IncCycle, SetCY) },
                    {OpcodesI8080.DAD_SP, new Dad(10, IncPC, IncCycle, GetHL, GetSP, SetHL, SetCY) },
                    {OpcodesI8080.LDA_a16, new Lda(_memory, 13, IncPC, IncCycle, GetPC, SetA) },
                    {OpcodesI8080.DCX_SP, new Dcx(5, IncPC, IncCycle, GetSP, SetSP) },
                    {OpcodesI8080.INR_A, new Inr(5, IncPC, IncCycle, GetA, SetA, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.DCR_A, new Dcr(5, IncPC, IncCycle, GetA, SetA, SetS, SetZ, SetP, SetAC ) },
                    {OpcodesI8080.MVI_A_d8, new Mvi(_memory, 7, IncPC, IncCycle, GetPC, SetA) },
                    {OpcodesI8080.CMC, new Cmc(4, IncPC, IncCycle, GetCY, SetCY) },
                    {OpcodesI8080.MOV_B_B, new Mov(5, IncPC, IncCycle, GetB, SetB) },
                    {OpcodesI8080.MOV_B_C, new Mov(5, IncPC, IncCycle, GetC, SetB) },
                    {OpcodesI8080.MOV_B_D, new Mov(5, IncPC, IncCycle, GetD, SetB) },
                    {OpcodesI8080.MOV_B_E, new Mov(5, IncPC, IncCycle, GetE, SetB) },
                    {OpcodesI8080.MOV_B_H, new Mov(5, IncPC, IncCycle, GetH, SetB) },
                    {OpcodesI8080.MOV_B_L, new Mov(5, IncPC, IncCycle, GetL, SetB) },
                    {OpcodesI8080.MOV_B_M, new Mov(7, IncPC, IncCycle, GetM, SetB) },
                    {OpcodesI8080.MOV_B_A, new Mov(5, IncPC, IncCycle, GetA, SetB) },
                    {OpcodesI8080.MOV_C_B, new Mov(5, IncPC, IncCycle, GetB, SetC) },
                    {OpcodesI8080.MOV_C_C, new Mov(5, IncPC, IncCycle, GetC, SetC) },
                    {OpcodesI8080.MOV_C_D, new Mov(5, IncPC, IncCycle, GetD, SetC) },
                    {OpcodesI8080.MOV_C_E, new Mov(5, IncPC, IncCycle, GetE, SetC) },
                    {OpcodesI8080.MOV_C_H, new Mov(5, IncPC, IncCycle, GetH, SetC) },
                    {OpcodesI8080.MOV_C_L, new Mov(5, IncPC, IncCycle, GetL, SetC) },
                    {OpcodesI8080.MOV_C_M, new Mov(7, IncPC, IncCycle, GetM, SetC) },
                    {OpcodesI8080.MOV_C_A, new Mov(5, IncPC, IncCycle, GetA, SetC) },

                    {OpcodesI8080.MOV_D_B, new Mov(5, IncPC, IncCycle, GetB, SetD) },
                    {OpcodesI8080.MOV_D_C, new Mov(5, IncPC, IncCycle, GetC, SetD) },
                    {OpcodesI8080.MOV_D_D, new Mov(5, IncPC, IncCycle, GetD, SetD) },
                    {OpcodesI8080.MOV_D_E, new Mov(5, IncPC, IncCycle, GetE, SetD) },
                    {OpcodesI8080.MOV_D_H, new Mov(5, IncPC, IncCycle, GetH, SetD) },
                    {OpcodesI8080.MOV_D_L, new Mov(5, IncPC, IncCycle, GetL, SetD) },
                    {OpcodesI8080.MOV_D_M, new Mov(7, IncPC, IncCycle, GetM, SetD) },
                    {OpcodesI8080.MOV_D_A, new Mov(5, IncPC, IncCycle, GetA, SetD) },
                    {OpcodesI8080.MOV_E_B, new Mov(5, IncPC, IncCycle, GetB, SetE) },
                    {OpcodesI8080.MOV_E_C, new Mov(5, IncPC, IncCycle, GetC, SetE) },
                    {OpcodesI8080.MOV_E_D, new Mov(5, IncPC, IncCycle, GetD, SetE) },
                    {OpcodesI8080.MOV_E_E, new Mov(5, IncPC, IncCycle, GetE, SetE) },
                    {OpcodesI8080.MOV_E_H, new Mov(5, IncPC, IncCycle, GetH, SetE) },
                    {OpcodesI8080.MOV_E_M, new Mov(7, IncPC, IncCycle, GetL, SetE) },
                    {OpcodesI8080.MOV_E_L, new Mov(5, IncPC, IncCycle, GetM, SetE) },
                    {OpcodesI8080.MOV_E_A, new Mov(5, IncPC, IncCycle, GetA, SetE) },

                    {OpcodesI8080.MOV_H_B, new Mov(5, IncPC, IncCycle, GetB, SetH) },
                    {OpcodesI8080.MOV_H_C, new Mov(5, IncPC, IncCycle, GetC, SetH) },
                    {OpcodesI8080.MOV_H_D, new Mov(5, IncPC, IncCycle, GetD, SetH) },
                    {OpcodesI8080.MOV_H_E, new Mov(5, IncPC, IncCycle, GetE, SetH) },
                    {OpcodesI8080.MOV_H_H, new Mov(5, IncPC, IncCycle, GetH, SetH) },
                    {OpcodesI8080.MOV_H_L, new Mov(5, IncPC, IncCycle, GetL, SetH) },
                    {OpcodesI8080.MOV_H_M, new Mov(7, IncPC, IncCycle, GetM, SetH) },
                    {OpcodesI8080.MOV_H_A, new Mov(5, IncPC, IncCycle, GetA, SetH) },
                    {OpcodesI8080.MOV_L_B, new Mov(5, IncPC, IncCycle, GetB, SetL) },
                    {OpcodesI8080.MOV_L_C, new Mov(5, IncPC, IncCycle, GetC, SetL) },
                    {OpcodesI8080.MOV_L_D, new Mov(5, IncPC, IncCycle, GetD, SetL) },
                    {OpcodesI8080.MOV_L_E, new Mov(5, IncPC, IncCycle, GetE, SetL) },
                    {OpcodesI8080.MOV_L_H, new Mov(5, IncPC, IncCycle, GetH, SetL) },
                    {OpcodesI8080.MOV_L_L, new Mov(5, IncPC, IncCycle, GetL, SetL) },
                    {OpcodesI8080.MOV_L_M, new Mov(7, IncPC, IncCycle, GetM, SetL) },
                    {OpcodesI8080.MOV_L_A, new Mov(5, IncPC, IncCycle, GetA, SetL) },

                    {OpcodesI8080.MOV_M_B, new Mov(7, IncPC, IncCycle, GetB, SetM) },
                    {OpcodesI8080.MOV_M_C, new Mov(7, IncPC, IncCycle, GetC, SetM) },
                    {OpcodesI8080.MOV_M_D, new Mov(7, IncPC, IncCycle, GetD, SetM) },
                    {OpcodesI8080.MOV_M_E, new Mov(7, IncPC, IncCycle, GetE, SetM) },
                    {OpcodesI8080.MOV_M_H, new Mov(7, IncPC, IncCycle, GetH, SetM) },
                    {OpcodesI8080.MOV_M_L, new Mov(7, IncPC, IncCycle, GetL, SetM) },
                    {OpcodesI8080.HLT, new Hlt(7, IncPC, IncCycle, SetHalted) },
                    {OpcodesI8080.MOV_M_A, new Mov(7, IncPC, IncCycle, GetA, SetM) },
                    {OpcodesI8080.MOV_A_B, new Mov(5, IncPC, IncCycle, GetB, SetA) },
                    {OpcodesI8080.MOV_A_C, new Mov(5, IncPC, IncCycle, GetC, SetA) },
                    {OpcodesI8080.MOV_A_D, new Mov(5, IncPC, IncCycle, GetD, SetA) },
                    {OpcodesI8080.MOV_A_E, new Mov(5, IncPC, IncCycle, GetE, SetA) },
                    {OpcodesI8080.MOV_A_H, new Mov(5, IncPC, IncCycle, GetH, SetA) },
                    {OpcodesI8080.MOV_A_L, new Mov(5, IncPC, IncCycle, GetL, SetA) },
                    {OpcodesI8080.MOV_A_M, new Mov(7, IncPC, IncCycle, GetM, SetA) },
                    {OpcodesI8080.MOV_A_A, new Mov(5, IncPC, IncCycle, GetA, SetA) },

                    {OpcodesI8080.ADD_B, new Add(4, IncPC, IncCycle, 1, GetA, GetB, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADD_C, new Add(4, IncPC, IncCycle, 1, GetA, GetC, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADD_D, new Add(4, IncPC, IncCycle, 1, GetA, GetD, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADD_E, new Add(4, IncPC, IncCycle, 1, GetA, GetE, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADD_H, new Add(4, IncPC, IncCycle, 1, GetA, GetH, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADD_L, new Add(4, IncPC, IncCycle, 1, GetA, GetL, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADD_M, new Add(7, IncPC, IncCycle, 1, GetA, GetM, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADD_A, new Add(4, IncPC, IncCycle, 1, GetA, GetA, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ADC_B, new Adc(4, IncPC, IncCycle, 1, GetA, GetB, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ADC_C, new Adc(4, IncPC, IncCycle, 1, GetA, GetC, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ADC_D, new Adc(4, IncPC, IncCycle, 1, GetA, GetD, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ADC_E, new Adc(4, IncPC, IncCycle, 1, GetA, GetE, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ADC_H, new Adc(4, IncPC, IncCycle, 1, GetA, GetH, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ADC_L, new Adc(4, IncPC, IncCycle, 1, GetA, GetL, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ADC_M, new Adc(7, IncPC, IncCycle, 1, GetA, GetM, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ADC_A, new Adc(4, IncPC, IncCycle, 1, GetA, GetA, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},

                    {OpcodesI8080.SUB_B, new Sub(4, IncPC, IncCycle, GetA, GetB, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SUB_C, new Sub(4, IncPC, IncCycle, GetA, GetC, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SUB_D, new Sub(4, IncPC, IncCycle, GetA, GetD, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SUB_E, new Sub(4, IncPC, IncCycle, GetA, GetE, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SUB_H, new Sub(4, IncPC, IncCycle, GetA, GetH, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SUB_L, new Sub(4, IncPC, IncCycle, GetA, GetL, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SUB_M, new Sub(7, IncPC, IncCycle, GetA, GetM, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SUB_A, new Sub(4, IncPC, IncCycle, GetA, GetA, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.SBB_B, new Sbb(4, IncPC, IncCycle, GetA, GetB, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.SBB_C, new Sbb(4, IncPC, IncCycle, GetA, GetC, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.SBB_D, new Sbb(4, IncPC, IncCycle, GetA, GetD, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.SBB_E, new Sbb(4, IncPC, IncCycle, GetA, GetE, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.SBB_H, new Sbb(4, IncPC, IncCycle, GetA, GetH, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.SBB_L, new Sbb(4, IncPC, IncCycle, GetA, GetL, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.SBB_M, new Sbb(7, IncPC, IncCycle, GetA, GetM, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.SBB_A, new Sbb(4, IncPC, IncCycle, GetA, GetA, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY)},
                    {OpcodesI8080.ANA_B, new Ana(4, IncPC, IncCycle, GetA, GetB, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ANA_C, new Ana(4, IncPC, IncCycle, GetA, GetC, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ANA_D, new Ana(4, IncPC, IncCycle, GetA, GetD, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ANA_E, new Ana(4, IncPC, IncCycle, GetA, GetE, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ANA_H, new Ana(4, IncPC, IncCycle, GetA, GetH, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ANA_L, new Ana(4, IncPC, IncCycle, GetA, GetL, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ANA_M, new Ana(7, IncPC, IncCycle, GetA, GetM, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.ANA_A, new Ana(4, IncPC, IncCycle, GetA, GetA, SetA, SetS, SetZ, SetAC, SetP, SetCY)},
                    {OpcodesI8080.XRA_B, new Xra(4, IncPC, IncCycle, GetA, GetB, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.XRA_C, new Xra(4, IncPC, IncCycle, GetA, GetC, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.XRA_D, new Xra(4, IncPC, IncCycle, GetA, GetD, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.XRA_E, new Xra(4, IncPC, IncCycle, GetA, GetE, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.XRA_H, new Xra(4, IncPC, IncCycle, GetA, GetH, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.XRA_L, new Xra(4, IncPC, IncCycle, GetA, GetL, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.XRA_M, new Xra(7, IncPC, IncCycle, GetA, GetM, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.XRA_A, new Xra(4, IncPC, IncCycle, GetA, GetA, SetA, SetS, SetZ, SetAC, SetP, SetCY) },

                    {OpcodesI8080.ORA_B, new Ora(4, IncPC, IncCycle, GetA, GetB, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.ORA_C, new Ora(4, IncPC, IncCycle, GetA, GetC, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.ORA_D, new Ora(4, IncPC, IncCycle, GetA, GetD, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.ORA_E, new Ora(4, IncPC, IncCycle, GetA, GetE, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.ORA_H, new Ora(4, IncPC, IncCycle, GetA, GetH, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.ORA_L, new Ora(4, IncPC, IncCycle, GetA, GetL, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.ORA_M, new Ora(7, IncPC, IncCycle, GetA, GetM, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.ORA_A, new Ora(4, IncPC, IncCycle, GetA, GetA, SetA, SetS, SetZ, SetAC, SetP, SetCY) },

                    {OpcodesI8080.CMP_B, new Cmp(4, IncPC, IncCycle, GetA, GetB, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.CMP_C, new Cmp(4, IncPC, IncCycle, GetA, GetC, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.CMP_D, new Cmp(4, IncPC, IncCycle, GetA, GetD, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.CMP_E, new Cmp(4, IncPC, IncCycle, GetA, GetE, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.CMP_H, new Cmp(4, IncPC, IncCycle, GetA, GetH, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.CMP_L, new Cmp(4, IncPC, IncCycle, GetA, GetL, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.CMP_M, new Cmp(7, IncPC, IncCycle, GetA, GetM, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.CMP_A, new Cmp(4, IncPC, IncCycle, GetA, GetA, SetS, SetZ, SetAC, SetP, SetCY) },

                    {OpcodesI8080.RNZ, new RnCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetZ) },
                    {OpcodesI8080.POP_B, new Pop(10, IncPC, IncCycle, POPInternal, SetBC) },
                    {OpcodesI8080.JNZ_a16, new JpnCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetZ) },
                    {OpcodesI8080.JMP_a16, new Jmp(10, _memory, IncPC, IncCycle, GetPC, SetPC) },
                    {OpcodesI8080.CNZ_a16, new CallnCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetZ) },
                    {OpcodesI8080.PUSH_B, new Push(11, IncPC, IncCycle, GetBC, PUSHInternal) },
                    {OpcodesI8080.ADI_d8, new Add(4, IncPC, IncCycle, 2, GetA, GetArg8, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.RST_0, new Rst(11, IncCycle, SetPC, 0, GetPC, PUSHInternal) },
                    {OpcodesI8080.RZ, new RCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetZ) },
                    {OpcodesI8080.RET, new Ret(10, 10, IncPC, IncCycle, SetPC, POPInternal) },
                    {OpcodesI8080.JZ_a16, new JpCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetZ) },
                    {OpcodesI8080.JMP_a16_alt, new Jmp(10, _memory, IncPC, IncCycle, GetPC, SetPC) },
                    {OpcodesI8080.CZ_a16, new CallCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetZ) },
                    {OpcodesI8080.CALL_a16, new Call(11, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal) },
                    {OpcodesI8080.ACI_d8, new Adc(4, IncPC, IncCycle, 2, GetA, GetArg8, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY) },
                    {OpcodesI8080.RST_1, new Rst(11, IncCycle, SetPC, 1, GetPC, PUSHInternal) },

                    {OpcodesI8080.RNC, new RnCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetCY) },
                    {OpcodesI8080.POP_D, new Pop(10, IncPC, IncCycle, POPInternal, SetDE) },
                    {OpcodesI8080.JNC_a16, new JpnCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetCY) },
                    {OpcodesI8080.OUT_d8, new Out(_memory, 10, IncPC, IncCycle, GetPC, GetA, _ports)},
                    {OpcodesI8080.CNC_a16, new CallnCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetCY) },
                    {OpcodesI8080.PUSH_D, new Push(11, IncPC, IncCycle, GetDE, PUSHInternal) },
                    {OpcodesI8080.SUI_d8, new Sub(10, IncPC, IncCycle, GetA, GetArg8, SetA, SetS, SetZ, SetAC, SetP, SetCY) },
                    {OpcodesI8080.RST_2, new Rst(11, IncCycle, SetPC, 2, GetPC, PUSHInternal) },
                    {OpcodesI8080.RC, new RCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetCY) },
                    {OpcodesI8080.JC_a16, new JpCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetCY) },
                    {OpcodesI8080.IN_d8, new In(_memory, 10, IncPC, IncCycle, GetPC, SetA, _ports) },
                    {OpcodesI8080.CC_a16, new CallCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetCY) },
                    {OpcodesI8080.CALL_a16_v2, new Call(11, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal) },
                    {OpcodesI8080.SBI_d8, new Sbb(10, IncPC, IncCycle, GetA, GetArg8, SetA, SetS, SetZ, SetAC, SetP, GetCY, SetCY) },
                    {OpcodesI8080.RST_3, new Rst(11, IncCycle, SetPC, 3, GetPC, PUSHInternal) },

                    {OpcodesI8080.RPO, new RnCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetP) },
                    {OpcodesI8080.POP_H, new Pop(10, IncPC, IncCycle, POPInternal, SetHL) },
                    {OpcodesI8080.JPO_a16, new JpnCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetP) },
                    {OpcodesI8080.XTHL, new Xthl(18, IncPC, IncCycle, POPInternal, PUSHInternal, GetHL, SetHL)},
                    {OpcodesI8080.CPO_a16, new CallnCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetP) },
                    {OpcodesI8080.ANI_d8, new Ana(4, IncPC, IncCycle, GetA, GetArg8, SetA, SetS, SetZ, SetAC, SetP, SetCY, 2)},
                    {OpcodesI8080.PUSH_H, new Push(11, IncPC, IncCycle, GetHL, PUSHInternal) },
                    {OpcodesI8080.RST_4, new Rst(11, IncCycle, SetPC, 4, GetPC, PUSHInternal) },
                    {OpcodesI8080.RPE, new RCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetP) },
                    {OpcodesI8080.PCHL, new Pchl(5, IncPC, IncCycle, GetHL, SetPC) },
                    {OpcodesI8080.JPE_a16, new JpCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetP) },
                    {OpcodesI8080.XCHG, new Xchg(5, IncPC, IncCycle, GetHL, SetHL, GetDE, SetDE) },
                    {OpcodesI8080.CPE_a16, new CallCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetP) },
                    {OpcodesI8080.CALL_a16_v3, new Call(11, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal) },
                    {OpcodesI8080.XRI_d8, new Xra(4, IncPC, IncCycle, GetA, GetArg8, SetA, SetS, SetZ, SetAC, SetP, SetCY, 2) },
                    {OpcodesI8080.RST_5, new Rst(11, IncCycle, SetPC, 5, GetPC, PUSHInternal) },

                    {OpcodesI8080.RP, new RnCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetS) },
                    {OpcodesI8080.POP_PSW, new Pop(10, IncPC, IncCycle, POPInternal, SetPSW) },
                    {OpcodesI8080.JP_a16, new JpnCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetS) },
                    {OpcodesI8080.DI, new Di(4, IncPC, IncCycle, SetInterrupts) },
                    {OpcodesI8080.CP_a16, new CallnCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetS) },
                    {OpcodesI8080.PUSH_PSW, new Push(11, IncPC, IncCycle, GetPSW, PUSHInternal) },
                    {OpcodesI8080.ORI_d8, new Ora(7, IncPC, IncCycle, GetA, GetArg8, SetA, SetS, SetZ, SetAC, SetP, SetCY, 2) },
                    {OpcodesI8080.RST_6, new Rst(11, IncCycle, SetPC, 6, GetPC, PUSHInternal) },
                    {OpcodesI8080.RM, new RCondition(5, 11, IncPC, IncCycle, SetPC, POPInternal, GetS) },
                    {OpcodesI8080.SPHL, new SPHL(5, IncPC, IncCycle, GetHL, SetSP) },
                    {OpcodesI8080.JM_a16, new JpCondition(10, _memory, IncPC, IncCycle, GetPC, SetPC, GetS) },
                    {OpcodesI8080.EI, new Ei(4, IncPC, IncCycle, SetInterrupts) },
                    {OpcodesI8080.CM_a16, new CallCondition(11, 17, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal, GetS) },
                    {OpcodesI8080.CALL_a16_v4, new Call(11, _memory, IncPC, IncCycle, GetPC, SetPC, PUSHInternal) },
                    {OpcodesI8080.CPI_d8, new  Cmp(7, IncPC, IncCycle, GetA, GetArg8, SetS, SetZ, SetAC, SetP, SetCY, 2)},
                    {OpcodesI8080.RST_7, new Rst(11, IncCycle, SetPC, 7, GetPC, PUSHInternal) },

                };
        }
        private bool GetCY()
        {
            return FlagCY;
        }
        private void SetCY(bool value)
        {
            FlagCY = value;
        }
        private bool GetAC()
        {
            return FlagAC;
        }
        private void SetAC(bool value)
        {
            FlagAC = value;
        }
        private void SetS(bool value)
        {
            FlagS = value;
        }
        private bool GetS()
        {
            return FlagS;
        }
        private void SetZ(bool value)
        {
            FlagZ = value;
        }
        private bool GetZ()
        {
            return FlagZ;
        }
        private void SetP(bool value)
        {
            FlagP = value;
        }
        private bool GetP()
        {
            return FlagP;
        }
        private ushort GetSP()
        {
            return SP;
        }
        private void SetSP(ushort value)
        {
            SP = value;
        }
        private ushort GetHL()
        {
            return HL;
        }
        private void SetHL(ushort value)
        {
            HL = value;
        }
        private ushort GetDE()
        {
            return DE;
        }
        private void SetDE(ushort value)
        {
            DE = value;
        }
        private ushort GetBC()
        {
            return BC;
        }
        private void SetBC(ushort value)
        {
            BC = value;
        }

        private void SetPSW(ushort value)
        {
            F = (byte)(value % 256);
            A = (byte)(value / 256);
        }
        private ushort GetPSW()
        {
            return (ushort)(A * 256 + F);
        }

        private byte GetArg8()
        {
            return _memory.Read8(PC + 1);
        }

        private byte GetA()
        {
            return A;
        }
        private void SetA(byte value)
        {
            A = value;
        }
        private byte GetB()
        {
            return B;
        }
        private void SetB(byte value)
        {
            B = value;
        }
        private byte GetC()
        {
            return C;
        }
        private void SetC(byte value)
        {
            C = value;
        }
        private byte GetD()
        {
            return D;
        }
        private void SetD(byte value)
        {
            D = value;
        }
        private byte GetE()
        {
            return E;
        }
        private void SetE(byte value)
        {
            E = value;
        }
        private byte GetH()
        {
            return H;
        }
        private void SetH(byte value)
        {
            H = value;
        }
        private byte GetL()
        {
            return L;
        }
        private void SetL(byte value)
        {
            L = value;
        }
        private byte GetM()
        {
            return _memory.Read8(HL);
        }
        private void SetM(byte value)
        {
            _memory.Write8(HL, value);
        }

        private void SetPC(ushort value)
        {
            PC = value;
        }
        private void IncPC(ushort value)
        {
            PC += value;
        }
        private ushort GetPC()
        {
            return PC;
        }

        private void IncCycle(int cycles)
        {
            Cycle += cycles;
        }
        private void SetInterrupts(bool interruptsEnabled)
        {
            InterruptsEnabled = interruptsEnabled;
        }
        private void SetHalted(bool halted)
        {
            Halted = halted;
        }
        public ushort GetRegister16(string register)
        {
            switch (register)
            {
                case nameof(AF):
                    return AF;
                case nameof(BC):
                    return BC;
                case nameof(DE):
                    return DE;
                case nameof(HL):
                    return HL;
                case nameof(SP):
                    return SP;
                case nameof(PC):
                    return PC;
            }
            throw new ArgumentOutOfRangeException(nameof(register));
        }

        public byte GetRegister8(string register)
        {
            switch(register) { 
                case nameof(A):
                    return A;
                case nameof(B):
                    return B;
                case nameof(C):
                    return C;
                case nameof(D):
                    return D;
                case nameof(E):
                    return E;
                case nameof(H):
                    return H;
                case nameof(L):
                    return L;
                case nameof(F):
                    return F;
            }
            throw new ArgumentOutOfRangeException(nameof(register));
        }

        public Dictionary<string, bool> GetFlags() 
        {
            return new Dictionary<string, bool>
            {
                { "S", FlagS },
                { "Z", FlagZ },
                { "AC", FlagAC },
                { "P", FlagP },
                { "C", FlagCY }
            };
        }

        public Dictionary<string, int> GetRegisters()
        {
            return new Dictionary<string, int>
            {
                { "AF", AF },
                { "BC", BC },
                { "DE", DE },
                { "HL", HL },
                { "SP", SP },
                { "PC", PC }
            };
        }

        private Dictionary<byte, ICpuCommand> _cpuCommands;

        public void Interrupt()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            PC = (ushort)CommandCounterStart;
            SP = ushort.MaxValue;
            AF = 0;
            BC = 0;
            DE = 0;
            HL = 0;
            Cycle = 0;
        }

        public long Tick()
        {
            if (!Halted)
            {
                var opCode = _memory.Read8(PC);
                ProcessCommand(opCode);
            }
            return Cycle;
        }

        protected virtual void ProcessCommand(byte opCode)
        {
            if (_cpuCommands.TryGetValue(opCode, out var operation))
            {
                operation.Execute();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void PUSHInternal(ushort v)
        {
            SP -= 2;
            _memory.Write8(SP, (byte)(v % 256));
            _memory.Write8(SP+1, (byte)(v / 256));
        }

        private ushort POPInternal()
        {
            var sp1 = _memory.Read8(SP);
            var sp2 = _memory.Read8(SP + 1);
            SP = (ushort)(SP + 2);
            return (ushort)(sp2 * 256 + sp1);

        }
    }
}
