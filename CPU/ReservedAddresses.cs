using YaNES.Core.Utils;

namespace YaNES.CPU
{
    internal class ReservedAddresses
    {
        internal const ushort StackBottom = 0x0100;
        internal const byte StartingStackPointerAddress = 0xFD;

        internal static readonly AddressSpace CpuAddressSpace = new() { Start = 0x0000, End = 0x1FFF };
        internal static readonly AddressSpace PpuAddressSpace = new() { Start = 0x2000, End = 0x3FFF };
        internal static readonly AddressSpace PrgAddressSpace = new() { Start = 0x8000, End = 0xFFFF };
    }
}
