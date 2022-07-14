using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class STA : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var memoryAddress = addressingMode.GetAddress(ram, registers);
            ram.Write8Bit(memoryAddress, registers.Accumulator.State);
        }
    }
}
