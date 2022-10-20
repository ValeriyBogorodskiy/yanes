using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal abstract class Instruction : IInstruction
    {
        private readonly byte opcode;

        internal Instruction(byte opcode)
        {
            this.opcode = opcode;
        }

        byte IInstruction.Opcode => opcode;

        public abstract int Execute(Bus bus, RegistersProvider registers);
    }
}
