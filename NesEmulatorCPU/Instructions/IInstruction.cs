using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal interface IInstruction
    {
        internal byte Opcode { get; }
        internal int Execute(RAM ram, RegistersProvider registers);
    }
}
