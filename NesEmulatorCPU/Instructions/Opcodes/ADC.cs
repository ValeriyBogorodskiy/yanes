using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class ADC : ArithmeticInstructionLogic
    {
        protected override byte GetValue(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            return addressingMode.GetRamValue(ram, registers);
        }
    }
}
