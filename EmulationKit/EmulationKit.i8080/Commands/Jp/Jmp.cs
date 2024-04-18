using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands.Jp
{
    internal class Jmp : JpBase
    {
        public Jmp(int cycles, IMemory memory, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<ushort> setPc) : base(cycles, memory, incPc, incCycles, getPc, setPc)
        {
        }

        protected override bool GetCondition()
        {
            return true;
        }
    }
}
