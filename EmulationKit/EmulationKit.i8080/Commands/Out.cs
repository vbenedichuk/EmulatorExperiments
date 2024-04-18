using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Out : D8Command
    {
        private readonly IPorts _ports;
        private readonly Func<byte> _getA;
        public Out(IMemory memory, int cycles, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Func<byte> getA, IPorts ports) : base(memory, cycles, incPc, incCycles, getPc)
        {
            _ports = ports;
            _getA = getA;
        }
        public override void Execute()
        {
            _ports.Out(ReadOperand8(), _getA());
            base.Execute();
        }
    }
}
