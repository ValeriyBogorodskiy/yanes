using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class LDX : LoadByteInstruction
    {
        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.IndexRegisterX;
    }
}
