using System;

namespace EmulationKit.i8080.Commands
{
    internal class Adc : BaseCommand
    {
        private readonly Func<byte> _getAccumulator;
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setTarget;
        private readonly Action<bool> _setS;
        private readonly Action<bool> _setZ;
        private readonly Action<bool> _setAc;
        private readonly Action<bool> _setP;
        private readonly Action<bool> _setCy;
        private readonly Func<bool> _getCy;
        public Adc(int cycles, Action<ushort> incPc, Action<int> incCycles, ushort opLength, Func<byte> getAccumulator, Func<byte> getSource, Action<byte> setTarget, Action<bool> setS, Action<bool> setZ, Action<bool> setAc, Action<bool> setP, Func<bool> getCy, Action<bool> setCy) : base(cycles, incPc, incCycles, opLength )
        {
            _getAccumulator = getAccumulator;
            _getSource = getSource;
            _setTarget = setTarget;
            _setS = setS;
            _setZ = setZ;
            _setAc = setAc;
            _setP = setP;
            _setCy = setCy;
            _getCy = getCy;
        }

        public override void Execute()
        {
            var cy = _getCy();
            Execute(cy);
        }
        protected virtual void Execute(bool cy)
        {
            var a = _getAccumulator();
            var b = _getSource();
            var result = a + b + (cy ? 1 : 0);
            var ac = FlagsCalculator.GetFlagACAdd(a, b) || FlagsCalculator.GetFlagACAdd((byte)(a + b), (byte)(cy ? 1 : 0));
            var newCy = result > 255;
            a = (byte)result;
            _setTarget(a);
            _setAc(ac);
            _setCy(newCy);

            _setS(FlagsCalculator.GetFlagS(a));
            _setZ(FlagsCalculator.GetFlagZ(a));
            _setP(FlagsCalculator.GetFlagP(a));

            base.Execute();
        }
    }
}
