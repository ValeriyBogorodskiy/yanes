using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class AbsoluteX : AddressingMode, IBoundaryCrossingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = (ushort)(bus.Read16bit(memoryAddress) + registers.IndexRegisterX.State);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }

        bool IBoundaryCrossingMode.CheckIfBoundaryWillBeCrossed(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var leastSignificantByte = bus.Read8bit(memoryAddress);
            var xRegister = registers.IndexRegisterX.State;

            return leastSignificantByte + xRegister > byte.MaxValue;
        }
    }
}
