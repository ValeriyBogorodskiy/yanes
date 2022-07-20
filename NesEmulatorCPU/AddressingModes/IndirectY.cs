using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class IndirectY : AddressingMode, IBoundaryCrossingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var leastSignificantByteAddress = ram.Read8bit(memoryAddress);
            var zeroPageAddress = ram.Read16bit(leastSignificantByteAddress);
            var valueAddress = (ushort)(zeroPageAddress + registers.IndexRegisterY.State);

            registers.ProgramCounter.State++;

            return valueAddress;
        }

        bool IBoundaryCrossingMode.CheckIfBoundaryWillBeCrossed(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var leastSignificantByteAddress = ram.Read8bit(memoryAddress);
            var leastSignificantByte = ram.Read8bit(leastSignificantByteAddress);
            var yRegister = registers.IndexRegisterY.State;

            return leastSignificantByte + yRegister > byte.MaxValue;
        }
    }
}
