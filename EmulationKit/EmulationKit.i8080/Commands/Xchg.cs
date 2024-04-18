using System;

namespace EmulationKit.i8080.Commands
{
    internal class Xchg : BaseCommand
    {
        protected readonly Func<ushort> _getHl;
        protected readonly Action<ushort> _setHl;
        protected readonly Func<ushort> _getDe;
        protected readonly Action<ushort> _setDe;
        public Xchg(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getHl, Action<ushort> setHl, Func<ushort> getDe, Action<ushort> setDe) : base(cycles, incPc, incCycles)
        {
            _getHl = getHl;
            _setHl = setHl;
            _getDe = getDe;
            _setDe = setDe;
        }
        public override void Execute()
        {
            var tmp = _getHl();
            _setHl(_getDe());
            _setDe(tmp);
            base.Execute();
        }
    }
}
