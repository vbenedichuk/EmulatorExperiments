using System;

namespace EmulationKit.i8080.Commands
{
    internal class Push : BaseCommand
    {
        private readonly Action<ushort> _push;
        private readonly Func<ushort> _getSource;

        public Push(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getSource, Action<ushort> push) : base(cycles, incPc, incCycles)
        {
            _push = push;
            _getSource = getSource;
        }
        public override void Execute()
        {
            _push(_getSource());
            base.Execute();
        }
    }
}
