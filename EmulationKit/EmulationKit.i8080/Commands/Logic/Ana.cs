using System;

namespace EmulationKit.i8080.Commands.Logic
{
    internal class Ana : LogicOp
    {
        public Ana(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getA, Func<byte> getSource, Action<byte> setA,
            Action<bool> setS, Action<bool> setZ, Action<bool> setAc, Action<bool> setP, Action<bool> setCy, ushort opLength = 1) :
            base(cycles, incPc, incCycles, getA, getSource, setA, setS, setZ, setAc, setP, setCy, opLength)
        {
        }

        protected override int GetResult(byte a, byte b)
        {
            return a & b;
        }

        protected override bool CalcAc(byte a, byte b)
        {
            return ((a | b) & 0x08) != 0;
        }
    }
}
