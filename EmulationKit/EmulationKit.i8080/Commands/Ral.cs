using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Ral : BaseCommand
    {
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setTarget;
        private readonly Func<bool> _getCy;
        private readonly Action<bool> _setCy;
        public Ral(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> setTarget, 
            Func<bool> getCY, Action<bool> setCY) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
            _getCy = getCY;
            _setCy = setCY;
        }

        public override void Execute()
        {
            var A = _getSource();
            var a7 = (A / 128) == 1;
            var a = A << 1;
            a = a + (_getCy() ? 1 : 0);
            _setCy(a7);
            _setTarget((byte)a);

            base.Execute();
        }
    }
}
