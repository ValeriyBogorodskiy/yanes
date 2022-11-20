using YaNes.PPU.Mirroring;
using YaNes.PPU.Registers;
using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNes.PPU
{
    public class Ppu : IPpu
    {
        private readonly byte[] ram = new byte[2048];
        // TODO : create correct mode
        private readonly MirroringMode mirroringMode = new HorizontalMirroringMode();
        private IRom? rom;
        private IInterruptsListener? interruptsListener;

        private readonly Controller controller = new();
        private readonly Status status = new();
        private readonly Address address = new();
        private byte dataBuffer;

        public int Scanline { get; private set; } = 0;
        public int ScanlineCycle { get; private set; } = 0;

        public byte Controller { get => controller.State; set => controller.State = value; }
        public byte Mask { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte Status { get => status.State; set => status.State = value; }
        public byte OamAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte OamData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte Scroll { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte Address { get => address.State; set => address.State = value; }
        public byte Data
        {
            get
            {
                var result = dataBuffer;
                var dataAddress = address.Buffer;
                address.Increment(controller.VramIncrement);

                if (dataAddress.InRange(ReservedAddresses.ChrRomAddressSpace))
                {
                    dataBuffer = rom!.Read8bitChr(dataAddress);
                }
                else if (dataAddress.InRange(ReservedAddresses.RamAddressSpace))
                {
                    dataBuffer = ram[mirroringMode.MirrorVramAddress(dataAddress)];
                }
                else if (dataAddress.InRange(ReservedAddresses.ForbiddenAddressSpace))
                {
                    throw new InvalidOperationException();
                }
                else if (dataAddress.InRange(ReservedAddresses.PaletteAddressSpace))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new InvalidOperationException();
                }

                return result;
            }
            set
            {
                var dataAddress = address.Buffer;
                address.Increment(controller.VramIncrement);

                if (dataAddress.InRange(ReservedAddresses.RamAddressSpace))
                {
                    ram[mirroringMode.MirrorVramAddress(dataAddress)] = value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void AttachRom(IRom rom)
        {
            this.rom = rom;
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

                if (Scanline == 241)
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

        public byte ReadRam(int address)
        {
            return ram[address];
        }
    }
}
