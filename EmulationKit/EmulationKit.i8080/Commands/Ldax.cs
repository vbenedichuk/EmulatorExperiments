using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Ldax : BaseCommand
    {
        private readonly IMemory _memory;
        private readonly Func<ushort> _getSourceAddr;
        private readonly Action<byte> _setTarget;
        public Ldax(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getSourceAddr, Action<byte> setTarget) 
            : base(cycles, incPc, incCycles)
        {
            _memory = memory;
            _getSourceAddr = getSourceAddr;
            _setTarget = setTarget;
        }

        public override void Execute()
        {
            _setTarget(_memory.Read8(_getSourceAddr()));
            base.Execute();
        }
    }
}
