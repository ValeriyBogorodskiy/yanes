using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class Indirect : AddressingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var indirectValueAddress = bus.Read16bit(memoryAddress);
            var valueAddress = bus.Read16bit(indirectValueAddress);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }
    }
}
