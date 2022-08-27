using NesEmulatorCPU.Utils;

namespace NesEmulatorCPU
{
    internal class Bus : IBus
    {
        private readonly RAM wRam = new();
        private readonly RAM vRam = new();
        private RAM ResolveDestination(ushort address)
        {
            if (IsInCPUAddressSpace(address))
                return wRam;

            if (IsInPPUAddressSpace(address))
                throw new NotImplementedException("PPU is not implemented yet");

            throw new IndexOutOfRangeException();
        }

        private static bool IsInCPUAddressSpace(ushort address) => address.InRange(ReservedAddresses.CpuRamStart, ReservedAddresses.CpuRamEnd);

        private static bool IsInPPUAddressSpace(ushort address) => address.InRange(ReservedAddresses.PPURamStart, ReservedAddresses.PPURamEnd);

        private static ushort MirrorAddress(ushort address)
        {
            if (IsInCPUAddressSpace(address))
                return (ushort)(address & 0b0000_0111_1111_1111);

            if (IsInPPUAddressSpace(address))
                throw new NotImplementedException("PPU is not implemented yet");

            throw new IndexOutOfRangeException();
        }

        public ushort Read16bit(ushort address) => ResolveDestination(address).Read16bit(MirrorAddress(address));

        public byte Read8bit(ushort address) => ResolveDestination(address).Read8bit(MirrorAddress(address));

        public void Write16Bit(ushort address, ushort value) => ResolveDestination(address).Write16Bit(MirrorAddress(address), value);

        public void Write8Bit(ushort address, byte value) => ResolveDestination(address).Write8Bit(MirrorAddress(address), value);
    }
}
