using YaNES.Core.Utils;

namespace YaNES.CPU
{
    internal class ReservedAddresses
    {
        internal const ushort StackBottom = 0x0100;
        internal const byte StartingStackPointerAddress = 0xFD;

        internal static readonly AddressSpace CpuAddressSpace = new() { Start = 0x0000, End = 0x1FFF };

        internal static readonly AddressSpace PpuAddressSpace = new() { Start = 0x2000, End = 0x3FFF };
        internal static readonly ushort PpuOamDma = 0x4014;

        internal static readonly AddressSpace PrgAddressSpace = new() { Start = 0x8000, End = 0xFFFF };

        internal static readonly AddressSpace ApuAddressSpace = new() { Start = 0x4000, End = 0x4013 };
        internal static readonly ushort ApuExtraAddress = 0x4015;

        internal static readonly ushort Joypad1Address = 0x4016;
        internal static readonly ushort Joypad2Address = 0x4017;
    }
}
