namespace NesEmulatorCPU
{
    internal class RAM : IRAM
    {
        private readonly byte[] cells = new byte[ushort.MaxValue];

        public byte Read8bit(ushort address) => cells[address];

        public ushort Read16bit(ushort address)
        {
            var leastSignificantByte = cells[address];

            // TODO : Does it really work like this?
            var addressMostSignificantByte = (ushort)(address & 0xFF00);
            var addressLeastSignificantByte = (byte)((address & 0x00FF) + 1);
            var mostSignificantByteAddress = addressMostSignificantByte + addressLeastSignificantByte;

            var mostSignificantByte = cells[mostSignificantByteAddress] << 8;

            return (ushort)(mostSignificantByte + leastSignificantByte);
        }

        public void Write8Bit(ushort address, byte value) => cells[address] = value;

        public void Write16Bit(ushort address, ushort value)
        {
            var leastSignificantByte = (byte)(value & 0x00FF);

            cells[address] = leastSignificantByte;

            // TODO : Does it really work like this?
            var addressMostSignificantByte = (ushort)(address & 0xFF00);
            var addressLeastSignificantByte = (byte)((address & 0x00FF) + 1);
            var mostSignificantByteAddress = addressMostSignificantByte + addressLeastSignificantByte;

            var mostSignificantByte = (byte)(value >> 8);

            cells[mostSignificantByteAddress] = mostSignificantByte;
        }
    }
}
