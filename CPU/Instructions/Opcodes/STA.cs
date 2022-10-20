using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class STA : StoreInstructionLogic
    {
        protected override CpuRegister8Bit GetSourceRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
