using System;

namespace EmulationKit.i8080.Commands.Ret
{
    internal class Ret : RetBase
    {
        public Ret(int cycles, int retCycles, Action<ushort> incPc, Action<int> incCycles, Action<ushort> setPc, Func<ushort> pop) : base(cycles, retCycles, incPc, incCycles, setPc, pop)
        {
        }

        protected override bool GetCondition()
        {
            return true;
        }
    }
}
