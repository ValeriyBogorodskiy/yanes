using YaNES.Core;

namespace PPU
{
    internal class Registers : IPpuRegisters
    {
        private byte controller;
        private byte mask;
        private byte status;
        private byte oamAddress;
        private byte oamData;
        private byte scroll;
        private byte address;
        private byte data;
    }
}
