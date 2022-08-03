using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class LDX : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(ram, registers);

            registers.IndexRegisterX.State = value;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, value.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, value.IsZero());
        }
    }
}
