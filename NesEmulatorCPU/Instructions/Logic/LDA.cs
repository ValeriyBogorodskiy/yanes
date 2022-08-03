using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class LDA : LoadByteInstruction
    {
        protected override CpuRegister8Bit TargetRegister(RegistersProvider registers) => registers.Accumulator;
    }
}
