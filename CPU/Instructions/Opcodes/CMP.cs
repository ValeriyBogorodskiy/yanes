using NesEmulatorCPU.Instructions.Base;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions.Opcodes
{
    internal class CMP : CompareInstruction
    {
        protected override byte GetRegisterValue(RegistersProvider registers) => registers.Accumulator.State;
    }
}
