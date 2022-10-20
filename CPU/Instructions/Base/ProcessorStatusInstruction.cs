using YaNES.CPU.Registers;
using static YaNES.CPU.Registers.ProcessorStatus;

namespace YaNES.CPU.Instructions.Base
{
    internal abstract class ProcessorStatusInstruction : Instruction
    {
        internal ProcessorStatusInstruction(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            registers.ProcessorStatus.Set(Flag, Value);
            return 2;
        }

        protected abstract Flags Flag { get; }
        protected abstract bool Value { get; }
    }
}
