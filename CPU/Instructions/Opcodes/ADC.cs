using YaNES.CPU.AddressingModes;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class ADC : ArithmeticInstructionLogic
    {
        protected override byte GetValue(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            return addressingMode.GetRamValue(bus, registers);
        }
    }
}
