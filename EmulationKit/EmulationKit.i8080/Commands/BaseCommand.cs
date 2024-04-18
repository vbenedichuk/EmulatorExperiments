using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal abstract class BaseCommand : ICpuCommand
    {
        protected readonly Action<ushort>? _incPc;
        protected readonly Action<int> _incCycles;
        protected readonly ushort _opLength;
        public int _cycles;
        public BaseCommand(int cycles, Action<ushort>? incPc, Action<int> incCycles, ushort opLength = 1)
        {
            _incPc = incPc;
            _incCycles = incCycles;
            _opLength = opLength;
            _cycles = cycles;
        }

        public virtual void Execute()
        {
            _incCycles?.Invoke(_cycles);
            _incPc?.Invoke(_opLength);
        }
    }
}
