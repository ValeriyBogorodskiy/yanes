using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal abstract class LSR
    {
        protected static void Execute(byte value, RegistersProvider registers)
        {
            var result = (byte)(value >> 1);

            registers.Accumulator.State = result;

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b0000_0001) > 0);
        }
    }

    internal class LSRAccumulator : LSR, IInstructionLogic
    {
        void IInstructionLogic.Execute(RAM ram, RegistersProvider registers)
        {
            Execute(registers.Accumulator.State, registers);
        }
    }

    internal class LSRWithAddressing : LSR, IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(ram, registers);
            Execute(value, registers);
        }
    }
}
