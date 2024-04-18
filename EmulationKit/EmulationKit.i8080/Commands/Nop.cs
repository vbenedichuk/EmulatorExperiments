using System;

namespace EmulationKit.i8080.Commands
{
    internal class Nop : BaseCommand
    {
        public Nop(int cycles, Action<ushort> incPc, Action<int> incCycles) : base(cycles, incPc, incCycles)
        {
        }
    }
}
