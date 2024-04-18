using System;

namespace EmulationKit.i8080.Commands
{
    internal class Cma : BaseCommand
    {
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setTarget;
        public Cma(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> setTarget) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            _setTarget((byte)~_getSource());
            base.Execute();
        }
    }
}
