using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Shld : D16Command
    {
        private readonly Func<ushort> _getSource;
        public Shld(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Func<ushort> getSource) 
            : base(cycles, memory, incPc, incCycles, getPc)
        {
            _getSource = getSource;
        }

        public override void Execute()
        {
            var addr = ReadOperand16();
            var hl = _getSource();
            _memory.Write8(addr, (byte)(hl%256));
            _memory.Write8(addr+1, (byte)(hl/256));
            base.Execute();
        }
    }
}
