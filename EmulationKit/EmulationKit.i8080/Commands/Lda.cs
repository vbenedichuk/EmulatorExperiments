using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Lda : D16Command
    {
        private readonly Action<byte> _setA;
        public Lda(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<byte> setA) 
            : base(cycles, memory, incPc, incCycles, getPc)
        {
            _setA = setA;
        }

        public override void Execute()
        {
            var addr = ReadOperand16();
            _setA(_memory.Read8(addr));
            base.Execute();
        }
    }
}
