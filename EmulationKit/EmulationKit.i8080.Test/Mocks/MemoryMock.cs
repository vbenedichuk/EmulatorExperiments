using EmulationKit.Abstractions;

namespace EmulationKit.i8080.Test.Mocks
{
    internal class MemoryMock : IMemory
    {
        private byte[] _memoryBytes;

        public MemoryMock(byte[] memoryBytes) 
        { 
            this._memoryBytes = memoryBytes;
        }

        public ushort Read16(int address)
        {
            throw new NotImplementedException();
        }

        public byte Read8(int address)
        {
            return _memoryBytes[address];
        }

        public void Write16(int address, ushort value)
        {
            throw new NotImplementedException();
        }

        public void Write8(int address, byte value)
        {
            _memoryBytes[address] = value;
        }
    }
}
