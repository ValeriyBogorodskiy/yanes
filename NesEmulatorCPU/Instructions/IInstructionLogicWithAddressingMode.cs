using NesEmulatorCPU.AddressingModes;
using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.Instructions
{
    internal interface IInstructionLogicWithAddressingMode
    {
        internal void Execute(AddressingMode addressingMode, Bus bus, RegistersProvider registers);
    }
}
