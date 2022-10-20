using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class CPY : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.IndexRegisterY.State;
    }
}
