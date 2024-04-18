using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.Abstractions
{
    public interface IPorts
    {
        byte In(byte address);
        void Out(byte port, byte dataByte);
    }
}
