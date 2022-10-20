using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class Absolute : AddressingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = bus.Read16bit(memoryAddress);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }
    }
}
