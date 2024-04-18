namespace EmulationKit.i8080.Disasm
{
    internal class InstructionDescriptor
    {
        public enum InstructionArgumentType { None, D8, D16, Address } 
        public string Mnemonic { get; set; }
        public int Length { get
            {
                switch(ArgumentType)
                {
                    case InstructionArgumentType.D8: return 2;
                    case InstructionArgumentType.D16: return 3;
                    case InstructionArgumentType.Address: return 3;
                }
                return 1;
            }
        }
        public InstructionArgumentType ArgumentType { get; set; } = InstructionArgumentType.None;
        public string? Comment { get; set; }
    }
}
