using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.i8080.Commands
{
    internal class SPHL : BaseCommand
    {
        private readonly Func<ushort> _getHl;
        private readonly Action<ushort> _setSp;
        public SPHL(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getHl, Action<ushort> setSp) 
            : base(cycles, incPc, incCycles)
        {
            _getHl = getHl;
            _setSp = setSp;
        }
        public override void Execute()
        {
            _setSp(_getHl());
            base.Execute();
        }
    }
}
