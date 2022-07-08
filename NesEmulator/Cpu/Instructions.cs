using NesEmulator.AddressingModes;
using NesEmulator.Instructions;

namespace NesEmulator.Cpu
{
    internal class Instructions
    {
        private readonly Instruction[] instructions = new Instruction[byte.MaxValue];

        internal Instructions()
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