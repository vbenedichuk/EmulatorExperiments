using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Lxi : D16Command
    {
        private readonly Action<ushort> _setTarget;
        public Lxi(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<ushort> setTarget) 
            : base(cycles, memory, incPc, incCycles, getPc)
        {
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            var value = ReadOperand16();
            _setTarget(value);
            base.Execute();
            
        }
    }
}
