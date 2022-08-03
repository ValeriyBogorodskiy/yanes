using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class DEC : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetRamAddress(ram, registers);
            var value = ram.Read8bit(valueAddress);
            var newValue = (byte)(value - 1);

            ram.Write8Bit(valueAddress, newValue);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, newValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, newValue.IsZero());
        }
    }
}
