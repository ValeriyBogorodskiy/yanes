using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class RLA : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            // ROL
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var carryMask = registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry) ? (byte)1 : (byte)0;
            var rotatedValue = (byte)(value << 1 | carryMask);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, rotatedValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, rotatedValue.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b10000000) > 0);

            bus.Write8Bit(valueAddress, rotatedValue);

            // AND
            var result = (byte)(rotatedValue & registers.Accumulator.State);

            registers.Accumulator.State = result;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
        }
    }
}
