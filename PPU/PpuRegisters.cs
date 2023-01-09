using YaNES.PPU.Registers;
using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNES.PPU
{
    // https://www.nesdev.org/wiki/PPU_registers#OAMDATA
    public partial class Ppu : IPpu, IPpuRegisters
    {
        private readonly Controller controller = new(0b1000_0000);
        private byte mask = 0;
        private readonly Status status = new();
        private byte oamAddress = 0;
        private readonly Scroll scroll = new();
        private readonly Address address = new();

        private byte dataBuffer;

        IPpuRegisters IPpu.Registers => this;

        public byte Controller
        {
            private get => controller.State;
            set
            {
                controller.State = value;
                OpenBus = value;
            }
        }

        public byte Mask
        {
            get => mask;
            set
            {
                mask = value;
                OpenBus = value;
            }
        }

        public byte Status
        {
            get
            {
                scroll.ResetLatch();
                return status.State;
            }
            private set
            {
                status.State = value;
                OpenBus = value;
            }
        }

        public byte OamAddress
        {
            private get => oamAddress;
            set
            {
                oamAddress = value;
                OpenBus = value;
            }
        }

        public byte OamData { get => throw new NotImplementedException(); }

        public byte Scroll
        {
            set
            {
                scroll.State = value;
                OpenBus = value;
            }
        }

        public byte Address

        {
            get => address.State;
            set
            {
                address.State = value;
                OpenBus = value;
            }
        }

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
                    throw new ArgumentOutOfRangeException();
                }
                else if (dataAddress.InRange(ReservedAddresses.PaletteAddressSpace))
                {
                    dataBuffer = paletteTable[MirrorPaletteTableAddress(dataAddress)];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

                return result;
            }
            set
            {
                var dataAddress = address.Buffer;
                address.Increment(controller.VramIncrement);

                if (dataAddress.InRange(ReservedAddresses.RamAddressSpace))
                {
                    var ramAddress = mirroringMode.MirrorVramAddress(dataAddress);
                    ram[ramAddress] = value;
                }
                else if (dataAddress.InRange(ReservedAddresses.PaletteAddressSpace))
                {
                    var paletteTableAddress = MirrorPaletteTableAddress(dataAddress);
                    paletteTable[paletteTableAddress] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

                OpenBus = value;
            }
        }

        public byte OpenBus { get; private set; }

        private static ushort MirrorPaletteTableAddress(ushort address)
        {
            return (ushort)(address - 0x3F00);
        }
    }
}
