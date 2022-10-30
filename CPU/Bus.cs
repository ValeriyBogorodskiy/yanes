using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNES.CPU
{
    internal class Bus : ICpuBus
    {
        private readonly Ram ram = new();

        private IRom? rom;
        private IPpu? ppu;

        private ushort MapAddress(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return (ushort)(address & 0b0000_0111_1111_1111);

            if (address.InRange(ReservedAddresses.PrgAddressSpace))
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

            if (address.InRange(ReservedAddresses.PpuAddressSpace)) // TODO : OAM DMA 0x4014
                return Read8BitPpu(address);

            throw new ArgumentOutOfRangeException();
        }

        private byte Read8BitPpu(ushort address)
        {
            if (ppu == null)
                throw new NullReferenceException();

            return MapAddress(address) switch
            {
                0x2002 => ppu.Status,
                0x2004 => ppu.OamData,
                0x2007 => ppu.Data,
                _ => throw new ArgumentOutOfRangeException(),
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
                ram.Write8Bit(MapAddress(address), value);
            else if (address.InRange(ReservedAddresses.PpuAddressSpace))
                Write8BitPpu(address, value);
            else
                throw new ArgumentOutOfRangeException();
        }

        private void Write8BitPpu(ushort address, byte value)
        {
            if (ppu == null)
                throw new NullReferenceException();

            switch (MapAddress(address))
            {
                case 0x2000:
                    ppu.Controller = value;
                    break;
                case 0x2006:
                    ppu.Address = value;
                    break;
                case 0x2007:
                    ppu.Data = value;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void Write16Bit(ushort address, ushort value)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                ram.Write16Bit(MapAddress(address), value);
            else
                throw new ArgumentOutOfRangeException();
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
