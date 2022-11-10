namespace YaNes.PPU.Registers
{
    internal class Address
    {
        private ushort buffer = 0;
        private bool highByte = true;

        public byte State
        {
            get
            {
                return highByte ? (byte)(buffer >> 8) : (byte)buffer;
            }
            set
            {
                var high = highByte ? value : (byte)(buffer >> 8);
                var low = highByte ? (byte)buffer : value;

                buffer = (ushort)(high << 8 + low);
                buffer = (ushort)(buffer & ReservedAddresses.HighestPpuAddress);

                highByte = !highByte;
            }
        }

        public ushort Buffer => buffer;

        public void Increment(int value)
        {
            buffer = (ushort)(buffer + value);
            buffer = (ushort)(buffer & ReservedAddresses.HighestPpuAddress);
        }
    }
}
