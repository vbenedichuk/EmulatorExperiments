namespace EmulationKit.i8080.Disasm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllBytes("C:\\Projects\\EmulationExpetiments\\Monitor-0.rom");
            var disassembler = new Disassembler();
            var appCode = disassembler.Disassemble(file, 0);
            var code = disassembler.GenerateCode(appCode);
            Console.WriteLine(code);
        }
    }
}
