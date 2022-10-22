using YaNES.Core;
using YaNES.Core.Utils;

namespace YaNES.CPU
{
    internal class Bus : ICpuBus
    {
        private readonly RAM ram = new();

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

            throw new IndexOutOfRangeException();
        }

        public byte Read8bit(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return ram.Read8bit(MapAddress(address));

            if (address.InRange(ReservedAddresses.PrgAddressSpace))
            {
                if (rom == null)
                    throw new NullReferenceException();

                return rom.Read8bitPrg(MapAddress(address));
            }

            throw new IndexOutOfRangeException();
        }

        public ushort Read16bit(ushort address)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                return ram.Read16bit(MapAddress(address));

            if (address.InRange(ReservedAddresses.PrgAddressSpace))
            {
                if (rom == null)
                    throw new NullReferenceException();

                return rom.Read16bitPrg(MapAddress(address));
            }

            throw new IndexOutOfRangeException();
        }

        public void Write8Bit(ushort address, byte value)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                ram.Write8Bit(MapAddress(address), value);
            else if (address.InRange(ReservedAddresses.PrgAddressSpace) || address.InRange(ReservedAddresses.PpuAddressSpace))
                throw new InvalidOperationException();
            else
                throw new IndexOutOfRangeException();
        }

        public void Write16Bit(ushort address, ushort value)
        {
            if (address.InRange(ReservedAddresses.CpuAddressSpace))
                ram.Write16Bit(MapAddress(address), value);
            else if (address.InRange(ReservedAddresses.PrgAddressSpace) || address.InRange(ReservedAddresses.PpuAddressSpace))
                throw new InvalidOperationException();
            else
                throw new IndexOutOfRangeException();
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
