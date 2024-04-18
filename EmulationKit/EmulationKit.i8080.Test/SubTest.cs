using EmulationKit.i8080.Test.Mocks;

namespace EmulationKit.i8080.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        // SUB
        #region SUB
        [TestCase(new byte[]
            {
                0x06, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 25h
                0x25,
                0x90, // SUB B
            }, 
            0x15, false, false, false, false, false,
            TestName = "SUB B"
            )]
        [TestCase(new byte[]
            {
                0x0e, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 26h
                0x26,
                0x91, // SUB C
            },
            0x16, false, false, false, false, false,
            TestName = "SUB C"
            )]
        [TestCase(new byte[]
            {
                0x16, // MVI D, 10h
                0x10,
                0x3e, // MVI A, 27h
                0x27,
                0x92, // SUB D
            },
            0x17, false, false, false, true, false,
            TestName = "SUB D"
            )]
        [TestCase(new byte[]
            {
                0x1e, // MVI E, 10h
                0x10,
                0x3e, // MVI A, 28h
                0x28,
                0x93, // SUB E
            },
            0x18, false, false, false, true, false,
            TestName = "SUB E"
            )]

        [TestCase(new byte[]
            {
                0x26, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 29h
                0x29,
                0x94, // SUB H
            },
            0x19, false, false, false, false, false,
            TestName = "SUB H"
            )]

        [TestCase(new byte[]
            {
                0x2e, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 2Ah
                0x2a,
                0x95, // SUB L
            },
            0x1A, false, false, false, false, false,
            TestName = "SUB L"
            )]

        [TestCase(new byte[]
            {
                0x00,
                0x3e, // MVI A, 2Bh
                0x2b,
                0x97, // SUB A
            },
            0, false, true, false, true, false,
            TestName = "SUB A"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 01h
                0x01,
                0x3e, // MVI A, 10h
                0x10,
                0x95, // SUB L
            },
            0x0f, false, false, true, true, false,
            TestName = "SUB L; Aux Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 20h
                0x20,
                0x3e, // MVI A, 0Fh
                0x0f,
                0x95, // SUB L
            },
            0xEF, true, false, false, false, true,
            TestName = "SUB L; Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 05h
                0x05,
                0x3e, // MVI A, 2Ch
                0x2c,
                0x96, // SUB M
                0x11  // DATA 11h
            },
            0x1B, false, false, false, true, false,
            TestName = "SUB M ; 1" 
            )]
        #endregion

        #region ADD
        [TestCase(new byte[]
            {
                0x06, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 25h
                0x25,
                0x80, // ADD B
            },
            0x35, false, false, false, true, false,
            TestName = "ADD B"
            )]
        [TestCase(new byte[]
            {
                0x0e, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 26h
                0x26,
                0x81, // ADD C
            },
            0x36, false, false, false, true, false,
            TestName = "ADD C"
            )]
        [TestCase(new byte[]
            {
                0x16, // MVI D, 10h
                0x10,
                0x3e, // MVI A, 27h
                0x27,
                0x82, // ADD D
            },
            0x37, false, false, false, false, false,
            TestName = "ADD D"
            )]
        [TestCase(new byte[]
            {
                0x1e, // MVI E, 10h
                0x10,
                0x3e, // MVI A, 28h
                0x28,
                0x83, // ADD E
            },
            0x38, false, false, false, false, false,
            TestName = "ADD E"
            )]

        [TestCase(new byte[]
            {
                0x26, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 29h
                0x29,
                0x84, // ADD H
            },
            0x39, false, false, false, true, false,
            TestName = "ADD H"
            )]

        [TestCase(new byte[]
            {
                0x2e, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 2Ah
                0x2a,
                0x85, // ADD L
            },
            0x3A, false, false, false, true, false,
            TestName = "ADD L"
            )]

        [TestCase(new byte[]
            {
                0x00, // NOP
                0x3e, // MVI A, 2Bh
                0x2b,
                0x87, // ADD A
            },
            0x56, false, false, true, true, false,
            TestName = "ADD A"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 01h
                0x01,
                0x3e, // MVI A, 0Fh
                0x0f,
                0x85, // ADD L
            },
            0x10, false, false, true, false, false,
            TestName = "ADD L; Aux Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 20h
                0x20,
                0x3e, // MVI A, FFh
                0xFF,
                0x85, // ADD L
            },
            0x1f, false, false, false, false, true,
            TestName = "ADD L; Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 05h
                0x05,
                0x3e, // MVI A, 2Ch
                0x2c,
                0x86, // ADD M
                0x11  // DATA 11h
            },
            0x3d, false, false, false, false, false,
            TestName = "ADD M"
            )]
        #endregion

        #region ADC No Carry
        [TestCase(new byte[]
            {
                0x06, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 25h
                0x25,
                0x88, // ADC B
            },
            0x35, false, false, false, true, false,
            TestName = "ADC B; No Carry"
            )]
        [TestCase(new byte[]
            {
                0x0e, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 26h
                0x26,
                0x89, // ADC C
            },
            0x36, false, false, false, true, false,
            TestName = "ADC C; No Carry"
            )]
        [TestCase(new byte[]
            {
                0x16, // MVI D, 10h
                0x10,
                0x3e, // MVI A, 27h
                0x27,
                0x8A, // ADC D
            },
            0x37, false, false, false, false, false,
            TestName = "ADC D; No Carry"
            )]
        [TestCase(new byte[]
            {
                0x1e, // MVI E, 10h
                0x10,
                0x3e, // MVI A, 28h
                0x28,
                0x8B, // ADC E
            },
            0x38, false, false, false, false, false,
            TestName = "ADC E; No Carry"
            )]

        [TestCase(new byte[]
            {
                0x26, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 29h
                0x29,
                0x8C, // ADC H
            },
            0x39, false, false, false, true, false,
            TestName = "ADC H; No Carry"
            )]

        [TestCase(new byte[]
            {
                0x2e, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 2Ah
                0x2a,
                0x8D, // ADC L
            },
            0x3A, false, false, false, true, false,
            TestName = "ADC L; No Carry"
            )]

        [TestCase(new byte[]
            {
                0x00,
                0x3e, // MVI A, 2Bh
                0x2b,
                0x8F, // ADC A
            },
            0x56, false, false, true, true, false,
            TestName = "ADC A; No Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 01h
                0x01,
                0x3e, // MVI A, 0Fh
                0x0f,
                0x8D, // ADC L
            },
            0x10, false, false, true, false, false,
            TestName = "ADC L; No Carry, Aux Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 20h
                0x20,
                0x3e, // MVI A, FFh
                0xFF,
                0x8D, // ADC L
            },
            0x1f, false, false, false, false, true,
            TestName = "ADC L; No Carry, Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 05h
                0x05,
                0x3e, // MVI A, 2Ch
                0x2c,
                0x8E, // ADC M
                0x11  // DATA 11h
            },
            0x3d, false, false, false, false, false,
            TestName = "ADC M; No Carry"
            )]
        #endregion

        #region ANA
        [TestCase(new byte[]
            {
                0x06, // MVI C, 0b00010000
                0b00010000,
                0x3e, // MVI A, 0b00010101
                0b00010101,
                0xA0, // ANA B
            },
            0b00010000, false, false, false, false, false,
            TestName = "ANA B"
            )]
        [TestCase(new byte[]
            {
                0x0e, // MVI C, 0b00010001
                0b00010001,
                0x3e, // MVI A, 0b00001111
                0b00001111,
                0xA1, // ANA C
            },
            0b00000001, false, false, true, false, false,
            TestName = "ANA C"
            )]
        [TestCase(new byte[]
            {
                0x16, // MVI D, 0b11111111
                0b11111111,
                0x3e, // MVI A, 0b11110000
                0b11110000,
                0xA2, // ANA D
            },
            0b11110000, true, false, true, true, false,
            TestName = "ANA D"
            )]
        [TestCase(new byte[]
            {
                0x1e, // MVI E, 0b10110101
                0b10110101,
                0x3e, // MVI A, 0b11011111
                0b11011111,
                0xA3, // ANA E
            },
            0b10010101, true, false, true, true, false,
            TestName = "ANA E"
            )]

        [TestCase(new byte[]
            {
                0x26, // MVI L, 0b01010101
                0b01010101,
                0x3e, // MVI A, 0b01010101
                0b01010101,
                0xA4, // ANA H
            },
            0b01010101, false, false, false, true, false,
            TestName = "ANA H"
            )]

        [TestCase(new byte[]
            {
                0x2e, // MVI L, 0b10101010
                0b10101010,
                0x3e, // MVI A, 0b01010101
                0b01010101,
                0xA5, // ANA L
            },
            0x00, false, true, true, true, false,
            TestName = "ANA L"
            )]

        [TestCase(new byte[]
            {
                0x00,
                0x3e, // MVI A, 2Bh
                0x2b,
                0xA7, // ADD A
            },
            0x2b, false, false, true, true, false,
            TestName = "ANA A"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 05h
                0x05,
                0x3e, // MVI A, 0b00000011
                0b00000011,
                0xA6, // ANA M
                0b00010001  // 0b00010001
            },
            0b00000001, false, false, false, false, false,
            TestName = "ANA M"
            )]
        #endregion

        #region ORA
        [TestCase(new byte[]
            {
                0x06, // MVI C, 0b00010000
                0b00000000,
                0x3e, // MVI A, 0b00010101
                0b00000000,
                0xB0, // ORA B
            },
            0b00000000, false, true, false, true, false,
            TestName = "ORA B"
            )]
        [TestCase(new byte[]
            {
                0x0e, // MVI C, 0b00010001
                0b00010001,
                0x3e, // MVI A, 0b00001111
                0b00001111,
                0xB1, // ORA C
            },
            0b00011111, false, false, false, false, false,
            TestName = "ORA C"
            )]
        [TestCase(new byte[]
            {
                0x16, // MVI D, 0b11111111
                0b11111111,
                0x3e, // MVI A, 0b11110000
                0b11110000,
                0xB2, // ORA D
            },
            0b11111111, true, false, false, true, false,
            TestName = "ORA D"
            )]
        [TestCase(new byte[]
            {
                0x1e, // MVI E, 0b10110101
                0b10110101,
                0x3e, // MVI A, 0b11011111
                0b11011111,
                0xB3, // ORA E
            },
            0b11111111, true, false, false, true, false,
            TestName = "ORA E"
            )]

        [TestCase(new byte[]
            {
                0x26, // MVI L, 0b01010101
                0b01010101,
                0x3e, // MVI A, 0b01010101
                0b01010101,
                0xB4, // ORA H
            },
            0b01010101, false, false, false, true, false,
            TestName = "ORA H"
            )]

        [TestCase(new byte[]
            {
                0x2e, // MVI L, 0b10101010
                0b10101010,
                0x3e, // MVI A, 0b01010101
                0b01010101,
                0xB5, // ORA L
            },
            0b11111111, true, false, false, true, false,
            TestName = "ORA L"
            )]

        [TestCase(new byte[]
            {
                0x00,
                0x3e, // MVI A, 2Bh
                0x2b,
                0xB7, // ORA A
            },
            0x2b, false, false, false, true, false,
            TestName = "ORA A"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 05h
                0x05,
                0x3e, // MVI A, 0b00000011
                0b00000011,
                0xB6, // ORA M
                0b00010001  // 0b00010001
            },
            0b00010011, false, false, false, false, false,
            TestName = "ORA M"
            )]
        #endregion
        public void TestAThreeOps(byte[] program, byte expectedResult,
            bool fs, bool fz, bool fa, bool fp, bool fc)
        {
            var memory = new MemoryMock(program);
            var cpu = new CpuI8080(memory, null);            
            cpu.Tick();
            cpu.Tick();
            cpu.Tick();
            Assert.That(cpu.GetRegister8("A"), Is.EqualTo(expectedResult));
            Assert.That(cpu.FlagS, Is.EqualTo(fs), "FS");
            Assert.That(cpu.FlagZ, Is.EqualTo(fz), "FZ");
            Assert.That(cpu.FlagAC, Is.EqualTo(fa), "FA");
            Assert.That(cpu.FlagP, Is.EqualTo(fp), "FP");
            Assert.That(cpu.FlagCY, Is.EqualTo(fc), "FC");
        }

        #region ADC
        [TestCase(new byte[]
            {
                0x06, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 25h
                0x25,
                0x88, // ADC B
            },
            0x36, false, false, false, true, false,
            TestName = "ADC B"
            )]
        [TestCase(new byte[]
            {
                0x0e, // MVI C, 10h
                0x10,
                0x3e, // MVI A, 26h
                0x26,
                0x89, // ADC C
            },
            0x37, false, false, false, false, false,
            TestName = "ADC C"
            )]
        [TestCase(new byte[]
            {
                0x16, // MVI D, 10h
                0x10,
                0x3e, // MVI A, 27h
                0x27,
                0x8A, // ADC D
            },
            0x38, false, false, false, false, false,
            TestName = "ADC D"
            )]
        [TestCase(new byte[]
            {
                0x1e, // MVI E, 10h
                0x10,
                0x3e, // MVI A, 28h
                0x28,
                0x8B, // ADC E
            },
            0x39, false, false, false, true, false,
            TestName = "ADC E"
            )]

        [TestCase(new byte[]
            {
                0x26, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 29h
                0x29,
                0x8C, // ADC H
            },
            0x3A, false, false, false, true, false,
            TestName = "ADC H"
            )]

        [TestCase(new byte[]
            {
                0x2e, // MVI L, 10h
                0x10,
                0x3e, // MVI A, 2Ah
                0x2a,
                0x8D, // ADC L
            },
            0x3B, false, false, false, false, false,
            TestName = "ADC L"
            )]

        [TestCase(new byte[]
            {
                0x00,
                0x3e, // MVI A, 2Bh
                0x2b,
                0x8F, // ADC A
            },
            0x57, false, false, true, false, false,
            TestName = "ADC A"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 01h
                0x01,
                0x3e, // MVI A, 0Fh
                0x0f,
                0x8D, // ADC L
            },
            0x11, false, false, true, true, false,
            TestName = "ADC L; Aux Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 20h
                0x20,
                0x3e, // MVI A, FFh
                0xFF,
                0x8D, // ADC L
            },
            0x20, false, false, true, false, true,
            TestName = "ADC L; Carry"
            )]
        [TestCase(new byte[]
            {
                0x2e, // MVI L, 05h
                0x05,
                0x3e, // MVI A, 2Ch
                0x2c,
                0x8E, // ADC M
                0x11  // DATA 11h
            },
            0x3E, false, false, false, false, false,
            TestName = "ADC M"
            )]
        #endregion
        public void TestAThreeOpsCarry(byte[] program, byte expectedResult,
          bool fs, bool fz, bool fa, bool fp, bool fc)
        {
            var memory = new MemoryMock(program);
            var cpu = new CpuI8080(memory, null);
            cpu.FlagCY = true;
            cpu.Tick();
            cpu.Tick();
            cpu.Tick();
            Assert.That(cpu.GetRegister8("A"), Is.EqualTo(expectedResult));
            Assert.That(cpu.FlagS, Is.EqualTo(fs), "FS");
            Assert.That(cpu.FlagZ, Is.EqualTo(fz), "FZ");
            Assert.That(cpu.FlagAC, Is.EqualTo(fa), "FA");
            Assert.That(cpu.FlagP, Is.EqualTo(fp), "FP");
            Assert.That(cpu.FlagCY, Is.EqualTo(fc), "FC");
        }
    }
}