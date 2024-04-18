using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Cmc : BaseCommand
    {
        private readonly Func<bool> _getCy;
        private readonly Action<bool> _setCy;
        public Cmc(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<bool> getCy, Action<bool> setCy) : base(cycles, incPc, incCycles)
        {
            _getCy = getCy;
            _setCy = setCy;
        }

        public override void Execute()
        {
            _setCy(!_getCy());
            base.Execute();
        }
    }
}
