using System;

namespace EmulationKit.i8080.Commands
{
    internal class Ei : BaseCommand
    {
        private readonly Action<bool> _setInterrupts;
        public Ei(int cycles, Action<ushort> incPc, Action<int> incCycles, Action<bool> setInterrupts) : 
            base(cycles, incPc, incCycles)
        {
            _setInterrupts = setInterrupts;
        }

        public override void Execute()
        {
            _setInterrupts(true);
            base.Execute();
        }
    }
}
