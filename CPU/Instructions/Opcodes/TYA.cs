using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class TYA : TransferInstruction
    {
        public TYA(byte opcode) : base(opcode)
        {
        }

        protected override CpuRegister8Bit SourceRegister(RegistersProvider registers) => registers.IndexRegisterY;
        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
