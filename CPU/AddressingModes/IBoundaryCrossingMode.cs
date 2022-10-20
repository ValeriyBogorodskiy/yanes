using YaNES.CPU.Registers;

namespace YaNES.CPU.AddressingModes
{
    internal interface IBoundaryCrossingMode
    {
        internal bool CheckIfBoundaryWillBeCrossed(Bus bus, RegistersProvider registers);
    }
}
