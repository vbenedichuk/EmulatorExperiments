using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Mov : BaseCommand
    {
        private Func<byte> _getSource;
        private Action<byte> _setTarget;
        public Mov(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> setTarget) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            _setTarget(_getSource());
            base.Execute();
        }
    }
}
