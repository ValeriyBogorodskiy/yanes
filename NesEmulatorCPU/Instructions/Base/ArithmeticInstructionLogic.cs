using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Base
{
    internal abstract class ArithmeticInstructionLogic : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var value = GetValue(addressingMode, ram, registers);
            Perform(value, ram, registers);
        }

        protected abstract byte GetValue(AddressingMode addressingMode, RAM ram, RegistersProvider registers);

        private void Perform(byte value, RAM ram, RegistersProvider registers)
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
