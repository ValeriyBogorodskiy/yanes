using NesEmulator.Cpu;
using NesEmulator.Registers;

namespace NesEmulator.Instructions
{
    internal class LDA : Instruction
    {
        private readonly RAM ram;
        private readonly ProgramCounter programCounter;
        private readonly Accumulator accumulator;
        private readonly ProcessorStatus processorStatus;

        internal LDA(RAM ram, ProgramCounter programCounter, Accumulator accumulator, ProcessorStatus processorStatus)
        {
            this.ram = ram;
            this.programCounter = programCounter;
            this.accumulator = accumulator;
            this.processorStatus = processorStatus;
        }

        internal override byte Opcode => 0xA9;

        internal override void Execute()
        {
            var valueAddress = programCounter.State;
            var value = ram.Read8bit(valueAddress);

            accumulator.Load(value);
            programCounter.Increment();
            processorStatus.UpdateNegativeFlag(value);
            processorStatus.UpdateZeroFlag(value);
        }
    }
}
