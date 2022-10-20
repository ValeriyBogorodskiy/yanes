using YaNES.CPU.Registers;

namespace YaNES.CPU.AddressingModes
{
    internal class IndirectY : AddressingMode, IBoundaryCrossingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var leastSignificantByteAddress = bus.Read8bit(memoryAddress);
            var zeroPageAddress = bus.Read16bit(leastSignificantByteAddress);
            var valueAddress = (ushort)(zeroPageAddress + registers.IndexRegisterY.State);

            registers.ProgramCounter.State++;

            return valueAddress;
        }

        bool IBoundaryCrossingMode.CheckIfBoundaryWillBeCrossed(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var leastSignificantByteAddress = bus.Read8bit(memoryAddress);
            var leastSignificantByte = bus.Read8bit(leastSignificantByteAddress);
            var yRegister = registers.IndexRegisterY.State;

            return leastSignificantByte + yRegister > byte.MaxValue;
        }
    }
}
