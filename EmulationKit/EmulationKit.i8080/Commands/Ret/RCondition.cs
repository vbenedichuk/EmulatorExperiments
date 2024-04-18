﻿using System;

namespace EmulationKit.i8080.Commands.Ret
{
    internal class RCondition : RetBase
    {
        private readonly Func<bool> _getCondition;
        public RCondition(int cycles, int retCycles, Action<ushort> incPc, Action<int> incCycles, Action<ushort> setPc, Func<ushort> pop, Func<bool> getCondition) : base(cycles, retCycles, incPc, incCycles, setPc, pop)
        {
            _getCondition = getCondition;
        }
        protected override bool GetCondition()
        {
            return _getCondition();
        }
    }
}
