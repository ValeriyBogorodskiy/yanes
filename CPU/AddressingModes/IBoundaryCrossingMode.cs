using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal interface IBoundaryCrossingMode
    {
        internal bool CheckIfBoundaryWillBeCrossed(Bus bus, RegistersProvider registers);
    }
}
