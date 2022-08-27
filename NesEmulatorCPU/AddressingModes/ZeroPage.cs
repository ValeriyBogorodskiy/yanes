using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class ZeroPage : AddressingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = bus.Read8bit(memoryAddress);

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
