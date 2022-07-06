using NesEmulator.Instructions;

namespace NesEmulator.Cpu
{
    internal class Instructions
    {
        private readonly Instruction[] instructions = new Instruction[byte.MaxValue];

        internal Instructions(Registers registers, RAM ram)
        {
            CreateInstruction(new LDA(ram, registers.ProgramCounter, registers.Accumulator, registers.ProcessorStatus));
            CreateInstruction(new TAX(registers.Accumulator, registers.IndexRegisterX, registers.ProcessorStatus));
            CreateInstruction(new INX(registers.IndexRegisterX, registers.ProcessorStatus));
        }

        private void CreateInstruction(Instruction instruction)
        {
            var opcode = instruction.Opcode;

            if (instructions[opcode] != null)
                throw new InvalidOperationException();

            instructions[opcode] = instruction;
        }

        internal Instruction GetInstructionForOpcode(byte opcode) => instructions[opcode];
    }
}