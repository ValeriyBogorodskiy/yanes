using NesEmulator.Registers;

namespace NesEmulator.Instructions
{
    internal class INX : Instruction
    {
        private readonly IndexRegisterX indexRegisterX;
        private readonly ProcessorStatus processorStatus;

        internal INX(IndexRegisterX indexRegisterX, ProcessorStatus processorStatus)
        {
            this.indexRegisterX = indexRegisterX;
            this.processorStatus = processorStatus;
        }

        internal override byte Opcode => 0XE8;

        internal override void Execute()
        {
            byte value = (byte)(indexRegisterX.State + 1);

            indexRegisterX.Load(value);
            processorStatus.UpdateNegativeFlag(value);
            processorStatus.UpdateZeroFlag(value);
        }
    }
}
