namespace NesEmulatorCPU.Tools
{
    internal static class Extensions
    {
        public static bool IsNegative(this byte value) => (value & BitMasks.Negative) != 0;

        public static bool IsZero(this byte value) => value == 0;
    }
}
