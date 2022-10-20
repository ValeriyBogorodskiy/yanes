using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class BVC : BranchingInstruction
    {
        public BVC(byte opcode) : base(opcode)
        {
        }

        protected override bool ConditionMet(Bus bus, RegistersProvider registers)
        {
            return !registers.ProcessorStatus.Get(ProcessorStatus.Flags.Overflow);
        }
    }
}
