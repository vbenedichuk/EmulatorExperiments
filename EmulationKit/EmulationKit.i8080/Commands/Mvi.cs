using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Mvi : D8Command
    {
        private readonly Action<byte> _setTarget;
        public Mvi(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<byte> setTarget) 
            : base(memory, cycles, incPc, incCycles, getPc)
        {
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            var value = ReadOperand8();
            _setTarget(value);
            base.Execute();
        }
    }
}
