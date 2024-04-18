using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal abstract class D8Command : BaseCommand
    {
        protected readonly IMemory _memory;
        protected readonly Func<ushort> _getPc;
        public D8Command(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc) : base(cycles, incPc, incCycles, 2)
        {
            _memory = memory;
            _getPc = getPc;
        }

        protected virtual byte ReadOperand8()
        {
            var pc1 = _memory.Read8(_getPc() + 1);
            return pc1;
        }
    }
}
