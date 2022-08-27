using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class BIT : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(bus, registers);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, (value & 0b10000000) > 0);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Overflow, (value & 0b01000000) > 0);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, (value & registers.Accumulator.State) == 0);
        }
    }
}
