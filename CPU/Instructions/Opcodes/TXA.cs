using YaNES.Core;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class TXA : TransferInstruction
    {
        public TXA(byte opcode) : base(opcode)
        {
        }

        protected override Register8Bit SourceRegister(RegistersProvider registers) => registers.IndexRegisterX;

        protected override Register8Bit TargetRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
