using EmulationKit.Abstractions;

namespace EmulationKit.UT88Min.Components
{
    internal class UtIO : IPorts
    {
        private byte _key = 0;
        public UtIO() { 
        }
        public byte In(byte address)
        {
            switch (address)
            {
                case 0xA0:
                    var result = _key;
                    _key = 0;
                    return result;
            }
            throw new NotImplementedException();
        }

        public void Out(byte port, byte dataByte)
        {
            switch (port)
            {
                case 0xA0:
                    return;
            }
            throw new NotImplementedException();
        }

        public void KeyPress(byte key)
        {
            _key = key;
        }
    }
}
