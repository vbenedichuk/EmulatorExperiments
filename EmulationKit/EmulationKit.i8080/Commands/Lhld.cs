using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Lhld : D16Command
    {
        private readonly Action<ushort> _writeTarget;

        public Lhld(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<ushort> writeTarget) 
            : base(cycles, memory, incPc, incCycles, getPc)
        {
            _writeTarget = writeTarget;
        }

        public override void Execute()
        {
            var addr = ReadOperand16();
            var l = _memory.Read8(addr);
            var h = _memory.Read8((ushort)(addr + 1));
            _writeTarget(Make16(h, l));
            base.Execute();
        }

        private static ushort Make16(byte high, byte low) => (ushort)((high << 8) | low);

    }
}
