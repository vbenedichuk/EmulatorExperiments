using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.Abstractions
{
    public interface IMemory
    {
        byte Read8(int address);
        void Write8(int address, byte value);
        ushort Read16(int address);
        void Write16(int address, ushort value);
        //uint Read32(int address);
        //void Write32(int address, uint value);
        //ulong Read64(int address);
        //void Write64(int address, ulong value);
    }
}
