using YaNES.Core;

namespace PPU
{
    public class Ppu : IPpu
    {
        private readonly byte[] ram = new byte[2048];

        private IRom? rom;

        public IPpuRegisters Registers => throw new NotImplementedException();

        public void AttachRom(IRom rom)
        {
            this.rom = rom;
        }
    }
}
