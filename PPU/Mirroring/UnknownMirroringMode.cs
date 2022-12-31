namespace YaNES.PPU.Mirroring
{
    internal class UnknownMirroringMode : MirroringMode
    {
        protected override ushort Match(int nameTableIndex, ushort address)
        {
            throw new InvalidOperationException();
        }
    }
}
