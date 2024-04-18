using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Dcx : BaseCommand
    {
        private readonly Func<ushort> _getSource;
        private readonly Action<ushort> _setTarget;
        public Dcx(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getSource, Action<ushort> setTarget) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            _setTarget((ushort)(_getSource() - 1));
            base.Execute();
        }
    }
}
