using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class CLV : ProcessorStatusInstruction
    {
        public CLV(byte opcode) : base(opcode)
        {
        }

        protected override ProcessorStatus.Flags Flag => ProcessorStatus.Flags.Overflow;

        protected override bool Value => false;
    }
}
