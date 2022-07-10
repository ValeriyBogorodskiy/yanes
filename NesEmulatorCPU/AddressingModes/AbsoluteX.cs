using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class AbsoluteX : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = (ushort)(ram.Read16bit(memoryAddress) + registers.IndexRegisterX.State);

            registers.ProgramCounter.State++;
            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
