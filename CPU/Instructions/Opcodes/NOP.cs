using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class NOP : Instruction
    {
        public NOP(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            return 2;
        }
    }
}
