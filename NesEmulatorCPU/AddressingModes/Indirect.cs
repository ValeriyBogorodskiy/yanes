using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class Indirect : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;

            var leastSignificantByteAddress = ram.Read16bit(memoryAddress);
            var leastSignificantByte = ram.Read8bit(leastSignificantByteAddress);

            var mostSignificantByteAddress = (ushort)(leastSignificantByteAddress + 1);
            var mostSignificantByte = ram.Read8bit(mostSignificantByteAddress);

            var valueAddress = BitConverter.ToUInt16(new byte[2] { leastSignificantByte, mostSignificantByte }, 0);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }
    }
}
