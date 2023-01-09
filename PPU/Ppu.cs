using YaNES.PPU.Mirroring;
using YaNES.Core;

namespace YaNES.PPU
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

            UpdateSpriteZeroHit();

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

                    ResetSpriteZeroHit();
                }
                else if (Scanline == 262)
                {
                    Scanline = 0;
                    status.Set(Registers.Status.Flags.VerticalBlank, false);
                    ResetSpriteZeroHit();
                }
            }
        }

        private void UpdateSpriteZeroHit()
        {
            var sprite0y = oamData[0];
            var sprite0x = oamData[3];

            if (sprite0y == Scanline && sprite0x <= ScanlineCycle) // TODO : check mask show sprites
                status.Set(Registers.Status.Flags.Sprite0Hit, true);
        }

        private void ResetSpriteZeroHit()
        {
            status.Set(Registers.Status.Flags.Sprite0Hit, false);
        }
    }
}
