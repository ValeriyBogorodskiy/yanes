using YaNES.Core;

namespace YaNES.ROM
{
    public class Rom : IRom
    {
        public byte[] PrgRom { get; }
        public byte[] ChrRom { get; }
        public byte Mirroring { get; }

        public Rom(byte[] prgRom, byte[] chrRom, byte mirroring)
        {
            PrgRom = prgRom;
            ChrRom = chrRom;
            Mirroring = mirroring;
        }

        public int PrgRomLength => PrgRom.Length;
        public int ChrRomLength => ChrRom.Length;

        public byte Read8bitPrg(ushort address) => PrgRom[address];
        public byte Read8bitChr(ushort address) => ChrRom[address];

        public ushort Read16bitPrg(ushort address)
        {
            var leastSignificantByte = PrgRom[address];

            var addressMostSignificantByte = (ushort)(address & 0xFF00);
            var addressLeastSignificantByte = (address & 0x00FF) + 1;
            var mostSignificantByteAddress = addressMostSignificantByte + addressLeastSignificantByte;

            var mostSignificantByte = PrgRom[mostSignificantByteAddress] << 8;

            return (ushort)(mostSignificantByte + leastSignificantByte);
        }
    }
}
