using System.Reflection.Emit;
using System.Text;

namespace EmulationKit.i8080.Disasm
{
    internal class Disassembler
    {
        private readonly IDictionary<byte, InstructionDescriptor> _instructionsSet = new Dictionary<byte, InstructionDescriptor>()
        {
            { 0x00, new InstructionDescriptor{ Mnemonic = "NOP" } },
            { 0x01, new InstructionDescriptor{ Mnemonic = "LXI B, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D16} },
            { 0x02, new InstructionDescriptor{ Mnemonic = "STAX B" } },
            { 0x03, new InstructionDescriptor{ Mnemonic = "INX B" } },
            { 0x04, new InstructionDescriptor{ Mnemonic = "INR B" } },
            { 0x05, new InstructionDescriptor{ Mnemonic = "DCR B" } },
            { 0x06, new InstructionDescriptor{ Mnemonic = "MVI B, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x07, new InstructionDescriptor{ Mnemonic = "RLC" } },
            { 0x08, new InstructionDescriptor{ Mnemonic = "NOP", Comment = "Code 0x08, should not be used." } },
            { 0x09, new InstructionDescriptor{ Mnemonic = "DAD B" } },
            { 0x0A, new InstructionDescriptor{ Mnemonic = "LDAX B" } },
            { 0x0B, new InstructionDescriptor{ Mnemonic = "DCX B" } },
            { 0x0C, new InstructionDescriptor{ Mnemonic = "INR C" } },
            { 0x0D, new InstructionDescriptor{ Mnemonic = "DCR C" } },
            { 0x0E, new InstructionDescriptor{ Mnemonic = "MVI C, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x0F, new InstructionDescriptor{ Mnemonic = "RRC" } },
            { 0x10, new InstructionDescriptor{ Mnemonic = "NOP", Comment = "Code 0x10, should not be used." } },
            { 0x11, new InstructionDescriptor{ Mnemonic = "LXI D, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D16} },
            { 0x12, new InstructionDescriptor{ Mnemonic = "STAX D" } },
            { 0x13, new InstructionDescriptor{ Mnemonic = "INX D" } },
            { 0x14, new InstructionDescriptor{ Mnemonic = "INR D" } },
            { 0x15, new InstructionDescriptor{ Mnemonic = "DCR D" } },
            { 0x16, new InstructionDescriptor{ Mnemonic = "MVI D, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x17, new InstructionDescriptor{ Mnemonic = "RAL" } },
            { 0x18, new InstructionDescriptor{ Mnemonic = "NOP", Comment = "Code 0x18, should not be used." } },
            { 0x19, new InstructionDescriptor{ Mnemonic = "DAD D" } },
            { 0x1A, new InstructionDescriptor{ Mnemonic = "LDAX D" } },
            { 0x1B, new InstructionDescriptor{ Mnemonic = "DCX D" } },
            { 0x1C, new InstructionDescriptor{ Mnemonic = "INR E" } },
            { 0x1D, new InstructionDescriptor{ Mnemonic = "DCR E" } },
            { 0x1E, new InstructionDescriptor{ Mnemonic = "MVI E, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x1F, new InstructionDescriptor{ Mnemonic = "RAR" } },
            { 0x20, new InstructionDescriptor{ Mnemonic = "NOP", Comment = "Code 0x20, should not be used." } },
            { 0x21, new InstructionDescriptor{ Mnemonic = "LXI H, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D16} },
            { 0x22, new InstructionDescriptor{ Mnemonic = "SHLD ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address } },
            { 0x23, new InstructionDescriptor{ Mnemonic = "INX H" } },
            { 0x24, new InstructionDescriptor{ Mnemonic = "INR H" } },
            { 0x25, new InstructionDescriptor{ Mnemonic = "DCR H" } },
            { 0x26, new InstructionDescriptor{ Mnemonic = "MVI H, d8", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x27, new InstructionDescriptor{ Mnemonic = "DAA" } },
            { 0x28, new InstructionDescriptor{ Mnemonic = "NOP", Comment = "Code 0x28, should not be used." } },
            { 0x29, new InstructionDescriptor{ Mnemonic = "DAD H" } },
            { 0x2A, new InstructionDescriptor{ Mnemonic = "LHLD ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address } },
            { 0x2B, new InstructionDescriptor{ Mnemonic = "DCX H" } },
            { 0x2C, new InstructionDescriptor{ Mnemonic = "INR L" } },
            { 0x2D, new InstructionDescriptor{ Mnemonic = "DCR L" } },
            { 0x2E, new InstructionDescriptor{ Mnemonic = "MVI L, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x2F, new InstructionDescriptor{ Mnemonic = "CMA" } },
            { 0x30, new InstructionDescriptor{ Mnemonic = "NOP", Comment = "Code 0x30, should not be used." } },
            { 0x31, new InstructionDescriptor{ Mnemonic = "LXI SP, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D16} },
            { 0x32, new InstructionDescriptor{ Mnemonic = "STA ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address } },
            { 0x33, new InstructionDescriptor{ Mnemonic = "INX SP" } },
            { 0x34, new InstructionDescriptor{ Mnemonic = "INR M" } },
            { 0x35, new InstructionDescriptor{ Mnemonic = "DCR M" } },
            { 0x36, new InstructionDescriptor{ Mnemonic = "MVI M, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x37, new InstructionDescriptor{ Mnemonic = "STC" } },
            { 0x38, new InstructionDescriptor{ Mnemonic = "NOP", Comment = "Code 0x38, should not be used." } },
            { 0x39, new InstructionDescriptor{ Mnemonic = "DAD SP" } },
            { 0x3A, new InstructionDescriptor{ Mnemonic = "LDA ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address } },
            { 0x3B, new InstructionDescriptor{ Mnemonic = "DCX SP" } },
            { 0x3C, new InstructionDescriptor{ Mnemonic = "INR A" } },
            { 0x3D, new InstructionDescriptor{ Mnemonic = "DCR A" } },
            { 0x3E, new InstructionDescriptor{ Mnemonic = "MVI A, ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8 } },
            { 0x3F, new InstructionDescriptor{ Mnemonic = "CMC" } },

            { 0x40, new InstructionDescriptor{ Mnemonic = "MOV B, B"} },
            { 0x41, new InstructionDescriptor{ Mnemonic = "MOV B, C"} },
            { 0x42, new InstructionDescriptor{ Mnemonic = "MOV B, D"} },
            { 0x43, new InstructionDescriptor{ Mnemonic = "MOV B, E"} },
            { 0x44, new InstructionDescriptor{ Mnemonic = "MOV B, H"} },
            { 0x45, new InstructionDescriptor{ Mnemonic = "MOV B, L"} },
            { 0x46, new InstructionDescriptor{ Mnemonic = "MOV B, M"} },
            { 0x47, new InstructionDescriptor{ Mnemonic = "MOV B, A"} },

            { 0x48, new InstructionDescriptor{ Mnemonic = "MOV C, B"} },
            { 0x49, new InstructionDescriptor{ Mnemonic = "MOV C, C"} },
            { 0x4a, new InstructionDescriptor{ Mnemonic = "MOV C, D"} },
            { 0x4b, new InstructionDescriptor{ Mnemonic = "MOV C, E"} },
            { 0x4c, new InstructionDescriptor{ Mnemonic = "MOV C, H"} },
            { 0x4d, new InstructionDescriptor{ Mnemonic = "MOV C, L"} },
            { 0x4e, new InstructionDescriptor{ Mnemonic = "MOV C, M"} },
            { 0x4f, new InstructionDescriptor{ Mnemonic = "MOV C, A"} },

            { 0x50, new InstructionDescriptor{ Mnemonic = "MOV D, B"} },
            { 0x51, new InstructionDescriptor{ Mnemonic = "MOV D, C"} },
            { 0x52, new InstructionDescriptor{ Mnemonic = "MOV D, D"} },
            { 0x53, new InstructionDescriptor{ Mnemonic = "MOV D, E"} },
            { 0x54, new InstructionDescriptor{ Mnemonic = "MOV D, H"} },
            { 0x55, new InstructionDescriptor{ Mnemonic = "MOV D, L"} },
            { 0x56, new InstructionDescriptor{ Mnemonic = "MOV D, M"} },
            { 0x57, new InstructionDescriptor{ Mnemonic = "MOV D, A"} },

            { 0x58, new InstructionDescriptor{ Mnemonic = "MOV E, B"} },
            { 0x59, new InstructionDescriptor{ Mnemonic = "MOV E, C"} },
            { 0x5a, new InstructionDescriptor{ Mnemonic = "MOV E, D"} },
            { 0x5b, new InstructionDescriptor{ Mnemonic = "MOV E, E"} },
            { 0x5c, new InstructionDescriptor{ Mnemonic = "MOV E, H"} },
            { 0x5d, new InstructionDescriptor{ Mnemonic = "MOV E, L"} },
            { 0x5e, new InstructionDescriptor{ Mnemonic = "MOV E, M"} },
            { 0x5f, new InstructionDescriptor{ Mnemonic = "MOV E, A"} },

            { 0x60, new InstructionDescriptor{ Mnemonic = "MOV H, B"} },
            { 0x61, new InstructionDescriptor{ Mnemonic = "MOV H, C"} },
            { 0x62, new InstructionDescriptor{ Mnemonic = "MOV H, D"} },
            { 0x63, new InstructionDescriptor{ Mnemonic = "MOV H, E"} },
            { 0x64, new InstructionDescriptor{ Mnemonic = "MOV H, H"} },
            { 0x65, new InstructionDescriptor{ Mnemonic = "MOV H, L"} },
            { 0x66, new InstructionDescriptor{ Mnemonic = "MOV H, M"} },
            { 0x67, new InstructionDescriptor{ Mnemonic = "MOV H, A"} },

            { 0x68, new InstructionDescriptor{ Mnemonic = "MOV L, B"} },
            { 0x69, new InstructionDescriptor{ Mnemonic = "MOV L, C"} },
            { 0x6a, new InstructionDescriptor{ Mnemonic = "MOV L, D"} },
            { 0x6b, new InstructionDescriptor{ Mnemonic = "MOV L, E"} },
            { 0x6c, new InstructionDescriptor{ Mnemonic = "MOV L, H"} },
            { 0x6d, new InstructionDescriptor{ Mnemonic = "MOV L, L"} },
            { 0x6e, new InstructionDescriptor{ Mnemonic = "MOV L, M"} },
            { 0x6f, new InstructionDescriptor{ Mnemonic = "MOV L, A"} },

            { 0x70, new InstructionDescriptor{ Mnemonic = "MOV M, B"} },
            { 0x71, new InstructionDescriptor{ Mnemonic = "MOV M, C"} },
            { 0x72, new InstructionDescriptor{ Mnemonic = "MOV M, D"} },
            { 0x73, new InstructionDescriptor{ Mnemonic = "MOV M, E"} },
            { 0x74, new InstructionDescriptor{ Mnemonic = "MOV M, H"} },
            { 0x75, new InstructionDescriptor{ Mnemonic = "MOV M, L"} },
            { 0x76, new InstructionDescriptor{ Mnemonic = "HLT"} },
            { 0x77, new InstructionDescriptor{ Mnemonic = "MOV M, A"} },

            { 0x78, new InstructionDescriptor{ Mnemonic = "MOV A, B"} },
            { 0x79, new InstructionDescriptor{ Mnemonic = "MOV A, C"} },
            { 0x7a, new InstructionDescriptor{ Mnemonic = "MOV A, D"} },
            { 0x7b, new InstructionDescriptor{ Mnemonic = "MOV A, E"} },
            { 0x7c, new InstructionDescriptor{ Mnemonic = "MOV A, H"} },
            { 0x7d, new InstructionDescriptor{ Mnemonic = "MOV A, L"} },
            { 0x7e, new InstructionDescriptor{ Mnemonic = "MOV A, M"} },
            { 0x7f, new InstructionDescriptor{ Mnemonic = "MOV A, A"} },

            { 0x80, new InstructionDescriptor{ Mnemonic = "ADD B"} },
            { 0x81, new InstructionDescriptor{ Mnemonic = "ADD C"} },
            { 0x82, new InstructionDescriptor{ Mnemonic = "ADD D"} },
            { 0x83, new InstructionDescriptor{ Mnemonic = "ADD E"} },
            { 0x84, new InstructionDescriptor{ Mnemonic = "ADD H"} },
            { 0x85, new InstructionDescriptor{ Mnemonic = "ADD L"} },
            { 0x86, new InstructionDescriptor{ Mnemonic = "ADD M"} },
            { 0x87, new InstructionDescriptor{ Mnemonic = "ADD A"} },

            { 0x88, new InstructionDescriptor{ Mnemonic = "ADC B"} },
            { 0x89, new InstructionDescriptor{ Mnemonic = "ADC C"} },
            { 0x8a, new InstructionDescriptor{ Mnemonic = "ADC D"} },
            { 0x8b, new InstructionDescriptor{ Mnemonic = "ADC E"} },
            { 0x8c, new InstructionDescriptor{ Mnemonic = "ADC H"} },
            { 0x8d, new InstructionDescriptor{ Mnemonic = "ADC L"} },
            { 0x8e, new InstructionDescriptor{ Mnemonic = "ADC M"} },
            { 0x8f, new InstructionDescriptor{ Mnemonic = "ADC A"} },

            { 0x90, new InstructionDescriptor{ Mnemonic = "SUB B"} },
            { 0x91, new InstructionDescriptor{ Mnemonic = "SUB C"} },
            { 0x92, new InstructionDescriptor{ Mnemonic = "SUB D"} },
            { 0x93, new InstructionDescriptor{ Mnemonic = "SUB E"} },
            { 0x94, new InstructionDescriptor{ Mnemonic = "SUB H"} },
            { 0x95, new InstructionDescriptor{ Mnemonic = "SUB L"} },
            { 0x96, new InstructionDescriptor{ Mnemonic = "SUB M"} },
            { 0x97, new InstructionDescriptor{ Mnemonic = "SUB A"} },

            { 0x98, new InstructionDescriptor{ Mnemonic = "SBB B"} },
            { 0x99, new InstructionDescriptor{ Mnemonic = "SBB C"} },
            { 0x9a, new InstructionDescriptor{ Mnemonic = "SBB D"} },
            { 0x9b, new InstructionDescriptor{ Mnemonic = "SBB E"} },
            { 0x9c, new InstructionDescriptor{ Mnemonic = "SBB H"} },
            { 0x9d, new InstructionDescriptor{ Mnemonic = "SBB L"} },
            { 0x9e, new InstructionDescriptor{ Mnemonic = "SBB M"} },
            { 0x9f, new InstructionDescriptor{ Mnemonic = "SBB A"} },

            { 0xA0, new InstructionDescriptor{ Mnemonic = "ANA B"} },
            { 0xA1, new InstructionDescriptor{ Mnemonic = "ANA C"} },
            { 0xA2, new InstructionDescriptor{ Mnemonic = "ANA D"} },
            { 0xA3, new InstructionDescriptor{ Mnemonic = "ANA E"} },
            { 0xA4, new InstructionDescriptor{ Mnemonic = "ANA H"} },
            { 0xA5, new InstructionDescriptor{ Mnemonic = "ANA L"} },
            { 0xA6, new InstructionDescriptor{ Mnemonic = "ANA M"} },
            { 0xA7, new InstructionDescriptor{ Mnemonic = "ANA A"} },

            { 0xA8, new InstructionDescriptor{ Mnemonic = "XRA B"} },
            { 0xA9, new InstructionDescriptor{ Mnemonic = "XRA C"} },
            { 0xAa, new InstructionDescriptor{ Mnemonic = "XRA D"} },
            { 0xAb, new InstructionDescriptor{ Mnemonic = "XRA E"} },
            { 0xAc, new InstructionDescriptor{ Mnemonic = "XRA H"} },
            { 0xAd, new InstructionDescriptor{ Mnemonic = "XRA L"} },
            { 0xAe, new InstructionDescriptor{ Mnemonic = "XRA M"} },
            { 0xAf, new InstructionDescriptor{ Mnemonic = "XRA A"} },

            { 0xB0, new InstructionDescriptor{ Mnemonic = "ORA B"} },
            { 0xB1, new InstructionDescriptor{ Mnemonic = "ORA C"} },
            { 0xB2, new InstructionDescriptor{ Mnemonic = "ORA D"} },
            { 0xB3, new InstructionDescriptor{ Mnemonic = "ORA E"} },
            { 0xB4, new InstructionDescriptor{ Mnemonic = "ORA H"} },
            { 0xB5, new InstructionDescriptor{ Mnemonic = "ORA L"} },
            { 0xB6, new InstructionDescriptor{ Mnemonic = "ORA M"} },
            { 0xB7, new InstructionDescriptor{ Mnemonic = "ORA A"} },

            { 0xB8, new InstructionDescriptor{ Mnemonic = "CMP B"} },
            { 0xB9, new InstructionDescriptor{ Mnemonic = "CMP C"} },
            { 0xBa, new InstructionDescriptor{ Mnemonic = "CMP D"} },
            { 0xBb, new InstructionDescriptor{ Mnemonic = "CMP E"} },
            { 0xBc, new InstructionDescriptor{ Mnemonic = "CMP H"} },
            { 0xBd, new InstructionDescriptor{ Mnemonic = "CMP L"} },
            { 0xBe, new InstructionDescriptor{ Mnemonic = "CMP M"} },
            { 0xBf, new InstructionDescriptor{ Mnemonic = "CMP A"} },

            { 0xC0, new InstructionDescriptor{ Mnemonic = "RNZ"} },
            { 0xC1, new InstructionDescriptor{ Mnemonic = "POP B"} },
            { 0xC2, new InstructionDescriptor{ Mnemonic = "JNZ ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xC3, new InstructionDescriptor{ Mnemonic = "JMP ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xC4, new InstructionDescriptor{ Mnemonic = "CNZ ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xC5, new InstructionDescriptor{ Mnemonic = "PUSH B"} },
            { 0xC6, new InstructionDescriptor{ Mnemonic = "ADI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xC7, new InstructionDescriptor{ Mnemonic = "RST 0"} },

            { 0xC8, new InstructionDescriptor{ Mnemonic = "RZ"} },
            { 0xC9, new InstructionDescriptor{ Mnemonic = "RET"} },
            { 0xCa, new InstructionDescriptor{ Mnemonic = "JZ ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xCb, new InstructionDescriptor{ Mnemonic = "JMP ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address, Comment = "SHOULD NOT BE USED!!! "} },
            { 0xCc, new InstructionDescriptor{ Mnemonic = "CZ ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xCd, new InstructionDescriptor{ Mnemonic = "CALL ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xCe, new InstructionDescriptor{ Mnemonic = "ACI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xCf, new InstructionDescriptor{ Mnemonic = "RST 1"} },

            { 0xD0, new InstructionDescriptor{ Mnemonic = "RNC"} },
            { 0xD1, new InstructionDescriptor{ Mnemonic = "POP D"} },
            { 0xD2, new InstructionDescriptor{ Mnemonic = "JNC ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xD3, new InstructionDescriptor{ Mnemonic = "OUT ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xD4, new InstructionDescriptor{ Mnemonic = "CNC ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xD5, new InstructionDescriptor{ Mnemonic = "PUSH D"} },
            { 0xD6, new InstructionDescriptor{ Mnemonic = "SUI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xD7, new InstructionDescriptor{ Mnemonic = "RST 2"} },

            { 0xD8, new InstructionDescriptor{ Mnemonic = "RC"} },
            { 0xD9, new InstructionDescriptor{ Mnemonic = "RET", Comment = "Possible error. Should not be used!"} },
            { 0xDa, new InstructionDescriptor{ Mnemonic = "JC ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xDb, new InstructionDescriptor{ Mnemonic = "IN ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xDc, new InstructionDescriptor{ Mnemonic = "CC ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xDd, new InstructionDescriptor{ Mnemonic = "CALL ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address, Comment = "Possible error. Should not be used!"} },
            { 0xDe, new InstructionDescriptor{ Mnemonic = "SBI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xDf, new InstructionDescriptor{ Mnemonic = "RST 3"} },

            { 0xE0, new InstructionDescriptor{ Mnemonic = "RPO"} },
            { 0xE1, new InstructionDescriptor{ Mnemonic = "POP H"} },
            { 0xE2, new InstructionDescriptor{ Mnemonic = "JPO ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xE3, new InstructionDescriptor{ Mnemonic = "XTHL"} },
            { 0xE4, new InstructionDescriptor{ Mnemonic = "CPO ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xE5, new InstructionDescriptor{ Mnemonic = "PUSH H"} },
            { 0xE6, new InstructionDescriptor{ Mnemonic = "ANI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xE7, new InstructionDescriptor{ Mnemonic = "RST 4"} },

            { 0xE8, new InstructionDescriptor{ Mnemonic = "RPE"} },
            { 0xE9, new InstructionDescriptor{ Mnemonic = "PCHL"} },
            { 0xEa, new InstructionDescriptor{ Mnemonic = "JPE ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xEb, new InstructionDescriptor{ Mnemonic = "XCHG"} },
            { 0xEc, new InstructionDescriptor{ Mnemonic = "CPE ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xEd, new InstructionDescriptor{ Mnemonic = "CALL ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address, Comment = "Possible error. Should not be used!"} },
            { 0xEe, new InstructionDescriptor{ Mnemonic = "XRI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xEf, new InstructionDescriptor{ Mnemonic = "RST 5"} },

            { 0xF0, new InstructionDescriptor{ Mnemonic = "RP"} },
            { 0xF1, new InstructionDescriptor{ Mnemonic = "POP PSW"} },
            { 0xF2, new InstructionDescriptor{ Mnemonic = "JP ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xF3, new InstructionDescriptor{ Mnemonic = "DI"} },
            { 0xF4, new InstructionDescriptor{ Mnemonic = "CP ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xF5, new InstructionDescriptor{ Mnemonic = "PUSH PSW"} },
            { 0xF6, new InstructionDescriptor{ Mnemonic = "ORI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xF7, new InstructionDescriptor{ Mnemonic = "RST 5"} },

            { 0xF8, new InstructionDescriptor{ Mnemonic = "RM"} },
            { 0xF9, new InstructionDescriptor{ Mnemonic = "SPHL"} },
            { 0xFa, new InstructionDescriptor{ Mnemonic = "JM ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xFb, new InstructionDescriptor{ Mnemonic = "EI"} },
            { 0xFc, new InstructionDescriptor{ Mnemonic = "CM ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address} },
            { 0xFd, new InstructionDescriptor{ Mnemonic = "CALL ", ArgumentType = InstructionDescriptor.InstructionArgumentType.Address, Comment = "Possible error. Should not be used!"} },
            { 0xFe, new InstructionDescriptor{ Mnemonic = "CPI ", ArgumentType = InstructionDescriptor.InstructionArgumentType.D8} },
            { 0xFf, new InstructionDescriptor{ Mnemonic = "RST 7"} },

        };

        public string GenerateCode(AppStructure appStructure)
        {
            var lines = new Dictionary<ushort, string>();
            var result = new StringBuilder();
            foreach (var line in appStructure.Lines)
            {
                lines.Add(line.Key, GetCommand(line.Value));
                result.Clear();
            }
            foreach (var line in lines)
            {
                if (appStructure.AddressSet.Contains(line.Key))
                {
                    result.AppendFormat("a_{0,4:X4}: {1} \n", line.Key, line.Value);
                }
                else
                {
                    result.AppendFormat("        {0} \n", line.Value);
                }
            }
            return result.ToString();
        }

        private string GetCommand(byte[] value)
        {
            var result = new StringBuilder();
            var opCode = value[0];
            var instruction = _instructionsSet[opCode];
            result.Append(instruction.Mnemonic);
            switch (instruction.ArgumentType)
            {
                case InstructionDescriptor.InstructionArgumentType.D8:
                    var d8 = value[1];
                    result.AppendFormat(" {0,2:X2}h", d8);
                    break;
                case InstructionDescriptor.InstructionArgumentType.D16:
                    var b1 = value[1];
                    var b2 = value[1];
                    result.AppendFormat(" {0,4:X4}h", b1 + b2 * 256);
                    break;
                case InstructionDescriptor.InstructionArgumentType.Address:
                    var ab1 = value[1];
                    var ab2 = value[1];
                    var addr = (ushort)(ab1 + ab2 * 256);
                    result.AppendFormat(" a_{0,4:X4}", addr);
                    break;
            }
            result.Append(" ; ");
            foreach (var bt in value)
            {
                result.AppendFormat("{0,2:X2} ", bt);
            }
            if (!string.IsNullOrWhiteSpace(instruction.Comment))
            {
                result.Append(instruction.Comment);
            }
            return result.ToString();
        }

        public AppStructure Disassemble(byte[] bytes, uint offset)
        {
            var resultStructure = new AppStructure();
            var pos = (ushort)0;
            var codes = new List<byte>(3);
            resultStructure.AddressSet.Add(0);
            resultStructure.AddressSet.Add(0x8);
            resultStructure.AddressSet.Add(0x10);
            resultStructure.AddressSet.Add(0x18);
            resultStructure.AddressSet.Add(0x20);
            resultStructure.AddressSet.Add(0x28);
            resultStructure.AddressSet.Add(0x30);
            resultStructure.AddressSet.Add(0x38);
            while (pos < bytes.Length)
            {
                var startPos = pos;
                codes.Clear();
                var opCode = bytes[pos++];
                codes.Add(opCode);
                var instruction = _instructionsSet[opCode];
                switch (instruction.ArgumentType)
                {
                    case InstructionDescriptor.InstructionArgumentType.D8:
                        var d8 = bytes[pos++];
                        codes.Add(d8);
                        break;
                    case InstructionDescriptor.InstructionArgumentType.D16:
                        var b1 = bytes[pos++];
                        var b2 = bytes[pos++];
                        codes.Add(b1);
                        codes.Add(b2);
                        break;
                    case InstructionDescriptor.InstructionArgumentType.Address:
                        var ab1 = bytes[pos++];
                        var ab2 = bytes[pos++];
                        var addr = (ushort)(ab1 + ab2 * 256);
                        resultStructure.AddressSet.Add(addr);
                        codes.Add(ab1);
                        codes.Add(ab2);
                        break;
                }
                resultStructure.Lines.Add(startPos, codes.ToArray());
            }
            return resultStructure;
        }
    }
}
