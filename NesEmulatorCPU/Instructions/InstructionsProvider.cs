using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions.Logic;

namespace NesEmulatorCPU.Instructions
{
    internal class InstructionsProvider
    {
        private readonly IInstruction[] instructions = new IInstruction[byte.MaxValue];

        internal InstructionsProvider()
        {
            RegisterInstruction(new InstructionBuilder().Opcode(0x69).Logic<ADC, Immediate>().Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Opcode(0x65).Logic<ADC, ZeroPage>().Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Opcode(0x75).Logic<ADC, ZeroPageX>().Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Opcode(0x6D).Logic<ADC, Absolute>().Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Opcode(0x7D).Logic<ADC, AbsoluteX>().Cycles(4).WithPageCrossing().Build());
        }

        private void RegisterInstruction(IInstruction instruction)
        {
            var opcode = instruction.Opcode;

            if (instructions[opcode] != null)
                throw new InvalidOperationException($"Instruction with opcode {opcode:X4} is already registered");

            instructions[opcode] = instruction;
        }

        internal IInstruction GetInstructionForOpcode(byte opcode) => instructions[opcode];
    }
}