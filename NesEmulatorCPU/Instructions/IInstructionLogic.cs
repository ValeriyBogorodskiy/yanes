using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal interface IInstructionLogic
    {
        internal void Execute(Bus bus, RegistersProvider registers);
    }
}
