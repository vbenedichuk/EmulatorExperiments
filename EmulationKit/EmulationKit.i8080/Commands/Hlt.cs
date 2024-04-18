using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Hlt : BaseCommand
    {
        private readonly Action<bool> _setHalted;
        public Hlt(int cycles, Action<ushort> incPc, Action<int> incCycles, Action<bool> setHalted) : base(cycles, incPc, incCycles)
        {
            _setHalted = setHalted;
        }

        public override void Execute()
        {
            _setHalted(true);
            base.Execute();
        }
    }
}
