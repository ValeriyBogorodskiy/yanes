using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class TAX : TransferInstruction
    {
        public TAX(byte opcode) : base(opcode)
        {
        }

        protected override CpuRegister8Bit SourceRegister(RegistersProvider registers) => registers.Accumulator;

        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.IndexRegisterX;
    }
}
