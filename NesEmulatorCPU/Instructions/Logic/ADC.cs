using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class ADC : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetAddress(ram, registers);

            var value = ram.Read8bit(valueAddress);
            var accumulatorState = registers.Accumulator.State;
            var carryIn = registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry) ? 1 : 0;

            var result = value + accumulatorState + carryIn;
            var byteResult = (byte)result;

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, byteResult.IsNegative());

            var overflowOccured = ((value & BitMasks.Negative) == (accumulatorState & BitMasks.Negative)) && ((value & BitMasks.Negative) != (result & BitMasks.Negative));
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Overflow, overflowOccured);

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, byteResult.IsZero());

            var carryOccured = (result & BitMasks.CarryBit) == BitMasks.CarryBit;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, carryOccured);

            registers.Accumulator.State = byteResult;
        }
    }
}
