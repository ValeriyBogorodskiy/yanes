using YaNES.CPU.AddressingModes;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class INS : ArithmeticInstructionLogic
    {
        protected override byte GetValue(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var newValue = (byte)(value + 1);

            bus.Write8Bit(valueAddress, newValue);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, newValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, newValue.IsZero());

            var onesComplement = (byte)~newValue;
            return onesComplement;
        }
    }
}
