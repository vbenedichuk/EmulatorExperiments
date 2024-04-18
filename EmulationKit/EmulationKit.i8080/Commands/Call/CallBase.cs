using EmulationKit.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.i8080.Commands.Call
{
    internal abstract class CallBase : D16Command
    {
        private readonly Action<ushort> _setPc;
        private readonly Action<ushort> _push;
        private readonly int _successCycles;

        public CallBase(int cycles, int successCycles, IMemory memory, Action<ushort> incPc, Action<int> incCycles,
            Func<ushort> getPc, Action<ushort> setPc, Action<ushort> push) : base(cycles, memory, incPc, incCycles, getPc)
        {
            _setPc = setPc;
            _push = push;
            _successCycles = successCycles;
        }

        protected abstract bool GetCondition();
        public override void Execute()
        {
            if (GetCondition())
            {
                var addr = ReadOperand16();
                _incCycles(_successCycles);
                _push((ushort)(_getPc() + 3));
                _setPc(addr);
            }
            else
            {
                base.Execute();
            }
        }
    }
}
