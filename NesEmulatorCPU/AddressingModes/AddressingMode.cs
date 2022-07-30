using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal abstract class AddressingMode
    {
        internal abstract ushort GetRamAddress(RAM ram, RegistersProvider registers);

        internal byte GetRamValue(RAM ram, RegistersProvider registers) => ram.Read8bit(GetRamAddress(ram, registers));
    }
}
