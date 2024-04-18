using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal abstract class D16Command : BaseCommand
    {
        protected readonly IMemory _memory;
        protected readonly Func<ushort> _getPc;
        protected D16Command(int cycles, IMemory memory, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc) : base(cycles, incPc, incCycles, 3)
        {
            _memory = memory;
            _getPc = getPc;
        }

        protected virtual ushort ReadOperand16()
        {
            var pc1 = _memory.Read8(_getPc() + 1);
            var pc2 = _memory.Read8(_getPc() + 2);
            return (ushort)(pc1 + pc2 * 256);
        }
    }
}
