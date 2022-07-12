using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class AbsoluteY : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = (ushort)(ram.Read16bit(memoryAddress) + registers.IndexRegisterY.State);

            registers.ProgramCounter.State += 2;

            return valueAddress;
        }
    }
}
