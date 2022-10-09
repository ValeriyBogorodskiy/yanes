using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal abstract class ROR
    {
        protected static void Execute(byte value, RegistersProvider registers)
        {
            var carryMask = registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry) ? (byte)1 : (byte)0;
            var result = (byte)((value >> 1) | (carryMask << 7));

            registers.Accumulator.State = result;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b0000_0001) > 0);
        }
    }

    internal class RORAccumulator : ROR, IInstructionLogic
    {
        void IInstructionLogic.Execute(Bus bus, RegistersProvider registers)
        {
            Execute(registers.Accumulator.State, registers);
        }
    }

    internal class RORWithAddressing : ROR, IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(bus, registers);
            Execute(value, registers);
        }
    }
}
