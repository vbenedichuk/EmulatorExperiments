using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Dad : BaseCommand
    {
        private readonly Func<ushort> _getSource1;
        private readonly Func<ushort> _getSource2;
        private readonly Action<ushort> _setTarget;
        private readonly Action<bool> _setCY;
        public Dad(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getSource1, Func<ushort> getSource2, Action<ushort> setTarget,
            Action<bool> setCY) : base(cycles, incPc, incCycles)
        {
            _getSource1 = getSource1;
            _getSource2 = getSource2;
            _setTarget = setTarget;
            _setCY = setCY;
        }

        public override void Execute()
        {
            var result = _getSource1() + _getSource2();
            if (result > 256 * 256)
            {
                _setCY(true);
            }
            else
            {
                _setCY(false);
            }
            _setTarget((ushort)result);

            base.Execute();
        }
    }
}
