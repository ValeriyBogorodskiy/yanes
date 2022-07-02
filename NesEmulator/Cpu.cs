using NesEmulator.Registers;

namespace NesEmulator
{
    internal class Cpu
    {
        private ProgramCounter programCounter = new();
        private StackPointer stackPointer = new();
        private Accumulator accumulator = new();
        private IndexRegisterX indexRegisterX = new();
        private IndexRegisterY indexRegisterY = new();
        private ProcessorStatus processorStatus = new();

        public void Run(Opcode[] program)
        {
            while (true)
            {
                var opcode = program[programCounter.NextInstructionAddress];
                programCounter.Increment();

                if (opcode.Instruction == Opcodes.BRK)
                    break;

                var instruction = OpcodeToInstruction(opcode.Instruction);
                instruction.Execute();
            }
        }

        private Instruction OpcodeToInstruction(Opcodes opcode)
        {
            return null;
        }
    }
}
