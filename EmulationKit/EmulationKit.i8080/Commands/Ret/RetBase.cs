using System;

namespace EmulationKit.i8080.Commands.Ret
{
    internal abstract class RetBase : BaseCommand
    {
        private readonly int _retCycles;
        private readonly Action<ushort> _setPc;
        private readonly Func<ushort> _pop;
        public RetBase(int cycles, int retCycles, Action<ushort> incPc, Action<int> incCycles, Action<ushort> setPc, Func<ushort> pop) : base(cycles, incPc, incCycles)
        {
            _retCycles = retCycles;
            _setPc = setPc;
            _pop = pop;
        }
        protected abstract bool GetCondition();
        public override void Execute()
        {
            if (GetCondition())
            {
                _incCycles(_retCycles);
                _setPc(_pop());
            }
            else
            {
                base.Execute();
            }

        }
    }
}
