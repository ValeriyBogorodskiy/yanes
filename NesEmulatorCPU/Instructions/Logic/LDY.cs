using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class LDY : LoadByteInstruction
    {
        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.IndexRegisterY;
    }
}
