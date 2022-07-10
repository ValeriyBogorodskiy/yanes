using NesEmulatorCPU.AddressingModes;

namespace NesEmulatorCPU.Instructions
{
    // TODO : naming
    internal class InstructionsProvider
    {
        private readonly Instruction[] instructions = new Instruction[byte.MaxValue];

        internal InstructionsProvider()
        {
            RegisterInstruction(new INX(0XE8));
            RegisterInstruction(new LDA<Immediate>(0xA9));
            RegisterInstruction(new TAX(0XAA));
        }

        private void RegisterInstruction(Instruction instruction)
        {
            var opcode = instruction.Opcode;

            if (instructions[opcode] != null)
                throw new InvalidOperationException();

            instructions[opcode] = instruction;
        }

        internal Instruction GetInstructionForOpcode(byte opcode) => instructions[opcode];
    }
}