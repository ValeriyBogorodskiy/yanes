using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class Immediate : AddressingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var valueAddress = registers.ProgramCounter.State;

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
