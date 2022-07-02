using NesEmulator.Instructions;
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

        // TODO: i don't like this abstraction
        private LoadedProgram loadedProgram;

        public void Run(byte[] rawProgram)
        {
            loadedProgram = new LoadedProgram(rawProgram);

            while (true)
            {
                var nextInstructionAddress = programCounter.Fetch();
                var rawOpcode = loadedProgram.Fetch(nextInstructionAddress);
                var opcode = (Opcodes)rawOpcode;

                programCounter.Increment();

                if (opcode == Opcodes.BRK)
                    break;

                var instruction = Decode(opcode);
                instruction.Execute();
            }
        }

        // TODO: cache instructions to reduce GC utilization
        private Instruction Decode(Opcodes opcode) => opcode switch
        {
            Opcodes.LDA => new LDA(loadedProgram, programCounter, accumulator, processorStatus),
            _ => throw new ArgumentException($"Unhandled opcode - {Enum.GetName(typeof(Opcodes), opcode)}")
        };
    }
}
