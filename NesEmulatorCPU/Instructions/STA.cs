using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal class STA<T> : InstructionWithMultipleAddressingModes<T> where T : AddressingMode, new()
    {
        public STA(byte opcode) : base(opcode)
        {
        }

        internal override void Execute(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = addressingMode.GetAddress(ram, registers);
            ram.Write8Bit(memoryAddress, registers.Accumulator.State);
        }
    }
}
