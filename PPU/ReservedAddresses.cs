using YaNES.Core.Utils;

namespace PPU
{
    internal class ReservedAddresses
    {
        internal static readonly AddressSpace ChrRomAddressSpace = new() { Start = 0x0000, End = 0x1FFF };
        internal static readonly AddressSpace RamAddressSpace = new() { Start = 0x2000, End = 0x2FFF };
        internal static readonly AddressSpace ForbiddenAddressSpace = new() { Start = 0x3000, End = 0x3EFF };
        internal static readonly AddressSpace PaletteAddressSpace = new() { Start = 0x3F00, End = 0x3FFF };
    }
}
