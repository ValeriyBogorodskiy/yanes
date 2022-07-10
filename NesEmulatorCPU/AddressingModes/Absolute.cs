using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class Absolute : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = ram.Read16bit(memoryAddress);

            registers.ProgramCounter.State++;
            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
