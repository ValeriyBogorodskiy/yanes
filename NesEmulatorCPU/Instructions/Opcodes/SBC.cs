using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class SBC : ArithmeticInstructionLogic
    {
        /// <summary>
        /// http://www.righto.com/2012/12/the-6502-overflow-flag-explained.html
        /// Section - Subtraction on the 6502
        /// </summary>
        protected override byte GetValue(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(ram, registers);
            var onesComplement = (byte)~value;
            return onesComplement;
        }
    }
}
