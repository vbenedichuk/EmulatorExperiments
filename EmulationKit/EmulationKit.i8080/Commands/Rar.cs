using System;

namespace EmulationKit.i8080.Commands
{
    internal class Rar : BaseCommand
    {
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setTarget;
        private readonly Func<bool> _getCy;
        private readonly Action<bool> _setCy;
        public Rar(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> setTarget,
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
            var a0 = (A % 2) == 1;
            var a = A >> 1;
            a = a + (_getCy() ? 128 : 0);
            _setCy(a0);
            _setTarget((byte)a);

            base.Execute();
        }
    }
}
