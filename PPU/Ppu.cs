using PPU.Mirroring;
using PPU.Registers;
using YaNES.Core;
using YaNES.Core.Utils;

namespace PPU
{
    public class Ppu : IPpu
    {
        private readonly byte[] ram = new byte[2048];
        private MirroringMode mirroringMode = new HorizontalMirroringMode(); // TODO : create correct mode somehow
        private IRom? rom;

        private readonly Controller controller = new();
        private readonly Address address = new();
        private byte dataBuffer;

        public byte Controller { get => controller.State; set => controller.State = value; }
        public byte Mask { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
    }
}
