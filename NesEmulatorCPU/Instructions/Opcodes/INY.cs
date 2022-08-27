using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class INY : IInstructionLogic
    {
        void IInstructionLogic.Execute(Bus bus, RegistersProvider registers)
        {
            byte value = (byte)(registers.IndexRegisterY.State + 1);

            registers.IndexRegisterY.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());
        }
    }
}
