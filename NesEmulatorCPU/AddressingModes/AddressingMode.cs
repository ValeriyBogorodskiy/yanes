using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal abstract class AddressingMode
    {
        internal abstract ushort GetRamAddress(Bus bus, RegistersProvider registers);

        internal byte GetRamValue(Bus bus, RegistersProvider registers) => bus.Read8bit(GetRamAddress(bus, registers));
    }
}
