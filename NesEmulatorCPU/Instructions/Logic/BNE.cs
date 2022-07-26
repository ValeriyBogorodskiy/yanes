using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class BNE : BranchingInstruction
    {
        public BNE(byte opcode) : base(opcode)
        {
        }

        protected override bool ConditionMet(RAM ram, RegistersProvider registers)
        {
            return !registers.ProcessorStatus.Get(ProcessorStatus.Flags.Zero);
        }
    }
}
