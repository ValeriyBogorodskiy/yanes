using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class SEI : ProcessorStatusInstruction
    {
        public SEI(byte opcode) : base(opcode)
        {
        }

        protected override ProcessorStatus.Flags Flag => ProcessorStatus.Flags.InterruptDisable;

        protected override bool Value => true;
    }
}
