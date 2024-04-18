using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands.Call
{
    internal class Call : CallBase
    {
        public Call(int cycles, IMemory memory, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<ushort> setPc, Action<ushort> push) : base(cycles, cycles, memory, incPc, incCycles, getPc, setPc, push)
        {
        }

        protected override bool GetCondition()
        {
            return true;
        }
    }
}
