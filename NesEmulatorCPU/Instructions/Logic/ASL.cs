using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;
using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU.Instructions.Logic
{
    // TODO : maybe change AddressingMode interface to return a value insted of address. But then I'll have to rewrite all addressing tests...
    internal abstract class ASL
    {
        protected static void Execute(byte value, RegistersProvider registers)
        {
            var result = (byte)(value << 1);

            registers.Accumulator.State = result;

            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Negative, result.IsNegative());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Zero, result.IsZero());
            registers.ProcessorStatus.Set(ProcessorStatus.Flags.Carry, (value & 0b10000000) > 0);
        }
    }

    internal class ASLAccumulator : ASL, IInstructionLogic
    {
        void IInstructionLogic.Execute(RAM ram, RegistersProvider registers)
        {
            Execute(registers.Accumulator.State, registers);
        }
    }

    internal class ASLWithAddressing : ASL, IInstructionLogicWithAddressingMode
    {
        void IInstructionLogicWithAddressingMode.Execute(AddressingMode addressingMode, RAM ram, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetAddress(ram, registers);
            var value = ram.Read8bit(valueAddress);

            Execute(value, registers);
        }
    }
}
