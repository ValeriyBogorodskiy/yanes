using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class Indirect : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            byte leastSignificantByte = ram.Read8bit(memoryAddress);

            registers.ProgramCounter.State++;

            memoryAddress = registers.ProgramCounter.State;
            byte mostSignificantByte = ram.Read8bit(memoryAddress);

            registers.ProgramCounter.State++;

            var result = BitConverter.ToUInt16(new byte[2] { mostSignificantByte, leastSignificantByte }, 0);
            return result;
        }
    }
}
