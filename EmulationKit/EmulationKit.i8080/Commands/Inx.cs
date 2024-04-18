using EmulationKit.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.i8080.Commands
{
    internal class Inx : BaseCommand
    {
        private readonly Func<ushort> _getSource;
        private readonly Action<ushort> _setTarget;
        public Inx(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getSource, Action<ushort> setTarget) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            _setTarget((ushort)(_getSource() + 1));
            base.Execute();
        }
    }
}
