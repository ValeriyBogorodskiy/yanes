namespace YaNes.PPU.Mirroring
{
    internal abstract class MirroringMode
    {
        public ushort MirrorVramAddress(ushort address)
        {
            if (address > ReservedAddresses.HighestPpuAddress)
                address = (ushort)(address & ReservedAddresses.HighestPpuAddress);

            var mirroredAddress = (ushort)(address & 0x2FFF);
            var vramSpaceAddress = (ushort)(mirroredAddress - 0x2000);
            var nameTableIndex = vramSpaceAddress / 0x400;

            return Match(nameTableIndex, vramSpaceAddress);
        }

        protected abstract ushort Match(int nameTableIndex, ushort address);
    }
}
