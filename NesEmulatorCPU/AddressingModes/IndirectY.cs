using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class IndirectY : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;

            var leastSignificantByteAddress = ram.Read8bit(memoryAddress);
            var leastSignificantByte = ram.Read8bit(leastSignificantByteAddress);

            var mostSignificantByteAddress = (byte)(leastSignificantByteAddress + 1);
            var mostSignificantByte = ram.Read8bit(mostSignificantByteAddress);

            var zeroPageAddress = BitConverter.ToUInt16(new byte[2] { leastSignificantByte, mostSignificantByte }, 0);
            var valueAddress = (ushort)(zeroPageAddress + registers.IndexRegisterY.State);

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
