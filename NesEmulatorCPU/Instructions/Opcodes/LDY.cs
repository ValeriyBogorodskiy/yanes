using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class LDY : LoadByteInstruction
    {
        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.IndexRegisterY;
    }
}
