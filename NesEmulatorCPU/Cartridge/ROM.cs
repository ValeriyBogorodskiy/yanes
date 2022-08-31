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
    }
}
