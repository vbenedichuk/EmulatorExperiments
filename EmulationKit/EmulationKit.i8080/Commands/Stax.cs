using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Stax : BaseCommand
    {
        Func<byte> _getSource;
        Action<byte> _writeTarget;
        public Stax(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> writeTarget) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _writeTarget = writeTarget;
        }

        public override void Execute()
        {
            _writeTarget(_getSource());
            base.Execute();
        }
    }
}
