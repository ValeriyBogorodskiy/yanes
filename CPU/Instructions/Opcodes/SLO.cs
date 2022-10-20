using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.CPU.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class SLO : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            // ASL
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var shiftedValue = (byte)(value << 1);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, shiftedValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, shiftedValue.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b1000_0000) > 0);

            bus.Write8Bit(valueAddress, shiftedValue);

            // ORA
            byte accumulatorValue = registers.Accumulator.State;

            var result = (byte)(shiftedValue | accumulatorValue);

            registers.Accumulator.State = result;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
        }
    }
}
