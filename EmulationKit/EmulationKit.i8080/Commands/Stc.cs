using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Stc : BaseCommand
    {
        private readonly Action<bool> _setCy;
        public Stc(int cycles, Action<ushort> incPc, Action<int> incCycles, Action<bool> setCy) 
            : base(cycles, incPc, incCycles)
        {
            _setCy = setCy;
        }

        public override void Execute()
        {
            _setCy.Invoke(true);
            base.Execute();
        }
    }
}
