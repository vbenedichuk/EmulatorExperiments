using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands.Jp
{
    internal abstract class JpBase : D16Command
    {
        private readonly Action<ushort> _setPc;
        public JpBase(int cycles, IMemory memory, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<ushort> setPc) : base(cycles, memory, incPc, incCycles, getPc)
        {
            _setPc = setPc;
        }
        protected abstract bool GetCondition();
        public override void Execute()
        {
            if (GetCondition())
            {
                var addr = ReadOperand16();
                _incCycles(_cycles);
                _setPc(addr);
            }
            else
            {
                base.Execute();
            }
        }
    }
}
