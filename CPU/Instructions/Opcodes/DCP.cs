using YaNES.CPU.AddressingModes;
using YaNES.CPU.Registers;
using YaNES.Utils;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class DCP : IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            // DEC
            var valueAddress = addressingMode.GetRamAddress(bus, registers);
            var value = bus.Read8bit(valueAddress);
            var newValue = (byte)(value - 1);

            bus.Write8Bit(valueAddress, newValue);
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, newValue.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, newValue.IsZero());

            // CMP
            var accumulatorValue = registers.Accumulator.State;
            var subtractionResult = (byte)(accumulatorValue - newValue);

            if (accumulatorValue < newValue)
            {
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, subtractionResult.IsNegative());
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, false);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, false);
            }
            else if (accumulatorValue == newValue)
            {
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, false);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, true);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, true);
            }
            else if (accumulatorValue > newValue)
            {
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, subtractionResult.IsNegative());
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, false);
                registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, true);
            }
        }
    }
}
