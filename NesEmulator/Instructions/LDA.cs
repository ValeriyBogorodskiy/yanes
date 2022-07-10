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
            var value = addressingMode.ReadValue(ram, registers);

            registers.Accumulator.State = value;
            registers.ProcessorStatus.UpdateNegativeFlag(value);
            registers.ProcessorStatus.UpdateZeroFlag(value);
        }
    }
}
