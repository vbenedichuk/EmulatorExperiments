using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands.Call
{
    internal class CallnCondition : CallBase
    {
        private readonly Func<bool> _getCondition;
        public CallnCondition(int cycles, int successCycles, IMemory memory, Action<ushort> incPc, Action<int> incCycles, Func<ushort> getPc, Action<ushort> setPc, Action<ushort> push, Func<bool> getCondition) : base(cycles, successCycles, memory, incPc, incCycles, getPc, setPc, push)
        {
            _getCondition = getCondition;
        }

        protected override bool GetCondition()
        {
            return !_getCondition();
        }
    }
}
