using NesEmulator.Instructions;
using NesEmulator.Registers;

namespace NesEmulator.Cpu
{
    // TODO: i have ICpu to hide implementation but how can i create Cpu instance without making class public (abstract factory?) 
    public class Cpu : ICpu
    {
        // TODO: added this for testing and maybe debugging. Not sure if this is a good decision
        public IReadOnlyCpuRegister16Bit ProgramCounter => programCounter;
        public IReadOnlyCpuRegister8Bit StackPointer => stackPointer;
        public IReadOnlyCpuRegister8Bit Accumulator => accumulator;
        public IReadOnlyCpuRegister8Bit IndexRegisterX => indexRegisterX;
        public IReadOnlyCpuRegister8Bit IndexRegisterY => indexRegisterY;
        public IReadOnlyCpuRegister8Bit ProcessorStatus => processorStatus;

        private ProgramCounter programCounter = new();
        private StackPointer stackPointer = new();
        private Accumulator accumulator = new();
        private IndexRegisterX indexRegisterX = new();
        private IndexRegisterY indexRegisterY = new();
        private ProcessorStatus processorStatus = new();

        // TODO: i don't like this abstraction
        private LoadedProgram loadedProgram;

        public void Run(byte[] program)
        {
            loadedProgram = new LoadedProgram(program);

            while (true)
            {
                var nextInstructionAddress = programCounter.Fetch();
                var rawOpcode = loadedProgram.Fetch(nextInstructionAddress);

                programCounter.Increment();

                if (rawOpcode == (byte)Opcodes.BRK)
                    break;

                var instruction = Decode(rawOpcode);
                instruction.Execute();
            }
        }

        // TODO: cache instructions to reduce GC utilization
        // TODO: add opcode value to Instruction class and create dictionary on startup (?)
        private Instruction Decode(byte rawOpcode) => (Opcodes)rawOpcode switch
        {
            Opcodes.LDA => new LDA(loadedProgram, programCounter, accumulator, processorStatus),
            Opcodes.TAX => new TAX(accumulator, indexRegisterX, processorStatus),
            Opcodes.INX => new INX(indexRegisterX, processorStatus),
            _ => throw new ArgumentException($"Unhandled opcode - {rawOpcode:X}")
        };
    }
}
