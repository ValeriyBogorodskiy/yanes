using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal abstract class ASL
    {
        protected static void Execute(byte value, RegistersProvider registers)
        {
            var result = (byte)(value << 1);

            registers.Accumulator.State = result;

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b1000_0000) > 0);
        }
    }

    internal class ASLAccumulator : ASL, IInstructionLogic
    {
        void IInstructionLogic.Execute(Bus bus, RegistersProvider registers)
        {
            Execute(registers.Accumulator.State, registers);
        }
    }

    internal class ASLWithAddressing : ASL, IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(bus, registers);
            Execute(value, registers);
        }
    }
}
