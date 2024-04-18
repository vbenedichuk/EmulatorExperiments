using System;

namespace EmulationKit.i8080.Commands
{
    internal class Cmp : BaseCommand
    {
        private readonly Func<byte> _getA;
        private readonly Func<byte> _getSource;
        private readonly Action<bool> _setS;
        private readonly Action<bool> _setZ;
        private readonly Action<bool> _setAc;
        private readonly Action<bool> _setP;
        private readonly Action<bool> _setCy;
        public Cmp(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getA, Func<byte> getSource, 
            Action<bool> setS, Action<bool> setZ, Action<bool> setAc, Action<bool> setP, Action<bool> setCy, ushort opLength = 1) 
            : base(cycles, incPc, incCycles, opLength)
        {
            _getA = getA;
            _getSource = getSource;
            _setS = setS;
            _setZ = setZ;
            _setAc = setAc;
            _setP = setP;
            _setCy = setCy;
        }

        public override void Execute()
        {
            var a = _getA();
            var b = _getSource();
            var result = a - b;
            _setAc(FlagsCalculator.GetFlagACSub(a, b, 0));
            _setCy(result < 0);
            a = (byte)result;

            _setS(FlagsCalculator.GetFlagS(a));
            _setZ(FlagsCalculator.GetFlagZ(a));
            _setP(FlagsCalculator.GetFlagP(a));
            base.Execute();
        }
    }
}
