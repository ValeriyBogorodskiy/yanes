using YaNES.Core;

namespace YaNes.PPU.Registers
{
    internal class Status : Register8BitWith<Status.Flags>
    {
        public enum Flags : byte
        {
            VerticalBlank = 1 << 7,
        }
    }
}
