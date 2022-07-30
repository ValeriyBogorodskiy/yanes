using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
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
