using YaNES.Core;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class STY : StoreInstructionLogic
    {
        protected override Register8Bit GetSourceRegister(RegistersProvider registers) => registers.IndexRegisterY;
    }
}
