using NesEmulator.AddressingModes;

namespace NesEmulator.Instructions
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
