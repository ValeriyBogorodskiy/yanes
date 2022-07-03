using NesEmulator.Registers;
using NesEmulator.Tools;

namespace NesEmulator.Instructions
{
    internal class LDA : Instruction
    {
        private readonly LoadedProgram loadedProgram;
        private readonly ProgramCounter programCounter;
        private readonly Accumulator accumulator;
        private readonly ProcessorStatus processorStatus;

        // TODO: reduce number of arguments?
        internal LDA(LoadedProgram loadedProgram, ProgramCounter programCounter, Accumulator accumulator, ProcessorStatus processorStatus)
        {
            this.loadedProgram = loadedProgram;
            this.programCounter = programCounter;
            this.accumulator = accumulator;
            this.processorStatus = processorStatus;
        }

        internal override void Execute()
        {
            var valueAddress = programCounter.Fetch();
            var value = loadedProgram.Fetch(valueAddress);

            accumulator.Load(value);
            programCounter.Increment();

            var isValueZero = value == 0;
            var isValueNegative = (value & BitMasks.Negative) != 0;

            processorStatus.Set(ProcessorStatus.Flags.Zero, isValueZero);
            processorStatus.Set(ProcessorStatus.Flags.Negative, isValueNegative);
        }
    }
}
