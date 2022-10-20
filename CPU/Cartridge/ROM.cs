namespace YaNES.CPU.Cartridge
{
    public class ROM
    {
        private byte[] PrgRom { get; }
        private byte[] ChrRom { get; }

        public ROM(byte[] PrgRom, byte[] ChrRom)
        {
            this.PrgRom = PrgRom;
            this.ChrRom = ChrRom;
        }

        internal byte Read8bitPrg(ushort address) => PrgRom[address];
        internal byte Read8bitChr(ushort address) => ChrRom[address];

        internal int PrgRomLength => PrgRom.Length;
        internal int ChrRomLength => ChrRom.Length;

        // TODO : copy paste from RAM
        internal ushort Read16bitPrg(ushort address)
        {
            var leastSignificantByte = PrgRom[address];

            // TODO : does it really work like this?
            var addressMostSignificantByte = (ushort)(address & 0xFF00);
            var addressLeastSignificantByte = (address & 0x00FF) + 1;
            var mostSignificantByteAddress = addressMostSignificantByte + addressLeastSignificantByte;

            var mostSignificantByte = PrgRom[mostSignificantByteAddress] << 8;

            return (ushort)(mostSignificantByte + leastSignificantByte);
        }
    }
}
