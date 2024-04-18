using System;

namespace EmulationKit.i8080.Commands
{
    internal class Rlc : BaseCommand
    {
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setTarget;
        private readonly Action<bool> _setCy;
        public Rlc(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> setTarget, Action<bool> setCY) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
            _setCy = setCY;
        }

        public override void Execute()
        {
            var A = _getSource();
            var a7 = (A / 128) == 1;
            var a = A << 1;
            _setCy(a7);
            a = a + (a7 ? 1 : 0);
            _setTarget((byte)a);

            base.Execute();
        }
    }
}
