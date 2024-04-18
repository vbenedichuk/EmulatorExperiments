using EmulationKit.i8080.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationKit.i8080.Test.CustomTests
{
    public class CyclePerformanceTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CycleTest()
        {
            var program = new byte[]
            {
                0x21,//LXI H,  6650h
                0x50,
                0x66,
                0x2b,//DCX H ;
                0x7d,//MOV A, L
                0xb4,//ORA H
                0xc2,//JNZ 0x0003
                0x03,
                0x00,
                0x76 // HLT
            };
            var memory = new MemoryMock(program);
            var cpu = new CpuI8080(memory, null);
            cpu.Tick();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while(cpu.GetRegister16("HL") != 0)
            {
                cpu.Tick();
                cpu.Tick();
                cpu.Tick();
                cpu.Tick();
            }
            stopwatch.Stop(); //around 420000 ticks (42 ms)
            Assert.Pass();

        }
    }
}
