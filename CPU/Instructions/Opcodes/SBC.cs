using YaNES.CPU.AddressingModes;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class SBC : ArithmeticInstructionLogic
    {
        /// <summary>
        /// http://www.righto.com/2012/12/the-6502-overflow-flag-explained.html
        /// Section - Subtraction on the 6502
        /// </summary>
        protected override byte GetValue(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(bus, registers);
            var onesComplement = (byte)~value;
            return onesComplement;
        }
    }
}
