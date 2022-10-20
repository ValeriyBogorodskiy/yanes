using YaNES.CPU.Registers;

namespace YaNES.CPU.AddressingModes
{
    internal class ZeroPageY : AddressingMode
    {
        internal override ushort GetRamAddress(Bus bus, RegistersProvider registers)
        {
            var memoryAddress = registers.ProgramCounter.State;
            var valueAddress = (byte)(bus.Read8bit(memoryAddress) + registers.IndexRegisterY.State);

            registers.ProgramCounter.State++;

            return valueAddress;
        }
    }
}
