using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class SEC : ProcessorStatusInstruction
    {
        public SEC(byte opcode) : base(opcode)
        {
        }

        protected override ProcessorStatus.Flags Flag => ProcessorStatus.Flags.Carry;

        protected override bool Value => true;
    }
}
