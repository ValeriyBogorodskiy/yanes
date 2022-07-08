using NesEmulator.Tools;

namespace NesEmulator.Cpu
{
    internal class RAM
    {
        private readonly byte[] cells = new byte[ushort.MaxValue];

        internal byte Read8bit(ushort address) => cells[address];

        internal ushort Read16bit(ushort address)
        {
            var leastSignificantByte = cells[address];
            var mostSignificantByte = cells[address + 1];

            return BitConverter.ToUInt16(new byte[2] { mostSignificantByte, leastSignificantByte }, 0);
        }

        internal void Write8Bit(ushort address, byte value) => cells[address] = value;

        internal void Write16Bit(ushort address, ushort value)
        {
            var bytes = BitConverter.GetBytes(value);

            var leastSignificantByte = bytes[1];
            var mostSignificantByte = bytes[0];

            cells[address] = leastSignificantByte;
            cells[address + 1] = mostSignificantByte;
        }
    }
}
