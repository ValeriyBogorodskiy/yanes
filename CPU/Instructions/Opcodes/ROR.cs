using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal abstract class ROR
    {
        protected static byte Rotate(byte value, RegistersProvider registers)
        {
            var carryMask = registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry) ? (byte)1 : (byte)0;
            var result = (byte)(value >> 1 | carryMask << 7);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b0000_0001) > 0);

            return result;
        }
    }

    internal class RORAccumulator : ROR, IInstructionLogic
    {
        void IInstructionLogic.Execute(Bus bus, RegistersProvider registers)
        {
            var accumulatorValue = registers.Accumulator.State;
            var shiftedAccumulatorValue = Rotate(accumulatorValue, registers);
            registers.Accumulator.State = shiftedAccumulatorValue;
        }
    }

    internal class RORWithAddressing : ROR, IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var ramValueAddress = addressingMode.GetRamAddress(bus, registers);
            var ramValue = bus.Read8bit(ramValueAddress);
            var shiftedRamValue = Rotate(ramValue, registers);
            bus.Write8Bit(ramValueAddress, shiftedRamValue);
        }
    }
}
