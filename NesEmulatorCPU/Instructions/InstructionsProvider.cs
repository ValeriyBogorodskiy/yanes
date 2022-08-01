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
            RegisterInstruction(new InstructionBuilder().Logic<ADC, IndirectY>().Opcode(0x71).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<AND, Immediate>().Opcode(0x29).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, ZeroPage>().Opcode(0x25).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, ZeroPageX>().Opcode(0x35).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, Absolute>().Opcode(0x2D).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, AbsoluteX>().Opcode(0x3D).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, AbsoluteY>().Opcode(0x39).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, IndirectX>().Opcode(0x21).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AND, IndirectY>().Opcode(0x31).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<ASLAccumulator>().Opcode(0x0A).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, ZeroPage>().Opcode(0x06).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, ZeroPageX>().Opcode(0x16).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, Absolute>().Opcode(0x0E).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ASLWithAddressing, AbsoluteX>().Opcode(0x1E).Cycles(7).Build());

            RegisterInstruction(new InstructionBuilder().Logic<BIT, ZeroPage>().Opcode(0x24).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<BIT, Absolute>().Opcode(0x2C).Cycles(4).Build());

            RegisterInstruction(new BPL(0X10));
            RegisterInstruction(new BMI(0X30));
            RegisterInstruction(new BVC(0X50));
            RegisterInstruction(new BVS(0X70));
            RegisterInstruction(new BCC(0X90));
            RegisterInstruction(new BCS(0XB0));
            RegisterInstruction(new BNE(0XD0));
            RegisterInstruction(new BEQ(0XF0));

            RegisterInstruction(new InstructionBuilder().Logic<CMP, Immediate>().Opcode(0XC9).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, ZeroPage>().Opcode(0XC5).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, ZeroPageX>().Opcode(0XD5).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, Absolute>().Opcode(0XCD).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, AbsoluteX>().Opcode(0XDD).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, AbsoluteY>().Opcode(0XD9).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, IndirectX>().Opcode(0XC1).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, IndirectY>().Opcode(0XD1).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<CPX, Immediate>().Opcode(0XE0).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPX, ZeroPage>().Opcode(0XE4).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPX, Absolute>().Opcode(0XEC).Cycles(4).Build());

            RegisterInstruction(new InstructionBuilder().Logic<CPY, Immediate>().Opcode(0XC0).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPY, ZeroPage>().Opcode(0XC4).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPY, Absolute>().Opcode(0XCC).Cycles(4).Build());

            RegisterInstruction(new InstructionBuilder().Logic<DEC, ZeroPage>().Opcode(0XC6).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DEC, ZeroPageX>().Opcode(0XD6).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DEC, Absolute>().Opcode(0XCE).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DEC, AbsoluteX>().Opcode(0XDE).Cycles(7).Build());

            RegisterInstruction(new InstructionBuilder().Logic<EOR, Immediate>().Opcode(0X49).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, ZeroPage>().Opcode(0X45).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, ZeroPageX>().Opcode(0X55).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, Absolute>().Opcode(0X4D).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, AbsoluteX>().Opcode(0X5D).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, AbsoluteY>().Opcode(0X59).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, IndirectX>().Opcode(0X41).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, IndirectY>().Opcode(0X51).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new CLC(0X18));
            RegisterInstruction(new SEC(0X38));
            RegisterInstruction(new CLI(0X58));
            RegisterInstruction(new SEI(0X78));
            RegisterInstruction(new CLV(0XB8));
            RegisterInstruction(new CLD(0XD8));
            RegisterInstruction(new SED(0XF8));

            RegisterInstruction(new InstructionBuilder().Logic<INC, ZeroPage>().Opcode(0XE6).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INC, ZeroPageX>().Opcode(0XF6).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INC, Absolute>().Opcode(0XEE).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INC, AbsoluteX>().Opcode(0XFE).Cycles(7).Build());

            RegisterInstruction(new InstructionBuilder().Logic<JMP, Absolute>().Opcode(0X4C).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<JMP, Indirect>().Opcode(0X6C).Cycles(5).Build());
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