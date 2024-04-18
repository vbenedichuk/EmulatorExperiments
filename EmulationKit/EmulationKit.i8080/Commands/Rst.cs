using System;

namespace EmulationKit.i8080.Commands
{
    internal class Rst : BaseCommand
    {
        private readonly Action<ushort> _setPc;
        private readonly int _rstNo;
        private readonly Func<ushort> _getPc;
        private readonly Action<ushort> _push;

        public Rst(int cycles, Action<int> incCycles, Action<ushort> setPc, int rstNo, Func<ushort> getPc, Action<ushort> push) : base(cycles, null, incCycles)
        {
            _setPc = setPc;
            _rstNo = rstNo;
            _getPc = getPc;
            _push = push;
        }
        public override void Execute()
        {
            _push((ushort)(_getPc() + 1));
            _setPc((ushort)(_rstNo * 8));
            _incCycles(_cycles);
        }
    }
}
