using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.i8080
{
    internal static class OpcodesI8080
    {
        public const byte NOP = 0x00;
        public const byte LXI_B_d16 = 0x01;
        public const byte STAX_B = 0x02;
        public const byte INX_B = 0x03;
        public const byte INR_B = 0x04;
        public const byte DCR_B = 0x05;
        public const byte MVI_B_d8 = 0x06;
        public const byte RLC = 0x07;
        // 0x08 not used
        public const byte DAD_B = 0x09;
        public const byte LDAX_B = 0x0A;
        public const byte DCX_B = 0x0B;
        public const byte INR_C = 0x0C;
        public const byte DCR_C = 0x0D;
        public const byte MVI_C_d8 = 0x0E;
        public const byte RRC = 0x0F;
        // 0x10 not uset
        public const byte LXI_D_d16 = 0x11;
        public const byte STAX_D = 0x12;
        public const byte INX_D = 0x13;
        public const byte INR_D = 0x14;
        public const byte DCR_D = 0x15;
        public const byte MVI_D_d8 = 0x16;
        public const byte RAL = 0x17;
        // 0x18 not uset
        public const byte DAD_D = 0x19;
        public const byte LDAX_D = 0x1A;
        public const byte DCX_D = 0x1B;
        public const byte INR_E = 0x1C;
        public const byte DCR_E = 0x1D;
        public const byte MVI_E_d8 = 0x1E;
        public const byte RAR = 0x1F;
        // 0x20 not uset
        public const byte LXI_H_d16 = 0x21;
        public const byte SHLD_a16 = 0x22;
        public const byte INX_H = 0x23;
        public const byte INR_H = 0x24;
        public const byte DCR_H = 0x25;
        public const byte MVI_H_d8 = 0x26;
        public const byte DAA = 0x27;
        // 0x28 not uset
        public const byte DAD_H = 0x29;
        public const byte LHLD_a16 = 0x2A;
        public const byte DCX_H = 0x2B;
        public const byte INR_L = 0x2C;
        public const byte DCR_L = 0x2D;
        public const byte MVI_L_d8 = 0x2E;
        public const byte CMA = 0x2F;
        // 0x30 not uset
        public const byte LXI_SP_d16 = 0x31;
        public const byte STA_a16 = 0x32;
        public const byte INX_SP = 0x33;
        public const byte INR_M = 0x34;
        public const byte DCR_M = 0x35;
        public const byte MVI_M_d8 = 0x36;
        public const byte STC = 0x37;
        // 0x38 not uset
        public const byte DAD_SP = 0x39;
        public const byte LDA_a16 = 0x3A;
        public const byte DCX_SP = 0x3B;
        public const byte INR_A = 0x3C;
        public const byte DCR_A = 0x3D;
        public const byte MVI_A_d8 = 0x3E;
        public const byte CMC = 0x3F;

        public const byte MOV_B_B = 0x40;
        public const byte MOV_B_C = 0x41;
        public const byte MOV_B_D = 0x42;
        public const byte MOV_B_E = 0x43;
        public const byte MOV_B_H = 0x44;
        public const byte MOV_B_L = 0x45;
        public const byte MOV_B_M = 0x46;
        public const byte MOV_B_A = 0x47;

        public const byte MOV_C_B = 0x48;
        public const byte MOV_C_C = 0x49;
        public const byte MOV_C_D = 0x4a;
        public const byte MOV_C_E = 0x4b;
        public const byte MOV_C_H = 0x4c;
        public const byte MOV_C_L = 0x4d;
        public const byte MOV_C_M = 0x4e;
        public const byte MOV_C_A = 0x4f;

        public const byte MOV_D_B = 0x50;
        public const byte MOV_D_C = 0x51;
        public const byte MOV_D_D = 0x52;
        public const byte MOV_D_E = 0x53;
        public const byte MOV_D_H = 0x54;
        public const byte MOV_D_L = 0x55;
        public const byte MOV_D_M = 0x56;
        public const byte MOV_D_A = 0x57;

        public const byte MOV_E_B = 0x58;
        public const byte MOV_E_C = 0x59;
        public const byte MOV_E_D = 0x5a;
        public const byte MOV_E_E = 0x5b;
        public const byte MOV_E_H = 0x5c;
        public const byte MOV_E_L = 0x5d;
        public const byte MOV_E_M = 0x5e;
        public const byte MOV_E_A = 0x5f;

        public const byte MOV_H_B = 0x60;
        public const byte MOV_H_C = 0x61;
        public const byte MOV_H_D = 0x62;
        public const byte MOV_H_E = 0x63;
        public const byte MOV_H_H = 0x64;
        public const byte MOV_H_L = 0x65;
        public const byte MOV_H_M = 0x66;
        public const byte MOV_H_A = 0x67;

        public const byte MOV_L_B = 0x68;
        public const byte MOV_L_C = 0x69;
        public const byte MOV_L_D = 0x6a;
        public const byte MOV_L_E = 0x6b;
        public const byte MOV_L_H = 0x6c;
        public const byte MOV_L_L = 0x6d;
        public const byte MOV_L_M = 0x6e;
        public const byte MOV_L_A = 0x6f;

        public const byte MOV_M_B = 0x70;
        public const byte MOV_M_C = 0x71;
        public const byte MOV_M_D = 0x72;
        public const byte MOV_M_E = 0x73;
        public const byte MOV_M_H = 0x74;
        public const byte MOV_M_L = 0x75;
        public const byte HLT = 0x76;
        public const byte MOV_M_A = 0x77;

        public const byte MOV_A_B = 0x78;
        public const byte MOV_A_C = 0x79;
        public const byte MOV_A_D = 0x7a;
        public const byte MOV_A_E = 0x7b;
        public const byte MOV_A_H = 0x7c;
        public const byte MOV_A_L = 0x7d;
        public const byte MOV_A_M = 0x7e;
        public const byte MOV_A_A = 0x7f;

        public const byte ADD_B = 0x80;
        public const byte ADD_C = 0x81;
        public const byte ADD_D = 0x82;
        public const byte ADD_E = 0x83;
        public const byte ADD_H = 0x84;
        public const byte ADD_L = 0x85;
        public const byte ADD_M = 0x86;
        public const byte ADD_A = 0x87;

        public const byte ADC_B = 0x88;
        public const byte ADC_C = 0x89;
        public const byte ADC_D = 0x8a;
        public const byte ADC_E = 0x8b;
        public const byte ADC_H = 0x8c;
        public const byte ADC_L = 0x8d;
        public const byte ADC_M = 0x8e;
        public const byte ADC_A = 0x8f;

        public const byte SUB_B = 0x90;
        public const byte SUB_C = 0x91;
        public const byte SUB_D = 0x92;
        public const byte SUB_E = 0x93;
        public const byte SUB_H = 0x94;
        public const byte SUB_L = 0x95;
        public const byte SUB_M = 0x96;
        public const byte SUB_A = 0x97;

        public const byte SBB_B = 0x98;
        public const byte SBB_C = 0x99;
        public const byte SBB_D = 0x9a;
        public const byte SBB_E = 0x9b;
        public const byte SBB_H = 0x9c;
        public const byte SBB_L = 0x9d;
        public const byte SBB_M = 0x9e;
        public const byte SBB_A = 0x9f;


        public const byte ANA_B = 0xA0;
        public const byte ANA_C = 0xA1;
        public const byte ANA_D = 0xA2;
        public const byte ANA_E = 0xA3;
        public const byte ANA_H = 0xA4;
        public const byte ANA_L = 0xA5;
        public const byte ANA_M = 0xA6;
        public const byte ANA_A = 0xA7;

        public const byte XRA_B = 0xA8;
        public const byte XRA_C = 0xA9;
        public const byte XRA_D = 0xAa;
        public const byte XRA_E = 0xAb;
        public const byte XRA_H = 0xAc;
        public const byte XRA_L = 0xAd;
        public const byte XRA_M = 0xAe;
        public const byte XRA_A = 0xAf;


        public const byte ORA_B = 0xb0;
        public const byte ORA_C = 0xb1;
        public const byte ORA_D = 0xb2;
        public const byte ORA_E = 0xb3;
        public const byte ORA_H = 0xb4;
        public const byte ORA_L = 0xb5;
        public const byte ORA_M = 0xb6;
        public const byte ORA_A = 0xb7;

        public const byte CMP_B = 0xb8;
        public const byte CMP_C = 0xb9;
        public const byte CMP_D = 0xba;
        public const byte CMP_E = 0xbb;
        public const byte CMP_H = 0xbc;
        public const byte CMP_L = 0xbd;
        public const byte CMP_M = 0xbe;
        public const byte CMP_A = 0xbf;


        public const byte RNZ = 0xc0;
        public const byte POP_B = 0xc1;
        public const byte JNZ_a16 = 0xc2;
        public const byte JMP_a16 = 0xc3;
        public const byte CNZ_a16 = 0xc4;
        public const byte PUSH_B = 0xc5;
        public const byte ADI_d8 = 0xc6;
        public const byte RST_0 = 0xc7;

        public const byte RZ = 0xc8;
        public const byte RET = 0xc9;
        public const byte JZ_a16 = 0xca;
        public const byte JMP_a16_alt = 0xcb;
        public const byte CZ_a16 = 0xcc;
        public const byte CALL_a16 = 0xcd;
        public const byte ACI_d8 = 0xce;
        public const byte RST_1 = 0xcf;


        public const byte RNC = 0xd0;
        public const byte POP_D = 0xd1;
        public const byte JNC_a16 = 0xd2;
        public const byte OUT_d8 = 0xd3;
        public const byte CNC_a16 = 0xd4;
        public const byte PUSH_D = 0xd5;
        public const byte SUI_d8 = 0xd6;
        public const byte RST_2 = 0xd7;

        public const byte RC = 0xd8;
        public const byte RET_v2 = 0xd9;
        public const byte JC_a16 = 0xda;
        public const byte IN_d8 = 0xdb;
        public const byte CC_a16 = 0xdc;
        public const byte CALL_a16_v2 = 0xdd;
        public const byte SBI_d8 = 0xde;
        public const byte RST_3 = 0xdf;


        public const byte RPO = 0xe0;
        public const byte POP_H = 0xe1;
        public const byte JPO_a16 = 0xe2;
        public const byte XTHL = 0xe3;
        public const byte CPO_a16 = 0xe4;
        public const byte PUSH_H = 0xe5;
        public const byte ANI_d8 = 0xe6;
        public const byte RST_4 = 0xe7;

        public const byte RPE = 0xe8;
        public const byte PCHL = 0xe9;
        public const byte JPE_a16 = 0xea;
        public const byte XCHG = 0xeb;
        public const byte CPE_a16 = 0xec;
        public const byte CALL_a16_v3 = 0xed;
        public const byte XRI_d8 = 0xee;
        public const byte RST_5 = 0xef;


        public const byte RP = 0xf0;
        public const byte POP_PSW = 0xf1;
        public const byte JP_a16 = 0xf2;
        public const byte DI = 0xf3;
        public const byte CP_a16 = 0xf4;
        public const byte PUSH_PSW = 0xf5;
        public const byte ORI_d8 = 0xf6;
        public const byte RST_6 = 0xf7;

        public const byte RM = 0xf8;
        public const byte SPHL = 0xf9;
        public const byte JM_a16 = 0xfa;
        public const byte EI = 0xfb;
        public const byte CM_a16 = 0xfc;
        public const byte CALL_a16_v4 = 0xfd;
        public const byte CPI_d8 = 0xfe;
        public const byte RST_7 = 0xff;

    }
}
