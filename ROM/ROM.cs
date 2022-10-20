using YaNES.Interfaces;

namespace YaNES.ROM
{
    public class ROM : IRom
    {
        private byte[] PrgRom { get; }
        private byte[] ChrRom { get; }

        public ROM(byte[] PrgRom, byte[] ChrRom)
        {
            this.PrgRom = PrgRom;
            this.ChrRom = ChrRom;
        }

        public int PrgRomLength => PrgRom.Length;
        public int ChrRomLength => ChrRom.Length;

        public byte Read8bitPrg(ushort address) => PrgRom[address];
        public byte Read8bitChr(ushort address) => ChrRom[address];

        // TODO : copy paste from RAM
        public ushort Read16bitPrg(ushort address)
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
