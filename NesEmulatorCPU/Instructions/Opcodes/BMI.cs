using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class BMI : BranchingInstruction
    {
        public BMI(byte opcode) : base(opcode)
        {
        }

        protected override bool ConditionMet(Bus bus, RegistersProvider registers)
        {
            return registers.ProcessorStatus.Get(ProcessorStatus.Flags.Negative);
        }
    }
}
