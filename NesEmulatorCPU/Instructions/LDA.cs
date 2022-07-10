using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class LDA<T> : InstructionWithMultipleAddressingModes<T> where T : AddressingMode, new()
    {
        internal LDA(byte opcode) : base(opcode)
        {
        }

        internal override void Execute(RAM ram, RegistersProvider registers)
        {
            var valueAddress = addressingMode.GetAddress(ram, registers);
            var value = ram.Read8bit(valueAddress);

            registers.Accumulator.State = value;
            registers.ProcessorStatus.UpdateNegativeFlag(value);
            registers.ProcessorStatus.UpdateZeroFlag(value);
        }
    }
}
