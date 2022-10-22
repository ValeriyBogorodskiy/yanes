using YaNES.Core;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class TAY : TransferInstruction
    {
        public TAY(byte opcode) : base(opcode)
        {
        }

        protected override Register8Bit SourceRegister(RegistersProvider registers) => registers.Accumulator;

        protected override Register8Bit TargetRegister(RegistersProvider registers) => registers.IndexRegisterY;
    }
}
