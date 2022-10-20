using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class IndirectX : AddressingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var zeroPageAddress = bus.Read8bit(memoryAddress);

            registers.ProgramCounter.State++;

            var leastSignificantByteAddress = (byte)(zeroPageAddress + registers.IndexRegisterX.State);
            var valueAddress = bus.Read16bit(leastSignificantByteAddress);

            return valueAddress;
        }
    }
}
