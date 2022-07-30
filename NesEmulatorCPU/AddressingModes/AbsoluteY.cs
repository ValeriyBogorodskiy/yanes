using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class AbsoluteY : AddressingMode, IBoundaryCrossingMode
    {
        internal override ushort GetRamAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = (ushort)(ram.Read16bit(memoryAddress) + registers.IndexRegisterY.State);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }

        bool IBoundaryCrossingMode.CheckIfBoundaryWillBeCrossed(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var leastSignificantByte = ram.Read8bit(memoryAddress);
            var xRegister = registers.IndexRegisterY.State;

            return leastSignificantByte + xRegister > byte.MaxValue;
        }
    }
}
