using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions.Logic;

namespace NesEmulatorCPU.Instructions
{
    internal class InstructionsProvider
    {
        private readonly IInstruction[] instructions = new IInstruction[byte.MaxValue];
        /// <summary>
        /// http://www.6502.org/tutorials/6502opcodes.html
        /// </summary>
        internal InstructionsProvider()
        {
            RegisterInstruction(new InstructionBuilder().Logic<ADC, Immediate>().Opcode(0x69).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ADC, ZeroPage>().Opcode(0x65).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ADC, ZeroPageX>().Opcode(0x75).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ADC, Absolute>().Opcode(0x6D).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ADC, AbsoluteX>().Opcode(0x7D).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<ADC, AbsoluteY>().Opcode(0x79).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<ADC, IndirectX>().Opcode(0x61).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ADC, IndirectX>().Opcode(0x71).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<AND, Immediate>().Opcode(0x29).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, ZeroPage>().Opcode(0x25).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, ZeroPageX>().Opcode(0x35).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, Absolute>().Opcode(0x2D).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, AbsoluteX>().Opcode(0x3D).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, AbsoluteY>().Opcode(0x39).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, IndirectX>().Opcode(0x21).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, IndirectX>().Opcode(0x31).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<ASLAccumulator>().Opcode(0x0A).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, ZeroPage>().Opcode(0x06).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, ZeroPageX>().Opcode(0x16).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, Absolute>().Opcode(0x0E).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, AbsoluteX>().Opcode(0x1E).Cycles(7).Build());
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