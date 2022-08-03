using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class CLI : ProcessorStatusInstruction
    {
        public CLI(byte opcode) : base(opcode)
        {
        }

        protected override ProcessorStatus.Flags Flag => ProcessorStatus.Flags.InterruptDisable;

        protected override bool Value => false;
    }
}
