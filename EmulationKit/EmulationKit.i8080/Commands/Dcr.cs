using System;

namespace EmulationKit.i8080.Commands
{
    internal class Dcr : BaseCommand
    {
        private readonly Func<byte> _getSource;
        private readonly Action<byte> _setTarget;
        private readonly Action<bool> _setS;
        private readonly Action<bool> _setZ;
        private readonly Action<bool> _setP;
        private readonly Action<bool> _setAC;
        public Dcr(int cycles, Action<ushort> incPc, Action<int> incCycles, Func<byte> getSource, Action<byte> setTarget,
            Action<bool> setS, Action<bool> setZ, Action<bool> setP, Action<bool> setAC) 
            : base(cycles, incPc, incCycles)
        {
            _getSource = getSource;
            _setTarget = setTarget;
            _setS = setS;
            _setZ = setZ;
            _setP = setP;
            _setAC = setAC;
        }

        public override void Execute()
        {
            var b = _getSource();
            var resultI = b - 1;
            var result = (byte)(resultI < 0 ? 255 : b);
            _setS(FlagsCalculator.GetFlagS(result));
            _setZ(FlagsCalculator.GetFlagZ(result));
            _setP(FlagsCalculator.GetFlagP(result));
            _setAC(FlagsCalculator.GetFlagACSub(b, 1, 0));
            _setTarget(result);
            base.Execute();
        }
    }
}
