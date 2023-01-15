using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNES.CPU
{
    internal class Bus : ICpuBus
    {
        private readonly Ram ram = new();
        private readonly IOamDmaTransferListener oamDmaTransferListener;
        private readonly byte[] oamDmaBuffer = new byte[256];

        private IRom? rom;
        private IPpu? ppu;
        private IJoypad[]? joypads;

        public Bus(IOamDmaTransferListener oamDmaTransferListener)
        {
            this.oamDmaTransferListener = oamDmaTransferListener;
        }

        private ushort MapAddress(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return (ushort)(address & 0b0000_0111_1111_1111);

            if (address.InRange(ReservedAddresses.PrgAddressSpace) || address == ReservedAddresses.PpuOamDma)
            {
                var mappedAddress = address - 0x8000;

                if (rom!.PrgRomLength == 0x4000 && mappedAddress >= 0x4000)
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

            if (address == ReservedAddresses.Joypad1Address)
                return joypads![0].Read();

            if (address == ReservedAddresses.Joypad2Address)
                return joypads![1].Read();

            throw new ArgumentOutOfRangeException();
        }

        private byte Read8BitPpu(ushort address)
        {
            var mappedAddress = MapAddress(address);

            return mappedAddress switch
            {
                0x2002 => ppu!.Registers.Status,
                0x2004 => ppu!.Registers.OamData,
                0x2007 => ppu!.Registers.Data,
                _ => ppu!.Registers.OpenBus
            };
        }

        public ushort Read16bit(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return ram.Read16bit(MapAddress(address));

            if (address.InRange(ReservedAddresses.PpuAddressSpace) || address == ReservedAddresses.PpuOamDma)
                return Read16BitPpu(address);

            if (address.InRange(ReservedAddresses.PrgAddressSpace))
                return rom!.Read16bitPrg(MapAddress(address));

            throw new ArgumentOutOfRangeException();
        }

        // TODO : Reading a nominally "write-only" register returns the latch's current value, as do the unused bits of PPUSTATUS (https://www.nesdev.org/wiki/PPU_registers)
        private ushort Read16BitPpu(ushort address)
        {
            var mappedAddress = MapAddress(address);

            return mappedAddress switch
            {
                0x2000 => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
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
                ushort dataStart = (ushort)(value << 8);

                for (var i = 0; i < oamDmaBuffer.Length; i++)
                    oamDmaBuffer[i] = Read8bit((ushort)(dataStart + i));

                ppu!.WriteOamData(oamDmaBuffer);
                oamDmaTransferListener.Trigger();
            }
            else if (address.InRange(ReservedAddresses.ApuAddressSpace) || address == ReservedAddresses.ApuExtraAddress)
            {
                // TODO : implement APU
            }
            else if (address == ReservedAddresses.Joypad1Address)
            {
                joypads![0].Write(value);
            }
            else if (address == ReservedAddresses.Joypad2Address)
            {
                joypads![1].Write(value);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void Write8BitPpu(ushort address, byte value)
        {
            var mappedAddress = MapAddress(address);

            switch (mappedAddress)
            {
                case 0x2000:
                    ppu!.Registers.Controller = value;
                    break;
                case 0x2001:
                    ppu!.Registers.Mask = value;
                    break;
                case 0x2003:
                    ppu!.Registers.OamAddress = value;
                    break;
                case 0x2005:
                    ppu!.Registers.Scroll = value;
                    break;
                case 0x2006:
                    ppu!.Registers.Address = value;
                    break;
                case 0x2007:
                    ppu!.Registers.Data = value;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
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

        public void AttachPpu(IPpu ppu)
        {
            this.ppu = ppu;
        }

        public void AttachJoypads(IJoypad[] joypads)
        {
            if (joypads.Length != 2)
            {
                throw new InvalidOperationException("Wrong number of joypads. Should be 2");
            }

            this.joypads = joypads;
        }

        public void AttachRom(IRom rom)
        {
            this.rom = rom;
        }
    }
}
