using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class Indirect : AddressingMode
    {
        internal override ushort GetRamAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var indirectValueAddress = ram.Read16bit(memoryAddress);
            var valueAddress = ram.Read16bit(indirectValueAddress);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }
    }
}
