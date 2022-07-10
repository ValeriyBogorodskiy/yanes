using NesEmulatorCPU.AddressingModes;

namespace NesEmulatorCPU.Instructions
{
    internal abstract class InstructionWithMultipleAddressingModes<T> : Instruction where T : AddressingMode, new()
    {
        protected readonly AddressingMode addressingMode;

        internal InstructionWithMultipleAddressingModes(byte opcode) : base(opcode)
        {
            addressingMode = new T();
        }
    }
}
