using YaNes.PPU.Mirroring;
using YaNES.Core;

namespace YaNes.PPU
{
    public partial class Ppu : IPpu, IPpuRegisters
    {
        private readonly byte[] ram = new byte[2048];
        private readonly byte[] paletteTable = new byte[32];

        private MirroringMode mirroringMode = new UnknownMirroringMode();
        private IRom? rom;
        private IInterruptsListener? interruptsListener;

        public int Scanline { get; private set; } = 0;
        public int ScanlineCycle { get; private set; } = 0;

        public void AttachRom(IRom rom)
        {
            this.rom = rom;

            if (rom.Mirroring == 0)
                mirroringMode = new HorizontalMirroringMode();
            else
                throw new NotImplementedException();
        }

        public void AttachInterruptsListener(IInterruptsListener interruptsListener)
        {
            this.interruptsListener = interruptsListener;
        }

        public void Update(int cycles)
        {
            ScanlineCycle += cycles;

            while (ScanlineCycle >= 341)
            {
                ScanlineCycle -= 341;
                Scanline++;

                if (Scanline < 241)
                {
                    FetchCurrentScanlineSprites();
                }
                else if (Scanline == 241)
                {
                    status.Set(Registers.Status.Flags.VerticalBlank, true);

                    if (controller.Get(Registers.Controller.Flags.GenerateNmi))
                        interruptsListener?.Trigger(Interrupt.NMI);
                }
                else if (Scanline == 262)
                {
                    Scanline = 0;
                    status.Set(Registers.Status.Flags.VerticalBlank, false);
                }
            }
        }
    }
}
