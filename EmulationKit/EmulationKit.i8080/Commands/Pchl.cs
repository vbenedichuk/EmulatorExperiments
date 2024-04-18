using System;

namespace EmulationKit.i8080.Commands
{
    internal class Pchl : BaseCommand
    {
        private readonly Func<ushort> _getHl;
        private readonly Action<ushort> _setPc;
        public Pchl(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getHl, Action<ushort> setPc) : base(cycles, incPc, incCycles)
        {
            _getHl = getHl;
            _setPc = setPc;
        }

        public override void Execute()
        {
            _setPc(_getHl());
            _incCycles(_cycles);
        }
    }
}
