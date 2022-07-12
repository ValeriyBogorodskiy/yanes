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

            RegisterInstruction(new LDA<Absolute>(0xAD));
            RegisterInstruction(new LDA<AbsoluteX>(0xBD));
            RegisterInstruction(new LDA<AbsoluteY>(0xB9));
            RegisterInstruction(new LDA<Immediate>(0xA9));
            RegisterInstruction(new LDA<IndirectX>(0xA1));
            RegisterInstruction(new LDA<IndirectY>(0xB1));
            RegisterInstruction(new LDA<ZeroPage>(0xA5));
            RegisterInstruction(new LDA<ZeroPageX>(0xB5));

            RegisterInstruction(new STA<Absolute>(0x8D));
            RegisterInstruction(new STA<AbsoluteX>(0x9D));
            RegisterInstruction(new STA<AbsoluteY>(0x99));
            RegisterInstruction(new STA<IndirectX>(0x81));
            RegisterInstruction(new STA<IndirectY>(0x11));
            RegisterInstruction(new STA<ZeroPage>(0x85));
            RegisterInstruction(new STA<ZeroPageX>(0x95));

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