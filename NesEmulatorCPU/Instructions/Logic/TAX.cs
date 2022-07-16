using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Tools;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class TAX : IInstructionLogic
    {
        void IInstructionLogic.Execute(RAM ram, RegistersProvider registers)
        {
            var value = registers.Accumulator.State;

            registers.IndexRegisterX.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());
        }
    }
}
