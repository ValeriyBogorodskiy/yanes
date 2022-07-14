using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class TAX : IInstructionLogic
    {
        void IInstructionLogic.Execute(RAM ram, RegistersProvider registers)
        {
            var value = registers.Accumulator.State;

            registers.IndexRegisterX.State = value;
            registers.ProcessorStatus.UpdateNegativeFlag(value);
            registers.ProcessorStatus.UpdateZeroFlag(value);
        }
    }
}
