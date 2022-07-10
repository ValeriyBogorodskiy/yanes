using NesEmulator.Cpu;

namespace NesEmulator.AddressingModes
{
    internal class Immediate : AddressingMode
    {
        internal override byte ReadValue(RAM ram, Cpu.Registers registers)
        {
            var valueAddress = registers.ProgramCounter.State;
            var value = ram.Read8bit(valueAddress);

            registers.ProgramCounter.State++;

            return value;
        }
    }
}
