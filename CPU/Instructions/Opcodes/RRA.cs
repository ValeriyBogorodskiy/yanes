using YaNES.CPU.AddressingModes;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;
using YaNES.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class RRA : ArithmeticInstructionLogic
    {
        protected override byte GetValue(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var carryMask = registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry) ? (byte)1 : (byte)0;
            var rotatedValue = (byte)(value >> 1 | carryMask << 7);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, rotatedValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, rotatedValue.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b0000_0001) > 0);

            bus.Write8Bit(valueAddress, rotatedValue);

            return rotatedValue;
        }
    }
}
