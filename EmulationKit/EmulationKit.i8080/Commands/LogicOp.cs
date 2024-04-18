using System;

namespace EmulationKit.i8080.Commands
{
    internal abstract class LogicOp : BaseCommand
    {
        private readonly Func<byte> _getA;
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setA;
        private readonly Action<bool> _setS;
        private readonly Action<bool> _setZ;
        private readonly Action<bool> _setAc;
        private readonly Action<bool> _setP;
        private readonly Action<bool> _setCy;
        public LogicOp(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getA, Func<byte> getSource, Action<byte> setA,
            Action<bool> setS, Action<bool> setZ, Action<bool> setAc, Action<bool> setP, Action<bool> setCy, ushort opLength = 1) : 
            base(cycles, incPc, incCycles, opLength)
        {
            _getA = getA;
            _getSource = getSource;
            _setA = setA;
            _setS = setS;
            _setZ = setZ;
            _setAc = setAc;
            _setP = setP;
            _setCy = setCy;
        }

        protected abstract int GetResult(byte a, byte b);
        protected abstract bool CalcAc(byte a, byte b);
        public override void Execute()
        {
            var a = _getA();
            var b = _getSource();
            var result = GetResult(a, b);
            _setAc(CalcAc(a,b));
            _setCy(false);
            a = (byte)result;
            _setA(a);
            _setS(FlagsCalculator.GetFlagS(a));
            _setZ(FlagsCalculator.GetFlagZ(a));
            _setP(FlagsCalculator.GetFlagP(a));
            base.Execute();
        }
    }
}
