using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class CMP : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.Accumulator.State;
    }
}
