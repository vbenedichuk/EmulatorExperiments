using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.i8080.Commands
{
    internal class Xthl : BaseCommand
    {
        private readonly Func<ushort> _pop;
        private readonly Action<ushort> _push;
        private readonly Func<ushort> _getHl;
        private readonly Action<ushort> _setHl;
        public Xthl(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> pop, Action<ushort> push, Func<ushort> getHl, Action<ushort> setHl) 
            : base(cycles, incPc, incCycles)
        {
            _pop = pop;
            _push = push;
            _setHl = setHl;
            _getHl = getHl;
        }
        public override void Execute()
        {
            var val = _pop();
            _push(_getHl());
            _setHl(val);
            base.Execute();
        }
    }
}
