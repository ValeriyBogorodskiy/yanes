namespace NesEmulator
{
    internal class Opcode
    {
        private readonly Opcodes instruction;

        public Opcode(byte rawInstruction)
        {
            instruction = (Opcodes)rawInstruction;
        }

        public Opcodes Instruction => instruction;
    }
}
