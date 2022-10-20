using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class STX : StoreInstructionLogic
    {
        protected override CpuRegister8Bit GetSourceRegister(RegistersProvider registers) => registers.IndexRegisterX;
    }
}
