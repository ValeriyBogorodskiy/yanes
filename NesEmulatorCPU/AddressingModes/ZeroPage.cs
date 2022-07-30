using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class ZeroPage : AddressingMode
    {
        internal override ushort GetRamAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = ram.Read8bit(memoryAddress);

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
