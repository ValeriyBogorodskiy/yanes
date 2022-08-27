namespace NesEmulatorCPU.Utils
{
    internal class ReservedAddresses
    {
        internal const ushort ProgramStartPointerAddress = 0xFFFC;

        internal const ushort StackBottom = 0x0100;
        internal const byte StartingStackPointerAddress = 0xFF;

        internal const ushort CpuRamStart = 0x0000;
        internal const ushort CpuRamEnd = 0x1FFF;

        internal const ushort PPURamStart = 0x2000;
        internal const ushort PPURamEnd = 0x3FFF;
    }
}
