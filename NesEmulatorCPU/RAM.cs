namespace NesEmulatorCPU
{
    internal class RAM
    {
        private readonly byte[] cells = new byte[ushort.MaxValue];

        internal byte Read8bit(ushort address) => cells[address];

        internal ushort Read16bit(ushort address)
        {
            var leastSignificantByte = cells[address];

            // TODO : Does it really work like this?
            var addressMostSignificantByte = (ushort)(address & 0xFF00);
            var addressLeastSignificantByte = (byte)((byte)address + 1);
            var mostSignificantByte = cells[addressMostSignificantByte + addressLeastSignificantByte] << 8;

            return (ushort)(mostSignificantByte + leastSignificantByte);
        }

        internal void Write8Bit(ushort address, byte value) => cells[address] = value;

        internal void Write16Bit(ushort address, ushort value)
        {
            var leastSignificantByte = (byte)(value & 0x00FF);
            var mostSignificantByte = (byte)(value >> 8);

            cells[address] = leastSignificantByte;
            cells[address + 1] = mostSignificantByte;
        }
    }
}
