using NesEmulatorCPU.AddressingModes;

namespace NesEmulatorCPU.Instructions
{
    internal class InstructionsProvider
    {
        private readonly IInstruction[] instructions = new IInstruction[byte.MaxValue];

        internal InstructionsProvider()
        {
            RegisterInstruction(new InstructionsBuilder().Opcode(0XE8).Logic<INX>().Cycles(2).Build());

            RegisterInstruction(new InstructionsBuilder().Opcode(0xAD).Logic<LDA, Absolute>().Cycles(4).Build());
            RegisterInstruction(new InstructionsBuilder().Opcode(0xBD).Logic<LDA, AbsoluteX>().Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionsBuilder().Opcode(0xB9).Logic<LDA, AbsoluteY>().Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionsBuilder().Opcode(0xA9).Logic<LDA, Immediate>().Cycles(2).Build());
            RegisterInstruction(new InstructionsBuilder().Opcode(0xA1).Logic<LDA, IndirectX>().Cycles(6).Build());
            RegisterInstruction(new InstructionsBuilder().Opcode(0xB1).Logic<LDA, IndirectY>().Cycles(5).WithPageCrossing().Build());
            RegisterInstruction(new InstructionsBuilder().Opcode(0xA5).Logic<LDA, ZeroPage>().Cycles(3).Build());
            RegisterInstruction(new InstructionsBuilder().Opcode(0xB5).Logic<LDA, ZeroPageX>().Cycles(4).Build());

            RegisterInstruction(new InstructionsBuilder().Opcode(0XAA).Logic<TAX>().Cycles(2).Build());
        }

        private void RegisterInstruction(IInstruction instruction)
        {
            var opcode = instruction.Opcode;

            if (instructions[opcode] != null)
                throw new InvalidOperationException();

            instructions[opcode] = instruction;
        }

        internal IInstruction GetInstructionForOpcode(byte opcode) => instructions[opcode];
    }
}