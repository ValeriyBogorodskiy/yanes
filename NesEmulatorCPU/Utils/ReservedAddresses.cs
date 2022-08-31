namespace NesEmulatorCPU.Utils
{
    internal class ReservedAddresses
    {
        internal const ushort ProgramStartPointerAddress = 0xFFFC;

        internal const ushort StackBottom = 0x0100;
        internal const byte StartingStackPointerAddress = 0xFF;

        internal static readonly AddressSpace CPUAddressSpace = new() { Start = 0x0000, End = 0x1FFF };
        internal static readonly AddressSpace PPUAddressSpace = new() { Start = 0x2000, End = 0x3FFF };
        internal static readonly AddressSpace PRGAddressSpace = new() { Start = 0x8000, End = 0xFFFF };
    }
}
