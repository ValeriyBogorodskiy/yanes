using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class IndirectX : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;

            var leastSignificantByteAddress = (byte)(ram.Read8bit(memoryAddress) + registers.IndexRegisterX.State);
            var leastSignificantByte = ram.Read8bit(leastSignificantByteAddress);

            var mostSignificantByteAddress = (byte)(leastSignificantByteAddress + 1);
            var mostSignificantByte = ram.Read8bit(mostSignificantByteAddress);

            var valueAddress = BitConverter.ToUInt16(new byte[2] { leastSignificantByte, mostSignificantByte }, 0);

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
