using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal abstract class ROL
    {
        protected static void Execute(byte value, RegistersProvider registers)
        {
            var carryMask = registers.ProcessorStatus.Get(ProcessorStatus.Flags.Carry) ? (byte)1 : (byte)0;
            var result = (byte)((value << 1) | carryMask);

            registers.Accumulator.State = result;
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b10000000) > 0);
        }
    }

    internal class ROLAccumulator : ROL, IInstructionLogic
    {
        void IInstructionLogic.Execute(RAM ram, RegistersProvider registers)
        {
            Execute(registers.Accumulator.State, registers);
        }
    }

    internal class ROLWithAddressing : ROL, IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var value = addressingMode.GetRamValue(ram, registers);
            Execute(value, registers);
        }
    }
}
