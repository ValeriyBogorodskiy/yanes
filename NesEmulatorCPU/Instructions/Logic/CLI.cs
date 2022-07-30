using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
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
