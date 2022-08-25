namespace NesEmulatorCPU.Utils
{
    internal class ReservedAddresses
    {
        internal const ushort ProgramStartPointerAddress = 0xFFFC;
        internal const ushort StackBottom = 0x0100;
        internal const byte StartingStackPointerAddress = 0xFF;
    }
}
