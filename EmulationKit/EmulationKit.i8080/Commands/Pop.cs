using System;

namespace EmulationKit.i8080.Commands
{
    internal class Pop : BaseCommand
    {
        private readonly Func<ushort> _pop;
        private readonly Action<ushort> _setTarget;
        public Pop(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> pop, Action<ushort> setTarget) : base(cycles, incPc, incCycles)
        {
            _pop = pop;
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            _setTarget(_pop());
            base.Execute();
        }
    }
}
