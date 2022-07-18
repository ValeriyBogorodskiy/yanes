using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal interface IBoundaryCrossingMode
    {
        internal bool CheckIfBoundaryWillBeCrossed(RAM ram, RegistersProvider registers);
    }
}
