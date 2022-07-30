using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class SED : ProcessorStatusInstruction
    {
        public SED(byte opcode) : base(opcode)
        {
        }

        protected override ProcessorStatus.Flags Flag => ProcessorStatus.Flags.Decimal;

        protected override bool Value => true;
    }
}
