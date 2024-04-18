using System;

namespace EmulationKit.i8080.Commands
{
    internal class Sub : Sbb
    {
        public Sub(int cycles, Action<ushort> incPc, Action<int> incCycles, 
            Func<byte> getAccumulator, Func<byte> getSource, Action<byte> setTarget,
            Action<bool> setS, Action<bool> setZ, Action<bool> setAc,
            Action<bool> setP, Action<bool> setCy) : 
            base(cycles, incPc, incCycles, getAccumulator, getSource, setTarget, 
                setS, setZ, setAc, setP, GetCyFalse, setCy)
        {
        }
        private static bool GetCyFalse()
        {
            return false;
        }
    }
}
