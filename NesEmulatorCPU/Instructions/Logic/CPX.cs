using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class CPX : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.IndexRegisterX.State;
    }
}
