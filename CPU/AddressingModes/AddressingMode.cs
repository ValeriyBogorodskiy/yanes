using YaNES.CPU.Registers;

namespace YaNES.CPU.AddressingModes
{
    internal abstract class AddressingMode
    {
        /// <summary>
        /// Reads memory address and increases program counter
        /// </summary>
        internal abstract ushort GetRamAddress(Bus bus, RegistersProvider registers);

        /// <summary>
        /// Reads value from memory and increases program counter
        /// </summary>
        internal byte GetRamValue(Bus bus, RegistersProvider registers) => bus.Read8bit(GetRamAddress(bus, registers));
    }
}
