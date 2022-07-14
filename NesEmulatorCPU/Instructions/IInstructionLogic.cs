using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal interface IInstructionLogic
    {
        internal void Execute(RAM ram, RegistersProvider registers);
    }
}
