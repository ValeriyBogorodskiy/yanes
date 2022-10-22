using YaNES.Core;
using YaNES.CPU.Instructions.Base;
using YaNES.CPU.Registers;

namespace YaNES.CPU.Instructions.Opcodes
{
    internal class LDA : LoadByteInstruction
    {
        protected override Register8Bit TargetRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
