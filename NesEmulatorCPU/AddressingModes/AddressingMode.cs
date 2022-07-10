using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal abstract class AddressingMode
    {
        internal abstract ushort GetAddress(RAM ram, RegistersProvider registers);
    }
}
