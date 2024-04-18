using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Sta : D16Command
    {
        private readonly Func<byte> _getA;
        public Sta(IMemory memory, int cycles, Action<ushort> incPc, Func<ushort> getPc, Action<int> incCycles, Func<byte> getA) 
            : base(cycles, memory, incPc, incCycles, getPc)
        {
            _getA = getA;
        }

        public override void Execute()
        {
            var addr = ReadOperand16();
            _memory.Write8(addr, _getA());
            base.Execute();
        }
    }
}
