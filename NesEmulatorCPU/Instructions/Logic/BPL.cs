using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class BPL : BranchingInstruction
    {
        public BPL(byte opcode) : base(opcode)
        {
        }

        protected override bool ConditionMet(RAM ram, RegistersProvider registers)
        {
            return !registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative);
        }
    }
}
