using System;

namespace EmulationKit.i8080.Commands
{
    internal class Rrc : BaseCommand
    {
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setTarget;
        private readonly Action<bool> _setCy;
        public Rrc(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> setTarget, Action<bool> setCY) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
            _setCy = setCY;
        }

        public override void Execute()
        {
            var A = _getSource();
            var a0 = A % 2 == 1;
            var a = A >> 1;
            _setCy(a0);
            a = a + (a0 ? 128 : 0);
            _setTarget((byte)a);
            base.Execute();
        }
    }
}
