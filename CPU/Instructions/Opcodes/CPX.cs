using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class CPX : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.IndexRegisterX.State;
    }
}
