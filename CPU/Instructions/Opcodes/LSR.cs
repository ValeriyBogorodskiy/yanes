using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal abstract class LSR
    {
        protected static byte Shift(byte value, RegistersProvider registers)
        {
            var result = (byte)(value >> 1);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b0000_0001) > 0);

            return result;
        }
    }

    internal class LSRAccumulator : LSR, IInstructionLogic
    {
        void IInstructionLogic.Execute(Bus bus, RegistersProvider registers)
        {
            var accumulatorValue = registers.Accumulator.State;
            var shiftedAccumulatorValue = Shift(accumulatorValue, registers);
            registers.Accumulator.State = shiftedAccumulatorValue;
        }
    }

    internal class LSRWithAddressing : LSR, IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var ramValueAddress = addressingMode.GetRamAddress(bus, registers);
            var ramValue = bus.Read8bit(ramValueAddress);
            var shiftedRamValue = Shift(ramValue, registers);
            bus.Write8Bit(ramValueAddress, shiftedRamValue);
        }
    }
}
