using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands.Jp
{
    internal class JpnCondition : JpBase
    {
        private readonly Func<bool> _getCondition;
        public JpnCondition(int cycles, IMemory memory, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<ushort> setPc, Func<bool> getCondition) : base(cycles, memory, incPc, incCycles, getPc, setPc)
        {
            _getCondition = getCondition;
        }
        protected override bool GetCondition()
        {
            return !_getCondition();
        }
    }
}
