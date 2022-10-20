using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class SRE : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            // LSR
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var shiftedValue = (byte)(value >> 1);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, shiftedValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, shiftedValue.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b0000_0001) > 0);

            bus.Write8Bit(valueAddress, shiftedValue);

            // EOR
            var result = (byte)(shiftedValue ^ registers.Accumulator.State);

            registers.Accumulator.State = result;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
        }
    }
}
