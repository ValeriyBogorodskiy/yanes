using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class IndirectX : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;

            var leastSignificantByteAddress = (byte)(ram.Read8bit(memoryAddress) + registers.IndexRegisterX.State);
            var valueAddress = ram.Read16bit(leastSignificantByteAddress);

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
