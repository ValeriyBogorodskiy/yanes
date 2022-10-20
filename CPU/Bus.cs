using YaNES.CPU.Cartridge;
using YaNES.CPU.Utils;

namespace YaNES.CPU
{
    internal class Bus : IBus
    {
        private readonly RAM wRam = new();
        private readonly RAM vRam = new();

        private ROM? rom;

        public void InsertRom(ROM rom)
        {
            this.rom = rom;
        }

        private ushort MapAddress(ushort address)
        {
            if (address.InRange(ReservedAddresses.CPUAddressSpace))
                return (ushort)(address & 0b0000_0111_1111_1111);

            if (address.InRange(ReservedAddresses.PRGAddressSpace))
            {
                if (rom == null)
                    throw new NullReferenceException();

                var mappedAddress = address - 0x8000;

                if (rom.PRGRomLength == 0x4000 && mappedAddress >= 0x4000)
                {
                    mappedAddress %= 0x4000;
                }

                return (ushort)mappedAddress;
            }

            throw new IndexOutOfRangeException();
        }

        public byte Read8bit(ushort address)
        {
            if (address.InRange(ReservedAddresses.CPUAddressSpace))
                return wRam.Read8bit(MapAddress(address));

            if (address.InRange(ReservedAddresses.PRGAddressSpace))
            {
                if (rom == null)
                    throw new NullReferenceException();

                return rom.Read8bitPRG(MapAddress(address));
            }

            throw new IndexOutOfRangeException();
        }

        public ushort Read16bit(ushort address)
        {
            if (address.InRange(ReservedAddresses.CPUAddressSpace))
                return wRam.Read16bit(MapAddress(address));

            if (address.InRange(ReservedAddresses.PRGAddressSpace))
            {
                if (rom == null)
                    throw new NullReferenceException();

                return rom.Read16bitPRG(MapAddress(address));
            }

            throw new IndexOutOfRangeException();
        }

        public void Write8Bit(ushort address, byte value)
        {
            if (address.InRange(ReservedAddresses.CPUAddressSpace))
                wRam.Write8Bit(MapAddress(address), value);
            else if (address.InRange(ReservedAddresses.PRGAddressSpace) || address.InRange(ReservedAddresses.PPUAddressSpace))
                throw new InvalidOperationException();
            else
                throw new IndexOutOfRangeException();
        }

        public void Write16Bit(ushort address, ushort value)
        {
            if (address.InRange(ReservedAddresses.CPUAddressSpace))
                wRam.Write16Bit(MapAddress(address), value);
            else if (address.InRange(ReservedAddresses.PRGAddressSpace) || address.InRange(ReservedAddresses.PPUAddressSpace))
                throw new InvalidOperationException();
            else
                throw new IndexOutOfRangeException();
        }
    }
}
