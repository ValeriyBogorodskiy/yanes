using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Tools;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class INX : IInstructionLogic
    {
        void IInstructionLogic.Execute(RAM ram, RegistersProvider registers)
        {
            byte value = (byte)(registers.IndexRegisterX.State + 1);

            registers.IndexRegisterX.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());
        }
    }
}
