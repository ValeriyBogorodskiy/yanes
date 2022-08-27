using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class ADC : ArithmeticInstructionLogic
    {
        protected override byte GetValue(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            return addressingMode.GetRamValue(bus, registers);
        }
    }
}
