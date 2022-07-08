using NesEmulator.AddressingModes;
using NesEmulator.Cpu;

namespace NesEmulator.Instructions
{
    internal class LDA<T> : InstructionWithMultipleAddressingModes<T> where T : AddressingMode, new()
    {
        public LDA(byte opcode) : base(opcode)
        {
        }

        internal override void Execute(RAM ram, Cpu.Registers registers)
        {
            var value = addressingMode.ReadValue(ram, registers);

            registers.Accumulator.Load(value);
            registers.ProcessorStatus.UpdateNegativeFlag(value);
            registers.ProcessorStatus.UpdateZeroFlag(value);
        }
    }
}
