using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class TAY : TransferInstruction
    {
        public TAY(byte opcode) : base(opcode)
        {
        }

        protected override CpuRegister8Bit SourceRegister(RegistersProvider registers) => registers.Accumulator;

        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.IndexRegisterY;
    }
}
