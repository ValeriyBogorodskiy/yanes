using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class CPX : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.IndexRegisterX.State;
    }
}
