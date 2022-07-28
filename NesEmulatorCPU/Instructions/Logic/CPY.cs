using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Logic
{
    internal class CPY : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.IndexRegisterY.State;
    }
}
