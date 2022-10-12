using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions.Opcodes;

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
            RegisterOfficialInstructions();
            RegisterUndocumentedInstructions();
        }

        private void RegisterOfficialInstructions()
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

            RegisterInstruction(new BPL(0x10));
            RegisterInstruction(new BMI(0x30));
            RegisterInstruction(new BVC(0x50));
            RegisterInstruction(new BVS(0x70));
            RegisterInstruction(new BCC(0x90));
            RegisterInstruction(new BCS(0xB0));
            RegisterInstruction(new BNE(0xD0));
            RegisterInstruction(new BEQ(0xF0));

            RegisterInstruction(new InstructionBuilder().Logic<CMP, Immediate>().Opcode(0xC9).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, ZeroPage>().Opcode(0xC5).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, ZeroPageX>().Opcode(0xD5).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, Absolute>().Opcode(0xCD).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, AbsoluteX>().Opcode(0xDD).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, AbsoluteY>().Opcode(0xD9).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, IndirectX>().Opcode(0xC1).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CMP, IndirectY>().Opcode(0xD1).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<CPX, Immediate>().Opcode(0xE0).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPX, ZeroPage>().Opcode(0xE4).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPX, Absolute>().Opcode(0xEC).Cycles(4).Build());

            RegisterInstruction(new InstructionBuilder().Logic<CPY, Immediate>().Opcode(0xC0).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPY, ZeroPage>().Opcode(0xC4).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<CPY, Absolute>().Opcode(0xCC).Cycles(4).Build());

            RegisterInstruction(new InstructionBuilder().Logic<DEC, ZeroPage>().Opcode(0xC6).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DEC, ZeroPageX>().Opcode(0xD6).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DEC, Absolute>().Opcode(0xCE).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DEC, AbsoluteX>().Opcode(0xDE).Cycles(7).Build());

            RegisterInstruction(new InstructionBuilder().Logic<EOR, Immediate>().Opcode(0x49).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, ZeroPage>().Opcode(0x45).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, ZeroPageX>().Opcode(0x55).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, Absolute>().Opcode(0x4D).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, AbsoluteX>().Opcode(0x5D).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, AbsoluteY>().Opcode(0x59).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, IndirectX>().Opcode(0x41).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<EOR, IndirectY>().Opcode(0x51).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new CLC(0x18));
            RegisterInstruction(new SEC(0x38));
            RegisterInstruction(new CLI(0x58));
            RegisterInstruction(new SEI(0x78));
            RegisterInstruction(new CLV(0xB8));
            RegisterInstruction(new CLD(0xD8));
            RegisterInstruction(new SED(0xF8));

            RegisterInstruction(new InstructionBuilder().Logic<INC, ZeroPage>().Opcode(0xE6).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INC, ZeroPageX>().Opcode(0xF6).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INC, Absolute>().Opcode(0xEE).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INC, AbsoluteX>().Opcode(0xFE).Cycles(7).Build());

            RegisterInstruction(new InstructionBuilder().Logic<JMP, Absolute>().Opcode(0x4C).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<JMP, Indirect>().Opcode(0x6C).Cycles(5).Build());

            RegisterInstruction(new InstructionBuilder().Logic<JSR, Absolute>().Opcode(0x20).Cycles(6).Build());

            RegisterInstruction(new InstructionBuilder().Logic<LDA, Immediate>().Opcode(0xA9).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDA, ZeroPage>().Opcode(0xA5).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDA, ZeroPageX>().Opcode(0xB5).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDA, Absolute>().Opcode(0xAD).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDA, AbsoluteX>().Opcode(0xBD).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDA, AbsoluteY>().Opcode(0xB9).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDA, IndirectX>().Opcode(0xA1).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDA, IndirectY>().Opcode(0xB1).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<LDX, Immediate>().Opcode(0xA2).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDX, ZeroPage>().Opcode(0xA6).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDX, ZeroPageY>().Opcode(0xB6).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDX, Absolute>().Opcode(0xAE).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDX, AbsoluteY>().Opcode(0xBE).Cycles(4).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<LDY, Immediate>().Opcode(0xA0).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDY, ZeroPage>().Opcode(0xA4).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDY, ZeroPageX>().Opcode(0xB4).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDY, Absolute>().Opcode(0xAC).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LDY, AbsoluteX>().Opcode(0xBC).Cycles(4).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<LSRAccumulator>().Opcode(0x4A).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LSRWithAddressing, ZeroPage>().Opcode(0x46).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LSRWithAddressing, ZeroPageX>().Opcode(0x56).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LSRWithAddressing, Absolute>().Opcode(0x4E).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LSRWithAddressing, AbsoluteX>().Opcode(0x5E).Cycles(7).Build());

            RegisterInstruction(new NOP(0xEA));

            RegisterInstruction(new InstructionBuilder().Logic<ORA, Immediate>().Opcode(0x09).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ORA, ZeroPage>().Opcode(0x05).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ORA, ZeroPageX>().Opcode(0x15).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ORA, Absolute>().Opcode(0x0D).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ORA, AbsoluteX>().Opcode(0x1D).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<ORA, AbsoluteY>().Opcode(0x19).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<ORA, IndirectX>().Opcode(0x01).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ORA, IndirectY>().Opcode(0x11).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new TAX(0xAA));
            RegisterInstruction(new TXA(0x8A));
            RegisterInstruction(new InstructionBuilder().Logic<DEX>().Opcode(0xCA).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INX>().Opcode(0xE8).Cycles(2).Build());
            RegisterInstruction(new TAY(0xA8));
            RegisterInstruction(new TYA(0x98));
            RegisterInstruction(new InstructionBuilder().Logic<DEY>().Opcode(0x88).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<INY>().Opcode(0xC8).Cycles(2).Build());

            RegisterInstruction(new InstructionBuilder().Logic<ROLAccumulator>().Opcode(0x2A).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ROLWithAddressing, ZeroPage>().Opcode(0x26).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ROLWithAddressing, ZeroPageX>().Opcode(0x36).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ROLWithAddressing, Absolute>().Opcode(0x2E).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<ROLWithAddressing, AbsoluteX>().Opcode(0x3E).Cycles(7).Build());

            RegisterInstruction(new InstructionBuilder().Logic<RORAccumulator>().Opcode(0x6A).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<RORWithAddressing, ZeroPage>().Opcode(0x66).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<RORWithAddressing, ZeroPageX>().Opcode(0x76).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<RORWithAddressing, Absolute>().Opcode(0x6E).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<RORWithAddressing, AbsoluteX>().Opcode(0x7E).Cycles(7).Build());

            RegisterInstruction(new RTI(0x40));
            RegisterInstruction(new RTS(0x60));

            RegisterInstruction(new InstructionBuilder().Logic<SBC, Immediate>().Opcode(0xE9).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<SBC, ZeroPage>().Opcode(0xE5).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<SBC, ZeroPageX>().Opcode(0xF5).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<SBC, Absolute>().Opcode(0xED).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<SBC, AbsoluteX>().Opcode(0xFD).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<SBC, AbsoluteY>().Opcode(0xF9).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<SBC, IndirectX>().Opcode(0xE1).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<SBC, IndirectY>().Opcode(0xF1).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<STA, ZeroPage>().Opcode(0x85).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STA, ZeroPageX>().Opcode(0x95).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STA, Absolute>().Opcode(0x8D).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STA, AbsoluteX>().Opcode(0x9D).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STA, AbsoluteY>().Opcode(0x99).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STA, IndirectX>().Opcode(0x81).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STA, IndirectY>().Opcode(0x91).Cycles(6).Build());

            RegisterInstruction(new TXS(0x9A));
            RegisterInstruction(new TSX(0xBA));
            RegisterInstruction(new PHA(0x48));
            RegisterInstruction(new PLA(0x68));
            RegisterInstruction(new PHP(0x08));
            RegisterInstruction(new PLP(0x28));

            RegisterInstruction(new InstructionBuilder().Logic<STX, ZeroPage>().Opcode(0x86).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STX, ZeroPageY>().Opcode(0x96).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STX, Absolute>().Opcode(0x8E).Cycles(4).Build());

            RegisterInstruction(new InstructionBuilder().Logic<STY, ZeroPage>().Opcode(0x84).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STY, ZeroPageX>().Opcode(0x94).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<STY, Absolute>().Opcode(0x8C).Cycles(4).Build());
        }

        private void RegisterUndocumentedInstructions()
        {
            RegisterInstruction(new NOP(0x1A));
            RegisterInstruction(new NOP(0x3A));
            RegisterInstruction(new NOP(0x5A));
            RegisterInstruction(new NOP(0x7A));
            RegisterInstruction(new NOP(0xDA));
            RegisterInstruction(new NOP(0xFA));

            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPage>().Opcode(0x04).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPageX>().Opcode(0x14).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPageX>().Opcode(0x34).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPage>().Opcode(0x44).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPageX>().Opcode(0x54).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPage>().Opcode(0x64).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPageX>().Opcode(0x74).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, Immediate>().Opcode(0x80).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, Immediate>().Opcode(0x82).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, Immediate>().Opcode(0x89).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, Immediate>().Opcode(0xC2).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPageX>().Opcode(0xD4).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, Immediate>().Opcode(0xE2).Cycles(2).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DOP, ZeroPageX>().Opcode(0xF4).Cycles(4).Build());

            RegisterInstruction(new InstructionBuilder().Logic<TOP, Absolute>().Opcode(0x0C).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<TOP, AbsoluteX>().Opcode(0x1C).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<TOP, AbsoluteX>().Opcode(0x3C).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<TOP, AbsoluteX>().Opcode(0x5C).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<TOP, AbsoluteX>().Opcode(0x7C).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<TOP, AbsoluteX>().Opcode(0xDC).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<TOP, AbsoluteX>().Opcode(0xFC).Cycles(4).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<LAX, ZeroPage>().Opcode(0xA7).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LAX, ZeroPageY>().Opcode(0xB7).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LAX, Absolute>().Opcode(0xAF).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LAX, AbsoluteY>().Opcode(0xBF).Cycles(4).WithPageCrossing().Build());
            RegisterInstruction(new InstructionBuilder().Logic<LAX, IndirectX>().Opcode(0xA3).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<LAX, IndirectY>().Opcode(0xB3).Cycles(5).WithPageCrossing().Build());

            RegisterInstruction(new InstructionBuilder().Logic<AAX, ZeroPage>().Opcode(0x87).Cycles(3).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AAX, ZeroPageY>().Opcode(0x97).Cycles(4).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AAX, IndirectX>().Opcode(0x83).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<AAX, Absolute>().Opcode(0x8F).Cycles(4).Build());

            RegisterInstruction(new InstructionBuilder().Logic<SBC, Immediate>().Opcode(0xEB).Cycles(2).Build());

            RegisterInstruction(new InstructionBuilder().Logic<DCP, ZeroPage>().Opcode(0xC7).Cycles(5).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DCP, ZeroPageX>().Opcode(0xD7).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DCP, Absolute>().Opcode(0xCF).Cycles(6).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DCP, AbsoluteX>().Opcode(0xDF).Cycles(7).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DCP, AbsoluteY>().Opcode(0xDB).Cycles(7).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DCP, IndirectX>().Opcode(0xC3).Cycles(8).Build());
            RegisterInstruction(new InstructionBuilder().Logic<DCP, IndirectY>().Opcode(0xD3).Cycles(8).Build());
        }

        private void RegisterInstruction(IInstruction instruction)
        {
            var opcode = instruction.Opcode;

            if (instructions[opcode] != null)
                throw new InvalidOperationException($"Instruction with opcode {opcode:X2} is already registered");

            instructions[opcode] = instruction;
        }

        internal IInstruction GetInstructionForOpcode(byte opcode)
        {
            var instruction = instructions[opcode];

            if (instruction == null)
                throw new NullReferenceException($"Unknown opcode {opcode:X2}");

            return instruction;
        }
    }
}