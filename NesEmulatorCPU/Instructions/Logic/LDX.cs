using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class LDX : LoadByteInstruction
    {
        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.IndexRegisterX;
    }
}
