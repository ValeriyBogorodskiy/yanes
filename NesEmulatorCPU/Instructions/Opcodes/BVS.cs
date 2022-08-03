using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class BVS : BranchingInstruction
    {
        public BVS(byte opcode) : base(opcode)
        {
        }

        protected override bool ConditionMet(RAM ram, RegistersProvider registers)
        {
            return registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow);
        }
    }
}
