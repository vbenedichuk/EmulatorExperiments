using System;
using System.Collections.Generic;

namespace EmulationKit.Abstractions
{
    public interface ICpu
    {
        long CommandCounterStart { get; set; }
        long Tick();
        void Interrupt();
        void Reset();
        Dictionary<string, int> GetRegisters();
        byte GetRegister8(string register);
        ushort GetRegister16(string register);
        Dictionary<string, bool> GetFlags();
    }
}
