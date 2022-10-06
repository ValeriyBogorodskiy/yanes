namespace NesEmulatorCPU.Cartridge
{
    public class ROM
    {
        private byte[] PRGRom { get; }
        private byte[] CHRRom { get; }

        public ROM(byte[] PRGRom, byte[] CHRRom)
        {
            this.PRGRom = PRGRom;
            this.CHRRom = CHRRom;
        }

        internal byte Read8bitPRG(ushort address) => PRGRom[address];
        internal byte Read8bitCHR(ushort address) => CHRRom[address];

        internal int PRGRomLength => PRGRom.Length;
        internal int CHRRomLength => CHRRom.Length;

        // TODO : copy paste from RAM
        internal ushort Read16bitPRG(ushort address)
        {
            var leastSignificantByte = PRGRom[address];

            // TODO : does it really work like this?
            var addressMostSignificantByte = (ushort)(address & 0xFF00);
            var addressLeastSignificantByte = (address & 0x00FF) + 1;
            var mostSignificantByteAddress = addressMostSignificantByte + addressLeastSignificantByte;

            var mostSignificantByte = PRGRom[mostSignificantByteAddress] << 8;

            return (ushort)(mostSignificantByte + leastSignificantByte);
        }
    }
}
