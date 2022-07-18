using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class AbsoluteX : AddressingMode, IBoundaryCrossingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = (ushort)(ram.Read16bit(memoryAddress) + registers.IndexRegisterX.State);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }

        bool IBoundaryCrossingMode.CheckIfBoundaryWillBeCrossed(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var leastSignificantByte = ram.Read8bit(memoryAddress);
            var xRegister = registers.IndexRegisterX.State;

            return leastSignificantByte + xRegister > byte.MaxValue;
        }
    }
}
