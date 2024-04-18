using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class In : D8Command
    {
        private readonly IPorts _ports;
        private readonly Action<byte> _setA;

        public In(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<byte> setA, IPorts ports) : 
            base(memory, cycles, incPc, incCycles, getPc)
        {
            _ports = ports;
            _setA = setA;
        }
        public override void Execute()
        {
            _setA(_ports.In(ReadOperand8()));
            base.Execute();
        }
    }
}
