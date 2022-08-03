using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class CLD : ProcessorStatusInstruction
    {
        public CLD(byte opcode) : base(opcode)
        {
        }

        protected override ProcessorStatus.Flags Flag => ProcessorStatus.Flags.Decimal;

        protected override bool Value => false;
    }
}
