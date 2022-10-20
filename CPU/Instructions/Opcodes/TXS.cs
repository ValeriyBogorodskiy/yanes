using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class TXS : Instruction
    {
        public TXS(byte opcode) : base(opcode)
        {
        }

        public override int Execute(Bus bus, RegistersProvider registers)
        {
            registers.StackPointer.State = registers.IndexRegisterX.State;
            return 2;
        }
    }
}
