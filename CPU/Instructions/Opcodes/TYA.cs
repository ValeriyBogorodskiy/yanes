using YaNES.Core;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class TYA : TransferInstruction
    {
        public TYA(byte opcode) : base(opcode)
        {
        }

        protected override Register8Bit SourceRegister(RegistersProvider registers) => registers.IndexRegisterY;
        protected override Register8Bit TargetRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
