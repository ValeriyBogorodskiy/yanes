using NesEmulator.Registers;

namespace NesEmulator.Instructions
{
    internal class TAX : Instruction
    {
        private readonly Accumulator accumulator;
        private readonly IndexRegisterX indexRegisterX;
        private readonly ProcessorStatus processorStatus;

        internal TAX(Accumulator accumulator, IndexRegisterX indexRegisterX, ProcessorStatus processorStatus)
        {
            this.accumulator = accumulator;
            this.indexRegisterX = indexRegisterX;
            this.processorStatus = processorStatus;
        }

        internal override void Execute()
        {
            var value = accumulator.State;

            indexRegisterX.Load(value);
            processorStatus.UpdateNegativeFlag(value);
            processorStatus.UpdateZeroFlag(value);
        }
    }
}
