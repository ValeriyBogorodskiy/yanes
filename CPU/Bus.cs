using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNES.CPU
{
    internal class Bus : ICpuBus
    {
        private readonly Ram ram = new();

        // TODO : it makes sense to move this field to PPU module
        // http://nemulator.com/files/nes_emu.txt
        private byte ppuOpenBus = 0;

        private IRom? rom;
        private IPpu? ppu;

        private ushort MapAddress(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return (ushort)(address & 0b0000_0111_1111_1111);

            if (address.InRange(ReservedAddresses.PrgAddressSpace) || address == ReservedAddresses.PpuOamDma)
            {
                if (rom == null)
                    throw new NullReferenceException();

                var mappedAddress = address - 0x8000;

                if (rom.PrgRomLength == 0x4000 && mappedAddress >= 0x4000)
                {
                    mappedAddress %= 0x4000;
                }

                return (ushort)mappedAddress;
            }

            if (address.InRange(ReservedAddresses.PpuAddressSpace))
                return (ushort)(address & 0x2007);

            throw new ArgumentOutOfRangeException();
        }

        public byte Read8bit(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return ram.Read8bit(MapAddress(address));

            if (address.InRange(ReservedAddresses.PrgAddressSpace))
                return rom!.Read8bitPrg(MapAddress(address));

            if (address.InRange(ReservedAddresses.PpuAddressSpace) || address == ReservedAddresses.PpuOamDma)
                return Read8BitPpu(address);

            // TODO : implement APU
            if (address.InRange(ReservedAddresses.ApuAddressSpace) || address == ReservedAddresses.ApuExtraAddress)
                return 0;

            // TODO : implement joypads
            if (address == ReservedAddresses.Joypad1Address || address == ReservedAddresses.Joypad2Address)
                return 0;

            throw new ArgumentOutOfRangeException();
        }

        private byte Read8BitPpu(ushort address)
        {
            if (ppu == null)
                throw new NullReferenceException();

            var mappedAddress = MapAddress(address);

            return mappedAddress switch
            {
                0x2002 => ppu.Status,
                0x2004 => 0, // ppu.OamData, TODO : implement
                0x2007 => ppu.Data,
                0x4014 => 0, // TODO : implement PpuOamDma
                _ => ppuOpenBus
            };
        }

        public ushort Read16bit(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return ram.Read16bit(MapAddress(address));

            if (address.InRange(ReservedAddresses.PrgAddressSpace))
                return rom!.Read16bitPrg(MapAddress(address));

            throw new ArgumentOutOfRangeException();
        }

        public void Write8Bit(ushort address, byte value)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
            {
                ram.Write8Bit(MapAddress(address), value);
            }
            else if (address.InRange(ReservedAddresses.PpuAddressSpace))
            {
                Write8BitPpu(address, value);
            }
            else if (address == ReservedAddresses.PpuOamDma)
            {
                // TODO : OAM DMA 0x4014
                // https://wiki.nesdev.com/w/index.php/PPU_programmer_reference#OAM_DMA_.28.244014.29_.3E_write
                // https://github.com/bugzmanov/nes_ebook/blob/master/code/ch6.4/src/bus.rs
            }
            else if (address.InRange(ReservedAddresses.ApuAddressSpace) || address == ReservedAddresses.ApuExtraAddress)
            {
                // TODO : implement APU
            }
            else if (address == ReservedAddresses.Joypad1Address || address == ReservedAddresses.Joypad2Address)
            {
                // TODO : implement joypads
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void Write8BitPpu(ushort address, byte value)
        {
            if (ppu == null)
                throw new NullReferenceException();

            var mappedAddress = MapAddress(address);

            switch (mappedAddress)
            {
                case 0x2000:
                    ppu.Controller = value;
                    break;
                case 0x2001:
                    ppu.Mask = value;
                    break;
                case 0x2003:
                    ppu.OamAddress = value;
                    break;
                case 0x2005:
                    ppu.Scroll = value;
                    break;
                case 0x2006:
                    ppu.Address = value;
                    break;
                case 0x2007:
                    ppu.Data = value;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }

            ppuOpenBus = value;
        }

        public void Write16Bit(ushort address, ushort value)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
            {
                ram.Write16Bit(MapAddress(address), value);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void AttachRom(IRom rom)
        {
            this.rom = rom;
        }

        public void AttachPpu(IPpu ppu)
        {
            this.ppu = ppu;
        }
    }
}
