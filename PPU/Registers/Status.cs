using YaNES.Core;

namespace PPU.Registers
{
    internal class Status : Register8BitWith<Status.Flags>
    {
        public enum Flags : byte
        {
            VerticalBlank = 1 << 7,
        }
    }
}
