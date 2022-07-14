using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal interface IPageCrossingMode
    {
        internal bool CheckIfPageWillBeCrossed(RAM ram, RegistersProvider registers);
    }
}
