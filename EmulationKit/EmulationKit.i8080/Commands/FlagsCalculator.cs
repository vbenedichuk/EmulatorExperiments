using System;
using System.Collections.Generic;
using System.Text;

namespace EmulationKit.i8080.Commands
{
    internal static class FlagsCalculator
    {

        public static bool GetFlagP(byte v)
        {
            int count = 0;
            while (v > 0)
            {
                if (v % 2 == 1)
                {
                    count++;
                }
                v = (byte)(v / 2);
            }
            return count % 2 == 0;
        }

        public static bool GetFlagZ(byte v)
        {
            return v == 0;
        }

        public static bool GetFlagS(byte v)
        {
            return (v & 0b10000000) == 0b10000000;
        }


        public static bool GetFlagACAdd(byte a, byte b)
        {
            return (a % 16 + b % 16) > 15;
        }
        public static bool GetFlagACSub(byte a, byte b, byte cy)
        {
            return (a % 16 - b % 16 - cy) < 0;
        }
    }
}
