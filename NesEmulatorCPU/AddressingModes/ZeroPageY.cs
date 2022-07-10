using NesEmulatorCPU.Registers;

namespace NesEmulatorCPU.AddressingModes
{
    internal class ZeroPageY : AddressingMode
    {
        internal override ushort GetAddress(RAM ram, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = (byte)(ram.Read8bit(memoryAddress) + registers.IndexRegisterY.State);

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
