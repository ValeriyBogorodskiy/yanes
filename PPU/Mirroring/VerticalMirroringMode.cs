namespace YaNES.PPU.Mirroring
{
    internal class VerticalMirroringMode : MirroringMode
    {
        protected override ushort Match(int nameTableIndex, ushort address)
        {
            return nameTableIndex switch
            {
                0 => address,
                1 => address,
                2 => (ushort)(address - 0x800),
                3 => (ushort)(address - 0x800),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
