using YaNES.Core;

namespace YaNES.PPU.Registers
{
    internal class Status : Register8BitWith<Status.Flags>
    {
        public enum Flags : byte
        {
            VerticalBlank = 1 << 7,
            Sprite0Hit = 1 << 6
        }
    }
}
