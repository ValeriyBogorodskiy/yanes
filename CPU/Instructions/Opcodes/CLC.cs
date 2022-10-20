using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class CLC : ProcessorStatusInstruction
    {
        public CLC(byte opcode) : base(opcode)
        {
        }

        protected override ProcessorStatus.Flags Flag => ProcessorStatus.Flags.Carry;

        protected override bool Value => false;
    }
}
