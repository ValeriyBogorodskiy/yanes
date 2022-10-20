using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class CMP : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.Accumulator.State;
    }
}
