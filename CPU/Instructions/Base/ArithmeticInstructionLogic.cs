using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.Utils;

namespace YaNES.CPU.Instructions.Base
{
    internal abstract class ArithmeticInstructionLogic : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var value = GetValue(addressingMode, bus, registers);
            Perform(value, bus, registers);
        }

        protected abstract byte GetValue(AddressingMode addressingMode, Bus bus, RegistersProvider registers);

        private void Perform(byte value, Bus bus, RegistersProvider registers)
        {
            var accumulatorState = registers.Accumulator.State;
            var carryIn = registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry) ? 1 : 0;

            var result = value + accumulatorState + carryIn;
            var byteResult = (byte)result;

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, byteResult.IsNegative());

            var overflowOccured = (value & BitMasks.Negative) == (accumulatorState & BitMasks.Negative) && (value & BitMasks.Negative) != (result & BitMasks.Negative);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Overflow, overflowOccured);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, byteResult.IsZero());

            var carryOccured = (result & BitMasks.CarryBit) == BitMasks.CarryBit;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, carryOccured);

            registers.Accumulator.State = byteResult;
        }
    }
}
