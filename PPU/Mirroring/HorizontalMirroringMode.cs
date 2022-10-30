namespace PPU.Mirroring
{
    internal class HorizontalMirroringMode : MirroringMode
    {
        protected override ushort Match(int nameTableIndex, ushort address)
        {
            return nameTableIndex switch
            {
                0 => address,
                1 => (ushort)(address - 0x400),
                2 => (ushort)(address - 0x400),
                3 => (ushort)(address - 0x800),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
