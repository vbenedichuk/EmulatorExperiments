using EmulationKit.Abstractions;
using System;

namespace EmulationKit.i8080.Commands
{
    internal class Daa : BaseCommand
    {
        private readonly Func<byte> _getAccumulator;
        private readonly Action<byte> _setAccumulator;
        private readonly Func<bool> _getCy;
        private readonly Action<bool> _setCy;
        private readonly Func<bool> _getAc;
        private readonly Action<bool> _setAc;
        private readonly Action<bool> _setS;
        private readonly Action<bool> _setZ;
        private readonly Action<bool> _setP;

        public Daa(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getAccumulator, Action<byte> setAccumulator,
            Func<bool> getCy, Action<bool> setCy, Func<bool> getAc, Action<bool> setAc,
            Action<bool> setS, Action<bool> setZ, Action<bool> setP) : base(cycles, incPc, incCycles)
        {
            _getAccumulator = getAccumulator;
            _setAccumulator = setAccumulator;
            _getCy = getCy;
            _setCy = setCy;
            _getAc = getAc;
            _setAc = setAc;
            _setS = setS;
            _setZ = setZ;
            _setP = setP;
        }

        public override void Execute()
        {
            var A = _getAccumulator();

            if (A % 16 > 9 || _getAc())
            {
                var aCheck = A % 16 + 6;
                if (aCheck > 15)
                {
                    _setAc(true);
                }
                else
                {
                    _setAc(false);
                }
                A += 6;
            }
            if (A / 16 > 9 || _getCy())
            {
                var newA = A + 16 * 6;
                if (newA > 255)
                {
                    _setCy(true);
                }
                else
                {
                    _setCy(false);
                }
                A = (byte)newA;
            }
            _setS(FlagsCalculator.GetFlagS(A));
            _setZ(FlagsCalculator.GetFlagZ(A));
            _setP(FlagsCalculator.GetFlagP(A));

            base.Execute();
        }
    }
}
