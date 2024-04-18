using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationKit.i8080.Disasm
{
    internal class AppStructure
    {
        public HashSet<ushort> AddressSet { get; private set; } = new HashSet<ushort>();
        public Dictionary<ushort, byte[]> Lines { get; private set; } = new Dictionary<ushort, byte[]>();

    }
}
