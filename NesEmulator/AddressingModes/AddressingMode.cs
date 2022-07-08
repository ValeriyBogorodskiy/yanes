using NesEmulator.Cpu;

namespace NesEmulator.AddressingModes
{
    internal abstract class AddressingMode
    {
        internal abstract byte ReadValue(RAM ram, Cpu.Registers registers);
    }
}
